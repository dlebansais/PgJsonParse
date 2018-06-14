using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgReward
    {
        int? RawRewardLevel { get; }
        List<Race> RaceRestrictionList { get; }
        Ability Ability { get; }
        string Notes { get; }
        Recipe Recipe { get; }
        Skill BonusSkill { get; }
    }
}
