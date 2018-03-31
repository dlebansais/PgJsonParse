namespace PgJsonObjects
{
    public class PetCountAbilityRequirement : AbilityRequirement
    {
        public PetCountAbilityRequirement(string RawPetTypeTag, double? RawMaxCount, ParseErrorInfo ErrorInfo)
        {
            RecipeKeyword ParsedPetTypeTag;
            StringToEnumConversion<RecipeKeyword>.TryParse(RawPetTypeTag, out ParsedPetTypeTag, ErrorInfo);
            PetTypeTag = ParsedPetTypeTag;

            MaxCount = RawMaxCount.HasValue ? RawMaxCount.Value : 0;
        }

        public RecipeKeyword PetTypeTag { get; private set; }
        public double MaxCount { get; private set; }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "PetCount");
            Generator.AddEnum("PetTypeTag", PetTypeTag);
            Generator.AddDouble("MaxCount", MaxCount);
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, TextMaps.RecipeKeywordTextMap[PetTypeTag]);

                return Result;
            }
        }
        #endregion
    }
}
