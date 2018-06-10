using System.Collections.Generic;

namespace PgJsonObjects
{
    public class IsVolunteerGuideAbilityRequirement : AbilityRequirement
    {
        #region Json Reconstruction
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.IsVolunteerGuide) } },
        }; } }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, "Is Volunteer Guide");

                return Result;
            }
        }
        #endregion
    }
}
