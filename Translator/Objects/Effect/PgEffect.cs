﻿namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgEffect
    {
        public string Name { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get; set; }
        public string SpewText { get; set; } = string.Empty;
        public EffectStackingType StackingType { get; set; }
        public EffectDisplayMode DisplayMode { get; set; }
        public int StackingPriority { get { return RawStackingPriority.HasValue ? RawStackingPriority.Value : 0; } }
        public int? RawStackingPriority { get; set; }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get; set; }
        public List<EffectKeyword> KeywordList { get; } = new List<EffectKeyword>();
        public List<AbilityKeyword> AbilityKeywordList { get; } = new List<AbilityKeyword>();
        public EffectParticle Particle { get; set; }
        public int TSysKeywordIndex { get { return RawTSysKeywordIndex.HasValue ? RawTSysKeywordIndex.Value : 0; } }
        public int? RawTSysKeywordIndex { get; set; }
    }
}
