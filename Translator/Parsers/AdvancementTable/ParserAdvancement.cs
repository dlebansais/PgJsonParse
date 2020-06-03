namespace Translator
{
    using PgJsonObjects;

    public class ParserAdvancement : Parser
    {
        public override object CreateItem()
        {
            return new PgAdvancement();
        }
    }
}
