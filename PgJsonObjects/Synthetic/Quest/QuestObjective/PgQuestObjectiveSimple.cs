namespace PgJsonObjects
{
    public class PgQuestObjectiveSimple : GenericPgObject, IPgQuestObjectiveSimple
    {
        public PgQuestObjectiveSimple(byte[] data, int offset)
            : base(data, offset)
        {
        }
    }
}
