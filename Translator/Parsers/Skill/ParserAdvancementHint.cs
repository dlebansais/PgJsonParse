namespace Translator
{
    using PgJsonObjects;

    public class ParserAdvancementHint : Parser
    {
        public override object CreateItem()
        {
            return new PgAdvancementHint();
        }
    }
}
