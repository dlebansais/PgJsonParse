﻿using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class XpTable : MainJsonObject<XpTable>, IPgXpTable
    {
        #region Direct Properties
        public string InternalName { get; private set; }
        public XpTableLevelCollection XpAmountList { get; } = new XpTableLevelCollection();
        public XpTableEnum EnumName { get; private set; }
        private int TotalXp = 0;
        private int Level = 0;
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return InternalName; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "InternalName", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseInternalName,
                GetString = () => StringToEnumConversion<XpTableEnum>.ToString(EnumName, null, XpTableEnum.Internal_None) } },
            { "XpAmounts", new FieldParser() {
                Type = FieldType.SimpleIntegerArray,
                ParseSimpleIntegerArray = ParseXpAmounts,
                GetIntegerArray = GetXpAmounts } },
        }; } }

        private void ParseInternalName(string value, ParseErrorInfo ErrorInfo)
        {
            InternalName = value;
            EnumName = StringToEnumConversion<XpTableEnum>.Parse(value, ErrorInfo);
        }

        private void ParseXpAmounts(int value, ParseErrorInfo ErrorInfo)
        {
            TotalXp += value;
            Level++;
            XpAmountList.Add(new XpTableLevel(Level, value, TotalXp));
        }

        private List<int> GetXpAmounts()
        {
            List<int> Result = new List<int>();

            foreach (XpTableLevel Item in XpAmountList)
                Result.Add(Item.Xp);

            return Result;
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get { return ""; }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            return false;
        }

        public static IPgXpTable ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> XpTableTable, string RawXpTableName, IPgXpTable ParsedXpTable, ref bool IsRawXpTableParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawXpTableParsed)
                return ParsedXpTable;

            IsRawXpTableParsed = true;

            if (RawXpTableName == null)
                return null;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in XpTableTable)
            {
                XpTable XpTableValue = Entry.Value as XpTable;
                if (XpTableValue.InternalName == RawXpTableName)
                {
                    IsConnected = true;
                    return XpTableValue;
                }
            }

            ErrorInfo.AddMissingKey(RawXpTableName);
            return null;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "XpTable"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObjectCollection> StoredObjectListTable = new Dictionary<int, ISerializableJsonObjectCollection>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(InternalName, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddObjectList(XpAmountList, data, ref offset, BaseOffset, 8, StoredObjectListTable);
            AddEnum(EnumName, data, ref offset, BaseOffset, 12);

            FinishSerializing(data, ref offset, BaseOffset, 14, StoredStringtable, null, null, null, null, null, null, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
