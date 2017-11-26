using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AdvancementTable : GenericJsonObject<AdvancementTable>
    {
        #region Direct Properties
        public Dictionary<int, Advancement> LevelTable { get; private set; }
        public string InternalName { get; private set; }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return InternalName; } }
        #endregion

        #region Parsing
        public override void Init(KeyValuePair<string, object> EntryRaw, ParseErrorInfo ErrorInfo)
        {
            InitializeKey(EntryRaw, ErrorInfo);

            LevelTable = new Dictionary<int, Advancement>();

            Dictionary<string, object> Levels;
            if ((Levels = EntryRaw.Value as Dictionary<string, object>) != null)
            {
                foreach (KeyValuePair<string, object> Level in Levels)
                {
                    string RawLevel = Level.Key;
                    if (RawLevel.StartsWith("Level_"))
                    {
                        int EntryLevel;
                        if (int.TryParse(RawLevel.Substring(6), out EntryLevel))
                        {
                            Dictionary<string, object> Fields;
                            if ((Fields = Level.Value as Dictionary<string, object>) != null)
                            {
                                Advancement ParsedAdvancement;
                                JsonObjectParser<Advancement>.InitAsSubitem(RawLevel, Fields, out ParsedAdvancement, ErrorInfo);
                                LevelTable.Add(EntryLevel, ParsedAdvancement);
                            }
                            else
                                ErrorInfo.AddInvalidObjectFormat("AdvancementTable: " + Key);

                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("AdvancementTable: " + Key + ", " + RawLevel);
                    }
                    else
                        ErrorInfo.AddInvalidObjectFormat("AdvancementTable: " + Key + ", " + RawLevel);
                }
            }
            else
                ErrorInfo.AddInvalidObjectFormat("AdvancementTable: " + Key);
        }

        protected override void InitializeKey(KeyValuePair<string, object> EntryRaw, ParseErrorInfo ErrorInfo)
        {
            base.InitializeKey(EntryRaw, ErrorInfo);

            int Index = Key.LastIndexOf('_');
            if (Index >= 0)
                InternalName = Key.Substring(Index + 1);
            else
                InternalName = Key;
        }

        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return null; } }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            foreach (KeyValuePair<int, Advancement> Level in LevelTable)
                Level.Value.GenerateObjectContent(Generator);

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get { return ""; }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, StorageVault> StorageVaultTable, Dictionary<string, AbilitySource> AbilitySourceTable)
        {
            return false;
        }

        public static AdvancementTable ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, AdvancementTable> AdvancementTableTable, string RawAdvancementTableName, AdvancementTable ParsedAdvancementTable, ref bool IsRawAdvancementTableParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawAdvancementTableParsed)
                return ParsedAdvancementTable;

            IsRawAdvancementTableParsed = true;

            if (RawAdvancementTableName == null)
                return null;

            foreach (KeyValuePair<string, AdvancementTable> Entry in AdvancementTableTable)
                if (Entry.Value.InternalName == RawAdvancementTableName)
                {
                    IsConnected = true;
                    //Entry.Value.AddLinkBack(LinkBack);
                    return Entry.Value;
                }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(RawAdvancementTableName);

            return null;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "AdvancementTable"; } }
        #endregion
    }
}
