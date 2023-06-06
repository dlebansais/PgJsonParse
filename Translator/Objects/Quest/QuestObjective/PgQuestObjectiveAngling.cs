namespace PgObjects
{
    public class PgQuestObjectiveAngling : PgQuestObjective
    {
        public AllowedFishingZone AllowedFishingZone { get; set; }
        public FishConfig FishConfig { get; set; }
        public string? Item_Key { get; set; }
    }
}
