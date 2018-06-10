using System.Collections.Generic;

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
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.PetCount) } },
            { "PetTypeTag", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<RecipeKeyword>.ToString(PetTypeTag) } },
            { "MaxCount", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => MaxCount } },
        }; } }

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
