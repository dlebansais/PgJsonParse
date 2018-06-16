using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class LoreBookInfo : GenericJsonObject<LoreBookInfo>, IPgLoreBookInfo
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

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, IGenericJsonObject> StoredObjectTable = new Dictionary<int, IGenericJsonObject>();

            AddObject(Gods, data, ref offset, BaseOffset, 0, StoredObjectTable);
            AddObject(Misc, data, ref offset, BaseOffset, 4, StoredObjectTable);
            AddObject(History, data, ref offset, BaseOffset, 8, StoredObjectTable);
            AddObject(Plot, data, ref offset, BaseOffset, 12, StoredObjectTable);
            AddObject(Stories, data, ref offset, BaseOffset, 16, StoredObjectTable);
            AddObject(GuideProgram, data, ref offset, BaseOffset, 20, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 24, null, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
