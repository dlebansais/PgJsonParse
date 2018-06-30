using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementSingleRace: GenericPgObject<PgAbilityRequirementSingleRace>, IPgAbilityRequirementSingleRace
    {
        public PgAbilityRequirementSingleRace(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementSingleRace CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementSingleRace CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementSingleRace(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public Race AllowedRace { get { return GetEnum<Race>(8); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
