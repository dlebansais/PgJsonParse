namespace PgJsonObjects
{
    public class PgQuestObjectiveSimple : GenericPgObject<PgQuestObjectiveSimple>, IPgQuestObjectiveSimple
    {
        public PgQuestObjectiveSimple(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveSimple CreateItem(byte[] data, int offset)
        {
            return new PgQuestObjectiveSimple(data, offset);
        }
    }
}
