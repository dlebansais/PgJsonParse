namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class StorageVaultEventLevel
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public int Key { get; set; }

    public int? RiShinShrine_Storage1On { get; set; }

    public int? RiShinShrine_Storage2On { get; set; }
}
