using System.Collections.Generic;

namespace PgJsonObjects
{
    public class DruidEventStateAbilityRequirement : AbilityRequirement, IPgAbilityRequirementDruidEventState
    {
        public DruidEventStateAbilityRequirement(string RawDisallowedState, ParseErrorInfo ErrorInfo)
        {
            DisallowedState ParsedDisallowedState;
            StringToEnumConversion<DisallowedState>.TryParse(RawDisallowedState, out ParsedDisallowedState, ErrorInfo);
            DisallowedState = ParsedDisallowedState;
        }

        public DisallowedState DisallowedState { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.DruidEventState) } },
            { "DisallowedStates", new FieldParser() {
                Type = FieldType.StringArray,
                GetStringArray = () => GenericJsonObject.CreateSingleOrEmptyStringList(StringToEnumConversion<DisallowedState>.ToString(DisallowedState)) } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, TextMaps.DisallowedStateTextMap[DisallowedState]);

                return Result;
            }
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddInt((int?)OtherRequirementType.DruidEventState, data, ref offset, BaseOffset, 0);
            AddString(Key, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 8, StoredStringListTable);
            AddEnum(DisallowedState, data, ref offset, BaseOffset, 12);

            FinishSerializing(data, ref offset, BaseOffset, 14, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
