namespace PgObjects
{
    public class PgQuestRewardFavor : PgQuestReward
    {
        public int Favor { get { return RawFavor.HasValue ? RawFavor.Value : 0; } }
        public int? RawFavor { get; set; }
        public PgNpcLocation? FavorNpc { get; set; }
    }
}
