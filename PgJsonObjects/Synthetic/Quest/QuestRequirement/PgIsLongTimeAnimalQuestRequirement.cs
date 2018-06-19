namespace PgJsonObjects
{
    public class PgIsLongTimeAnimalQuestRequirement : GenericPgObject<PgIsLongTimeAnimalQuestRequirement>, IPgIsLongTimeAnimalQuestRequirement
    {
        public PgIsLongTimeAnimalQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgIsLongTimeAnimalQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgIsLongTimeAnimalQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgIsLongTimeAnimalQuestRequirement(data, ref offset);
        }
    }
}
