namespace PgObjects
{
    public class PgNpcLocation
    {
        public MapAreaName NpcArea { get; set; }
        public string NpcId { get; set; } = string.Empty;
        public string? Npc_Key { get; set; }
        public SpecialNpc NpcEnum { get; set; }
        public string NpcName { get; set; } = string.Empty;
    }
}
