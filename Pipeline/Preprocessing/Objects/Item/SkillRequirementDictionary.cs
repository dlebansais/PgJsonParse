namespace Preprocessor;

using System.Collections.Generic;

public class SkillRequirementDictionary : Dictionary<string, int>
{
    public SkillRequirementDictionary()
    {
    }

    public SkillRequirementDictionary(IEnumerable<KeyValuePair<string, int>> keyValuePairs)
        : base(keyValuePairs)
    {
    }
}
