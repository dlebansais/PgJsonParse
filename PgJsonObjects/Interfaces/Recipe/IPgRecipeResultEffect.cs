using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgRecipeResultEffect
    {
        RecipeEffect Effect { get; }
        CraftedBoost Boost { get; }
        int MinLevel { get; }
        int? RawMinLevel { get; }
        int MaxLevel { get; }
        int? RawMaxLevel { get; }
        DecomposeSkill Skill { get; }
        Augment ExtractedAugment { get; }
        float RepairMinEfficiency { get; }
        float? RawRepairMinEfficiency { get; }
        float RepairMaxEfficiency { get; }
        float? RawRepairMaxEfficiency { get; }
        FloatFormat RepairMinEfficiencyFormat { get; }
        FloatFormat RepairMaxEfficiencyFormat { get; }
        int RepairCooldown { get; }
        int? RawRepairCooldown { get; }
        int BoostLevel { get; }
        int? RawBoostLevel { get; }
        int AdditionalEnchantments { get; }
        int? RawAdditionalEnchantments { get; }
        Appearance BoostedAnimal { get; }
        EnhancementEffect Enhancement { get; }
        float AddedQuantity { get; }
        float? RawAddedQuantity { get; }
        int ConsumedEnhancementPoints { get; }
        int? RawConsumedEnhancementPoints { get; }
        ShamanicSlotPower SlotPower { get; }
        MoonPhases MoonPhase { get; }
        int SlotPowerLevel { get; }
        int? RawSlotPowerLevel { get; }
        int BrewPartCount { get; }
        int? RawBrewPartCount { get; }
        int BrewLevel { get; }
        int? RawBrewLevel { get; }
        List<RecipeItemKey> BrewPartList { get; }
        List<RecipeResultKey> BrewResultList { get; }
        int AdjustedReuseTime { get; }
        int? RawAdjustedReuseTime { get; }
        IPgItem Item { get; }
        bool IsCamouflaged { get; }
        bool? RawIsCamouflaged { get; }
        PowerWaxType PowerWaxType { get; }
        RecipeItemKey RecipeItemKey { get; }

        string CombinedEffect { get; }
    }
}
