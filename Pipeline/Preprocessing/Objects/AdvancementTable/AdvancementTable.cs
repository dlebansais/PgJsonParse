namespace Preprocessor;

using FreeSql.DataAnnotations;
using System.Collections.Generic;

public class AdvancementTable
{
    public AdvancementTable(string name, AdvancementCollection levels)
    {
        Name = name;
        Levels = levels;
    }

    [Column(IsIdentity = true, IsPrimary = true)]
    public int Key { get; set; }

    [Navigate(nameof(Advancement.Key))]
    public List<Advancement> Levels { get; set; }

    public string Name { get; set; }
}
