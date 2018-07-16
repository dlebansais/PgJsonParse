namespace PgJsonObjects
{
    public interface IPgPowerTier
    {
        IPgPowerEffectCollection EffectList { get; }
        int SkillLevelPrereq { get; }
        int? RawSkillLevelPrereq { get; }
    }
}
