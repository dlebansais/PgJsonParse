using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SkillRewardAbility : SkillRewardCommon
    {
        public SkillRewardAbility(int RewardLevel, List<Race> RaceRestrictionList, IPgAbility Ability)
            : base(RewardLevel)
        {
            this.RaceRestrictionList = RaceRestrictionList;
            this.Ability = Ability;
        }

        public List<Race> RaceRestrictionList { get; private set; }
        public IPgAbility Ability { get; private set; }

        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (Race Race in RaceRestrictionList)
                    AddWithFieldSeparator(ref Result, TextMaps.RaceTextMap[Race]);
                AddWithFieldSeparator(ref Result, Ability.Name);

                return Result;
            }
        }
    }
}
