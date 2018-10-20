using Presentation;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PlayerTitle : MainJsonObject<PlayerTitle>, IPgPlayerTitle
    {
        public static Dictionary<string, string> KeyToTitleMap = new Dictionary<string, string>()
        {
            { "Title_5009", "Event_Halloween_CultistOfZhiaLian" },
            { "Title_5010", "Event_Halloween_SeniorCultistOfZhiaLian" },
        };

        public static Dictionary<string, int> TitleToKeyMap = new Dictionary<string, int>()
        {
            { "Event_Halloween_CultistOfZhiaLian", 5009 },
            { "Event_Halloween_SeniorCultistOfZhiaLian", 5010 },
        };

        #region Direct Properties
        public string Title { get; private set; }
        public string RawTitle { get; private set; }
        public string Tooltip { get; private set; }
        public List<TitleKeyword> KeywordList { get; } = new List<TitleKeyword>();
        public int? Id
        {
            get
            {
                int Result;
                if (Key.Length > 6 && int.TryParse(Key.Substring(6), out Result))
                    return Result;
                else
                    return null;
            }
        }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return Title; } }
        public const int SearchResultIconId = 5851;
        public string SearchResultIconFileName { get { return "icon_" + PlayerTitle.SearchResultIconId; } }
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = false;
            return IsConnected;
        }

        public static IPgPlayerTitle ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> PlayerTitleTable, int? PlayerTitleId, IPgPlayerTitle ParsedPlayerTitle, ref bool IsRawPlayerTitleParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawPlayerTitleParsed)
                return ParsedPlayerTitle;

            if (!PlayerTitleId.HasValue)
                return null;

            IsRawPlayerTitleParsed = true;
            string PlayerTitleName = "Title_" + PlayerTitleId.Value;

            foreach (KeyValuePair<string, IJsonKey> Entry in PlayerTitleTable)
            {
                PlayerTitle PlayerTitleValue = Entry.Value as PlayerTitle;
                if (Entry.Key == PlayerTitleName)
                {
                    IsConnected = true;
                    PlayerTitleValue.AddLinkBack(LinkBack);
                    return PlayerTitleValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(PlayerTitleName);

            return null;
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
