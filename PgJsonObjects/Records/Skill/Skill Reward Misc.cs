using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SkillRewardMisc : SkillRewardCommon
    {
        public SkillRewardMisc(int RewardLevel, List<Race> RaceRestrictionList, string Text)
            : base(RewardLevel)
        {
            this.RaceRestrictionList = RaceRestrictionList;
            this.Text = Text;
        }

        public List<Race> RaceRestrictionList { get; private set; }
        public string Text { get; private set; }

        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (Race Race in RaceRestrictionList)
                    AddWithFieldSeparator(ref Result, TextMaps.RaceTextMap[Race]);
                AddWithFieldSeparator(ref Result, Text);

                return Result;
            }
        }
    }
}
