namespace PgJsonObjects
{
    public class PgAbilityRequirementRecipeKnown: GenericPgObject, IPgAbilityRequirementRecipeKnown
    {
        public PgAbilityRequirementRecipeKnown(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Recipe RecipeKnown { get { return GetObject(4, ref _RecipeKnown); } } private Recipe _RecipeKnown;
    }
}
