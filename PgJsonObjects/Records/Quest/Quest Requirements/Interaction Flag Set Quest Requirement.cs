using System.Collections.Generic;

namespace PgJsonObjects
{
    public class InteractionFlagSetQuestRequirement : QuestRequirement
    {
        public InteractionFlagSetQuestRequirement(OtherRequirementType OtherRequirementType, string InteractionFlag)
            : base(OtherRequirementType)
        {
            this.InteractionFlag = InteractionFlag;
        }

        public string InteractionFlag { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "InteractionFlag", new FieldParser() {
                Type = FieldType.String,
                GetString = () => InteractionFlag } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (InteractionFlag != null)
                    Result = InteractionFlag;

                return Result;
            }
        }
        #endregion
    }
}
