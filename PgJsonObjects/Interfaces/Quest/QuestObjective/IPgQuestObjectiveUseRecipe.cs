namespace PgJsonObjects
{
    public interface IPgQuestObjectiveUseRecipe
    {
        IPgSkill Skill { get; }
        IPgRecipeCollection RecipeTargetList { get; }
        IPgItemCollection ResultItemList { get; }
        RecipeKeyword RecipeTarget { get; }
        ItemKeyword ResultItemKeyword { get; }
    }
}
