using System.Collections.Generic;

namespace PgJsonObjects
{
    public class HasEffectKeywordQuestRequirement : QuestRequirement
    {
        public HasEffectKeywordQuestRequirement(OtherRequirementType OtherRequirementType, EffectKeyword RequirementKeyword)
            : base(OtherRequirementType)
        {
            Keyword = RequirementKeyword;
        }

        public EffectKeyword Keyword { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "Keyword", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<EffectKeyword>.ToString(Keyword, null, EffectKeyword.Internal_None) } },
        }; } }

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
