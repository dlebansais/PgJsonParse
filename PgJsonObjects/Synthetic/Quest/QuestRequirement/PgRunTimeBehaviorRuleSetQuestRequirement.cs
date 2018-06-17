namespace PgJsonObjects
{
    public class PgRunTimeBehaviorRuleSetQuestRequirement : GenericPgObject, IPgRunTimeBehaviorRuleSetQuestRequirement
    {
        public PgRunTimeBehaviorRuleSetQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string RequirementRule { get { return GetString(4); } }
        public string Rule { get { return GetString(8); } }
    }
}
