using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementCurHealth: GenericPgObject<PgAbilityRequirementCurHealth>, IPgAbilityRequirementCurHealth
    {
        public PgAbilityRequirementCurHealth(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementCurHealth CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementCurHealth CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementCurHealth(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public double Health { get { return RawHealth.HasValue ? RawHealth.Value : 0; } }
        public double? RawHealth { get { return GetDouble(8); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Health", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => Health } },
        }; } }
    }
}
