using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgAbility : IJsonKey, IObjectContentGenerator, IBackLinkable
    {
        AbilityAnimation Animation { get; }
        bool CanBeOnSidebar { get; }
        bool? RawCanBeOnSidebar { get; }
        bool CanSuppressMonsterShout { get; }
        bool? RawCanSuppressMonsterShout { get; }
        bool CanTargetUntargetableEnemies { get; }
        bool? RawCanTargetUntargetableEnemies { get; }
        bool DelayLoopIsAbortedIfAttacked { get; }
        bool? RawDelayLoopIsAbortedIfAttacked { get; }
        bool InternalAbility { get; }
        bool? RawInternalAbility { get; }
        bool IsHarmless { get; }
        bool? RawIsHarmless { get; }
        bool WorksInCombat { get; }
        bool? RawWorksInCombat { get; }
        bool WorksUnderwater { get; }
        bool? RawWorksUnderwater { get; }
        List<Deaths> CausesOfDeathList { get; }
        IPgRecipeCostCollection CostList { get; }
        int CombatRefreshBaseAmount { get; }
        int? RawCombatRefreshBaseAmount { get; }
        PowerSkill CompatibleSkill { get; }
        DamageType DamageType { get; }
        string RawSpecialCasterRequirementsErrorMessage { get; }
        double ConsumedItemChance { get; }
        double? RawConsumedItemChance { get; }
        double ConsumedItemChanceToStickInCorpse { get; }
        double? RawConsumedItemChanceToStickInCorpse { get; }
        int ConsumedItemCount { get; }
        int? RawConsumedItemCount { get; }
        string DelayLoopMessage { get; }
        double DelayLoopTime { get; }
        double? RawDelayLoopTime { get; }
        string Description { get; }
        AbilityIndicatingEnabled EffectKeywordsIndicatingEnabled { get; }
        AbilityPetType PetTypeTagReq { get; }
        int IconId { get; }
        int? RawIconId { get; }
        string InternalName { get; }
        string ItemKeywordReqErrorMessage { get; }
        List<AbilityItemKeyword> ItemKeywordReqList { get; }
        List<AbilityKeyword> KeywordList { get; }
        int Level { get; }
        int? RawLevel { get; }
        string Name { get; }
        int PetTypeTagReqMax { get; }
        int? RawPetTypeTagReqMax { get; }
        IPgAbility Prerequisite { get; }
        AbilityProjectile Projectile { get; }
        AbilityTarget Target { get; }
        IPgAbilityPvX PvE { get; }
        IPgAbilityPvX PvP { get; }
        double ResetTime { get; }
        double? RawResetTime { get; }
        string SelfParticle { get; }
        IPgAbility SharesResetTimerWith { get; }
        IPgSkill Skill { get; }
        IPgAbilityRequirementCollection CombinedRequirementList { get; }
        string SpecialInfo { get; }
        int SpecialTargetingTypeReq { get; }
        int? RawSpecialTargetingTypeReq { get; }
        TargetEffectKeyword TargetEffectKeywordReq { get; }
        AbilityTargetParticle TargetParticle { get; }
        IPgAbility UpgradeOf { get; }
        TooltipsExtraKeywords ExtraKeywordsForTooltips { get; }
        ConsumedItems ConsumedItems { get; }
        IPgAbility AbilityGroup { get; }
        IPgAbilityRequirementCollection SpecialCasterRequirementList { get; }
        IPgAttributeCollection AttributesThatDeltaAmmoStickChanceList { get; }
        IPgAttributeCollection AttributesThatDeltaDelayLoopTimeList { get; }
        IPgAttributeCollection AttributesThatDeltaPowerCostList { get; }
        IPgAttributeCollection AttributesThatDeltaResetTimeList { get; }
        IPgAttributeCollection AttributesThatModPowerCostList { get; }
        IPgItem ConsumedItemLink { get; }
        bool WorksWhileFalling { get; }
        bool? RawWorksWhileFalling { get; }
        bool IgnoreEffectErrors { get; }
        bool? RawIgnoreEffectErrors { get; }
        bool RawAttributesThatDeltaAmmoStickChanceListIsEmpty { get; }
        bool RawAttributesThatDeltaDelayLoopTimeListIsEmpty { get; }
        bool RawAttributesThatDeltaPowerCostListIsEmpty { get; }
        bool RawAttributesThatDeltaResetTimeListIsEmpty { get; }
        bool RawAttributesThatModPowerCostListIsEmpty { get; }
        PowerSkill RawSkill { get; }
        IPgGenericSourceCollection SourceList { get; }

        ConsumedItem ConsumedItem { get; }
    }
}
