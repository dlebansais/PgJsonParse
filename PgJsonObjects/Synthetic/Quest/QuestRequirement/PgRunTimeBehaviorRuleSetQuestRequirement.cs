using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRunTimeBehaviorRuleSetQuestRequirement : PgQuestRequirement<PgRunTimeBehaviorRuleSetQuestRequirement>, IPgRunTimeBehaviorRuleSetQuestRequirement
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

        public string RequirementRule { get { return GetString(PropertiesOffset + 0); } }
        public string Rule { get { return GetString(PropertiesOffset + 4); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "Rule", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RequirementRule } },
        }; } }
    }
}
