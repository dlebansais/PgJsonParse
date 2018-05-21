using PgJsonReader;
using System;
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

        protected override Dictionary<string, FieldValueHandler> FieldTable {  get; } = new Dictionary<string, FieldValueHandler>()
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
            ParseFieldValueString(Value, ErrorInfo, "ItemBehavior UseVerb", This.ParseUseVerb);
        }

        private void ParseUseVerb(string RawUseVerb, ParseErrorInfo ErrorInfo)
        {
            ItemUseVerb ParsedUseVerb;
            StringToEnumConversion<ItemUseVerb>.TryParse(RawUseVerb, TextMaps.UseVerbMap, out ParsedUseVerb, ErrorInfo);
            UseVerb = ParsedUseVerb;
        }

        private static void ParseFieldServerInfo(ItemBehavior This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringObjectOrArray(Value, ErrorInfo, "ItemBehavior ServerInfo", This.ParseServerInfo);
        }

        private void ParseServerInfo(JsonObject RawServerInfo, ParseErrorInfo ErrorInfo)
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
            ParseFieldValueStringArray(Value, ErrorInfo, "ItemBehavior UseRequirements", This.ParseUseRequirements);
        }

        private bool ParseUseRequirements(string RawUseRequirement, ParseErrorInfo ErrorInfo)
        {
            ItemUseRequirement ParsedUseRequirement;
            if (StringToEnumConversion<ItemUseRequirement>.TryParse(RawUseRequirement, out ParsedUseRequirement, ErrorInfo))
            {
                UseRequirementList.Add(ParsedUseRequirement);
                return true;
            }

            return false;
        }

        private static void ParseFieldUseAnimation(ItemBehavior This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "ItemBehavior UseAnimation", This.ParseUseAnimation);
        }

        private void ParseUseAnimation(string RawUseAnimation, ParseErrorInfo ErrorInfo)
        {
            ItemUseAnimation ParsedUseAnimation;
            StringToEnumConversion<ItemUseAnimation>.TryParse(RawUseAnimation, out ParsedUseAnimation, ErrorInfo);
            UseAnimation = ParsedUseAnimation;
        }

        private static void ParseFieldUseDelayAnimation(ItemBehavior This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "ItemBehavior UseDelayAnimation", This.ParseUseDelayAnimation);
        }

        private void ParseUseDelayAnimation(string RawUseDelayAnimation, ParseErrorInfo ErrorInfo)
        {
            ItemUseAnimation ParsedUseDelayAnimation;
            StringToEnumConversion<ItemUseAnimation>.TryParse(RawUseDelayAnimation, out ParsedUseDelayAnimation, ErrorInfo);
            UseDelayAnimation = ParsedUseDelayAnimation;
        }

        private static void ParseFieldMetabolismCost(ItemBehavior This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "ItemBehavior MetabolismCost", This.ParseMetabolismCost);
        }

        private void ParseMetabolismCost(long RawMetabolismCost, ParseErrorInfo ErrorInfo)
        {
            this.RawMetabolismCost = (int)RawMetabolismCost;
        }

        private static void ParseFieldUseDelay(ItemBehavior This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "ItemBehavior UseDelay", This.ParseUseDelay);
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;

            if (ServerInfo != null)
                IsConnected |= ServerInfo.Connect(ErrorInfo, Parent, AllTables);

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "ItemBehavior"; } }
        #endregion
    }
}
