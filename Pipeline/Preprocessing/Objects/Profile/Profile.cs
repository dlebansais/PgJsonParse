namespace Preprocessor;

using System.Linq;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class Profile
{
    public Profile(string[] rawProfile)
    {
        EffectList = rawProfile.ToArray();
    }

    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public int Key { get; set; }

    [Column(MapType = typeof(string))]
    public string[] EffectList { get; }

    public string[] ToRawProfile(string key)
    {
        return EffectList.ToArray();
    }
}
