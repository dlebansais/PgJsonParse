namespace Translator
{
    using PgJsonObjects;

    public class ParserItemBehavior : Parser
    {
        public override object CreateItem()
        {
            return new PgItemBehavior();
        }
    }
}
