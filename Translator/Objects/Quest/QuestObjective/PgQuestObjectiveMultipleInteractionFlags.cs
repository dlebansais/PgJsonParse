namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgQuestObjectiveMultipleInteractionFlags : PgQuestObjective
    {
        public List<string> InteractionFlagList { get; set; } = new List<string>();
    }
}
