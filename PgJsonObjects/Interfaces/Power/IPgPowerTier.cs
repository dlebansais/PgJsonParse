namespace PgJsonObjects
{
    public interface IPgPowerTier
    {
        PowerEffectCollection EffectList { get; }
        int SkillLevelPrereq { get; }
        int? RawSkillLevelPrereq { get; }
    }
}
