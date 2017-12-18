namespace PgJsonObjects
{
    public class HasEffectKeywordQuestRequirement : QuestRequirement
    {
        public HasEffectKeywordQuestRequirement(EffectKeyword RequirementKeyword)
        {
            Keyword = RequirementKeyword;
        }

        public EffectKeyword Keyword { get; private set; }

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

                AddWithFieldSeparator(ref Result, TextMaps.EffectKeywordTextMap[Keyword]);

                return Result;
            }
        }
        #endregion
    }
}
