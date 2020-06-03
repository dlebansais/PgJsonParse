namespace Translator
{
    using PgJsonObjects;

    public class ParserLevelCapInteraction : Parser
    {
        public override object CreateItem()
        {
            return new PgLevelCapInteraction();
        }
    }
}
