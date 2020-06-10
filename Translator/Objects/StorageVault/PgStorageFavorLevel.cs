namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgStorageFavorLevel
    {
        public Dictionary<Favor, int> LevelTable { get; } = new Dictionary<Favor, int>();
    }
}
