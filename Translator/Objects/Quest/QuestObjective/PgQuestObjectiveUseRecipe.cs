namespace PgJsonObjects
{
    public class PgQuestObjectiveUseRecipe : PgQuestObjective
    {
        public PgSkill Skill { get; set; }
        public PgRecipeCollection RecipeTargetList { get; } = new PgRecipeCollection();
        public PgItemCollection ResultItemList { get; } = new PgItemCollection();
        public RecipeKeyword RecipeTarget { get; set; }
        public ItemKeyword ResultItemKeyword { get; set; }
    }
}
