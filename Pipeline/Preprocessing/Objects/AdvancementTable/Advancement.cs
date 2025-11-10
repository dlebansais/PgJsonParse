namespace Preprocessor;

using System.Collections.Generic;
using FreeSql.DataAnnotations;

public class Advancement
{
    public Advancement(AdvancementEffectAttributeCollection attributes, int level)
    {
        Attributes = attributes;
        Level = level;
    }

    [Column(IsIdentity = true, IsPrimary = true)]
    public int Key { get; set; }

    [Navigate(nameof(AdvancementEffectAttribute.Key))]
    public List<AdvancementEffectAttribute> Attributes { get; set; }

    public int Level { get; set; }
}
