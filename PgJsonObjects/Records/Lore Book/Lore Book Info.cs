using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class LoreBookInfo : GenericJsonObject<LoreBookInfo>
    {
        #region Direct Properties
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return ""; } }
        public const int SearchResultIconId = 5792;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Gods", new FieldParser() { Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => { }} },
            { "Misc", new FieldParser() { Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => { }} },
            { "History", new FieldParser() { Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => { }} },
            { "Plot", new FieldParser() { Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => { }} },
            { "Stories", new FieldParser() { Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => { }} },
            { "GuideProgram", new FieldParser() { Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => { }} },
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
        protected override string FieldTableName { get { return "LoreBookInfo"; } }
        #endregion
    }
}
