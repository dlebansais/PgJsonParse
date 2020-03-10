using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RaceQuestRequirement : QuestRequirement
    {
        public RaceQuestRequirement(OtherRequirementType OtherRequirementType, string allowedRace, string disallowedRace)
            : base(OtherRequirementType)
        {
            AllowedRace = allowedRace;
            DisallowedRace = disallowedRace;
        }

        public string AllowedRace { get; private set; }
        public string DisallowedRace { get; private set; }

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

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (AllowedRace != null)
                    Result = AllowedRace;
                else if (DisallowedRace != null)
                    Result = DisallowedRace;

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

            AddString(AllowedRace, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(DisallowedRace, data, ref offset, BaseOffset, 4, StoredStringtable);

            FinishSerializing(data, ref offset, BaseOffset, 8, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
