using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementIsLycanthrope: GenericPgObject<PgAbilityRequirementIsLycanthrope>, IPgAbilityRequirementIsLycanthrope
    {
        public PgAbilityRequirementIsLycanthrope(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsLycanthrope CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementIsLycanthrope CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementIsLycanthrope(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
