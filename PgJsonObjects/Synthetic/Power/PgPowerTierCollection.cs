using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPowerTierCollection : List<IPgPowerTier>, IPgPowerTierCollection
    {
        public static IPgPowerTier CreateItem(byte[] data, ref int offset)
        {
            return new PgPowerTier(data, ref offset);
        }
    }
}
