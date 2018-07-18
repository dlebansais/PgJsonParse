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
        public bool IsCamouflaged { get { return RawIsCamouflaged.HasValue && RawIsCamouflaged.Value; } }
        public bool? RawIsCamouflaged { get { return GetBool(80, 0); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }
        protected override List<string> FieldTableOrder { get { return new List<string>(); } }

        public override string SortingName { get { return null; } }
    }
}
