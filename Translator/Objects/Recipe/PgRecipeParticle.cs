namespace PgObjects
{
    public class PgRecipeParticle
    {
        public RecipeParticle Particle { get; set; }
        public uint Color0 { get { return RawColor0.HasValue ? RawColor0.Value : 0; } }
        public uint? RawColor0 { get; set; }
        public uint Color1 { get { return RawColor1.HasValue ? RawColor1.Value : 0; } }
        public uint? RawColor1 { get; set; }
        public uint LightColor { get { return RawLightColor.HasValue ? RawLightColor.Value : 0; } }
        public uint? RawLightColor { get; set; }
    }
}
