namespace PgJsonObjects
{
    public class PgQuestObjectiveUseRecipe : GenericPgObject<PgQuestObjectiveUseRecipe>, IPgQuestObjectiveUseRecipe
    {
        public PgQuestObjectiveUseRecipe(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveUseRecipe CreateItem(byte[] data, int offset)
        {
            return new PgQuestObjectiveUseRecipe(data, offset);
        }

        public Skill ConnectedSkill { get { return GetObject(0, ref _ConnectedSkill); } } private Skill _ConnectedSkill;
        public RecipeCollection RecipeTargetList { get { return GetObjectList(4, ref _RecipeTargetList, RecipeCollection.CreateItem, () => new RecipeCollection()); } } private RecipeCollection _RecipeTargetList;
        public ItemCollection ResultItemList { get { return GetObjectList(8, ref _ResultItemList, ItemCollection.CreateItem, () => new ItemCollection()); } } private ItemCollection _ResultItemList;
    }
}
