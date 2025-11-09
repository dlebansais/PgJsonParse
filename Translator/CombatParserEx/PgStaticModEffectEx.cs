namespace PgObjects;

using Translator;

public class PgStaticModEffectEx
{
    public required StaticModifier Modifier { get; init; }

    public PgNumericValueEx Data { get; init; } = PgNumericValueEx.Empty;
}
