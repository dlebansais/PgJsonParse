using System.Collections.Generic;

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

        public override string Key { get { return GetString(4); } }
        public int MinCount { get { return RawMinCount.HasValue ? RawMinCount.Value : 0; } }
        public int? RawMinCount { get { return GetInt(8); } }
        public int MaxCount { get { return RawMaxCount.HasValue ? RawMaxCount.Value : 0; } }
        public int? RawMaxCount { get { return GetInt(12); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(16, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public AbilityKeyword Keyword { get { return GetEnum<AbilityKeyword>(20); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
