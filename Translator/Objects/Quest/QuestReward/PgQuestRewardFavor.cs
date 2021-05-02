namespace PgObjects
{
    public class PgQuestRewardFavor : PgQuestReward
    {
        public int Favor { get { return RawFavor.HasValue ? RawFavor.Value : 0; } }
        public int? RawFavor { get; set; }
        public PgNpcLocation FavorNpcLocation { get; set; } = null!;
    }
}
