namespace PgJsonObjects
{
    public class SkillRewardRecipe : SkillRewardCommon
    {
        public SkillRewardRecipe(int RewardLevel, Recipe Recipe)
            : base(RewardLevel)
        {
            this.Recipe = Recipe;
        }

        public Recipe Recipe { get; private set; }

        public override string TextContent { get { return Recipe.Name; } }
    }
}
