namespace Preprocessor;

using System.Collections.Generic;

public class SkillDictionary : Dictionary<string, Skill>, IDictionaryValueBuilderString<Skill, RawSkill>
{
    public Skill FromRaw(string key, RawSkill rawSkill) => new(key, rawSkill);
    public RawSkill ToRaw(Skill skill) => skill.ToRawSkill();
}
