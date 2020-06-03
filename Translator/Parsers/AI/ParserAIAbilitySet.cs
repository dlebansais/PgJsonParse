namespace Translator
{
    using PgJsonObjects;

    public class ParserAIAbilitySet : Parser
    {
        public override object CreateItem()
        {
            return new PgAIAbilitySet();
        }
    }
}
