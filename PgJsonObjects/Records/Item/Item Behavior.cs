﻿using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemBehavior : GenericJsonObject<ItemBehavior>
    {
        #region Direct Properties
        public ServerInfo ServerInfo { get; private set; }
        public List<ItemUseRequirement> UseRequirementList { get; private set; } = new List<ItemUseRequirement>();
        public ItemUseAnimation UseAnimation { get; private set; }
        public ItemUseAnimation UseDelayAnimation { get; private set; }
        public int MetabolismCost { get { return RawMetabolismCost.HasValue ? RawMetabolismCost.Value : 0; } }
        public int? RawMetabolismCost { get; private set; }
        public double UseDelay { get { return RawUseDelay.HasValue ? RawUseDelay.Value : 0; } }
        public double? RawUseDelay { get; private set; }
        public ItemUseVerb UseVerb { get; private set; }
        private bool IsServerInfoEmpty;
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
            { "UseVerb", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => UseVerb = StringToEnumConversion<ItemUseVerb>.Parse(value, TextMaps.UseVerbMap, errorInfo),
                GetString = () => StringToEnumConversion<ItemUseVerb>.ToString(UseVerb, TextMaps.UseVerbMap, ItemUseVerb.Internal_None) } },
            { "ServerInfo", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = ParseServerInfo,
                SetArrayIsEmpty = () => IsServerInfoEmpty = true,
                GetObjectArray = () => CreateSingleOrEmptyList(ServerInfo),
                GetArrayIsEmpty = () => IsServerInfoEmpty,
                SimplifyArray = true } },
            { "UseRequirements", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<ItemUseRequirement>.ParseList(value, UseRequirementList, errorInfo),
                GetStringArray = () => StringToEnumConversion<ItemUseRequirement>.ToStringList(UseRequirementList) } },
            { "UseAnimation", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => UseAnimation = StringToEnumConversion<ItemUseAnimation>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<ItemUseAnimation>.ToString(UseAnimation, null, ItemUseAnimation.Internal_None) } },
            { "UseDelayAnimation", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => UseDelayAnimation = StringToEnumConversion<ItemUseAnimation>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<ItemUseAnimation>.ToString(UseDelayAnimation, null, ItemUseAnimation.Internal_None) } },
            { "MetabolismCost", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawMetabolismCost = value,
                GetInteger = () => RawMetabolismCost } },
            { "UseDelay", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawUseDelay = value,
                GetFloat = () => RawUseDelay } },
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

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, IGenericJsonObject> StoredObjectTable = new Dictionary<int, IGenericJsonObject>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();

            AddObject(ServerInfo, data, ref offset, BaseOffset, 0, StoredObjectTable);
            AddEnumList(UseRequirementList, data, ref offset, BaseOffset, 4, StoredEnumListTable);
            AddEnum(UseAnimation, data, ref offset, BaseOffset, 8);
            AddEnum(UseDelayAnimation, data, ref offset, BaseOffset, 10);
            AddInt(RawMetabolismCost, data, ref offset, BaseOffset, 12);
            AddDouble(RawUseDelay, data, ref offset, BaseOffset, 16);
            AddEnum(UseVerb, data, ref offset, BaseOffset, 20);

            FinishSerializing(data, ref offset, BaseOffset, 22, null, StoredObjectTable, null, StoredEnumListTable, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
