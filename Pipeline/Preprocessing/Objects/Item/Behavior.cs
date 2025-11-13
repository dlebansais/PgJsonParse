namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class Behavior : IHasKey<int>, IHasParentKey<int>
{
    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public int ParentKey { get; set; }

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public int? MetabolismCost { get; set; }
    
    public int? MinStackSizeNeeded { get; set; }
    
    public string? UseAnimation { get; set; }
    
    public decimal? UseDelay { get; set; }
    
    public string? UseDelayAnimation { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? UseRequirements { get; set; }
    
    public string? UseVerb { get; set; }
}
