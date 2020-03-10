using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityAmmo : MainJsonObject<AbilityAmmo>, IPgAbilityAmmo
    {
        #region Direct Properties
        public ItemKeyword Keyword { get; private set; }
        public int Count { get { return RawCount.HasValue ? RawCount.Value : 0; } }
        public int? RawCount { get; private set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "ItemKeyword", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Keyword = StringToEnumConversion<ItemKeyword>.Parse(value, errorInfo),
                GetString  = () => StringToEnumConversion<ItemKeyword>.ToString(Keyword) } },
            { "Count", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawCount = value,
                GetInteger = () => RawCount } },
        }; } }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, TextMaps.ItemKeywordTextMap[Keyword]);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            return true;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "AbilityAmmo"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddInt(Count, data, ref offset, BaseOffset, 0);
            AddStringList(new List<string>(), data, ref offset, BaseOffset, 4, StoredStringListTable);
            AddEnum(Keyword, data, ref offset, BaseOffset, 8);

            FinishSerializing(data, ref offset, BaseOffset, 10, null, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
