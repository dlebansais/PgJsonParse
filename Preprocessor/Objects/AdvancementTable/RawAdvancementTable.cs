namespace Preprocessor;

internal class RawAdvancementTable
{
    public RawAdvancementTable(string name, RawAdvancementDictionary levels)
    {
        Name = name;
        Levels = levels;
    }

    public string Name { get; set; }
    public RawAdvancementDictionary Levels { get; set; }
}
