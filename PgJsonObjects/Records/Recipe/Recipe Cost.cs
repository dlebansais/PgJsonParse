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
        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Currency", ParseFieldCurrency },
            { "Price", ParseFieldPrice },
        };

        private static void ParseFieldCurrency(RecipeCost This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawCurrency;
            if ((RawCurrency = Value as string) != null)
                This.ParseCurrency(RawCurrency, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeCost Currency");
        }

        private void ParseCurrency(string RawCurrency, ParseErrorInfo ErrorInfo)
        {
            RecipeCurrency ParsedCurrency;
            StringToEnumConversion<RecipeCurrency>.TryParse(RawCurrency, out ParsedCurrency, ErrorInfo);
            Currency = ParsedCurrency;
        }

        private static void ParseFieldPrice(RecipeCost This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParsePrice((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParsePrice(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeCost Price");
        }

        private void ParsePrice(double RawPrice, ParseErrorInfo ErrorInfo)
        {
            this.RawPrice = RawPrice;
        }
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

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            return false;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "RecipeCost"; } }
        #endregion
    }
}
