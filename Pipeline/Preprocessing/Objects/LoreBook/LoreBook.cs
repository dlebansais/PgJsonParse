namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class LoreBook
{
    public LoreBook()
    {
    }

    public LoreBook(int key, LoreBook other)
    {
        Key = key;
        Category = other.Category;
        InternalName = other.InternalName;
        IsClientLocal = other.IsClientLocal;
        Keywords = other.Keywords;
        LocationHint = other.LocationHint;
        Text = other.Text;
        Title = other.Title;
        Visibility = other.Visibility;
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    public string? Category { get; set; }

    public string? InternalName { get; set; }

    public bool? IsClientLocal { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? Keywords { get; set; }

    public string? LocationHint { get; set; }

    public string? Text { get; set; }

    public string? Title { get; set; }

    public string? Visibility { get; set; }
}
