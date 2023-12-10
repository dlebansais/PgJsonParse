namespace PgObjects
{
    public class PgQuestRewardEffect : PgQuestReward
    {
        public string? Effect_Key { get; set; }
        public EffectKeyword Keyword { get; set; }
        public string? Special { get; set; }
    }
}
