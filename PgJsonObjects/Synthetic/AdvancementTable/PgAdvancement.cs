using Presentation;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAdvancement : GenericPgObject<PgAdvancement>, IPgAdvancement
    {
        public PgAdvancement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAdvancement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAdvancement CreateNew(byte[] data, ref int offset)
        {
            PgAdvancement Result = new PgAdvancement(data, ref offset);
            string Key = Result.Key;
            return Result;
        }

        public override string Key { get { return GetString(0); } }
        public float NonCombatRegenHealthMod { get { return (float)(RawNonCombatRegenHealthMod.HasValue ? RawNonCombatRegenHealthMod.Value : 0); } }
        public double? RawNonCombatRegenHealthMod { get { return GetDouble(4); } }
        public float CombatRegenHealthMod { get { return (float)(RawCombatRegenHealthMod.HasValue ? RawCombatRegenHealthMod.Value : 0); } }
        public double? RawCombatRegenHealthMod { get { return GetDouble(8); } }
        public float CombatRegenHealthDelta { get { return (float)(RawCombatRegenHealthDelta.HasValue ? RawCombatRegenHealthDelta.Value : 0); } }
        public double? RawCombatRegenHealthDelta { get { return GetDouble(12); } }
        public float NonCombatRegenArmorMod { get { return (float)(RawNonCombatRegenArmorMod.HasValue ? RawNonCombatRegenArmorMod.Value : 0); } }
        public double? RawNonCombatRegenArmorMod { get { return GetDouble(16); } }
        public float NonCombatRegenArmordelta { get { return (float)(RawNonCombatRegenArmordelta.HasValue ? RawNonCombatRegenArmordelta.Value : 0); } }
        public double? RawNonCombatRegenArmordelta { get { return GetDouble(20); } }
        public float CombatRegenArmorMod { get { return (float)(RawCombatRegenArmorMod.HasValue ? RawCombatRegenArmorMod.Value : 0); } }
        public double? RawCombatRegenArmorMod { get { return GetDouble(24); } }
        public float NonCombatRegenPowerMod { get { return (float)(RawNonCombatRegenPowerMod.HasValue ? RawNonCombatRegenPowerMod.Value : 0); } }
        public double? RawNonCombatRegenPowerMod { get { return GetDouble(28); } }
        public float CombatRegenPowerMod { get { return (float)(RawCombatRegenPowerMod.HasValue ? RawCombatRegenPowerMod.Value : 0); } }
        public double? RawCombatRegenPowerMod { get { return GetDouble(32); } }
        public float NonCombatRegenRageMod { get { return (float)(RawNonCombatRegenRageMod.HasValue ? RawNonCombatRegenRageMod.Value : 0); } }
        public double? RawNonCombatRegenRageMod { get { return GetDouble(36); } }
        public float CombatRegenRageMod { get { return (float)(RawCombatRegenRageMod.HasValue ? RawCombatRegenRageMod.Value : 0); } }
        public double? RawCombatRegenRageMod { get { return GetDouble(40); } }
        public float MentalDefenseRating { get { return (float)(RawMentalDefenseRating.HasValue ? RawMentalDefenseRating.Value : 0); } }
        public double? RawMentalDefenseRating { get { return GetDouble(44); } }
        public float SprintBoost { get { return (float)(RawSprintBoost.HasValue ? RawSprintBoost.Value : 0); } }
        public double? RawSprintBoost { get { return GetDouble(48); } }
        public float TauntMod { get { return (float)(RawTauntMod.HasValue ? RawTauntMod.Value : 0); } }
        public double? RawTauntMod { get { return GetDouble(52); } }
        public float IgnoreChanceFear { get { return (float)(RawIgnoreChanceFear.HasValue ? RawIgnoreChanceFear.Value : 0); } }
        public double? RawIgnoreChanceFear { get { return GetDouble(56); } }
        public float IgnoreChanceMezz { get { return (float)(RawIgnoreChanceMezz.HasValue ? RawIgnoreChanceMezz.Value : 0); } }
        public double? RawIgnoreChanceMezz { get { return GetDouble(60); } }
        public float IgnoreChanceKnockback { get { return (float)(RawIgnoreChanceKnockback.HasValue ? RawIgnoreChanceKnockback.Value : 0); } }
        public double? RawIgnoreChanceKnockback { get { return GetDouble(64); } }
        public float EvasionChance { get { return (float)(RawEvasionChance.HasValue ? RawEvasionChance.Value : 0); } }
        public double? RawEvasionChance { get { return GetDouble(68); } }
        public float LootBoostChanceUncommon { get { return (float)(RawLootBoostChanceUncommon.HasValue ? RawLootBoostChanceUncommon.Value : 0); } }
        public double? RawLootBoostChanceUncommon { get { return GetDouble(72); } }
        public float LootBoostChanceRare { get { return (float)(RawLootBoostChanceRare.HasValue ? RawLootBoostChanceRare.Value : 0); } }
        public double? RawLootBoostChanceRare { get { return GetDouble(76); } }
        public float LootBoostChanceExceptional { get { return (float)(RawLootBoostChanceExceptional.HasValue ? RawLootBoostChanceExceptional.Value : 0); } }
        public double? RawLootBoostChanceExceptional { get { return GetDouble(80); } }
        public float LootBoostChanceEpic { get { return (float)(RawLootBoostChanceEpic.HasValue ? RawLootBoostChanceEpic.Value : 0); } }
        public double? RawLootBoostChanceEpic { get { return GetDouble(84); } }
        public float LootBoostChanceLegendary { get { return (float)(RawLootBoostChanceLegendary.HasValue ? RawLootBoostChanceLegendary.Value : 0); } }
        public double? RawLootBoostChanceLegendary { get { return GetDouble(88); } }
        public float MaxHealth { get { return (float)(RawMaxHealth.HasValue ? RawMaxHealth.Value : 0); } }
        public double? RawMaxHealth { get { return GetDouble(92); } }
        public float MaxArmor { get { return (float)(RawMaxArmor.HasValue ? RawMaxArmor.Value : 0); } }
        public double? RawMaxArmor { get { return GetDouble(96); } }
        public float MaxRage { get { return (float)(RawMaxRage.HasValue ? RawMaxRage.Value : 0); } }
        public double? RawMaxRage { get { return GetDouble(100); } }
        public float MaxPower { get { return (float)(RawMaxPower.HasValue ? RawMaxPower.Value : 0); } }
        public double? RawMaxPower { get { return GetDouble(104); } }
        public float MaxBreath { get { return (float)(RawMaxBreath.HasValue ? RawMaxBreath.Value : 0); } }
        public double? RawMaxBreath { get { return GetDouble(108); } }
        public float BoostUniversalDirect { get { return (float)(RawBoostUniversalDirect.HasValue ? RawBoostUniversalDirect.Value : 0); } }
        public double? RawBoostUniversalDirect { get { return GetDouble(112); } }
        public float BoostAbilityRageAttack { get { return (float)(RawBoostAbilityRageAttack.HasValue ? RawBoostAbilityRageAttack.Value : 0); } }
        public double? RawBoostAbilityRageAttack { get { return GetDouble(116); } }
        public float ModAbilityRageAttack { get { return (float)(RawModAbilityRageAttack.HasValue ? RawModAbilityRageAttack.Value : 0); } }
        public double? RawModAbilityRageAttack { get { return GetDouble(120); } }
        public float MonsterCombatXpValue { get { return (float)(RawMonsterCombatXpValue.HasValue ? RawMonsterCombatXpValue.Value : 0); } }
        public double? RawMonsterCombatXpValue { get { return GetDouble(124); } }
        public float CombatRegenArmorDelta { get { return (float)(RawCombatRegenArmorDelta.HasValue ? RawCombatRegenArmorDelta.Value : 0); } }
        public double? RawCombatRegenArmorDelta { get { return GetDouble(128); } }
        public float CombatRegenDelta { get { return (float)(RawCombatRegenDelta.HasValue ? RawCombatRegenDelta.Value : 0); } }
        public double? RawCombatRegenDelta { get { return GetDouble(132); } }
        public float MaxInventorySize { get { return (float)(RawMaxInventorySize.HasValue ? RawMaxInventorySize.Value : 0); } }
        public double? RawMaxInventorySize { get { return GetDouble(136); } }
        public float MaxMetabolism { get { return (float)(RawMaxMetabolism.HasValue ? RawMaxMetabolism.Value : 0); } }
        public double? RawMaxMetabolism { get { return GetDouble(140); } }
        public float NpcModFavorFromGifts { get { return (float)(RawNpcModFavorFromGifts.HasValue ? RawNpcModFavorFromGifts.Value : 0); } }
        public double? RawNpcModFavorFromGifts { get { return GetDouble(144); } }
        public float NpcModFavorFromHangouts { get { return (float)(RawNpcModFavorFromHangouts.HasValue ? RawNpcModFavorFromHangouts.Value : 0); } }
        public double? RawNpcModFavorFromHangouts { get { return GetDouble(148); } }
        public float NpcModMaxSalesValue { get { return (float)(RawNpcModMaxSalesValue.HasValue ? RawNpcModMaxSalesValue.Value : 0); } }
        public double? RawNpcModMaxSalesValue { get { return GetDouble(152); } }
        public float NpcModTrainingCost { get { return (float)(RawNpcModTrainingCost.HasValue ? RawNpcModTrainingCost.Value : 0); } }
        public double? RawNpcModTrainingCost { get { return GetDouble(156); } }
        public int NumInventoryFolders { get { return RawNumInventoryFolders.HasValue ? RawNumInventoryFolders.Value : 0; } }
        public int? RawNumInventoryFolders { get { return GetInt(160); } }
        public float HighCleanlinessXpEarnedMod { get { return (float)(RawHighCleanlinessXpEarnedMod.HasValue ? RawHighCleanlinessXpEarnedMod.Value : 0); } }
        public double? RawHighCleanlinessXpEarnedMod { get { return GetDouble(164); } }
        public float LowCleanlinessXpEarnedMod { get { return (float)(RawLowCleanlinessXpEarnedMod.HasValue ? RawLowCleanlinessXpEarnedMod.Value : 0); } }
        public double? RawLowCleanlinessXpEarnedMod { get { return GetDouble(168); } }
        public float MaxArmorMitigationRatio { get { return (float)(RawMaxArmorMitigationRatio.HasValue ? RawMaxArmorMitigationRatio.Value : 0); } }
        public double? RawMaxArmorMitigationRatio { get { return GetDouble(172); } }
        public float ShowCleanlinessIndicators { get { return (float)(RawShowCleanlinessIndicators.HasValue ? RawShowCleanlinessIndicators.Value : 0); } }
        public double? RawShowCleanlinessIndicators { get { return GetDouble(176); } }
        public float HighCommunityXpEarnedMod { get { return (float)(RawHighCommunityXpEarnedMod.HasValue ? RawHighCommunityXpEarnedMod.Value : 0); } }
        public double? RawHighCommunityXpEarnedMod { get { return GetDouble(180); } }
        public float LowCommunityXpEarnedMod { get { return (float)(RawLowCommunityXpEarnedMod.HasValue ? RawLowCommunityXpEarnedMod.Value : 0); } }
        public double? RawLowCommunityXpEarnedMod { get { return GetDouble(184); } }
        public float ShowCommunityIndicators { get { return (float)(RawShowCommunityIndicators.HasValue ? RawShowCommunityIndicators.Value : 0); } }
        public double? RawShowCommunityIndicators { get { return GetDouble(188); } }
        public float HighPeaceblenessXpEarnedMod { get { return (float)(RawHighPeaceblenessXpEarnedMod.HasValue ? RawHighPeaceblenessXpEarnedMod.Value : 0); } }
        public double? RawHighPeaceblenessXpEarnedMod { get { return GetDouble(192); } }
        public float LowPeaceblenessXpEarnedMod { get { return (float)(RawLowPeaceblenessXpEarnedMod.HasValue ? RawLowPeaceblenessXpEarnedMod.Value : 0); } }
        public double? RawLowPeaceblenessXpEarnedMod { get { return GetDouble(196); } }
        public float ShowPeaceblenessIndicators { get { return (float)(RawShowPeaceblenessIndicators.HasValue ? RawShowPeaceblenessIndicators.Value : 0); } }
        public double? RawShowPeaceblenessIndicators { get { return GetDouble(200); } }
        public float StaffArmorAutoHeal { get { return (float)(RawStaffArmorAutoHeal.HasValue ? RawStaffArmorAutoHeal.Value : 0); } }
        public double? RawStaffArmorAutoHeal { get { return GetDouble(204); } }
        public float MaxMapPinsPerArea { get { return (float)(RawMaxMapPinsPerArea.HasValue ? RawMaxMapPinsPerArea.Value : 0); } }
        public double? RawMaxMapPinsPerArea { get { return GetDouble(208); } }
        public float MaxMapPinIcons { get { return (float)(RawMaxMapPinIcons.HasValue ? RawMaxMapPinIcons.Value : 0); } }
        public double? RawMaxMapPinIcons { get { return GetDouble(212); } }
        public float WorkOrderCoinRewardMod { get { return (float)(RawWorkOrderCoinRewardMod.HasValue ? RawWorkOrderCoinRewardMod.Value : 0); } }
        public double? RawWorkOrderCoinRewardMod { get { return GetDouble(216); } }
        public float MaxActiveWorkOrders { get { return (float)(RawMaxActiveWorkOrders.HasValue ? RawMaxActiveWorkOrders.Value : 0); } }
        public double? RawMaxActiveWorkOrders { get { return GetDouble(220); } }
        public float PlayerOrdersMaxActive { get { return (float)(RawPlayerOrdersMaxActive.HasValue ? RawPlayerOrdersMaxActive.Value : 0); } }
        public double? RawPlayerOrdersMaxActive { get { return GetDouble(224); } }
        public float ShopInventorySizeDelta { get { return (float)(RawShopInventorySizeDelta.HasValue ? RawShopInventorySizeDelta.Value : 0); } }
        public double? RawShopInventorySizeDelta { get { return GetDouble(228); } }
        public float MailShopNumFree { get { return (float)(RawMailShopNumFree.HasValue ? RawMailShopNumFree.Value : 0); } }
        public double? RawMailShopNumFree { get { return GetDouble(232); } }
        public float ShopHiringMaxPrepDays { get { return (float)(RawShopHiringMaxPrepDays.HasValue ? RawShopHiringMaxPrepDays.Value : 0); } }
        public double? RawShopHiringMaxPrepDays { get { return GetDouble(236); } }
        public float ShopLogDaysKept { get { return (float)(RawShopLogDaysKept.HasValue ? RawShopLogDaysKept.Value : 0); } }
        public double? RawShopLogDaysKept { get { return GetDouble(240); } }
        public float ShopHiringNumFree { get { return (float)(RawShopHiringNumFree.HasValue ? RawShopHiringNumFree.Value : 0); } }
        public double? RawShopHiringNumFree { get { return GetDouble(244); } }
        public float CriticalHitDamage { get { return (float)(RawCriticalHitDamage.HasValue ? RawCriticalHitDamage.Value : 0); } }
        public double? RawCriticalHitDamage { get { return GetDouble(248); } }
        public float MonsterCritChance { get { return (float)(RawMonsterCritChance.HasValue ? RawMonsterCritChance.Value : 0); } }
        public double? RawMonsterCritChance { get { return GetDouble(252); } }
        public float AccuracyBoost { get { return (float)(RawAccuracyBoost.HasValue ? RawAccuracyBoost.Value : 0); } }
        public double? RawAccuracyBoost { get { return GetDouble(256); } }
        public float BoostPoisonIndirect { get { return (float)(RawBoostPoisonIndirect.HasValue ? RawBoostPoisonIndirect.Value : 0); } }
        public double? RawBoostPoisonIndirect { get { return GetDouble(260); } }
        public float EvasionChanceProjectile { get { return (float)(RawEvasionChanceProjectile.HasValue ? RawEvasionChanceProjectile.Value : 0); } }
        public double? RawEvasionChanceProjectile { get { return GetDouble(264); } }
        public float EvasionChanceMelee { get { return (float)(RawEvasionChanceMelee.HasValue ? RawEvasionChanceMelee.Value : 0); } }
        public double? RawEvasionChanceMelee { get { return GetDouble(268); } }
        public float ModCriticalHitDamageRageAttack { get { return (float)(RawModCriticalHitDamageRageAttack.HasValue ? RawModCriticalHitDamageRageAttack.Value : 0); } }
        public double? RawModCriticalHitDamageRageAttack { get { return GetDouble(272); } }
        public float BoostWerewolfMetabolismHeathRegen { get { return (float)(RawBoostWerewolfMetabolismHeathRegen.HasValue ? RawBoostWerewolfMetabolismHeathRegen.Value : 0); } }
        public double? RawBoostWerewolfMetabolismHeathRegen { get { return GetDouble(276); } }
        public float BoostWerewolfMetabolismPowerRegen { get { return (float)(RawBoostWerewolfMetabolismPowerRegen.HasValue ? RawBoostWerewolfMetabolismPowerRegen.Value : 0); } }
        public double? RawBoostWerewolfMetabolismPowerRegen { get { return GetDouble(280); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(284, ref _FieldTableOrder); } }  private List<string> _FieldTableOrder;
        public List<int> VulnerabilityDataList { get { return GetIntList(288, ref _VulnerabilityDataList); } } private List<int> _VulnerabilityDataList;
        public List<int> MitigationDataList { get { return GetIntList(292, ref _MitigationDataList); } } private List<int> _MitigationDataList;
        public List<int> DirectModDataList { get { return GetIntList(296, ref _DirectModDataList); } } private List<int> _DirectModDataList;
        public List<int> IndirectModDataList { get { return GetIntList(300, ref _IndirectModDataList); } } private List<int> _IndirectModDataList;
        public int MonsterMatchOwnerSpeed { get { return RawMonsterMatchOwnerSpeed.HasValue ? RawMonsterMatchOwnerSpeed.Value : 0; } }
        public int? RawMonsterMatchOwnerSpeed { get { return GetInt(304); } }
        public float ArmorMitigationMod { get { return (float)(RawArmorMitigationMod.HasValue ? RawArmorMitigationMod.Value : 0); } }
        public double? RawArmorMitigationMod { get { return GetInt(308); } }
        public float AutoHealHealthMod { get { return (float)(RawAutoHealHealthMod.HasValue ? RawAutoHealHealthMod.Value : 0); } }
        public double? RawAutoHealHealthMod { get { return GetInt(312); } }
        public float AutoHealArmorMod { get { return (float)(RawAutoHealArmorMod.HasValue ? RawAutoHealArmorMod.Value : 0); } }
        public double? RawAutoHealArmorMod { get { return GetInt(316); } }
        public float ArmorMitigationRatio { get { return (float)(RawArmorMitigationRatio.HasValue ? RawArmorMitigationRatio.Value : 0); } }
        public double? RawArmorMitigationRatio { get { return GetInt(320); } }
        public int ShowFairyEnergyIndicator { get { return RawShowFairyEnergyIndicator.HasValue ? RawShowFairyEnergyIndicator.Value : 0; } }
        public int? RawShowFairyEnergyIndicator { get { return GetInt(324); } }
        public int BoostAbilityPetSpecialAttack { get { return RawBoostAbilityPetSpecialAttack.HasValue ? RawBoostAbilityPetSpecialAttack.Value : 0; } }
        public int? RawBoostAbilityPetSpecialAttack { get { return GetInt(328); } }
        public int BoostAbilityPetSpecialTrick { get { return RawBoostAbilityPetSpecialTrick.HasValue ? RawBoostAbilityPetSpecialTrick.Value : 0; } }
        public int? RawBoostAbilityPetSpecialTrick { get { return GetInt(332); } }
        public int BoostAbilityPetBasicAttack { get { return RawBoostAbilityPetBasicAttack.HasValue ? RawBoostAbilityPetBasicAttack.Value : 0; } }
        public int? RawBoostAbilityPetBasicAttack { get { return GetInt(336); } }
        public float BoostAutoHealHealthSender { get { return (float)(RawBoostAutoHealHealthSender.HasValue ? RawBoostAutoHealHealthSender.Value : 0); } }
        public double? RawBoostAutoHealHealthSender { get { return GetInt(340); } }
        public float BoostAutoHealArmorSender { get { return (float)(RawBoostAutoHealArmorSender.HasValue ? RawBoostAutoHealArmorSender.Value : 0); } }
        public double? RawBoostAutoHealArmorSender { get { return GetInt(344); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "IGNORE_CHANCE_FEAR", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawIgnoreChanceFear } },
            { "IGNORE_CHANCE_MEZ", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawIgnoreChanceMezz } },
            { "IGNORE_CHANCE_KNOCKBACK", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawIgnoreChanceKnockback } },
            { "MENTAL_DEFENSE_RATING", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMentalDefenseRating } },
            { "NONCOMBAT_REGEN_HEALTH_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawNonCombatRegenHealthMod } },
            { "COMBAT_REGEN_HEALTH_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawCombatRegenHealthMod } },
            { "COMBAT_REGEN_HEALTH_DELTA", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawCombatRegenHealthDelta } },
            { "NONCOMBAT_REGEN_ARMOR_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawNonCombatRegenArmorMod } },
            { "NONCOMBAT_REGEN_ARMOR_DELTA", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawNonCombatRegenArmordelta } },
            { "COMBAT_REGEN_ARMOR_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawCombatRegenArmorMod } },
            { "NONCOMBAT_REGEN_POWER_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawNonCombatRegenPowerMod } },
            { "COMBAT_REGEN_POWER_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawCombatRegenPowerMod } },
            { "NONCOMBAT_REGEN_RAGE_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawNonCombatRegenRageMod } },
            { "COMBAT_REGEN_RAGE_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawCombatRegenRageMod } },
            { "SPRINT_BOOST", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawSprintBoost } },
            { "TAUNT_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawTauntMod } },
            { "EVASION_CHANCE", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawEvasionChance } },
            { "EVASION_CHANCE_PROJECTILE", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawEvasionChanceProjectile } },
            { "EVASION_CHANCE_MELEE", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawEvasionChanceMelee } },
            { "MOD_CRITICAL_HIT_DAMAGE_RAGEATTACK", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawModCriticalHitDamageRageAttack } },
            { "BOOST_WEREWOLFMETABOLISM_HEALTHREGEN", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawBoostWerewolfMetabolismHeathRegen } },
            { "BOOST_WEREWOLFMETABOLISM_POWERREGEN", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawBoostWerewolfMetabolismPowerRegen } },
            { "LOOT_BOOST_CHANCE_UNCOMMON", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawLootBoostChanceUncommon } },
            { "LOOT_BOOST_CHANCE_RARE", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawLootBoostChanceRare } },
            { "LOOT_BOOST_CHANCE_EXCEPTIONAL", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawLootBoostChanceExceptional } },
            { "LOOT_BOOST_CHANCE_EPIC", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawLootBoostChanceEpic } },
            { "LOOT_BOOST_CHANCE_LEGENDARY", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawLootBoostChanceLegendary } },
            { "MAX_HEALTH", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMaxHealth } },
            { "MAX_ARMOR", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMaxArmor } },
            { "MAX_RAGE", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMaxRage } },
            { "MAX_POWER", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMaxPower } },
            { "MAX_BREATH", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMaxBreath } },
            { "BOOST_UNIVERSAL_DIRECT", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawBoostUniversalDirect } },
            { "BOOST_ABILITY_RAGEATTACK", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawBoostAbilityRageAttack } },
            { "MOD_ABILITY_RAGEATTACK", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawModAbilityRageAttack } },
            { "MONSTER_COMBAT_XP_VALUE", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMonsterCombatXpValue } },
            { "COMBAT_REGEN_ARMOR_DELTA", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawCombatRegenArmorDelta } },
            { "COMBAT_REGEN_POWER_DELTA", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawCombatRegenDelta } },
            { "MAX_INVENTORY_SIZE", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMaxInventorySize } },
            { "MAX_METABOLISM", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMaxMetabolism } },
            { "NPC_MOD_FAVORFROMGIFTS", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawNpcModFavorFromGifts } },
            { "NPC_MOD_FAVORFROMHANGOUTS", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawNpcModFavorFromHangouts } },
            { "NPC_MOD_MAXSALESVALUE", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawNpcModMaxSalesValue } },
            { "NPC_MOD_TRAININGCOST", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawNpcModTrainingCost } },
            { "NUM_INVENTORY_FOLDERS", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumInventoryFolders } },
            { "HIGH_CLEANLINESS_XP_EARNED_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawHighCleanlinessXpEarnedMod } },
            { "LOW_CLEANLINESS_XP_EARNED_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawLowCleanlinessXpEarnedMod } },
            { "MAX_ARMOR_MITIGATION_RATIO", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMaxArmorMitigationRatio } },
            { "SHOW_CLEANLINESS_INDICATORS", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawShowCleanlinessIndicators } },
            { "HIGH_COMMUNITY_XP_EARNED_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawHighCommunityXpEarnedMod } },
            { "LOW_COMMUNITY_XP_EARNED_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawLowCommunityXpEarnedMod } },
            { "SHOW_COMMUNITY_INDICATORS", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawShowCommunityIndicators } },
            { "HIGH_PEACEABLENESS_XP_EARNED_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawHighPeaceblenessXpEarnedMod } },
            { "LOW_PEACEABLENESS_XP_EARNED_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawLowPeaceblenessXpEarnedMod } },
            { "SHOW_PEACEABLENESS_INDICATORS", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawShowPeaceblenessIndicators } },
            { "STAFF_ARMOR_AUTOHEAL", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawStaffArmorAutoHeal } },
            { "MAX_MAP_PINS_PER_AREA", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMaxMapPinsPerArea } },
            { "MAX_MAP_PIN_ICONS", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMaxMapPinIcons } },
            { "WORKORDER_COIN_REWARD_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawWorkOrderCoinRewardMod } },
            { "MAX_ACTIVE_WORKORDERS", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMaxActiveWorkOrders } },
            { "PLAYER_ORDERS_MAX_ACTIVE", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawPlayerOrdersMaxActive } },
            { "SHOP_INVENTORY_SIZE_DELTA", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawShopInventorySizeDelta } },
            { "MAIL_SHOP_NUMFREE", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMailShopNumFree } },
            { "SHOP_HIRING_MAX_PREPAY_DAYS", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawShopHiringMaxPrepDays } },
            { "SHOP_LOG_DAYSKEPT", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawShopLogDaysKept } },
            { "SHOP_HIRING_NUMFREE", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawShopHiringNumFree } },
            { "MOD_CRITICAL_HIT_DAMAGE", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawCriticalHitDamage } },
            { "MONSTER_MATCH_OWNER_SPEED", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawMonsterMatchOwnerSpeed } },
            { "ARMOR_MITIGATION_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawArmorMitigationMod } },
            { "AUTOHEAL_HEALTH_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawAutoHealHealthMod } },
            { "AUTOHEAL_ARMOR_MOD", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawAutoHealArmorMod } },
            { "ARMOR_MITIGATION_RATIO", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawArmorMitigationRatio } },
            { "SHOW_FAIRYENERGY_INDICATORS", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawShowFairyEnergyIndicator } },
            { "BOOST_ABILITY_PET_SPECIALATTACK", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawBoostAbilityPetSpecialAttack } },
            { "BOOST_ABILITY_PET_SPECIALTRICK", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawBoostAbilityPetSpecialTrick } },
            { "BOOST_ABILITY_PET_BASICATTACK", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawBoostAbilityPetBasicAttack } },
            { "BOOST_AUTOHEAL_HEALTH_SENDER", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawBoostAutoHealHealthSender } },
            { "BOOST_AUTOHEAL_ARMOR_SENDER", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawBoostAutoHealArmorSender } },
        }; } }

        #region Json Reconstruction
        public const int DamageMultiplier = 10000;

        public override void ListObjectContent(JsonGenerator generator, string ParserKey)
        {
            string RawDamageType;
            DamageType ParsedDamageType;

            if (IsDamageTypeEntry(ParserKey, "VULN_", null, out RawDamageType, out ParsedDamageType, null))
            {
                for (int i = 0; i * 2 < VulnerabilityDataList.Count; i++)
                    if ((DamageType)VulnerabilityDataList[i * 2] == ParsedDamageType)
                    {
                        generator.AddDouble("VULN_" + RawDamageType.ToUpper(), (double)VulnerabilityDataList[i * 2 + 1] / DamageMultiplier);
                        break;
                    }
                return;
            }

            else if (IsDamageTypeEntry(ParserKey, "MITIGATION_", null, out RawDamageType, out ParsedDamageType, null))
            {
                for (int i = 0; i * 2 < MitigationDataList.Count; i++)
                    if ((DamageType)MitigationDataList[i * 2] == ParsedDamageType)
                    {
                        generator.AddDouble("MITIGATION_" + RawDamageType.ToUpper(), (double)MitigationDataList[i * 2 + 1] / DamageMultiplier);
                        break;
                    }
                return;
            }

            else if (IsDamageTypeEntry(ParserKey, "MOD_", "_INDIRECT", out RawDamageType, out ParsedDamageType, null))
            {
                for (int i = 0; i * 2 < IndirectModDataList.Count; i++)
                    if ((DamageType)IndirectModDataList[i * 2] == ParsedDamageType)
                    {
                        generator.AddDouble("MOD_" + RawDamageType.ToUpper() + "_INDIRECT", (double)IndirectModDataList[i * 2 + 1] / DamageMultiplier);
                        break;
                    }
                return;
            }

            else if (IsDamageTypeEntry(ParserKey, "MOD_", "_DIRECT", out RawDamageType, out ParsedDamageType, null))
            {
                for (int i = 0; i * 2 < DirectModDataList.Count; i++)
                    if ((DamageType)DirectModDataList[i * 2] == ParsedDamageType)
                    {
                        generator.AddDouble("MOD_" + RawDamageType.ToUpper() + "_DIRECT", (double)DirectModDataList[i * 2 + 1] / DamageMultiplier);
                        break;
                    }
                return;
            }

            else
                base.ListObjectContent(generator, ParserKey);
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
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion

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

        public List<string> VulnerabilityList { get { return GetDamageList(VulnerabilityDataList, PgAdvancement.GetVulnerabilityText); } }
        public List<string> MitigationList { get { return GetDamageList(MitigationDataList, PgAdvancement.GetMitigationText); } }
        public List<string> DirectModList { get { return GetDamageList(DirectModDataList, PgAdvancement.GetDirectModText); } }
        public List<string> IndirectModList { get { return GetDamageList(IndirectModDataList, PgAdvancement.GetIndirectModText); } }

        public static List<string> GetDamageList(List<int> dataList, Func<DamageType, float, string> getText)
        {
            List<string> Result = new List<string>();
            for (int i = 0; i * 2 < dataList.Count; i++)
            {
                DamageType DamageType = (DamageType)dataList[i * 2 + 0];
                float Value = (float)((double)dataList[i * 2 + 1] / DamageMultiplier);

                Result.Add(getText(DamageType, Value));
            }

            return Result;
        }

        public static string GetVulnerabilityText(DamageType damageType, float value)
        {
            string DamageTypeString = TextMaps.DamageTypeTextMap[damageType];

            if (value == -1)
                return "Immune to " + DamageTypeString;
            else
            {
                string ValueString = InvariantCulture.SingleToString(value * 100);
                if (value > 0)
                    ValueString = "+" + ValueString;

                string Suffix;

                if (value == 0.25F)
                    Suffix = " (Effective)";
                else if (value == 0.5F)
                    Suffix = " (Very Effective)";
                else if (value == -0.25F)
                    Suffix = " (Ineffective)";
                else if (value == -0.5F)
                    Suffix = " (Very Ineffective)";
                else
                    Suffix = "";

                return "Vulnerability to " + DamageTypeString + ": " + ValueString + "%" + Suffix;
            }
        }

        public static string GetMitigationText(DamageType damageType, float value)
        {
            string DamageTypeString = TextMaps.DamageTypeTextMap[damageType];
            string ValueString = InvariantCulture.SingleToString(value);
            if (value > 0)
                ValueString = "+" + ValueString;

            return DamageTypeString + " Mitigation: " + ValueString;
        }

        public static string GetDirectModText(DamageType damageType, float value)
        {
            string DamageTypeString = TextMaps.DamageTypeTextMap[damageType];
            string ValueString = InvariantCulture.SingleToString(value);
            if (value > 0)
                ValueString = "+" + ValueString;

            return DamageTypeString + " Direct Damage: " + ValueString + "%";
        }

        public static string GetIndirectModText(DamageType damageType, float value)
        {
            string DamageTypeString = TextMaps.DamageTypeTextMap[damageType];
            string ValueString = InvariantCulture.SingleToString(value);
            if (value > 0)
                ValueString = "+" + ValueString;

            return DamageTypeString + " Indirect Damage: " + ValueString + "%";
        }
    }
}
