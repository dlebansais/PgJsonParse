namespace Translator
{
    using PgJsonObjects;

    public class ParserDirectedGoal : Parser
    {
        public override object CreateItem()
        {
            return new PgDirectedGoal();
        }
    }
}
