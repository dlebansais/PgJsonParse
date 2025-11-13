namespace Preprocessor;

using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class Area : IHasKey<string>
{
    public Area()
    {
        Key = string.Empty;
    }

    public Area(string key, Area other)
    {
        Key = key;
        FriendlyName = other.FriendlyName;
        ShortFriendlyName = other.ShortFriendlyName;
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public string Key { get; set; }

    public string? FriendlyName { get; set; }
    
    public string? ShortFriendlyName { get; set; }

    private static readonly Dictionary<string, string> AreaTable = new()
    {
        { "Ilmari", "Desert1" },
        { "Kur", "KurMountains" },
        { "Khyrulek's Crypt", "Tomb1" },
        { "Anagoge Island", "NewbieIsland" },
        { "Serbule Caves", "SerbuleCaves" },
        { "Serbule Hills", "Serbule2" },
        { "Sun Vale", "SunVale" },
        { "Hogan's Keep", "Cave1" },
        { "Gazluk Dungeons", "GazlukCaves" },
        { "Gazluk Plateau", "Gazluk" },
        { "Wolf Cave", "Cave2" },
        { "Myconian Caverns", "MyconianCave" },
        { "Gazluk Keep", "GazlukKeep" },
        { "Ranalon Den", "RanalonDen" },
        { "Red Wing Casino", "Casino" },
        { "Rahu Caves", "RahuCaves" },
        { "Rahu Sewer", "RahuSewer" },
        { "Fae Realm", "FaeRealm" },
        { "Sacred Grotto", "SacredGrotto" },
        { "Hogan's Basement", "HogansBasement" },
        { "Serbule Crypt", "SerbuleCrypt" },
        { "Goblin Dungeon", "GoblinDungeon" },
        { "Carpal Tunnels", "CarpalTunnels" },
        { "New Prestonbule", "NewPrestonbule" },
        { "Snowblood Shadow/Gazluk Shadow Caves", "SnowbloodShadowGazlukShadowCaves" },
        { "Kur Tower", "KurTower" },
        { "Winter Nexus", "WinterNexus" },
        { "The Wintertide", "TheWintertide" },
        { "Nightmare Caves", "NightmareCaves" },
    };

    private static readonly Dictionary<string, string> AreaTableReversed = AreaTable.ToDictionary(x => x.Value, x => x.Key);

    public static string? FromRawAreaName(string? rawAreaName, out string? originalAreaName)
    {
        if (rawAreaName is not null && (rawAreaName == "Ilmari Desert" || rawAreaName == "Kur Mountains" || AreaTable.ContainsKey(rawAreaName)))
        {
            originalAreaName = rawAreaName;

            if (rawAreaName == "Ilmari Desert")
                return "Desert1";
            else if (rawAreaName == "Kur Mountains")
                return "KurMountains";
            else
                return AreaTable[rawAreaName];
        }
        else
        {
            originalAreaName = null;
            return rawAreaName;
        }
    }

    public static string? ToRawAreaName(string? areaName, string? originalAreaName)
    {
        if (areaName is not null && AreaTableReversed.ContainsKey(areaName) && (originalAreaName == "Ilmari Desert" || originalAreaName == "Kur Mountains" || originalAreaName == AreaTableReversed[areaName]))
            return originalAreaName;
        else
            return areaName;
    }
}
