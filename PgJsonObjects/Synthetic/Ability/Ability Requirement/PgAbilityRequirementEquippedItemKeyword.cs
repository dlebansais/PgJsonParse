namespace PgJsonObjects
{
    public class PgAbilityRequirementEquippedItemKeyword: GenericPgObject, IPgAbilityRequirementEquippedItemKeyword
    {
        public PgAbilityRequirementEquippedItemKeyword(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public int MinCount { get { return RawMinCount.HasValue ? RawMinCount.Value : 0; } }
        public int? RawMinCount { get { return GetInt(4); } }
        public int MaxCount { get { return RawMaxCount.HasValue ? RawMaxCount.Value : 0; } }
        public int? RawMaxCount { get { return GetInt(8); } }
        public AbilityKeyword Keyword { get { return GetEnum<AbilityKeyword>(8); } }
    }
}
