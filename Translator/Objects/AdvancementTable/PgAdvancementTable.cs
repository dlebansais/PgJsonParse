namespace PgObjects
{
    using System.Collections.Generic;

    public class PgAdvancementTable
    {
        public string Key { get; set; } = string.Empty;
        public string InternalName { get; set; } = string.Empty;
        public List<PgAdvancement> AdvancementList { get; set; } = new();
    }
}
