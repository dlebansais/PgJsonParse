using System.Collections.Generic;

namespace PgObjects;

public class PgPermanentModEffectEx
{
    public required CombatKeywordEx Keyword { get; init; }

    public required PgNumericValueEx Data { get; init; }

    public GameDamageType DamageType { get; init; } = GameDamageType.Internal_None;

    public float RandomChance { get; init; } = float.NaN;

    public float DelayInSeconds { get; init; } = float.NaN;

    public float DurationInSeconds { get; init; } = float.NaN;

    public float RecurringDelay { get; init; } = float.NaN;

    public CombatTarget Target { get; init; }

    public List<CombatCondition> ConditionList { get; init; } = new();

    public List<AbilityKeyword> ConditionAbilityList { get; init; } = new();
}
