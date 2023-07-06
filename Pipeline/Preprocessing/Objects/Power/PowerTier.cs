namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

public class PowerTier
{
    private const string IconTagStart = "<icon=";

    public PowerTier(int tier, RawPowerTier rawPowerTier)
    {
        EffectDescriptions = ParseEffectDescriptions(rawPowerTier.EffectDescs);
        MaxLevel = rawPowerTier.MaxLevel;
        MinLevel = rawPowerTier.MinLevel;
        MinRarity = rawPowerTier.MinRarity;
        SkillLevelPrerequirement = rawPowerTier.SkillLevelPrereq;
        Tier = tier;
    }

    private PowerEffect[]? ParseEffectDescriptions(string[]? rawEffectDescs)
    {
        if (rawEffectDescs is null)
            return null;

        List<PowerEffect> Result = new();
        foreach (string EffectDescription in rawEffectDescs)
            Result.Add(ParseEffectDescription(EffectDescription));

        // Fix a power.
        if (Result.Count == 1 && Result[0] is PowerEffect PowerEffect && PowerEffect.Description is string Description)
        {
            const string ToughHoofStartPattern = "Tough Hoof deals ";
            const string ToughHoofEndPattern = @" Trauma damage to the target each time they attack and damage you (within 8 seconds)";

            // Extract the damage.
            string Pattern = @$"{ToughHoofStartPattern}([0-9]+){ToughHoofEndPattern.Replace("(", @"\(").Replace(")", @"\)")}";
            Match Match = Regex.Match(Description, Pattern);

            if (Match.Success)
            {
                string MatchValue = Match.Value;
                string DamageString = MatchValue.Substring(ToughHoofStartPattern.Length, MatchValue.Length - ToughHoofStartPattern.Length - ToughHoofEndPattern.Length);

                decimal AttributeEffect = decimal.Parse(DamageString, CultureInfo.InvariantCulture);

                PowerEffect.AttributeName = "BOOST_ABILITYDOT_TOUGHHOOF";
                PowerEffect.AttributeEffect = AttributeEffect;
            }
        }

        return Result.ToArray();
    }

    private PowerEffect ParseEffectDescription(string rawEffectDesc)
    {
        PowerEffect Result = new();

        if (rawEffectDesc.StartsWith("{") && rawEffectDesc.EndsWith("}"))
        {
            string EffectString = rawEffectDesc.Substring(1, rawEffectDesc.Length - 2);
            Result = ParseAttributeEffectDescription(EffectString);
        }
        else if (!rawEffectDesc.Contains("{") && !rawEffectDesc.Contains("}"))
            Result = ParseSimpleEffectDescription(rawEffectDesc);
        else
            PreprocessorException.Throw(this);

        return Result;
    }

    private PowerEffect ParseAttributeEffectDescription(string effectString)
    {
        string[] Split = effectString.Split('{');
        if (Split.Length < 2 || Split.Length > 3)
            PreprocessorException.Throw(this);

        string AttributeName = Split[0];
        string AttributeEffectString = Split[1];
        string? AttributeSkill;

        if (!AttributeName.EndsWith("}"))
            PreprocessorException.Throw(this);

        AttributeName = AttributeName.Substring(0, AttributeName.Length - 1);
        if (AttributeName.Contains("{") || AttributeName.Contains("}"))
            PreprocessorException.Throw(this);

        if (AttributeName.Length == 0 || AttributeEffectString.Length == 0)
            PreprocessorException.Throw(this);

        if (Split.Length == 3)
        {
            if (!AttributeEffectString.EndsWith("}"))
                PreprocessorException.Throw(this);

            AttributeEffectString = AttributeEffectString.Substring(0, AttributeEffectString.Length - 1);
            AttributeSkill = Split[2];
        }
        else
            AttributeSkill = null;

        decimal AttributeEffect = decimal.Parse(AttributeEffectString, CultureInfo.InvariantCulture);

        return new PowerEffect() { AttributeName = AttributeName, AttributeEffect = AttributeEffect, AttributeSkill = AttributeSkill };
    }

    private PowerEffect ParseSimpleEffectDescription(string powerName)
    {
        List<int> IconIdList = new();
        string content = powerName;

        for (; ;)
        {
            // Search for the <icon=...> pattern.
            string IconTagPattern = @$"{IconTagStart}([a-zA-Z0-9#]+)>";
            Match IconTagMatch;

            IconTagMatch = Regex.Match(content, IconTagPattern);
            if (!IconTagMatch.Success)
                break;

            string MatchValue = IconTagMatch.Value;
            int MatchLength = IconTagMatch.Length;

            string IconIdString = MatchValue.Substring(IconTagStart.Length, MatchValue.Length - IconTagStart.Length - 1);
            int IconId = int.Parse(IconIdString);
            IconIdList.Add(IconId);

            content = content.Substring(MatchLength, content.Length - MatchLength);
        }

        string Description = content;

        // Fix bat stability icon.
        if (content.StartsWith("Bat Stability provides +") && content.EndsWith("% Projectile Evasion for 10 seconds") && IconIdList.Contains(3553))
        {
            IconIdList.Remove(3553);
            IconIdList.Add(3547);
        }

        // Fix Sic 'Em.
        bool IsSicEmFixed;
        if (Description.Contains("Sic Em"))
        {
            Description = Description.Replace("Sic Em", "Sic 'Em");
            IsSicEmFixed = true;
        }
        else
            IsSicEmFixed = false;

        int[]? IconIds = IconIdList.Count > 0 ? IconIdList.ToArray() : null;
        PowerEffect Result = new() { Description = Description, IconIds = IconIds };
        Result.SetIsSicEmFixed(IsSicEmFixed);

        return Result;
    }

    public PowerEffect[]? EffectDescriptions { get; set; }
    public int? MaxLevel { get; set; }
    public int? MinLevel { get; set; }
    public string? MinRarity { get; set; }
    public int? SkillLevelPrerequirement { get; set; }
    public int? Tier { get; set; }

    public RawPowerTier ToRawPowerTier()
    {
        RawPowerTier Result = new();

        Result.EffectDescs = ToRawEffectDescriptions(EffectDescriptions);
        Result.MaxLevel = MaxLevel;
        Result.MinLevel = MinLevel;
        Result.MinRarity = MinRarity;
        Result.SkillLevelPrereq = SkillLevelPrerequirement;

        return Result;
    }

    private static string[]? ToRawEffectDescriptions(PowerEffect[]? effectDescriptions)
    {
        if (effectDescriptions is null)
            return null;

        List<string> Result = new();
        foreach (PowerEffect EffectDescription in effectDescriptions)
            Result.Add(ToRawEffectDescription(EffectDescription));

        return Result.ToArray();
    }

    private static string ToRawEffectDescription(PowerEffect powerEffect)
    {
        if (powerEffect.Description is not null)
            return ToRawSimpleEffectDescription(powerEffect);
        else
            return ToRawAttributeEffectDescription(powerEffect);
    }

    private static string ToRawSimpleEffectDescription(PowerEffect powerEffect)
    {
        string Icons = string.Empty;
        string Description = powerEffect.Description ?? throw new NullReferenceException();

        if (powerEffect.IconIds is not null)
        {
            List<int> IconIdList = new(powerEffect.IconIds);

            // Fix bat stability icon.
            if (Description.StartsWith("Bat Stability provides +") && Description.EndsWith("% Projectile Evasion for 10 seconds") && IconIdList.Contains(3547))
            {
                IconIdList.Remove(3547);
                IconIdList.Add(3553);
            }

            foreach (int IconId in IconIdList)
                Icons += $"{IconTagStart}{IconId}>";
        }

        // Restore Sic 'Em typo.
        if (powerEffect.GetIsSicEmFixed())
            Description = Description.Replace("Sic 'Em", "Sic Em");

        string Result = $"{Icons}{Description}";

        return Result;
    }

    private static string ToRawAttributeEffectDescription(PowerEffect powerEffect)
    {
        string Result = $"{{{powerEffect.AttributeName}}}{{{powerEffect.AttributeEffect?.ToString(CultureInfo.InvariantCulture)}}}";

        if (powerEffect.AttributeSkill is not null)
            Result += $"{{{powerEffect.AttributeSkill}}}";

        return Result;
    }
}
