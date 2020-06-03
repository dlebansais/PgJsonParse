namespace Translator
{
    using PgJsonObjects;

    public class ParserReport : Parser
    {
        public override object CreateItem()
        {
            return new PgReport();
        }
    }
}
