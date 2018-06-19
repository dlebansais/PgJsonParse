namespace PgJsonObjects
{
    public class RecipeEffectSource : GenericSource
    {
        #region Init
        public RecipeEffectSource(IPgRecipe Recipe)
        {
            this.Recipe = Recipe;
        }
        #endregion

        #region Properties
        public IPgRecipe Recipe { get; private set; }
        #endregion
    }
}
