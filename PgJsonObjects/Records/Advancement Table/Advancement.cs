using PgJsonReader;
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
        protected override Dictionary<string, FieldParser> FieldTable { get; } = new Dictionary<string, FieldParser>()
        {
            { "VULNERABILITY", null },
            { "MITIGATION", null },
            { "MOD_INDIRECT", null },
            { "MOD_DIRECT", null },
            { "NONCOMBAT_REGEN_HEALTH_MOD", ParseFieldNonCombatRegenHealthMod },
            { "COMBAT_REGEN_HEALTH_MOD", ParseFieldCombatRegenHealthMod },
            { "COMBAT_REGEN_HEALTH_DELTA", ParseFieldCombatRegenHealthDelta },
            { "NONCOMBAT_REGEN_ARMOR_MOD", ParseFieldNonCombatRegenArmorMod },
            { "NONCOMBAT_REGEN_ARMOR_DELTA", ParseFieldNonCombatRegenArmordelta },
            { "COMBAT_REGEN_ARMOR_MOD", ParseFieldCombatRegenArmorMod },
            { "NONCOMBAT_REGEN_POWER_MOD", ParseFieldNonCombatRegenPowerMod },
            { "COMBAT_REGEN_POWER_MOD", ParseFieldCombatRegenPowerMod },
            { "NONCOMBAT_REGEN_RAGE_MOD", ParseFieldNonCombatRegenRageMod },
            { "COMBAT_REGEN_RAGE_MOD", ParseFieldCombatRegenRageMod },
            { "MENTAL_DEFENSE_RATING", ParseFieldMentalDefenseRating },
            { "SPRINT_BOOST", ParseFieldSprintBoost },
            { "TAUNT_MOD", ParseFieldTauntMod },
            { "IGNORE_CHANCE_FEAR", ParseFieldIgnoreChanceFear },
            { "IGNORE_CHANCE_MEZ", ParseFieldIgnoreChanceMezz },
            { "IGNORE_CHANCE_KNOCKBACK", ParseFieldIgnoreChanceKnockback },
            { "EVASION_CHANCE", ParseFieldEvasionChance },
            { "LOOT_BOOST_CHANCE_UNCOMMON", ParseFieldLootBoostChanceUncommon },
            { "LOOT_BOOST_CHANCE_RARE", ParseFieldLootBoostChanceRare },
            { "LOOT_BOOST_CHANCE_EXCEPTIONAL", ParseFieldLootBoostChanceExceptional },
            { "LOOT_BOOST_CHANCE_EPIC", ParseFieldLootBoostChanceEpic },
            { "LOOT_BOOST_CHANCE_LEGENDARY", ParseFieldLootBoostChanceLegendary },
            { "MAX_HEALTH", ParseFieldMaxHealth },
            { "MAX_ARMOR", ParseFieldMaxArmor },
            { "MAX_RAGE", ParseFieldMaxRage },
            { "MAX_POWER", ParseFieldMaxPower },
            { "MAX_BREATH", ParseFieldMaxBreath },
            { "BOOST_UNIVERSAL_DIRECT", ParseFieldBoostUniversalDirect },
            { "BOOST_ABILITY_RAGEATTACK", ParseFieldBoostAbilityRageAttack },
            { "MOD_ABILITY_RAGEATTACK", ParseFieldModAbilityRageAttack },
            { "MONSTER_COMBAT_XP_VALUE", ParseFieldMonsterCombatXpValue },
            { "COMBAT_REGEN_ARMOR_DELTA", ParseFieldCombatRegenArmorDelta },
            { "COMBAT_REGEN_POWER_DELTA", ParseFieldCombatRegenDelta },
            { "MAX_INVENTORY_SIZE", ParseFieldMaxInventorySize },
            { "MAX_METABOLISM", ParseFieldMaxMetabolism },
            { "NPC_MOD_FAVORFROMGIFTS", ParseFieldNpcModFavorFromGifts },
            { "NPC_MOD_FAVORFROMHANGOUTS", ParseFieldNpcModFavorFromHangouts },
            { "NPC_MOD_MAXSALESVALUE", ParseFieldNpcModMaxSalesValue },
            { "NPC_MOD_TRAININGCOST", ParseFieldNpcModTrainingCost },
            { "NUM_INVENTORY_FOLDERS", ParseFieldNumInventoryFolders },
            { "HIGH_CLEANLINESS_XP_EARNED_MOD", ParseFieldHighCleanlinessXpEarnedMod },
            { "LOW_CLEANLINESS_XP_EARNED_MOD", ParseFieldLowCleanlinessXpEarnedMod },
            { "MAX_ARMOR_MITIGATION_RATIO", ParseFieldMaxArmorMitigationRatio },
            { "SHOW_CLEANLINESS_INDICATORS", ParseFieldShowCleanlinessIndicators },
            { "HIGH_COMMUNITY_XP_EARNED_MOD", ParseFieldHighCommunityXpEarnedMod },
            { "LOW_COMMUNITY_XP_EARNED_MOD", ParseFieldLowCommunityXpEarnedMod },
            { "SHOW_COMMUNITY_INDICATORS", ParseFieldShowCommunityIndicators },
            { "HIGH_PEACEABLENESS_XP_EARNED_MOD", ParseFieldHighPeaceblenessXpEarnedMod },
            { "LOW_PEACEABLENESS_XP_EARNED_MOD", ParseFieldLowPeaceblenessXpEarnedMod },
            { "SHOW_PEACEABLENESS_INDICATORS", ParseFieldShowPeaceblenessIndicators },
            { "STAFF_ARMOR_AUTOHEAL", ParseFieldStaffArmorAutoHeal },
            { "MAX_MAP_PINS_PER_AREA", ParseFieldMaxMapPinsPerArea },
            { "MAX_MAP_PIN_ICONS", ParseFieldMaxMapPinIcons },
            { "WORKORDER_COIN_REWARD_MOD", ParseFieldWorkOrderCoinRewardMod },
            { "MAX_ACTIVE_WORKORDERS", ParseFieldMaxActiveWorkOrders },
            { "PLAYER_ORDERS_MAX_ACTIVE", ParseFieldPlayerOrdersMaxActive },
            { "SHOP_INVENTORY_SIZE_DELTA", ParseFieldShopInventorySizeDelta },
            { "MAIL_SHOP_NUMFREE", ParseFieldMailShopNumFree },
            { "SHOP_HIRING_MAX_PREPAY_DAYS", ParseFieldShopHiringMaxPrepDays },
            { "SHOP_LOG_DAYSKEPT", ParseFieldShopLogDaysKept },
            { "SHOP_HIRING_NUMFREE", ParseFieldShopHiringNumFree },
            { "MOD_CRITICAL_HIT_DAMAGE", ParseFieldCriticalHitDamage },
        };

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

        private static void ParseFieldNonCombatRegenHealthMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement NonCombatRegenHealthMod", This.ParseNonCombatRegenHealthMod);
        }

        private void ParseNonCombatRegenHealthMod(double RawNonCombatRegenHealthMod, ParseErrorInfo ErrorInfo)
        {
            this.RawNonCombatRegenHealthMod = RawNonCombatRegenHealthMod;
        }

        private static void ParseFieldCombatRegenHealthMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement CombatRegenHealthMod", This.ParseCombatRegenHealthMod);
        }

        private void ParseCombatRegenHealthMod(double RawCombatRegenHealthMod, ParseErrorInfo ErrorInfo)
        {
            this.RawCombatRegenHealthMod = RawCombatRegenHealthMod;
        }

        private static void ParseFieldCombatRegenHealthDelta(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement CombatRegenHealthDelta", This.ParseCombatRegenHealthDelta);
        }

        private void ParseCombatRegenHealthDelta(double RawCombatRegenHealthDelta, ParseErrorInfo ErrorInfo)
        {
            this.RawCombatRegenHealthDelta = RawCombatRegenHealthDelta;
        }

        private static void ParseFieldNonCombatRegenArmorMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement NonCombatRegenArmorMod", This.ParseNonCombatRegenArmorMod);
        }

        private void ParseNonCombatRegenArmorMod(double RawNonCombatRegenArmorMod, ParseErrorInfo ErrorInfo)
        {
            this.RawNonCombatRegenArmorMod = RawNonCombatRegenArmorMod;
        }

        private static void ParseFieldNonCombatRegenArmordelta(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement NonCombatRegenArmordelta", This.ParseNonCombatRegenArmordelta);
        }

        private void ParseNonCombatRegenArmordelta(double RawNonCombatRegenArmordelta, ParseErrorInfo ErrorInfo)
        {
            this.RawNonCombatRegenArmordelta = RawNonCombatRegenArmordelta;
        }

        private static void ParseFieldCombatRegenArmorMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement CombatRegenArmorMod", This.ParseCombatRegenArmorMod);
        }

        private void ParseCombatRegenArmorMod(double RawCombatRegenArmorMod, ParseErrorInfo ErrorInfo)
        {
            this.RawCombatRegenArmorMod = RawCombatRegenArmorMod;
        }

        private static void ParseFieldNonCombatRegenPowerMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement NonCombatRegenPowerMod", This.ParseNonCombatRegenPowerMod);
        }

        private void ParseNonCombatRegenPowerMod(double RawNonCombatRegenPowerMod, ParseErrorInfo ErrorInfo)
        {
            this.RawNonCombatRegenPowerMod = RawNonCombatRegenPowerMod;
        }

        private static void ParseFieldCombatRegenPowerMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement CombatRegenPowerMod", This.ParseCombatRegenPowerMod);
        }

        private void ParseCombatRegenPowerMod(double RawCombatRegenPowerMod, ParseErrorInfo ErrorInfo)
        {
            this.RawCombatRegenPowerMod = RawCombatRegenPowerMod;
        }

        private static void ParseFieldNonCombatRegenRageMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement NonCombatRegenRageMod", This.ParseNonCombatRegenRageMod);
        }

        private void ParseNonCombatRegenRageMod(double RawNonCombatRegenRageMod, ParseErrorInfo ErrorInfo)
        {
            this.RawNonCombatRegenRageMod = RawNonCombatRegenRageMod;
        }

        private static void ParseFieldCombatRegenRageMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement CombatRegenRageMod", This.ParseCombatRegenRageMod);
        }

        private void ParseCombatRegenRageMod(double RawCombatRegenRageMod, ParseErrorInfo ErrorInfo)
        {
            this.RawCombatRegenRageMod = RawCombatRegenRageMod;
        }

        private static void ParseFieldMentalDefenseRating(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement MentalDefenseRating", This.ParseMentalDefenseRating);
        }

        private void ParseMentalDefenseRating(double RawMentalDefenseRating, ParseErrorInfo ErrorInfo)
        {
            this.RawMentalDefenseRating = RawMentalDefenseRating;
        }

        private static void ParseFieldSprintBoost(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement SprintBoost", This.ParseSprintBoost);
        }

        private void ParseSprintBoost(double RawSprintBoost, ParseErrorInfo ErrorInfo)
        {
            this.RawSprintBoost = RawSprintBoost;
        }

        private static void ParseFieldTauntMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement TauntMod", This.ParseTauntMod);
        }

        private void ParseTauntMod(double RawTauntMod, ParseErrorInfo ErrorInfo)
        {
            this.RawTauntMod = RawTauntMod;
        }

        private static void ParseFieldIgnoreChanceFear(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement IgnoreChanceFear", This.ParseIgnoreChanceFear);
        }

        private void ParseIgnoreChanceFear(double RawIgnoreChanceFear, ParseErrorInfo ErrorInfo)
        {
            this.RawIgnoreChanceFear = RawIgnoreChanceFear;
        }

        private static void ParseFieldIgnoreChanceMezz(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement IgnoreChanceMezz", This.ParseIgnoreChanceMezz);
        }

        private void ParseIgnoreChanceMezz(double RawIgnoreChanceMezz, ParseErrorInfo ErrorInfo)
        {
            this.RawIgnoreChanceMezz = RawIgnoreChanceMezz;
        }

        private static void ParseFieldIgnoreChanceKnockback(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement IgnoreChanceKnockback", This.ParseIgnoreChanceKnockback);
        }

        private void ParseIgnoreChanceKnockback(double RawIgnoreChanceKnockback, ParseErrorInfo ErrorInfo)
        {
            this.RawIgnoreChanceKnockback = RawIgnoreChanceKnockback;
        }

        private static void ParseFieldEvasionChance(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement EvasionChance", This.ParseEvasionChance);
        }

        private void ParseEvasionChance(double RawEvasionChance, ParseErrorInfo ErrorInfo)
        {
            this.RawEvasionChance = RawEvasionChance;
        }

        private static void ParseFieldLootBoostChanceUncommon(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement LootBoostChanceUncommon", This.ParseLootBoostChanceUncommon);
        }

        private void ParseLootBoostChanceUncommon(double RawLootBoostChanceUncommon, ParseErrorInfo ErrorInfo)
        {
            this.RawLootBoostChanceUncommon = RawLootBoostChanceUncommon;
        }

        private static void ParseFieldLootBoostChanceRare(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement LootBoostChanceRare", This.ParseLootBoostChanceRare);
        }

        private void ParseLootBoostChanceRare(double RawLootBoostChanceRare, ParseErrorInfo ErrorInfo)
        {
            this.RawLootBoostChanceRare = RawLootBoostChanceRare;
        }

        private static void ParseFieldLootBoostChanceExceptional(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement LootBoostChanceExceptional", This.ParseLootBoostChanceExceptional);
        }

        private void ParseLootBoostChanceExceptional(double RawLootBoostChanceExceptional, ParseErrorInfo ErrorInfo)
        {
            this.RawLootBoostChanceExceptional = RawLootBoostChanceExceptional;
        }

        private static void ParseFieldLootBoostChanceEpic(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement LootBoostChanceEpic", This.ParseLootBoostChanceEpic);
        }

        private void ParseLootBoostChanceEpic(double RawLootBoostChanceEpic, ParseErrorInfo ErrorInfo)
        {
            this.RawLootBoostChanceEpic = RawLootBoostChanceEpic;
        }

        private static void ParseFieldLootBoostChanceLegendary(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement LootBoostChanceLegendary", This.ParseLootBoostChanceLegendary);
        }

        private void ParseLootBoostChanceLegendary(double RawLootBoostChanceLegendary, ParseErrorInfo ErrorInfo)
        {
            this.RawLootBoostChanceLegendary = RawLootBoostChanceLegendary;
        }

        private static void ParseFieldMaxHealth(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement MaxHealth", This.ParseMaxHealth);
        }

        private void ParseMaxHealth(double RawMaxHealth, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxHealth = RawMaxHealth;
        }

        private static void ParseFieldMaxArmor(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement MaxArmor", This.ParseMaxArmor);
        }

        private void ParseMaxArmor(double RawMaxArmor, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxArmor = RawMaxArmor;
        }

        private static void ParseFieldMaxRage(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement MaxRage", This.ParseMaxRage);
        }

        private void ParseMaxRage(double RawMaxRage, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxRage = RawMaxRage;
        }

        private static void ParseFieldMaxPower(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement MaxPower", This.ParseMaxPower);
        }

        private void ParseMaxPower(double RawMaxPower, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxPower = RawMaxPower;
        }

        private static void ParseFieldMaxBreath(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement MaxBreath", This.ParseMaxBreath);
        }

        private void ParseMaxBreath(double RawMaxBreath, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxBreath = RawMaxBreath;
        }

        private static void ParseFieldBoostUniversalDirect(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement BoostUniversalDirect", This.ParseBoostUniversalDirect);
        }

        private void ParseBoostUniversalDirect(double RawBoostUniversalDirect, ParseErrorInfo ErrorInfo)
        {
            this.RawBoostUniversalDirect = RawBoostUniversalDirect;
        }

        private static void ParseFieldBoostAbilityRageAttack(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement BoostAbilityRageAttack", This.ParseBoostAbilityRageAttack);
        }

        private void ParseBoostAbilityRageAttack(double RawBoostAbilityRageAttack, ParseErrorInfo ErrorInfo)
        {
            this.RawBoostAbilityRageAttack = RawBoostAbilityRageAttack;
        }

        private static void ParseFieldModAbilityRageAttack(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement ModAbilityRageAttack", This.ParseModAbilityRageAttack);
        }

        private void ParseModAbilityRageAttack(double RawModAbilityRageAttack, ParseErrorInfo ErrorInfo)
        {
            this.RawModAbilityRageAttack = RawModAbilityRageAttack;
        }

        private static void ParseFieldMonsterCombatXpValue(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement MonsterCombatXpValue", This.ParseMonsterCombatXpValue);
        }

        private void ParseMonsterCombatXpValue(double RawMonsterCombatXpValue, ParseErrorInfo ErrorInfo)
        {
            this.RawMonsterCombatXpValue = RawMonsterCombatXpValue;
        }

        private static void ParseFieldCombatRegenArmorDelta(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement CombatRegenArmorDelta", This.ParseCombatRegenArmorDelta);
        }

        private void ParseCombatRegenArmorDelta(double RawCombatRegenArmorDelta, ParseErrorInfo ErrorInfo)
        {
            this.RawCombatRegenArmorDelta = RawCombatRegenArmorDelta;
        }

        private static void ParseFieldCombatRegenDelta(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement CombatRegenDelta", This.ParseCombatRegenDelta);
        }

        private void ParseCombatRegenDelta(double RawCombatRegenDelta, ParseErrorInfo ErrorInfo)
        {
            this.RawCombatRegenDelta = RawCombatRegenDelta;
        }

        private static void ParseFieldCombatRegenMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement CombatRegenMod", This.ParseCombatRegenMod);
        }

        private void ParseCombatRegenMod(double RawCombatRegenMod, ParseErrorInfo ErrorInfo)
        {
            this.RawCombatRegenMod = RawCombatRegenMod;
        }

        private static void ParseFieldMaxInventorySize(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement MaxInventorySize", This.ParseMaxInventorySize);
        }

        private void ParseMaxInventorySize(double RawMaxInventorySize, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxInventorySize = RawMaxInventorySize;
        }

        private static void ParseFieldMaxMetabolism(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement MaxMetabolism", This.ParseMaxMetabolism);
        }

        private void ParseMaxMetabolism(double RawMaxMetabolism, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxMetabolism = RawMaxMetabolism;
        }

        private static void ParseFieldNpcModFavorFromGifts(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement NpcModFavorFromGifts", This.ParseNpcModFavorFromGifts);
        }

        private void ParseNpcModFavorFromGifts(double RawNpcModFavorFromGifts, ParseErrorInfo ErrorInfo)
        {
            this.RawNpcModFavorFromGifts = RawNpcModFavorFromGifts;
        }

        private static void ParseFieldNpcModFavorFromHangouts(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement NpcModFavorFromHangouts", This.ParseNpcModFavorFromHangouts);
        }

        private void ParseNpcModFavorFromHangouts(double RawNpcModFavorFromHangouts, ParseErrorInfo ErrorInfo)
        {
            this.RawNpcModFavorFromHangouts = RawNpcModFavorFromHangouts;
        }

        private static void ParseFieldNpcModMaxSalesValue(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement NpcModMaxSalesValue", This.ParseNpcModMaxSalesValue);
        }

        private void ParseNpcModMaxSalesValue(double RawNpcModMaxSalesValue, ParseErrorInfo ErrorInfo)
        {
            this.RawNpcModMaxSalesValue = RawNpcModMaxSalesValue;
        }

        private static void ParseFieldNpcModTrainingCost(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement NpcModTrainingCost", This.ParseNpcModTrainingCost);
        }

        private void ParseNpcModTrainingCost(double RawNpcModTrainingCost, ParseErrorInfo ErrorInfo)
        {
            this.RawNpcModTrainingCost = RawNpcModTrainingCost;
        }

        private static void ParseFieldNumInventoryFolders(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "AdvancementTable NumInventoryFolders", This.ParseNumInventoryFolders);
        }

        private void ParseNumInventoryFolders(long RawNumInventoryFolders, ParseErrorInfo ErrorInfo)
        {
            this.RawNumInventoryFolders = (int)RawNumInventoryFolders;
        }
        
        private static void ParseFieldHighCleanlinessXpEarnedMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement HighCleanlinessXpEarnedMod", This.ParseHighCleanlinessXpEarnedMod);
        }

        private void ParseHighCleanlinessXpEarnedMod(double RawHighCleanlinessXpEarnedMod, ParseErrorInfo ErrorInfo)
        {
            this.RawHighCleanlinessXpEarnedMod = RawHighCleanlinessXpEarnedMod;
        }

        private static void ParseFieldLowCleanlinessXpEarnedMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement LowCleanlinessXpEarnedMod", This.ParseLowCleanlinessXpEarnedMod);
        }

        private void ParseLowCleanlinessXpEarnedMod(double RawLowCleanlinessXpEarnedMod, ParseErrorInfo ErrorInfo)
        {
            this.RawLowCleanlinessXpEarnedMod = RawLowCleanlinessXpEarnedMod;
        }

        private static void ParseFieldMaxArmorMitigationRatio(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement MaxArmorMitigationRatio", This.ParseMaxArmorMitigationRatio);
        }

        private void ParseMaxArmorMitigationRatio(double RawMaxArmorMitigationRatio, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxArmorMitigationRatio = RawMaxArmorMitigationRatio;
        }

        private static void ParseFieldShowCleanlinessIndicators(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement ShowCleanlinessIndicators", This.ParseShowCleanlinessIndicators);
        }

        private void ParseShowCleanlinessIndicators(double RawShowCleanlinessIndicators, ParseErrorInfo ErrorInfo)
        {
            this.RawShowCleanlinessIndicators = RawShowCleanlinessIndicators;
        }

        private static void ParseFieldHighCommunityXpEarnedMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement HighCommunityXpEarnedMod", This.ParseHighCommunityXpEarnedMod);
        }

        private void ParseHighCommunityXpEarnedMod(double RawHighCommunityXpEarnedMod, ParseErrorInfo ErrorInfo)
        {
            this.RawHighCommunityXpEarnedMod = RawHighCommunityXpEarnedMod;
        }

        private static void ParseFieldLowCommunityXpEarnedMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement LowCommunityXpEarnedMod", This.ParseLowCommunityXpEarnedMod);
        }

        private void ParseLowCommunityXpEarnedMod(double RawLowCommunityXpEarnedMod, ParseErrorInfo ErrorInfo)
        {
            this.RawLowCommunityXpEarnedMod = RawLowCommunityXpEarnedMod;
        }

        private static void ParseFieldShowCommunityIndicators(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement ShowCommunityIndicators", This.ParseShowCommunityIndicators);
        }

        private void ParseShowCommunityIndicators(double RawShowCommunityIndicators, ParseErrorInfo ErrorInfo)
        {
            this.RawShowCommunityIndicators = RawShowCommunityIndicators;
        }

        private static void ParseFieldHighPeaceblenessXpEarnedMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement HighPeaceblenessXpEarnedMod", This.ParseHighPeaceblenessXpEarnedMod);
        }

        private void ParseHighPeaceblenessXpEarnedMod(double RawHighPeaceblenessXpEarnedMod, ParseErrorInfo ErrorInfo)
        {
            this.RawHighPeaceblenessXpEarnedMod = RawHighPeaceblenessXpEarnedMod;
        }

        private static void ParseFieldLowPeaceblenessXpEarnedMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement LowPeaceblenessXpEarnedMod", This.ParseLowPeaceblenessXpEarnedMod);
        }

        private void ParseLowPeaceblenessXpEarnedMod(double RawLowPeaceblenessXpEarnedMod, ParseErrorInfo ErrorInfo)
        {
            this.RawLowPeaceblenessXpEarnedMod = RawLowPeaceblenessXpEarnedMod;
        }

        private static void ParseFieldShowPeaceblenessIndicators(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement ShowPeaceblenessIndicators", This.ParseShowPeaceblenessIndicators);
        }

        private void ParseShowPeaceblenessIndicators(double RawShowPeaceblenessIndicators, ParseErrorInfo ErrorInfo)
        {
            this.RawShowPeaceblenessIndicators = RawShowPeaceblenessIndicators;
        }

        private static void ParseFieldStaffArmorAutoHeal(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement StaffArmorAutoHeal", This.ParseStaffArmorAutoHeal);
        }

        private void ParseStaffArmorAutoHeal(double RawStaffArmorAutoHeal, ParseErrorInfo ErrorInfo)
        {
            this.RawStaffArmorAutoHeal = RawStaffArmorAutoHeal;
        }

        private static void ParseFieldMaxMapPinsPerArea(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement MaxMapPinsPerArea", This.ParseMaxMapPinsPerArea);
        }

        private void ParseMaxMapPinsPerArea(double RawMaxMapPinsPerArea, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxMapPinsPerArea = RawMaxMapPinsPerArea;
        }

        private static void ParseFieldMaxMapPinIcons(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement MaxMapPinIcons", This.ParseMaxMapPinIcons);
        }

        private void ParseMaxMapPinIcons(double RawMaxMapPinIcons, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxMapPinIcons = RawMaxMapPinIcons;
        }

        private static void ParseFieldWorkOrderCoinRewardMod(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement WorkOrderCoinRewardMod", This.ParseWorkOrderCoinRewardMod);
        }

        private void ParseWorkOrderCoinRewardMod(double RawWorkOrderCoinRewardMod, ParseErrorInfo ErrorInfo)
        {
            this.RawWorkOrderCoinRewardMod = RawWorkOrderCoinRewardMod;
        }

        private static void ParseFieldMaxActiveWorkOrders(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement MaxActiveWorkOrders", This.ParseMaxActiveWorkOrders);
        }

        private void ParseMaxActiveWorkOrders(double RawMaxActiveWorkOrders, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxActiveWorkOrders = RawMaxActiveWorkOrders;
        }

        private static void ParseFieldPlayerOrdersMaxActive(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement PlayerOrdersMaxActive", This.ParsePlayerOrdersMaxActive);
        }

        private void ParsePlayerOrdersMaxActive(double RawPlayerOrdersMaxActive, ParseErrorInfo ErrorInfo)
        {
            this.RawPlayerOrdersMaxActive = RawPlayerOrdersMaxActive;
        }

        private static void ParseFieldShopInventorySizeDelta(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement ShopInventorySizeDelta", This.ParseShopInventorySizeDelta);
        }

        private void ParseShopInventorySizeDelta(double RawShopInventorySizeDelta, ParseErrorInfo ErrorInfo)
        {
            this.RawShopInventorySizeDelta = RawShopInventorySizeDelta;
        }

        private static void ParseFieldMailShopNumFree(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement MailShopNumFree", This.ParseMailShopNumFree);
        }

        private void ParseMailShopNumFree(double RawMailShopNumFree, ParseErrorInfo ErrorInfo)
        {
            this.RawMailShopNumFree = RawMailShopNumFree;
        }

        private static void ParseFieldShopHiringMaxPrepDays(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement ShopHiringMaxPrepDays", This.ParseShopHiringMaxPrepDays);
        }

        private void ParseShopHiringMaxPrepDays(double RawShopHiringMaxPrepDays, ParseErrorInfo ErrorInfo)
        {
            this.RawShopHiringMaxPrepDays = RawShopHiringMaxPrepDays;
        }

        private static void ParseFieldShopLogDaysKept(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement ShopLogDaysKept", This.ParseShopLogDaysKept);
        }

        private void ParseShopLogDaysKept(double RawShopLogDaysKept, ParseErrorInfo ErrorInfo)
        {
            this.RawShopLogDaysKept = RawShopLogDaysKept;
        }

        private static void ParseFieldShopHiringNumFree(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement ShopHiringNumFree", This.ParseShopHiringNumFree);
        }

        private void ParseShopHiringNumFree(double RawShopHiringNumFree, ParseErrorInfo ErrorInfo)
        {
            this.RawShopHiringNumFree = RawShopHiringNumFree;
        }

        private static void ParseFieldCriticalHitDamage(Advancement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Advancement CriticalHitDamage", This.ParseCriticalHitDamage);
        }

        private void ParseCriticalHitDamage(double RawCriticalHitDamage, ParseErrorInfo ErrorInfo)
        {
            this.RawCriticalHitDamage = RawCriticalHitDamage;
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
