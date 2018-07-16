using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirementOr : AbilityRequirement, IPgAbilityRequirementOr
    {
        public AbilityRequirementOr(IPgAbilityRequirementCollection orList, string errorMsg)
        {
            OrList = orList;
            ErrorMsg = errorMsg;
        }

        public override OtherRequirementType Type { get { return OtherRequirementType.Or; } }
        public IPgAbilityRequirementCollection OrList { get; private set; }
        public string ErrorMsg { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
            { "List", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => OrList } },
            { "ErrorMsg", new FieldParser() {
                Type = FieldType.String,
                GetString = () => ErrorMsg } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (AbilityRequirement Item in OrList)
                    AddWithFieldSeparator(ref Result, Item.TextContent);
                AddWithFieldSeparator(ref Result, ErrorMsg);

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

            Dictionary<int, IPgCollection> StoredObjectListTable = new Dictionary<int, IPgCollection>();

            AddObjectList(OrList, data, ref offset, BaseOffset, 0, StoredObjectListTable);
            AddString(ErrorMsg, data, ref offset, BaseOffset, 4, StoredStringtable);

            FinishSerializing(data, ref offset, BaseOffset, 8, StoredStringtable, null, null, null, null, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
