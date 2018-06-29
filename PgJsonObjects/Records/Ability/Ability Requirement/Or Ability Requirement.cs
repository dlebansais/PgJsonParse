﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class OrAbilityRequirement : AbilityRequirement, IPgAbilityRequirementOr
    {
        public OrAbilityRequirement(AbilityRequirementCollection OrList, string RawErrorMsg)
        {
            this.OrList = OrList;
            ErrorMsg = RawErrorMsg;
        }

        public AbilityRequirementCollection OrList { get; private set; }
        public string ErrorMsg { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.Or) } },
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

                return Result;
            }
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObjectCollection> StoredObjectListTable = new Dictionary<int, ISerializableJsonObjectCollection>();

            AddInt((int?)OtherRequirementType, data, ref offset, BaseOffset, 0);
            AddString(Key, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddObjectList(OrList, data, ref offset, BaseOffset, 8, StoredObjectListTable);
            AddString(ErrorMsg, data, ref offset, BaseOffset, 12, StoredStringtable);

            FinishSerializing(data, ref offset, BaseOffset, 16, StoredStringtable, null, null, null, null, null, null, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
