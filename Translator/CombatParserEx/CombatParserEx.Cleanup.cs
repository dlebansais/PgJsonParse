namespace TranslatorCombatParserEx;

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
            CleanupDynamicEffects(pgCombatModEx, ref i, ref Buff);
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

        Debug.Assert(PermanentEffect.Target != CombatTarget.Internal_None);
        Debug.Assert(!float.IsNaN(PermanentEffect.Data.Value) || PermanentEffect.Keyword == CombatKeywordEx.GiveBuffEachAttack);

        switch (PermanentEffect.Keyword)
        {
            case CombatKeywordEx.RestoreHealth:
                CleanupPermanentEffectRestoreHealth(pgCombatModEx, index, PermanentEffect);
                break;
            case CombatKeywordEx.RestoreArmor:
                CleanupPermanentEffectRestoreArmor(pgCombatModEx, index, PermanentEffect);
                break;
            case CombatKeywordEx.RestorePower:
                CleanupPermanentEffectRestorePower(pgCombatModEx, index, PermanentEffect);
                break;
            default:
                break;
        }
    }

    private void CleanupDynamicEffects(PgCombatModEx pgCombatModEx, ref int index, ref CombatKeywordEx buff)
    {
        PgCombatModEffectEx DynamicEffect = pgCombatModEx.DynamicEffects[index];

        if (DynamicEffect.Keyword == CombatKeywordEx.GiveBuff ||
            DynamicEffect.Keyword == CombatKeywordEx.GiveBuffConditionalAttack ||
            DynamicEffect.Keyword == CombatKeywordEx.GiveBuffEachAttack ||
            DynamicEffect.Keyword == CombatKeywordEx.GiveBuffOneAttack ||
            DynamicEffect.Keyword == CombatKeywordEx.GiveBuffOneHit ||
            DynamicEffect.Keyword == CombatKeywordEx.GiveBuffOneRageAttack ||
            DynamicEffect.Keyword == CombatKeywordEx.GiveBuffOneUse ||
            DynamicEffect.Keyword == CombatKeywordEx.LastingMark)
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
                CleanupDynamicEffectRestoreHealth(pgCombatModEx, ref index, buff, DynamicEffect);
                break;
            case CombatKeywordEx.RestoreArmor:
                CleanupDynamicEffectRestoreArmor(pgCombatModEx, ref index, buff, DynamicEffect);
                break;
            case CombatKeywordEx.RestorePower:
                CleanupDynamicEffectRestorePower(pgCombatModEx, ref index, buff, DynamicEffect);
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
                    case Internal_HammerRestoreArmor:
                        Candidate = Ability.KeywordList.Contains(AbilityKeyword.Hammer) &&
                                    Ability.PvE.SpecialValueList.Exists((specialValue) => specialValue.Label == "Restores" && specialValue.Suffix == "Armor after a 6-second delay");
                        break;
                    case Internal_ArcheryThatCrit:
                        Candidate = Ability.KeywordList.Contains(AbilityKeyword.Archery) &&
                                    Ability.KeywordList.Contains(AbilityKeyword.AnatomyCriticals);
                        break;
                    default:
                        Candidate = Ability.KeywordList.Contains(Keyword);
                        break;
                }

                if (Candidate && !AbilityList.Contains(Ability))
                    AbilityList.Add(Ability);
            }

        TargetCategories Target = keywordList.Contains(AbilityKeyword.DruidNonBasic) ? TargetCategories.Mixed : TargetCategories.None;

        foreach (PgAbility Ability in AbilityList)
        {
            Debug.Assert(Target == TargetCategories.None ||
                         Target == TargetCategories.Mixed ||
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

    private static bool IsSpecialValueCompatible(CombatTarget target,  TargetCategories targetCategories, PgSpecialValue specialValue)
    {
        return true;
    }

    private void AssertPermanentFields(PgPermanentModEffectEx permanentEffect, CombatKeywordEx keyword, PermanentFields fields)
    {
        Debug.Assert(permanentEffect.Keyword == keyword);
        Debug.Assert(fields.HasFlag(PermanentFields.Data) || fields.HasFlag(PermanentFields.DataValue) || fields.HasFlag(PermanentFields.DataValuePositive) || float.IsNaN(permanentEffect.Data.Value));
        Debug.Assert(fields.HasFlag(PermanentFields.Data) || !fields.HasFlag(PermanentFields.DataValue) || fields.HasFlag(PermanentFields.DataValuePositive) || permanentEffect.Data.Value != 0);
        Debug.Assert(fields.HasFlag(PermanentFields.Data) || fields.HasFlag(PermanentFields.DataValue) || !fields.HasFlag(PermanentFields.DataValuePositive) || permanentEffect.Data.Value > 0);
        Debug.Assert(fields.HasFlag(PermanentFields.Data) || fields.HasFlag(PermanentFields.DataPercent) || !permanentEffect.Data.IsPercent);
        Debug.Assert(fields.HasFlag(PermanentFields.DamageType) || permanentEffect.DamageType == GameDamageType.Internal_None);
        Debug.Assert(fields.HasFlag(PermanentFields.RandomChance) || float.IsNaN(permanentEffect.RandomChance));
        Debug.Assert(fields.HasFlag(PermanentFields.DelayInSeconds) || float.IsNaN(permanentEffect.DelayInSeconds));
        Debug.Assert(fields.HasFlag(PermanentFields.DurationInSeconds) || float.IsNaN(permanentEffect.DurationInSeconds));
        Debug.Assert(fields.HasFlag(PermanentFields.RecurringDelay) || float.IsNaN(permanentEffect.RecurringDelay));
        Debug.Assert(fields.HasFlag(PermanentFields.Target) || permanentEffect.Target == CombatTarget.Internal_None);
        Debug.Assert(fields.HasFlag(PermanentFields.ConditionList) || permanentEffect.ConditionList.Count == 0);
        Debug.Assert(fields.HasFlag(PermanentFields.ConditionAbilityList) || permanentEffect.ConditionAbilityList.Count == 0);
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

    private static bool TryGetStaticModEffect(PgCombatModEffectEx dynamicEffect, AbilitySetDescriptor descriptor, StaticModifier staticModifier, [NotNullWhen(true)] out PgStaticModEffectEx? staticModEffectEx)
    {
        switch (staticModifier)
        {
            case StaticModifier.SpecialValueMajorHeal:
                Debug.Assert(IsBeneficial(descriptor.TargetCategories));
                return TryGetSpecialValueByAttribute(dynamicEffect, descriptor, staticModifier, "BOOST_MAJORHEAL_SENDER", out staticModEffectEx);
            case StaticModifier.SpecialValueMinorHeal:
                Debug.Assert(IsBeneficial(descriptor.TargetCategories));
                return TryGetSpecialValueByAttribute(dynamicEffect, descriptor, staticModifier, "BOOST_MINORHEAL_SENDER", out staticModEffectEx);
            case StaticModifier.SpecialValueCustomRestoreHealth:
                Debug.Assert(IsBeneficial(descriptor.TargetCategories));
                return TryGetSpecialValueByLabel(dynamicEffect, descriptor,
                    staticModifier, new List<SpecialValueText>()
                {
                    new() { Label = "Restore", Suffix = "Health" },
                    new() { Label = "Restore", Suffix = "Health to yourself" },
                    new() { Label = "Restore", Suffix = "Health (or Armor if Health is full) to Pet" },
                    new() { Label = "Restore", Suffix = "Health to least-healthy ally" },
                    new() { Label = "Pets within 10 meters heal", Suffix = "" },
                }, new List<SpecialValueText>()
                {
                    new() { Label = "Restore", Suffix = "Health every 2 secs" },
                    new() { Label = "Restore", Suffix = "Health every 4 seconds" },
                    new() { Label = "Restore", Suffix = "Health to Nearby Allies every 4 seconds" },
                }, out staticModEffectEx);
            case StaticModifier.SpecialValueCustomRestoreArmor:
                Debug.Assert(IsBeneficial(descriptor.TargetCategories));
                return TryGetSpecialValueByLabel(dynamicEffect, descriptor, staticModifier,
                    new List<SpecialValueText>()
                {
                    new() { Label = "Restore", Suffix = "Armor" },
                }, new List<SpecialValueText>()
                {
                    new() { Label = "Restore", Suffix = "Armor every 2 secs" },
                    new() { Label = "Restore", Suffix = "Armor every 3 seconds" },
                }, out staticModEffectEx);
            case StaticModifier.SpecialValueCustomRestorePower:
                Debug.Assert(IsBeneficial(descriptor.TargetCategories));
                return TryGetSpecialValueByLabel(dynamicEffect, descriptor, staticModifier,
                    new List<SpecialValueText>()
                {
                    new() { Label = "Restore", Suffix = "Power" },
                    new() { Label = "Glyph restores", Suffix = "Power when touched" },
                    new() { Label = "Restore", Suffix = "Power to allies" },
                }, new List<SpecialValueText>()
                {
                    new() { Label = "Restore", Suffix = "Power to Nearby Allies every 4 seconds" },
                    new() { Label = "Restore", Suffix = "Power every 3 secs" },
                    new() { Label = "Restore", Suffix = "Power every 4 seconds" },
                }, out staticModEffectEx);
            default:
                Debug.Fail("Unhandled StaticModifier");
                staticModEffectEx = null;
                return false;
        }
    }

    private static bool TryGetSpecialValueByAttribute(PgCombatModEffectEx dynamicEffect, AbilitySetDescriptor descriptor, StaticModifier staticModifier, string attributeName, [NotNullWhen(true)] out PgStaticModEffectEx? staticModEffectEx)
    {
        if (descriptor.AbilityList.TrueForAll((Ability) => Ability.PvE.SpecialValueList.Exists((PgSpecialValue SpecialValue) =>
        {
            return SpecialValue.AttributesThatDeltaList.Contains(attributeName) &&
                   IsSpecialValueCompatible(dynamicEffect.Target, descriptor.TargetCategories, SpecialValue);
        })))
        {
            staticModEffectEx = new PgStaticModEffectEx()
            {
                Modifier = staticModifier,
                Data = dynamicEffect.Data,
            };

            return true;
        }

        staticModEffectEx = null;
        return false;
    }

    private static bool TryGetSpecialValueByLabel(PgCombatModEffectEx dynamicEffect, AbilitySetDescriptor descriptor, StaticModifier staticModifier, List<SpecialValueText> specialValueTexts, List<SpecialValueText> specialValueRecurringTexts, [NotNullWhen(true)] out PgStaticModEffectEx? staticModEffectEx)
    {
        List<SpecialValueText> textList =  float.IsNaN(dynamicEffect.RecurringDelay)
                                           ? specialValueTexts
                                           : specialValueRecurringTexts;

        if (descriptor.AbilityList.TrueForAll((Ability) => Ability.PvE.SpecialValueList.Exists((PgSpecialValue SpecialValue) =>
        {
            string SpecialValueLabel = SpecialValue.Label;
            string SpecialValueSuffix = SpecialValue.Suffix;

            if (SpecialValueLabel == "Restores")
                SpecialValueLabel = "Restore";

            return textList.Exists((SpecialValueText svt) =>
            {
                return SpecialValueLabel == svt.Label && SpecialValueSuffix == svt.Suffix &&
                       IsSpecialValueCompatible(dynamicEffect.Target, descriptor.TargetCategories, SpecialValue);
            });
        })))
        {
            staticModEffectEx = new PgStaticModEffectEx()
            {
                Modifier = staticModifier,
                Data = dynamicEffect.Data,
            };

            return true;
        }
        else
        {
            staticModEffectEx = null;
            return false;
        }
    }

    private void CleanupPermanentEffectRestoreHealth(PgCombatModEx pgCombatModEx, int index, PgPermanentModEffectEx permanentEffect)
    {
        AssertPermanentFields(permanentEffect, CombatKeywordEx.RestoreHealth,
                              PermanentFields.DataValuePositive |
                              PermanentFields.DelayInSeconds |
                              PermanentFields.RecurringDelay |
                              PermanentFields.Target);

        Debug.Assert(float.IsNaN(permanentEffect.RecurringDelay) ||
                     permanentEffect.Target == CombatTarget.DruidHealingSanctuary ||
                     permanentEffect.Target == CombatTarget.SpiritFoxPowerGlyph ||
                     permanentEffect.Target == CombatTarget.FairyFaeConduit);

        Debug.Assert(float.IsNaN(permanentEffect.DelayInSeconds) ||
                     permanentEffect.Target == CombatTarget.SpiritFoxPowerGlyph);
    }

    private void CleanupDynamicEffectRestoreHealth(PgCombatModEx pgCombatModEx, ref int index, CombatKeywordEx buff, PgCombatModEffectEx dynamicEffect)
    {
        AssertDynamicFields(dynamicEffect, CombatKeywordEx.RestoreHealth,
                            DynamicFields.AbilityList |
                            DynamicFields.DataValuePositive |
                            DynamicFields.DataPercent |
                            DynamicFields.RandomChance |
                            DynamicFields.DelayInSeconds |
                            DynamicFields.RecurringDelay |
                            DynamicFields.Target |
                            DynamicFields.TargetRange |
                            DynamicFields.ConditionList |
                            DynamicFields.ConditionAbilityList |
                            DynamicFields.ConditionValue |
                            DynamicFields.ConditionPercentage |
                            DynamicFields.IsEveryOtherUse);
        Debug.Assert(float.IsNaN(dynamicEffect.DelayInSeconds) || float.IsNaN(dynamicEffect.RecurringDelay));
        Debug.Assert(!dynamicEffect.Data.IsPercent || dynamicEffect.AbilityList.Count > 0);

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

            Debug.Assert(!dynamicEffect.Data.IsPercent);
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

            if (dynamicEffect.Target == CombatTarget.AnimalHandlingPet && Descriptor.TargetCategories == TargetCategories.Pet)
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

        if (buff == CombatKeywordEx.Internal_None && dynamicEffect.AbilityList.Count > 0)
        {
            AbilitySetDescriptor Descriptor = GetAbilities(dynamicEffect.AbilityList);

            if (IsBeneficial(Descriptor.TargetCategories))
            {
                PgStaticModEffectEx? StaticModEffectEx = null;

                _ = TryGetStaticModEffect(dynamicEffect, Descriptor, StaticModifier.SpecialValueMajorHeal, out StaticModEffectEx) ||
                    TryGetStaticModEffect(dynamicEffect, Descriptor, StaticModifier.SpecialValueMinorHeal, out StaticModEffectEx) ||
                    TryGetStaticModEffect(dynamicEffect, Descriptor, StaticModifier.SpecialValueCustomRestoreHealth, out StaticModEffectEx);

                if (StaticModEffectEx is not null && dynamicEffect.Target == CombatTarget.Internal_None)
                {
                    pgCombatModEx.StaticEffects.Add(StaticModEffectEx);
                    pgCombatModEx.DynamicEffects.RemoveAt(index);
                    index--;
                }
                else
                {
                    string Description = pgCombatModEx.Description;
                    string? Label0 = null;
                    string? Suffix0 = null;
                    string? Label1 = null;
                    string? Suffix1 = null;
                    string? Label2 = null;
                    string? Suffix2 = null;
                    if (Descriptor.AbilityList[0].PvE.SpecialValueList.Count > 0)
                    {
                        PgSpecialValue s = Descriptor.AbilityList[0].PvE.SpecialValueList[0];
                        Label0 = s.Label;
                        Suffix0 = s.Suffix;
                    }
                    if (Descriptor.AbilityList[0].PvE.SpecialValueList.Count > 1)
                    {
                        PgSpecialValue s = Descriptor.AbilityList[0].PvE.SpecialValueList[1];
                        Label1 = s.Label;
                        Suffix1 = s.Suffix;
                    }
                    if (Descriptor.AbilityList[0].PvE.SpecialValueList.Count > 2)
                    {
                        PgSpecialValue s = Descriptor.AbilityList[0].PvE.SpecialValueList[2];
                        Label2 = s.Label;
                        Suffix2 = s.Suffix;
                    }
                }

                Debug.Assert(!dynamicEffect.Data.IsPercent || StaticModEffectEx is not null);
            }
        }
    }

    private void CleanupPermanentEffectRestoreArmor(PgCombatModEx pgCombatModEx, int index, PgPermanentModEffectEx permanentEffect)
    {
        AssertPermanentFields(permanentEffect, CombatKeywordEx.RestoreArmor,
                              PermanentFields.DataValuePositive |
                              PermanentFields.DelayInSeconds |
                              PermanentFields.RecurringDelay |
                              PermanentFields.Target);

        Debug.Assert(float.IsNaN(permanentEffect.RecurringDelay) ||
                     permanentEffect.Target == CombatTarget.DruidHealingSanctuary ||
                     permanentEffect.Target == CombatTarget.SpiritFoxPowerGlyph ||
                     permanentEffect.Target == CombatTarget.FairyFaeConduit);

        Debug.Assert(float.IsNaN(permanentEffect.DelayInSeconds) ||
                     permanentEffect.Target == CombatTarget.SpiritFoxPowerGlyph);
    }

    private void CleanupDynamicEffectRestoreArmor(PgCombatModEx pgCombatModEx, ref int index, CombatKeywordEx buff, PgCombatModEffectEx dynamicEffect)
    {
        AssertDynamicFields(dynamicEffect, CombatKeywordEx.RestoreArmor,
                            DynamicFields.AbilityList |
                            DynamicFields.DataValuePositive |
                            DynamicFields.DataPercent |
                            DynamicFields.RandomChance |
                            DynamicFields.DelayInSeconds |
                            DynamicFields.RecurringDelay |
                            DynamicFields.Target |
                            DynamicFields.TargetRange |
                            DynamicFields.ConditionList |
                            DynamicFields.ConditionAbilityList |
                            DynamicFields.ConditionValue |
                            DynamicFields.ConditionPercentage |
                            DynamicFields.IsEveryOtherUse);
        Debug.Assert(float.IsNaN(dynamicEffect.DelayInSeconds) || float.IsNaN(dynamicEffect.RecurringDelay));
        Debug.Assert(!dynamicEffect.Data.IsPercent || dynamicEffect.AbilityList.Count > 0);

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

            Debug.Assert(!dynamicEffect.Data.IsPercent);
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

            if (dynamicEffect.Target == CombatTarget.AnimalHandlingPet && Descriptor.TargetCategories == TargetCategories.Pet)
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

        if (buff == CombatKeywordEx.Internal_None && dynamicEffect.AbilityList.Count > 0)
        {
            AbilitySetDescriptor Descriptor = GetAbilities(dynamicEffect.AbilityList);

            if (IsBeneficial(Descriptor.TargetCategories))
            {
                PgStaticModEffectEx? StaticModEffectEx = null;

                _ = TryGetStaticModEffect(dynamicEffect, Descriptor, StaticModifier.SpecialValueCustomRestoreArmor, out StaticModEffectEx);

                if (StaticModEffectEx is not null && dynamicEffect.Target == CombatTarget.Internal_None)
                {
                    pgCombatModEx.StaticEffects.Add(StaticModEffectEx);
                    pgCombatModEx.DynamicEffects.RemoveAt(index);
                    index--;
                }
                else
                {
                    string Description = pgCombatModEx.Description;
                    string? Label0 = null;
                    string? Suffix0 = null;
                    string? Label1 = null;
                    string? Suffix1 = null;
                    string? Label2 = null;
                    string? Suffix2 = null;
                    if (Descriptor.AbilityList[0].PvE.SpecialValueList.Count > 0)
                    {
                        PgSpecialValue s = Descriptor.AbilityList[0].PvE.SpecialValueList[0];
                        Label0 = s.Label;
                        Suffix0 = s.Suffix;
                    }
                    if (Descriptor.AbilityList[0].PvE.SpecialValueList.Count > 1)
                    {
                        PgSpecialValue s = Descriptor.AbilityList[0].PvE.SpecialValueList[1];
                        Label1 = s.Label;
                        Suffix1 = s.Suffix;
                    }
                    if (Descriptor.AbilityList[0].PvE.SpecialValueList.Count > 2)
                    {
                        PgSpecialValue s = Descriptor.AbilityList[0].PvE.SpecialValueList[2];
                        Label2 = s.Label;
                        Suffix2 = s.Suffix;
                    }
                }

                Debug.Assert(!dynamicEffect.Data.IsPercent || StaticModEffectEx is not null);
            }
        }
    }

    private void CleanupPermanentEffectRestorePower(PgCombatModEx pgCombatModEx, int index, PgPermanentModEffectEx permanentEffect)
    {
        AssertPermanentFields(permanentEffect, CombatKeywordEx.RestorePower,
                              PermanentFields.DataValuePositive |
                              PermanentFields.DelayInSeconds |
                              PermanentFields.RecurringDelay |
                              PermanentFields.Target);

        Debug.Assert(float.IsNaN(permanentEffect.RecurringDelay) ||
                     permanentEffect.Target == CombatTarget.DruidHealingSanctuary ||
                     permanentEffect.Target == CombatTarget.SpiritFoxPowerGlyph ||
                     permanentEffect.Target == CombatTarget.FairyFaeConduit);

        Debug.Assert(float.IsNaN(permanentEffect.DelayInSeconds) ||
                     permanentEffect.Target == CombatTarget.SpiritFoxPowerGlyph);
    }

    private void CleanupDynamicEffectRestorePower(PgCombatModEx pgCombatModEx, ref int index, CombatKeywordEx buff, PgCombatModEffectEx dynamicEffect)
    {
        AssertDynamicFields(dynamicEffect, CombatKeywordEx.RestorePower,
                            DynamicFields.AbilityList |
                            DynamicFields.DataValuePositive |
                            DynamicFields.DataPercent |
                            DynamicFields.RandomChance |
                            DynamicFields.DelayInSeconds |
                            DynamicFields.RecurringDelay |
                            DynamicFields.Target |
                            DynamicFields.TargetRange |
                            DynamicFields.ConditionList |
                            DynamicFields.ConditionAbilityList |
                            DynamicFields.ConditionValue |
                            DynamicFields.ConditionPercentage |
                            DynamicFields.IsEveryOtherUse);
        Debug.Assert(float.IsNaN(dynamicEffect.DelayInSeconds) || float.IsNaN(dynamicEffect.RecurringDelay));
        Debug.Assert(!dynamicEffect.Data.IsPercent || dynamicEffect.AbilityList.Count > 0);

        if (buff != CombatKeywordEx.Internal_None)
        {
            Debug.Assert(buff == CombatKeywordEx.GiveBuffOneHit ||
                         buff == CombatKeywordEx.GiveBuffOneAttack ||
                         buff == CombatKeywordEx.LastingMark ||
                         (buff == CombatKeywordEx.GiveBuff &&
                            (dynamicEffect.AbilityList.Count > 0 ||
                             dynamicEffect.ConditionList.Contains(CombatCondition.TargetOfAbility)) ||
                             (!float.IsNaN(dynamicEffect.RecurringDelay) && dynamicEffect.AbilityList.Count == 0)) ||
                         (buff == CombatKeywordEx.GiveBuffOneUse &&
                            (dynamicEffect.AbilityList.Count > 0 ||
                             dynamicEffect.ConditionList.Contains(CombatCondition.TargetKilled) ||
                             dynamicEffect.ConditionList.Contains(CombatCondition.TargetIsKilled))));

            Debug.Assert(!dynamicEffect.Data.IsPercent);
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

            if (dynamicEffect.Target == CombatTarget.AnimalHandlingPet && Descriptor.TargetCategories == TargetCategories.Pet)
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

        if (buff == CombatKeywordEx.Internal_None && dynamicEffect.AbilityList.Count > 0)
        {
            AbilitySetDescriptor Descriptor = GetAbilities(dynamicEffect.AbilityList);

            if (IsBeneficial(Descriptor.TargetCategories))
            {
                PgStaticModEffectEx? StaticModEffectEx = null;

                _ = TryGetStaticModEffect(dynamicEffect, Descriptor, StaticModifier.SpecialValueCustomRestorePower, out StaticModEffectEx);

                if (StaticModEffectEx is not null && dynamicEffect.Target == CombatTarget.Internal_None)
                {
                    pgCombatModEx.StaticEffects.Add(StaticModEffectEx);
                    pgCombatModEx.DynamicEffects.RemoveAt(index);
                    index--;
                }
                else
                {
                    string Description = pgCombatModEx.Description;
                    string? Label0 = null;
                    string? Suffix0 = null;
                    string? Label1 = null;
                    string? Suffix1 = null;
                    string? Label2 = null;
                    string? Suffix2 = null;
                    if (Descriptor.AbilityList[0].PvE.SpecialValueList.Count > 0)
                    {
                        PgSpecialValue s = Descriptor.AbilityList[0].PvE.SpecialValueList[0];
                        Label0 = s.Label;
                        Suffix0 = s.Suffix;
                    }
                    if (Descriptor.AbilityList[0].PvE.SpecialValueList.Count > 1)
                    {
                        PgSpecialValue s = Descriptor.AbilityList[0].PvE.SpecialValueList[1];
                        Label1 = s.Label;
                        Suffix1 = s.Suffix;
                    }
                    if (Descriptor.AbilityList[0].PvE.SpecialValueList.Count > 2)
                    {
                        PgSpecialValue s = Descriptor.AbilityList[0].PvE.SpecialValueList[2];
                        Label2 = s.Label;
                        Suffix2 = s.Suffix;
                    }
                }

                Debug.Assert(!dynamicEffect.Data.IsPercent || StaticModEffectEx is not null);
            }
        }
    }
}
