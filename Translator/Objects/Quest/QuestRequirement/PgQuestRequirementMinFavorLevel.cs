namespace PgObjects
{
    public class PgQuestRequirementMinFavorLevel : PgQuestRequirement
    {
        public PgNpcLocation FavorNpc { get; set; }
        public Favor FavorLevel { get; set; }
        public int MinFavor { get { return RawMinFavor.HasValue ? RawMinFavor.Value : 0; } }
        public int? RawMinFavor { get; set; }
    }
}
