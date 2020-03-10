using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRaceQuestRequirement : PgQuestRequirement<PgRaceQuestRequirement>, IPgRaceQuestRequirement
    {
        public PgRaceQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgRaceQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgRaceQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgRaceQuestRequirement(data, ref offset);
        }

        public string AllowedRace { get { return GetString(PropertiesOffset + 0); } }
        public string DisallowedRace { get { return GetString(PropertiesOffset + 4); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "AllowedRace", new FieldParser() {
                Type = FieldType.String,
                GetString = () => AllowedRace } },
            { "DisallowedRace", new FieldParser() {
                Type = FieldType.String,
                GetString = () => DisallowedRace } },
        }; } }
    }
}
