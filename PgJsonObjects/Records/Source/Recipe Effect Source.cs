namespace PgJsonObjects
{
    public class RecipeEffectSource : GenericSource
    {
        #region Init
        public RecipeEffectSource(Recipe Recipe)
        {
            this.Recipe = Recipe;
        }
        #endregion

        #region Properties
        public Recipe Recipe { get; private set; }
        #endregion
    }
}
