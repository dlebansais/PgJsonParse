using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgGameNpc
    {
        string Name { get; }
        string AreaFriendlyName { get; }
        List<NpcPreference> PreferenceList { get; }
        List<NpcPreference> LikeList { get; }
        List<NpcPreference> HateList { get; }
        MapAreaName AreaName { get; }
    }
}
