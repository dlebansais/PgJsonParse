using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeCost : GenericJsonObject<RecipeCost>, IPgRecipeCost
    {
        #region Direct Properties
        public double Price { get { return RawPrice.HasValue ? RawPrice.Value : 0; } }
        public double? RawPrice { get; private set; }
        public RecipeCurrency Currency { get; private set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Currency", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Currency = StringToEnumConversion<RecipeCurrency>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<RecipeCurrency>.ToString(Currency, null, RecipeCurrency.Internal_None) } },
            { "Price", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawPrice = value,
                GetFloat = () => RawPrice } },
        }; } }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (Currency != RecipeCurrency.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.RecipeCurrencyTextMap[Currency]);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            return false;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "RecipeCost"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddDouble(RawPrice, data, ref offset, BaseOffset, 4);
            AddEnum(Currency, data, ref offset, BaseOffset, 8);

            FinishSerializing(data, ref offset, BaseOffset, 10, StoredStringtable, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
