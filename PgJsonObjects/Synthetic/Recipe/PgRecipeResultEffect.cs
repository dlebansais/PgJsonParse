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

        public override string Key { get { return GetString(0); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(4, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public RecipeEffect Effect { get { return GetEnum<RecipeEffect>(8); } }
        public CraftedBoost Boost { get { return GetEnum<CraftedBoost>(10); } }
        public int MinLevel { get { return RawMinLevel.HasValue ? RawMinLevel.Value : 0; } }
        public int? RawMinLevel { get { return GetInt(12); } }
        public int MaxLevel { get { return RawMaxLevel.HasValue ? RawMaxLevel.Value : 0; } }
        public int? RawMaxLevel { get { return GetInt(16); } }
        public DecomposeSkill Skill { get { return GetEnum<DecomposeSkill>(20); } }
        public Augment ExtractedAugment { get { return GetEnum<Augment>(22); } }
        public float RepairMinEfficiency { get { return RawRepairMinEfficiency.HasValue ? RawRepairMinEfficiency.Value : 0; } }
        public float? RawRepairMinEfficiency { get { return (float?)GetDouble(24); } }
        public float RepairMaxEfficiency { get { return RawRepairMaxEfficiency.HasValue ? RawRepairMaxEfficiency.Value : 0; } }
        public float? RawRepairMaxEfficiency { get { return (float?)GetDouble(28); } }
        public FloatFormat RepairMinEfficiencyFormat { get { return GetEnum<FloatFormat>(32); } }
        public FloatFormat RepairMaxEfficiencyFormat { get { return GetEnum<FloatFormat>(34); } }
        public int RepairCooldown { get { return RawRepairCooldown.HasValue ? RawRepairCooldown.Value : 0; } }
        public int? RawRepairCooldown { get { return GetInt(36); } }
        public int BoostLevel { get { return RawBoostLevel.HasValue ? RawBoostLevel.Value : 0; } }
        public int? RawBoostLevel { get { return GetInt(40); } }
        public int AdditionalEnchantments { get { return RawAdditionalEnchantments.HasValue ? RawAdditionalEnchantments.Value : 0; } }
        public int? RawAdditionalEnchantments { get { return GetInt(44); } }
        public Appearance BoostedAnimal { get { return GetEnum<Appearance>(48); } }
        public EnhancementEffect Enhancement { get { return GetEnum<EnhancementEffect>(50); } }
        public float AddedQuantity { get { return RawAddedQuantity.HasValue ? RawAddedQuantity.Value : 0; } }
        public float? RawAddedQuantity { get { return (float?)GetDouble(52); } }
        public int ConsumedEnhancementPoints { get { return RawConsumedEnhancementPoints.HasValue ? RawConsumedEnhancementPoints.Value : 0; } }
        public int? RawConsumedEnhancementPoints { get { return GetInt(56); } }
        public ShamanicSlotPower SlotPower { get { return GetEnum<ShamanicSlotPower>(60); } }
        public MoonPhases MoonPhase { get { return GetEnum<MoonPhases>(62); } }
        public int SlotPowerLevel { get { return RawSlotPowerLevel.HasValue ? RawSlotPowerLevel.Value : 0; } }
        public int? RawSlotPowerLevel { get { return GetInt(64); } }
        public int BrewPartCount { get { return RawBrewPartCount.HasValue ? RawBrewPartCount.Value : 0; } }
        public int? RawBrewPartCount { get { return GetInt(68); } }
        public int BrewLevel { get { return RawBrewLevel.HasValue ? RawBrewLevel.Value : 0; } }
        public int? RawBrewLevel { get { return GetInt(72); } }
        public List<RecipeItemKey> BrewPartList { get { return GetEnumList(76, ref _BrewPartList); } } private List<RecipeItemKey> _BrewPartList;
        public List<RecipeResultKey> BrewResultList { get { return GetEnumList(80, ref _BrewResultList); } } private List<RecipeResultKey> _BrewResultList;
        public int AdjustedReuseTime { get { return RawAdjustedReuseTime.HasValue ? RawAdjustedReuseTime.Value : 0; } }
        public int? RawAdjustedReuseTime { get { return GetInt(84); } }
        public bool IsCamouflaged { get { return RawIsCamouflaged.HasValue && RawIsCamouflaged.Value; } }
        public bool? RawIsCamouflaged { get { return GetBool(88, 0); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }
    }
}
