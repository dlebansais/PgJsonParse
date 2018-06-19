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

        public double NonCombatRegenHealthMod { get { return RawNonCombatRegenHealthMod.HasValue ? RawNonCombatRegenHealthMod.Value : 0; } }
        public double? RawNonCombatRegenHealthMod { get { return GetDouble(0); } }
        public double CombatRegenHealthMod { get { return RawCombatRegenHealthMod.HasValue ? RawCombatRegenHealthMod.Value : 0; } }
        public double? RawCombatRegenHealthMod { get { return GetDouble(4); } }
        public double CombatRegenHealthDelta { get { return RawCombatRegenHealthDelta.HasValue ? RawCombatRegenHealthDelta.Value : 0; } }
        public double? RawCombatRegenHealthDelta { get { return GetDouble(8); } }
        public double NonCombatRegenArmorMod { get { return RawNonCombatRegenArmorMod.HasValue ? RawNonCombatRegenArmorMod.Value : 0; } }
        public double? RawNonCombatRegenArmorMod { get { return GetDouble(12); } }
        public double NonCombatRegenArmordelta { get { return RawNonCombatRegenArmordelta.HasValue ? RawNonCombatRegenArmordelta.Value : 0; } }
        public double? RawNonCombatRegenArmordelta { get { return GetDouble(16); } }
        public double CombatRegenArmorMod { get { return RawCombatRegenArmorMod.HasValue ? RawCombatRegenArmorMod.Value : 0; } }
        public double? RawCombatRegenArmorMod { get { return GetDouble(20); } }
        public double NonCombatRegenPowerMod { get { return RawNonCombatRegenPowerMod.HasValue ? RawNonCombatRegenPowerMod.Value : 0; } }
        public double? RawNonCombatRegenPowerMod { get { return GetDouble(24); } }
        public double CombatRegenPowerMod { get { return RawCombatRegenPowerMod.HasValue ? RawCombatRegenPowerMod.Value : 0; } }
        public double? RawCombatRegenPowerMod { get { return GetDouble(28); } }
        public double NonCombatRegenRageMod { get { return RawNonCombatRegenRageMod.HasValue ? RawNonCombatRegenRageMod.Value : 0; } }
        public double? RawNonCombatRegenRageMod { get { return GetDouble(32); } }
        public double CombatRegenRageMod { get { return RawCombatRegenRageMod.HasValue ? RawCombatRegenRageMod.Value : 0; } }
        public double? RawCombatRegenRageMod { get { return GetDouble(36); } }
        public double MentalDefenseRating { get { return RawMentalDefenseRating.HasValue ? RawMentalDefenseRating.Value : 0; } }
        public double? RawMentalDefenseRating { get { return GetDouble(40); } }
        public double SprintBoost { get { return RawSprintBoost.HasValue ? RawSprintBoost.Value : 0; } }
        public double? RawSprintBoost { get { return GetDouble(44); } }
        public double TauntMod { get { return RawTauntMod.HasValue ? RawTauntMod.Value : 0; } }
        public double? RawTauntMod { get { return GetDouble(48); } }
        public double IgnoreChanceFear { get { return RawIgnoreChanceFear.HasValue ? RawIgnoreChanceFear.Value : 0; } }
        public double? RawIgnoreChanceFear { get { return GetDouble(52); } }
        public double IgnoreChanceMezz { get { return RawIgnoreChanceMezz.HasValue ? RawIgnoreChanceMezz.Value : 0; } }
        public double? RawIgnoreChanceMezz { get { return GetDouble(56); } }
        public double IgnoreChanceKnockback { get { return RawIgnoreChanceKnockback.HasValue ? RawIgnoreChanceKnockback.Value : 0; } }
        public double? RawIgnoreChanceKnockback { get { return GetDouble(60); } }
        public double EvasionChance { get { return RawEvasionChance.HasValue ? RawEvasionChance.Value : 0; } }
        public double? RawEvasionChance { get { return GetDouble(64); } }
        public double LootBoostChanceUncommon { get { return RawLootBoostChanceUncommon.HasValue ? RawLootBoostChanceUncommon.Value : 0; } }
        public double? RawLootBoostChanceUncommon { get { return GetDouble(68); } }
        public double LootBoostChanceRare { get { return RawLootBoostChanceRare.HasValue ? RawLootBoostChanceRare.Value : 0; } }
        public double? RawLootBoostChanceRare { get { return GetDouble(72); } }
        public double LootBoostChanceExceptional { get { return RawLootBoostChanceExceptional.HasValue ? RawLootBoostChanceExceptional.Value : 0; } }
        public double? RawLootBoostChanceExceptional { get { return GetDouble(76); } }
        public double LootBoostChanceEpic { get { return RawLootBoostChanceEpic.HasValue ? RawLootBoostChanceEpic.Value : 0; } }
        public double? RawLootBoostChanceEpic { get { return GetDouble(80); } }
        public double LootBoostChanceLegendary { get { return RawLootBoostChanceLegendary.HasValue ? RawLootBoostChanceLegendary.Value : 0; } }
        public double? RawLootBoostChanceLegendary { get { return GetDouble(84); } }
        public double MaxHealth { get { return RawMaxHealth.HasValue ? RawMaxHealth.Value : 0; } }
        public double? RawMaxHealth { get { return GetDouble(88); } }
        public double MaxArmor { get { return RawMaxArmor.HasValue ? RawMaxArmor.Value : 0; } }
        public double? RawMaxArmor { get { return GetDouble(92); } }
        public double MaxRage { get { return RawMaxRage.HasValue ? RawMaxRage.Value : 0; } }
        public double? RawMaxRage { get { return GetDouble(96); } }
        public double MaxPower { get { return RawMaxPower.HasValue ? RawMaxPower.Value : 0; } }
        public double? RawMaxPower { get { return GetDouble(100); } }
        public double MaxBreath { get { return RawMaxBreath.HasValue ? RawMaxBreath.Value : 0; } }
        public double? RawMaxBreath { get { return GetDouble(104); } }
        public double BoostUniversalDirect { get { return RawBoostUniversalDirect.HasValue ? RawBoostUniversalDirect.Value : 0; } }
        public double? RawBoostUniversalDirect { get { return GetDouble(108); } }
        public double BoostAbilityRageAttack { get { return RawBoostAbilityRageAttack.HasValue ? RawBoostAbilityRageAttack.Value : 0; } }
        public double? RawBoostAbilityRageAttack { get { return GetDouble(112); } }
        public double ModAbilityRageAttack { get { return RawModAbilityRageAttack.HasValue ? RawModAbilityRageAttack.Value : 0; } }
        public double? RawModAbilityRageAttack { get { return GetDouble(116); } }
        public double MonsterCombatXpValue { get { return RawMonsterCombatXpValue.HasValue ? RawMonsterCombatXpValue.Value : 0; } }
        public double? RawMonsterCombatXpValue { get { return GetDouble(120); } }
        public double CombatRegenArmorDelta { get { return RawCombatRegenArmorDelta.HasValue ? RawCombatRegenArmorDelta.Value : 0; } }
        public double? RawCombatRegenArmorDelta { get { return GetDouble(124); } }
        public double CombatRegenDelta { get { return RawCombatRegenDelta.HasValue ? RawCombatRegenDelta.Value : 0; } }
        public double? RawCombatRegenDelta { get { return GetDouble(128); } }
        public double MaxInventorySize { get { return RawMaxInventorySize.HasValue ? RawMaxInventorySize.Value : 0; } }
        public double? RawMaxInventorySize { get { return GetDouble(132); } }
        public double MaxMetabolism { get { return RawMaxMetabolism.HasValue ? RawMaxMetabolism.Value : 0; } }
        public double? RawMaxMetabolism { get { return GetDouble(136); } }
        public double NpcModFavorFromGifts { get { return RawNpcModFavorFromGifts.HasValue ? RawNpcModFavorFromGifts.Value : 0; } }
        public double? RawNpcModFavorFromGifts { get { return GetDouble(140); } }
        public double NpcModFavorFromHangouts { get { return RawNpcModFavorFromHangouts.HasValue ? RawNpcModFavorFromHangouts.Value : 0; } }
        public double? RawNpcModFavorFromHangouts { get { return GetDouble(144); } }
        public double NpcModMaxSalesValue { get { return RawNpcModMaxSalesValue.HasValue ? RawNpcModMaxSalesValue.Value : 0; } }
        public double? RawNpcModMaxSalesValue { get { return GetDouble(148); } }
        public double NpcModTrainingCost { get { return RawNpcModTrainingCost.HasValue ? RawNpcModTrainingCost.Value : 0; } }
        public double? RawNpcModTrainingCost { get { return GetDouble(152); } }
        public int NumInventoryFolders { get { return RawNumInventoryFolders.HasValue ? RawNumInventoryFolders.Value : 0; } }
        public int? RawNumInventoryFolders { get { return GetInt(156); } }
        public double HighCleanlinessXpEarnedMod { get { return RawHighCleanlinessXpEarnedMod.HasValue ? RawHighCleanlinessXpEarnedMod.Value : 0; } }
        public double? RawHighCleanlinessXpEarnedMod { get { return GetDouble(160); } }
        public double LowCleanlinessXpEarnedMod { get { return RawLowCleanlinessXpEarnedMod.HasValue ? RawLowCleanlinessXpEarnedMod.Value : 0; } }
        public double? RawLowCleanlinessXpEarnedMod { get { return GetDouble(164); } }
        public double MaxArmorMitigationRatio { get { return RawMaxArmorMitigationRatio.HasValue ? RawMaxArmorMitigationRatio.Value : 0; } }
        public double? RawMaxArmorMitigationRatio { get { return GetDouble(168); } }
        public double ShowCleanlinessIndicators { get { return RawShowCleanlinessIndicators.HasValue ? RawShowCleanlinessIndicators.Value : 0; } }
        public double? RawShowCleanlinessIndicators { get { return GetDouble(172); } }
        public double HighCommunityXpEarnedMod { get { return RawHighCommunityXpEarnedMod.HasValue ? RawHighCommunityXpEarnedMod.Value : 0; } }
        public double? RawHighCommunityXpEarnedMod { get { return GetDouble(176); } }
        public double LowCommunityXpEarnedMod { get { return RawLowCommunityXpEarnedMod.HasValue ? RawLowCommunityXpEarnedMod.Value : 0; } }
        public double? RawLowCommunityXpEarnedMod { get { return GetDouble(180); } }
        public double ShowCommunityIndicators { get { return RawShowCommunityIndicators.HasValue ? RawShowCommunityIndicators.Value : 0; } }
        public double? RawShowCommunityIndicators { get { return GetDouble(184); } }
        public double HighPeaceblenessXpEarnedMod { get { return RawHighPeaceblenessXpEarnedMod.HasValue ? RawHighPeaceblenessXpEarnedMod.Value : 0; } }
        public double? RawHighPeaceblenessXpEarnedMod { get { return GetDouble(188); } }
        public double LowPeaceblenessXpEarnedMod { get { return RawLowPeaceblenessXpEarnedMod.HasValue ? RawLowPeaceblenessXpEarnedMod.Value : 0; } }
        public double? RawLowPeaceblenessXpEarnedMod { get { return GetDouble(192); } }
        public double ShowPeaceblenessIndicators { get { return RawShowPeaceblenessIndicators.HasValue ? RawShowPeaceblenessIndicators.Value : 0; } }
        public double? RawShowPeaceblenessIndicators { get { return GetDouble(196); } }
        public double StaffArmorAutoHeal { get { return RawStaffArmorAutoHeal.HasValue ? RawStaffArmorAutoHeal.Value : 0; } }
        public double? RawStaffArmorAutoHeal { get { return GetDouble(200); } }
        public double MaxMapPinsPerArea { get { return RawMaxMapPinsPerArea.HasValue ? RawMaxMapPinsPerArea.Value : 0; } }
        public double? RawMaxMapPinsPerArea { get { return GetDouble(204); } }
        public double MaxMapPinIcons { get { return RawMaxMapPinIcons.HasValue ? RawMaxMapPinIcons.Value : 0; } }
        public double? RawMaxMapPinIcons { get { return GetDouble(208); } }
        public double WorkOrderCoinRewardMod { get { return RawWorkOrderCoinRewardMod.HasValue ? RawWorkOrderCoinRewardMod.Value : 0; } }
        public double? RawWorkOrderCoinRewardMod { get { return GetDouble(212); } }
        public double MaxActiveWorkOrders { get { return RawMaxActiveWorkOrders.HasValue ? RawMaxActiveWorkOrders.Value : 0; } }
        public double? RawMaxActiveWorkOrders { get { return GetDouble(216); } }
        public double PlayerOrdersMaxActive { get { return RawPlayerOrdersMaxActive.HasValue ? RawPlayerOrdersMaxActive.Value : 0; } }
        public double? RawPlayerOrdersMaxActive { get { return GetDouble(220); } }
        public double ShopInventorySizeDelta { get { return RawShopInventorySizeDelta.HasValue ? RawShopInventorySizeDelta.Value : 0; } }
        public double? RawShopInventorySizeDelta { get { return GetDouble(224); } }
        public double MailShopNumFree { get { return RawMailShopNumFree.HasValue ? RawMailShopNumFree.Value : 0; } }
        public double? RawMailShopNumFree { get { return GetDouble(228); } }
        public double ShopHiringMaxPrepDays { get { return RawShopHiringMaxPrepDays.HasValue ? RawShopHiringMaxPrepDays.Value : 0; } }
        public double? RawShopHiringMaxPrepDays { get { return GetDouble(232); } }
        public double ShopLogDaysKept { get { return RawShopLogDaysKept.HasValue ? RawShopLogDaysKept.Value : 0; } }
        public double? RawShopLogDaysKept { get { return GetDouble(236); } }
        public double ShopHiringNumFree { get { return RawShopHiringNumFree.HasValue ? RawShopHiringNumFree.Value : 0; } }
        public double? RawShopHiringNumFree { get { return GetDouble(240); } }
        public double CriticalHitDamage { get { return RawCriticalHitDamage.HasValue ? RawCriticalHitDamage.Value : 0; } }
        public double? RawCriticalHitDamage { get { return GetDouble(244); } }
    }
}
