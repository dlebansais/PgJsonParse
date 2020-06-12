namespace PgObjects
{
    public class PgQuestObjectiveInteractionFlag : PgQuestObjective
    {
        public string InteractionTarget { get; set; } = string.Empty;
        public string InteractionFlag { get; set; } = string.Empty;
    }
}
