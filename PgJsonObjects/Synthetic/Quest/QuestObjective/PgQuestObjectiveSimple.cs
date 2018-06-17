namespace PgJsonObjects
{
    public class PgQuestObjectiveSimple : GenericPgObject, IPgQuestObjectiveSimple
    {
        public PgQuestObjectiveSimple(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgQuestObjectiveSimple(data, offset);
        }
    }
}
