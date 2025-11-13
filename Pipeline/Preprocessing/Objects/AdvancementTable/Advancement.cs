namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class Advancement : IHasKey<int>, IHasParentKey<int>
{
    public Advancement(AdvancementEffectAttributeCollection attributes, int level)
    {
        Attributes = attributes.ToArray();
        Level = level;
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public int ParentKey { get; set; }

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public AdvancementEffectAttribute[] Attributes { get; set; }

    public int Level { get; set; }
}
