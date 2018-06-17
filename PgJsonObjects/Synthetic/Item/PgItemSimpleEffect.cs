namespace PgJsonObjects
{
    public class PgItemSimpleEffect : GenericPgObject, IPgItemSimpleEffect
    {
        public PgItemSimpleEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string Description { get { return GetString(0); } }
    }
}
