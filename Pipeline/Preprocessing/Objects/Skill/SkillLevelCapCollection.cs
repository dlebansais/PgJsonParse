namespace Preprocessor;

using System.Collections.Generic;

public class SkillLevelCapCollection : List<SkillLevelCap>
{
    public SkillLevelCapCollection()
    {
    }

    public SkillLevelCapCollection(IEnumerable<SkillLevelCap> collection)
        : base(collection)
    {
    }
}
