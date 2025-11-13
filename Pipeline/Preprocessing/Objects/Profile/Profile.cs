namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class Profile : IHasKey<string>
{
    public Profile(string key, string[] rawProfile)
    {
        Key = key;
        EffectList = rawProfile;
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public string Key { get; set; }

    [Column(MapType = typeof(string))]
    public string[] EffectList { get; set; }

    public string[] ToRawProfile(string key)
    {
        return EffectList;
    }
}
