namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class ItemUse
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public int Key { get; set; }

    [Column(MapType = typeof(string))]
    public int[]? RecipesThatUseItem { get; set; }
}
