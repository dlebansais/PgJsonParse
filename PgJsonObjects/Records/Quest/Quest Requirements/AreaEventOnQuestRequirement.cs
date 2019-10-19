using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AreaEventOnQuestRequirement : QuestRequirement, IPgAreaEventOnQuestRequirement
    {
        public AreaEventOnQuestRequirement(OtherRequirementType OtherRequirementType, MapAreaName RequirementArea)
            : base(OtherRequirementType)
        {
            AreaName = RequirementArea;
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "AreaEvent", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<MapAreaName>.ToString(AreaName)} },
        }; } }

        public MapAreaName AreaName { get; private set; }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (AreaName != MapAreaName.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.MapAreaNameTextMap[AreaName]);

                return Result;
            }
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            SerializeJsonObjectInternalProlog(data, ref offset, StoredStringtable, StoredStringListTable);
            int BaseOffset = offset;

            AddEnum(AreaName, data, ref offset, BaseOffset, 0);

            FinishSerializing(data, ref offset, BaseOffset, 2, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
