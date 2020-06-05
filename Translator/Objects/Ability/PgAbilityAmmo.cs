namespace PgJsonObjects
{
    public class PgAbilityAmmo
    {
        public ItemKeyword ItemKeyword { get; set; }
        public int Count { get { return RawCount.HasValue ? RawCount.Value : 0; } }
        public int? RawCount { get; set; }
    }
}
