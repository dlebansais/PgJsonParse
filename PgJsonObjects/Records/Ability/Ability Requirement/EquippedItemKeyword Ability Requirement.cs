namespace PgJsonObjects
{
    public class EquippedItemKeywordAbilityRequirement : AbilityRequirement
    {
        public EquippedItemKeywordAbilityRequirement(string RawKeyword, double? RawCount, ParseErrorInfo ErrorInfo)
        {
            AbilityKeyword ParsedKeyword;
            StringToEnumConversion<AbilityKeyword>.TryParse(RawKeyword, out ParsedKeyword, ErrorInfo);
            Keyword = ParsedKeyword;
            Count = RawCount.HasValue ? RawCount.Value : 0;
        }

        public AbilityKeyword Keyword { get; private set; }
        public double Count { get; private set; }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "EquippedItemKeyword");

            Generator.CloseObject();
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
