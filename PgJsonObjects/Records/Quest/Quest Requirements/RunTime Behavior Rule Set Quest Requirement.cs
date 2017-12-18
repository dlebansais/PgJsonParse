namespace PgJsonObjects
{
    public class RunTimeBehaviorRuleSetQuestRequirement : QuestRequirement
    {
        public RunTimeBehaviorRuleSetQuestRequirement(string RequirementRule)
        {
            if (RequirementRule == "ChristmasQuests")
                Rule = "During Christmas Quests";
            else
                Rule = RequirementRule;
        }

        public string Rule { get; private set; }

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
