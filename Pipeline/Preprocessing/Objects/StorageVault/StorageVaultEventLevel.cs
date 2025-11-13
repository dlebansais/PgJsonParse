namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class StorageVaultEventLevel : IHasKey<int>, IHasParentKey<string>
{
    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public string ParentKey { get; set; } = string.Empty;

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public int? RiShinShrine_Storage1On { get; set; }

    public int? RiShinShrine_Storage2On { get; set; }
}
