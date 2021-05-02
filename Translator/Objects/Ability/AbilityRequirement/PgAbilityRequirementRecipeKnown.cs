namespace PgObjects
{
    public class PgAbilityRequirementRecipeKnown : PgAbilityRequirement
    {
        public PgRecipe Recipe { get; set; } = null!;
    }
}
