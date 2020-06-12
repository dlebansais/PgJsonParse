namespace PgObjects
{
    public class PgSourceEffect : PgSource
    {
        public bool IsMiscEffect { get { return RawIsMiscEffect.HasValue && RawIsMiscEffect.Value; } }
        public bool? RawIsMiscEffect { get; set; }
        public PgEffect Effect { get; set; }
    }
}
