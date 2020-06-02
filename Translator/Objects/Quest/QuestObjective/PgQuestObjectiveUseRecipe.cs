namespace PgJsonObjects
{
    public class PgQuestObjectiveUseRecipe : PgQuestObjective
    {
        public PgSkill Skill { get; set; }
        public PgRecipeCollection RecipeTargetList { get; set; } = new PgRecipeCollection();
        public PgItemCollection ResultItemList { get; set; } = new PgItemCollection();
        public RecipeKeyword RecipeTarget { get; set; }
        public ItemKeyword ResultItemKeyword { get; set; }
    }
}
