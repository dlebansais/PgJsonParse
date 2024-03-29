﻿namespace PgObjects
{
    public class PgRecipeParticle
    {
        public RecipeParticle Particle { get; set; }
        public uint PrimaryColor { get { return RawPrimaryColor.HasValue ? RawPrimaryColor.Value : 0; } }
        public uint? RawPrimaryColor { get; set; }
        public uint SecondaryColor { get { return RawSecondaryColor.HasValue ? RawSecondaryColor.Value : 0; } }
        public uint? RawSecondaryColor { get; set; }
        public uint LightColor { get { return RawLightColor.HasValue ? RawLightColor.Value : 0; } }
        public uint? RawLightColor { get; set; }
    }
}
