using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class LoreBookInfo : MainJsonObject<LoreBookInfo>, IPgLoreBookInfo
    {
        #region Direct Properties
        public IPgLoreBookInfoCategory Gods { get; private set; }
        public IPgLoreBookInfoCategory Misc { get; private set; }
        public IPgLoreBookInfoCategory History { get; private set; }
        public IPgLoreBookInfoCategory Plot { get; private set; }
        public IPgLoreBookInfoCategory Stories { get; private set; }
        public IPgLoreBookInfoCategory GuideProgram { get; private set; }
        public IPgLoreBookInfoCategory NotesAndSigns { get; private set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return ""; } }
        public const int SearchResultIconId = 5792;
        public string SearchResultIconFileName { get { return "icon_" + LoreBookInfo.SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Gods", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Gods = JsonObjectParser<LoreBookInfoCategory>.Parse("Gods", value, errorInfo),
                GetObject = () => Gods as IObjectContentGenerator } },
            { "Misc", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Misc = JsonObjectParser<LoreBookInfoCategory>.Parse("Misc", value, errorInfo),
                GetObject = () => Misc as IObjectContentGenerator } },
            { "History", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => History = JsonObjectParser<LoreBookInfoCategory>.Parse("History", value, errorInfo),
                GetObject = () => History as IObjectContentGenerator } },
            { "Plot", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Plot = JsonObjectParser<LoreBookInfoCategory>.Parse("Plot", value, errorInfo),
                GetObject = () => Plot as IObjectContentGenerator } },
            { "Stories", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Stories = JsonObjectParser<LoreBookInfoCategory>.Parse("Stories", value, errorInfo),
                GetObject = () => Stories as IObjectContentGenerator } },
            { "GuideProgram", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GuideProgram = JsonObjectParser<LoreBookInfoCategory>.Parse("GuideProgram", value, errorInfo),
                GetObject = () => GuideProgram as IObjectContentGenerator } },
            { "NotesAndSigns", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NotesAndSigns = JsonObjectParser<LoreBookInfoCategory>.Parse("NotesAndSigns", value, errorInfo),
                GetObject = () => NotesAndSigns as IObjectContentGenerator } },
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
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
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddObject(Gods as ISerializableJsonObject, data, ref offset, BaseOffset, 4, StoredObjectTable);
            AddObject(Misc as ISerializableJsonObject, data, ref offset, BaseOffset, 8, StoredObjectTable);
            AddObject(History as ISerializableJsonObject, data, ref offset, BaseOffset, 12, StoredObjectTable);
            AddObject(Plot as ISerializableJsonObject, data, ref offset, BaseOffset, 16, StoredObjectTable);
            AddObject(Stories as ISerializableJsonObject, data, ref offset, BaseOffset, 20, StoredObjectTable);
            AddObject(GuideProgram as ISerializableJsonObject, data, ref offset, BaseOffset, 24, StoredObjectTable);
            AddObject(NotesAndSigns as ISerializableJsonObject, data, ref offset, BaseOffset, 28, StoredObjectTable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 32, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 36, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
