using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeSource : GenericJsonObject<RecipeSource>
    {
        #region Direct Properties
        public Recipe ConnectedRecipe { get; private set; }
        private int? RawRecipeId;
        private bool IsRecipeIdParsed;
        public SourceTypes Type { get; private set; }
        public PowerSkill SkillTypeId { get; private set; }
        public Skill ConnectedSkillTypeId { get; private set; }
        private bool IsSkillTypeIdParsed;
        public Item ConnectedItem { get; private set; }
        private string RawItemName;
        private bool IsItemNameParsed;
        private int RawItemTypeId;
        private string RawNpcName;
        private string RawEffectName;
        public Quest ConnectedQuest { get; private set; }
        private string RawQuestName;
        private int? RawQuestId;
        private bool IsQuestNameParsed;
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override void InitializeKey(KeyValuePair<string, object> EntryRaw, ParseErrorInfo ErrorInfo)
        {
            base.InitializeKey(EntryRaw, ErrorInfo);

            int Index = Key.LastIndexOf('_');
            if (Index >= 0)
            {
                int ParsedRecipeId;
                if (int.TryParse(Key.Substring(Index + 1), out ParsedRecipeId))
                    RawRecipeId = ParsedRecipeId;
            }
        }

        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Type", ParseFieldType },
            { "SkillTypeId", ParseFieldSkillTypeId },
            { "ItemName", ParseFieldItemName },
            { "ItemTypeId", ParseFieldItemTypeId },
            { "NpcName", ParseFieldNpcName },
            { "EffectName", ParseFieldEffectName },
            { "QuestName", ParseFieldQuestName },
            { "QuestId", ParseFieldQuestId },
        };

        private static void ParseFieldType(RecipeSource This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawType;
            if ((RawType = Value as string) != null)
                This.ParseType(RawType, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource Type");
        }

        private void ParseType(string RawType, ParseErrorInfo ErrorInfo)
        {
            SourceTypes ParsedType;
            StringToEnumConversion<SourceTypes>.TryParse(RawType, out ParsedType, ErrorInfo);
            Type = ParsedType;
        }

        private static void ParseFieldSkillTypeId(RecipeSource This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawSkillTypeId;
            if ((RawSkillTypeId = Value as string) != null)
                This.ParseSkillTypeId(RawSkillTypeId, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource SkillTypeId");
        }

        private void ParseSkillTypeId(string RawSkillTypeId, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ParsedSkillTypeId;
            StringToEnumConversion<PowerSkill>.TryParse(RawSkillTypeId, out ParsedSkillTypeId, ErrorInfo);
            SkillTypeId = ParsedSkillTypeId;
        }

        private static void ParseFieldItemName(RecipeSource This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawItemName;
            if ((RawItemName = Value as string) != null)
                This.ParseItemName(RawItemName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource ItemName");
        }

        private void ParseItemName(string RawItemName, ParseErrorInfo ErrorInfo)
        {
            this.RawItemName = RawItemName;
        }

        private static void ParseFieldItemTypeId(RecipeSource This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseItemTypeId((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource ItemTypeId");
        }

        private void ParseItemTypeId(int RawItemTypeId, ParseErrorInfo ErrorInfo)
        {
            this.RawItemTypeId = RawItemTypeId;
        }

        private static void ParseFieldNpcName(RecipeSource This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawNpcName;
            if ((RawNpcName = Value as string) != null)
                This.ParseNpcName(RawNpcName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource NpcName");
        }

        private void ParseNpcName(string RawNpcName, ParseErrorInfo ErrorInfo)
        {
            this.RawNpcName = RawNpcName;
        }

        private static void ParseFieldEffectName(RecipeSource This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawEffectName;
            if ((RawEffectName = Value as string) != null)
                This.ParseEffectName(RawEffectName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource EffectName");
        }

        private void ParseEffectName(string RawEffectName, ParseErrorInfo ErrorInfo)
        {
            this.RawEffectName = RawEffectName;
        }

        private static void ParseFieldQuestName(RecipeSource This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawQuestName;
            if ((RawQuestName = Value as string) != null)
                This.ParseQuestName(RawQuestName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource QuestName");
        }

        private void ParseQuestName(string RawQuestName, ParseErrorInfo ErrorInfo)
        {
            this.RawQuestName = RawQuestName;
        }

        private static void ParseFieldQuestId(RecipeSource This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseQuestId((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource QuestId");
        }

        private void ParseQuestId(int RawQuestId, ParseErrorInfo ErrorInfo)
        {
            this.RawQuestId = RawQuestId;
        }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, TextMaps.SourceTypesTextMap[Type]);
                if (ConnectedRecipe != null)
                    AddWithFieldSeparator(ref Result, ConnectedRecipe.Name);
                if (ConnectedSkillTypeId != null)
                    AddWithFieldSeparator(ref Result, ConnectedSkillTypeId.Name);
                if (ConnectedItem != null)
                    AddWithFieldSeparator(ref Result, ConnectedItem.Name);
                if (ConnectedQuest != null)
                    AddWithFieldSeparator(ref Result, ConnectedQuest.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = false;

            if (RawRecipeId.HasValue)
            {
                ConnectedRecipe = PgJsonObjects.Recipe.ConnectByKey(ErrorInfo, RecipeTable, RawRecipeId.Value, ConnectedRecipe, ref IsRecipeIdParsed, ref IsConnected, null);
                if (ConnectedRecipe != null)
                    ConnectedRecipe.SetSource(this, ErrorInfo);
            }

            if (SkillTypeId != PowerSkill.Internal_None && SkillTypeId != PowerSkill.AnySkill && SkillTypeId != PowerSkill.Unknown)
                ConnectedSkillTypeId = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, SkillTypeId, ConnectedSkillTypeId, ref IsSkillTypeIdParsed, ref IsConnected, ConnectedRecipe);

            if (RawItemName != null)
                ConnectedItem = PgJsonObjects.Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItemName, ConnectedItem, ref IsItemNameParsed, ref IsConnected, ConnectedRecipe);

            if (RawQuestId.HasValue)
                ConnectedQuest = PgJsonObjects.Quest.ConnectByKey(ErrorInfo, QuestTable, RawQuestId.Value, ConnectedQuest, ref IsQuestNameParsed, ref IsConnected, ConnectedRecipe);
            else if (RawQuestName != null)
                ConnectedQuest = PgJsonObjects.Quest.ConnectSingleProperty(ErrorInfo, QuestTable, RawQuestName, ConnectedQuest, ref IsQuestNameParsed, ref IsConnected, ConnectedRecipe);

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "RecipeSource"; } }
        #endregion
    }
}
