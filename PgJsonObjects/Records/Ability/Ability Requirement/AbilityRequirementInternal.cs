using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirementInternal : AbilityRequirement, IGenericPgObject
    {
        public AbilityRequirementInternal(AbilityItemKeyword Keyword)
        {
            this.Keyword = Keyword;
        }

        public virtual void Init()
        {
        }

        public AbilityItemKeyword Keyword { get; private set; }
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
        }; } }
    }
}
