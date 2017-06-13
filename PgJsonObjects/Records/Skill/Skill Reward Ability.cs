namespace PgJsonObjects
{
    public class SkillRewardAbility : SkillRewardCommon
    {
        public SkillRewardAbility(int RewardLevel, Ability Ability)
            : base(RewardLevel)
        {
            this.Ability = Ability;
        }

        public Ability Ability { get; private set; }

        public override string TextContent { get { return Ability.Name; } }
    }
}
