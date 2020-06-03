namespace Translator
{
    using PgJsonObjects;

    public class ParserSkill : Parser
    {
        public override object CreateItem()
        {
            return new PgSkill();
        }
    }
}
