using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class GameArea : MainJsonObject<GameArea>, IPgGameArea
    {
        #region Direct Properties
        public string FriendlyName { get; private set; }
        public string ShortFriendlyName { get; private set; }
        public MapAreaName KeyArea { get; private set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return Key; } }
        public const int SearchResultIconId = 2118;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override void InitializeKey(string key, int index, IJsonValue value, ParseErrorInfo ErrorInfo)
        {
            base.InitializeKey(key, index, value, ErrorInfo);

            if (Key.StartsWith("Area"))
                KeyArea = StringToEnumConversion<MapAreaName>.Parse(Key.Substring(4), TextMaps.MapAreaNameStringMap, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Area Key");
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "FriendlyName", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseFriendlyName,
                GetString = () => FriendlyName } },
            { "ShortFriendlyName", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => ShortFriendlyName = value,
                GetString = () => ShortFriendlyName } },
        }; } }

        private void ParseFriendlyName(string RawFriendlyName, ParseErrorInfo ErrorInfo)
        {
            FriendlyName = RawFriendlyName;
            if (KeyArea != MapAreaName.Internal_None && FriendlyName != TextMaps.MapAreaNameTextMap[KeyArea])
                ErrorInfo.AddInvalidObjectFormat("Area FriendlyName");
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, FriendlyName);
                AddWithFieldSeparator(ref Result, ShortFriendlyName);

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
        protected override string FieldTableName { get { return "Area"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();

            AddString(FriendlyName, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(ShortFriendlyName, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddEnum(KeyArea, data, ref offset, BaseOffset, 8);

            FinishSerializing(data, ref offset, BaseOffset, 10, StoredStringtable, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
