namespace Translator
{
    using PgJsonObjects;

    public class ParserQuestRequirement : Parser
    {
        public override object CreateItem()
        {
            return new PgQuestRequirement();
        }
    }
}
