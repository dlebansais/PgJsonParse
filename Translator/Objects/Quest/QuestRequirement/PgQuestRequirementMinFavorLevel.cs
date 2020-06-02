namespace PgJsonObjects
{
    public class PgQuestRequirementMinFavorLevel : PgQuestRequirement
    {
        public PgNpc FavorNpc { get; set; }
        public bool IsEmpty { get { return RawIsEmpty.HasValue && RawIsEmpty.Value; } }
        public bool? RawIsEmpty { get; set; }
        public Favor FavorLevel { get; set; }
        public string FavorNpcId { get; set; } = string.Empty;
        public string FavorNpcName { get; set; } = string.Empty;
        public MapAreaName FavorNpcArea { get; set; }
    }
}
