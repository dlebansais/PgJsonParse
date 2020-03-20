using System;
using System.Collections;
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
        public IPgDirectedGoal CategoryGate { get; private set; }
        public List<Race> ForRaceList { get; private set; } = new List<Race>();
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
                GetBool = () => CategoryGate != null ? null : (bool?)true} },
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
                GetInteger = () => CategoryGate != null ? CategoryGate.RawId : null } },
            { "ForRaces", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<Race>.ParseList(value, ForRaceList, errorInfo),
                GetStringArray = () => StringToEnumConversion<Race>.ToStringList(ForRaceList) } },
        }; } }

        private void ParseLabel(string RawLabel, ParseErrorInfo ErrorInfo)
        {
            Label = RawLabel;
            ErrorInfo.AddIconId(SearchResultIconId);
        }

        private bool? RawIsCategoryGate;
        private int? RawCategoryGateId;
        private bool IsRawCategoryGateIdParsed;
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

                foreach (Race Item in ForRaceList)
                    AddWithFieldSeparator(ref Result, TextMaps.RaceTextMap[Item]);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = false;

            Dictionary<string, IJsonKey> DirectedGoalTable = AllTables[typeof(DirectedGoal)];

            CategoryGate = DirectedGoal.ConnectSingleProperty(ErrorInfo, DirectedGoalTable, RawCategoryGateId, CategoryGate, ref IsRawCategoryGateIdParsed, ref IsConnected, this);

            if (CategoryGate != null)
                if (RawIsCategoryGate.HasValue && RawIsCategoryGate.Value == true)
                    ErrorInfo.AddInvalidObjectFormat("DirectedGoal " + Key);

            if (CategoryGate == null)
                if ((!RawIsCategoryGate.HasValue || !RawIsCategoryGate.Value))
                    ErrorInfo.AddInvalidObjectFormat("DirectedGoal " + Key);

            return IsConnected;
        }

        public static IPgDirectedGoal ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> DirectedGoalTable, int? RawDirectedGoalId, IPgDirectedGoal ParsedDirectedGoal, ref bool IsRawDirectedGoalParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawDirectedGoalParsed)
                return ParsedDirectedGoal;

            IsRawDirectedGoalParsed = true;

            if (!RawDirectedGoalId.HasValue)
                return null;

            foreach (KeyValuePair<string, IJsonKey> Entry in DirectedGoalTable)
            {
                DirectedGoal DirectedGoalValue = Entry.Value as DirectedGoal;
                if (DirectedGoalValue.RawId.HasValue && DirectedGoalValue.RawId.Value == RawDirectedGoalId.Value)
                {
                    IsConnected = true;
                    DirectedGoalValue.AddLinkBack(LinkBack);
                    return DirectedGoalValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(RawDirectedGoalId.Value.ToString());

            return null;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "DirectedGoal"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringTable = new Dictionary<int, string>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringTable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 4, StoredStringListTable);
            AddInt(RawId, data, ref offset, BaseOffset, 8);
            AddString(Label, data, ref offset, BaseOffset, 12, StoredStringTable);
            AddString(Zone, data, ref offset, BaseOffset, 16, StoredStringTable);
            AddString(LargeHint, data, ref offset, BaseOffset, 20, StoredStringTable);
            AddString(SmallHint, data, ref offset, BaseOffset, 24, StoredStringTable);
            AddObject(CategoryGate as ISerializableJsonObject, data, ref offset, BaseOffset, 28, StoredObjectTable);
            AddEnumList(ForRaceList, data, ref offset, BaseOffset, 32, StoredEnumListTable);

            FinishSerializing(data, ref offset, BaseOffset, 36, StoredStringTable, StoredObjectTable, null, StoredEnumListTable, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
