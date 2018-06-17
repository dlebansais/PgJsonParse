using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgEffect : MainPgObject, IPgEffect
    {
        public PgEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgEffect(data, offset);
        }

        public string Name { get { return GetString(0); } }
        public string Desc { get { return GetString(4); } }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get { return GetInt(8); } }
        public string SpewText { get { return GetString(12); } }
        public EffectStackingType StackingType { get { return GetEnum<EffectStackingType>(16); } }
        public EffectDisplayMode DisplayMode { get { return GetEnum<EffectDisplayMode>(18); } }
        public int StackingPriority { get { return RawStackingPriority.HasValue ? RawStackingPriority.Value : 0; } }
        public int? RawStackingPriority { get { return GetInt(20); } }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get { return GetInt(24); } }
        public List<EffectKeyword> KeywordList { get { return GetEnumList(28, ref _KeywordList); } } private List<EffectKeyword> _KeywordList;
        public List<AbilityKeyword> AbilityKeywordList { get { return GetEnumList(32, ref _AbilityKeywordList); } } private List<AbilityKeyword> _AbilityKeywordList;
        public EffectParticle Particle { get { return GetEnum<EffectParticle>(36); } }
    }
}
