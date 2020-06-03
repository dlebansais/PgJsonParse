namespace Translator
{
    using PgJsonObjects;

    public class ParserPowerTier : Parser
    {
        public override object CreateItem()
        {
            return new PgPowerTier();
        }
    }
}
