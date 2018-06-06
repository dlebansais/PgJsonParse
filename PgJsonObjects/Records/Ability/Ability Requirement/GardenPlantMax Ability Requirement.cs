using System.Collections.Generic;

namespace PgJsonObjects
{
    public class GardenPlantMaxAbilityRequirement : AbilityRequirement
    {
        public GardenPlantMaxAbilityRequirement(string RawTypeTag, int? RawMax, ParseErrorInfo ErrorInfo)
        {
            AbilityTypeTag ParsedTypeTag;
            StringToEnumConversion<AbilityTypeTag>.TryParse(RawTypeTag, out ParsedTypeTag, ErrorInfo);
            TypeTag = ParsedTypeTag;
            Max = RawMax.HasValue ? RawMax.Value : 0;
        }

        public AbilityTypeTag TypeTag { get; private set; }
        public int Max { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.GardenPlantMax) } },
            { "TypeTag", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<AbilityTypeTag>.ToString(TypeTag) } },
            { "Max", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => Max } },
        }; } }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "GardenPlantMax");
            Generator.AddEnum("TypeTag", TypeTag);
            Generator.AddInteger("Max", Max);
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, TextMaps.AbilityTypeTagTextMap[TypeTag]);

                return Result;
            }
        }
        #endregion
    }
}
