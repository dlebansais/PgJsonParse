namespace Translator
{
    using PgJsonObjects;

    public class ParserStorageRequirement : Parser
    {
        public override object CreateItem()
        {
            return new PgStorageRequirement();
        }
    }
}
