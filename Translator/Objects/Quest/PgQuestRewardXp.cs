namespace PgJsonObjects
{
    public class PgQuestRewardXp
    {
        public PgSkill Skill { get; set; }
        public int Xp { get { return RawXp.HasValue ? RawXp.Value : 0; } }
        public int? RawXp { get; set; }
    }
}
