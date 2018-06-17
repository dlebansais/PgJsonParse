namespace PgJsonObjects
{
    public class PgRunTimeBehaviorRuleSetQuestRequirement : GenericPgObject<PgRunTimeBehaviorRuleSetQuestRequirement>, IPgRunTimeBehaviorRuleSetQuestRequirement
    {
        public PgRunTimeBehaviorRuleSetQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgRunTimeBehaviorRuleSetQuestRequirement CreateItem(byte[] data, int offset)
        {
            return new PgRunTimeBehaviorRuleSetQuestRequirement(data, offset);
        }

        public string RequirementRule { get { return GetString(4); } }
        public string Rule { get { return GetString(8); } }
    }
}
