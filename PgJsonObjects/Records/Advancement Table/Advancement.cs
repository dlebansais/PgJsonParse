﻿using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Advancement : GenericJsonObject<Advancement>
    {
        #region Direct Properties
        public Dictionary<DamageType, double> VulnerabilityTable { get; } = new Dictionary<DamageType, double>();
        public Dictionary<DamageType, double> MitigationTable { get; } = new Dictionary<DamageType, double>();
        public Dictionary<DamageType, double> DirectModTable { get; } = new Dictionary<DamageType, double>();
        public Dictionary<DamageType, double> IndirectModTable { get; } = new Dictionary<DamageType, double>();
        public double NonCombatRegenHealthMod { get { return RawNonCombatRegenHealthMod.HasValue ? RawNonCombatRegenHealthMod.Value : 0; } }
        private double? RawNonCombatRegenHealthMod;
        public double CombatRegenHealthMod { get { return RawCombatRegenHealthMod.HasValue ? RawCombatRegenHealthMod.Value : 0; } }
        private double? RawCombatRegenHealthMod;
        public double CombatRegenHealthDelta { get { return RawCombatRegenHealthDelta.HasValue ? RawCombatRegenHealthDelta.Value : 0; } }
        private double? RawCombatRegenHealthDelta;
        public double NonCombatRegenArmorMod { get { return RawNonCombatRegenArmorMod.HasValue ? RawNonCombatRegenArmorMod.Value : 0; } }
        private double? RawNonCombatRegenArmorMod;
        public double NonCombatRegenArmordelta { get { return RawNonCombatRegenArmordelta.HasValue ? RawNonCombatRegenArmordelta.Value : 0; } }
        private double? RawNonCombatRegenArmordelta;
        public double CombatRegenArmorMod { get { return RawCombatRegenArmorMod.HasValue ? RawCombatRegenArmorMod.Value : 0; } }
        private double? RawCombatRegenArmorMod;
        public double NonCombatRegenPowerMod { get { return RawNonCombatRegenPowerMod.HasValue ? RawNonCombatRegenPowerMod.Value : 0; } }
        private double? RawNonCombatRegenPowerMod;
        public double CombatRegenPowerMod { get { return RawCombatRegenPowerMod.HasValue ? RawCombatRegenPowerMod.Value : 0; } }
        private double? RawCombatRegenPowerMod;
        public double NonCombatRegenRageMod { get { return RawNonCombatRegenRageMod.HasValue ? RawNonCombatRegenRageMod.Value : 0; } }
        private double? RawNonCombatRegenRageMod;
        public double CombatRegenRageMod { get { return RawCombatRegenRageMod.HasValue ? RawCombatRegenRageMod.Value : 0; } }
        private double? RawCombatRegenRageMod;
        public double MentalDefenseRating { get { return RawMentalDefenseRating.HasValue ? RawMentalDefenseRating.Value : 0; } }
        private double? RawMentalDefenseRating;
        public double SprintBoost { get { return RawSprintBoost.HasValue ? RawSprintBoost.Value : 0; } }
        private double? RawSprintBoost;
        public double TauntMod { get { return RawTauntMod.HasValue ? RawTauntMod.Value : 0; } }
        private double? RawTauntMod;
        public double IgnoreChanceFear { get { return RawIgnoreChanceFear.HasValue ? RawIgnoreChanceFear.Value : 0; } }
        private double? RawIgnoreChanceFear;
        public double IgnoreChanceMezz { get { return RawIgnoreChanceMezz.HasValue ? RawIgnoreChanceMezz.Value : 0; } }
        private double? RawIgnoreChanceMezz;
        public double IgnoreChanceKnockback { get { return RawIgnoreChanceKnockback.HasValue ? RawIgnoreChanceKnockback.Value : 0; } }
        private double? RawIgnoreChanceKnockback;
        public double EvasionChance { get { return RawEvasionChance.HasValue ? RawEvasionChance.Value : 0; } }
        private double? RawEvasionChance;
        public double LootBoostChanceUncommon { get { return RawLootBoostChanceUncommon.HasValue ? RawLootBoostChanceUncommon.Value : 0; } }
        private double? RawLootBoostChanceUncommon;
        public double LootBoostChanceRare { get { return RawLootBoostChanceRare.HasValue ? RawLootBoostChanceRare.Value : 0; } }
        private double? RawLootBoostChanceRare;
        public double LootBoostChanceExceptional { get { return RawLootBoostChanceExceptional.HasValue ? RawLootBoostChanceExceptional.Value : 0; } }
        private double? RawLootBoostChanceExceptional;
        public double LootBoostChanceEpic { get { return RawLootBoostChanceEpic.HasValue ? RawLootBoostChanceEpic.Value : 0; } }
        private double? RawLootBoostChanceEpic;
        public double LootBoostChanceLegendary { get { return RawLootBoostChanceLegendary.HasValue ? RawLootBoostChanceLegendary.Value : 0; } }
        private double? RawLootBoostChanceLegendary;
        public double MaxHealth { get { return RawMaxHealth.HasValue ? RawMaxHealth.Value : 0; } }
        private double? RawMaxHealth;
        public double MaxArmor { get { return RawMaxArmor.HasValue ? RawMaxArmor.Value : 0; } }
        private double? RawMaxArmor;
        public double MaxRage { get { return RawMaxRage.HasValue ? RawMaxRage.Value : 0; } }
        private double? RawMaxRage;
        public double MaxPower { get { return RawMaxPower.HasValue ? RawMaxPower.Value : 0; } }
        private double? RawMaxPower;
        public double MaxBreath { get { return RawMaxBreath.HasValue ? RawMaxBreath.Value : 0; } }
        private double? RawMaxBreath;
        public double BoostUniversalDirect { get { return RawBoostUniversalDirect.HasValue ? RawBoostUniversalDirect.Value : 0; } }
        private double? RawBoostUniversalDirect;
        public double BoostAbilityRageAttack { get { return RawBoostAbilityRageAttack.HasValue ? RawBoostAbilityRageAttack.Value : 0; } }
        private double? RawBoostAbilityRageAttack;
        public double ModAbilityRageAttack { get { return RawModAbilityRageAttack.HasValue ? RawModAbilityRageAttack.Value : 0; } }
        private double? RawModAbilityRageAttack;
        public double MonsterCombatXpValue { get { return RawMonsterCombatXpValue.HasValue ? RawMonsterCombatXpValue.Value : 0; } }
        private double? RawMonsterCombatXpValue;
        public double CombatRegenArmorDelta { get { return RawCombatRegenArmorDelta.HasValue ? RawCombatRegenArmorDelta.Value : 0; } }
        private double? RawCombatRegenArmorDelta;
        public double CombatRegenDelta { get { return RawCombatRegenDelta.HasValue ? RawCombatRegenDelta.Value : 0; } }
        private double? RawCombatRegenDelta;
        public double CombatRegenMod { get { return RawCombatRegenMod.HasValue ? RawCombatRegenMod.Value : 0; } }
        private double? RawCombatRegenMod;
        public double MaxInventorySize { get { return RawMaxInventorySize.HasValue ? RawMaxInventorySize.Value : 0; } }
        private double? RawMaxInventorySize;
        public double MaxMetabolism { get { return RawMaxMetabolism.HasValue ? RawMaxMetabolism.Value : 0; } }
        private double? RawMaxMetabolism;
        public double NpcModFavorFromGifts { get { return RawNpcModFavorFromGifts.HasValue ? RawNpcModFavorFromGifts.Value : 0; } }
        private double? RawNpcModFavorFromGifts;
        public double NpcModFavorFromHangouts { get { return RawNpcModFavorFromHangouts.HasValue ? RawNpcModFavorFromHangouts.Value : 0; } }
        private double? RawNpcModFavorFromHangouts;
        public double NpcModMaxSalesValue { get { return RawNpcModMaxSalesValue.HasValue ? RawNpcModMaxSalesValue.Value : 0; } }
        private double? RawNpcModMaxSalesValue;
        public double NpcModTrainingCost { get { return RawNpcModTrainingCost.HasValue ? RawNpcModTrainingCost.Value : 0; } }
        private double? RawNpcModTrainingCost;
        public int NumInventoryFolders { get { return RawNumInventoryFolders.HasValue ? RawNumInventoryFolders.Value : 0; } }
        private int? RawNumInventoryFolders;
        public double HighCleanlinessXpEarnedMod { get { return RawHighCleanlinessXpEarnedMod.HasValue ? RawHighCleanlinessXpEarnedMod.Value : 0; } }
        private double? RawHighCleanlinessXpEarnedMod;
        public double LowCleanlinessXpEarnedMod { get { return RawLowCleanlinessXpEarnedMod.HasValue ? RawLowCleanlinessXpEarnedMod.Value : 0; } }
        private double? RawLowCleanlinessXpEarnedMod;
        public double MaxArmorMitigationRatio { get { return RawMaxArmorMitigationRatio.HasValue ? RawMaxArmorMitigationRatio.Value : 0; } }
        private double? RawMaxArmorMitigationRatio;
        public double ShowCleanlinessIndicators { get { return RawShowCleanlinessIndicators.HasValue ? RawShowCleanlinessIndicators.Value : 0; } }
        private double? RawShowCleanlinessIndicators;
        public double HighCommunityXpEarnedMod { get { return RawHighCommunityXpEarnedMod.HasValue ? RawHighCommunityXpEarnedMod.Value : 0; } }
        private double? RawHighCommunityXpEarnedMod;
        public double LowCommunityXpEarnedMod { get { return RawLowCommunityXpEarnedMod.HasValue ? RawLowCommunityXpEarnedMod.Value : 0; } }
        private double? RawLowCommunityXpEarnedMod;
        public double ShowCommunityIndicators { get { return RawShowCommunityIndicators.HasValue ? RawShowCommunityIndicators.Value : 0; } }
        private double? RawShowCommunityIndicators;
        public double HighPeaceblenessXpEarnedMod { get { return RawHighPeaceblenessXpEarnedMod.HasValue ? RawHighPeaceblenessXpEarnedMod.Value : 0; } }
        private double? RawHighPeaceblenessXpEarnedMod;
        public double LowPeaceblenessXpEarnedMod { get { return RawLowPeaceblenessXpEarnedMod.HasValue ? RawLowPeaceblenessXpEarnedMod.Value : 0; } }
        private double? RawLowPeaceblenessXpEarnedMod;
        public double ShowPeaceblenessIndicators { get { return RawShowPeaceblenessIndicators.HasValue ? RawShowPeaceblenessIndicators.Value : 0; } }
        private double? RawShowPeaceblenessIndicators;
        public double StaffArmorAutoHeal { get { return RawStaffArmorAutoHeal.HasValue ? RawStaffArmorAutoHeal.Value : 0; } }
        private double? RawStaffArmorAutoHeal;
        public double MaxMapPinsPerArea { get { return RawMaxMapPinsPerArea.HasValue ? RawMaxMapPinsPerArea.Value : 0; } }
        private double? RawMaxMapPinsPerArea;
        public double MaxMapPinIcons { get { return RawMaxMapPinIcons.HasValue ? RawMaxMapPinIcons.Value : 0; } }
        private double? RawMaxMapPinIcons;
        public double WorkOrderCoinRewardMod { get { return RawWorkOrderCoinRewardMod.HasValue ? RawWorkOrderCoinRewardMod.Value : 0; } }
        private double? RawWorkOrderCoinRewardMod;
        public double MaxActiveWorkOrders { get { return RawMaxActiveWorkOrders.HasValue ? RawMaxActiveWorkOrders.Value : 0; } }
        private double? RawMaxActiveWorkOrders;
        public double PlayerOrdersMaxActive { get { return RawPlayerOrdersMaxActive.HasValue ? RawPlayerOrdersMaxActive.Value : 0; } }
        private double? RawPlayerOrdersMaxActive;
        public double ShopInventorySizeDelta { get { return RawShopInventorySizeDelta.HasValue ? RawShopInventorySizeDelta.Value : 0; } }
        private double? RawShopInventorySizeDelta;
        public double MailShopNumFree { get { return RawMailShopNumFree.HasValue ? RawMailShopNumFree.Value : 0; } }
        private double? RawMailShopNumFree;
        public double ShopHiringMaxPrepDays { get { return RawShopHiringMaxPrepDays.HasValue ? RawShopHiringMaxPrepDays.Value : 0; } }
        private double? RawShopHiringMaxPrepDays;
        public double ShopLogDaysKept { get { return RawShopLogDaysKept.HasValue ? RawShopLogDaysKept.Value : 0; } }
        private double? RawShopLogDaysKept;
        public double ShopHiringNumFree { get { return RawShopHiringNumFree.HasValue ? RawShopHiringNumFree.Value : 0; } }
        private double? RawShopHiringNumFree;
        public double CriticalHitDamage { get { return RawCriticalHitDamage.HasValue ? RawCriticalHitDamage.Value : 0; } }
        private double? RawCriticalHitDamage;
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            /*{ "VULNERABILITY", null },
            { "MITIGATION", null },
            { "MOD_INDIRECT", null },
            { "MOD_DIRECT", null },*/
            { "NONCOMBAT_REGEN_HEALTH_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawNonCombatRegenHealthMod = value; }} },
            { "COMBAT_REGEN_HEALTH_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawCombatRegenHealthMod = value; }} },
            { "COMBAT_REGEN_HEALTH_DELTA", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawCombatRegenHealthDelta = value; }} },
            { "NONCOMBAT_REGEN_ARMOR_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawNonCombatRegenArmorMod = value; }} },
            { "NONCOMBAT_REGEN_ARMOR_DELTA", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawNonCombatRegenArmordelta = value; }} },
            { "COMBAT_REGEN_ARMOR_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawCombatRegenArmorMod = value; }} },
            { "NONCOMBAT_REGEN_POWER_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawNonCombatRegenPowerMod = value; }} },
            { "COMBAT_REGEN_POWER_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawCombatRegenPowerMod = value; }} },
            { "NONCOMBAT_REGEN_RAGE_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawNonCombatRegenRageMod = value; }} },
            { "COMBAT_REGEN_RAGE_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawCombatRegenRageMod = value; }} },
            { "MENTAL_DEFENSE_RATING", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawMentalDefenseRating = value; }} },
            { "SPRINT_BOOST", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawSprintBoost = value; }} },
            { "TAUNT_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawTauntMod = value; }} },
            { "IGNORE_CHANCE_FEAR", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawIgnoreChanceFear = value; }} },
            { "IGNORE_CHANCE_MEZ", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawIgnoreChanceMezz = value; }} },
            { "IGNORE_CHANCE_KNOCKBACK", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawIgnoreChanceKnockback = value; }} },
            { "EVASION_CHANCE", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawEvasionChance = value; }} },
            { "LOOT_BOOST_CHANCE_UNCOMMON", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawLootBoostChanceUncommon = value; }} },
            { "LOOT_BOOST_CHANCE_RARE", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawLootBoostChanceRare = value; }} },
            { "LOOT_BOOST_CHANCE_EXCEPTIONAL", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawLootBoostChanceExceptional = value; }} },
            { "LOOT_BOOST_CHANCE_EPIC", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawLootBoostChanceEpic = value; }} },
            { "LOOT_BOOST_CHANCE_LEGENDARY", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawLootBoostChanceLegendary = value; }} },
            { "MAX_HEALTH", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawMaxHealth = value; }} },
            { "MAX_ARMOR", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawMaxArmor = value; }} },
            { "MAX_RAGE", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawMaxRage = value; }} },
            { "MAX_POWER", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawMaxPower = value; }} },
            { "MAX_BREATH", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawMaxBreath = value; }} },
            { "BOOST_UNIVERSAL_DIRECT", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawBoostUniversalDirect = value; }} },
            { "BOOST_ABILITY_RAGEATTACK", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawBoostAbilityRageAttack = value; }} },
            { "MOD_ABILITY_RAGEATTACK", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawModAbilityRageAttack = value; }} },
            { "MONSTER_COMBAT_XP_VALUE", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawMonsterCombatXpValue = value; }} },
            { "COMBAT_REGEN_ARMOR_DELTA", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawCombatRegenArmorDelta = value; }} },
            { "COMBAT_REGEN_POWER_DELTA", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawCombatRegenDelta = value; }} },
            { "MAX_INVENTORY_SIZE", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawMaxInventorySize = value; }} },
            { "MAX_METABOLISM", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawMaxMetabolism = value; }} },
            { "NPC_MOD_FAVORFROMGIFTS", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawNpcModFavorFromGifts = value; }} },
            { "NPC_MOD_FAVORFROMHANGOUTS", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawNpcModFavorFromHangouts = value; }} },
            { "NPC_MOD_MAXSALESVALUE", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawNpcModMaxSalesValue = value; }} },
            { "NPC_MOD_TRAININGCOST", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawNpcModTrainingCost = value; }} },
            { "NUM_INVENTORY_FOLDERS", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawNumInventoryFolders = value; }} },
            { "HIGH_CLEANLINESS_XP_EARNED_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawHighCleanlinessXpEarnedMod = value; }} },
            { "LOW_CLEANLINESS_XP_EARNED_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawLowCleanlinessXpEarnedMod = value; }} },
            { "MAX_ARMOR_MITIGATION_RATIO", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawMaxArmorMitigationRatio = value; }} },
            { "SHOW_CLEANLINESS_INDICATORS", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawShowCleanlinessIndicators = value; }} },
            { "HIGH_COMMUNITY_XP_EARNED_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawHighCommunityXpEarnedMod = value; }} },
            { "LOW_COMMUNITY_XP_EARNED_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawLowCommunityXpEarnedMod = value; }} },
            { "SHOW_COMMUNITY_INDICATORS", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawShowCommunityIndicators = value; }} },
            { "HIGH_PEACEABLENESS_XP_EARNED_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawHighPeaceblenessXpEarnedMod = value; }} },
            { "LOW_PEACEABLENESS_XP_EARNED_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawLowPeaceblenessXpEarnedMod = value; }} },
            { "SHOW_PEACEABLENESS_INDICATORS", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawShowPeaceblenessIndicators = value; }} },
            { "STAFF_ARMOR_AUTOHEAL", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawStaffArmorAutoHeal = value; }} },
            { "MAX_MAP_PINS_PER_AREA", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawMaxMapPinsPerArea = value; }} },
            { "MAX_MAP_PIN_ICONS", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawMaxMapPinIcons = value; }} },
            { "WORKORDER_COIN_REWARD_MOD", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawWorkOrderCoinRewardMod = value; }} },
            { "MAX_ACTIVE_WORKORDERS", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawMaxActiveWorkOrders = value; }} },
            { "PLAYER_ORDERS_MAX_ACTIVE", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawPlayerOrdersMaxActive = value; }} },
            { "SHOP_INVENTORY_SIZE_DELTA", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawShopInventorySizeDelta = value; }} },
            { "MAIL_SHOP_NUMFREE", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawMailShopNumFree = value; }} },
            { "SHOP_HIRING_MAX_PREPAY_DAYS", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawShopHiringMaxPrepDays = value; }} },
            { "SHOP_LOG_DAYSKEPT", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawShopLogDaysKept = value; }} },
            { "SHOP_HIRING_NUMFREE", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawShopHiringNumFree = value; }} },
            { "MOD_CRITICAL_HIT_DAMAGE", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawCriticalHitDamage = value; }} },
        }; } }

        protected override bool IsCustomFieldParsed(string FieldKey, object FieldValue, ParseErrorInfo ErrorInfo)
        {
            if (ParseDamageTypeEntry(FieldKey, FieldValue, "VULN_", null, VulnerabilityTable, ErrorInfo))
            {
                ParsedFields["VULNERABILITY"] = true;
                return true;
            }

            else if (ParseDamageTypeEntry(FieldKey, FieldValue, "MITIGATION_", null, MitigationTable, ErrorInfo))
            {
                ParsedFields["MITIGATION"] = true;
                return true;
            }

            else if (ParseDamageTypeEntry(FieldKey, FieldValue, "MOD_", "_INDIRECT", IndirectModTable, ErrorInfo))
            {
                ParsedFields["MOD_INDIRECT"] = true;
                return true;
            }

            else if (ParseDamageTypeEntry(FieldKey, FieldValue, "MOD_", "_DIRECT", DirectModTable, ErrorInfo))
            {
                ParsedFields["MOD_DIRECT"] = true;
                return true;
            }
            else
                return false;
        }

        private bool ParseDamageTypeEntry(string FieldKey, object FieldValue, string StartPattern, string EndPattern, Dictionary<DamageType, double> DamageTypeTable, ParseErrorInfo ErrorInfo)
        {
            if (FieldKey.StartsWith(StartPattern) && (EndPattern == null || FieldKey.EndsWith(EndPattern)))
            {
                string RawDamageType;

                if (EndPattern == null)
                    RawDamageType = FieldKey[StartPattern.Length] + FieldKey.Substring(StartPattern.Length + 1).ToLower();
                else
                    RawDamageType = FieldKey[StartPattern.Length] + FieldKey.Substring(StartPattern.Length + 1, FieldKey.Length - StartPattern.Length - EndPattern.Length - 1).ToLower();

                double Value = ParseValue(FieldValue, ErrorInfo);

                DamageType ParsedDamageType;
                if (StringToEnumConversion<DamageType>.TryParse(RawDamageType, out ParsedDamageType, ErrorInfo))
                {
                    if (DamageTypeTable.ContainsKey(ParsedDamageType))
                        ErrorInfo.AddDuplicateString("Advancement", RawDamageType);
                    else
                        DamageTypeTable.Add(ParsedDamageType, Value);
                }

                return true;
            }

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

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            GenerateDamageTypeContent(Generator, "VULN_", "", VulnerabilityTable);
            GenerateDamageTypeContent(Generator, "MITIGATION_", "", MitigationTable);
            GenerateDamageTypeContent(Generator, "MOD_", "_INDIRECT", IndirectModTable);
            GenerateDamageTypeContent(Generator, "MOD_", "_DIRECT", DirectModTable);

            Generator.AddDouble("NonCombatRegenHealthMod", RawNonCombatRegenHealthMod);
            Generator.AddDouble("CombatRegenHealthMod", RawCombatRegenHealthMod);
            Generator.AddDouble("CombatRegenHealthDelta", RawCombatRegenHealthDelta);
            Generator.AddDouble("NonCombatRegenArmorMod", RawNonCombatRegenArmorMod);
            Generator.AddDouble("NonCombatRegenArmordelta", RawNonCombatRegenArmordelta);
            Generator.AddDouble("CombatRegenArmorMod", RawCombatRegenArmorMod);
            Generator.AddDouble("NonCombatRegenPowerMod", RawNonCombatRegenPowerMod);
            Generator.AddDouble("CombatRegenPowerMod", RawCombatRegenPowerMod);
            Generator.AddDouble("NonCombatRegenRageMod", RawNonCombatRegenRageMod);
            Generator.AddDouble("CombatRegenRageMod", RawCombatRegenRageMod);
            Generator.AddDouble("MentalDefenseRating", RawMentalDefenseRating);
            Generator.AddDouble("SprintBoost", RawSprintBoost);
            Generator.AddDouble("TauntMod", RawTauntMod);
            Generator.AddDouble("IgnoreChanceFear", RawIgnoreChanceFear);
            Generator.AddDouble("IgnoreChanceMezz", RawIgnoreChanceMezz);
            Generator.AddDouble("IgnoreChanceKnockback", RawIgnoreChanceKnockback);
            Generator.AddDouble("EvasionChance", RawEvasionChance);
            Generator.AddDouble("LootBoostChanceUncommon", RawLootBoostChanceUncommon);
            Generator.AddDouble("LootBoostChanceRare", RawLootBoostChanceRare);
            Generator.AddDouble("LootBoostChanceExceptional", RawLootBoostChanceExceptional);
            Generator.AddDouble("LootBoostChanceEpic", RawLootBoostChanceEpic);
            Generator.AddDouble("LootBoostChanceLegendary", RawLootBoostChanceLegendary);
            Generator.AddDouble("MaxHealth", RawMaxHealth);
            Generator.AddDouble("MaxArmor", RawMaxArmor);
            Generator.AddDouble("MaxRage", RawMaxRage);
            Generator.AddDouble("MaxPower", RawMaxPower);
            Generator.AddDouble("MaxBreath", RawMaxBreath);
            Generator.AddDouble("BoostUniversalDirect", RawBoostUniversalDirect);
            Generator.AddDouble("BoostAbilityRageAttack", RawBoostAbilityRageAttack);
            Generator.AddDouble("ModAbilityRageAttack", RawModAbilityRageAttack);
            Generator.AddDouble("MonsterCombatXpValue", RawMonsterCombatXpValue);
            Generator.AddDouble("CombatRegenArmorDelta", RawCombatRegenArmorDelta);
            Generator.AddDouble("CombatRegenDelta", RawCombatRegenDelta);
            Generator.AddDouble("CombatRegenMod", RawCombatRegenMod);
            Generator.AddDouble("MaxInventorySize", RawMaxInventorySize);
            Generator.AddDouble("MaxMetabolism", RawMaxMetabolism);
            Generator.AddDouble("NpcModFavorFromGifts", RawNpcModFavorFromGifts);
            Generator.AddDouble("NpcModFavorFromHangouts", RawNpcModFavorFromHangouts);
            Generator.AddDouble("NpcModMaxSalesValue", RawNpcModMaxSalesValue);
            Generator.AddDouble("NpcModTrainingCost", RawNpcModTrainingCost);
            Generator.AddDouble("NumInventoryFolders", RawNumInventoryFolders);
            Generator.AddDouble("HighCleanlinessXpEarnedMod", RawHighCleanlinessXpEarnedMod);
            Generator.AddDouble("LowCleanlinessXpEarnedMod", RawLowCleanlinessXpEarnedMod);
            Generator.AddDouble("MaxArmorMitigationRatio", RawMaxArmorMitigationRatio);
            Generator.AddDouble("ShowCleanlinessIndicators", RawShowCleanlinessIndicators);
            Generator.AddDouble("HighCommunityXpEarnedMod", RawHighCommunityXpEarnedMod);
            Generator.AddDouble("LowCommunityXpEarnedMod", RawLowCommunityXpEarnedMod);
            Generator.AddDouble("ShowCommunityIndicators", RawShowCommunityIndicators);
            Generator.AddDouble("HighPeaceblenessXpEarnedMod", RawHighPeaceblenessXpEarnedMod);
            Generator.AddDouble("LowPeaceblenessXpEarnedMod", RawLowPeaceblenessXpEarnedMod);
            Generator.AddDouble("ShowPeaceblenessIndicators", RawShowPeaceblenessIndicators);
            Generator.AddDouble("StaffArmorAutoHeal", RawStaffArmorAutoHeal);
            Generator.AddDouble("MaxMapPinsPerArea", RawMaxMapPinsPerArea);
            Generator.AddDouble("MaxMapPinIcons", RawMaxMapPinIcons);
            Generator.AddDouble("WorkOrderCoinRewardMod", RawWorkOrderCoinRewardMod);
            Generator.AddDouble("MaxActiveWorkOrders", RawMaxActiveWorkOrders);
            Generator.AddDouble("PlayerOrdersMaxActive", RawPlayerOrdersMaxActive);
            Generator.AddDouble("ShopInventorySizeDelta", RawShopInventorySizeDelta);
            Generator.AddDouble("MailShopNumFree", RawMailShopNumFree);
            Generator.AddDouble("ShopHiringMaxPrepDays", RawShopHiringMaxPrepDays);
            Generator.AddDouble("ShopLogDaysKept", RawShopLogDaysKept);
            Generator.AddDouble("ShopHiringNumFree", RawShopHiringNumFree);
            Generator.AddDouble("CriticalHitDamage", RawCriticalHitDamage);

            Generator.CloseObject();
        }

        private void GenerateDamageTypeContent(JsonGenerator Generator, string StartPattern, string EndPattern, Dictionary<DamageType, double> DamageTypeTable)
        {
            foreach (KeyValuePair<DamageType, double> Entry in DamageTypeTable)
                Generator.AddDouble(StartPattern + Entry.Key.ToString().ToUpper() + EndPattern, Entry.Value);
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get { return ""; }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            return false;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Advancement"; } }
        #endregion
    }
}
