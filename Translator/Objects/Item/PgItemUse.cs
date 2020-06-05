namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgItemUse
    {
        public List<int> RecipesThatUseItem { get; } = new List<int>();
    }
}
