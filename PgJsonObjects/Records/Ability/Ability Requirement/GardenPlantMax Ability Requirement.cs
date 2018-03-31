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
