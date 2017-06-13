namespace PgJsonObjects
{
    public class SkillRewardUnlock : SkillRewardCommon
    {
        public SkillRewardUnlock(int RewardLevel, Skill Skill, int Level)
            : base(RewardLevel)
        {
            this.Skill = Skill;
            this.Level = Level;
        }

        public Skill Skill { get; private set; }
        public int Level { get; private set; }

        public override string TextContent { get { return Skill.Name; } }
    }
}
