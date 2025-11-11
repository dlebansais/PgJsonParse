namespace Preprocessor;

using System.Linq;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class Profile
{
    public Profile(string key, string[] rawProfile)
    {
        Key = key;
        EffectList = rawProfile.ToArray();
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public string Key { get; set; }

    [Column(MapType = typeof(string))]
    public string[] EffectList { get; }

    public string[] ToRawProfile(string key)
    {
        return EffectList.ToArray();
    }
}
