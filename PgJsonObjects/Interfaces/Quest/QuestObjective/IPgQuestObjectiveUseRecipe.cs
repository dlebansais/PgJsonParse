namespace PgJsonObjects
{
    public interface IPgQuestObjectiveUseRecipe
    {
        IPgSkill Skill { get; }
        RecipeCollection RecipeTargetList { get; }
        ItemCollection ResultItemList { get; }
    }
}
