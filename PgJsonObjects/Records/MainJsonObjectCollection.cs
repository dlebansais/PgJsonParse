using System.Collections.Generic;

namespace PgJsonObjects
{
    public class MainJsonObjectCollection<T> : List<T>, IMainJsonObjectCollection
         where T : MainJsonObject<T>
    {
        public IMainJsonObject GetAt(int index)
        {
            return this[index];
        }
    }
}
