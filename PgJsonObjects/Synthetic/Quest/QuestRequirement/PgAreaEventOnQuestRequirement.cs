using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAreaEventOnQuestRequirement : PgQuestRequirement<PgAreaEventOnQuestRequirement>, IPgAreaEventOnQuestRequirement
    {
        public PgAreaEventOnQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAreaEventOnQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAreaEventOnQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgAreaEventOnQuestRequirement(data, ref offset);
        }

        public MapAreaName AreaName { get { return GetEnum<MapAreaName>(PropertiesOffset + 0); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "AreaEvent", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<MapAreaName>.ToString(AreaName)} },
        }; } }
    }
}
