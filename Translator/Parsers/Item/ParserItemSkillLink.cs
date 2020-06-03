namespace Translator
{
    using PgJsonObjects;

    public class ParserItemSkillLink : Parser
    {
        public override object CreateItem()
        {
            return new PgItemSkillLink();
        }
    }
}
