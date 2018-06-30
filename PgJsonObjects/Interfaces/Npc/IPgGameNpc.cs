namespace PgJsonObjects
{
    public interface IPgGameNpc : IJsonKey, IObjectContentGenerator
    {
        string Name { get; }
        string AreaFriendlyName { get; }
        NpcPreferenceCollection PreferenceList { get; }
        NpcPreferenceCollection LikeList { get; }
        NpcPreferenceCollection HateList { get; }
        MapAreaName AreaName { get; }
    }
}
