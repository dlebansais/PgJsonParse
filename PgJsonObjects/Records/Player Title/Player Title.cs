using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PlayerTitle : MainJsonObject<PlayerTitle>, IPgPlayerTitle
    {
        #region Direct Properties
        public string Title { get; private set; }
        public string RawTitle { get; private set; }
        public string Tooltip { get; private set; }
        public List<TitleKeyword> KeywordList { get; } = new List<TitleKeyword>();
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return Title; } }
        public const int SearchResultIconId = 5851;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Title", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseTitle,
                GetString = () => RawTitle } },
            { "Tooltip", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Tooltip = value,
                GetString = () => Tooltip } },
            { "Keywords", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<TitleKeyword>.ParseList(value, KeywordList, errorInfo),
                GetStringArray = () => StringToEnumConversion<TitleKeyword>.ToStringList(KeywordList) } },
        }; } }

        private void ParseTitle(string value, ParseErrorInfo errorInfo)
        {
            RawTitle = value;
            Title = StripStringTags(value);
        }

        private string StripStringTags(string s)
        {
            int TagStartIndex, TagEndIndex;

            while ((TagStartIndex = s.IndexOf('<')) >= 0 && (TagEndIndex = s.IndexOf('>', TagStartIndex)) >= 0)
            {
                s = s.Substring(0, TagStartIndex) + s.Substring(TagEndIndex + 1);
            }

            return s;
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Title);
                AddWithFieldSeparator(ref Result, Tooltip);

                foreach (TitleKeyword Keyword in KeywordList)
                    AddWithFieldSeparator(ref Result, TextMaps.TitleKeywordTextMap[Keyword]);

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
        protected override string FieldTableName { get { return "PlayerTitle"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(Title, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddString(RawTitle, data, ref offset, BaseOffset, 8, StoredStringtable);
            AddString(Tooltip, data, ref offset, BaseOffset, 12, StoredStringtable);
            AddEnumList(KeywordList, data, ref offset, BaseOffset, 16, StoredEnumListTable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 20, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 24, StoredStringtable, null, null, StoredEnumListTable, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
