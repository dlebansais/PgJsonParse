namespace PgJsonObjects
{
    public interface IPgRecipeEffectSource : IPgGenericSource
    {
        IPgRecipe Recipe { get; }
    }
}
