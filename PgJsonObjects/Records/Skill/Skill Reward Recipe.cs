using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SkillRewardRecipe : SkillRewardCommon
    {
        public SkillRewardRecipe(int RewardLevel, List<Race> RaceRestrictionList, Recipe Recipe)
            : base(RewardLevel)
        {
            this.RaceRestrictionList = RaceRestrictionList;
            this.Recipe = Recipe;
        }

        public List<Race> RaceRestrictionList { get; private set; }
        public Recipe Recipe { get; private set; }

        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (Race Race in RaceRestrictionList)
                    AddWithFieldSeparator(ref Result, TextMaps.RaceTextMap[Race]);
                AddWithFieldSeparator(ref Result, Recipe.Name);

                return Result;
            }
        }
    }
}
