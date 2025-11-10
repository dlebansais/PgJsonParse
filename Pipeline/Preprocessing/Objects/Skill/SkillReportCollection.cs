namespace Preprocessor;

using System.Collections.Generic;

public class SkillReportCollection : List<SkillReport>
{
    public SkillReportCollection()
    {
    }

    public SkillReportCollection(IEnumerable<SkillReport> collection)
        : base(collection)
    {
    }
}
