using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeCost : GenericJsonObject<RecipeCost>
    {
        #region Constants
        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
        {
            { "Currency", ParseFieldCurrency },
            { "Price", ParseFieldPrice },
        };
        #endregion

        #region Properties
        public RecipeCurrency Currency { get; private set; }
        public double Price { get { return RawPrice.HasValue ? RawPrice.Value : 0; } }
        private double? RawPrice;

        public string CombinedCost
        {
            get
            {
                string Result = Price.ToString();

                switch (Currency)
                {
                    case RecipeCurrency.GuildCredits:
                        Result += " Guild Credit(s)";
                        break;

                    default:
                        Result += " " + Currency.ToString();
                        break;
                }

                return Result;
            }
        }
        #endregion

        #region Client Interface
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

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("Currency", StringToEnumConversion<RecipeCurrency>.ToString(Currency, null, RecipeCurrency.Internal_None));
            Generator.AddDouble("Price", RawPrice);

            Generator.CloseObject();
        }

        public override string TextContent
        {
            get
            {
                string Result = "";

                return Result;
            }
        }
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "RecipeCost"; } }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            return false;
        }
        #endregion
    }
}
