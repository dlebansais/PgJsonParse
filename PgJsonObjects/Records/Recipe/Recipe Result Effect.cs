using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeResultEffect
    {
        public RecipeEffect Effect { get; set; }
        public DecomposeMaterial Material { get; set; }
        public DecomposeSkill Skill { get; set; }
        public Augment ExtractedAugment { get; set; }
        public float RepairMinEfficiency { get; set; }
        public FloatFormat RepairMinEfficiencyFormat { get; set; }
        public float RepairMaxEfficiency { get; set; }
        public FloatFormat RepairMaxEfficiencyFormat { get; set; }
        public int RepairCooldown { get; set; }
        public CraftedBoost Boost { get; set; }
        public int BoostLevel { get; set; }
        public bool IsCamouflaged { get; set; }
        public int? AdditionalEnchantments { get; set; }
        public Appearance BoostedAnimal { get; set; }
        public EnhancementEffect Enhancement { get; set; }
        public float AddedQuantity { get; set; }
        public int ConsumedEnhancementPoints { get; set; }
        public ShamanicSlotPower SlotPower { get; set; }
        public int SlotPowerLevel { get; set; }
        public int BrewPartCount { get; set; }
        public int BrewLevel { get; set; }
        public List<RecipeItemKey> BrewPartList { get; set; }
        public List<RecipeResultKey> BrewResultList { get; set; }
        public int AdjustedReuseTime { get; set; }
        public MoonPhases MoonPhase { get; set; }

        public string CombinedEffect
        {
            get
            {
                string Result;

                switch (Effect)
                {
                    default:
                        return TextMaps.RecipeEffectTextMap[Effect];

                    case RecipeEffect.DecomposeItemByTSysLevels:
                        return "Decompose to create " + TextMaps.DecomposeMaterialTextMap[Material] + " with " + TextMaps.DecomposeSkillTextMap[Skill];

                    case RecipeEffect.ExtractTSysPower:
                        return "Extract " + TextMaps.AugmentTextMap[ExtractedAugment] + " using " + TextMaps.DecomposeMaterialTextMap[Material] + " with " + TextMaps.DecomposeSkillTextMap[Skill];

                    case RecipeEffect.RepairItemDurability:
                        return "Repair Between " + (RepairMinEfficiency * 100) + "% and " + (RepairMaxEfficiency * 100) + "% Of Item Durability, with a cooldown of " + TimeSpan.FromHours(RepairCooldown).ToString();

                    case RecipeEffect.TSysCraftedEquipment:
                        Result = "Craft " + TextMaps.CraftedBoostTextMap[Boost] + " Tier " + BoostLevel;

                        if (AdditionalEnchantments.HasValue)
                            Result += " with " + AdditionalEnchantments.Value + " additional enchantments";

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
