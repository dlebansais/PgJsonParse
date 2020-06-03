namespace Translator
{
    using PgJsonObjects;

    public class ParserRecipeCost : Parser
    {
        public override object CreateItem()
        {
            return new PgRecipeCost();
        }
    }
}
