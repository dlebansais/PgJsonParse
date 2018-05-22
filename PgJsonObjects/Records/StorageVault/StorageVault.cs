﻿using PgJsonReader;
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
            ParseFieldValueInteger(Value, ErrorInfo, "StorageVault ID", This.ParseID);
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
            ParseFieldValueInteger(Value, ErrorInfo, "StorageVault NumSlots", This.ParseNumSlots);
        }

        private void ParseNumSlots(long RawNumSlots, ParseErrorInfo ErrorInfo)
        {
            this.RawNumSlots = (int)RawNumSlots;
        }

        private static void ParseFieldHasAssociatedNpc(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueBool(Value, ErrorInfo, "StorageVault HasAssociatedNpc", This.ParseHasAssociatedNpc);
        }

        private void ParseHasAssociatedNpc(bool RawHasAssociatedNpc, ParseErrorInfo ErrorInfo)
        {
            this.RawHasAssociatedNpc = RawHasAssociatedNpc;
        }

        private static void ParseFieldLevels(StorageVault This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValue(Value, ErrorInfo, "StorageVault Levels", This.ParseLevels);
        }

        private void ParseLevels(JsonObject RawLevels, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, IJsonValue> Entry in RawLevels)
            {
                int FavorLevel;
                JsonInteger AsJValue;

                if ((AsJValue = Entry.Value as JsonInteger) != null)
                    FavorLevel = AsJValue.Number;
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
            ParseFieldValue(Value, ErrorInfo, "StorageVault Requirements", This.ParseRequirements);
        }

        private void ParseRequirements(JsonObject RawRequirement, ParseErrorInfo ErrorInfo)
        {
            if (RawRequirement.Has("T"))
            {
                JsonString AsJsonString;
                if ((AsJsonString = RawRequirement["T"] as JsonString) != null)
                {
                    string RequirementType = AsJsonString.String;
                    if (RequirementType == "InteractionFlagSet")
                    {
                        if (RawRequirement.Has("InteractionFlag"))
                        {
                            JsonString InteractionFlag;
                            if ((InteractionFlag = RawRequirement["InteractionFlag"] as JsonString) != null)
                            {
                                if (InteractionFlag.String == "Ivyn_Gave_Passcode")
                                    InteractionFlagRequirement = "Ivyn Gave Passcode";
                                else if (InteractionFlag.String == "Serbule2_TapestryInnChest")
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
