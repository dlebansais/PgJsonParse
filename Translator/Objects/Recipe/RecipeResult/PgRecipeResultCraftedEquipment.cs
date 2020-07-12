namespace PgObjects
{
    public class PgRecipeResultCraftedEquipment : PgRecipeResultEffect
    {
        public CraftedBoost Boost { get; set; }
        public bool IsCamouflaged { get { return RawIsCamouflaged.HasValue && RawIsCamouflaged.Value; } }
        public bool? RawIsCamouflaged { get; set; }
        public int BoostLevel { get { return RawBoostLevel.HasValue ? RawBoostLevel.Value : 0; } }
        public int? RawBoostLevel { get; set; }
        public int AdditionalEnchantments { get { return RawAdditionalEnchantments.HasValue ? RawAdditionalEnchantments.Value : 0; } }
        public int? RawAdditionalEnchantments { get; set; }
        public Appearance BoostedAnimal { get; set; }
    }
}
