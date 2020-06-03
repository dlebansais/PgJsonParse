namespace Translator
{
    using PgJsonObjects;

    public class ParserDoT : Parser
    {
        public override object CreateItem()
        {
            return new PgDoT();
        }
    }
}
