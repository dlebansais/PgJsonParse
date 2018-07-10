using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementHasEffectKeyword: PgAbilityRequirement<PgAbilityRequirementHasEffectKeyword>, IPgAbilityRequirementHasEffectKeyword
    {
        public PgAbilityRequirementHasEffectKeyword(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementHasEffectKeyword CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementHasEffectKeyword CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementHasEffectKeyword(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public AbilityKeyword Keyword { get { return GetEnum<AbilityKeyword>(12); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.HasEffectKeyword) } },
            { "Keyword", new FieldParser() {
                Type = FieldType.String,
                GetString  = () => StringToEnumConversion<AbilityKeyword>.ToString(Keyword) } },
        }; } }
    }
}
