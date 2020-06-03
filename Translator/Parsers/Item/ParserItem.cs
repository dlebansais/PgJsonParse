namespace Translator
{
    using PgJsonObjects;

    public class ParserItem : Parser
    {
        public override object CreateItem()
        {
            return new PgItem();
        }
    }
}
