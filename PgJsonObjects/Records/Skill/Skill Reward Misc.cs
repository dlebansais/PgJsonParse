namespace PgJsonObjects
{
    public class SkillRewardMisc : SkillRewardCommon
    {
        public SkillRewardMisc(int RewardLevel, string Text)
            : base(RewardLevel)
        {
            this.Text = Text;
        }

        public string Text { get; private set; }

        public override string TextContent { get { return Text; } }
    }
}
