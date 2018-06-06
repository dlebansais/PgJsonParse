using System.Collections.Generic;

namespace PgJsonObjects
{
    public class InHotspotAbilityRequirement : AbilityRequirement
    {
        public InHotspotAbilityRequirement(string RawName)
        {
            Name = RawName;
        }

        public string Name { get; private set; }
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.InHotspot) } },
            { "Name", new FieldParser() {
                Type = FieldType.String,
                GetString  = () => Name } },
        }; } }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "InHotspot");
            Generator.AddString("Name", Name);
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Name);

                return Result;
            }
        }
        #endregion
    }
}
