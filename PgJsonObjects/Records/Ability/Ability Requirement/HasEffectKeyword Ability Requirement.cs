namespace PgJsonObjects
{
    public class HasEffectKeywordAbilityRequirement : AbilityRequirement
    {
        public HasEffectKeywordAbilityRequirement(string RawKeyword, ParseErrorInfo ErrorInfo)
        {
            AbilityKeyword ParsedKeyword;
            StringToEnumConversion<AbilityKeyword>.TryParse(RawKeyword, out ParsedKeyword, ErrorInfo);
            Keyword = ParsedKeyword;
        }

        public AbilityKeyword Keyword { get; private set; }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "HasEffectKeyword");

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
