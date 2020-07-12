namespace PgObjects
{
    using System.Collections.Generic;

    public class PgRecipeResultBrewItem : PgRecipeResultEffect
    {
        public int BrewLine { get { return RawBrewLine.HasValue ? RawBrewLine.Value : 0; } }
        public int? RawBrewLine { get; set; }
        public int BrewStrength { get { return RawBrewStrength.HasValue ? RawBrewStrength.Value : 0; } }
        public int? RawBrewStrength { get; set; }
        public List<RecipeItemKey> BrewPartList { get; set; } = new List<RecipeItemKey>();
        public List<RecipeResultKey> BrewResultList { get; set; } = new List<RecipeResultKey>();
    }
}
