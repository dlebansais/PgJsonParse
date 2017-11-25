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
            string RawName;
            if ((RawName = Value as string) != null)
                This.ParseName(RawName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Npc Name");
        }

        private void ParseName(string RawName, ParseErrorInfo ErrorInfo)
        {
            Name = RawName;
        }

        private static void ParseFieldAreaName(GameNpc This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawAreaName;
            if ((RawAreaName = Value as string) != null)
                This.ParseAreaName(RawAreaName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Npc AreaName");
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
                ErrorInfo.AddInvalidObjectFormat("Npc AreaName");
        }

        private static void ParseFieldAreaFriendlyName(GameNpc This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawAreaFriendlyName;
            if ((RawAreaFriendlyName = Value as string) != null)
                This.ParseAreaFriendlyName(RawAreaFriendlyName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Npc AreaFriendlyName");
        }

        private void ParseAreaFriendlyName(string RawAreaFriendlyName, ParseErrorInfo ErrorInfo)
        {
            AreaFriendlyName = RawAreaFriendlyName;
        }

        private static void ParseFieldPreferences(GameNpc This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawPreferences;
            if ((RawPreferences = Value as ArrayList) != null)
                This.ParsePreferences(RawPreferences, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Npc Preferences");
        }

        private void ParsePreferences(ArrayList RawPreferences, ParseErrorInfo ErrorInfo)
        {
            foreach (object RawPreference in RawPreferences)
            {
                Dictionary<string, object> AsDictionary;
                if ((AsDictionary = RawPreference as Dictionary<string, object>) != null)
                    ParsePreferences(AsDictionary, ErrorInfo);
                else
                    ErrorInfo.AddInvalidObjectFormat("Npc Preferences");
            }
        }

        private void ParsePreferences(Dictionary<string, object> RawPreference, ParseErrorInfo ErrorInfo)
        {
            NpcPreference ParsedPreference;
            JsonObjectParser<NpcPreference>.InitAsSubitem("Preference", RawPreference, out ParsedPreference, ErrorInfo);

            if (ParsedPreference.Preference > 0)
                LikeList.Add(ParsedPreference);
            else if (ParsedPreference.Preference < 0)
                HateList.Add(ParsedPreference);
            else
                ErrorInfo.AddInvalidObjectFormat("Npc Preferences");
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, AbilitySource> AbilitySourceTable)
        {
            bool IsConnected = false;

            foreach (NpcPreference Preference in LikeList)
                IsConnected = Preference.Connect(ErrorInfo, this, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, GameNpcTable, AbilitySourceTable);
            foreach (NpcPreference Preference in HateList)
                IsConnected = Preference.Connect(ErrorInfo, this, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, GameNpcTable, AbilitySourceTable);

            return IsConnected;
        }

        public static GameNpc ConnectByKey(ParseErrorInfo ErrorInfo, Dictionary<string, GameNpc> GameNpcTable, string GameNpcKey, GameNpc ParsedGameNpc, ref bool IsRawGameNpcParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawGameNpcParsed)
                return ParsedGameNpc;

            IsRawGameNpcParsed = true;

            if (GameNpcKey == null)
                return null;

            foreach (KeyValuePair<string, GameNpc> Entry in GameNpcTable)
                if (Entry.Value.Key == GameNpcKey)
                {
                    IsConnected = true;
                    Entry.Value.AddLinkBack(LinkBack);
                    return Entry.Value;
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
