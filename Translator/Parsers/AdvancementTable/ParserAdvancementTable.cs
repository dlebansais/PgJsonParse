namespace Translator
{
    using PgJsonObjects;

    public class ParserAdvancementTable : Parser
    {
        public override object CreateItem()
        {
            return new PgAdvancementTable();
        }
    }
}
