namespace Translator
{
    using PgJsonObjects;

    public class ParserXpTable : Parser
    {
        public override object CreateItem()
        {
            return new PgXpTable();
        }
    }
}
