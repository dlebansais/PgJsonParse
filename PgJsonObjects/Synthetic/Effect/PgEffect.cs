using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgEffect : MainPgObject<PgEffect>, IPgEffect
    {
        public PgEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 46;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgEffect(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public string Name { get { return GetString(4); } }
        public string Desc { get { return GetString(8); } }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get { return GetInt(12); } }
        public string SpewText { get { return GetString(16); } }
        public EffectStackingType StackingType { get { return GetEnum<EffectStackingType>(20); } }
        public EffectDisplayMode DisplayMode { get { return GetEnum<EffectDisplayMode>(22); } }
        public int StackingPriority { get { return RawStackingPriority.HasValue ? RawStackingPriority.Value : 0; } }
        public int? RawStackingPriority { get { return GetInt(24); } }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get { return GetInt(28); } }
        public List<EffectKeyword> KeywordList { get { return GetEnumList(32, ref _KeywordList); } } private List<EffectKeyword> _KeywordList;
        public List<AbilityKeyword> AbilityKeywordList { get { return GetEnumList(36, ref _AbilityKeywordList); } } private List<AbilityKeyword> _AbilityKeywordList;
        protected override List<string> FieldTableOrder { get { return GetStringList(40, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public EffectParticle Particle { get { return GetEnum<EffectParticle>(44); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
