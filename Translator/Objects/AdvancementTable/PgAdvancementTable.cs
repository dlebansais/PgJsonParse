namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgAdvancementTable
    {
        public string Key { get; set; }
        public Dictionary<int, PgAdvancement> LevelTable { get; set; } = new Dictionary<int, PgAdvancement>();
    }
}
