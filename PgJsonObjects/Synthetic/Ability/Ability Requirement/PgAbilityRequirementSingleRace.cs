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
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public Race AllowedRace { get { return GetEnum<Race>(12); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "AllowedRace", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<Race>.ToString(AllowedRace) } },
        }; } }
    }
}
