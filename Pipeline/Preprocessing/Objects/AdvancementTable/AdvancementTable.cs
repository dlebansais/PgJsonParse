namespace Preprocessor;

using FreeSql.DataAnnotations;

public class AdvancementTable : IHasKey<int>
{
    public AdvancementTable(int key, string name, AdvancementCollection levels)
    {
        Key = key;
        Name = name;
        Levels = levels.ToArray();
    }

    [Column(IsPrimary = true)]
    public int Key { get; set; }

    public Advancement[] Levels { get; set; }

    public string Name { get; set; }
}
