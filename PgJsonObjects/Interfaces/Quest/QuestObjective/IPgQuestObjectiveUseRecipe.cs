namespace PgJsonObjects
{
    public interface IPgQuestObjectiveUseRecipe
    {
        Skill ConnectedSkill { get; }
        RecipeCollection RecipeTargetList { get; }
        ItemCollection ResultItemList { get; }
    }
}
