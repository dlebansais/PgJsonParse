using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgNpcPreferenceCollection : List<IPgNpcPreference>, IPgNpcPreferenceCollection
    {
        public static PgNpcPreference CreateItem(byte[] data, ref int offset)
        {
            return new PgNpcPreference(data, ref offset);
        }
    }
}
