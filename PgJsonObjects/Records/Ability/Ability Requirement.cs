using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirement : GenericJsonObject<AbilityRequirement>
    {
        #region Constants
        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
        {
            { "T", ParseFieldT },
            { "Keyword", ParseFieldKeyword },
            { "Name", ParseFieldName },
            { "Item", ParseFieldItem },
            { "Count", ParseFieldCount },
        };
        #endregion

        #region Properties
        public OtherRequirementType T { get; private set; }
        public AbilityKeyword Keyword { get; private set; }
        public string Name { get; private set; }
        public Item Item { get; private set; }
        public double Count { get { return RawCount.HasValue ? RawCount.Value : 0; } }
        private double? RawCount;
        #endregion

        #region Client Interface
        private static void ParseFieldT(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawT;
            if ((RawT = Value as string) != null)
                This.ParseT(RawT, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement T");
        }

        private void ParseT(string RawT, ParseErrorInfo ErrorInfo)
        {
            OtherRequirementType ParsedT;
            StringToEnumConversion<OtherRequirementType>.TryParse(RawT, out ParsedT, ErrorInfo);
            T = ParsedT;
        }

        private static void ParseFieldKeyword(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawKeyword;
            if ((RawKeyword = Value as string) != null)
                This.ParseKeyword(RawKeyword, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Keyword");
        }

        private void ParseKeyword(string RawKeyword, ParseErrorInfo ErrorInfo)
        {
            AbilityKeyword ParsedKeyword;
            StringToEnumConversion<AbilityKeyword>.TryParse(RawKeyword, out ParsedKeyword, ErrorInfo);
            Keyword = ParsedKeyword;
        }

        private static void ParseFieldName(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawName;
            if ((RawName = Value as string) != null)
                This.ParseName(RawName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Name");
        }

        private void ParseName(string RawName, ParseErrorInfo ErrorInfo)
        {
            Name = RawName;
        }

        private static void ParseFieldItem(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawItem;
            if ((RawItem = Value as string) != null)
                This.ParseItem(RawItem, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Item");
        }

        private void ParseItem(string RawItem, ParseErrorInfo ErrorInfo)
        {
            this.RawItem = RawItem;
            IsRawItemParsed = false;
        }

        private static void ParseFieldCount(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseCount((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParseCount(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Count");
        }

        private void ParseCount(double RawCount, ParseErrorInfo ErrorInfo)
        {
            this.RawCount = RawCount;
        }

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", T.ToString());
            Generator.AddString("Keyword", Keyword.ToString());
            Generator.AddString("Name", Name);
            Generator.AddString("Item", RawItem);
            Generator.AddDouble("Count", RawCount);

            Generator.CloseObject();
        }

        private string RawItem;
        private bool IsRawItemParsed;
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "AbilityRequirement"; } }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable)
        {
            bool IsConnected = false;

            if (!IsRawItemParsed)
            {
                IsRawItemParsed = true;

                if (RawItem != null)
                {
                    foreach (KeyValuePair<string, Item> Entry in ItemTable)
                        if (Entry.Value.InternalName == RawItem)
                        {
                            Item = Entry.Value;
                            IsConnected = true;
                            break;
                        }

                    if (Item == null)
                        ErrorInfo.AddMissingKey(RawItem);
                }
            }

            return IsConnected;
        }
        #endregion
    }
}
