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

        protected virtual void AddWithFieldSeparator(ref string Result, string s)
        {
            if (s != null)
                Result += s + JsonGenerator.FieldSeparator;
        }
    }
}
