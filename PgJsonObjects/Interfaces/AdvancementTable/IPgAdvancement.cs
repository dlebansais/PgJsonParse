﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgAdvancement
    {
        int Id { get; }
        float NonCombatRegenHealthMod { get; }
        double? RawNonCombatRegenHealthMod { get; }
        float CombatRegenHealthMod { get; }
        double? RawCombatRegenHealthMod { get; }
        float CombatRegenHealthDelta { get; }
        double? RawCombatRegenHealthDelta { get; }
        float NonCombatRegenArmorMod { get; }
        double? RawNonCombatRegenArmorMod { get; }
        float NonCombatRegenArmordelta { get; }
        double? RawNonCombatRegenArmordelta { get; }
        float CombatRegenArmorMod { get; }
        double? RawCombatRegenArmorMod { get; }
        float NonCombatRegenPowerMod { get; }
        double? RawNonCombatRegenPowerMod { get; }
        float CombatRegenPowerMod { get; }
        double? RawCombatRegenPowerMod { get; }
        float NonCombatRegenRageMod { get; }
        double? RawNonCombatRegenRageMod { get; }
        float CombatRegenRageMod { get; }
        double? RawCombatRegenRageMod { get; }
        float MentalDefenseRating { get; }
        double? RawMentalDefenseRating { get; }
        float SprintBoost { get; }
        double? RawSprintBoost { get; }
        float TauntMod { get; }
        double? RawTauntMod { get; }
        float IgnoreChanceFear { get; }
        double? RawIgnoreChanceFear { get; }
        float IgnoreChanceMezz { get; }
        double? RawIgnoreChanceMezz { get; }
        float IgnoreChanceKnockback { get; }
        double? RawIgnoreChanceKnockback { get; }
        float EvasionChance { get; }
        double? RawEvasionChance { get; }
        float LootBoostChanceUncommon { get; }
        double? RawLootBoostChanceUncommon { get; }
        float LootBoostChanceRare { get; }
        double? RawLootBoostChanceRare { get; }
        float LootBoostChanceExceptional { get; }
        double? RawLootBoostChanceExceptional { get; }
        float LootBoostChanceEpic { get; }
        double? RawLootBoostChanceEpic { get; }
        float LootBoostChanceLegendary { get; }
        double? RawLootBoostChanceLegendary { get; }
        float MaxHealth { get; }
        double? RawMaxHealth { get; }
        float MaxArmor { get; }
        double? RawMaxArmor { get; }
        float MaxRage { get; }
        double? RawMaxRage { get; }
        float MaxPower { get; }
        double? RawMaxPower { get; }
        float MaxBreath { get; }
        double? RawMaxBreath { get; }
        float BoostUniversalDirect { get; }
        double? RawBoostUniversalDirect { get; }
        float BoostAbilityRageAttack { get; }
        double? RawBoostAbilityRageAttack { get; }
        float ModAbilityRageAttack { get; }
        double? RawModAbilityRageAttack { get; }
        float MonsterCombatXpValue { get; }
        double? RawMonsterCombatXpValue { get; }
        float CombatRegenArmorDelta { get; }
        double? RawCombatRegenArmorDelta { get; }
        float CombatRegenDelta { get; }
        double? RawCombatRegenDelta { get; }
        float MaxInventorySize { get; }
        double? RawMaxInventorySize { get; }
        float MaxMetabolism { get; }
        double? RawMaxMetabolism { get; }
        float NpcModFavorFromGifts { get; }
        double? RawNpcModFavorFromGifts { get; }
        float NpcModFavorFromHangouts { get; }
        double? RawNpcModFavorFromHangouts { get; }
        float NpcModMaxSalesValue { get; }
        double? RawNpcModMaxSalesValue { get; }
        float NpcModTrainingCost { get; }
        double? RawNpcModTrainingCost { get; }
        int NumInventoryFolders { get; }
        int? RawNumInventoryFolders { get; }
        float HighCleanlinessXpEarnedMod { get; }
        double? RawHighCleanlinessXpEarnedMod { get; }
        float LowCleanlinessXpEarnedMod { get; }
        double? RawLowCleanlinessXpEarnedMod { get; }
        float MaxArmorMitigationRatio { get; }
        double? RawMaxArmorMitigationRatio { get; }
        float ShowCleanlinessIndicators { get; }
        double? RawShowCleanlinessIndicators { get; }
        float HighCommunityXpEarnedMod { get; }
        double? RawHighCommunityXpEarnedMod { get; }
        float LowCommunityXpEarnedMod { get; }
        double? RawLowCommunityXpEarnedMod { get; }
        float ShowCommunityIndicators { get; }
        double? RawShowCommunityIndicators { get; }
        float HighPeaceblenessXpEarnedMod { get; }
        double? RawHighPeaceblenessXpEarnedMod { get; }
        float LowPeaceblenessXpEarnedMod { get; }
        double? RawLowPeaceblenessXpEarnedMod { get; }
        float ShowPeaceblenessIndicators { get; }
        double? RawShowPeaceblenessIndicators { get; }
        float StaffArmorAutoHeal { get; }
        double? RawStaffArmorAutoHeal { get; }
        float MaxMapPinsPerArea { get; }
        double? RawMaxMapPinsPerArea { get; }
        float MaxMapPinIcons { get; }
        double? RawMaxMapPinIcons { get; }
        float WorkOrderCoinRewardMod { get; }
        double? RawWorkOrderCoinRewardMod { get; }
        float MaxActiveWorkOrders { get; }
        double? RawMaxActiveWorkOrders { get; }
        float PlayerOrdersMaxActive { get; }
        double? RawPlayerOrdersMaxActive { get; }
        float ShopInventorySizeDelta { get; }
        double? RawShopInventorySizeDelta { get; }
        float MailShopNumFree { get; }
        double? RawMailShopNumFree { get; }
        float ShopHiringMaxPrepDays { get; }
        double? RawShopHiringMaxPrepDays { get; }
        float ShopLogDaysKept { get; }
        double? RawShopLogDaysKept { get; }
        float ShopHiringNumFree { get; }
        double? RawShopHiringNumFree { get; }
        float CriticalHitDamage { get; }
        double? RawCriticalHitDamage { get; }
        float EvasionChanceProjectile { get; }
        double? RawEvasionChanceProjectile { get; }
        float EvasionChanceMelee { get; }
        double? RawEvasionChanceMelee { get; }
        float ModCriticalHitDamageRageAttack { get; }
        double? RawModCriticalHitDamageRageAttack { get; }
        float BoostWerewolfMetabolismHeathRegen { get; }
        double? RawBoostWerewolfMetabolismHeathRegen { get; }
        float BoostWerewolfMetabolismPowerRegen { get; }
        double? RawBoostWerewolfMetabolismPowerRegen { get; }

        List<string> VulnerabilityList { get; }
        List<string> MitigationList { get; }
        List<string> DirectModList { get; }
        List<string> IndirectModList { get; }
    }
}
