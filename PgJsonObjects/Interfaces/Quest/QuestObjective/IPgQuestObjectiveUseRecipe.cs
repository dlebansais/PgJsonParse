namespace PgJsonObjects
{
    public interface IPgQuestObjectiveUseRecipe
    {
        IPgSkill ConnectedSkill { get; }
        RecipeCollection RecipeTargetList { get; }
        ItemCollection ResultItemList { get; }
    }
}
