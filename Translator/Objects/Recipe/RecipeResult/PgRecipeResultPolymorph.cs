namespace PgObjects
{
    public class PgRecipeResultPolymorph : PgRecipeResultEffect
    {
        public uint Color { get { return RawColor.HasValue ? RawColor.Value : 0; } }
        public uint? RawColor { get; set; }
    }
}
