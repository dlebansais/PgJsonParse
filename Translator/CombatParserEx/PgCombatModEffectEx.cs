namespace PgObjects;

using System.Collections.Generic;

public class PgCombatModEffectEx
{
    public required CombatKeywordEx Keyword { get; init; }

    public List<AbilityKeyword> AbilityList { get; init; } = new();

    public required PgNumericValueEx Data { get; init; }

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

    public CombatCondition Condition { get; init; } = CombatCondition.Internal_None;

    public List<AbilityKeyword> ConditionAbilityList { get; init; } = new();

    public float ConditionValue { get; init; } = float.NaN;

    public float ConditionPercentage { get; init; } = float.NaN;

    public bool IsEveryOtherUse { get; init; }
}
