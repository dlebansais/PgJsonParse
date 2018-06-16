namespace PgJsonObjects
{
    public interface IPgRecipeSource
    {
        Recipe ConnectedRecipe { get; }
        Skill SkillTypeId { get; }
        Item ConnectedItem { get; }
        GameNpc Npc { get; }
        Effect ConnectedEffect { get; }
        Quest ConnectedQuest { get; }
        SourceTypes Type { get; }
    }
}
