namespace PgJsonObjects
{
    public class PgAdvancementTable : GenericPgObject, IPgAdvancementTable
    {
        public PgAdvancementTable(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string InternalName { get { return GetString(0); } }
    }
}
