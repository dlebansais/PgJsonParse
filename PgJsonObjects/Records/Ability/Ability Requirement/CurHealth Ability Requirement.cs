using System.Collections.Generic;

namespace PgJsonObjects
{
    public class CurHealthAbilityRequirement : AbilityRequirement
    {
        public CurHealthAbilityRequirement(double? RawHealth)
        {
            Health = RawHealth.Value;
        }

        public double Health { get; private set; }
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.CurHealth) } },
            { "Health", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => Health } },
        }; } }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "CurHealth");
            Generator.AddDouble("Health", Health);
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, "CurHealth");

                return Result;
            }
        }
        #endregion
    }
}
