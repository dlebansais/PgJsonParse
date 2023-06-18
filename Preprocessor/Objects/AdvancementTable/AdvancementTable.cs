namespace Preprocessor;

internal class AdvancementTable
{
    public AdvancementTable(string name, AdvancementDictionary levels)
    {
        Name = name;
        Levels = levels;
    }

    public string Name { get; set; }
    
    public AdvancementDictionary Levels { get; set; }
}
