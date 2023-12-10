namespace PgObjects
{
    public class PgQuestRequirementAttributeMatchesScriptAtomic : PgQuestRequirement
    {
        public string Attribute { get; set; } = string.Empty;
        public string ScriptAtomicInt { get; set; } = string.Empty;
    }
}
