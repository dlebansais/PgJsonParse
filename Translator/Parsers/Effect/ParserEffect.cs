namespace Translator
{
    using PgJsonObjects;

    public class ParserEffect : Parser
    {
        public override object CreateItem()
        {
            return new PgEffect();
        }
    }
}
