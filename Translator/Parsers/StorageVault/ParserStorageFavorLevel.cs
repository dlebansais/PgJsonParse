namespace Translator
{
    using PgJsonObjects;

    public class ParserStorageFavorLevel : Parser
    {
        public override object CreateItem()
        {
            return new PgStorageFavorLevel();
        }
    }
}
