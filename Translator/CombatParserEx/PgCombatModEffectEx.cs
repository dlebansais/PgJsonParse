namespace PgObjects;

using System.Collections.Generic;

public class PgCombatModEffectEx
{
    public required CombatKeywordEx Keyword { get; init; }

    public required List<AbilityKeyword> AbilityList { get; init; }

    public required PgNumericValueEx Data { get; init; }

    public GameDamageType DamageType { get; init; } = GameDamageType.Internal_None;

    public GameCombatSkill CombatSkill { get; init; } = GameCombatSkill.Internal_None;

    public float RandomChance { get; init; } = float.NaN;

    public float DelayInSeconds { get; init; } = float.NaN;

    public float DurationInSeconds { get; init; } = float.NaN;

    public float RecurringDelay { get; init; } = float.NaN;

    public CombatTarget Target { get; init; }

    public float TargetRange { get; init; } = float.NaN;

    public CombatCondition Condition { get; init; } = CombatCondition.Internal_None;

    public AbilityKeyword ActiveAbilityCondition { get; init; } = AbilityKeyword.Internal_None;
}
