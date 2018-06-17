namespace PgJsonObjects
{
    public class PgItemAttributeLink : GenericPgObject, IPgItemAttributeLink
    {
        public PgItemAttributeLink(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public float AttributeEffect { get { return (float)GetDouble(4); } }
        public Attribute Link { get { return GetObject(8, ref _Link); } } private Attribute _Link;
    }
}
