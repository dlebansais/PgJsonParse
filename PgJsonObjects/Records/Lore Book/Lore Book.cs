using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class LoreBook : GenericJsonObject<LoreBook>, ISearchableObject
    {
        #region Direct Properties
        public string Title { get; private set; }
        public string LocationHint { get; private set; }
        public List<LoreBookKeyword> KeywordList { get; private set; } = new List<LoreBookKeyword>();
        public LoreBookCategory Category { get; private set; }
        public LoreBookVisibility Visibility { get; private set; }
        public string InternalName { get; private set; }
        public string Text { get; private set; }
        public bool IsClientLocal { get { return RawIsClientLocal.HasValue ? RawIsClientLocal.Value : false; } }
        public bool? RawIsClientLocal { get; private set; }
        private bool IsKeywordListEmpty;
        #endregion

        #region Indirect Properties
        public virtual string SortingName { get { return Title; } }
        public const int SearchResultIconId = 5792;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Title", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Title = value,
                GetString = () => Title } },
            { "LocationHint", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => LocationHint = value,
                GetString = () => LocationHint } },
            { "Category", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Category = StringToEnumConversion<LoreBookCategory>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<LoreBookCategory>.ToString(Category, null, LoreBookCategory.Internal_None) } },
            { "Keywords", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<LoreBookKeyword>.ParseList(value, KeywordList, errorInfo),
                SetArrayIsEmpty = () => IsKeywordListEmpty = true,
                GetStringArray = () => StringToEnumConversion<LoreBookKeyword>.ToStringList(KeywordList),
                GetArrayIsEmpty = () => IsKeywordListEmpty } },
            { "IsClientLocal", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsClientLocal = value,
                GetBool = () => RawIsClientLocal  } },
            { "Visibility", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Visibility = StringToEnumConversion<LoreBookVisibility>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<LoreBookVisibility>.ToString(Visibility, null, LoreBookVisibility.Internal_None) } },
            { "InternalName", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => InternalName = value,
                GetString = () => InternalName } },
            { "Text", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Text = value,
                GetString = () => Text } },
        }; } }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Title );
                AddWithFieldSeparator(ref Result, LocationHint);
                if (Category != LoreBookCategory.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.LoreBookCategoryTextMap[Category]);
                foreach (LoreBookKeyword Keyword in KeywordList)
                    AddWithFieldSeparator(ref Result, TextMaps.LoreBookKeywordTextMap[Keyword]);
                if (RawIsClientLocal.HasValue && RawIsClientLocal.Value)
                    AddWithFieldSeparator(ref Result, "Is Client Local");
                if (Visibility != LoreBookVisibility.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.LoreBookVisibilityTextMap[Visibility]);
                AddWithFieldSeparator(ref Result, InternalName);
                AddWithFieldSeparator(ref Result, Text);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;

            return IsConnected;
        }

        public static LoreBook ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> LoreBookTable, int LoreBookId, LoreBook ParsedLoreBook, ref bool IsRawLoreBookParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawLoreBookParsed)
                return ParsedLoreBook;

            IsRawLoreBookParsed = true;
            string LoreBookName = "Book_" + LoreBookId;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in LoreBookTable)
            {
                LoreBook LoreBookValue = Entry.Value as LoreBook;
                if (Entry.Key == LoreBookName)
                {
                    IsConnected = true;
                    LoreBookValue.AddLinkBack(LinkBack);
                    return LoreBookValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(LoreBookName);

            return null;
        }

        public static LoreBook ConnectByInternalName(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> LoreBookTable, string RawLoreBookName, LoreBook ParsedLoreBook, ref bool IsRawLoreBookParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawLoreBookParsed)
                return ParsedLoreBook;

            IsRawLoreBookParsed = true;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in LoreBookTable)
            {
                LoreBook LoreBookValue = Entry.Value as LoreBook;
                if (LoreBookValue.InternalName == RawLoreBookName)
                {
                    IsConnected = true;
                    LoreBookValue.AddLinkBack(LinkBack);
                    return LoreBookValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(RawLoreBookName);

            return null;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "LoreBook"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();

            AddString(Title, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(LocationHint, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddEnumList(KeywordList, data, ref offset, BaseOffset, 8, StoredEnumListTable);
            AddEnum(Category, data, ref offset, BaseOffset, 12);
            AddEnum(Visibility, data, ref offset, BaseOffset, 14);
            AddString(InternalName, data, ref offset, BaseOffset, 16, StoredStringtable);
            AddString(Text, data, ref offset, BaseOffset, 20, StoredStringtable);
            AddBool(RawIsClientLocal, data, ref offset, ref BitOffset, BaseOffset, 24, 0);
            CloseBool(ref offset, ref BitOffset);

            FinishSerializing(data, ref offset, BaseOffset, 26, StoredStringtable, null, null, StoredEnumListTable, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
