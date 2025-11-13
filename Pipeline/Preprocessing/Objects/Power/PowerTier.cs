namespace Preprocessor;

using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class PowerTier : IHasKey<int>, IHasParentKey<int>
{
    private const string IconTagStart = "<icon=";

    public PowerTier(int tier, RawPowerTier rawPowerTier)
    {
        EffectDescriptions = ParseEffectDescriptions(rawPowerTier.EffectDescs);
        EffectDescriptionsIsEmpty = EffectDescriptions is not null && EffectDescriptions.Length == 0;
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
        PowerEffect Result;

        if (rawEffectDesc.StartsWith("{") && rawEffectDesc.EndsWith("}"))
        {
            string EffectString = rawEffectDesc.Substring(1, rawEffectDesc.Length - 2);
            Result = ParseAttributeEffectDescription(EffectString);
        }
        else if (!rawEffectDesc.Contains("{") && !rawEffectDesc.Contains("}"))
            Result = ParseSimpleEffectDescription(rawEffectDesc);
        else
            throw new PreprocessorException(this);

        return Result;
    }

    private PowerEffect ParseAttributeEffectDescription(string effectString)
    {
        string[] Split = effectString.Split('{');
        if (Split.Length < 2 || Split.Length > 3)
            throw new PreprocessorException(this);

        string AttributeName = Split[0];
        string AttributeEffectString = Split[1];
        string? AttributeSkill;

        if (!AttributeName.EndsWith("}"))
            throw new PreprocessorException(this);

        AttributeName = AttributeName.Substring(0, AttributeName.Length - 1);
        Debug.Assert(!AttributeName.Contains("{"));
        if (AttributeName.Contains("}"))
            throw new PreprocessorException(this);

        if (AttributeName.Length == 0 || AttributeEffectString.Length == 0)
            throw new PreprocessorException(this);

        if (Split.Length == 3)
        {
            if (!AttributeEffectString.EndsWith("}"))
                throw new PreprocessorException(this);

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
        string Content = powerName;

        for (; ;)
        {
            // Search for the <icon=...> pattern.
            string IconTagPattern = @$"{IconTagStart}([a-zA-Z0-9#]+)>";
            Match IconTagMatch;

            IconTagMatch = Regex.Match(Content, IconTagPattern);
            if (!IconTagMatch.Success)
                break;

            string MatchValue = IconTagMatch.Value;
            int MatchLength = IconTagMatch.Length;

            string IconIdString = MatchValue.Substring(IconTagStart.Length, MatchValue.Length - IconTagStart.Length - 1);
            int IconId = int.Parse(IconIdString);
            IconIdList.Add(IconId);

            Content = Content.Substring(MatchLength, Content.Length - MatchLength);
        }

        string Description = Content;

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

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public int ParentKey { get; set; }

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public PowerEffect[]? EffectDescriptions { get; set; }

    [JsonIgnore]
    public bool EffectDescriptionsIsEmpty { get; set; }

    public int? MaxLevel { get; set; }

    public int? MinLevel { get; set; }

    public string? MinRarity { get; set; }

    public int? SkillLevelPrerequirement { get; set; }

    public int Tier { get; set; }

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
        if (powerEffect.Description is string Description)
            return ToRawSimpleEffectDescription(powerEffect, Description);
        else
            return ToRawAttributeEffectDescription(powerEffect);
    }

    private static string ToRawSimpleEffectDescription(PowerEffect powerEffect, string description)
    {
        string Icons = string.Empty;

        if (powerEffect.IconIds is not null)
        {
            List<int> IconIdList = new(powerEffect.IconIds);

            foreach (int IconId in IconIdList)
                Icons += $"{IconTagStart}{IconId}>";
        }

        // Restore Sic 'Em typo.
        if (powerEffect.GetIsSicEmFixed())
            description = description.Replace("Sic 'Em", "Sic Em");

        string Result = $"{Icons}{description}";

        return Result;
    }

    private static string ToRawAttributeEffectDescription(PowerEffect powerEffect)
    {
        string Result = $"{{{powerEffect.AttributeName}}}{{{powerEffect.AttributeEffect!.Value.ToString(CultureInfo.InvariantCulture)}}}";

        if (powerEffect.AttributeSkill is not null)
            Result += $"{{{powerEffect.AttributeSkill}}}";

        return Result;
    }
}
