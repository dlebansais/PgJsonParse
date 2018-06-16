﻿using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class LoreBookInfoCategory : GenericJsonObject<LoreBookInfoCategory>, IPgLoreBookInfoCategory
    {
        #region Direct Properties
        public string Title { get; private set; }
        public string SubTitle { get; private set; }
        public string SortTitle { get; private set; }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return ""; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Title", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Title = value,
                GetString = () => Title } },
            { "SubTitle", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => SubTitle = value,
                GetString = () => SubTitle } },
            { "SortTitle", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => SortTitle = value,
                GetString = () => SortTitle } },
        }; } }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Title);
                AddWithFieldSeparator(ref Result, SubTitle);
                AddWithFieldSeparator(ref Result, SortTitle);

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
        protected override string FieldTableName { get { return "LoreBookInfo Category"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();

            AddString(Title, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(SubTitle, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(SortTitle, data, ref offset, BaseOffset, 0, StoredStringtable);

            FinishSerializing(data, ref offset, BaseOffset, 68, StoredStringtable, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}