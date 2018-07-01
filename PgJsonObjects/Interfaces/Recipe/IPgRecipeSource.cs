namespace PgJsonObjects
{
    public interface IPgRecipeSource : IJsonKey, IObjectContentGenerator
    {
        IPgRecipe ConnectedRecipe { get; }
        IPgSkill SkillTypeId { get; }
        IPgItem ConnectedItem { get; }
        IPgGameNpc Npc { get; }
        IPgEffect ConnectedEffect { get; }
        IPgQuest ConnectedQuest { get; }
        string RawNpcId { get; }
        string RawNpcName { get; }
        string RawEffectTypeId { get; }
        SourceTypes Type { get; }
    }
}
