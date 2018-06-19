namespace PgJsonObjects
{
    public class PgAbilityRequirementRecipeKnown: GenericPgObject<PgAbilityRequirementRecipeKnown>, IPgAbilityRequirementRecipeKnown
    {
        public PgAbilityRequirementRecipeKnown(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementRecipeKnown CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementRecipeKnown CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementRecipeKnown(data, ref offset);
        }

        public IPgRecipe RecipeKnown { get { return GetObject(4, ref _RecipeKnown, PgRecipe.CreateNew); } } private IPgRecipe _RecipeKnown;
    }
}
