namespace PgObjects
{
    public class PgQuestObjectiveCompleteQuest : PgQuestObjective
    {
        public PgQuest TargetQuest { get; set; } = null!;
    }
}
