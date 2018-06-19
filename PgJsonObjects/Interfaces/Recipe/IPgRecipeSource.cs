namespace PgJsonObjects
{
    public interface IPgRecipeSource
    {
        IPgRecipe ConnectedRecipe { get; }
        IPgSkill SkillTypeId { get; }
        IPgItem ConnectedItem { get; }
        IPgGameNpc Npc { get; }
        IPgEffect ConnectedEffect { get; }
        IPgQuest ConnectedQuest { get; }
        SourceTypes Type { get; }
    }
}
