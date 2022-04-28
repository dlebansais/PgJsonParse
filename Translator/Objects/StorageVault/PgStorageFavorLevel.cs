namespace PgObjects
{
    using System.Collections.Generic;

    public class PgStorageFavorLevel
    {
        public List<PgFavorSlotPair> LevelTable { get; set; } = new();
    }
}
