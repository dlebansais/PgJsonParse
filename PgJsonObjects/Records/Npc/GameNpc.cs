using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class GameNpc : MainJsonObject<GameNpc>, IPgGameNpc
    {
        #region Direct Properties
        public string Name { get; private set; }
        public string AreaFriendlyName { get; private set; }
        public IPgNpcPreferenceCollection PreferenceList { get; private set; } = new NpcPreferenceCollection();
        public IPgNpcPreferenceCollection LikeList { get; private set; } = new NpcPreferenceCollection();
        public IPgNpcPreferenceCollection HateList { get; private set; } = new NpcPreferenceCollection();
        public MapAreaName AreaName { get; private set; }
        #endregion

        #region Indirect Properties
        private bool IsMatchingVaultParsed;
        public StorageVault MatchingVault { get; private set; }
        public override string SortingName { get { return Name; } }
        public const int SearchResultIconId = 2118;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Name", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Name = value,
                GetString = () => Name } },
            { "AreaName", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseAreaName,
                GetString = GetAreaName } },
            { "AreaFriendlyName", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => AreaFriendlyName = value,
                GetString = () => AreaFriendlyName } },
            { "Preferences", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = ParsePreferences,
                GetObjectArray= () => PreferenceList } },
        }; } }

        private void ParseAreaName(string value, ParseErrorInfo ErrorInfo)
        {
            if (value.StartsWith("Area"))
                AreaName = StringToEnumConversion<MapAreaName>.Parse(value.Substring(4), TextMaps.MapAreaNameStringMap, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("GameNpc AreaName");
        }

        private string GetAreaName()
        {
            if (AreaName != MapAreaName.Internal_None)
                return "area" + StringToEnumConversion<MapAreaName>.ToString(AreaName);
            else
                return null;
        }

        private void ParsePreferences(JsonObject RawPreference, ParseErrorInfo ErrorInfo)
        {
            NpcPreference ParsedPreference;
            JsonObjectParser<NpcPreference>.InitAsSubitem("Preference", RawPreference, out ParsedPreference, ErrorInfo);

            if (ParsedPreference != null)
            {
                PreferenceList.Add(ParsedPreference);

                if (ParsedPreference.Preference > 0)
                    LikeList.Add(ParsedPreference);
                else if (ParsedPreference.Preference < 0)
                    HateList.Add(ParsedPreference);
                else
                    ErrorInfo.AddInvalidObjectFormat("GameNpc Preferences");
            }
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

        public static IPgGameNpc ConnectByKey(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> GameNpcTable, string GameNpcKey, IPgGameNpc ParsedGameNpc, ref bool IsRawGameNpcParsed, ref bool IsConnected, IBackLinkable LinkBack)
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

        public static GameNpc ConnectByName(ParseErrorInfo ErrorInfo, Dictionary<string, GameNpc> GameNpcTable, string RawNpcName, GameNpc ParsedGameNpc, ref bool IsRawGameNpcParsed, ref bool IsConnected, IBackLinkable LinkBack)
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

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();
            Dictionary<int, IPgCollection> StoredObjectListTable = new Dictionary<int, IPgCollection>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(Name, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddString(AreaFriendlyName, data, ref offset, BaseOffset, 8, StoredStringtable);
            AddObjectList(PreferenceList, data, ref offset, BaseOffset, 12, StoredObjectListTable);
            AddObjectList(LikeList, data, ref offset, BaseOffset, 16, StoredObjectListTable);
            AddObjectList(HateList, data, ref offset, BaseOffset, 20, StoredObjectListTable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 24, StoredStringListTable);
            AddEnum(AreaName, data, ref offset, BaseOffset, 28);

            FinishSerializing(data, ref offset, BaseOffset, 30, StoredStringtable, null, null, null, null, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
