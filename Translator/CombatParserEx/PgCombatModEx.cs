namespace PgObjects;

using System.Collections.Generic;

public class PgCombatModEx
{
    public required string Description { get; init; }

    public List<PgPermanentModEffectEx> PermanentEffects { get; init; } = new();

    public List<PgCombatModEffectEx> DynamicEffects { get; init; } = new();
}
