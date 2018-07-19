using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgEffectSource : PgGenericSource<PgEffectSource>, IPgEffectSource
    {
        public PgEffectSource(byte[] data, ref int offset)
            : base(data, offset)
        {
        }
        
        protected override PgEffectSource CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgEffectSource CreateNew(byte[] data, ref int offset)
        {
            return new PgEffectSource(data, ref offset);
        }

        public override void Init(IGenericPgObject Parent)
        {
            Parent.AddLinkBack(Effect);
        }

        public IPgEffect Effect { get { return GetObject(PropertiesOffset + 0, ref _Effect, PgEffect.CreateNew); } } private IPgEffect _Effect;

        public override string Key { get { return null; } }
        protected override List<string> FieldTableOrder { get { return null; } }
        protected override Dictionary<string, FieldParser> FieldTable { get { return null; } }
        public override string SortingName { get { return null; } }
    }
}
