using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgReward
    {
        int? RawRewardLevel { get; }
        List<Race> RaceRestrictionList { get; }
        IPgAbility Ability { get; }
        string Notes { get; }
        IPgRecipe Recipe { get; }
        IPgSkill BonusSkill { get; }
    }
}
