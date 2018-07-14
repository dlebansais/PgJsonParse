using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirementDruidEventState : AbilityRequirement, IPgAbilityRequirementDruidEventState
    {
        public AbilityRequirementDruidEventState(DisallowedState DisallowedState)
        {
            this.DisallowedState = DisallowedState;
        }

        public override OtherRequirementType Type { get { return OtherRequirementType.DruidEventState; } }
        public DisallowedState DisallowedState { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
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

                if (DisallowedState != DisallowedState.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.DisallowedStateTextMap[DisallowedState]);

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

            AddEnum(DisallowedState, data, ref offset, BaseOffset, 0);

            FinishSerializing(data, ref offset, BaseOffset, 2, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
