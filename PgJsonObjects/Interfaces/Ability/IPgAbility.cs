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
        string SpecialCasterRequirementsErrorMessage { get; }
        //double ConsumedItemChance { get; }
        //double? RawConsumedItemChance { get; }
        //double ConsumedItemChanceToStickInCorpse { get; }
        //double? RawConsumedItemChanceToStickInCorpse { get; }
        //int ConsumedItemCount { get; }
        //int? RawConsumedItemCount { get; }
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
        string SelfParticle { get; } //TODO: display it
        IPgAbility SharesResetTimerWith { get; }
        IPgSkill Skill { get; }
        IPgAbilityRequirementCollection CombinedRequirementList { get; }
        string SpecialInfo { get; }
        int SpecialTargetingTypeReq { get; } //TODO: display it
        int? RawSpecialTargetingTypeReq { get; }
        TargetEffectKeyword TargetEffectKeywordReq { get; }
        AbilityTargetParticle TargetParticle { get; }
        IPgAbility UpgradeOf { get; }
        TooltipsExtraKeywords ExtraKeywordsForTooltips { get; }
        ConsumedItemCategory ConsumedItems { get; }
        IPgAbility AbilityGroup { get; }
        IPgAbilityRequirementCollection SpecialCasterRequirementList { get; }
        IPgAttributeCollection AttributesThatModAmmoConsumeChanceList { get; }
        IPgAttributeCollection AttributesThatDeltaDelayLoopTimeList { get; }
        IPgAttributeCollection AttributesThatDeltaPowerCostList { get; }
        IPgAttributeCollection AttributesThatDeltaResetTimeList { get; }
        IPgAttributeCollection AttributesThatModPowerCostList { get; }
        IPgItem ConsumedItemLink { get; }
        bool WorksWhileFalling { get; }
        bool? RawWorksWhileFalling { get; }
        bool IgnoreEffectErrors { get; }
        bool? RawIgnoreEffectErrors { get; }
        bool RawAttributesThatModAmmoConsumeChanceListIsEmpty { get; }
        bool RawAttributesThatDeltaDelayLoopTimeListIsEmpty { get; }
        bool RawAttributesThatDeltaPowerCostListIsEmpty { get; }
        bool RawAttributesThatDeltaResetTimeListIsEmpty { get; }
        bool RawAttributesThatModPowerCostListIsEmpty { get; }
        PowerSkill RawSkill { get; }
        IPgGenericSourceCollection SourceList { get; }
        IPgItem ConsumedItemDescription { get; }
        bool DelayLoopIsOnlyUsedInCombat { get; }
        bool? RawDelayLoopIsOnlyUsedInCombat { get; }
        string AmmoDescription { get; }
        double AmmoConsumeChance { get; }
        double? RawAmmoConsumeChance { get; }
        double AmmoStickChance { get; }
        double? RawAmmoStickChance { get; }

        ConsumedItem ConsumedItem { get; }
        string DigitStrippedName { get; }
        int LineIndex { get; }
        string BaseName { get; }
    }
}
