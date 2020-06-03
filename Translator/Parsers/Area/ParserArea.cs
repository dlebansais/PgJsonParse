namespace Translator
{
    using PgJsonObjects;

    public class ParserArea : Parser
    {
        public override object CreateItem()
        {
            return new PgArea();
        }
    }
}
