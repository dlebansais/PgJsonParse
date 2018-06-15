namespace PgJsonObjects
{
    interface IPgAbilitySource
    {
        Ability ConnectedAbility { get; }
        Skill SkillTypeId { get; }
        Item ConnectedItem { get; }
        GameNpc Npc { get; }
        Effect ConnectedEffect { get; }
        Recipe ConnectedRecipeEffect { get; }
        Quest ConnectedQuest { get; }
        SourceTypes Type { get; }
    }
}
