namespace Preprocessor;

using System.Collections.Generic;

internal class SkillDictionary : Dictionary<string, Skill>, IDictionaryValueBuilder<Skill, Skill>
{
    public Skill FromRaw(Skill area) => area;
    public Skill ToRaw(Skill area) => area;
}
