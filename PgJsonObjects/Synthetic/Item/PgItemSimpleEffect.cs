namespace PgJsonObjects
{
    public class PgItemSimpleEffect : GenericPgObject<PgItemSimpleEffect>, IPgItemSimpleEffect
    {
        public PgItemSimpleEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgItemSimpleEffect CreateItem(byte[] data, int offset)
        {
            return new PgItemSimpleEffect(data, offset);
        }

        public string Description { get { return GetString(0); } }
    }
}
