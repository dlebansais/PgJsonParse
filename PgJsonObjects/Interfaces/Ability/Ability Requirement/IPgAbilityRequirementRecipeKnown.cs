namespace PgJsonObjects
{
    public interface IPgAbilityRequirementRecipeKnown : IPgAbilityRequirement
    {
        IPgRecipe RecipeKnown { get; }
    }
}
