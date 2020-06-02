namespace PgJsonObjects
{
    public class PgQuestObjectiveAnatomy : PgQuestObjective
    {
        public PgSkill Skill { get; set; }
        public PgSkill AnatomyType { get; set; }
        public string Target { get; set; }
    }
}
