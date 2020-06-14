namespace PgObjects
{
    using System.Collections.Generic;

    public class PgStorageFavorLevel
    {
        public Dictionary<Favor, int> LevelTable { get; set; } = new Dictionary<Favor, int>();
    }
}
