namespace PgObjects;

public class PgPermanentModEffectEx
{
    public required CombatKeywordEx Keyword { get; init; }

    public required PgNumericValueEx Data { get; init; }

    public CombatTarget Target { get; init; }
}
