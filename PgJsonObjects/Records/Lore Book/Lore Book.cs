using Newtonsoft.Json.Linq;
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
        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Title", ParseFieldTitle },
            { "LocationHint", ParseFieldLocationHint },
            { "Category", ParseFieldCategory },
            { "Keywords", ParseFieldKeywords },
            { "IsClientLocal", ParseFieldIsClientLocal },
            { "Visibility", ParseFieldVisibility },
            { "InternalName", ParseFieldInternalName },
            { "Text", ParseFieldText },
        };

        private static void ParseFieldTitle(LoreBook This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "LoreBook Title", This.ParseTitle);
        }

        private void ParseTitle(string RawTitle, ParseErrorInfo ErrorInfo)
        {
            Title = RawTitle;
        }

        private static void ParseFieldLocationHint(LoreBook This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "LoreBook LocationHint", This.ParseLocationHint);
        }

        private void ParseLocationHint(string RawLocationHint, ParseErrorInfo ErrorInfo)
        {
            LocationHint = RawLocationHint;
        }

        private static void ParseFieldCategory(LoreBook This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "LoreBook Category", This.ParseCategory);
        }

        private void ParseCategory(string RawCategory, ParseErrorInfo ErrorInfo)
        {
            LoreBookCategory ParsedLoreBookCategory;
            StringToEnumConversion<LoreBookCategory>.TryParse(RawCategory, out ParsedLoreBookCategory, ErrorInfo);
            Category = ParsedLoreBookCategory;
        }

        private static void ParseFieldKeywords(LoreBook This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "LoreBook Keywords", This.ParseKeywords);
        }

        private bool ParseKeywords(string RawKeyword, ParseErrorInfo ErrorInfo)
        {
            LoreBookKeyword ParsedLoreBookKeyword;
            if (StringToEnumConversion<LoreBookKeyword>.TryParse(RawKeyword, out ParsedLoreBookKeyword, ErrorInfo))
            {
                KeywordList.Add(ParsedLoreBookKeyword);
                return true;
            }
            else
                return false;
        }

        private static void ParseFieldIsClientLocal(LoreBook This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseIsClientLocal((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("LoreBook IsClientLocal");
        }

        private void ParseIsClientLocal(bool RawIsClientLocal, ParseErrorInfo ErrorInfo)
        {
            this.RawIsClientLocal = RawIsClientLocal;
        }

        private static void ParseFieldVisibility(LoreBook This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "LoreBook Visibility", This.ParseVisibility);
        }

        private void ParseVisibility(string RawVisibility, ParseErrorInfo ErrorInfo)
        {
            LoreBookVisibility ParsedLoreBookVisibility;
            StringToEnumConversion<LoreBookVisibility>.TryParse(RawVisibility, out ParsedLoreBookVisibility, ErrorInfo);
            Visibility = ParsedLoreBookVisibility;
        }

        private static void ParseFieldInternalName(LoreBook This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "LoreBook InternalName", This.ParseInternalName);
        }

        private void ParseInternalName(string RawInternalName, ParseErrorInfo ErrorInfo)
        {
            InternalName = RawInternalName;
        }

        private static void ParseFieldText(LoreBook This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "LoreBook Text", This.ParseText);
        }

        private void ParseText(string RawText, ParseErrorInfo ErrorInfo)
        {
            Text = RawText;
        }
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
