using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class LoreBook : GenericJsonObject<LoreBook>
    {
        #region Direct Properties
        public string Title { get; private set; }
        public string LocationHint { get; private set; }
        public LoreBookCategory Category { get; private set; }
        public List<LoreBookKeyword> KeywordList { get; private set; } = new List<LoreBookKeyword>();
        public bool IsClientLocal { get { return RawIsClientLocal.HasValue ? RawIsClientLocal.Value : false; } }
        public bool? RawIsClientLocal { get; private set; }
        public LoreBookVisibility Visibility { get; private set; }
        public string InternalName { get; private set; }
        public string Text { get; private set; }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return Title; } }
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
                GetStringArray = () => StringToEnumConversion<LoreBookKeyword>.ToStringList(KeywordList) } },
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

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.CloseObject();
        }
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
    }
}
