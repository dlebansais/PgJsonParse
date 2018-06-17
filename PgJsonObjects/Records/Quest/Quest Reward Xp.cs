namespace PgJsonObjects
{
    public class QuestRewardXp : IPgQuestRewardXp
    {
        public Skill Skill { get; set; }
        public int Xp { get { return RawXp.HasValue ? RawXp.Value : 0; } }
        public int? RawXp { get; set; }
        public PowerSkill RawSkill { get; set; }
        public bool IsSkillParsed { get; set; }
    }
}
