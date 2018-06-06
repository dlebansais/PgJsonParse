using System.Collections.Generic;

namespace PgJsonObjects
{
    public class InternalAbilityRequirement : AbilityRequirement
    {
        public InternalAbilityRequirement(AbilityItemKeyword Keyword)
        {
            this.Keyword = Keyword;
        }

        public AbilityItemKeyword Keyword { get; private set; }
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
        }; } }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
        }
        #endregion
    }
}
