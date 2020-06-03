namespace Translator
{
    using PgJsonObjects;

    public class ParserLoreBookInfoCategory : Parser
    {
        public override object CreateItem()
        {
            return new PgLoreBookInfoCategory();
        }
    }
}
