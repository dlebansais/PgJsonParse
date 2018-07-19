using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirementInternal : AbilityRequirement, IGenericPgObject
    {
        public AbilityRequirementInternal(AbilityItemKeyword keyword)
        {
            Keyword = keyword;
        }

        public virtual void Init()
        {
        }

        public void AddLinkBackCollection(IPgBackLinkableCollection LinkBackCollection)
        {
        }

        public AbilityItemKeyword Keyword { get; private set; }
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
        }; } }
    }
}
