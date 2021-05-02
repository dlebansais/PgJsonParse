namespace PgObjects
{
    public class PgNpcLocation
    {
        public MapAreaName NpcArea { get; set; }
        public string NpcId { get; set; } = string.Empty;
        public PgNpc Npc { get; set; } = null!;
        public SpecialNpc NpcEnum { get; set; }
        public string NpcName { get; set; } = string.Empty;
    }
}
