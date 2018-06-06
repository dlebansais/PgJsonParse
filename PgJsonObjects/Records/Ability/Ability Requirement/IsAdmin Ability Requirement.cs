using System.Collections.Generic;

namespace PgJsonObjects
{
    public class IsAdminAbilityRequirement : AbilityRequirement
    {
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.IsAdmin) } },
        }; } }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "IsAdmin");
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, "Is Admin");

                return Result;
            }
        }
        #endregion
    }
}
