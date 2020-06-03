namespace Translator
{
    using PgJsonObjects;

    public class ParserAttribute : Parser
    {
        public override object CreateItem()
        {
            return new PgAttribute();
        }
    }
}
