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
        public string BoostedAnimal { get; set; }
        public EnhancementEffect Enhancement { get; set; }
        public float AddedQuantity { get; set; }
        public int ConsumedEnhancementPoints { get; set; }
        public ShamanicSlotPower SlotPower { get; set; }
        public int SlotPowerLevel { get; set; }
        public int BrewPartCount { get; set; }
        public int BrewLevel { get; set; }
        public List<RecipeItemKey> BrewPartList { get; set; }
        public List<RecipeResultKey> BrewResultList { get; set; }
    }
}
