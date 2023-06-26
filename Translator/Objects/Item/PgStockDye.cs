namespace PgObjects
{
    public class PgStockDye
    {
        public uint Color1 { get { return RawColor1.HasValue ? RawColor1.Value : 0; } }
        public uint? RawColor1 { get; set; }
        public uint Color2 { get { return RawColor2.HasValue ? RawColor2.Value : 0; } }
        public uint? RawColor2 { get; set; }
        public uint Color3 { get { return RawColor3.HasValue ? RawColor3.Value : 0; } }
        public uint? RawColor3 { get; set; }
        public bool IsGlowEnabled { get; set; }
    }
}
