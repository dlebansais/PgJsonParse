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

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "UseVerb", new FieldParser() { Type = FieldType.String, ParseString = (string value, ParseErrorInfo errorInfo) => { UseVerb = StringToEnumConversion<ItemUseVerb>.Parse(value, TextMaps.UseVerbMap, errorInfo); }} },
            { "ServerInfo", new FieldParser() { Type = FieldType.ObjectArray, ParseObjectArray = ParseServerInfo } },
            { "UseRequirements", new FieldParser() { Type = FieldType.SimpleStringArray, ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => { StringToEnumConversion<ItemUseRequirement>.ParseList(value, UseRequirementList, errorInfo); }} },
            { "UseAnimation", new FieldParser() { Type = FieldType.String, ParseString = (string value, ParseErrorInfo errorInfo) => { UseAnimation = StringToEnumConversion<ItemUseAnimation>.Parse(value, errorInfo); }} },
            { "UseDelayAnimation", new FieldParser() { Type = FieldType.String, ParseString = (string value, ParseErrorInfo errorInfo) => { UseDelayAnimation = StringToEnumConversion<ItemUseAnimation>.Parse(value, errorInfo); }} },
            { "MetabolismCost", new FieldParser() { Type = FieldType.Integer, ParseInteger = (int value, ParseErrorInfo errorInfo) => { RawMetabolismCost = value; }} },
            { "UseDelay", new FieldParser() { Type = FieldType.Float, ParseFloat = (float value, ParseErrorInfo errorInfo) => { RawUseDelay = value; }} },
        }; } }

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
