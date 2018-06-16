namespace PgJsonObjects
{
    public class PgInteractionFlagSetQuestRequirement : GenericPgObject, IPgInteractionFlagSetQuestRequirement
    {
        public PgInteractionFlagSetQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string InteractionFlag { get { return GetString(0); } }
    }
}
