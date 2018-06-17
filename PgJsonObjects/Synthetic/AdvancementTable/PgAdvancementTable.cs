namespace PgJsonObjects
{
    public class PgAdvancementTable : MainPgObject, IPgAdvancementTable
    {
        public PgAdvancementTable(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAdvancementTable(data, offset);
        }

        public string InternalName { get { return GetString(0); } }
    }
}
