namespace PgObjects
{
    public abstract class PgAbilityParticle
    {
        public uint AoEColor { get { return RawAoEColor.HasValue ? RawAoEColor.Value : 0; } }
        public uint? RawAoEColor { get; set; }
        public int AoERange { get { return RawAoERange.HasValue ? RawAoERange.Value : 0; } }
        public int? RawAoERange { get; set; }
        public uint PrimaryColor { get { return RawPrimaryColor.HasValue ? RawPrimaryColor.Value : 0; } }
        public uint? RawPrimaryColor { get; set; }
        public uint SecondaryColor { get { return RawSecondaryColor.HasValue ? RawSecondaryColor.Value : 0; } }
        public uint? RawSecondaryColor { get; set; }
    }
}
