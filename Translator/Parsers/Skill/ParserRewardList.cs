namespace Translator
{
    using PgJsonObjects;

    public class ParserRewardList : Parser
    {
        public override object CreateItem()
        {
            return new PgRewardList();
        }
    }
}
