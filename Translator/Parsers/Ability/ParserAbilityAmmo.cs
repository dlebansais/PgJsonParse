namespace Translator
{
    using PgJsonObjects;

    public class ParserAbilityAmmo : Parser
    {
        public override object CreateItem()
        {
            return new PgAbilityAmmo();
        }
    }
}
