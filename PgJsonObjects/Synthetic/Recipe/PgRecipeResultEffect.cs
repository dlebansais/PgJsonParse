using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRecipeResultEffect : GenericPgObject<PgRecipeResultEffect>, IPgRecipeResultEffect
    {
        public PgRecipeResultEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgRecipeResultEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgRecipeResultEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgRecipeResultEffect(data, ref offset);
        }

        public override string Key { get { return null; } }
        public RecipeEffect Effect { get { return GetEnum<RecipeEffect>(0); } }
        public CraftedBoost Boost { get { return GetEnum<CraftedBoost>(2); } }
        public int MinLevel { get { return RawMinLevel.HasValue ? RawMinLevel.Value : 0; } }
        public int? RawMinLevel { get { return GetInt(4); } }
        public int MaxLevel { get { return RawMaxLevel.HasValue ? RawMaxLevel.Value : 0; } }
        public int? RawMaxLevel { get { return GetInt(8); } }
        public DecomposeSkill Skill { get { return GetEnum<DecomposeSkill>(12); } }
        public Augment ExtractedAugment { get { return GetEnum<Augment>(14); } }
        public float RepairMinEfficiency { get { return RawRepairMinEfficiency.HasValue ? RawRepairMinEfficiency.Value : 0; } }
        public float? RawRepairMinEfficiency { get { return (float?)GetDouble(16); } }
        public float RepairMaxEfficiency { get { return RawRepairMaxEfficiency.HasValue ? RawRepairMaxEfficiency.Value : 0; } }
        public float? RawRepairMaxEfficiency { get { return (float?)GetDouble(20); } }
        public FloatFormat RepairMinEfficiencyFormat { get { return GetEnum<FloatFormat>(24); } }
        public FloatFormat RepairMaxEfficiencyFormat { get { return GetEnum<FloatFormat>(26); } }
        public int RepairCooldown { get { return RawRepairCooldown.HasValue ? RawRepairCooldown.Value : 0; } }
        public int? RawRepairCooldown { get { return GetInt(28); } }
        public int BoostLevel { get { return RawBoostLevel.HasValue ? RawBoostLevel.Value : 0; } }
        public int? RawBoostLevel { get { return GetInt(32); } }
        public int AdditionalEnchantments { get { return RawAdditionalEnchantments.HasValue ? RawAdditionalEnchantments.Value : 0; } }
        public int? RawAdditionalEnchantments { get { return GetInt(36); } }
        public Appearance BoostedAnimal { get { return GetEnum<Appearance>(40); } }
        public EnhancementEffect Enhancement { get { return GetEnum<EnhancementEffect>(42); } }
        public float AddedQuantity { get { return RawAddedQuantity.HasValue ? RawAddedQuantity.Value : 0; } }
        public float? RawAddedQuantity { get { return (float?)GetDouble(44); } }
        public int ConsumedEnhancementPoints { get { return RawConsumedEnhancementPoints.HasValue ? RawConsumedEnhancementPoints.Value : 0; } }
        public int? RawConsumedEnhancementPoints { get { return GetInt(48); } }
        public ShamanicSlotPower SlotPower { get { return GetEnum<ShamanicSlotPower>(52); } }
        public MoonPhases MoonPhase { get { return GetEnum<MoonPhases>(54); } }
        public int SlotPowerLevel { get { return RawSlotPowerLevel.HasValue ? RawSlotPowerLevel.Value : 0; } }
        public int? RawSlotPowerLevel { get { return GetInt(56); } }
        public int BrewPartCount { get { return RawBrewPartCount.HasValue ? RawBrewPartCount.Value : 0; } }
        public int? RawBrewPartCount { get { return GetInt(60); } }
        public int BrewLevel { get { return RawBrewLevel.HasValue ? RawBrewLevel.Value : 0; } }
        public int? RawBrewLevel { get { return GetInt(64); } }
        public List<RecipeItemKey> BrewPartList { get { return GetEnumList(68, ref _BrewPartList); } } private List<RecipeItemKey> _BrewPartList;
        public List<RecipeResultKey> BrewResultList { get { return GetEnumList(72, ref _BrewResultList); } } private List<RecipeResultKey> _BrewResultList;
        public int AdjustedReuseTime { get { return RawAdjustedReuseTime.HasValue ? RawAdjustedReuseTime.Value : 0; } }
        public int? RawAdjustedReuseTime { get { return GetInt(76); } }
        public IPgItem Item { get { return GetObject(80, ref _Item, PgItem.CreateNew); } } private IPgItem _Item;
        public bool IsCamouflaged { get { return RawIsCamouflaged.HasValue && RawIsCamouflaged.Value; } }
        public bool? RawIsCamouflaged { get { return GetBool(84, 0); } }
        public PowerWaxType PowerWaxType { get { return GetEnum<PowerWaxType>(86); } }
        public RecipeItemKey RecipeItemKey { get { return GetEnum<RecipeItemKey>(88); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }
        protected override List<string> FieldTableOrder { get { return new List<string>(); } }

        public override string SortingName { get { return null; } }

        public string CombinedEffect
        {
            get { return PgRecipeResultEffect.GetCombinedEffect(this); }
        }

        public static string GetCombinedEffect(IPgRecipeResultEffect item)
        {
            string Result;

            switch (item.Effect)
            {
                default:
                    return TextMaps.RecipeEffectTextMap[item.Effect];

                case RecipeEffect.ExtractTSysPower:
                    return "Extract " + TextMaps.AugmentTextMap[item.ExtractedAugment] + " using material level " + item.MinLevel + "-" + item.MaxLevel + " with " + TextMaps.DecomposeSkillTextMap[item.Skill];

                case RecipeEffect.RepairItemDurability:
                    return "Repair Between " + (item.RepairMinEfficiency * 100) + "% and " + (item.RepairMaxEfficiency * 100) + "% Of Item Durability, with a cooldown of " + TimeSpan.FromHours(item.RepairCooldown).ToString() + ", items in level range " + item.MinLevel + "-" + item.MaxLevel;

                case RecipeEffect.TSysCraftedEquipment:
                    Result = "Craft " + TextMaps.CraftedBoostTextMap[item.Boost] + " Tier " + item.BoostLevel;

                    if (item.RawAdditionalEnchantments.HasValue)
                        Result += " with " + item.RawAdditionalEnchantments.Value + " additional enchantments";

                    if (item.BoostedAnimal != Appearance.Internal_None)
                        Result += " for " + TextMaps.AppearanceTextMap[item.BoostedAnimal] + " only";

                    return Result;

                case RecipeEffect.CraftingEnhanceItem:
                    Result = "Add " + TextMaps.EnhancementEffectTextMap[item.Enhancement];

                    switch (item.Enhancement)
                    {
                        case EnhancementEffect.Pockets:
                        case EnhancementEffect.Armor:
                            Result += " (" + (int)item.AddedQuantity + ")";
                            break;

                        default:
                            Result += " (" + (int)(item.AddedQuantity * 100) + "%)";
                            break;
                    }

                    Result += " and consume " + item.ConsumedEnhancementPoints + " Craft Points";
                    return Result;

                case RecipeEffect.AddItemTSysPower:
                    return "Infuse " + TextMaps.ShamanicSlotPowerTextMap[item.SlotPower] + " (Tier " + item.SlotPowerLevel + "), consuming 100 Craft Points";

                case RecipeEffect.BrewItem:
                    return "Brewed drink";

                case RecipeEffect.AdjustRecipeReuseTime:
                    return "Adjust Recipe Reuse Time, " + item.AdjustedReuseTime + "s during " + item.MoonPhase;

                case RecipeEffect.GiveItemPower:
                    return "Give Power to " + (item.Item != null ? item.Item.Name : " unknown item");

                case RecipeEffect.AddItemTSysPowerWax:
                    return "Wax " + TextMaps.PowerWaxTypeTextMap[item.PowerWaxType] + " (Tier " + item.SlotPowerLevel + ", " + "..." + "), consuming 100 Craft Points";

                case RecipeEffect.ConsumeItemUses:
                    return "Consume Item Uses " + TextMaps.RecipeItemKeyTextMap[item.RecipeItemKey] + " (" + item.AdjustedReuseTime + ")";

                case RecipeEffect.DeltaCurFairyEnergy:
                    return "Delta Cur Fairy Energy (" + item.BoostLevel + ")";

                case RecipeEffect.Teleport:
                    return "Teleport(" + ")";
            }
        }
    }
}
