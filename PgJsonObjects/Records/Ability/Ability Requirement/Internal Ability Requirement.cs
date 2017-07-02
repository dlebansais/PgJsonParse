namespace PgJsonObjects
{
    public class InternalAbilityRequirement : AbilityRequirement
    {
        public InternalAbilityRequirement(AbilityItemKeyword Keyword)
        {
            this.Keyword = Keyword;
        }

        public AbilityItemKeyword Keyword { get; private set; }
    }
}
