namespace PgObjects;

using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

public record PgCombatModEffectEx
{
    public required CombatKeywordEx Keyword { get; init; }

    public List<AbilityKeyword> AbilityList { get; init; } = new();

    public PgNumericValueEx Data { get; init; } = PgNumericValueEx.Empty;

    public GameDamageType DamageType { get; init; } = GameDamageType.Internal_None;

    public GameDamageCategory DamageCategory { get; init; } = GameDamageCategory.Internal_None;

    public GameCombatSkill CombatSkill { get; init; } = GameCombatSkill.Internal_None;

    public float RandomChance { get; init; } = float.NaN;

    public float DelayInSeconds { get; init; } = float.NaN;

    public float DurationInSeconds { get; init; } = float.NaN;

    public float RecurringDelay { get; init; } = float.NaN;

    public CombatTarget Target { get; init; }

    public float TargetRange { get; init; } = float.NaN;

    public List<AbilityKeyword> TargetAbilityList { get; init; } = new();

    public PgCombatConditionCollectionEx ConditionList { get; init; } = new();

    public List<AbilityKeyword> ConditionAbilityList { get; init; } = new();

    public float ConditionValue { get; init; } = float.NaN;

    public float ConditionPercentage { get; init; } = float.NaN;

    public bool IsEveryOtherUse { get; init; }

    public virtual bool Equals(PgCombatModEffectEx? other)
    {
        return other is not null &&
               Keyword == other.Keyword &&
               Enumerable.SequenceEqual(AbilityList, other.AbilityList) &&
               IsFloatEqual(Data.Value, other.Data.Value) &&
               DamageType == other.DamageType &&
               DamageCategory == other.DamageCategory &&
               CombatSkill == other.CombatSkill &&
               DamageType == other.DamageType &&
               IsFloatEqual(RandomChance, other.RandomChance) &&
               IsFloatEqual(DelayInSeconds, other.DelayInSeconds) &&
               IsFloatEqual(DurationInSeconds, other.DurationInSeconds) &&
               IsFloatEqual(RecurringDelay, other.RecurringDelay) &&
               Target == other.Target &&
               IsFloatEqual(TargetRange, other.TargetRange) &&
               Enumerable.SequenceEqual(TargetAbilityList, other.TargetAbilityList) &&
               Enumerable.SequenceEqual(ConditionList, other.ConditionList) &&
               Enumerable.SequenceEqual(ConditionAbilityList, other.ConditionAbilityList) &&
               IsFloatEqual(ConditionValue, other.ConditionValue) &&
               IsFloatEqual(ConditionPercentage, other.ConditionPercentage) &&
               IsEveryOtherUse == other.IsEveryOtherUse;
    }

    private static bool IsFloatEqual(float a, float b)
    {
        return (float.IsNaN(a) && float.IsNaN(b)) || a == b;
    }

    public override int GetHashCode() => base.GetHashCode();
}
