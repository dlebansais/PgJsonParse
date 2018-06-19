namespace PgJsonObjects
{
    interface IPgAbilitySource
    {
        IPgAbility ConnectedAbility { get; }
        IPgSkill SkillTypeId { get; }
        IPgItem ConnectedItem { get; }
        IPgGameNpc Npc { get; }
        IPgEffect ConnectedEffect { get; }
        IPgRecipe ConnectedRecipeEffect { get; }
        IPgQuest ConnectedQuest { get; }
        SourceTypes Type { get; }
    }
}
