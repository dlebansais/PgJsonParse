using System.Collections.Generic;

namespace PgJsonObjects
{
    interface IPgEffect
    {
        string Name { get; }
        string Desc { get; }
        int IconId { get; }
        int? RawIconId { get; }
        string SpewText { get; }
        EffectStackingType StackingType { get; }
        EffectDisplayMode DisplayMode { get; }
        int StackingPriority { get; }
        int? RawStackingPriority { get; }
        int Duration { get; }
        int? RawDuration { get; }
        List<EffectKeyword> KeywordList { get; }
        List<AbilityKeyword> AbilityKeywordList { get; }
        EffectParticle Particle { get; }
    }
}
