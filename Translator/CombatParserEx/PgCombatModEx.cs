namespace PgObjects;

using System.Collections.Generic;

public class PgCombatModEx
{
    public required string Description { get; init; }

    public required List<PgCombatModEffectEx> StaticEffects { get; init; }

    public required List<PgCombatModEffectEx> DynamicEffects { get; init; }
}
