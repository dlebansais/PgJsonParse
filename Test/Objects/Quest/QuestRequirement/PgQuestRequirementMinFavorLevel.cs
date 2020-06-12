namespace PgObjects
{
    public class PgQuestRequirementMinFavorLevel : PgQuestRequirement
    {
        public PgNpcLocation FavorNpc { get; set; }
        public Favor FavorLevel { get; set; }
    }
}
