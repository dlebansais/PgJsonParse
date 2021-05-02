namespace PgObjects
{
    public class PgQuestObjectiveRequirementActiveCombatSkill : PgQuestObjectiveRequirement
    {
        public PgSkill Skill { get; set; } = null!;
    }
}
