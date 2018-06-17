namespace PgJsonObjects
{
    public class PgItemAttributeLink : GenericPgObject<PgItemAttributeLink>, IPgItemAttributeLink
    {
        public PgItemAttributeLink(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgItemAttributeLink CreateItem(byte[] data, int offset)
        {
            return new PgItemAttributeLink(data, offset);
        }

        public float AttributeEffect { get { return (float)GetDouble(4); } }
        public Attribute Link { get { return GetObject(8, ref _Link); } } private Attribute _Link;
    }
}
