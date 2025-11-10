namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class NpcServiceLevelRange
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public int Key { get; set; }

    public int Max { get; set; }

    public int Min { get; set; }
}
