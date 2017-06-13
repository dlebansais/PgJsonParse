namespace PgJsonObjects
{
    public class SkillRewardBonusLevel : SkillRewardCommon
    {
        public SkillRewardBonusLevel(int RewardLevel, Skill Skill)
            : base(RewardLevel)
        {
            this.Skill = Skill;
        }

        public Skill Skill { get; private set; }

        public override string TextContent { get { return Skill.Name; } }
    }
}
