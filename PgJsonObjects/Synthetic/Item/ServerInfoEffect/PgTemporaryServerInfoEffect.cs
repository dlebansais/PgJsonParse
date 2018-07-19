using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgTemporaryServerInfoEffect : GenericPgObject<PgTemporaryServerInfoEffect>, IPgServerInfoEffect, IPgTemporaryServerInfoEffect
    {
        public PgTemporaryServerInfoEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgTemporaryServerInfoEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgTemporaryServerInfoEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgTemporaryServerInfoEffect(data, ref offset);
        }

        public override string Key { get { return null; } }
        public IPgItemEffect Boost { get { return GetObject(8, ref _Boost, PgItemEffectCollection.CreateNew); } } private IPgItemEffect _Boost;
        public float AttributeEffect { get { return RawAttributeEffect.HasValue ? RawAttributeEffect.Value : 0; } }
        public float? RawAttributeEffect { get { return (float)GetDouble(12); } }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get { return GetInt(16); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(20, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }

        public override string SortingName { get { return null; } }

        public IList<IBackLinkable> GetLinkBack()
        {
            return Boost?.GetLinkBack();
        }
    }
}
