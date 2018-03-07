using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace PgJsonObjects
{
    public class GameNpc : GenericJsonObject<GameNpc>
    {
        #region Direct Properties
        public string Name { get; private set; }
        public MapAreaName AreaName { get; private set; }
        public string AreaFriendlyName { get; private set; }
        public List<NpcPreference> LikeList { get; private set; } = new List<NpcPreference>();
        public List<NpcPreference> HateList { get; private set; } = new List<NpcPreference>();
        #endregion

        #region Indirect Properties
        private bool IsMatchingVaultParsed;
        public StorageVault MatchingVault { get; private set; }
        protected override string SortingName { get { return Name; } }
        public const int SearchResultIconId = 2118;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Name", ParseFieldName },
            { "AreaName", ParseFieldAreaName },
            { "AreaFriendlyName", ParseFieldAreaFriendlyName },
            { "Preferences", ParseFieldPreferences },
        };

        private static void ParseFieldName(GameNpc This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "GameNpc Name", This.ParseName);
        }

        private void ParseName(string RawName, ParseErrorInfo ErrorInfo)
        {
            Name = RawName;
        }

        private static void ParseFieldAreaName(GameNpc This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "GameNpc AreaName", This.ParseAreaName);
        }

        private void ParseAreaName(string RawAreaName, ParseErrorInfo ErrorInfo)
        {
            if (RawAreaName.StartsWith("Area"))
            {
                MapAreaName ParsedMapAreaName;
                StringToEnumConversion<MapAreaName>.TryParse(RawAreaName.Substring(4), TextMaps.MapAreaNameStringMap, out ParsedMapAreaName, ErrorInfo);
                AreaName = ParsedMapAreaName;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("GameNpc AreaName");
        }

        private static void ParseFieldAreaFriendlyName(GameNpc This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "GameNpc AreaFriendlyName", This.ParseAreaFriendlyName);
        }

        private void ParseAreaFriendlyName(string RawAreaFriendlyName, ParseErrorInfo ErrorInfo)
        {
            AreaFriendlyName = RawAreaFriendlyName;
        }

        private static void ParseFieldPreferences(GameNpc This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueArray(Value, ErrorInfo, "GameNpc Preferences", This.ParsePreferences);
        }

        private void ParsePreferences(ArrayList RawPreferences, ParseErrorInfo ErrorInfo)
        {
            foreach (object RawPreference in RawPreferences)
            {
                JObject AsJObject;
                if ((AsJObject = RawPreference as JObject) != null)
                    ParsePreferences(AsJObject, ErrorInfo);
                else
                    ErrorInfo.AddInvalidObjectFormat("GameNpc Preferences");
            }
        }

        private void ParsePreferences(JObject RawPreference, ParseErrorInfo ErrorInfo)
        {
            NpcPreference ParsedPreference;
            JsonObjectParser<NpcPreference>.InitAsSubitem("Preference", RawPreference, out ParsedPreference, ErrorInfo);

            if (ParsedPreference.Preference > 0)
                LikeList.Add(ParsedPreference);
            else if (ParsedPreference.Preference < 0)
                HateList.Add(ParsedPreference);
            else
                ErrorInfo.AddInvalidObjectFormat("GameNpc Preferences");
        }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("Name", Name);
            Generator.AddString("AreaFriendlyName", AreaFriendlyName);

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Name);
                if (AreaName != MapAreaName.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.MapAreaNameTextMap[AreaName]);
                AddWithFieldSeparator(ref Result, AreaFriendlyName);

                foreach (NpcPreference Preference in LikeList)
                    Result += Preference.TextContent;

                foreach (NpcPreference Preference in HateList)
                    Result += Preference.TextContent;

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];
            Dictionary<string, IGenericJsonObject> StorageVaultTable = AllTables[typeof(StorageVault)];

            List<Item> HatedGiftList = new List<Item>();

            foreach (NpcPreference Preference in HateList)
            {
                IsConnected = Preference.Connect(ErrorInfo, this, AllTables);
                foreach (Gift Gift in Preference.ItemFavorList)
                    if (!HatedGiftList.Contains(Gift.Item))
                        HatedGiftList.Add(Gift.Item);
            }

            foreach (NpcPreference Preference in LikeList)
            {
                IsConnected = Preference.Connect(ErrorInfo, this, AllTables);

                foreach (Item Item in HatedGiftList)
                    foreach (Gift Gift in Preference.ItemFavorList)
                        if (Gift.Item == Item)
                        {
                            Gift.SetHated();
                            break;
                        }
            }

            MatchingVault = StorageVault.ConnectByKey(null, StorageVaultTable, Key, MatchingVault, ref IsMatchingVaultParsed, ref IsConnected, this);

            return IsConnected;
        }

        public static GameNpc ConnectByKey(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> GameNpcTable, string GameNpcKey, GameNpc ParsedGameNpc, ref bool IsRawGameNpcParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawGameNpcParsed)
                return ParsedGameNpc;

            IsRawGameNpcParsed = true;

            if (GameNpcKey == null)
                return null;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in GameNpcTable)
            {
                GameNpc GameNpcValue = Entry.Value as GameNpc;
                if (GameNpcValue.Key == GameNpcKey)
                {
                    IsConnected = true;
                    GameNpcValue.AddLinkBack(LinkBack);
                    return GameNpcValue;
                }
            }

            //if (ErrorInfo != null)
            //    ErrorInfo.AddMissingKey(GameNpcKey);

            return null;
        }

        public static GameNpc ConnectByName(ParseErrorInfo ErrorInfo, Dictionary<string, GameNpc> GameNpcTable, string RawNpcName, GameNpc ParsedGameNpc, ref bool IsRawGameNpcParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawGameNpcParsed)
                return ParsedGameNpc;

            IsRawGameNpcParsed = true;

            if (RawNpcName == null)
                return null;

            foreach (KeyValuePair<string, GameNpc> Entry in GameNpcTable)
                if (Entry.Value.Name == RawNpcName)
                {
                    IsConnected = true;
                    Entry.Value.AddLinkBack(LinkBack);
                    return Entry.Value;
                }

            return null;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Npc"; } }
        #endregion
    }
}
