namespace Translator
{
    using PgJsonObjects;

    public class ParserPower : Parser
    {
        public override object CreateItem()
        {
            return new PgPower();
        }
    }
}
