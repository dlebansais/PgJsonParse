namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class ItemUse : IHasKey<int>
{
    public ItemUse()
    {
    }

    public ItemUse(int key, ItemUse other)
    {
        Key = key;
        RecipesThatUseItem = other.RecipesThatUseItem;
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [Column(MapType = typeof(string))]
    public int[]? RecipesThatUseItem { get; set; }
}
