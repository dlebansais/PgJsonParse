using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItemAttributeLink : PgItemEffect<PgItemAttributeLink>, IPgItemAttributeLink
    {
        public PgItemAttributeLink(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgItemAttributeLink CreateItem(byte[] data, ref int offset)
        {
            return new PgItemAttributeLink(data, ref offset);
        }

        public override string Key { get { return null; } }
        public float AttributeEffect { get { return (float)GetDouble(4); } }
        public IPgAttribute Link { get { return GetObject(8, ref _Link, PgAttribute.CreateNew); } } private IPgAttribute _Link;
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }
    }
}
