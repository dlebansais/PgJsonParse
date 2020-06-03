namespace Translator
{
    using PgJsonObjects;

    public class ParserRecipeItem : Parser
    {
        public override object CreateItem()
        {
            return new PgRecipeItem();
        }
    }
}
