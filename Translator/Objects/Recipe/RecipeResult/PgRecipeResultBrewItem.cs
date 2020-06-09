namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgRecipeResultBrewItem : PgRecipeResultEffect
    {
        public int BrewPartCount { get { return RawBrewPartCount.HasValue ? RawBrewPartCount.Value : 0; } }
        public int? RawBrewPartCount { get; set; }
        public int BrewLevel { get { return RawBrewLevel.HasValue ? RawBrewLevel.Value : 0; } }
        public int? RawBrewLevel { get; set; }
        public List<RecipeItemKey> BrewPartList { get; } = new List<RecipeItemKey>();
        public List<RecipeResultKey> BrewResultList { get; } = new List<RecipeResultKey>();
    }
}
