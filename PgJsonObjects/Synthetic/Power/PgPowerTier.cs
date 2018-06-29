namespace PgJsonObjects
{
    public class PgPowerTier : GenericPgObject<PgPowerTier>, IPgPowerTier
    {
        public PgPowerTier(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgPowerTier CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgPowerTier CreateNew(byte[] data, ref int offset)
        {
            return new PgPowerTier(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public PowerEffectCollection EffectList { get { return GetObjectList(4, ref _EffectList, PowerEffectCollection.CreateItem, () => new PowerEffectCollection()); } } private PowerEffectCollection _EffectList;
        public int SkillLevelPrereq { get { return RawSkillLevelPrereq.HasValue ? RawSkillLevelPrereq.Value : 0; } }
        public int? RawSkillLevelPrereq { get { return GetInt(8); } }
    }
}
