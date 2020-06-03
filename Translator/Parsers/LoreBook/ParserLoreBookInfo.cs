namespace Translator
{
    using PgJsonObjects;

    public class ParserLoreBookInfo : Parser
    {
        public override object CreateItem()
        {
            return new PgLoreBookInfo();
        }
    }
}
