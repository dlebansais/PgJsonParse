using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeResultEffect : IPgRecipeResultEffect
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
        public List<RecipeItemKey> BrewPartList { get; set; }
        public List<RecipeResultKey> BrewResultList { get; set; }
        public int AdjustedReuseTime { get { return RawAdjustedReuseTime.HasValue ? RawAdjustedReuseTime.Value : 0; } }
        public int? RawAdjustedReuseTime { get; set; }
        public bool IsCamouflaged { get { return RawIsCamouflaged.HasValue && RawIsCamouflaged.Value; } }
        public bool? RawIsCamouflaged { get; set; }

        public string CombinedEffect
        {
            get
            {
                string Result;

                switch (Effect)
                {
                    default:
                        return TextMaps.RecipeEffectTextMap[Effect];

                    //case RecipeEffect.DecomposeItemByTSysLevels:
                    //    return "Decompose to create " + TextMaps.DecomposeMaterialTextMap[Material] + " with " + TextMaps.DecomposeSkillTextMap[Skill];

                    case RecipeEffect.ExtractTSysPower:
                        return "Extract " + TextMaps.AugmentTextMap[ExtractedAugment] + " using material level " + MinLevel + "-" + MaxLevel + " with " + TextMaps.DecomposeSkillTextMap[Skill];

                    case RecipeEffect.RepairItemDurability:
                        return "Repair Between " + (RepairMinEfficiency * 100) + "% and " + (RepairMaxEfficiency * 100) + "% Of Item Durability, with a cooldown of " + TimeSpan.FromHours(RepairCooldown).ToString() + ", items in level range " + MinLevel + "-" + MaxLevel;

                    case RecipeEffect.TSysCraftedEquipment:
                        Result = "Craft " + TextMaps.CraftedBoostTextMap[Boost] + " Tier " + BoostLevel;

                        if (RawAdditionalEnchantments.HasValue)
                            Result += " with " + RawAdditionalEnchantments.Value + " additional enchantments";

                        if (BoostedAnimal != Appearance.Internal_None)
                            Result += " for " + TextMaps.AppearanceTextMap[BoostedAnimal] + " only";

                        return Result;

                    case RecipeEffect.CraftingEnhanceItem:
                        Result = "Add " + TextMaps.EnhancementEffectTextMap[Enhancement];

                        switch (Enhancement)
                        {
                            case EnhancementEffect.Pockets:
                            case EnhancementEffect.Armor:
                                Result += " (" + (int)AddedQuantity + ")";
                                break;

                            default:
                                Result += " (" + (int)(AddedQuantity * 100) + "%)";
                                break;
                        }

                        Result += " and consume " + ConsumedEnhancementPoints + " Craft Points";
                        return Result;

                    case RecipeEffect.AddItemTSysPower:
                        return "Infuse " + TextMaps.ShamanicSlotPowerTextMap[SlotPower] + " (Tier " + SlotPowerLevel + "), consuming 100 Craft Points";

                    case RecipeEffect.BrewItem:
                        return "Brewed drink";

                    case RecipeEffect.AdjustRecipeReuseTime:
                        return "Adjust Recipe Reuse Time, " + AdjustedReuseTime + "s during " + MoonPhase;
                }
            }
        }
    }
}
