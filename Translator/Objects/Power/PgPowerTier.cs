namespace PgJsonObjects
{
    public class PgPowerTier
    {
        public PgPowerEffectCollection EffectList { get; } = new PgPowerEffectCollection();
        public int SkillLevelPrereq { get { return RawSkillLevelPrereq.HasValue ? RawSkillLevelPrereq.Value : 0; } }
        public int? RawSkillLevelPrereq { get; set; }
    }
}
