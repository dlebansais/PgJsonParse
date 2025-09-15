namespace PgObjects;

public class PgModEffectCollection
{
    public PgModEffectCollection(string skill_Key, int length)
    {
        Skill_Key = skill_Key;
        Items = new PgModEffect[length];
    }

    public string Skill_Key { get; }
    public PgModEffect[] Items { get; }
}
