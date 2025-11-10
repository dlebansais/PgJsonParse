namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class StorageVaultLevel
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public int Key { get; set; }

    public int? BestFriends { get; set; }

    public int? CloseFriends { get; set; }

    public int? Comfortable { get; set; }

    public int? Despised { get; set; }

    public int? Friends { get; set; }

    public int? LikeFamily { get; set; }

    public int? Neutral { get; set; }

    public int? SoulMates { get; set; }
}
