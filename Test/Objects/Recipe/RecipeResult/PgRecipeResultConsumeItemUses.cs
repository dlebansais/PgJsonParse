namespace PgObjects
{
    public class PgRecipeResultConsumeItemUses : PgRecipeResultEffect
    {
        public RecipeItemKey RecipeItemKey { get; set; }
        public int AdjustedReuseTime { get { return RawAdjustedReuseTime.HasValue ? RawAdjustedReuseTime.Value : 0; } }
        public int? RawAdjustedReuseTime { get; set; }
    }
}
