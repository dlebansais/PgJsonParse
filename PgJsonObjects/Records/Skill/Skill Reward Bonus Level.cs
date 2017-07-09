using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SkillRewardBonusLevel : SkillRewardCommon
    {
        public SkillRewardBonusLevel(int RewardLevel, List<Race> RaceRestrictionList, Skill Skill)
            : base(RewardLevel)
        {
            this.RaceRestrictionList = RaceRestrictionList;
            this.Skill = Skill;
        }

        public List<Race> RaceRestrictionList { get; private set; }
        public Skill Skill { get; private set; }

        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (Race Race in RaceRestrictionList)
                    AddWithFieldSeparator(ref Result, TextMaps.RaceTextMap[Race]);
                AddWithFieldSeparator(ref Result, Skill.Name);

                return Result;
            }
        }
    }
}
