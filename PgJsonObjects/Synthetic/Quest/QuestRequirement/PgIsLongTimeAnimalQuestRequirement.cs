namespace PgJsonObjects
{
    public class PgIsLongTimeAnimalQuestRequirement : GenericPgObject, IPgIsLongTimeAnimalQuestRequirement
    {
        public PgIsLongTimeAnimalQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgIsLongTimeAnimalQuestRequirement(data, offset);
        }
    }
}
