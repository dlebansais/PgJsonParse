namespace PgJsonObjects
{
    public class PgAbilityRequirementEquippedItemKeyword: GenericPgObject<PgAbilityRequirementEquippedItemKeyword>, IPgAbilityRequirementEquippedItemKeyword
    {
        public PgAbilityRequirementEquippedItemKeyword(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementEquippedItemKeyword CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementEquippedItemKeyword CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementEquippedItemKeyword(data, ref offset);
        }

        public int MinCount { get { return RawMinCount.HasValue ? RawMinCount.Value : 0; } }
        public int? RawMinCount { get { return GetInt(4); } }
        public int MaxCount { get { return RawMaxCount.HasValue ? RawMaxCount.Value : 0; } }
        public int? RawMaxCount { get { return GetInt(8); } }
        public AbilityKeyword Keyword { get { return GetEnum<AbilityKeyword>(8); } }
    }
}
