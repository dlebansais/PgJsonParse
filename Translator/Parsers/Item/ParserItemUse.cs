namespace Translator
{
    using PgJsonObjects;

    public class ParserItemUse : Parser
    {
        public override object CreateItem()
        {
            return new PgItemUse();
        }
    }
}
