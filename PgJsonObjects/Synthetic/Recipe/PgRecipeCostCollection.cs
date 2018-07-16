using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRecipeCostCollection : List<IPgRecipeCost>, IPgRecipeCostCollection
    {
        public static PgRecipeCost CreateItem(byte[] data, ref int offset)
        {
            return PgRecipeCost.CreateNew(data, ref offset);
        }
    }
}
