namespace PgObjects
{
    using System.Collections.Generic;

    public class PgQuestObjectiveMultipleInteractionFlags : PgQuestObjective
    {
        public List<InteractionFlag> InteractionFlagList { get; set; } = new List<InteractionFlag>();
    }
}
