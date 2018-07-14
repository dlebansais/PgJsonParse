namespace PgJsonObjects
{
    public interface IPgAbilityRequirementRecipeKnown : IPgAbilityRequirement
    {
        IPgRecipe Recipe { get; }
    }
}
