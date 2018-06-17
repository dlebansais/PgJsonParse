namespace PgJsonObjects
{
    public class PgInteractionFlagSetQuestRequirement : GenericPgObject<PgInteractionFlagSetQuestRequirement>, IPgInteractionFlagSetQuestRequirement
    {
        public PgInteractionFlagSetQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgInteractionFlagSetQuestRequirement CreateItem(byte[] data, int offset)
        {
            return new PgInteractionFlagSetQuestRequirement(data, offset);
        }

        public string InteractionFlag { get { return GetString(4); } }
    }
}
