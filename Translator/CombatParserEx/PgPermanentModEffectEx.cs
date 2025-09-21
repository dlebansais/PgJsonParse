namespace PgObjects;

public class PgPermanentModEffectEx
{
    public required CombatKeywordEx Keyword { get; init; }

    public required PgNumericValueEx Data { get; init; }

    public float DelayInSeconds { get; init; } = float.NaN;

    public CombatTarget Target { get; init; }

    public CombatCondition Condition { get; init; } = CombatCondition.Internal_None;

    public AbilityKeyword ActiveAbilityCondition { get; init; } = AbilityKeyword.Internal_None;
}
