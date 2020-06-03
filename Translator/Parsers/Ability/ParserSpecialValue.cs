namespace Translator
{
    using PgJsonObjects;

    public class ParserSpecialValue : Parser
    {
        public override object CreateItem()
        {
            return new PgSpecialValue();
        }
    }
}
