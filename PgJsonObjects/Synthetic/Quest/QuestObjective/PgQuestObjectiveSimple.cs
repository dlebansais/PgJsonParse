namespace PgJsonObjects
{
    public class PgQuestObjectiveSimple : GenericPgObject<PgQuestObjectiveSimple>, IPgQuestObjectiveSimple
    {
        public PgQuestObjectiveSimple(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveSimple CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveSimple CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveSimple(data, ref offset);
        }
    }
}
