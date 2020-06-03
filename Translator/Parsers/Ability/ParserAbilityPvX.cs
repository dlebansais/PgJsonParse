namespace Translator
{
    using PgJsonObjects;

    public class ParserAbilityPvX : Parser
    {
        public override object CreateItem()
        {
            return new PgAbilityPvX();
        }
    }
}
