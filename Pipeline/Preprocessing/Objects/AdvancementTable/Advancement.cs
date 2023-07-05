namespace Preprocessor;

public class Advancement
{
    public Advancement(AdvancementEffectAttributeCollection attributes, int level)
    {
        Attributes = attributes;
        Level = level;
    }

    public AdvancementEffectAttributeCollection Attributes { get; }
    public int Level { get; set; }
}
