using System.Collections.Generic;

namespace PgJsonObjects
{
    public class InGraveyardAbilityRequirement : AbilityRequirement
    {
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.InGraveyard) } },
        }; } }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "InGraveyard");
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, "In Graveyard");

                return Result;
            }
        }
        #endregion
    }
}
