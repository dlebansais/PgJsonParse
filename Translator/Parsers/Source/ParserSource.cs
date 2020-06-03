namespace Translator
{
    using PgJsonObjects;

    public class ParserSource : Parser
    {
        public override object CreateItem()
        {
            return new PgSource();
        }
    }
}
