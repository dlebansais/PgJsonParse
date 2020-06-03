namespace Translator
{
    using PgJsonObjects;

    public class ParserLoreBook : Parser
    {
        public override object CreateItem()
        {
            return new PgLoreBook();
        }
    }
}
