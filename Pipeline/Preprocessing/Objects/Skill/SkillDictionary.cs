namespace Preprocessor;

using System.Collections.Generic;

public class SkillDictionary : Dictionary<string, Skill>, IDictionaryValueBuilder<Skill, RawSkill>
{
    public Skill FromRaw(RawSkill rawSkill) => new Skill(rawSkill);
    public RawSkill ToRaw(Skill skill) => skill.ToRawSkill();
}
