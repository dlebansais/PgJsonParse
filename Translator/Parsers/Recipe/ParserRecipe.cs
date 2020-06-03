namespace Translator
{
    using PgJsonObjects;

    public class ParserRecipe : Parser
    {
        public override object CreateItem()
        {
            return new PgRecipe();
        }
    }
}
