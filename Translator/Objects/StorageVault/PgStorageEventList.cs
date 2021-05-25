namespace PgObjects
{
    using System.Collections.Generic;

    public class PgStorageEventList
    {
        public Dictionary<EventLevel, int> EventTable { get; set; } = new Dictionary<EventLevel, int>();
    }
}
