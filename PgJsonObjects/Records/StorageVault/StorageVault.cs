using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class StorageVault : GenericJsonObject<StorageVault>
    {
        #region Direct Properties
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        private int? RawId;
        private bool IsGameNpcParsed;
        public GameNpc MatchingNpc { get; private set; }
        public string NpcFriendlyName { get; private set; }
        public MapAreaName Area { get; private set; }
        public int NumSlots { get { return RawNumSlots.HasValue ? RawNumSlots.Value : 0; } }
        public int? RawNumSlots { get; private set; }
        public bool HasAssociatedNpc { get { return RawHasAssociatedNpc.HasValue && RawHasAssociatedNpc.Value; } }
        public bool? RawHasAssociatedNpc { get; private set; }
        public Dictionary<Favor, int> FavorLevelTable { get; private set; } = new Dictionary<Favor, int>();
        public ItemKeyword RequiredItemKeyword { get; private set; }
        public string RequirementDescription { get; private set; }
        public MapAreaName Grouping { get; private set; }
        public string InteractionFlagRequirement { get; private set; }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return NpcFriendlyName; } }
        public const int SearchResultIconId = 2118;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "ID", ParseFieldID },
            { "NpcFriendlyName", ParseFieldNpcFriendlyName },
            { "Area", ParseFieldArea },
            { "NumSlots", ParseFieldNumSlots },
            { "HasAssociatedNpc", ParseFieldHasAssociatedNpc },
            { "Levels", ParseFieldLevels },
            { "RequiredItemKeyword", ParseFieldRequiredItemKeyword },
            { "RequirementDescription", ParseFieldRequirementDescription },
            { "Grouping", ParseFieldGrouping },
            { "Requirements", ParseFieldRequirements },
        };

        private static void ParseFieldID(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "StorageVault ID", This.ParseID);
        }

        private void ParseID(long RawId, ParseErrorInfo ErrorInfo)
        {
            this.RawId = (int)RawId;
        }

        private static void ParseFieldNpcFriendlyName(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "StorageVault NpcFriendlyName", This.ParseNpcFriendlyName);
        }

        private void ParseNpcFriendlyName(string RawNpcFriendlyName, ParseErrorInfo ErrorInfo)
        {
            NpcFriendlyName = RawNpcFriendlyName;
        }

        private static void ParseFieldArea(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "StorageVault Area", This.ParseArea);
        }

        private void ParseArea(string RawArea, ParseErrorInfo ErrorInfo)
        {
            if (RawArea == "*")
                return;

            if (!RawArea.StartsWith("Area"))
            {
                ErrorInfo.AddInvalidObjectFormat("StorageVault Area");
                return;
            }

            MapAreaName ParsedMapAreaName;
            StringToEnumConversion<MapAreaName>.TryParse(RawArea.Substring(4), TextMaps.MapAreaNameStringMap, out ParsedMapAreaName, ErrorInfo);
            Area = ParsedMapAreaName;
        }

        private static void ParseFieldNumSlots(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "StorageVault NumSlots", This.ParseNumSlots);
        }

        private void ParseNumSlots(long RawNumSlots, ParseErrorInfo ErrorInfo)
        {
            this.RawNumSlots = (int)RawNumSlots;
        }

        private static void ParseFieldHasAssociatedNpc(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseHasAssociatedNpc((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("StorageVault HasAssociatedNpc");
        }

        private void ParseHasAssociatedNpc(bool RawHasAssociatedNpc, ParseErrorInfo ErrorInfo)
        {
            this.RawHasAssociatedNpc = RawHasAssociatedNpc;
        }

        private static void ParseFieldLevels(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            JObject AsJObject;
            if ((AsJObject = Value as JObject) != null)
                This.ParseLevels(AsJObject, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("StorageVault Levels");
        }

        private void ParseLevels(JObject RawLevels, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, JToken> Entry in RawLevels)
            {
                int FavorLevel;
                JValue AsJValue;

                if (((AsJValue = Entry.Value as JValue) != null) && AsJValue.Type == JTokenType.Integer)
                    FavorLevel = (int)(long)AsJValue.Value;
                else
                {
                    ErrorInfo.AddInvalidObjectFormat("StorageVault Levels");
                    break;
                }

                Favor ParsedFavor;
                if (!StringToEnumConversion<Favor>.TryParse(Entry.Key, out ParsedFavor, ErrorInfo))
                    continue;

                FavorLevelTable.Add(ParsedFavor, FavorLevel);
            }
        }

        private static void ParseFieldRequiredItemKeyword(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "StorageVault RequiredItemKeyword", This.ParseRequiredItemKeyword);
        }

        private void ParseRequiredItemKeyword(string RawRequiredItemKeyword, ParseErrorInfo ErrorInfo)
        {
            ItemKeyword ParsedItemKeyword;
            StringToEnumConversion<ItemKeyword>.TryParse(RawRequiredItemKeyword, out ParsedItemKeyword, ErrorInfo);
            RequiredItemKeyword = ParsedItemKeyword;
        }

        private static void ParseFieldRequirementDescription(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "StorageVault RequirementDescription", This.ParseRequirementDescription);
        }

        private void ParseRequirementDescription(string RawRequirementDescription, ParseErrorInfo ErrorInfo)
        {
            RequirementDescription = RawRequirementDescription;
        }

        private static void ParseFieldGrouping(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "StorageVault Grouping", This.ParseRequirementDescription);
        }

        private void ParseGrouping(string RawGrouping, ParseErrorInfo ErrorInfo)
        {
            if (RawGrouping == "*")
                return;

            if (!RawGrouping.StartsWith("Area"))
            {
                ErrorInfo.AddInvalidObjectFormat("StorageVault Grouping");
                return;
            }

            MapAreaName ParsedGrouping;
            StringToEnumConversion<MapAreaName>.TryParse(RawGrouping.Substring(4), TextMaps.MapAreaNameStringMap, out ParsedGrouping, ErrorInfo);
            Grouping = ParsedGrouping;
        }

        private static void ParseFieldRequirements(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            JObject AsJObject;
            if ((AsJObject = Value as JObject) != null)
                This.ParseRequirements(AsJObject, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("StorageVault Requirements");
        }

        private void ParseRequirements(JObject RawRequirement, ParseErrorInfo ErrorInfo)
        {
            if (RawRequirement.ContainsKey("T"))
            {
                JValue AsJValue;
                if (((AsJValue = RawRequirement["T"] as JValue) != null) && AsJValue.Type == JTokenType.String)
                {
                    string RequirementType = AsJValue.Value as string;
                    if (RequirementType == "InteractionFlagSet")
                    {
                        if (RawRequirement.ContainsKey("InteractionFlag"))
                        {
                            JValue InteractionFlag = RawRequirement["InteractionFlag"] as JValue;
                            if (InteractionFlag != null && InteractionFlag.Type == JTokenType.String)
                            {
                                if (InteractionFlag.Value as string == "Ivyn_Gave_Passcode")
                                    InteractionFlagRequirement = "Ivyn Gave Passcode";
                                else if (InteractionFlag.Value as string == "Serbule2_TapestryInnChest")
                                    InteractionFlagRequirement = "Serbule Hills Tapestry Inn Chest";
                                else
                                    ErrorInfo.AddInvalidObjectFormat("StorageVault Requirements");
                            }
                            else
                                ErrorInfo.AddInvalidObjectFormat("StorageVault Requirements");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("StorageVault Requirements");
                    }
                    else
                        ErrorInfo.AddInvalidObjectFormat("StorageVault Requirements");
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("StorageVault Requirements");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("StorageVault Requirements");
        }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("NpcFriendlyName", NpcFriendlyName);

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, NpcFriendlyName);
                if (Area != MapAreaName.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.MapAreaNameTextMap[Area]);
                if (Grouping != MapAreaName.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.MapAreaNameTextMap[Grouping]);
                if (RawHasAssociatedNpc.HasValue)
                    AddWithFieldSeparator(ref Result, HasAssociatedNpc ? "HasAssociatedNpc:Yes" : "HasAssociatedNpc:No");

                foreach (KeyValuePair<Favor, int> Entry in FavorLevelTable)
                    AddWithFieldSeparator(ref Result, TextMaps.FavorTextMap[Entry.Key]);

                if (RequiredItemKeyword != ItemKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemKeywordTextMap[RequiredItemKeyword]);
                AddWithFieldSeparator(ref Result, RequirementDescription);
                AddWithFieldSeparator(ref Result, InteractionFlagRequirement);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> GameNpcTable = AllTables[typeof(GameNpc)];

            MatchingNpc = GameNpc.ConnectByKey(null, GameNpcTable, Key, MatchingNpc, ref IsGameNpcParsed, ref IsConnected, this);

            return IsConnected;
        }

        public static StorageVault ConnectByKey(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> StorageVaultTable, string StorageVaultKey, StorageVault ParsedStorageVault, ref bool IsRawStorageVaultParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawStorageVaultParsed)
                return ParsedStorageVault;

            IsRawStorageVaultParsed = true;

            if (StorageVaultKey == null)
                return null;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in StorageVaultTable)
            {
                StorageVault StorageVaultValue = Entry.Value as StorageVault;
                if (StorageVaultValue.Key == StorageVaultKey)
                {
                    IsConnected = true;
                    StorageVaultValue.AddLinkBack(LinkBack);
                    return StorageVaultValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(StorageVaultKey);

            return null;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "StorageVault"; } }
        #endregion
    }
}
