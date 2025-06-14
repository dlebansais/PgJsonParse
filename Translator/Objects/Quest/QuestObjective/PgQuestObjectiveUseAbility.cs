using System.Collections.Generic;

namespace PgObjects
{
    public class PgQuestObjectiveUseAbility : PgQuestObjective
    {
        public AbilityKeyword TargetKeyword { get; set; }
        public AbilityKeyword AbilityKeyword { get; set; }
        public PgQuestObjectiveRequirementCollection QuestRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
        public List<InteractionFlag> InteractionFlagList { get; set; } = new List<InteractionFlag>();
    }
}
