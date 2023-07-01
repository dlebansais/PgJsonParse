namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

internal class PowerTier
{
    private const string IconTagStart = "<icon=";

    public PowerTier(RawPowerTier rawPowerTier)
    {
        EffectDescriptions = ParseEffectDescriptions(rawPowerTier.EffectDescs);
        MaxLevel = rawPowerTier.MaxLevel;
        MinLevel = rawPowerTier.MinLevel;
        MinRarity = rawPowerTier.MinRarity;
        SkillLevelPrerequirement = rawPowerTier.SkillLevelPrereq;
    }

    private PowerEffect[]? ParseEffectDescriptions(string[]? content)
    {
        if (content is null)
            return null;

        List<PowerEffect> Result = new();
        foreach (string EffectDescription in content)
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

    private PowerEffect ParseEffectDescription(string content)
    {
        if (content.StartsWith("{") && content.EndsWith("}"))
        {
            string EffectString = content.Substring(1, content.Length - 2);
            return ParseAttributeEffectDescription(EffectString);
        }
        else if (content.Contains("{") || content.Contains("}"))
            throw new InvalidCastException();
        else
            return ParseSimpleEffectDescription(content);
    }

    private PowerEffect ParseAttributeEffectDescription(string effectString)
    {
        string[] Split = effectString.Split('{');
        if (Split.Length < 2 || Split.Length > 3)
            throw new InvalidCastException();

        string AttributeName = Split[0];
        string AttributeEffectString = Split[1];
        string? AttributeSkill;

        if (!AttributeName.EndsWith("}"))
            throw new InvalidCastException();

        AttributeName = AttributeName.Substring(0, AttributeName.Length - 1);
        if (AttributeName.Contains("{") || AttributeName.Contains("}"))
            throw new InvalidCastException();

        if (AttributeName.Length == 0 || AttributeEffectString.Length == 0)
            throw new InvalidCastException();

        if (Split.Length == 3)
        {
            if (!AttributeEffectString.EndsWith("}"))
                throw new InvalidCastException();

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

        // Fix bat stability icon.
        if (content.StartsWith("Bat Stability provides +") && content.EndsWith("% Projectile Evasion for 10 seconds") && IconIdList.Contains(3553))
        {
            IconIdList.Remove(3553);
            IconIdList.Add(3547);
        }

        int[]? IconIds = IconIdList.Count > 0 ? IconIdList.ToArray() : null;

        return new PowerEffect() { Description = content, IconIds = IconIds };
    }

    public PowerEffect[]? EffectDescriptions { get; set; }
    public int? MaxLevel { get; set; }
    public int? MinLevel { get; set; }
    public string? MinRarity { get; set; }
    public int? SkillLevelPrerequirement { get; set; }

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

    private string[]? ToRawEffectDescriptions(PowerEffect[]? effectDescriptions)
    {
        if (effectDescriptions is null)
            return null;

        List<string> Result = new();
        foreach (PowerEffect EffectDescription in effectDescriptions)
            Result.Add(ToRawEffectDescription(EffectDescription));

        return Result.ToArray();
    }

    private string ToRawEffectDescription(PowerEffect powerEffect)
    {
        if (powerEffect.Description is not null)
            return ToRawSimpleEffectDescription(powerEffect);
        else
            return ToRawAttributeEffectDescription(powerEffect);
    }

    private string ToRawSimpleEffectDescription(PowerEffect powerEffect)
    {
        string Icons = string.Empty;

        if (powerEffect.Description is string Description && powerEffect.IconIds is not null)
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

        string Result = $"{Icons}{powerEffect.Description}";

        return Result;
    }

    private string ToRawAttributeEffectDescription(PowerEffect powerEffect)
    {
        string Result = $"{{{powerEffect.AttributeName}}}{{{powerEffect.AttributeEffect?.ToString(CultureInfo.InvariantCulture)}}}";

        if (powerEffect.AttributeSkill is not null)
            Result += $"{{{powerEffect.AttributeSkill}}}";

        return Result;
    }
}
