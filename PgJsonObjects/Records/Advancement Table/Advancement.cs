using PgJsonReader;
using Presentation;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Advancement : GenericJsonObject<Advancement>, IPgAdvancement
    {
        #region Direct Properties
        public Dictionary<DamageType, double> VulnerabilityTable { get; } = new Dictionary<DamageType, double>();
        public Dictionary<DamageType, double> MitigationTable { get; } = new Dictionary<DamageType, double>();
        public Dictionary<DamageType, double> DirectModTable { get; } = new Dictionary<DamageType, double>();
        public Dictionary<DamageType, double> IndirectModTable { get; } = new Dictionary<DamageType, double>();
        public float NonCombatRegenHealthMod { get { return (float)(RawNonCombatRegenHealthMod.HasValue ? RawNonCombatRegenHealthMod.Value : 0); } }
        public double? RawNonCombatRegenHealthMod { get; private set; }
        public float CombatRegenHealthMod { get { return (float)(RawCombatRegenHealthMod.HasValue ? RawCombatRegenHealthMod.Value : 0); } }
        public double? RawCombatRegenHealthMod { get; private set; }
        public float CombatRegenHealthDelta { get { return (float)(RawCombatRegenHealthDelta.HasValue ? RawCombatRegenHealthDelta.Value : 0); } }
        public double? RawCombatRegenHealthDelta { get; private set; }
        public float NonCombatRegenArmorMod { get { return (float)(RawNonCombatRegenArmorMod.HasValue ? RawNonCombatRegenArmorMod.Value : 0); } }
        public double? RawNonCombatRegenArmorMod { get; private set; }
        public float NonCombatRegenArmordelta { get { return (float)(RawNonCombatRegenArmordelta.HasValue ? RawNonCombatRegenArmordelta.Value : 0); } }
        public double? RawNonCombatRegenArmordelta { get; private set; }
        public float CombatRegenArmorMod { get { return (float)(RawCombatRegenArmorMod.HasValue ? RawCombatRegenArmorMod.Value : 0); } }
        public double? RawCombatRegenArmorMod { get; private set; }
        public float NonCombatRegenPowerMod { get { return (float)(RawNonCombatRegenPowerMod.HasValue ? RawNonCombatRegenPowerMod.Value : 0); } }
        public double? RawNonCombatRegenPowerMod { get; private set; }
        public float CombatRegenPowerMod { get { return (float)(RawCombatRegenPowerMod.HasValue ? RawCombatRegenPowerMod.Value : 0); } }
        public double? RawCombatRegenPowerMod { get; private set; }
        public float NonCombatRegenRageMod { get { return (float)(RawNonCombatRegenRageMod.HasValue ? RawNonCombatRegenRageMod.Value : 0); } }
        public double? RawNonCombatRegenRageMod { get; private set; }
        public float CombatRegenRageMod { get { return (float)(RawCombatRegenRageMod.HasValue ? RawCombatRegenRageMod.Value : 0); } }
        public double? RawCombatRegenRageMod { get; private set; }
        public float MentalDefenseRating { get { return (float)(RawMentalDefenseRating.HasValue ? RawMentalDefenseRating.Value : 0); } }
        public double? RawMentalDefenseRating { get; private set; }
        public float SprintBoost { get { return (float)(RawSprintBoost.HasValue ? RawSprintBoost.Value : 0); } }
        public double? RawSprintBoost { get; private set; }
        public float TauntMod { get { return (float)(RawTauntMod.HasValue ? RawTauntMod.Value : 0); } }
        public double? RawTauntMod { get; private set; }
        public float IgnoreChanceFear { get { return (float)(RawIgnoreChanceFear.HasValue ? RawIgnoreChanceFear.Value : 0); } }
        public double? RawIgnoreChanceFear { get; private set; }
        public float IgnoreChanceMezz { get { return (float)(RawIgnoreChanceMezz.HasValue ? RawIgnoreChanceMezz.Value : 0); } }
        public double? RawIgnoreChanceMezz { get; private set; }
        public float IgnoreChanceKnockback { get { return (float)(RawIgnoreChanceKnockback.HasValue ? RawIgnoreChanceKnockback.Value : 0); } }
        public double? RawIgnoreChanceKnockback { get; private set; }
        public float EvasionChance { get { return (float)(RawEvasionChance.HasValue ? RawEvasionChance.Value : 0); } }
        public double? RawEvasionChance { get; private set; }
        public float LootBoostChanceUncommon { get { return (float)(RawLootBoostChanceUncommon.HasValue ? RawLootBoostChanceUncommon.Value : 0); } }
        public double? RawLootBoostChanceUncommon { get; private set; }
        public float LootBoostChanceRare { get { return (float)(RawLootBoostChanceRare.HasValue ? RawLootBoostChanceRare.Value : 0); } }
        public double? RawLootBoostChanceRare { get; private set; }
        public float LootBoostChanceExceptional { get { return (float)(RawLootBoostChanceExceptional.HasValue ? RawLootBoostChanceExceptional.Value : 0); } }
        public double? RawLootBoostChanceExceptional { get; private set; }
        public float LootBoostChanceEpic { get { return (float)(RawLootBoostChanceEpic.HasValue ? RawLootBoostChanceEpic.Value : 0); } }
        public double? RawLootBoostChanceEpic { get; private set; }
        public float LootBoostChanceLegendary { get { return (float)(RawLootBoostChanceLegendary.HasValue ? RawLootBoostChanceLegendary.Value : 0); } }
        public double? RawLootBoostChanceLegendary { get; private set; }
        public float MaxHealth { get { return (float)(RawMaxHealth.HasValue ? RawMaxHealth.Value : 0); } }
        public double? RawMaxHealth { get; private set; }
        public float MaxArmor { get { return (float)(RawMaxArmor.HasValue ? RawMaxArmor.Value : 0); } }
        public double? RawMaxArmor { get; private set; }
        public float MaxRage { get { return (float)(RawMaxRage.HasValue ? RawMaxRage.Value : 0); } }
        public double? RawMaxRage { get; private set; }
        public float MaxPower { get { return (float)(RawMaxPower.HasValue ? RawMaxPower.Value : 0); } }
        public double? RawMaxPower { get; private set; }
        public float MaxBreath { get { return (float)(RawMaxBreath.HasValue ? RawMaxBreath.Value : 0); } }
        public double? RawMaxBreath { get; private set; }
        public float BoostUniversalDirect { get { return (float)(RawBoostUniversalDirect.HasValue ? RawBoostUniversalDirect.Value : 0); } }
        public double? RawBoostUniversalDirect { get; private set; }
        public float BoostAbilityRageAttack { get { return (float)(RawBoostAbilityRageAttack.HasValue ? RawBoostAbilityRageAttack.Value : 0); } }
        public double? RawBoostAbilityRageAttack { get; private set; }
        public float ModAbilityRageAttack { get { return (float)(RawModAbilityRageAttack.HasValue ? RawModAbilityRageAttack.Value : 0); } }
        public double? RawModAbilityRageAttack { get; private set; }
        public float MonsterCombatXpValue { get { return (float)(RawMonsterCombatXpValue.HasValue ? RawMonsterCombatXpValue.Value : 0); } }
        public double? RawMonsterCombatXpValue { get; private set; }
        public float CombatRegenArmorDelta { get { return (float)(RawCombatRegenArmorDelta.HasValue ? RawCombatRegenArmorDelta.Value : 0); } }
        public double? RawCombatRegenArmorDelta { get; private set; }
        public float CombatRegenDelta { get { return (float)(RawCombatRegenDelta.HasValue ? RawCombatRegenDelta.Value : 0); } }
        public double? RawCombatRegenDelta { get; private set; }
        public float MaxInventorySize { get { return (float)(RawMaxInventorySize.HasValue ? RawMaxInventorySize.Value : 0); } }
        public double? RawMaxInventorySize { get; private set; }
        public float MaxMetabolism { get { return (float)(RawMaxMetabolism.HasValue ? RawMaxMetabolism.Value : 0); } }
        public double? RawMaxMetabolism { get; private set; }
        public float NpcModFavorFromGifts { get { return (float)(RawNpcModFavorFromGifts.HasValue ? RawNpcModFavorFromGifts.Value : 0); } }
        public double? RawNpcModFavorFromGifts { get; private set; }
        public float NpcModFavorFromHangouts { get { return (float)(RawNpcModFavorFromHangouts.HasValue ? RawNpcModFavorFromHangouts.Value : 0); } }
        public double? RawNpcModFavorFromHangouts { get; private set; }
        public float NpcModMaxSalesValue { get { return (float)(RawNpcModMaxSalesValue.HasValue ? RawNpcModMaxSalesValue.Value : 0); } }
        public double? RawNpcModMaxSalesValue { get; private set; }
        public float NpcModTrainingCost { get { return (float)(RawNpcModTrainingCost.HasValue ? RawNpcModTrainingCost.Value : 0); } }
        public double? RawNpcModTrainingCost { get; private set; }
        public int NumInventoryFolders { get { return RawNumInventoryFolders.HasValue ? RawNumInventoryFolders.Value : 0; } }
        public int? RawNumInventoryFolders { get; private set; }
        public float HighCleanlinessXpEarnedMod { get { return (float)(RawHighCleanlinessXpEarnedMod.HasValue ? RawHighCleanlinessXpEarnedMod.Value : 0); } }
        public double? RawHighCleanlinessXpEarnedMod { get; private set; }
        public float LowCleanlinessXpEarnedMod { get { return (float)(RawLowCleanlinessXpEarnedMod.HasValue ? RawLowCleanlinessXpEarnedMod.Value : 0); } }
        public double? RawLowCleanlinessXpEarnedMod { get; private set; }
        public float MaxArmorMitigationRatio { get { return (float)(RawMaxArmorMitigationRatio.HasValue ? RawMaxArmorMitigationRatio.Value : 0); } }
        public double? RawMaxArmorMitigationRatio { get; private set; }
        public float ShowCleanlinessIndicators { get { return (float)(RawShowCleanlinessIndicators.HasValue ? RawShowCleanlinessIndicators.Value : 0); } }
        public double? RawShowCleanlinessIndicators { get; private set; }
        public float HighCommunityXpEarnedMod { get { return (float)(RawHighCommunityXpEarnedMod.HasValue ? RawHighCommunityXpEarnedMod.Value : 0); } }
        public double? RawHighCommunityXpEarnedMod { get; private set; }
        public float LowCommunityXpEarnedMod { get { return (float)(RawLowCommunityXpEarnedMod.HasValue ? RawLowCommunityXpEarnedMod.Value : 0); } }
        public double? RawLowCommunityXpEarnedMod { get; private set; }
        public float ShowCommunityIndicators { get { return (float)(RawShowCommunityIndicators.HasValue ? RawShowCommunityIndicators.Value : 0); } }
        public double? RawShowCommunityIndicators { get; private set; }
        public float HighPeaceblenessXpEarnedMod { get { return (float)(RawHighPeaceblenessXpEarnedMod.HasValue ? RawHighPeaceblenessXpEarnedMod.Value : 0); } }
        public double? RawHighPeaceblenessXpEarnedMod { get; private set; }
        public float LowPeaceblenessXpEarnedMod { get { return (float)(RawLowPeaceblenessXpEarnedMod.HasValue ? RawLowPeaceblenessXpEarnedMod.Value : 0); } }
        public double? RawLowPeaceblenessXpEarnedMod { get; private set; }
        public float ShowPeaceblenessIndicators { get { return (float)(RawShowPeaceblenessIndicators.HasValue ? RawShowPeaceblenessIndicators.Value : 0); } }
        public double? RawShowPeaceblenessIndicators { get; private set; }
        public float StaffArmorAutoHeal { get { return (float)(RawStaffArmorAutoHeal.HasValue ? RawStaffArmorAutoHeal.Value : 0); } }
        public double? RawStaffArmorAutoHeal { get; private set; }
        public float MaxMapPinsPerArea { get { return (float)(RawMaxMapPinsPerArea.HasValue ? RawMaxMapPinsPerArea.Value : 0); } }
        public double? RawMaxMapPinsPerArea { get; private set; }
        public float MaxMapPinIcons { get { return (float)(RawMaxMapPinIcons.HasValue ? RawMaxMapPinIcons.Value : 0); } }
        public double? RawMaxMapPinIcons { get; private set; }
        public float WorkOrderCoinRewardMod { get { return (float)(RawWorkOrderCoinRewardMod.HasValue ? RawWorkOrderCoinRewardMod.Value : 0); } }
        public double? RawWorkOrderCoinRewardMod { get; private set; }
        public float MaxActiveWorkOrders { get { return (float)(RawMaxActiveWorkOrders.HasValue ? RawMaxActiveWorkOrders.Value : 0); } }
        public double? RawMaxActiveWorkOrders { get; private set; }
        public float PlayerOrdersMaxActive { get { return (float)(RawPlayerOrdersMaxActive.HasValue ? RawPlayerOrdersMaxActive.Value : 0); } }
        public double? RawPlayerOrdersMaxActive { get; private set; }
        public float ShopInventorySizeDelta { get { return (float)(RawShopInventorySizeDelta.HasValue ? RawShopInventorySizeDelta.Value : 0); } }
        public double? RawShopInventorySizeDelta { get; private set; }
        public float MailShopNumFree { get { return (float)(RawMailShopNumFree.HasValue ? RawMailShopNumFree.Value : 0); } }
        public double? RawMailShopNumFree { get; private set; }
        public float ShopHiringMaxPrepDays { get { return (float)(RawShopHiringMaxPrepDays.HasValue ? RawShopHiringMaxPrepDays.Value : 0); } }
        public double? RawShopHiringMaxPrepDays { get; private set; }
        public float ShopLogDaysKept { get { return (float)(RawShopLogDaysKept.HasValue ? RawShopLogDaysKept.Value : 0); } }
        public double? RawShopLogDaysKept { get; private set; }
        public float ShopHiringNumFree { get { return (float)(RawShopHiringNumFree.HasValue ? RawShopHiringNumFree.Value : 0); } }
        public double? RawShopHiringNumFree { get; private set; }
        public float CriticalHitDamage { get { return (float)(RawCriticalHitDamage.HasValue ? RawCriticalHitDamage.Value : 0); } }
        public double? RawCriticalHitDamage { get; private set; }
        public float MonsterCritChance { get { return (float)(RawMonsterCritChance.HasValue ? RawMonsterCritChance.Value : 0); } }
        public double? RawMonsterCritChance { get; private set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return null; } }

        public int Id
        {
            get
            {
                if (Key.StartsWith("Level_"))
                {
                    if (int.TryParse(Key.Substring(6), out int Result))
                        return Result;
                }

                return 0;
            }
        }

        public List<string> VulnerabilityList { get { return GetDamageList(VulnerabilityTable, PgAdvancement.GetVulnerabilityText); } }
        public List<string> MitigationList { get { return GetDamageList(MitigationTable, PgAdvancement.GetMitigationText); } }
        public List<string> DirectModList { get { return GetDamageList(DirectModTable, PgAdvancement.GetDirectModText); } }
        public List<string> IndirectModList { get { return GetDamageList(IndirectModTable, PgAdvancement.GetIndirectModText); } }

        public static List<string> GetDamageList(Dictionary<DamageType, double> table, Func<DamageType, float, string> getText)
        {
            List<string> Result = new List<string>();
            foreach (KeyValuePair<DamageType, double> Entry in table)
                Result.Add(getText(Entry.Key, (float)Entry.Value));

            return Result;
        }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "IGNORE_CHANCE_FEAR", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawIgnoreChanceFear = value,
                GetFloat = () => RawIgnoreChanceFear } },
            { "IGNORE_CHANCE_MEZ", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawIgnoreChanceMezz = value,
                GetFloat = () => RawIgnoreChanceMezz } },
            { "IGNORE_CHANCE_KNOCKBACK", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawIgnoreChanceKnockback = value,
                GetFloat = () => RawIgnoreChanceKnockback } },
            { "MENTAL_DEFENSE_RATING", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMentalDefenseRating = value,
                GetFloat = () => RawMentalDefenseRating } },
            { "NONCOMBAT_REGEN_HEALTH_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawNonCombatRegenHealthMod = value,
                GetFloat = () => RawNonCombatRegenHealthMod } },
            { "COMBAT_REGEN_HEALTH_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawCombatRegenHealthMod = value,
                GetFloat = () => RawCombatRegenHealthMod } },
            { "COMBAT_REGEN_HEALTH_DELTA", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawCombatRegenHealthDelta = value,
                GetFloat = () => RawCombatRegenHealthDelta } },
            { "NONCOMBAT_REGEN_ARMOR_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawNonCombatRegenArmorMod = value,
                GetFloat = () => RawNonCombatRegenArmorMod } },
            { "NONCOMBAT_REGEN_ARMOR_DELTA", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawNonCombatRegenArmordelta = value,
                GetFloat = () => RawNonCombatRegenArmordelta } },
            { "COMBAT_REGEN_ARMOR_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawCombatRegenArmorMod = value,
                GetFloat = () => RawCombatRegenArmorMod } },
            { "NONCOMBAT_REGEN_POWER_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawNonCombatRegenPowerMod = value,
                GetFloat = () => RawNonCombatRegenPowerMod } },
            { "COMBAT_REGEN_POWER_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawCombatRegenPowerMod = value,
                GetFloat = () => RawCombatRegenPowerMod } },
            { "NONCOMBAT_REGEN_RAGE_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawNonCombatRegenRageMod = value,
                GetFloat = () => RawNonCombatRegenRageMod } },
            { "COMBAT_REGEN_RAGE_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawCombatRegenRageMod = value,
                GetFloat = () => RawCombatRegenRageMod } },
            { "SPRINT_BOOST", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawSprintBoost = value,
                GetFloat = () => RawSprintBoost } },
            { "TAUNT_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawTauntMod = value,
                GetFloat = () => RawTauntMod } },
            { "EVASION_CHANCE", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawEvasionChance = value,
                GetFloat = () => RawEvasionChance } },
            { "LOOT_BOOST_CHANCE_UNCOMMON", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawLootBoostChanceUncommon = value,
                GetFloat = () => RawLootBoostChanceUncommon } },
            { "LOOT_BOOST_CHANCE_RARE", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawLootBoostChanceRare = value,
                GetFloat = () => RawLootBoostChanceRare } },
            { "LOOT_BOOST_CHANCE_EXCEPTIONAL", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawLootBoostChanceExceptional = value,
                GetFloat = () => RawLootBoostChanceExceptional } },
            { "LOOT_BOOST_CHANCE_EPIC", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawLootBoostChanceEpic = value,
                GetFloat = () => RawLootBoostChanceEpic } },
            { "LOOT_BOOST_CHANCE_LEGENDARY", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawLootBoostChanceLegendary = value,
                GetFloat = () => RawLootBoostChanceLegendary } },
            { "MAX_HEALTH", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMaxHealth = value,
                GetFloat = () => RawMaxHealth } },
            { "MAX_ARMOR", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMaxArmor = value,
                GetFloat = () => RawMaxArmor } },
            { "MAX_RAGE", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMaxRage = value,
                GetFloat = () => RawMaxRage } },
            { "MAX_POWER", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMaxPower = value,
                GetFloat = () => RawMaxPower } },
            { "MAX_BREATH", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMaxBreath = value,
                GetFloat = () => RawMaxBreath } },
            { "BOOST_UNIVERSAL_DIRECT", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawBoostUniversalDirect = value,
                GetFloat = () => RawBoostUniversalDirect } },
            { "BOOST_ABILITY_RAGEATTACK", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawBoostAbilityRageAttack = value,
                GetFloat = () => RawBoostAbilityRageAttack } },
            { "MOD_ABILITY_RAGEATTACK", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawModAbilityRageAttack = value,
                GetFloat = () => RawModAbilityRageAttack } },
            { "MONSTER_COMBAT_XP_VALUE", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMonsterCombatXpValue = value,
                GetFloat = () => RawMonsterCombatXpValue } },
            { "COMBAT_REGEN_ARMOR_DELTA", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawCombatRegenArmorDelta = value,
                GetFloat = () => RawCombatRegenArmorDelta } },
            { "COMBAT_REGEN_POWER_DELTA", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawCombatRegenDelta = value,
                GetFloat = () => RawCombatRegenDelta } },
            { "MAX_INVENTORY_SIZE", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMaxInventorySize = value,
                GetFloat = () => RawMaxInventorySize } },
            { "MAX_METABOLISM", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMaxMetabolism = value,
                GetFloat = () => RawMaxMetabolism } },
            { "NPC_MOD_FAVORFROMGIFTS", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawNpcModFavorFromGifts = value,
                GetFloat = () => RawNpcModFavorFromGifts } },
            { "NPC_MOD_FAVORFROMHANGOUTS", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawNpcModFavorFromHangouts = value,
                GetFloat = () => RawNpcModFavorFromHangouts } },
            { "NPC_MOD_MAXSALESVALUE", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawNpcModMaxSalesValue = value,
                GetFloat = () => RawNpcModMaxSalesValue } },
            { "NPC_MOD_TRAININGCOST", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawNpcModTrainingCost = value,
                GetFloat = () => RawNpcModTrainingCost } },
            { "NUM_INVENTORY_FOLDERS", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawNumInventoryFolders = value,
                GetInteger = () => RawNumInventoryFolders } },
            { "HIGH_CLEANLINESS_XP_EARNED_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawHighCleanlinessXpEarnedMod = value,
                GetFloat = () => RawHighCleanlinessXpEarnedMod } },
            { "LOW_CLEANLINESS_XP_EARNED_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawLowCleanlinessXpEarnedMod = value,
                GetFloat = () => RawLowCleanlinessXpEarnedMod } },
            { "MAX_ARMOR_MITIGATION_RATIO", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMaxArmorMitigationRatio = value,
                GetFloat = () => RawMaxArmorMitigationRatio } },
            { "SHOW_CLEANLINESS_INDICATORS", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawShowCleanlinessIndicators = value,
                GetFloat = () => RawShowCleanlinessIndicators } },
            { "HIGH_COMMUNITY_XP_EARNED_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawHighCommunityXpEarnedMod = value,
                GetFloat = () => RawHighCommunityXpEarnedMod } },
            { "LOW_COMMUNITY_XP_EARNED_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawLowCommunityXpEarnedMod = value,
                GetFloat = () => RawLowCommunityXpEarnedMod } },
            { "SHOW_COMMUNITY_INDICATORS", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawShowCommunityIndicators = value,
                GetFloat = () => RawShowCommunityIndicators } },
            { "HIGH_PEACEABLENESS_XP_EARNED_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawHighPeaceblenessXpEarnedMod = value,
                GetFloat = () => RawHighPeaceblenessXpEarnedMod } },
            { "LOW_PEACEABLENESS_XP_EARNED_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawLowPeaceblenessXpEarnedMod = value,
                GetFloat = () => RawLowPeaceblenessXpEarnedMod } },
            { "SHOW_PEACEABLENESS_INDICATORS", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawShowPeaceblenessIndicators = value,
                GetFloat = () => RawShowPeaceblenessIndicators } },
            { "STAFF_ARMOR_AUTOHEAL", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawStaffArmorAutoHeal = value,
                GetFloat = () => RawStaffArmorAutoHeal } },
            { "MAX_MAP_PINS_PER_AREA", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMaxMapPinsPerArea = value,
                GetFloat = () => RawMaxMapPinsPerArea } },
            { "MAX_MAP_PIN_ICONS", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMaxMapPinIcons = value,
                GetFloat = () => RawMaxMapPinIcons } },
            { "WORKORDER_COIN_REWARD_MOD", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawWorkOrderCoinRewardMod = value,
                GetFloat = () => RawWorkOrderCoinRewardMod } },
            { "MAX_ACTIVE_WORKORDERS", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMaxActiveWorkOrders = value,
                GetFloat = () => RawMaxActiveWorkOrders } },
            { "PLAYER_ORDERS_MAX_ACTIVE", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawPlayerOrdersMaxActive = value,
                GetFloat = () => RawPlayerOrdersMaxActive } },
            { "SHOP_INVENTORY_SIZE_DELTA", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawShopInventorySizeDelta = value,
                GetFloat = () => RawShopInventorySizeDelta } },
            { "MAIL_SHOP_NUMFREE", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMailShopNumFree = value,
                GetFloat = () => RawMailShopNumFree } },
            { "SHOP_HIRING_MAX_PREPAY_DAYS", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawShopHiringMaxPrepDays = value,
                GetFloat = () => RawShopHiringMaxPrepDays } },
            { "SHOP_LOG_DAYSKEPT", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawShopLogDaysKept = value,
                GetFloat = () => RawShopLogDaysKept } },
            { "SHOP_HIRING_NUMFREE", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawShopHiringNumFree = value,
                GetFloat = () => RawShopHiringNumFree } },
            { "MOD_CRITICAL_HIT_DAMAGE", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawCriticalHitDamage = value,
                GetFloat = () => RawCriticalHitDamage } },
            { "MONSTER_CRIT_CHANCE", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMonsterCritChance = value,
                GetFloat = () => RawMonsterCritChance } },
        }; } }

        protected override bool IsCustomFieldParsed(string FieldKey, object FieldValue, ParseErrorInfo ErrorInfo)
        {
            if (ParseDamageTypeEntry(FieldKey, FieldValue, "VULN_", null, VulnerabilityTable, ErrorInfo))
            {
                ParsedFields["VULNERABILITY"] = true;
                FieldTableOrder.Add(FieldKey);
                return true;
            }

            else if (ParseDamageTypeEntry(FieldKey, FieldValue, "MITIGATION_", null, MitigationTable, ErrorInfo))
            {
                ParsedFields["MITIGATION"] = true;
                FieldTableOrder.Add(FieldKey);
                return true;
            }

            else if (ParseDamageTypeEntry(FieldKey, FieldValue, "MOD_", "_INDIRECT", IndirectModTable, ErrorInfo))
            {
                ParsedFields["MOD_INDIRECT"] = true;
                FieldTableOrder.Add(FieldKey);
                return true;
            }

            else if (ParseDamageTypeEntry(FieldKey, FieldValue, "MOD_", "_DIRECT", DirectModTable, ErrorInfo))
            {
                ParsedFields["MOD_DIRECT"] = true;
                FieldTableOrder.Add(FieldKey);
                return true;
            }
            else
                return false;
        }

        private bool ParseDamageTypeEntry(string FieldKey, object FieldValue, string StartPattern, string EndPattern, Dictionary<DamageType, double> DamageTypeTable, ParseErrorInfo ErrorInfo)
        {
            if (IsDamageTypeEntry(FieldKey, StartPattern, EndPattern, out string RawDamageType, out DamageType ParsedDamageType, ErrorInfo))
            {
                if (DamageTypeTable.ContainsKey(ParsedDamageType))
                    ErrorInfo.AddDuplicateString("Advancement", RawDamageType);
                else
                {
                    double Value = ParseValue(FieldValue, ErrorInfo);
                    DamageTypeTable.Add(ParsedDamageType, Value);
                }

                return true;
            }

            return false;
        }

        private bool IsDamageTypeEntry(string FieldKey, string StartPattern, string EndPattern, out string RawDamageType, out DamageType ParsedDamageType, ParseErrorInfo ErrorInfo)
        {
            if (FieldKey.StartsWith(StartPattern) && (EndPattern == null || FieldKey.EndsWith(EndPattern)))
            {
                if (EndPattern == null)
                    RawDamageType = FieldKey[StartPattern.Length] + FieldKey.Substring(StartPattern.Length + 1).ToLower();
                else
                    RawDamageType = FieldKey[StartPattern.Length] + FieldKey.Substring(StartPattern.Length + 1, FieldKey.Length - StartPattern.Length - EndPattern.Length - 1).ToLower();

                if (StringToEnumConversion<DamageType>.TryParse(RawDamageType, out ParsedDamageType, ErrorInfo))
                    return true;
            }

            RawDamageType = null;
            ParsedDamageType = DamageType.Internal_None;
            return false;
        }

        private double ParseValue(object Value, ParseErrorInfo ErrorInfo)
        {
            JsonInteger AsJsonInteger;
            JsonFloat AsJsonFloat;

            if ((AsJsonInteger = Value as JsonInteger) != null)
                return AsJsonInteger.Number;

            else if ((AsJsonFloat = Value as JsonFloat) != null)
                return AsJsonFloat.Number;

            else
            {
                ErrorInfo.AddInvalidObjectFormat("Advancement Value");
                return 0;
            }
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (KeyValuePair<DamageType, double> Entry in VulnerabilityTable)
                    AddWithFieldSeparator(ref Result, TextMaps.DamageTypeTextMap[Entry.Key]);
                foreach (KeyValuePair<DamageType, double> Entry in MitigationTable)
                    AddWithFieldSeparator(ref Result, TextMaps.DamageTypeTextMap[Entry.Key]);
                foreach (KeyValuePair<DamageType, double> Entry in DirectModTable)
                    AddWithFieldSeparator(ref Result, TextMaps.DamageTypeTextMap[Entry.Key]);
                foreach (KeyValuePair<DamageType, double> Entry in IndirectModTable)
                    AddWithFieldSeparator(ref Result, TextMaps.DamageTypeTextMap[Entry.Key]);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            return false;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Advancement"; } }
        #endregion

        #region Json Reconstruction
        public override void ListObjectContent(JsonGenerator generator, string ParserKey)
        {
            string RawDamageType;
            DamageType ParsedDamageType;

            if (IsDamageTypeEntry(ParserKey, "VULN_", null, out RawDamageType, out ParsedDamageType, null))
            {
                generator.AddDouble("VULN_" + RawDamageType.ToUpper(), VulnerabilityTable[ParsedDamageType]);
                return;
            }

            else if (IsDamageTypeEntry(ParserKey, "MITIGATION_", null, out RawDamageType, out ParsedDamageType, null))
            {
                generator.AddDouble("MITIGATION_" + RawDamageType.ToUpper(), MitigationTable[ParsedDamageType]);
                return;
            }

            else if (IsDamageTypeEntry(ParserKey, "MOD_", "_INDIRECT", out RawDamageType, out ParsedDamageType, null))
            {
                generator.AddDouble("MOD_" + RawDamageType.ToUpper() + "_INDIRECT", IndirectModTable[ParsedDamageType]);
                return;
            }

            else if (IsDamageTypeEntry(ParserKey, "MOD_", "_DIRECT", out RawDamageType, out ParsedDamageType, null))
            {
                generator.AddDouble("MOD_" + RawDamageType.ToUpper() + "_DIRECT", DirectModTable[ParsedDamageType]);
                return;
            }

            else
                base.ListObjectContent(generator, ParserKey);
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();
            Dictionary<int, List<int>> StoredIntListTable = new Dictionary<int, List<int>>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddDouble(RawNonCombatRegenHealthMod, data, ref offset, BaseOffset, 4);
            AddDouble(RawCombatRegenHealthMod, data, ref offset, BaseOffset, 8);
            AddDouble(RawCombatRegenHealthDelta, data, ref offset, BaseOffset, 12);
            AddDouble(RawNonCombatRegenArmorMod, data, ref offset, BaseOffset, 16);
            AddDouble(RawNonCombatRegenArmordelta, data, ref offset, BaseOffset, 20);
            AddDouble(RawCombatRegenArmorMod, data, ref offset, BaseOffset, 24);
            AddDouble(RawNonCombatRegenPowerMod, data, ref offset, BaseOffset, 28);
            AddDouble(RawCombatRegenPowerMod, data, ref offset, BaseOffset, 32);
            AddDouble(RawNonCombatRegenRageMod, data, ref offset, BaseOffset, 36);
            AddDouble(RawCombatRegenRageMod, data, ref offset, BaseOffset, 40);
            AddDouble(RawMentalDefenseRating, data, ref offset, BaseOffset, 44);
            AddDouble(RawSprintBoost, data, ref offset, BaseOffset, 48);
            AddDouble(RawTauntMod, data, ref offset, BaseOffset, 52);
            AddDouble(RawIgnoreChanceFear, data, ref offset, BaseOffset, 56);
            AddDouble(RawIgnoreChanceMezz, data, ref offset, BaseOffset, 60);
            AddDouble(RawIgnoreChanceKnockback, data, ref offset, BaseOffset, 64);
            AddDouble(RawEvasionChance, data, ref offset, BaseOffset, 68);
            AddDouble(RawLootBoostChanceUncommon, data, ref offset, BaseOffset, 72);
            AddDouble(RawLootBoostChanceRare, data, ref offset, BaseOffset, 76);
            AddDouble(RawLootBoostChanceExceptional, data, ref offset, BaseOffset, 80);
            AddDouble(RawLootBoostChanceEpic, data, ref offset, BaseOffset, 84);
            AddDouble(RawLootBoostChanceLegendary, data, ref offset, BaseOffset, 88);
            AddDouble(RawMaxHealth, data, ref offset, BaseOffset, 92);
            AddDouble(RawMaxArmor, data, ref offset, BaseOffset, 96);
            AddDouble(RawMaxRage, data, ref offset, BaseOffset, 100);
            AddDouble(RawMaxPower, data, ref offset, BaseOffset, 104);
            AddDouble(RawMaxBreath, data, ref offset, BaseOffset, 108);
            AddDouble(RawBoostUniversalDirect, data, ref offset, BaseOffset, 112);
            AddDouble(RawBoostAbilityRageAttack, data, ref offset, BaseOffset, 116);
            AddDouble(RawModAbilityRageAttack, data, ref offset, BaseOffset, 120);
            AddDouble(RawMonsterCombatXpValue, data, ref offset, BaseOffset, 124);
            AddDouble(RawCombatRegenArmorDelta, data, ref offset, BaseOffset, 128);
            AddDouble(RawCombatRegenDelta, data, ref offset, BaseOffset, 132);
            AddDouble(RawMaxInventorySize, data, ref offset, BaseOffset, 136);
            AddDouble(RawMaxMetabolism, data, ref offset, BaseOffset, 140);
            AddDouble(RawNpcModFavorFromGifts, data, ref offset, BaseOffset, 144);
            AddDouble(RawNpcModFavorFromHangouts, data, ref offset, BaseOffset, 148);
            AddDouble(RawNpcModMaxSalesValue, data, ref offset, BaseOffset, 152);
            AddDouble(RawNpcModTrainingCost, data, ref offset, BaseOffset, 156);
            AddInt(RawNumInventoryFolders, data, ref offset, BaseOffset, 160);
            AddDouble(RawHighCleanlinessXpEarnedMod, data, ref offset, BaseOffset, 164);
            AddDouble(RawLowCleanlinessXpEarnedMod, data, ref offset, BaseOffset, 168);
            AddDouble(RawMaxArmorMitigationRatio, data, ref offset, BaseOffset, 172);
            AddDouble(RawShowCleanlinessIndicators, data, ref offset, BaseOffset, 176);
            AddDouble(RawHighCommunityXpEarnedMod, data, ref offset, BaseOffset, 180);
            AddDouble(RawLowCommunityXpEarnedMod, data, ref offset, BaseOffset, 184);
            AddDouble(RawShowCommunityIndicators, data, ref offset, BaseOffset, 188);
            AddDouble(RawHighPeaceblenessXpEarnedMod, data, ref offset, BaseOffset, 192);
            AddDouble(RawLowPeaceblenessXpEarnedMod, data, ref offset, BaseOffset, 196);
            AddDouble(RawShowPeaceblenessIndicators, data, ref offset, BaseOffset, 200);
            AddDouble(RawStaffArmorAutoHeal, data, ref offset, BaseOffset, 204);
            AddDouble(RawMaxMapPinsPerArea, data, ref offset, BaseOffset, 208);
            AddDouble(RawMaxMapPinIcons, data, ref offset, BaseOffset, 212);
            AddDouble(RawWorkOrderCoinRewardMod, data, ref offset, BaseOffset, 216);
            AddDouble(RawMaxActiveWorkOrders, data, ref offset, BaseOffset, 220);
            AddDouble(RawPlayerOrdersMaxActive, data, ref offset, BaseOffset, 224);
            AddDouble(RawShopInventorySizeDelta, data, ref offset, BaseOffset, 228);
            AddDouble(RawMailShopNumFree, data, ref offset, BaseOffset, 232);
            AddDouble(RawShopHiringMaxPrepDays, data, ref offset, BaseOffset, 236);
            AddDouble(RawShopLogDaysKept, data, ref offset, BaseOffset, 240);
            AddDouble(RawShopHiringNumFree, data, ref offset, BaseOffset, 244);
            AddDouble(RawCriticalHitDamage, data, ref offset, BaseOffset, 248);
            AddDouble(RawMonsterCritChance, data, ref offset, BaseOffset, 252);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 256, StoredStringListTable);

            List<int> VulnerabilityList = new List<int>();
            foreach (KeyValuePair<DamageType, double> Entry in VulnerabilityTable)
            {
                VulnerabilityList.Add((int)Entry.Key);
                VulnerabilityList.Add((int)Math.Round(Entry.Value * PgAdvancement.DamageMultiplier));
            }
            AddIntList(VulnerabilityList, data, ref offset, BaseOffset, 260, StoredIntListTable);

            List<int> MitigationList = new List<int>();
            foreach (KeyValuePair<DamageType, double> Entry in MitigationTable)
            {
                MitigationList.Add((int)Entry.Key);
                MitigationList.Add((int)Math.Round(Entry.Value * PgAdvancement.DamageMultiplier));
            }
            AddIntList(MitigationList, data, ref offset, BaseOffset, 264, StoredIntListTable);

            List<int> DirectModList = new List<int>();
            foreach (KeyValuePair<DamageType, double> Entry in DirectModTable)
            {
                DirectModList.Add((int)Entry.Key);
                DirectModList.Add((int)Math.Round(Entry.Value * PgAdvancement.DamageMultiplier));
            }
            AddIntList(DirectModList, data, ref offset, BaseOffset, 268, StoredIntListTable);

            List<int> IndirectModList = new List<int>();
            foreach (KeyValuePair<DamageType, double> Entry in IndirectModTable)
            {
                IndirectModList.Add((int)Entry.Key);
                IndirectModList.Add((int)Math.Round(Entry.Value * PgAdvancement.DamageMultiplier));
            }
            AddIntList(IndirectModList, data, ref offset, BaseOffset, 272, StoredIntListTable);

            FinishSerializing(data, ref offset, BaseOffset, 276, StoredStringtable, null, null, null, StoredIntListTable, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
