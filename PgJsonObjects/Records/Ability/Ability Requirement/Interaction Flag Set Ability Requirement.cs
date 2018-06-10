using System.Collections.Generic;

namespace PgJsonObjects
{
    public class InteractionFlagSetAbilityRequirement : AbilityRequirement
    {
        public InteractionFlagSetAbilityRequirement(string RawInteractionFlag)
        {
            InteractionFlag = RawInteractionFlag;
        }

        public string InteractionFlag { get; private set; }
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.InteractionFlagSet) } },
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

                AddWithFieldSeparator(ref Result, InteractionFlag);

                return Result;
            }
        }
        #endregion
    }
}
