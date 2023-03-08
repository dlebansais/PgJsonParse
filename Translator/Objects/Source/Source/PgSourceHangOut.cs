namespace PgObjects
{
    public class PgSourceHangOut : PgSource
    {
        public PgNpcLocation Npc { get; set; } = null!;
        public int HangOut { get { return RawHangOut.HasValue ? RawHangOut.Value : 0; } }
        public int? RawHangOut { get; set; }
    }
}
