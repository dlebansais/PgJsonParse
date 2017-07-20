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
        private int? RawItemTypeId;
        private string RawNpcName;
        public Effect ConnectedEffect { get; private set; }
        private string RawEffectName;
        private bool IsEffectParsed;
        public Quest ConnectedQuest { get; private set; }
        private string RawQuestName;
        private int? RawQuestId;
        private bool IsQuestNameParsed;
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }

        public override void SetIndirectProperties(Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, ParseErrorInfo ErrorInfo)
        {
            if (ConnectedRecipe != null)
            {
                GenericSource ConvertedSource = ToSpecificSource(ErrorInfo);
                if (ConvertedSource != null)
                    ConnectedRecipe.SetSource(ConvertedSource, ErrorInfo);
            }
        }

        private GenericSource ToSpecificSource(ParseErrorInfo ErrorInfo)
        {
            switch (Type)
            {
                case SourceTypes.AutomaticFromSkill:
                    if (ConnectedSkillTypeId != null)
                        return new SkillupSource(ConnectedSkillTypeId);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Recipe Source (AutomaticFromSkill)");
                        return null;
                    }

                case SourceTypes.Item:
                    if (ConnectedItem != null)
                        return new ItemSource(ConnectedItem);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Recipe Source (Item)");
                        return null;
                    }

                case SourceTypes.Training:
                    if (RawNpcName != null)
                        return new TrainingSource(RawNpcName);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Recipe Source (Training)");
                        return null;
                    }

                case SourceTypes.Effect:
                    if (ConnectedEffect != null)
                        return new EffectSource(ConnectedEffect);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Recipe Source (Effect)");
                        return null;
                    }

                case SourceTypes.Quest:
                    if (ConnectedQuest != null)
                        return new QuestSource(ConnectedQuest);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Recipe Source (Quest)");
                        return null;
                    }

                case SourceTypes.Gift:
                    if (RawNpcName != null)
                        return new GiftSource(RawNpcName);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Recipe Source (Gift)");
                        return null;
                    }

                case SourceTypes.HangOut:
                    if (RawNpcName != null)
                        return new HangOutSource(RawNpcName);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Recipe Source (HangOut)");
                        return null;
                    }

                default:
                    return null;
            }
        }
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
            if (Type == SourceTypes.AutomaticFromSkill)
            {
                PowerSkill ParsedSkillTypeId;
                StringToEnumConversion<PowerSkill>.TryParse(RawSkillTypeId, out ParsedSkillTypeId, ErrorInfo);
                SkillTypeId = ParsedSkillTypeId;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource SkillTypeId (type)");
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
            if (Type == SourceTypes.Item)
                this.RawItemName = RawItemName;
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource ItemName (type)");
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
            if (Type == SourceTypes.Item)
                this.RawItemTypeId = RawItemTypeId;
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource ItemTypeId (type)");
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
            if (Type == SourceTypes.Training || Type == SourceTypes.Gift || Type == SourceTypes.HangOut)
                this.RawNpcName = RawNpcName;
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource NpcName (type)");
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
            if (Type == SourceTypes.Effect)
                this.RawEffectName = RawEffectName;
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource EffectName (type)");
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
            if (Type == SourceTypes.Quest)
                this.RawQuestName = RawQuestName;
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource QuestName (type)");
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
            if (Type == SourceTypes.Quest)
                this.RawQuestId = RawQuestId;
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource QuestId (type)");
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
                if (ConnectedEffect != null)
                    AddWithFieldSeparator(ref Result, ConnectedEffect.Name);
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
                ConnectedRecipe = PgJsonObjects.Recipe.ConnectByKey(ErrorInfo, RecipeTable, RawRecipeId.Value, ConnectedRecipe, ref IsRecipeIdParsed, ref IsConnected, null);

            if (SkillTypeId != PowerSkill.Internal_None && SkillTypeId != PowerSkill.AnySkill && SkillTypeId != PowerSkill.Unknown)
                ConnectedSkillTypeId = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, SkillTypeId, ConnectedSkillTypeId, ref IsSkillTypeIdParsed, ref IsConnected, ConnectedRecipe);

            if (RawItemTypeId.HasValue)
                ConnectedItem = PgJsonObjects.Item.ConnectByKey(ErrorInfo, ItemTable, RawItemTypeId.Value, ConnectedItem, ref IsItemNameParsed, ref IsConnected, ConnectedRecipe);
            else if (RawItemName != null)
                ConnectedItem = PgJsonObjects.Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItemName, ConnectedItem, ref IsItemNameParsed, ref IsConnected, ConnectedRecipe);

            if (RawEffectName != null)
                ConnectedEffect = PgJsonObjects.Effect.ConnectSingleProperty(ErrorInfo, EffectTable, RawEffectName, ConnectedEffect, ref IsEffectParsed, ref IsConnected, ConnectedRecipe);

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
