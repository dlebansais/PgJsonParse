using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgLevelCapInteractionCollection : List<IPgLevelCapInteraction>, IPgLevelCapInteractionCollection
    {
        public static PgLevelCapInteraction CreateItem(byte[] data, ref int offset)
        {
            return new PgLevelCapInteraction(data, ref offset);
        }
    }
}
