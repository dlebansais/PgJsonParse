namespace Translator
{
    using PgJsonObjects;

    public class ParserStorageVault : Parser
    {
        public override object CreateItem()
        {
            return new PgStorageVault();
        }
    }
}
