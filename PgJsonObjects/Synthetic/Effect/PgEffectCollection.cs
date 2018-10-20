using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgEffectCollection : List<IPgEffect>, IPgEffectCollection
    {
        public static PgEffect CreateItem(byte[] data, ref int offset)
        {
            return new PgEffect(data, ref offset);
        }
    }
}
