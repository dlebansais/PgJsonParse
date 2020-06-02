namespace PgJsonObjects
{
    public class PgAbilityRequirementRecipeKnown : PgAbilityRequirement
    {
        public override OtherRequirementType Type { get { return OtherRequirementType.RecipeKnown; } }
        public PgRecipe Recipe { get; set; }
    }
}
