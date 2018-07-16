using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRecipeResultEffectCollection : List<IPgRecipeResultEffect>, IPgRecipeResultEffectCollection
    {
        public static PgRecipeResultEffect CreateItem(byte[] data, ref int offset)
        {
            return new PgRecipeResultEffect(data, ref offset);
        }
    }
}
