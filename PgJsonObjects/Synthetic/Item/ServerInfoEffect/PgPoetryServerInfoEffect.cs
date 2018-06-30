using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPoetryServerInfoEffect : GenericPgObject<PgPoetryServerInfoEffect>, IPgServerInfoEffect, IPgPoetryServerInfoEffect
    {
        public PgPoetryServerInfoEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgPoetryServerInfoEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgPoetryServerInfoEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgPoetryServerInfoEffect(data, ref offset);
        }

        public override string Key { get { return null; } }
        public int PoetryXpValue { get { return RawPoetryXpValue.HasValue ? RawPoetryXpValue.Value : 0; } }
        public int? RawPoetryXpValue { get { return GetInt(4); } }
        public int RecitalXpValue { get { return RawRecitalXpValue.HasValue ? RawRecitalXpValue.Value : 0; } }
        public int? RawRecitalXpValue { get { return GetInt(8); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
