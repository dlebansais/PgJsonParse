using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemBehavior : GenericJsonObject<ItemBehavior>, IPgItemBehavior
    {
        #region Direct Properties
        public IPgServerInfo ServerInfo { get; private set; }
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
        public override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        public void SetLinkBack(IBackLinkable LinkBack)
        {
            this.LinkBack = LinkBack;
            if (ServerInfo != null)
                (ServerInfo as ServerInfo).SetLinkBack(LinkBack);
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
                GetObjectArray = () => GenericJsonObject.CreateSingleOrEmptyList(ServerInfo),
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
                (ServerInfo as ServerInfo).SetLinkBack(LinkBack);
            }
            else
                ServerInfo = null;
        }

        private IBackLinkable LinkBack;
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
                    Result += (ServerInfo as ServerInfo).TextContent;
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
                IsConnected |= (ServerInfo as ServerInfo).Connect(ErrorInfo, Parent, AllTables);

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
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddObject(ServerInfo as ISerializableJsonObject, data, ref offset, BaseOffset, 4, StoredObjectTable);
            AddEnumList(UseRequirementList, data, ref offset, BaseOffset, 8, StoredEnumListTable);
            AddEnum(UseAnimation, data, ref offset, BaseOffset, 12);
            AddEnum(UseDelayAnimation, data, ref offset, BaseOffset, 14);
            AddInt(RawMetabolismCost, data, ref offset, BaseOffset, 16);
            AddDouble(RawUseDelay, data, ref offset, BaseOffset, 20);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 24, StoredStringListTable);
            AddEnum(UseVerb, data, ref offset, BaseOffset, 28);

            FinishSerializing(data, ref offset, BaseOffset, 30, StoredStringtable, StoredObjectTable, null, StoredEnumListTable, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
