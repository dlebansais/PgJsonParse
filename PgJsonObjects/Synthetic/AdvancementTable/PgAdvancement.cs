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
            return new PgAdvancement(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public double NonCombatRegenHealthMod { get { return RawNonCombatRegenHealthMod.HasValue ? RawNonCombatRegenHealthMod.Value : 0; } }
        public double? RawNonCombatRegenHealthMod { get { return GetDouble(4); } }
        public double CombatRegenHealthMod { get { return RawCombatRegenHealthMod.HasValue ? RawCombatRegenHealthMod.Value : 0; } }
        public double? RawCombatRegenHealthMod { get { return GetDouble(8); } }
        public double CombatRegenHealthDelta { get { return RawCombatRegenHealthDelta.HasValue ? RawCombatRegenHealthDelta.Value : 0; } }
        public double? RawCombatRegenHealthDelta { get { return GetDouble(12); } }
        public double NonCombatRegenArmorMod { get { return RawNonCombatRegenArmorMod.HasValue ? RawNonCombatRegenArmorMod.Value : 0; } }
        public double? RawNonCombatRegenArmorMod { get { return GetDouble(16); } }
        public double NonCombatRegenArmordelta { get { return RawNonCombatRegenArmordelta.HasValue ? RawNonCombatRegenArmordelta.Value : 0; } }
        public double? RawNonCombatRegenArmordelta { get { return GetDouble(20); } }
        public double CombatRegenArmorMod { get { return RawCombatRegenArmorMod.HasValue ? RawCombatRegenArmorMod.Value : 0; } }
        public double? RawCombatRegenArmorMod { get { return GetDouble(24); } }
        public double NonCombatRegenPowerMod { get { return RawNonCombatRegenPowerMod.HasValue ? RawNonCombatRegenPowerMod.Value : 0; } }
        public double? RawNonCombatRegenPowerMod { get { return GetDouble(28); } }
        public double CombatRegenPowerMod { get { return RawCombatRegenPowerMod.HasValue ? RawCombatRegenPowerMod.Value : 0; } }
        public double? RawCombatRegenPowerMod { get { return GetDouble(32); } }
        public double NonCombatRegenRageMod { get { return RawNonCombatRegenRageMod.HasValue ? RawNonCombatRegenRageMod.Value : 0; } }
        public double? RawNonCombatRegenRageMod { get { return GetDouble(36); } }
        public double CombatRegenRageMod { get { return RawCombatRegenRageMod.HasValue ? RawCombatRegenRageMod.Value : 0; } }
        public double? RawCombatRegenRageMod { get { return GetDouble(40); } }
        public double MentalDefenseRating { get { return RawMentalDefenseRating.HasValue ? RawMentalDefenseRating.Value : 0; } }
        public double? RawMentalDefenseRating { get { return GetDouble(44); } }
        public double SprintBoost { get { return RawSprintBoost.HasValue ? RawSprintBoost.Value : 0; } }
        public double? RawSprintBoost { get { return GetDouble(48); } }
        public double TauntMod { get { return RawTauntMod.HasValue ? RawTauntMod.Value : 0; } }
        public double? RawTauntMod { get { return GetDouble(52); } }
        public double IgnoreChanceFear { get { return RawIgnoreChanceFear.HasValue ? RawIgnoreChanceFear.Value : 0; } }
        public double? RawIgnoreChanceFear { get { return GetDouble(56); } }
        public double IgnoreChanceMezz { get { return RawIgnoreChanceMezz.HasValue ? RawIgnoreChanceMezz.Value : 0; } }
        public double? RawIgnoreChanceMezz { get { return GetDouble(60); } }
        public double IgnoreChanceKnockback { get { return RawIgnoreChanceKnockback.HasValue ? RawIgnoreChanceKnockback.Value : 0; } }
        public double? RawIgnoreChanceKnockback { get { return GetDouble(64); } }
        public double EvasionChance { get { return RawEvasionChance.HasValue ? RawEvasionChance.Value : 0; } }
        public double? RawEvasionChance { get { return GetDouble(68); } }
        public double LootBoostChanceUncommon { get { return RawLootBoostChanceUncommon.HasValue ? RawLootBoostChanceUncommon.Value : 0; } }
        public double? RawLootBoostChanceUncommon { get { return GetDouble(72); } }
        public double LootBoostChanceRare { get { return RawLootBoostChanceRare.HasValue ? RawLootBoostChanceRare.Value : 0; } }
        public double? RawLootBoostChanceRare { get { return GetDouble(76); } }
        public double LootBoostChanceExceptional { get { return RawLootBoostChanceExceptional.HasValue ? RawLootBoostChanceExceptional.Value : 0; } }
        public double? RawLootBoostChanceExceptional { get { return GetDouble(80); } }
        public double LootBoostChanceEpic { get { return RawLootBoostChanceEpic.HasValue ? RawLootBoostChanceEpic.Value : 0; } }
        public double? RawLootBoostChanceEpic { get { return GetDouble(84); } }
        public double LootBoostChanceLegendary { get { return RawLootBoostChanceLegendary.HasValue ? RawLootBoostChanceLegendary.Value : 0; } }
        public double? RawLootBoostChanceLegendary { get { return GetDouble(88); } }
        public double MaxHealth { get { return RawMaxHealth.HasValue ? RawMaxHealth.Value : 0; } }
        public double? RawMaxHealth { get { return GetDouble(92); } }
        public double MaxArmor { get { return RawMaxArmor.HasValue ? RawMaxArmor.Value : 0; } }
        public double? RawMaxArmor { get { return GetDouble(96); } }
        public double MaxRage { get { return RawMaxRage.HasValue ? RawMaxRage.Value : 0; } }
        public double? RawMaxRage { get { return GetDouble(100); } }
        public double MaxPower { get { return RawMaxPower.HasValue ? RawMaxPower.Value : 0; } }
        public double? RawMaxPower { get { return GetDouble(104); } }
        public double MaxBreath { get { return RawMaxBreath.HasValue ? RawMaxBreath.Value : 0; } }
        public double? RawMaxBreath { get { return GetDouble(108); } }
        public double BoostUniversalDirect { get { return RawBoostUniversalDirect.HasValue ? RawBoostUniversalDirect.Value : 0; } }
        public double? RawBoostUniversalDirect { get { return GetDouble(112); } }
        public double BoostAbilityRageAttack { get { return RawBoostAbilityRageAttack.HasValue ? RawBoostAbilityRageAttack.Value : 0; } }
        public double? RawBoostAbilityRageAttack { get { return GetDouble(116); } }
        public double ModAbilityRageAttack { get { return RawModAbilityRageAttack.HasValue ? RawModAbilityRageAttack.Value : 0; } }
        public double? RawModAbilityRageAttack { get { return GetDouble(120); } }
        public double MonsterCombatXpValue { get { return RawMonsterCombatXpValue.HasValue ? RawMonsterCombatXpValue.Value : 0; } }
        public double? RawMonsterCombatXpValue { get { return GetDouble(124); } }
        public double CombatRegenArmorDelta { get { return RawCombatRegenArmorDelta.HasValue ? RawCombatRegenArmorDelta.Value : 0; } }
        public double? RawCombatRegenArmorDelta { get { return GetDouble(128); } }
        public double CombatRegenDelta { get { return RawCombatRegenDelta.HasValue ? RawCombatRegenDelta.Value : 0; } }
        public double? RawCombatRegenDelta { get { return GetDouble(132); } }
        public double MaxInventorySize { get { return RawMaxInventorySize.HasValue ? RawMaxInventorySize.Value : 0; } }
        public double? RawMaxInventorySize { get { return GetDouble(136); } }
        public double MaxMetabolism { get { return RawMaxMetabolism.HasValue ? RawMaxMetabolism.Value : 0; } }
        public double? RawMaxMetabolism { get { return GetDouble(140); } }
        public double NpcModFavorFromGifts { get { return RawNpcModFavorFromGifts.HasValue ? RawNpcModFavorFromGifts.Value : 0; } }
        public double? RawNpcModFavorFromGifts { get { return GetDouble(144); } }
        public double NpcModFavorFromHangouts { get { return RawNpcModFavorFromHangouts.HasValue ? RawNpcModFavorFromHangouts.Value : 0; } }
        public double? RawNpcModFavorFromHangouts { get { return GetDouble(148); } }
        public double NpcModMaxSalesValue { get { return RawNpcModMaxSalesValue.HasValue ? RawNpcModMaxSalesValue.Value : 0; } }
        public double? RawNpcModMaxSalesValue { get { return GetDouble(152); } }
        public double NpcModTrainingCost { get { return RawNpcModTrainingCost.HasValue ? RawNpcModTrainingCost.Value : 0; } }
        public double? RawNpcModTrainingCost { get { return GetDouble(156); } }
        public int NumInventoryFolders { get { return RawNumInventoryFolders.HasValue ? RawNumInventoryFolders.Value : 0; } }
        public int? RawNumInventoryFolders { get { return GetInt(160); } }
        public double HighCleanlinessXpEarnedMod { get { return RawHighCleanlinessXpEarnedMod.HasValue ? RawHighCleanlinessXpEarnedMod.Value : 0; } }
        public double? RawHighCleanlinessXpEarnedMod { get { return GetDouble(164); } }
        public double LowCleanlinessXpEarnedMod { get { return RawLowCleanlinessXpEarnedMod.HasValue ? RawLowCleanlinessXpEarnedMod.Value : 0; } }
        public double? RawLowCleanlinessXpEarnedMod { get { return GetDouble(168); } }
        public double MaxArmorMitigationRatio { get { return RawMaxArmorMitigationRatio.HasValue ? RawMaxArmorMitigationRatio.Value : 0; } }
        public double? RawMaxArmorMitigationRatio { get { return GetDouble(172); } }
        public double ShowCleanlinessIndicators { get { return RawShowCleanlinessIndicators.HasValue ? RawShowCleanlinessIndicators.Value : 0; } }
        public double? RawShowCleanlinessIndicators { get { return GetDouble(176); } }
        public double HighCommunityXpEarnedMod { get { return RawHighCommunityXpEarnedMod.HasValue ? RawHighCommunityXpEarnedMod.Value : 0; } }
        public double? RawHighCommunityXpEarnedMod { get { return GetDouble(180); } }
        public double LowCommunityXpEarnedMod { get { return RawLowCommunityXpEarnedMod.HasValue ? RawLowCommunityXpEarnedMod.Value : 0; } }
        public double? RawLowCommunityXpEarnedMod { get { return GetDouble(184); } }
        public double ShowCommunityIndicators { get { return RawShowCommunityIndicators.HasValue ? RawShowCommunityIndicators.Value : 0; } }
        public double? RawShowCommunityIndicators { get { return GetDouble(188); } }
        public double HighPeaceblenessXpEarnedMod { get { return RawHighPeaceblenessXpEarnedMod.HasValue ? RawHighPeaceblenessXpEarnedMod.Value : 0; } }
        public double? RawHighPeaceblenessXpEarnedMod { get { return GetDouble(192); } }
        public double LowPeaceblenessXpEarnedMod { get { return RawLowPeaceblenessXpEarnedMod.HasValue ? RawLowPeaceblenessXpEarnedMod.Value : 0; } }
        public double? RawLowPeaceblenessXpEarnedMod { get { return GetDouble(196); } }
        public double ShowPeaceblenessIndicators { get { return RawShowPeaceblenessIndicators.HasValue ? RawShowPeaceblenessIndicators.Value : 0; } }
        public double? RawShowPeaceblenessIndicators { get { return GetDouble(200); } }
        public double StaffArmorAutoHeal { get { return RawStaffArmorAutoHeal.HasValue ? RawStaffArmorAutoHeal.Value : 0; } }
        public double? RawStaffArmorAutoHeal { get { return GetDouble(204); } }
        public double MaxMapPinsPerArea { get { return RawMaxMapPinsPerArea.HasValue ? RawMaxMapPinsPerArea.Value : 0; } }
        public double? RawMaxMapPinsPerArea { get { return GetDouble(208); } }
        public double MaxMapPinIcons { get { return RawMaxMapPinIcons.HasValue ? RawMaxMapPinIcons.Value : 0; } }
        public double? RawMaxMapPinIcons { get { return GetDouble(212); } }
        public double WorkOrderCoinRewardMod { get { return RawWorkOrderCoinRewardMod.HasValue ? RawWorkOrderCoinRewardMod.Value : 0; } }
        public double? RawWorkOrderCoinRewardMod { get { return GetDouble(216); } }
        public double MaxActiveWorkOrders { get { return RawMaxActiveWorkOrders.HasValue ? RawMaxActiveWorkOrders.Value : 0; } }
        public double? RawMaxActiveWorkOrders { get { return GetDouble(220); } }
        public double PlayerOrdersMaxActive { get { return RawPlayerOrdersMaxActive.HasValue ? RawPlayerOrdersMaxActive.Value : 0; } }
        public double? RawPlayerOrdersMaxActive { get { return GetDouble(224); } }
        public double ShopInventorySizeDelta { get { return RawShopInventorySizeDelta.HasValue ? RawShopInventorySizeDelta.Value : 0; } }
        public double? RawShopInventorySizeDelta { get { return GetDouble(228); } }
        public double MailShopNumFree { get { return RawMailShopNumFree.HasValue ? RawMailShopNumFree.Value : 0; } }
        public double? RawMailShopNumFree { get { return GetDouble(232); } }
        public double ShopHiringMaxPrepDays { get { return RawShopHiringMaxPrepDays.HasValue ? RawShopHiringMaxPrepDays.Value : 0; } }
        public double? RawShopHiringMaxPrepDays { get { return GetDouble(236); } }
        public double ShopLogDaysKept { get { return RawShopLogDaysKept.HasValue ? RawShopLogDaysKept.Value : 0; } }
        public double? RawShopLogDaysKept { get { return GetDouble(240); } }
        public double ShopHiringNumFree { get { return RawShopHiringNumFree.HasValue ? RawShopHiringNumFree.Value : 0; } }
        public double? RawShopHiringNumFree { get { return GetDouble(244); } }
        public double CriticalHitDamage { get { return RawCriticalHitDamage.HasValue ? RawCriticalHitDamage.Value : 0; } }
        public double? RawCriticalHitDamage { get { return GetDouble(248); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(252, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public List<int> VulnerabilityList { get { return GetIntList(256, ref _VulnerabilityList); } } private List<int> _VulnerabilityList;
        public List<int> MitigationList { get { return GetIntList(260, ref _MitigationList); } } private List<int> _MitigationList;
        public List<int> DirectModList { get { return GetIntList(264, ref _DirectModList); } } private List<int> _DirectModList;
        public List<int> IndirectModList { get { return GetIntList(268, ref _IndirectModList); } } private List<int> _IndirectModList;

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
        }; } }

        #region Json Reconstruction
        public const int DamageMultiplier = 10000;

        public override void ListObjectContent(JsonGenerator generator, string ParserKey)
        {
            string RawDamageType;
            DamageType ParsedDamageType;

            if (IsDamageTypeEntry(ParserKey, "VULN_", null, out RawDamageType, out ParsedDamageType, null))
            {
                for (int i = 0; i * 2 < VulnerabilityList.Count; i++)
                    if ((DamageType)VulnerabilityList[i * 2] == ParsedDamageType)
                    {
                        generator.AddDouble("VULN_" + RawDamageType.ToUpper(), (double)VulnerabilityList[i * 2 + 1] / DamageMultiplier);
                        break;
                    }
                return;
            }

            else if (IsDamageTypeEntry(ParserKey, "MITIGATION_", null, out RawDamageType, out ParsedDamageType, null))
            {
                for (int i = 0; i * 2 < MitigationList.Count; i++)
                    if ((DamageType)MitigationList[i * 2] == ParsedDamageType)
                    {
                        generator.AddDouble("MITIGATION_" + RawDamageType.ToUpper(), (double)MitigationList[i * 2 + 1] / DamageMultiplier);
                        break;
                    }
                return;
            }

            else if (IsDamageTypeEntry(ParserKey, "MOD_", "_INDIRECT", out RawDamageType, out ParsedDamageType, null))
            {
                for (int i = 0; i * 2 < IndirectModList.Count; i++)
                    if ((DamageType)IndirectModList[i * 2] == ParsedDamageType)
                    {
                        generator.AddDouble("MOD_" + RawDamageType.ToUpper() + "_INDIRECT", (double)IndirectModList[i * 2 + 1] / DamageMultiplier);
                        break;
                    }
                return;
            }

            else if (IsDamageTypeEntry(ParserKey, "MOD_", "_DIRECT", out RawDamageType, out ParsedDamageType, null))
            {
                for (int i = 0; i * 2 < DirectModList.Count; i++)
                    if ((DamageType)DirectModList[i * 2] == ParsedDamageType)
                    {
                        generator.AddDouble("MOD_" + RawDamageType.ToUpper() + "_DIRECT", (double)DirectModList[i * 2 + 1] / DamageMultiplier);
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
    }
}
