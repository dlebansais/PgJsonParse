using System.Collections.Generic;

namespace PgObjects;

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

    public CombatTarget Target { get; init; }
}
