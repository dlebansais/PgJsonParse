namespace PgObjects;

using System.Collections.Generic;

public class PgCombatModEx
{
    public required string Description { get; init; }

    public required List<PgPermanentModEffectEx> PermanentEffects { get; init; }

    public required List<PgCombatModEffectEx> DynamicEffects { get; init; }
}
