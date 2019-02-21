using System.Collections.Generic;

namespace PgJsonObjects
{
    public class HangOutCompletedQuestRequirement : QuestRequirement
    {
        public HangOutCompletedQuestRequirement(OtherRequirementType OtherRequirementType, string HangOut)
            : base(OtherRequirementType)
        {
            this.HangOut = HangOut;
        }

        public string HangOut { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "HangOut", new FieldParser() {
                Type = FieldType.String,
                GetString = () => HangOut } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (HangOut != null)
                    Result = HangOut;

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

            AddString(HangOut, data, ref offset, BaseOffset, 0, StoredStringtable);

            FinishSerializing(data, ref offset, BaseOffset, 4, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
