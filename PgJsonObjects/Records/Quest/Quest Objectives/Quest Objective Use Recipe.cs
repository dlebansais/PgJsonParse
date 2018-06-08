using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveUseRecipe : QuestObjective
    {
        #region Init
        public QuestObjectiveUseRecipe(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, PowerSkill Skill, RecipeKeyword RecipeTarget, ItemKeyword ResultItemKeyword)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.Skill = Skill;
            this.RecipeTarget = RecipeTarget;
            this.ResultItemKeyword = ResultItemKeyword;
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Type", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<QuestObjectiveType>.ToString(Type, null, QuestObjectiveType.Internal_None) } },
            { "MustCompleteEarlierObjectivesFirst", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawMustCompleteEarlierObjectivesFirst } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Description } },
            { "Number", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumber } },
            { "Target", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<RecipeKeyword>.ToString(RecipeTarget) } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(Skill, null, PowerSkill.Internal_None) } },
            { "ResultItemKeyword", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(ResultItemKeyword, null, ItemKeyword.Internal_None) } },
        }; } }
        #endregion

        #region Properties
        public PowerSkill Skill { get; private set; }
        public Skill ConnectedSkill { get; private set; }
        private bool IsSkillParsed;
        public RecipeKeyword RecipeTarget { get; private set; }
        public List<Recipe> RecipeTargetList { get; private set; } = new List<Recipe>();
        private bool IsRecipeTargetParsed;
        public ItemKeyword ResultItemKeyword { get; private set; }
        public List<Item> ResultItemList { get; private set; } = new List<Item>();
        private bool IsResultItemParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (ConnectedSkill != null)
                    AddWithFieldSeparator(ref Result, ConnectedSkill.Name);
                if (RecipeTarget != RecipeKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.RecipeKeywordTextMap[RecipeTarget]);
                foreach (Recipe Recipe in RecipeTargetList)
                    AddWithFieldSeparator(ref Result, Recipe.Name);
                if (ResultItemKeyword != ItemKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemKeywordTextMap[ResultItemKeyword]);
                foreach (Item Item in ResultItemList)
                    AddWithFieldSeparator(ref Result, Item.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IGenericJsonObject> RecipeTable = AllTables[typeof(Recipe)];
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            ConnectedSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, Skill, ConnectedSkill, ref IsSkillParsed, ref IsConnected, this);
            RecipeTargetList = PgJsonObjects.Recipe.ConnectByKeyword(ErrorInfo, RecipeTable, RecipeTarget, RecipeTargetList, ref IsRecipeTargetParsed, ref IsConnected, ParentQuest);
            ResultItemList = PgJsonObjects.Item.ConnectByKeyword(ErrorInfo, ItemTable, ResultItemKeyword, ResultItemList, ref IsResultItemParsed, ref IsConnected, ParentQuest);

            return IsConnected;
        }
        #endregion
    }
}
