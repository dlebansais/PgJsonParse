namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgAdvancementTable
    {
        public Dictionary<int, PgAdvancement> LevelTable { get; set; } = new Dictionary<int, PgAdvancement>();
    }
}
