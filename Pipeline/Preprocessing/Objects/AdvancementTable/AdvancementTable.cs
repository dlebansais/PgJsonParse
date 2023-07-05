namespace Preprocessor;

public class AdvancementTable
{
    public AdvancementTable(string name, AdvancementCollection levels)
    {
        Name = name;
        Levels = levels;
    }

    public AdvancementCollection Levels { get; set; }
    public string Name { get; set; }
}
