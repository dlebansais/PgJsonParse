namespace Translator
{
    using PgJsonObjects;

    public class ParserQuest : Parser
    {
        public override object CreateItem()
        {
            return new PgQuest();
        }
    }
}
