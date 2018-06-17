namespace PgJsonObjects
{
    public class PgAbilityRequirementRecipeKnown: GenericPgObject, IPgAbilityRequirementRecipeKnown
    {
        public PgAbilityRequirementRecipeKnown(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementRecipeKnown(data, offset);
        }

        public Recipe RecipeKnown { get { return GetObject(4, ref _RecipeKnown); } } private Recipe _RecipeKnown;
    }
}
