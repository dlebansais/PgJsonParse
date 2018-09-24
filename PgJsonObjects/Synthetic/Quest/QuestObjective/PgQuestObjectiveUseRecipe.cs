using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveUseRecipe : PgQuestObjective<PgQuestObjectiveUseRecipe>, IPgQuestObjectiveUseRecipe
    {
        public PgQuestObjectiveUseRecipe(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveUseRecipe CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveUseRecipe CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveUseRecipe(data, ref offset);
        }

        public IPgSkill Skill { get { return GetObject(PropertiesOffset + 0, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;
        public IPgRecipeCollection RecipeTargetList { get { return GetObjectList(PropertiesOffset + 4, ref _RecipeTargetList, PgRecipeCollection.CreateItem, () => new PgRecipeCollection()); } } private IPgRecipeCollection _RecipeTargetList;
        public IPgItemCollection ResultItemList { get { return GetObjectList(PropertiesOffset + 8, ref _ResultItemList, PgItemCollection.CreateItem, () => new PgItemCollection()); } } private IPgItemCollection _ResultItemList;
        public RecipeKeyword RecipeTarget { get { return GetEnum<RecipeKeyword>(PropertiesOffset + 12); } }
        public ItemKeyword ResultItemKeyword { get { return GetEnum<ItemKeyword>(PropertiesOffset + 14); } }

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
                GetString = () => Skill != null ? StringToEnumConversion<PowerSkill>.ToString(Skill.CombatSkill, null, PowerSkill.Internal_None) : null } },
            { "ResultItemKeyword", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(ResultItemKeyword, TextMaps.ItemKeywordStringMap, ItemKeyword.Internal_None) } },
        }; } }

        public override IList<IBackLinkable> GetLinkBack()
        {
            List<IBackLinkable> Result = new List<IBackLinkable>();
            Result.Add(Skill);
            if (RecipeTargetList != null)
                Result.AddRange(RecipeTargetList);
            if (ResultItemList != null)
                Result.AddRange(ResultItemList);

            return Result;
        }

        public override IPgQuestObjectiveRequirement QuestObjectiveRequirement { get { return null; } }
    }
}
