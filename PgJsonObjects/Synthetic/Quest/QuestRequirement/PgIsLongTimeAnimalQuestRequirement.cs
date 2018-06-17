namespace PgJsonObjects
{
    public class PgIsLongTimeAnimalQuestRequirement : GenericPgObject<PgIsLongTimeAnimalQuestRequirement>, IPgIsLongTimeAnimalQuestRequirement
    {
        public PgIsLongTimeAnimalQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgIsLongTimeAnimalQuestRequirement CreateItem(byte[] data, int offset)
        {
            return new PgIsLongTimeAnimalQuestRequirement(data, offset);
        }
    }
}
