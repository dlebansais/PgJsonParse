namespace PgObjects
{
    public class PgNpcCapIncrease
    {
        public Favor CapIncreaseFavor { get; set; }
        public int Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public int? RawValue { get; set; }
    }
}
