using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemBehavior : GenericJsonObject<ItemBehavior>
    {
        #region Direct Properties
        public ItemUseVerb UseVerb { get; private set; }
        public ServerInfo ServerInfo { get; private set; }
        public List<ItemUseRequirement> UseRequirementList { get; private set; } = new List<ItemUseRequirement>();
        public ItemUseAnimation UseAnimation { get; private set; }
        public ItemUseAnimation UseDelayAnimation { get; private set; }
        public int MetabolismCost { get { return RawMetabolismCost.HasValue ? RawMetabolismCost.Value : 0; } }
        public int? RawMetabolismCost { get; private set; }
        public double UseDelay { get { return RawUseDelay.HasValue ? RawUseDelay.Value : 0; } }
        public double? RawUseDelay { get; private set; }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        public void SetLinkBack(GenericJsonObject LinkBack)
        {
            this.LinkBack = LinkBack;
            if (ServerInfo != null)
                ServerInfo.SetLinkBack(LinkBack);
        }

        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "UseVerb", ParseFieldUseVerb},
            { "ServerInfo", ParseFieldServerInfo },
            { "UseRequirements", ParseFieldUseRequirements },
            { "UseAnimation", ParseFieldUseAnimation },
            { "UseDelayAnimation", ParseFieldUseDelayAnimation },
            { "MetabolismCost", ParseFieldMetabolismCost },
            { "UseDelay", ParseFieldUseDelay },
        };

        private static void ParseFieldUseVerb(ItemBehavior This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawUseVerb;
            if ((RawUseVerb = Value as string) != null)
                This.ParseUseVerb(RawUseVerb, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ItemBehavior UseVerb");
        }

        private void ParseUseVerb(string RawUseVerb, ParseErrorInfo ErrorInfo)
        {
            ItemUseVerb ParsedUseVerb;
            StringToEnumConversion<ItemUseVerb>.TryParse(RawUseVerb, TextMaps.UseVerbMap, out ParsedUseVerb, ErrorInfo);
            UseVerb = ParsedUseVerb;
        }

        private static void ParseFieldServerInfo(ItemBehavior This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawServerInfo;
            Dictionary<string, object> AsDictionary;

            if ((RawServerInfo = Value as ArrayList) != null)
                This.ParseServerInfo(RawServerInfo, ErrorInfo);
            else if ((AsDictionary = Value as Dictionary<string, object>) != null)
                This.ParseServerInfoAsDictionary(AsDictionary, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ItemBehavior ServerInfo");
        }

        private void ParseServerInfo(ArrayList RawServerInfo, ParseErrorInfo ErrorInfo)
        {
            foreach (object ServerInfo in RawServerInfo)
            {
                Dictionary<string, object> AsDictionary;
                if ((AsDictionary = ServerInfo as Dictionary<string, object>) != null)
                    ParseServerInfoAsDictionary(AsDictionary, ErrorInfo);
                else
                    ErrorInfo.AddInvalidObjectFormat("ItemBehavior ServerInfo");
            }
        }

        private void ParseServerInfoAsDictionary(Dictionary<string, object> RawServerInfo, ParseErrorInfo ErrorInfo)
        {
            ServerInfo ParsedServerInfo;
            JsonObjectParser<ServerInfo>.InitAsSubitem(Key, RawServerInfo, out ParsedServerInfo, ErrorInfo);

            if (ParsedServerInfo != null)
            {
                ServerInfo = ParsedServerInfo;
                ServerInfo.SetLinkBack(LinkBack);
            }
            else
                ServerInfo = null;
        }

        private static void ParseFieldUseRequirements(ItemBehavior This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawUseRequirements;
            if ((RawUseRequirements = Value as ArrayList) != null)
                This.ParseUseRequirements(RawUseRequirements, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ItemBehavior UseRequirements");
        }

        private void ParseUseRequirements(ArrayList RawUseRequirements, ParseErrorInfo ErrorInfo)
        {
            foreach (object RawUseRequirement in RawUseRequirements)
            {
                string UseRequirement;
                if ((UseRequirement = RawUseRequirement as string) != null)
                {
                    ItemUseRequirement ParsedUseRequirement;
                    StringToEnumConversion<ItemUseRequirement>.TryParse(UseRequirement, out ParsedUseRequirement, ErrorInfo);
                    UseRequirementList.Add(ParsedUseRequirement);
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("ItemBehavior UseRequirements");
            }
        }

        private static void ParseFieldUseAnimation(ItemBehavior This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawUseAnimation;
            if ((RawUseAnimation = Value as string) != null)
                This.ParseUseAnimation(RawUseAnimation, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ItemBehavior UseAnimation");
        }

        private void ParseUseAnimation(string RawUseAnimation, ParseErrorInfo ErrorInfo)
        {
            ItemUseAnimation ParsedUseAnimation;
            StringToEnumConversion<ItemUseAnimation>.TryParse(RawUseAnimation, out ParsedUseAnimation, ErrorInfo);
            UseAnimation = ParsedUseAnimation;
        }

        private static void ParseFieldUseDelayAnimation(ItemBehavior This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawUseDelayAnimation;
            if ((RawUseDelayAnimation = Value as string) != null)
                This.ParseUseDelayAnimation(RawUseDelayAnimation, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ItemBehavior UseDelayAnimation");
        }

        private void ParseUseDelayAnimation(string RawUseDelayAnimation, ParseErrorInfo ErrorInfo)
        {
            ItemUseAnimation ParsedUseDelayAnimation;
            StringToEnumConversion<ItemUseAnimation>.TryParse(RawUseDelayAnimation, out ParsedUseDelayAnimation, ErrorInfo);
            UseDelayAnimation = ParsedUseDelayAnimation;
        }

        private static void ParseFieldMetabolismCost(ItemBehavior This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseMetabolismCost((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ItemBehavior MetabolismCost");
        }

        private void ParseMetabolismCost(int RawMetabolismCost, ParseErrorInfo ErrorInfo)
        {
            this.RawMetabolismCost = RawMetabolismCost;
        }

        private static void ParseFieldUseDelay(ItemBehavior This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseUseDelay((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParseUseDelay(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ItemBehavior UseDelay");
        }

        private void ParseUseDelay(double RawUseDelay, ParseErrorInfo ErrorInfo)
        {
            this.RawUseDelay = RawUseDelay;
        }

        private GenericJsonObject LinkBack;
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

                if (UseVerb != ItemUseVerb.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemUseVerbTextMap[UseVerb]);
                if (ServerInfo != null)
                    Result += ServerInfo.TextContent;
                foreach (ItemUseRequirement Item in UseRequirementList)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemUseRequirementTextMap[Item]);
                if (UseAnimation != ItemUseAnimation.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemUseAnimationTextMap[UseAnimation]);
                if (UseDelayAnimation != ItemUseAnimation.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemUseAnimationTextMap[UseDelayAnimation]);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, AbilitySource> AbilitySourceTable)
        {
            bool IsConnected = false;

            if (ServerInfo != null)
                IsConnected |= ServerInfo.Connect(ErrorInfo, Parent, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, GameNpcTable, AbilitySourceTable);

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "ItemBehavior"; } }
        #endregion
    }
}
