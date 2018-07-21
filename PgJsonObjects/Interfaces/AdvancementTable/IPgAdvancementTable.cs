using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgAdvancementTable : IJsonKey, IObjectContentGenerator, IBackLinkable
    {
        int Id { get; }
        string InternalName { get; }
        string FriendlyName { get; }
        Dictionary<int, IPgAdvancement> LevelTable { get; }
        bool HasManyLevels { get; }
        int CurrentLevel { get; }
        IPgAdvancement CurrentAdvancement { get; }
        void OnLevelChange(int change);
    }
}
