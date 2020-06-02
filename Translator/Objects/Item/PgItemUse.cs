namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgItemUse
    {
        public List<int> RecipesThatUseItem { get; set; } = new List<int>();
    }
}
