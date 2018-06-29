namespace PgJsonObjects
{
    public class PgRunTimeBehaviorRuleSetQuestRequirement : GenericPgObject<PgRunTimeBehaviorRuleSetQuestRequirement>, IPgRunTimeBehaviorRuleSetQuestRequirement
    {
        public PgRunTimeBehaviorRuleSetQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgRunTimeBehaviorRuleSetQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgRunTimeBehaviorRuleSetQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgRunTimeBehaviorRuleSetQuestRequirement(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public string RequirementRule { get { return GetString(8); } }
        public string Rule { get { return GetString(12); } }
    }
}
