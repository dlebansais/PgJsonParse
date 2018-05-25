﻿using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

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
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Name", new FieldParser() { Type = FieldType.String, ParseString = (string value, ParseErrorInfo errorInfo) => { Name = value; }} },
            { "AreaName", new FieldParser() { Type = FieldType.String, ParseString = ParseAreaName } },
            { "AreaFriendlyName", new FieldParser() { Type = FieldType.String, ParseString = (string value, ParseErrorInfo errorInfo) => { AreaFriendlyName = value; }} },
            { "Preferences", new FieldParser() { Type = FieldType.ObjectArray, ParseObjectArray = ParsePreferences } },
        }; } }

        private void ParseAreaName(string RawAreaName, ParseErrorInfo ErrorInfo)
        {
            if (RawAreaName.StartsWith("Area"))
                AreaName = StringToEnumConversion<MapAreaName>.Parse(RawAreaName.Substring(4), TextMaps.MapAreaNameStringMap, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("GameNpc AreaName");
        }

        private void ParsePreferences(JsonObject RawPreference, ParseErrorInfo ErrorInfo)
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

        public override string ToString()
        {
            if (AreaName == MapAreaName.Internal_None)
                return "\"" + Name + "\"";
            else if (AreaFriendlyName != null)
                return "\"" + Name + "/" + AreaFriendlyName + "\"";
            else
                return "\"" + Name + "/??\"";
        }
        #endregion
    }
}
