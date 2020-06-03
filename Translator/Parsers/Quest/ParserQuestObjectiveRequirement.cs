namespace Translator
{
    using PgJsonObjects;

    public class ParserQuestObjectiveRequirement : Parser
    {
        public override object CreateItem()
        {
            return new PgQuestObjectiveRequirement();
        }
    }
}
