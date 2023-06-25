namespace PgObjects
{
    public class PgDroppedAppearance
    {
        public ItemDroppedAppearance Appearance { get; set; }
        public AppearanceSkin Skin { get; set; }
        public AppearanceSkin Cork { get; set; }
        public AppearanceSkin Food { get; set; }
        public AppearanceSkin Plate { get; set; }
        public uint Color { get { return RawColor.HasValue ? RawColor.Value : 0; } }
        public uint? RawColor { get; set; }
        public uint SkinColor { get { return RawSkinColor.HasValue ? RawSkinColor.Value : 0; } }
        public uint? RawSkinColor { get; set; }
    }
}
