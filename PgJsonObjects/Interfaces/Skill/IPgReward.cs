using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgReward
    {
        string Key { get; }
        int RewardLevel { get; }
        int? RawRewardLevel { get; }
        List<Race> RaceRestrictionList { get; }
        IPgAbility Ability { get; }
        string Notes { get; }
        IPgRecipe Recipe { get; }
        IPgSkill BonusSkill { get; }
    }
}
