namespace PgJsonObjects
{
    public class PgAdvancementTable : MainPgObject<PgAdvancementTable>, IPgAdvancementTable
    {
        public PgAdvancementTable(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAdvancementTable CreateItem(byte[] data, int offset)
        {
            return new PgAdvancementTable(data, offset);
        }

        public string InternalName { get { return GetString(0); } }
    }
}
