namespace Preprocessor;

internal class AdvancementTable
{
    public AdvancementTable(string name, AdvancementDictionary levels)
    {
        Name = name;
        Levels = levels;
    }

    public AdvancementDictionary Levels { get; set; }
    public string Name { get; set; }
}
