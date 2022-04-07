namespace PgObjects
{
    public class PgQuestObjectiveUseRecipe : PgQuestObjective
    {
        public RecipeKeyword Target { get; set; }
        public string? Skill_Key { get; set; }
        public ItemKeyword ResultItemKeyword { get; set; }
    }
}
