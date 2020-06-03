namespace Translator
{
    using PgJsonObjects;

    public class ParserNpc : Parser
    {
        public override object CreateItem()
        {
            return new PgNpc();
        }
    }
}
