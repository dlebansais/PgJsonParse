namespace PgJsonObjects
{
    public class PgAdvancementTable : MainPgObject<PgAdvancementTable>, IPgAdvancementTable
    {
        public PgAdvancementTable(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 4;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgAdvancementTable CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAdvancementTable CreateNew(byte[] data, ref int offset)
        {
            return new PgAdvancementTable(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public string InternalName { get { return GetString(4); } }
    }
}
