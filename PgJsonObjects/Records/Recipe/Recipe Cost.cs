using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeCost : GenericJsonObject<RecipeCost>
    {
        #region Direct Properties
        public RecipeCurrency Currency { get; private set; }
        public double Price { get { return RawPrice.HasValue ? RawPrice.Value : 0; } }
        private double? RawPrice;
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Currency", new FieldParser() { Type = FieldType.String, ParseString = (string value, ParseErrorInfo errorInfo) => { Currency = StringToEnumConversion<RecipeCurrency>.Parse(value, errorInfo); }} },
            { "Price", new FieldParser() { Type = FieldType.Float, ParseFloat = (float value, ParseErrorInfo errorInfo) => { RawPrice = value; }} },
        }; } }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("Currency", StringToEnumConversion<RecipeCurrency>.ToString(Currency, null, RecipeCurrency.Internal_None));
            Generator.AddDouble("Price", RawPrice);

            Generator.CloseObject();
        }
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
    }
}
