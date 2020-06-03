namespace Translator
{
    using PgJsonObjects;

    public class ParserAIAbility : Parser
    {
        public override object CreateItem()
        {
            return new PgAIAbility();
        }
    }
}
