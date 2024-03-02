namespace PgObjects
{
    public class PgNpcLevelRange
    {
        public int Min { get { return RawMin.HasValue ? RawMin.Value : 0; } }
        public int? RawMin { get; set; }
        public int Max { get { return RawMax.HasValue ? RawMax.Value : 0; } }
        public int? RawMax { get; set; }
    }
}
