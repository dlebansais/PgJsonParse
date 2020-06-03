namespace Translator
{
    using PgJsonObjects;

    public class ParserPowerEffect : Parser
    {
        public override object CreateItem()
        {
            return new PgPowerEffect();
        }
    }
}
