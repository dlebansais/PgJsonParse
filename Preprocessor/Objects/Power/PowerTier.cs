namespace Preprocessor;

internal class PowerTier
{
    public PowerTier(RawPowerTier rawPowerTier)
    {
        EffectDescriptions = rawPowerTier.EffectDescs;
        MaxLevel = rawPowerTier.MaxLevel;
        MinLevel = rawPowerTier.MinLevel;
        MinRarity = rawPowerTier.MinRarity;
        SkillLevelPrerequirement = rawPowerTier.SkillLevelPrereq;
    }

    public string[]? EffectDescriptions { get; set; }
    public int? MaxLevel { get; set; }
    public int? MinLevel { get; set; }
    public string? MinRarity { get; set; }
    public int? SkillLevelPrerequirement { get; set; }

    public RawPowerTier ToRawPowerTier()
    {
        RawPowerTier Result = new();

        Result.EffectDescs = EffectDescriptions;
        Result.MaxLevel = MaxLevel;
        Result.MinLevel = MinLevel;
        Result.MinRarity = MinRarity;
        Result.SkillLevelPrereq = SkillLevelPrerequirement;

        return Result;
    }
}
