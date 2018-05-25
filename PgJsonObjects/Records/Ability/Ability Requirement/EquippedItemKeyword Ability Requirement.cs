namespace PgJsonObjects
{
    public class EquippedItemKeywordAbilityRequirement : AbilityRequirement
    {
        public EquippedItemKeywordAbilityRequirement(string RawKeyword, int? RawMinCount, ParseErrorInfo ErrorInfo)
        {
            AbilityKeyword ParsedKeyword;
            StringToEnumConversion<AbilityKeyword>.TryParse(RawKeyword, out ParsedKeyword, ErrorInfo);
            Keyword = ParsedKeyword;
            MinCount = RawMinCount.HasValue ? RawMinCount.Value : 0;
        }

        public AbilityKeyword Keyword { get; private set; }
        public int MinCount { get; private set; }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "EquippedItemKeyword");
            Generator.AddEnum("Keyword", Keyword);
            Generator.AddInteger("MinCount", MinCount);
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, TextMaps.AbilityKeywordTextMap[Keyword]);

                return Result;
            }
        }
        #endregion
    }
}
