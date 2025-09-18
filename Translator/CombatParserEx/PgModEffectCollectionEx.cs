namespace PgObjects;

public class PgModEffectCollectionEx
{
    public PgModEffectCollectionEx(string skill_Key, int length)
    {
        Skill_Key = skill_Key;
        Items = new PgModEffectEx[length];
    }

    public string Skill_Key { get; }
    public PgModEffectEx[] Items { get; }
}
