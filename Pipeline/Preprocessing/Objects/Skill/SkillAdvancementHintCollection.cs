namespace Preprocessor;

using System.Collections.Generic;

public class SkillAdvancementHintCollection : List<SkillAdvancementHint>
{
    public SkillAdvancementHintCollection()
    {
    }

    public SkillAdvancementHintCollection(IEnumerable<SkillAdvancementHint> collection)
        : base(collection)
    {
    }
}
