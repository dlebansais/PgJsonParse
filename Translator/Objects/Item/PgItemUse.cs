namespace PgObjects
{
    public class PgItemUse
    {
        public string Key { get; set; } = string.Empty;
        public PgRecipeCollection RecipeList { get; set; } = new PgRecipeCollection();
    }
}
