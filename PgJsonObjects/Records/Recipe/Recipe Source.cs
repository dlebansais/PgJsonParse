using PgJsonReader;
using System;
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
        private string RawNpcId;
        private string RawNpcName;
        private bool IsNpcParsed;
        public GameNpc Npc { get; private set; }
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

        public override void SetIndirectProperties(Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables, ParseErrorInfo ErrorInfo)
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
                        return new TrainingSource(RawNpcName, Npc);
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
                        return new GiftSource(RawNpcName, Npc);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Recipe Source (Gift)");
                        return null;
                    }

                case SourceTypes.HangOut:
                    if (RawNpcName != null)
                        return new HangOutSource(RawNpcName, Npc);
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
        protected override void InitializeKey(KeyValuePair<string, IJsonValue> EntryRaw, ParseErrorInfo ErrorInfo)
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

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Type", new FieldParser() { Type = FieldType.String, ParserString = ParseType } },
            { "SkillTypeId", new FieldParser() { Type = FieldType.String, ParserString = ParseSkillTypeId } },
            { "ItemTypeId", new FieldParser() { Type = FieldType.Integer, ParserInteger = ParseItemTypeId } },
            { "Npc", new FieldParser() { Type = FieldType.String, ParserString = ParseNpc } },
            { "EffectName", new FieldParser() { Type = FieldType.String, ParserString = ParseEffectName } },
            { "EffectTypeId", new FieldParser() { Type = FieldType.String, ParserString = ParseEffectTypeId } },
            { "QuestId", new FieldParser() { Type = FieldType.Integer, ParserInteger = ParseQuestId } },
        }; } }

        private void ParseType(string RawType, ParseErrorInfo ErrorInfo)
        {
            SourceTypes ParsedType;
            StringToEnumConversion<SourceTypes>.TryParse(RawType, out ParsedType, ErrorInfo);
            Type = ParsedType;
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

        private void ParseItemTypeId(int RawItemTypeId, ParseErrorInfo ErrorInfo)
        {
            if (Type == SourceTypes.Item)
                this.RawItemTypeId = (int)RawItemTypeId;
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource ItemTypeId (type)");
        }

        private void ParseNpc(string RawNpc, ParseErrorInfo ErrorInfo)
        {
            string ParsedNpcId;
            string ParsedNpcName;
            if (Quest.TryParseNPC(RawNpc, out ParsedNpcId, out ParsedNpcName, ErrorInfo))
            {
                RawNpcId = ParsedNpcId;
                RawNpcName = ParsedNpcName;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource Npc");
        }

        private void ParseEffectName(string RawEffectName, ParseErrorInfo ErrorInfo)
        {
            if (Type == SourceTypes.Effect)
                this.RawEffectName = RawEffectName;
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource EffectName (type)");
        }

        private void ParseEffectTypeId(string RawEffectTypeId, ParseErrorInfo ErrorInfo)
        {
        }

        private void ParseQuestId(int RawQuestId, ParseErrorInfo ErrorInfo)
        {
            if (Type == SourceTypes.Quest)
                this.RawQuestId = (int)RawQuestId;
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
                /*
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
                */

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IGenericJsonObject> RecipeTable = AllTables[typeof(Recipe)];
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];
            Dictionary<string, IGenericJsonObject> EffectTable = AllTables[typeof(Effect)];
            Dictionary<string, IGenericJsonObject> QuestTable = AllTables[typeof(Quest)];
            Dictionary<string, IGenericJsonObject> GameNpcTable = AllTables[typeof(GameNpc)];

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

            Npc = GameNpc.ConnectByKey(ErrorInfo, GameNpcTable, RawNpcId, Npc, ref IsNpcParsed, ref IsConnected, ConnectedRecipe);
            if (RawNpcId != null && Npc == null)
            {
                SpecialNpc ParsedSpecialNpc;
                if (StringToEnumConversion<SpecialNpc>.TryParse(RawNpcId, out ParsedSpecialNpc, ErrorInfo))
                    RawNpcName = TextMaps.SpecialNpcTextMap[ParsedSpecialNpc];
            }

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "RecipeSource"; } }
        #endregion
    }
}
