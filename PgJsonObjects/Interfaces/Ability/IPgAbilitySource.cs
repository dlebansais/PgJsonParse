namespace PgJsonObjects
{
    interface IPgAbilitySource : IJsonKey, IObjectContentGenerator
    {
        IPgAbility ConnectedAbility { get; }
        IPgSkill SkillTypeId { get; }
        IPgItem ConnectedItem { get; }
        IPgGameNpc Npc { get; }
        IPgEffect ConnectedEffect { get; }
        IPgRecipe ConnectedRecipeEffect { get; }
        IPgQuest ConnectedQuest { get; }
        string EffectTypeId { get; }
        SourceTypes Type { get; }
    }
}
