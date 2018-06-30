using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementIsNotInCombat: GenericPgObject<PgAbilityRequirementIsNotInCombat>, IPgAbilityRequirementIsNotInCombat
    {
        public PgAbilityRequirementIsNotInCombat(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsNotInCombat CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementIsNotInCombat CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementIsNotInCombat(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
