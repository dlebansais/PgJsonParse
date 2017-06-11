namespace PgJsonObjects
{
    public class QuestRewardXp
    {
        public PowerSkill Skill { get; set; }
        public Skill ConnectedSkill { get; set; }
        public bool IsSkillParsed { get; set; }
        public int Xp { get; set; }
    }
}
