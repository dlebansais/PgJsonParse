namespace PgJsonObjects
{
    public class PgInteractionFlagSetQuestRequirement : GenericPgObject, IPgInteractionFlagSetQuestRequirement
    {
        public PgInteractionFlagSetQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgInteractionFlagSetQuestRequirement(data, offset);
        }

        public string InteractionFlag { get { return GetString(4); } }
    }
}
