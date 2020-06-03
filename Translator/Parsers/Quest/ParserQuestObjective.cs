namespace Translator
{
    using PgJsonObjects;

    public class ParserQuestObjective : Parser
    {
        public override object CreateItem()
        {
            return new PgQuestObjective();
        }
    }
}
