namespace Translator
{
    using PgJsonObjects;

    public class ParserPlayerTitle : Parser
    {
        public override object CreateItem()
        {
            return new PgPlayerTitle();
        }
    }
}
