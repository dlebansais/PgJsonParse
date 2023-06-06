namespace PgObjects
{
    using System.Collections.Generic;

    public class PgEffect : PgObject
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get; set; }
        public EffectDisplayMode DisplayMode { get; set; }
        public string SpewText { get; set; } = string.Empty;
        public PgEffectParticle? Particle { get; set; }
        public EffectStackingType StackingType { get; set; }
        public int StackingPriority { get { return RawStackingPriority.HasValue ? RawStackingPriority.Value : 0; } }
        public int? RawStackingPriority { get; set; }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get; set; }
        public List<EffectKeyword> KeywordList { get; set; } = new List<EffectKeyword>();
        public List<AbilityKeyword> AbilityKeywordList { get; set; } = new List<AbilityKeyword>();

        public int FriendlyIconId { get; set; }
        public string FriendlyName { get; set; } = string.Empty;

        public override int ObjectIconId { get { return FriendlyIconId; } }
        public override string ObjectName { get { return FriendlyName; } }
        public override string ToString() { return FriendlyName; }
    }
}
