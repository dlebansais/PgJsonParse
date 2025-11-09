using System.Collections.Generic;

namespace PgObjects;

public record PgPermanentModEffectEx
{
    public required CombatKeywordEx Keyword { get; init; }

    public PgNumericValueEx Data { get; init; } = PgNumericValueEx.Empty;

    public GameDamageType DamageType { get; init; } = GameDamageType.Internal_None;

    public float RandomChance { get; init; } = float.NaN;

    public float DelayInSeconds { get; init; } = float.NaN;

    public float DurationInSeconds { get; init; } = float.NaN;

    public float RecurringDelay { get; init; } = float.NaN;

    public CombatTarget Target { get; init; }

    public PgCombatConditionCollectionEx ConditionList { get; init; } = new();

    public List<AbilityKeyword> ConditionAbilityList { get; init; } = new();
}
