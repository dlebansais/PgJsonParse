namespace PgJsonObjects
{
    public interface IPgGameNpc : IJsonKey, IObjectContentGenerator, IBackLinkable
    {
        string Name { get; }
        string AreaFriendlyName { get; }
        IPgNpcPreferenceCollection PreferenceList { get; }
        IPgNpcPreferenceCollection LikeList { get; }
        IPgNpcPreferenceCollection HateList { get; }
        MapAreaName AreaName { get; }
    }
}
