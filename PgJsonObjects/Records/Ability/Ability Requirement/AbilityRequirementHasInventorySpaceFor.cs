﻿using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirementHasInventorySpaceFor : AbilityRequirement, IPgAbilityRequirementHasInventorySpaceFor
    {
        public AbilityRequirementHasInventorySpaceFor(string RawItem)
        {
            this.RawItem = RawItem;
        }

        public override OtherRequirementType Type { get { return OtherRequirementType.HasInventorySpaceFor; } }
        public IPgItem Item { get; private set; }
        private string RawItem;
        private bool IsRawItemParsed;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
            { "Item", new FieldParser() {
                Type = FieldType.String,
                GetString  = () => Item != null ? Item.InternalName : null} },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (Item != null)
                    AddWithFieldSeparator(ref Result, Item.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];

            Item = PgJsonObjects.Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItem, Item, ref IsRawItemParsed, ref IsConnected, this);

            return IsConnected;
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            SerializeJsonObjectInternalProlog(data, ref offset, StoredStringtable, StoredStringListTable);
            int BaseOffset = offset;

            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddObject(Item as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 4, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}