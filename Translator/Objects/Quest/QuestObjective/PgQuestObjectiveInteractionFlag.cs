namespace PgObjects
{
    public class PgQuestObjectiveInteractionFlag : PgQuestObjective
    {
        public string Target { get; set; } = string.Empty;
        public InteractionFlag InteractionFlag { get; set; }
    }
}
