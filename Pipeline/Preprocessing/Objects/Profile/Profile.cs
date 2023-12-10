namespace Preprocessor;

using System.Collections.Generic;
using System.Linq;

public class Profile
{
    public Profile(string[] rawProfile)
    {
        EffectList = rawProfile.ToList();
    }

    public List<string> EffectList { get; }

    public string[] ToRawProfile(string key)
    {
        return EffectList.ToArray();
    }
}
