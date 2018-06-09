﻿using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PlayerTitle : GenericJsonObject<PlayerTitle>
    {
        #region Direct Properties
        public string Title { get; private set; }
        private string RawTitle;
        public string Tooltip { get; private set; }
        public List<TitleKeyword> KeywordList { get; } = new List<TitleKeyword>();
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return Title; } }
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "PlayerTitle"; } }
        #endregion
    }
}
