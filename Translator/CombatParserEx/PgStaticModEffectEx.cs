using System.Collections.Generic;
using Translator;

namespace PgObjects;

public class PgStaticModEffectEx
{
    public required StaticModifier Modifier { get; init; }

    public PgNumericValueEx Data { get; init; } = PgNumericValueEx.Empty;
}
