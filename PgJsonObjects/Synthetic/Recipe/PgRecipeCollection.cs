using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRecipeCollection : List<IPgRecipe>, IPgRecipeCollection
    {
        public static PgRecipe CreateItem(byte[] data, ref int offset)
        {
            return new PgRecipe(data, ref offset);
        }
    }
}
