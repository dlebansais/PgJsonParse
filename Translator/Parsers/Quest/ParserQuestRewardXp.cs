namespace Translator
{
    using PgJsonObjects;

    public class ParserQuestRewardXp : Parser
    {
        public override object CreateItem()
        {
            return new PgQuestRewardXp();
        }
    }
}
