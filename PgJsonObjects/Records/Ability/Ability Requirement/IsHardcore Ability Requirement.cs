using System.Collections.Generic;

namespace PgJsonObjects
{
    public class IsHardcoreAbilityRequirement : AbilityRequirement
    {
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.IsHardcore) } },
        }; } }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "IsHardcore");
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, "Is Hardcore");

                return Result;
            }
        }
        #endregion
    }
}
