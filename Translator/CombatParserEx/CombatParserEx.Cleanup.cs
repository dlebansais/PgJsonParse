namespace TranslatorCombatParserEx;

using System.Collections.Generic;
using System.Diagnostics;
using PgObjects;
using Translator;

internal partial class CombatParserEx
{
    private List<string> HandledKeys = new();

    public void Cleanup(string powerKey, PgCombatModEx pgCombatModEx)
    {
        if (HandledKeys.Contains(powerKey))
            return;
        HandledKeys.Add(powerKey);

        MergeSelfAndAllies(pgCombatModEx);

        for (int i = 0; i < pgCombatModEx.PermanentEffects.Count; i++)
            CleanupPermanentEffects(pgCombatModEx, i);

        CombatKeywordEx Buff = CombatKeywordEx.Internal_None;
        for (int i = 0; i < pgCombatModEx.DynamicEffects.Count; i++)
            CleanupDynamicEffects(pgCombatModEx, i, ref Buff);
    }

    private void MergeSelfAndAllies(PgCombatModEx pgCombatModEx)
    {
        for (int i = 1; i < pgCombatModEx.DynamicEffects.Count; i++)
        {
            PgCombatModEffectEx DynamicEffect1 = pgCombatModEx.DynamicEffects[i - 1];
            PgCombatModEffectEx DynamicEffect2 = pgCombatModEx.DynamicEffects[i];

            if (DynamicEffect1.Target == CombatTarget.Self && DynamicEffect2.Target == CombatTarget.Allies &&
                DynamicEffect1 with { Target = CombatTarget.Internal_None } == DynamicEffect2 with { Target = CombatTarget.Internal_None })
            {
                pgCombatModEx.DynamicEffects[i - 1] = DynamicEffect1 with { Target = CombatTarget.SelfAndAllies };
                pgCombatModEx.DynamicEffects.RemoveAt(i);
            }
        }
    }

    private void CleanupPermanentEffects(PgCombatModEx pgCombatModEx, int index)
    {
        PgPermanentModEffectEx PermanentEffect = pgCombatModEx.PermanentEffects[index];

        switch (PermanentEffect.Keyword)
        {
            default:
                break;
        }
    }

    private void CleanupDynamicEffects(PgCombatModEx pgCombatModEx, int index, ref CombatKeywordEx buff)
    {
        PgCombatModEffectEx DynamicEffect = pgCombatModEx.DynamicEffects[index];

        if (DynamicEffect.Keyword == CombatKeywordEx.GiveBuff ||
            DynamicEffect.Keyword == CombatKeywordEx.GiveBuffConditionalAttack ||
            DynamicEffect.Keyword == CombatKeywordEx.GiveBuffEachAttack ||
            DynamicEffect.Keyword == CombatKeywordEx.GiveBuffOneAttack ||
            DynamicEffect.Keyword == CombatKeywordEx.GiveBuffOneHit ||
            DynamicEffect.Keyword == CombatKeywordEx.GiveBuffOneRageAttack ||
            DynamicEffect.Keyword == CombatKeywordEx.GiveBuffOneUse)
            buff = DynamicEffect.Keyword;

        if ((DynamicEffect.ConditionList.Contains(CombatCondition.AbilityTriggered) || DynamicEffect.ConditionList.Contains(CombatCondition.AbilityNotTriggered)) &&
            DynamicEffect.AbilityList.Count > 0 &&
            DynamicEffect.ConditionAbilityList.Count == 0)
        {
            DynamicEffect.ConditionAbilityList.AddRange(DynamicEffect.AbilityList);
            DynamicEffect.AbilityList.Clear();
        }

        Debug.Assert(!ContainsAbilityCondition(DynamicEffect.ConditionList) || DynamicEffect.ConditionAbilityList.Count > 0);
        Debug.Assert(ContainsAbilityCondition(DynamicEffect.ConditionList) || DynamicEffect.ConditionAbilityList.Count == 0);

        switch (DynamicEffect.Keyword)
        {
            case CombatKeywordEx.RestoreHealth:
                CleanupDynamicEffectRestoreHealth(pgCombatModEx, index, buff, DynamicEffect);
                break;
            default:
                break;
        }
    }

    private static bool ContainsAbilityCondition(PgCombatConditionCollectionEx conditionList)
    {
        return conditionList.Contains(CombatCondition.TargetOfAbility) ||
               conditionList.Contains(CombatCondition.WhilePlayingSong) ||
               conditionList.Contains(CombatCondition.StandingSomewhere) ||
               conditionList.Contains(CombatCondition.AbilityTriggered) ||
               conditionList.Contains(CombatCondition.AbilityNotTriggered);
    }

    private AbilitySetDescriptor GetAbilities(List<AbilityKeyword> abilityList)
    {
        AbilitySet AbilitySet = GetAbilitySet(abilityList);

        if (AbilitySetCache.TryGetValue(AbilitySet, out AbilitySetDescriptor? CachedDescriptor))
        {
            return CachedDescriptor;
        }

        List<PgAbility> AbilityList = new();

        foreach (AbilityKeyword Keyword in abilityList)
        {
            foreach (string Key in AbilityObjectKeyList)
            {
                PgAbility Ability = AbilityFromKey(Key);

                if (Ability.InternalName == "EventBossClaudiaSummon")
                    continue;

                if (Ability.KeywordList.Contains(Keyword) && !AbilityList.Contains(Ability))
                {
                    AbilityList.Add(Ability);
                }
            }
        }

        AbilityTarget Target = AbilityTarget.Internal_None;

        foreach (PgAbility Ability in AbilityList)
        {
            if (Ability.Name.StartsWith("Cabal"))
            {
            }

            Debug.Assert(Target == AbilityTarget.Internal_None ||
                         Target == Ability.Target ||
                         Ability.KeywordList.Contains(AbilityKeyword.MinorHealAttack) ||
                         Ability.KeywordList.Contains(AbilityKeyword.SurvivalUtility) ||
                         ((Target == AbilityTarget.Self || Target == AbilityTarget.Ally || Target == AbilityTarget.AllyOrSelf || Target == Internal_MixedSelfAlly) && 
                            (Ability.Target == AbilityTarget.Self || Ability.Target == AbilityTarget.Ally || Ability.Target == AbilityTarget.AllyOrSelf || Ability.Target == AbilityTarget.PermanentPet)));

            if (Target != Internal_MixedSelfAlly)
            {
                if (Ability.Target == AbilityTarget.Self ||
                    Ability.Target == AbilityTarget.Ally ||
                    Ability.Target == AbilityTarget.AllyOrSelf ||
                    Ability.Target == AbilityTarget.PermanentPet ||
                    Ability.KeywordList.Contains(AbilityKeyword.SurvivalUtility) ||
                    Ability.KeywordList.Contains(AbilityKeyword.MinorHealAttack))
                {
                    if (Target == AbilityTarget.Internal_None)
                        Target = Ability.Target;
                    else if (Target != Ability.Target)
                        Target = Internal_MixedSelfAlly;
                }
                else
                    Target = Ability.Target;
            }
        }

        Debug.Assert(Target != AbilityTarget.Internal_None);

        AbilitySetDescriptor Result = new()
        {
            AbilityList = AbilityList,
            Target = Target
        };

        AbilitySetCache.Add(AbilitySet, Result);

        return Result;
    }

    private const AbilityTarget Internal_MixedSelfAlly = (AbilityTarget)0xFFFF;

    private Dictionary<AbilitySet, AbilitySetDescriptor> AbilitySetCache = new();

    private AbilitySet GetAbilitySet(List<AbilityKeyword> abilityList)
    {
        Debug.Assert(abilityList.Count > 0 && abilityList.Count <= 3);

        return abilityList.Count == 1
               ? new AbilitySet() { Ability0 = abilityList[0] }
               : abilityList.Count == 2
               ? new AbilitySet() { Ability0 = abilityList[0], Ability1 = abilityList[1] }
               : new AbilitySet() { Ability0 = abilityList[0], Ability1 = abilityList[1], Ability2 = abilityList[2] };
    }

    private static bool IsBeneficial(AbilityTarget target)
    {
        return target is AbilityTarget.Self
                      or AbilityTarget.AllyOrSelf
                      or AbilityTarget.Ally
                      or AbilityTarget.DeadAlly
                      or AbilityTarget.Pet
                      or AbilityTarget.PermanentPet
                      or AbilityTarget.Corpse
                      or Internal_MixedSelfAlly;
    }

    private static bool IsBeneficial(CombatTarget target)
    {
        return target is not CombatTarget.Internal_None;
    }

    private static bool IsEqual(CombatTarget combatTarget, AbilityTarget abilityTarget)
    {
        return combatTarget == CombatTarget.Self && abilityTarget == AbilityTarget.Self;
    }

    private void AssertDynamicFields(PgCombatModEffectEx dynamicEffect, CombatKeywordEx keyword, DynamicFields fields)
    {
        Debug.Assert(dynamicEffect.Keyword == keyword);
        Debug.Assert(fields.HasFlag(DynamicFields.AbilityList) || dynamicEffect.AbilityList.Count == 0);
        Debug.Assert(fields.HasFlag(DynamicFields.Data) || fields.HasFlag(DynamicFields.DataValue) || fields.HasFlag(DynamicFields.DataValuePositive) || float.IsNaN(dynamicEffect.Data.Value));
        Debug.Assert(fields.HasFlag(DynamicFields.Data) || !fields.HasFlag(DynamicFields.DataValue) || fields.HasFlag(DynamicFields.DataValuePositive) || dynamicEffect.Data.Value != 0);
        Debug.Assert(fields.HasFlag(DynamicFields.Data) || fields.HasFlag(DynamicFields.DataValue) || !fields.HasFlag(DynamicFields.DataValuePositive) || dynamicEffect.Data.Value > 0);
        Debug.Assert(fields.HasFlag(DynamicFields.Data) || fields.HasFlag(DynamicFields.DataPercent) || !dynamicEffect.Data.IsPercent);
        Debug.Assert(fields.HasFlag(DynamicFields.DamageType) || dynamicEffect.DamageType == GameDamageType.Internal_None);
        Debug.Assert(fields.HasFlag(DynamicFields.DamageCategory) || dynamicEffect.DamageCategory == GameDamageCategory.Internal_None);
        Debug.Assert(fields.HasFlag(DynamicFields.CombatSkill) || dynamicEffect.CombatSkill == GameCombatSkill.Internal_None);
        Debug.Assert(fields.HasFlag(DynamicFields.RandomChance) || float.IsNaN(dynamicEffect.RandomChance));
        Debug.Assert(fields.HasFlag(DynamicFields.DelayInSeconds) || float.IsNaN(dynamicEffect.DelayInSeconds));
        Debug.Assert(fields.HasFlag(DynamicFields.DurationInSeconds) || float.IsNaN(dynamicEffect.DurationInSeconds));
        Debug.Assert(fields.HasFlag(DynamicFields.RecurringDelay) || float.IsNaN(dynamicEffect.RecurringDelay));
        Debug.Assert(fields.HasFlag(DynamicFields.Target) || dynamicEffect.Target == CombatTarget.Internal_None);
        Debug.Assert(fields.HasFlag(DynamicFields.TargetRange) || float.IsNaN(dynamicEffect.TargetRange));
        Debug.Assert(fields.HasFlag(DynamicFields.TargetAbilityList) || dynamicEffect.TargetAbilityList.Count == 0);
        Debug.Assert(fields.HasFlag(DynamicFields.ConditionList) || dynamicEffect.ConditionList.Count == 0);
        Debug.Assert(fields.HasFlag(DynamicFields.ConditionAbilityList) || dynamicEffect.ConditionAbilityList.Count == 0);
        Debug.Assert(fields.HasFlag(DynamicFields.ConditionValue) || float.IsNaN(dynamicEffect.ConditionValue));
        Debug.Assert(fields.HasFlag(DynamicFields.ConditionPercentage) || float.IsNaN(dynamicEffect.ConditionPercentage));
        Debug.Assert(fields.HasFlag(DynamicFields.IsEveryOtherUse) || !dynamicEffect.IsEveryOtherUse);
    }

    private void CleanupDynamicEffectRestoreHealth(PgCombatModEx pgCombatModEx, int index, CombatKeywordEx buff, PgCombatModEffectEx dynamicEffect)
    {
        AssertDynamicFields(dynamicEffect, CombatKeywordEx.RestoreHealth,
                            DynamicFields.AbilityList |
                            DynamicFields.DataValuePositive |
                            DynamicFields.RandomChance |
                            DynamicFields.DelayInSeconds |
                            DynamicFields.RecurringDelay |
                            DynamicFields.Target |
                            DynamicFields.TargetRange |
                            DynamicFields.ConditionList |
                            DynamicFields.ConditionAbilityList |
                            DynamicFields.ConditionValue |
                            DynamicFields.ConditionPercentage);
        Debug.Assert(float.IsNaN(dynamicEffect.DelayInSeconds) || float.IsNaN(dynamicEffect.RecurringDelay));

        if (buff != CombatKeywordEx.Internal_None)
        {
            Debug.Assert(buff == CombatKeywordEx.GiveBuffOneHit ||
                         buff == CombatKeywordEx.GiveBuffOneAttack ||
                         (buff == CombatKeywordEx.GiveBuff &&
                            (dynamicEffect.AbilityList.Count > 0 ||
                             dynamicEffect.ConditionList.Contains(CombatCondition.TargetOfAbility)) ||
                             (!float.IsNaN(dynamicEffect.RecurringDelay) && dynamicEffect.AbilityList.Count == 0)) ||
                         (buff == CombatKeywordEx.GiveBuffOneUse &&
                            (dynamicEffect.AbilityList.Count > 0 ||
                             dynamicEffect.ConditionList.Contains(CombatCondition.TargetKilled) ||
                             dynamicEffect.ConditionList.Contains(CombatCondition.TargetIsKilled))));
        }

        if (dynamicEffect.AbilityList.Count > 0)
        {
            AbilitySetDescriptor Descriptor = GetAbilities(dynamicEffect.AbilityList);

            if (!IsBeneficial(dynamicEffect.Target) && !IsBeneficial(Descriptor.Target))
            {
                Debug.Assert(dynamicEffect.Target == CombatTarget.Internal_None);
                dynamicEffect = dynamicEffect with { Target = CombatTarget.Self };
                pgCombatModEx.DynamicEffects[index] = dynamicEffect;
            }

            if (dynamicEffect.Target == CombatTarget.Self && Descriptor.Target == AbilityTarget.Self)
            {
                dynamicEffect = dynamicEffect with { Target = CombatTarget.Internal_None };
                pgCombatModEx.DynamicEffects[index] = dynamicEffect;
            }

            Debug.Assert(!IsEqual(dynamicEffect.Target, Descriptor.Target));

            if (!float.IsNaN(dynamicEffect.TargetRange))
            {
                Debug.Assert(Descriptor.Target == AbilityTarget.Ally ||
                             Descriptor.Target == AbilityTarget.AllyOrSelf ||
                             Descriptor.Target == Internal_MixedSelfAlly ||
                             dynamicEffect.Target == CombatTarget.Allies ||
                             dynamicEffect.Target == CombatTarget.SelfAndAllies);
            }
        }
    }
}
