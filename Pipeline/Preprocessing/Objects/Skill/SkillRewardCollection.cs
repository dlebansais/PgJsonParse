namespace Preprocessor;

using System.Collections.Generic;

public class SkillRewardCollection : List<SkillReward>
{
    public SkillRewardCollection()
    {
    }

    public SkillRewardCollection(IEnumerable<SkillReward> rewards)
        : base(rewards)
    {
    }
}
