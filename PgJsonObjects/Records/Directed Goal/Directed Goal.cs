using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class DirectedGoal : MainJsonObject<DirectedGoal>, IPgDirectedGoal
    {
        #region Direct Properties
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get; private set; }
        public string Label { get; private set; }
        public string Zone { get; private set; }
        public string LargeHint { get; private set; }
        public string SmallHint { get; private set; }
        public int CategoryGateId { get { return RawCategoryGateId.HasValue ? RawCategoryGateId.Value : 0; } }
        public int? RawCategoryGateId { get; private set; }
        public bool IsCategoryGate { get { return RawIsCategoryGate.HasValue ? RawIsCategoryGate.Value : false; } }
        public bool? RawIsCategoryGate { get; private set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return Label; } }
        public const int SearchResultIconId = 2118;
        public string SearchResultIconFileName { get { return "icon_" + DirectedGoal.SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Id", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawId = value,
                GetInteger = () => RawId } },
            { "Label", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseLabel,
                GetString = () => Label } },
            { "Zone", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Zone = value,
                GetString = () => Zone  } },
            { "IsCategoryGate", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsCategoryGate = value,
                GetBool = () => RawIsCategoryGate } },
            { "LargeHint", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => LargeHint = value,
                GetString = () => LargeHint } },
            { "SmallHint", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => SmallHint = value,
                GetString = () => SmallHint } },
            { "CategoryGateId", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawCategoryGateId = value,
                GetInteger = () => RawCategoryGateId } },
        }; } }

        private void ParseLabel(string RawLabel, ParseErrorInfo ErrorInfo)
        {
            Label = RawLabel;
            ErrorInfo.AddIconId(SearchResultIconId);
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Label);
                AddWithFieldSeparator(ref Result, Zone);
                AddWithFieldSeparator(ref Result, LargeHint);
                AddWithFieldSeparator(ref Result, SmallHint);

                if (RawIsCategoryGate.HasValue && RawIsCategoryGate.Value)
                    AddWithFieldSeparator(ref Result, "Is Category Gate");

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = false;

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "DirectedGoal"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddInt(RawId, data, ref offset, BaseOffset, 4);
            AddString(Label, data, ref offset, BaseOffset, 8, StoredStringtable);
            AddString(Zone, data, ref offset, BaseOffset, 12, StoredStringtable);
            AddString(LargeHint, data, ref offset, BaseOffset, 16, StoredStringtable);
            AddString(SmallHint, data, ref offset, BaseOffset, 20, StoredStringtable);
            AddInt(RawCategoryGateId, data, ref offset, BaseOffset, 24);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 28, StoredStringListTable);
            AddBool(RawIsCategoryGate, data, ref offset, ref BitOffset, BaseOffset, 32, 0);
            CloseBool(ref offset, ref BitOffset);

            FinishSerializing(data, ref offset, BaseOffset, 34, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
