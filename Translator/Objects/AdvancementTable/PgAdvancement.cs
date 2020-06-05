namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgAdvancement
    {
        public Dictionary<DamageType, float> VulnerabilityTable { get; } = new Dictionary<DamageType, float>();
        public float VulnerabilityToBurst { get { return RawIgnoreChanceFear.HasValue ? RawIgnoreChanceFear.Value : 0; } }
        public float? RawVulnerabilityToBurst { get; set; }
        public Dictionary<DamageType, float> MitigationTable { get; } = new Dictionary<DamageType, float>();
        public Dictionary<DamageType, float> MitigationDirectTable { get; } = new Dictionary<DamageType, float>();
        public Dictionary<DamageType, float> MitigationIndirectTable { get; } = new Dictionary<DamageType, float>();
        public Dictionary<DamageType, float> ModDirectTable { get; } = new Dictionary<DamageType, float>();
        public Dictionary<DamageType, float> ModIndirectTable { get; } = new Dictionary<DamageType, float>();
        public Dictionary<DamageType, float> BoostIndirectTable { get; } = new Dictionary<DamageType, float>();
        public float IgnoreChanceFear { get { return RawIgnoreChanceFear.HasValue ? RawIgnoreChanceFear.Value : 0; } }
        public float? RawIgnoreChanceFear { get; set; }
        public float IgnoreChanceMez { get { return RawIgnoreChanceMez.HasValue ? RawIgnoreChanceMez.Value : 0; } }
        public float? RawIgnoreChanceMez { get; set; }
        public float IgnoreChanceKnockback { get { return RawIgnoreChanceKnockback.HasValue ? RawIgnoreChanceKnockback.Value : 0; } }
        public float? RawIgnoreChanceKnockback { get; set; }
        public float MentalDefenseRating { get { return RawMentalDefenseRating.HasValue ? RawMentalDefenseRating.Value : 0; } }
        public float? RawMentalDefenseRating { get; set; }
        public float NonCombatRegenHealthMod { get { return RawNonCombatRegenHealthMod.HasValue ? RawNonCombatRegenHealthMod.Value : 0; } }
        public float? RawNonCombatRegenHealthMod { get; set; }
        public float CombatRegenHealthMod { get { return RawCombatRegenHealthMod.HasValue ? RawCombatRegenHealthMod.Value : 0; } }
        public float? RawCombatRegenHealthMod { get; set; }
        public float CombatRegenHealthDelta { get { return RawCombatRegenHealthDelta.HasValue ? RawCombatRegenHealthDelta.Value : 0; } }
        public float? RawCombatRegenHealthDelta { get; set; }
        public float NonCombatRegenArmorMod { get { return RawNonCombatRegenArmorMod.HasValue ? RawNonCombatRegenArmorMod.Value : 0; } }
        public float? RawNonCombatRegenArmorMod { get; set; }
        public float NonCombatRegenArmorDelta { get { return RawNonCombatRegenArmorDelta.HasValue ? RawNonCombatRegenArmorDelta.Value : 0; } }
        public float? RawNonCombatRegenArmorDelta { get; set; }
        public float CombatRegenArmorMod { get { return RawCombatRegenArmorMod.HasValue ? RawCombatRegenArmorMod.Value : 0; } }
        public float? RawCombatRegenArmorMod { get; set; }
        public float NonCombatRegenPowerMod { get { return RawNonCombatRegenPowerMod.HasValue ? RawNonCombatRegenPowerMod.Value : 0; } }
        public float? RawNonCombatRegenPowerMod { get; set; }
        public float CombatRegenPowerMod { get { return RawCombatRegenPowerMod.HasValue ? RawCombatRegenPowerMod.Value : 0; } }
        public float? RawCombatRegenPowerMod { get; set; }
        public float SprintBoost { get { return RawSprintBoost.HasValue ? RawSprintBoost.Value : 0; } }
        public float? RawSprintBoost { get; set; }
        public float TauntMod { get { return RawTauntMod.HasValue ? RawTauntMod.Value : 0; } }
        public float? RawTauntMod { get; set; }
        public float EvasionChance { get { return RawEvasionChance.HasValue ? RawEvasionChance.Value : 0; } }
        public float? RawEvasionChance { get; set; }
        public float EvasionChanceProjectile { get { return RawEvasionChanceProjectile.HasValue ? RawEvasionChanceProjectile.Value : 0; } }
        public float? RawEvasionChanceProjectile { get; set; }
        public float EvasionChanceMelee { get { return RawEvasionChanceMelee.HasValue ? RawEvasionChanceMelee.Value : 0; } }
        public float? RawEvasionChanceMelee { get; set; }
        public float ModCriticalHitDamageRageAttack { get { return RawModCriticalHitDamageRageAttack.HasValue ? RawModCriticalHitDamageRageAttack.Value : 0; } }
        public float? RawModCriticalHitDamageRageAttack { get; set; }
        public float BoostWerewolfMetabolismHeathRegen { get { return RawBoostWerewolfMetabolismHeathRegen.HasValue ? RawBoostWerewolfMetabolismHeathRegen.Value : 0; } }
        public float? RawBoostWerewolfMetabolismHeathRegen { get; set; }
        public float BoostWerewolfMetabolismPowerRegen { get { return RawBoostWerewolfMetabolismPowerRegen.HasValue ? RawBoostWerewolfMetabolismPowerRegen.Value : 0; } }
        public float? RawBoostWerewolfMetabolismPowerRegen { get; set; }
        public float LootBoostChanceUncommon { get { return RawLootBoostChanceUncommon.HasValue ? RawLootBoostChanceUncommon.Value : 0; } }
        public float? RawLootBoostChanceUncommon { get; set; }
        public float LootBoostChanceRare { get { return RawLootBoostChanceRare.HasValue ? RawLootBoostChanceRare.Value : 0; } }
        public float? RawLootBoostChanceRare { get; set; }
        public float LootBoostChanceExceptional { get { return RawLootBoostChanceExceptional.HasValue ? RawLootBoostChanceExceptional.Value : 0; } }
        public float? RawLootBoostChanceExceptional { get; set; }
        public float LootBoostChanceEpic { get { return RawLootBoostChanceEpic.HasValue ? RawLootBoostChanceEpic.Value : 0; } }
        public float? RawLootBoostChanceEpic { get; set; }
        public float LootBoostChanceLegendary { get { return RawLootBoostChanceLegendary.HasValue ? RawLootBoostChanceLegendary.Value : 0; } }
        public float? RawLootBoostChanceLegendary { get; set; }
        public float MaxHealth { get { return RawMaxHealth.HasValue ? RawMaxHealth.Value : 0; } }
        public float? RawMaxHealth { get; set; }
        public float MaxArmor { get { return RawMaxArmor.HasValue ? RawMaxArmor.Value : 0; } }
        public float? RawMaxArmor { get; set; }
        public float MaxRage { get { return RawMaxRage.HasValue ? RawMaxRage.Value : 0; } }
        public float? RawMaxRage { get; set; }
        public float MaxPower { get { return RawMaxPower.HasValue ? RawMaxPower.Value : 0; } }
        public float? RawMaxPower { get; set; }
        public float MaxBreath { get { return RawMaxBreath.HasValue ? RawMaxBreath.Value : 0; } }
        public float? RawMaxBreath { get; set; }
        public float BoostUniversalDirect { get { return RawBoostUniversalDirect.HasValue ? RawBoostUniversalDirect.Value : 0; } }
        public float? RawBoostUniversalDirect { get; set; }
        public float BoostAbilityRageAttack { get { return RawBoostAbilityRageAttack.HasValue ? RawBoostAbilityRageAttack.Value : 0; } }
        public float? RawBoostAbilityRageAttack { get; set; }
        public float ModAbilityRageAttack { get { return RawModAbilityRageAttack.HasValue ? RawModAbilityRageAttack.Value : 0; } }
        public float? RawModAbilityRageAttack { get; set; }
        public float MonsterCombatXpValue { get { return RawMonsterCombatXpValue.HasValue ? RawMonsterCombatXpValue.Value : 0; } }
        public float? RawMonsterCombatXpValue { get; set; }
        public float CombatRegenArmorDelta { get { return RawCombatRegenArmorDelta.HasValue ? RawCombatRegenArmorDelta.Value : 0; } }
        public float? RawCombatRegenArmorDelta { get; set; }
        public float CombatRegenPowerDelta { get { return RawCombatRegenPowerDelta.HasValue ? RawCombatRegenPowerDelta.Value : 0; } }
        public float? RawCombatRegenPowerDelta { get; set; }
        public float MaxInventorySize { get { return RawMaxInventorySize.HasValue ? RawMaxInventorySize.Value : 0; } }
        public float? RawMaxInventorySize { get; set; }
        public float MaxMetabolism { get { return RawMaxMetabolism.HasValue ? RawMaxMetabolism.Value : 0; } }
        public float? RawMaxMetabolism { get; set; }
        public float NpcModFavorFromGifts { get { return RawNpcModFavorFromGifts.HasValue ? RawNpcModFavorFromGifts.Value : 0; } }
        public float? RawNpcModFavorFromGifts { get; set; }
        public float NpcModFavorFromHangouts { get { return RawNpcModFavorFromHangouts.HasValue ? RawNpcModFavorFromHangouts.Value : 0; } }
        public float? RawNpcModFavorFromHangouts { get; set; }
        public float NpcModMaxSalesValue { get { return RawNpcModMaxSalesValue.HasValue ? RawNpcModMaxSalesValue.Value : 0; } }
        public float? RawNpcModMaxSalesValue { get; set; }
        public float NpcModTrainingCost { get { return RawNpcModTrainingCost.HasValue ? RawNpcModTrainingCost.Value : 0; } }
        public float? RawNpcModTrainingCost { get; set; }
        public int NumInventoryFolders { get { return RawNumInventoryFolders.HasValue ? RawNumInventoryFolders.Value : 0; } }
        public int? RawNumInventoryFolders { get; set; }
        public float HighCleanlinessXpEarnedMod { get { return RawHighCleanlinessXpEarnedMod.HasValue ? RawHighCleanlinessXpEarnedMod.Value : 0; } }
        public float? RawHighCleanlinessXpEarnedMod { get; set; }
        public float LowCleanlinessXpEarnedMod { get { return RawLowCleanlinessXpEarnedMod.HasValue ? RawLowCleanlinessXpEarnedMod.Value : 0; } }
        public float? RawLowCleanlinessXpEarnedMod { get; set; }
        public int ShowCleanlinessIndicators { get { return RawShowCleanlinessIndicators.HasValue ? RawShowCleanlinessIndicators.Value : 0; } }
        public int? RawShowCleanlinessIndicators { get; set; }
        public float HighCommunityXpEarnedMod { get { return RawHighCommunityXpEarnedMod.HasValue ? RawHighCommunityXpEarnedMod.Value : 0; } }
        public float? RawHighCommunityXpEarnedMod { get; set; }
        public float LowCommunityXpEarnedMod { get { return RawLowCommunityXpEarnedMod.HasValue ? RawLowCommunityXpEarnedMod.Value : 0; } }
        public float? RawLowCommunityXpEarnedMod { get; set; }
        public int ShowCommunityIndicators { get { return RawShowCommunityIndicators.HasValue ? RawShowCommunityIndicators.Value : 0; } }
        public int? RawShowCommunityIndicators { get; set; }
        public float HighPeaceablenessXpEarnedMod { get { return RawHighPeaceablenessXpEarnedMod.HasValue ? RawHighPeaceablenessXpEarnedMod.Value : 0; } }
        public float? RawHighPeaceablenessXpEarnedMod { get; set; }
        public float LowPeaceablenessXpEarnedMod { get { return RawLowPeaceablenessXpEarnedMod.HasValue ? RawLowPeaceablenessXpEarnedMod.Value : 0; } }
        public float? RawLowPeaceablenessXpEarnedMod { get; set; }
        public int ShowPeaceablenessIndicators { get { return RawShowPeaceablenessIndicators.HasValue ? RawShowPeaceablenessIndicators.Value : 0; } }
        public int? RawShowPeaceablenessIndicators { get; set; }
        public float StaffArmorAutoHeal { get { return RawStaffArmorAutoHeal.HasValue ? RawStaffArmorAutoHeal.Value : 0; } }
        public float? RawStaffArmorAutoHeal { get; set; }
        public int MaxMapPinsPerArea { get { return RawMaxMapPinsPerArea.HasValue ? RawMaxMapPinsPerArea.Value : 0; } }
        public int? RawMaxMapPinsPerArea { get; set; }
        public int MaxMapPinIcons { get { return RawMaxMapPinIcons.HasValue ? RawMaxMapPinIcons.Value : 0; } }
        public int? RawMaxMapPinIcons { get; set; }
        public float WorkOrderCoinRewardMod { get { return RawWorkOrderCoinRewardMod.HasValue ? RawWorkOrderCoinRewardMod.Value : 0; } }
        public float? RawWorkOrderCoinRewardMod { get; set; }
        public int MaxActiveWorkOrders { get { return RawMaxActiveWorkOrders.HasValue ? RawMaxActiveWorkOrders.Value : 0; } }
        public int? RawMaxActiveWorkOrders { get; set; }
        public int PlayerOrdersMaxActive { get { return RawPlayerOrdersMaxActive.HasValue ? RawPlayerOrdersMaxActive.Value : 0; } }
        public int? RawPlayerOrdersMaxActive { get; set; }
        public int ShopInventorySizeDelta { get { return RawShopInventorySizeDelta.HasValue ? RawShopInventorySizeDelta.Value : 0; } }
        public int? RawShopInventorySizeDelta { get; set; }
        public int MailShopNumFree { get { return RawMailShopNumFree.HasValue ? RawMailShopNumFree.Value : 0; } }
        public int? RawMailShopNumFree { get; set; }
        public int ShopHiringMaxPrepDays { get { return RawShopHiringMaxPrepDays.HasValue ? RawShopHiringMaxPrepDays.Value : 0; } }
        public int? RawShopHiringMaxPrepDays { get; set; }
        public int ShopLogDaysKept { get { return RawShopLogDaysKept.HasValue ? RawShopLogDaysKept.Value : 0; } }
        public int? RawShopLogDaysKept { get; set; }
        public int ShopHiringNumFree { get { return RawShopHiringNumFree.HasValue ? RawShopHiringNumFree.Value : 0; } }
        public int? RawShopHiringNumFree { get; set; }
        public float ModCriticalHitDamage { get { return RawModCriticalHitDamage.HasValue ? RawModCriticalHitDamage.Value : 0; } }
        public float? RawModCriticalHitDamage { get; set; }
        public float MonsterCritChance { get { return RawMonsterCritChance.HasValue ? RawMonsterCritChance.Value : 0; } }
        public float? RawMonsterCritChance { get; set; }
        public float AccuracyBoost { get { return RawAccuracyBoost.HasValue ? RawAccuracyBoost.Value : 0; } }
        public float? RawAccuracyBoost { get; set; }
        public int MonsterMatchOwnerSpeed { get { return RawMonsterMatchOwnerSpeed.HasValue ? RawMonsterMatchOwnerSpeed.Value : 0; } }
        public int? RawMonsterMatchOwnerSpeed { get; set; }
        public float ArmorMitigationMod { get { return RawArmorMitigationMod.HasValue ? RawArmorMitigationMod.Value : 0; } }
        public float? RawArmorMitigationMod { get; set; }
        public float AutoHealHealthMod { get { return RawAutoHealHealthMod.HasValue ? RawAutoHealHealthMod.Value : 0; } }
        public float? RawAutoHealHealthMod { get; set; }
        public float AutoHealArmorMod { get { return RawAutoHealArmorMod.HasValue ? RawAutoHealArmorMod.Value : 0; } }
        public float? RawAutoHealArmorMod { get; set; }
        public float ArmorMitigationRatio { get { return RawArmorMitigationRatio.HasValue ? RawArmorMitigationRatio.Value : 0; } }
        public float? RawArmorMitigationRatio { get; set; }
        public int ShowFairyEnergyIndicator { get { return RawShowFairyEnergyIndicator.HasValue ? RawShowFairyEnergyIndicator.Value : 0; } }
        public int? RawShowFairyEnergyIndicator { get; set; }
        public int BoostAbilityPetSpecialAttack { get { return RawBoostAbilityPetSpecialAttack.HasValue ? RawBoostAbilityPetSpecialAttack.Value : 0; } }
        public int? RawBoostAbilityPetSpecialAttack { get; set; }
        public int BoostAbilityPetSpecialTrick { get { return RawBoostAbilityPetSpecialTrick.HasValue ? RawBoostAbilityPetSpecialTrick.Value : 0; } }
        public int? RawBoostAbilityPetSpecialTrick { get; set; }
        public int BoostAbilityPetBasicAttack { get { return RawBoostAbilityPetBasicAttack.HasValue ? RawBoostAbilityPetBasicAttack.Value : 0; } }
        public int? RawBoostAbilityPetBasicAttack { get; set; }
        public float BoostAutoHealHealthSender { get { return RawBoostAutoHealHealthSender.HasValue ? RawBoostAutoHealHealthSender.Value : 0; } }
        public float? RawBoostAutoHealHealthSender { get; set; }
        public float BoostAutoHealArmorSender { get { return RawBoostAutoHealArmorSender.HasValue ? RawBoostAutoHealArmorSender.Value : 0; } }
        public float? RawBoostAutoHealArmorSender { get; set; }
    }
}
