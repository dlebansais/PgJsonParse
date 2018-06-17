using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class StorageVault : GenericJsonObject<StorageVault>, IPgStorageVault, ISearchableObject
    {
        #region Direct Properties
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get; private set; }
        public GameNpc MatchingNpc { get; private set; }
        public int NumSlots { get { return RawNumSlots.HasValue ? RawNumSlots.Value : 0; } }
        public int? RawNumSlots { get; private set; }
        public string RequirementDescription { get; private set; }
        public string InteractionFlagRequirement { get; private set; }
        public string NpcFriendlyName { get; private set; }
        public ItemKeyword RequiredItemKeyword { get; private set; }
        public MapAreaName Grouping { get; private set; }
        public bool HasAssociatedNpc { get { return RawHasAssociatedNpc.HasValue && RawHasAssociatedNpc.Value; } }
        public bool? RawHasAssociatedNpc { get; private set; }

        public Dictionary<Favor, int> FavorLevelTable { get; private set; } = new Dictionary<Favor, int>();
        private bool IsGameNpcParsed;
        private MapAreaName Area;
        #endregion

        #region Indirect Properties
        public virtual string SortingName { get { return NpcFriendlyName; } }
        public const int SearchResultIconId = 2118;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "ID", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawId = value,
                GetInteger = () => RawId } },
            { "NpcFriendlyName", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => NpcFriendlyName = value,
                GetString = () => NpcFriendlyName } },
            { "Area", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseArea,
                GetString = GetArea } },
            { "NumSlots", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawNumSlots = value,
                GetInteger = () => RawNumSlots } },
            { "HasAssociatedNpc", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawHasAssociatedNpc = value,
                GetBool = () => RawHasAssociatedNpc } },
            { "Levels", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = ParseLevels,
                GetObject = GetLevels } },
            { "RequiredItemKeyword", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RequiredItemKeyword = StringToEnumConversion<ItemKeyword>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(RequiredItemKeyword, null, ItemKeyword.Internal_None) } },
            { "RequirementDescription", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RequirementDescription = value,
                GetString = () => RequirementDescription } },
            { "Grouping", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseGrouping,
                GetString = GetGrouping } },
            { "Requirements", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = ParseRequirements,
                GetObject = GetRequirements } },
        }; } }

        private void ParseArea(string RawArea, ParseErrorInfo ErrorInfo)
        {
            if (RawArea == "*")
            {
                Area = MapAreaName.Several;
                StringToEnumConversion<MapAreaName>.SetCustomParsedEnum(Area);
            }

            else if (RawArea.StartsWith("Area"))
                Area = StringToEnumConversion<MapAreaName>.Parse(RawArea.Substring(4), ErrorInfo);

            else
                ErrorInfo.AddInvalidObjectFormat("StorageVault Area");
        }

        private string GetArea()
        {
            if (Area == MapAreaName.Several)
                return "*";
            else
                return "Area" + StringToEnumConversion<MapAreaName>.ToString(Area);
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

                if (!StringToEnumConversion<Favor>.TryParse(Entry.Key, out Favor ParsedFavor, ErrorInfo))
                    continue;

                FavorLevelTable.Add(ParsedFavor, FavorLevel);
            }
        }

        private IGenericJsonObject GetLevels()
        {
            FavorLevelDesc Result = new FavorLevelDesc();

            foreach (KeyValuePair<Favor, int> Entry in FavorLevelTable)
                Result.SetFavorLevel(Entry.Key, Entry.Value);
            
            return Result;
        }

        private void ParseGrouping(string RawGrouping, ParseErrorInfo ErrorInfo)
        {
            if (RawGrouping == "*")
            {
                Grouping = MapAreaName.Several;
                StringToEnumConversion<MapAreaName>.SetCustomParsedEnum(Grouping);
            }

            else if (RawGrouping.StartsWith("Area"))
                Grouping = StringToEnumConversion<MapAreaName>.Parse(RawGrouping.Substring(4), TextMaps.MapAreaNameStringMap, ErrorInfo);

            else
                ErrorInfo.AddInvalidObjectFormat("StorageVault Grouping");
        }

        private string GetGrouping()
        {
            if (Grouping == MapAreaName.Several)
                return "*";
            else
                return "Area" + StringToEnumConversion<MapAreaName>.ToString(Grouping, null);
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

        private IGenericJsonObject GetRequirements()
        {
            InteractionFlagSetAbilityRequirement Result;

            if (InteractionFlagRequirement == "Ivyn Gave Passcode")
                Result = new InteractionFlagSetAbilityRequirement("Ivyn_Gave_Passcode");
            else if (InteractionFlagRequirement == "Serbule Hills Tapestry Inn Chest")
                Result = new InteractionFlagSetAbilityRequirement("Serbule2_TapestryInnChest");
            else
                Result = null;

            if (Result != null)
            {
                List<string> FakeOrder = new List<string>() { "T", "InteractionFlag" };
                Result.CopyFieldTableOrder("Requirements", FakeOrder);
            }

            return Result;
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

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddInt(RawId, data, ref offset, BaseOffset, 0);
            AddObject(MatchingNpc, data, ref offset, BaseOffset, 4, StoredObjectTable);
            AddInt(RawNumSlots, data, ref offset, BaseOffset, 8);
            AddString(RequirementDescription, data, ref offset, BaseOffset, 12, StoredStringtable);
            AddString(InteractionFlagRequirement, data, ref offset, BaseOffset, 16, StoredStringtable);
            AddString(NpcFriendlyName, data, ref offset, BaseOffset, 20, StoredStringtable);
            AddEnum(RequiredItemKeyword, data, ref offset, BaseOffset, 24);
            AddEnum(Grouping, data, ref offset, BaseOffset, 26);
            AddBool(RawHasAssociatedNpc, data, ref offset, ref BitOffset, BaseOffset, 28, 0);
            CloseBool(ref offset, ref BitOffset);

            FinishSerializing(data, ref offset, BaseOffset, 30, StoredStringtable, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
