using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AdvancementTable : GenericJsonObject<AdvancementTable>
    {
        #region Properties
        public Dictionary<int, Advancement> LevelTable { get; private set; }
        #endregion

        #region Client Interface
        public override void Init(KeyValuePair<string, object> EntryRaw, ParseErrorInfo ErrorInfo)
        {
            InitializeKey(EntryRaw);

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

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            foreach (KeyValuePair<int, Advancement> Level in LevelTable)
                Level.Value.GenerateObjectContent(Generator);

            Generator.CloseObject();
        }

        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (KeyValuePair<int, Advancement> Entry in LevelTable)
                    Result += Entry.Value.TextContent + JsonGenerator.FieldSeparator;

                return Result;
            }
        }
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return null; } }
        protected override string FieldTableName { get { return "AdvancementTable"; } }

        protected override void InitializeFields()
        {
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable)
        {
            return false;
        }
        #endregion
    }
}
