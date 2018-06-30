using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveUseRecipe : GenericPgObject<PgQuestObjectiveUseRecipe>, IPgQuestObjectiveUseRecipe
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

        public override string Key { get { return GetString(0); } }
        public IPgSkill Skill { get { return GetObject(4, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;
        public RecipeCollection RecipeTargetList { get { return GetObjectList(8, ref _RecipeTargetList, RecipeCollection.CreateItem, () => new RecipeCollection()); } } private RecipeCollection _RecipeTargetList;
        public ItemCollection ResultItemList { get { return GetObjectList(12, ref _ResultItemList, ItemCollection.CreateItem, () => new ItemCollection()); } } private ItemCollection _ResultItemList;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
