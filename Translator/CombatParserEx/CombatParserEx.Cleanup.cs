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

        if ((DynamicEffect.ConditionList.Contains(CombatCondition.AbilityTriggered) ||
             DynamicEffect.ConditionList.Contains(CombatCondition.AbilityNotTriggered) ||
             DynamicEffect.ConditionList.Contains(CombatCondition.StandingSomewhere)) &&
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

    private AbilitySetDescriptor GetAbilities(List<AbilityKeyword> keywordList)
    {
        AbilitySet AbilitySet = GetAbilitySet(keywordList);

        if (AbilitySetCache.TryGetValue(AbilitySet, out AbilitySetDescriptor? CachedDescriptor))
            return CachedDescriptor;

        List<PgAbility> AbilityList = new();

        foreach (AbilityKeyword Keyword in keywordList)
            foreach (string Key in AbilityObjectKeyList)
            {
                PgAbility Ability = AbilityFromKey(Key);

                if (Ability.KeywordList.Contains(AbilityKeyword.Lint_MonsterAbility) && !Ability.KeywordList.Contains(AbilityKeyword.MinigolemAbility))
                    continue;

                bool Candidate;

                switch (Keyword)
                {
                    case Internal_MajorHealToYou:
                        Candidate = Ability.KeywordList.Contains(AbilityKeyword.MajorHeal) && Ability.Target == AbilityTarget.Self;
                        break;
                    default:
                        Candidate = Ability.KeywordList.Contains(Keyword);
                        break;
                }

                if (Candidate && !AbilityList.Contains(Ability))
                    AbilityList.Add(Ability);
            }

        TargetCategories Target = TargetCategories.None;

        foreach (PgAbility Ability in AbilityList)
        {
            Debug.Assert(Target == TargetCategories.None ||
                         (IsBeneficial(Target) && IsDefensive(Ability)) ||
                         (!IsBeneficial(Target) && IsOffsensive(Ability)));

            switch (Ability.Target)
            {
                case AbilityTarget.Enemy:
                    Target |= TargetCategories.Ennemy;
                    break;
                case AbilityTarget.EnemiesAroundSelf:
                    Target |= TargetCategories.AllEnnemies;
                    break;
                case AbilityTarget.Self:
                    Target |= TargetCategories.Self;
                    break;
                case AbilityTarget.Ally:
                    Target |= TargetCategories.Ally;
                    break;
                case AbilityTarget.AllyOrSelf:
                    Target |= TargetCategories.Ally;
                    Target |= TargetCategories.Self;
                    break;
                case AbilityTarget.DeadAlly:
                    Target |= TargetCategories.Ally;
                    break;
                case AbilityTarget.Corpse:
                    Target |= TargetCategories.Ally;
                    break;
                case AbilityTarget.Pet:
                case AbilityTarget.PermanentPet:
                    Target |= TargetCategories.Pet;
                    break;
                default:
                    Debug.Fail("Unhandled AbilityTarget");
                    break;
            }
        }

        Debug.Assert(Target != TargetCategories.None);

        AbilitySetDescriptor Result = new()
        {
            AbilityList = AbilityList,
            TargetCategories = Target,
        };

        AbilitySetCache.Add(AbilitySet, Result);

        return Result;
    }

    private const AbilityTarget Internal_MixedSelfAlly = (AbilityTarget)0xFFFF;

    private static bool IsOffsensive(PgAbility ability)
    {
        return ability.Target == AbilityTarget.EnemiesAroundSelf || ability.Target == AbilityTarget.Enemy;
    }

    private static bool IsDefensive(PgAbility ability)
    {
        return (ability.Target != AbilityTarget.EnemiesAroundSelf && ability.Target != AbilityTarget.Enemy) ||
               ability.KeywordList.Contains(AbilityKeyword.MinorHealAttack) ||
               ability.KeywordList.Contains(AbilityKeyword.SurvivalUtility);
    }

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

    private static bool IsBeneficial(CombatTarget target)
    {
        return target is not CombatTarget.Internal_None;
    }

    private static bool IsBeneficial(TargetCategories targetCategories)
    {
        Debug.Assert(targetCategories != TargetCategories.None);

        return targetCategories.HasFlag(TargetCategories.Self) ||
               targetCategories.HasFlag(TargetCategories.Ally) ||
               targetCategories.HasFlag(TargetCategories.Pet) ||
               targetCategories.HasFlag(TargetCategories.TargetPet);
    }

    private static bool IsEqual(CombatTarget combatTarget, TargetCategories targetCategories)
    {
        return (combatTarget == CombatTarget.Self && targetCategories == TargetCategories.Self) ||
               (combatTarget == CombatTarget.Allies && targetCategories == TargetCategories.Ally) ||
               (combatTarget == CombatTarget.SelfAndAllies && targetCategories == (TargetCategories.Self | TargetCategories.Ally));
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
        Debug.Assert(fields.HasFlag(DynamicFields.CombatSkill) || dynamicEffect.CombatSkill == GameCombatSkill.Internal_None || dynamicEffect.ConditionList.Contains(CombatCondition.ActiveSkill));
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
                            DynamicFields.DataPercent | //TODO check healing ability
                            DynamicFields.RandomChance |
                            DynamicFields.DelayInSeconds |
                            DynamicFields.DurationInSeconds | //TODO check song
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

            if (!IsBeneficial(dynamicEffect.Target) && !IsBeneficial(Descriptor.TargetCategories))
            {
                Debug.Assert(dynamicEffect.Target == CombatTarget.Internal_None);
                dynamicEffect = dynamicEffect with { Target = CombatTarget.Self };
                pgCombatModEx.DynamicEffects[index] = dynamicEffect;
            }

            if (dynamicEffect.Target == CombatTarget.Self && Descriptor.TargetCategories == TargetCategories.Self)
            {
                dynamicEffect = dynamicEffect with { Target = CombatTarget.Internal_None };
                pgCombatModEx.DynamicEffects[index] = dynamicEffect;
            }

            if (dynamicEffect.Target == CombatTarget.SelfAndAllies && Descriptor.TargetCategories == (TargetCategories.Self | TargetCategories.Ally))
            {
                dynamicEffect = dynamicEffect with { Target = CombatTarget.Internal_None };
                pgCombatModEx.DynamicEffects[index] = dynamicEffect;
            }

            Debug.Assert(!IsEqual(dynamicEffect.Target, Descriptor.TargetCategories));

            if (!float.IsNaN(dynamicEffect.TargetRange))
            {
                Debug.Assert(Descriptor.TargetCategories.HasFlag(TargetCategories.Ally) ||
                             Descriptor.TargetCategories.HasFlag(TargetCategories.Pet) ||
                             Descriptor.TargetCategories.HasFlag(TargetCategories.TargetPet) ||
                             dynamicEffect.Target != CombatTarget.Self);
            }
        }
    }
}
