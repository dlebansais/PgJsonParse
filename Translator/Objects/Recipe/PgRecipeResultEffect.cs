namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgRecipeResultEffect
    {
        public RecipeEffect Effect { get; set; }
        public CraftedBoost Boost { get; set; }
        public int MinLevel { get { return RawMinLevel.HasValue ? RawMinLevel.Value : 0; } }
        public int? RawMinLevel { get; set; }
        public int MaxLevel { get { return RawMaxLevel.HasValue ? RawMaxLevel.Value : 0; } }
        public int? RawMaxLevel { get; set; }
        public DecomposeSkill Skill { get; set; }
        public Augment ExtractedAugment { get; set; }
        public float RepairMinEfficiency { get { return RawRepairMinEfficiency.HasValue ? RawRepairMinEfficiency.Value : 0; } }
        public float? RawRepairMinEfficiency { get; set; }
        public float RepairMaxEfficiency { get { return RawRepairMaxEfficiency.HasValue ? RawRepairMaxEfficiency.Value : 0; } }
        public float? RawRepairMaxEfficiency { get; set; }
        public FloatFormat RepairMinEfficiencyFormat { get; set; }
        public FloatFormat RepairMaxEfficiencyFormat { get; set; }
        public int RepairCooldown { get { return RawRepairCooldown.HasValue ? RawRepairCooldown.Value : 0; } }
        public int? RawRepairCooldown { get; set; }
        public int BoostLevel { get { return RawBoostLevel.HasValue ? RawBoostLevel.Value : 0; } }
        public int? RawBoostLevel { get; set; }
        public int AdditionalEnchantments { get { return RawAdditionalEnchantments.HasValue ? RawAdditionalEnchantments.Value : 0; } }
        public int? RawAdditionalEnchantments { get; set; }
        public Appearance BoostedAnimal { get; set; }
        public EnhancementEffect Enhancement { get; set; }
        public float AddedQuantity { get { return RawAddedQuantity.HasValue ? RawAddedQuantity.Value : 0; } }
        public float? RawAddedQuantity { get; set; }
        public int ConsumedEnhancementPoints { get { return RawConsumedEnhancementPoints.HasValue ? RawConsumedEnhancementPoints.Value : 0; } }
        public int? RawConsumedEnhancementPoints { get; set; }
        public ShamanicSlotPower SlotPower { get; set; }
        public MoonPhases MoonPhase { get; set; }
        public int SlotPowerLevel { get { return RawSlotPowerLevel.HasValue ? RawSlotPowerLevel.Value : 0; } }
        public int? RawSlotPowerLevel { get; set; }
        public int BrewPartCount { get { return RawBrewPartCount.HasValue ? RawBrewPartCount.Value : 0; } }
        public int? RawBrewPartCount { get; set; }
        public int BrewLevel { get { return RawBrewLevel.HasValue ? RawBrewLevel.Value : 0; } }
        public int? RawBrewLevel { get; set; }
        public List<RecipeItemKey> BrewPartList { get; private set; } = new List<RecipeItemKey>();
        public void SetBrewPartList(List<RecipeItemKey> newBrewPartList) { BrewPartList = newBrewPartList; }
        public List<RecipeResultKey> BrewResultList { get; private set; } = new List<RecipeResultKey>();
        public void SetResultList(List<RecipeResultKey> newResultList) { BrewResultList = newResultList; }
        public int AdjustedReuseTime { get { return RawAdjustedReuseTime.HasValue ? RawAdjustedReuseTime.Value : 0; } }
        public int? RawAdjustedReuseTime { get; set; }
        public string RawItemName { get; set; }
        public PgItem Item { get; set; }
        public bool IsCamouflaged { get { return RawIsCamouflaged.HasValue && RawIsCamouflaged.Value; } }
        public bool? RawIsCamouflaged { get; set; }
        public PowerWaxType PowerWaxType { get; set; }
        public RecipeItemKey RecipeItemKey { get; set; }
    }
}
