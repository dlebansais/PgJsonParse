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

        public float AttributeEffect { get { return (float)GetDouble(4); } }
        public IPgAttribute Link { get { return GetObject(8, ref _Link, PgAttribute.CreateNew); } } private IPgAttribute _Link;
    }
}
