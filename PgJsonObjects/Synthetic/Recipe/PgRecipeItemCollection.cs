using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRecipeItemCollection : List<IPgRecipeItem>, IPgRecipeItemCollection
    {
        public static PgRecipeItem CreateItem(byte[] data, ref int offset)
        {
            return new PgRecipeItem(data, ref offset);
        }
    }
}
