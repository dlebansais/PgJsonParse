using System.Collections.Generic;

namespace PgJsonObjects
{
    public class IsLongTimeAnimalQuestRequirement : QuestRequirement
    {
        public IsLongTimeAnimalQuestRequirement(OtherRequirementType OtherRequirementType)
            : base(OtherRequirementType)
        {
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                return Result;
            }
        }
        #endregion
    }
}
