namespace PgJsonObjects
{
    public class SkillRewardUnlock : SkillRewardCommon
    {
        public SkillRewardUnlock(int RewardLevel, IPgSkill Skill, int Level)
            : base(RewardLevel)
        {
            this.Skill = Skill;
            this.Level = Level;
        }

        public IPgSkill Skill { get; private set; }
        public int Level { get; private set; }

        public override string TextContent { get { return Skill.Name; } }
    }
}
