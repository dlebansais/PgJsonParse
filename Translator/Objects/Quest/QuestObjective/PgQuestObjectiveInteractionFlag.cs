namespace PgObjects
{
    public class PgQuestObjectiveInteractionFlag : PgQuestObjective
    {
        public string Target { get; set; } = string.Empty;
        public string InteractionFlag { get; set; } = string.Empty;
    }
}
