namespace PgJsonObjects
{
    public class PgQuestObjectiveInteractionFlag : GenericPgObject<PgQuestObjectiveInteractionFlag>, IPgQuestObjectiveInteractionFlag
    {
        public PgQuestObjectiveInteractionFlag(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveInteractionFlag CreateItem(byte[] data, int offset)
        {
            return new PgQuestObjectiveInteractionFlag(data, offset);
        }

        public string InteractionFlag { get { return GetString(0); } }
        public string InteractionTarget { get { return GetString(4); } }
    }
}
