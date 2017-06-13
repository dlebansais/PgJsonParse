namespace PgJsonObjects
{
    public abstract class SkillRewardCommon
    {
        public SkillRewardCommon(int RewardLevel)
        {
            this.RewardLevel = RewardLevel;
        }

        public int RewardLevel { get; private set; }
        public abstract string TextContent { get; }
    }
}
