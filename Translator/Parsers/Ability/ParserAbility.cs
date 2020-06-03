namespace Translator
{
    using PgJsonObjects;

    public class ParserAbility : Parser
    {
        public override object CreateItem()
        {
            return new PgAbility();
        }
    }
}
