using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementRace: GenericPgObject<PgAbilityRequirementRace>, IPgAbilityRequirementRace
    {
        public PgAbilityRequirementRace(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementRace CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementRace CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementRace(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public List<Race> AllowedRaceList { get { return GetEnumList(8, ref _AllowedRaceList); } } private List<Race> _AllowedRaceList;
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "AllowedRace", new FieldParser() {
                Type = FieldType.StringArray,
                GetStringArray = () => StringToEnumConversion<Race>.ToStringList(AllowedRaceList) } },
        }; } }
    }
}
