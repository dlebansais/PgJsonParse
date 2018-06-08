using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RunTimeBehaviorRuleSetQuestRequirement : QuestRequirement
    {
        public RunTimeBehaviorRuleSetQuestRequirement(OtherRequirementType OtherRequirementType, string RequirementRule)
            : base(OtherRequirementType)
        {
            this.RequirementRule = RequirementRule;

            if (RequirementRule == "ChristmasQuests")
                Rule = "During Christmas Quests";
            else
                Rule = RequirementRule;
        }

        public string RequirementRule { get; private set; }
        public string Rule { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "Rule", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RequirementRule } },
        }; } }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "RuntimeBehaviorRuleSet");

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (Rule != null)
                    Result = Rule;

                return Result;
            }
        }
        #endregion
    }
}
