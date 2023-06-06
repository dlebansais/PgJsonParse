namespace PgObjects
{
    public class PgEffectParticle
    {
        public EffectParticle Particle { get; set; }
        public uint AoEColor { get { return RawAoEColor.HasValue ? RawAoEColor.Value : 0; } }
        public uint? RawAoEColor { get; set; }
        public int AoERange { get { return RawAoERange.HasValue ? RawAoERange.Value : 0; } }
        public int? RawAoERange { get; set; }
    }
}
