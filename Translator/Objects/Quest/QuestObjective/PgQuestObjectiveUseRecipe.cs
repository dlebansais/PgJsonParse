namespace PgObjects
{
    public class PgQuestObjectiveUseRecipe : PgQuestObjective
    {
        public string? Target_Key { get; set; }
        public RecipeKeyword TargetKeyword { get; set; }
        public string? Skill_Key { get; set; }
        public ItemKeyword ResultItemKeyword { get; set; }
    }
}
