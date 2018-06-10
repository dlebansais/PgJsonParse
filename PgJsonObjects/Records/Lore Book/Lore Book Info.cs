using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class LoreBookInfo : GenericJsonObject<LoreBookInfo>
    {
        #region Direct Properties
        public LoreBookInfoCategory Gods { get; private set; }
        public LoreBookInfoCategory Misc { get; private set; }
        public LoreBookInfoCategory History { get; private set; }
        public LoreBookInfoCategory Plot { get; private set; }
        public LoreBookInfoCategory Stories { get; private set; }
        public LoreBookInfoCategory GuideProgram { get; private set; }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return ""; } }
        public const int SearchResultIconId = 5792;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Gods", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Gods = JsonObjectParser<LoreBookInfoCategory>.Parse("Gods", value, errorInfo),
                GetObject = () => Gods } },
            { "Misc", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Misc = JsonObjectParser<LoreBookInfoCategory>.Parse("Misc", value, errorInfo),
                GetObject = () => Misc } },
            { "History", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => History = JsonObjectParser<LoreBookInfoCategory>.Parse("History", value, errorInfo),
                GetObject = () => History } },
            { "Plot", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Plot = JsonObjectParser<LoreBookInfoCategory>.Parse("Plot", value, errorInfo),
                GetObject = () => Plot } },
            { "Stories", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Stories = JsonObjectParser<LoreBookInfoCategory>.Parse("Stories", value, errorInfo),
                GetObject = () => Stories } },
            { "GuideProgram", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GuideProgram = JsonObjectParser<LoreBookInfoCategory>.Parse("GuideProgram", value, errorInfo),
                GetObject = () => GuideProgram } },
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
