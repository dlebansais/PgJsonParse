namespace Translator
{
    using PgJsonObjects;

    public class ParserNpcPreference : Parser
    {
        public override object CreateItem()
        {
            return new PgNpcPreference();
        }
    }
}
