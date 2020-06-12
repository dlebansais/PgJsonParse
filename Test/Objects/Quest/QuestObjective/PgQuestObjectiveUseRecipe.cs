namespace PgObjects
{
    public class PgQuestObjectiveUseRecipe : PgQuestObjective
    {
        public RecipeKeyword RecipeTarget { get; set; }
        public PgSkill Skill { get; set; }
        public ItemKeyword ResultItemKeyword { get; set; }
    }
}
