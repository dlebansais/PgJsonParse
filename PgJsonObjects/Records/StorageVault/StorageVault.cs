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
            if (Value is int)
                This.ParseID((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("StorageVault ID");
        }

        private void ParseID(int RawId, ParseErrorInfo ErrorInfo)
        {
            this.RawId = RawId;
        }

        private static void ParseFieldNpcFriendlyName(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawNpcFriendlyName;
            if ((RawNpcFriendlyName = Value as string) != null)
                This.ParseNpcFriendlyName(RawNpcFriendlyName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("StorageVault NpcFriendlyName");
        }

        private void ParseNpcFriendlyName(string RawNpcFriendlyName, ParseErrorInfo ErrorInfo)
        {
            NpcFriendlyName = RawNpcFriendlyName;
        }

        private static void ParseFieldArea(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawArea;
            if ((RawArea = Value as string) != null)
                This.ParseArea(RawArea, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("StorageVault Area");
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
            if (Value is int)
                This.ParseNumSlots((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("StorageVault NumSlots");
        }

        private void ParseNumSlots(int RawNumSlots, ParseErrorInfo ErrorInfo)
        {
            this.RawNumSlots = RawNumSlots;
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
            Dictionary<string, object> RawLevels;
            if ((RawLevels = Value as Dictionary<string, object>) != null)
                This.ParseLevels(RawLevels, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("StorageVault Levels");
        }

        private void ParseLevels(Dictionary<string, object> RawLevels, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, object> Entry in RawLevels)
            {
                int FavorLevel;

                if (Entry.Value is int)
                    FavorLevel = (int)Entry.Value;
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
            string RawRequiredItemKeyword;
            if ((RawRequiredItemKeyword = Value as string) != null)
                This.ParseRequiredItemKeyword(RawRequiredItemKeyword, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("StorageVault RequiredItemKeyword");
        }

        private void ParseRequiredItemKeyword(string RawRequiredItemKeyword, ParseErrorInfo ErrorInfo)
        {
            ItemKeyword ParsedItemKeyword;
            StringToEnumConversion<ItemKeyword>.TryParse(RawRequiredItemKeyword, out ParsedItemKeyword, ErrorInfo);
            RequiredItemKeyword = ParsedItemKeyword;
        }

        private static void ParseFieldRequirementDescription(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawRequirementDescription;
            if ((RawRequirementDescription = Value as string) != null)
                This.ParseRequirementDescription(RawRequirementDescription, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("StorageVault RequirementDescription");
        }

        private void ParseRequirementDescription(string RawRequirementDescription, ParseErrorInfo ErrorInfo)
        {
            RequirementDescription = RawRequirementDescription;
        }

        private static void ParseFieldGrouping(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawGrouping;
            if ((RawGrouping = Value as string) != null)
                This.ParseGrouping(RawGrouping, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("StorageVault Grouping");
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
            Dictionary<string, object> AsDictionary;
            if ((AsDictionary = Value as Dictionary<string, object>) != null)
                This.ParseRequirements(AsDictionary, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("StorageVault Requirements");
        }

        private void ParseRequirements(Dictionary<string, object> RawRequirement, ParseErrorInfo ErrorInfo)
        {
            if (RawRequirement.ContainsKey("T"))
            {
                string RequirementType;
                if ((RequirementType = RawRequirement["T"] as string) != null)
                {
                    if (RequirementType == "InteractionFlagSet")
                    {
                        if (RawRequirement.ContainsKey("InteractionFlag"))
                        {
                            string InteractionFlag = RawRequirement["InteractionFlag"] as string;
                            if (InteractionFlag == "Ivyn_Gave_Passcode")
                            {
                                InteractionFlagRequirement = "Ivyn Gave Passcode";
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
