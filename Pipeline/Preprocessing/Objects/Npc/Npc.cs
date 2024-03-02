namespace Preprocessor;

using System.Collections.Generic;
using System.Globalization;

public class Npc
{
    private const string AreaHeader = "Area";

    public Npc(RawNpc rawNpc)
    {
        AreaFriendlyName = rawNpc.AreaFriendlyName;
        (AreaName, OriginalAreaName) = ParseAreaName(rawNpc.AreaName);
        Description = rawNpc.Desc;
        ItemGifts = rawNpc.ItemGifts;
        Name = rawNpc.Name;
        (PositionX, PositionY, PositionZ) = ParsePosition(rawNpc.Pos);
        (UnsortedPreferences, Preferences) = ParsePreferences(rawNpc.Preferences);
        Services = Preprocessor.ToSingleOrMultiple(rawNpc.Services, (RawNpcService rawNpcService) => new NpcService(rawNpcService), out ServicesFormat);
    }

    private static (string?, string?) ParseAreaName(string? rawAreaName)
    {
        if (rawAreaName is null)
            return (null, null);

        if (!rawAreaName.StartsWith(AreaHeader))
            throw new PreprocessorException();

        string? AreaName = rawAreaName.Substring(AreaHeader.Length);
        AreaName = Area.FromRawAreaName(AreaName, out string? OriginalAreaName);

        return (AreaName, OriginalAreaName);
    }

    private static (decimal?, decimal?, decimal?) ParsePosition(string? content)
    {
        if (content is null)
            return (null, null, null);

        string[] Splitted = content.Split(' ');
        if (Splitted.Length != 3)
            throw new PreprocessorException();

        decimal? PositionX = null;
        decimal? PositionY = null;
        decimal? PositionZ = null;

        for (int i = 0; i < Splitted.Length; i++)
        {
            string[] Coordinates = Splitted[i].Split(':');
            if (Coordinates.Length != 2)
                throw new PreprocessorException();

            string CoordinateName = Coordinates[0];
            string CoordinateValue = Coordinates[1];

            if (!decimal.TryParse(CoordinateValue, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal Value))
                throw new PreprocessorException();

            switch (CoordinateName)
            {
                case "x":
                    PositionX = Value;
                    break;

                case "y":
                    PositionY = Value;
                    break;

                case "z":
                    PositionZ = Value;
                    break;

                default:
                    throw new PreprocessorException();
            }
        }

        return (PositionX, PositionY, PositionZ);
    }

    private static (NpcPreference[]?, NpcPreference[]?) ParsePreferences(RawNpcPreference[]? content)
    {
        List<NpcPreference> UnsortedPreferences = new();

        if (content is null)
            return (null, null);

        for (int i = 0; i < content.Length; i++)
            UnsortedPreferences.Add(new NpcPreference(content[i]));

        List<NpcPreference> Preferences = new(UnsortedPreferences);
        Preferences.Sort(SortByDesire);

        return (UnsortedPreferences.ToArray(), Preferences.ToArray());
    }

    private static int SortByDesire(NpcPreference p1, NpcPreference p2)
    {
        if (p1.Desire is string Desire1 && p2.Desire is string Desire2)
        {
            Dictionary<string, int> DesirePreferenceTable = new()
            {
                { "Love", 2 },
                { "Like", 1 },
                { "Dislike", -1 },
                { "Hate", -2 },
            };

            return DesirePreferenceTable[Desire2] - DesirePreferenceTable[Desire1];
        }
        else
            return 0;
    }

    public string? AreaFriendlyName { get; set; }
    public string? AreaName { get; set; }
    public string? Description { get; set; }
    public string[]? ItemGifts { get; set; }
    public string? Name { get; set; }
    public decimal? PositionX { get; set; }
    public decimal? PositionY { get; set; }
    public decimal? PositionZ { get; set; }
    public NpcPreference[]? Preferences { get; set; }
    public NpcService[]? Services { get; set; }

    public RawNpc ToRawNpc()
    {
        RawNpc Result = new();

        Result.AreaFriendlyName = AreaFriendlyName;
        Result.AreaName = ToRawAreaName(AreaName, OriginalAreaName);
        Result.Desc = Description;
        Result.ItemGifts = ItemGifts;
        Result.Name = Name;
        Result.Pos = ToRawPosition(PositionX, PositionY, PositionZ);
        Result.Preferences = ToRawNpcPreferences(UnsortedPreferences);
        Result.Services = Preprocessor.FromSingleOrMultiple(Services, (NpcService npcService) => npcService.ToRawNpcService(), ServicesFormat);

        return Result;
    }

    private static string? ToRawAreaName(string? areaName, string? originalAreaName)
    {
        if (areaName is null)
            return null;

        return $"{AreaHeader}{Area.ToRawAreaName(areaName, originalAreaName)}";
    }

    private static string? ToRawPosition(decimal? positionX, decimal? positionY, decimal? positionZ)
    {
        if (positionX is null || positionY is null || positionZ is null)
            return null;

        return $"x:{positionX?.ToString(CultureInfo.InvariantCulture)} y:{positionY?.ToString(CultureInfo.InvariantCulture)} z:{positionZ?.ToString(CultureInfo.InvariantCulture)}";
    }

    private static RawNpcPreference[]? ToRawNpcPreferences(NpcPreference[]? npcPreferenceArray)
    {
        if (npcPreferenceArray is null)
            return null;

        RawNpcPreference[] Result = new RawNpcPreference[npcPreferenceArray.Length];

        for (int i = 0; i < npcPreferenceArray.Length; i++)
            Result[i] = npcPreferenceArray[i].ToRawNpcPreference();

        return Result;
    }

    private NpcPreference[]? UnsortedPreferences;
    private string? OriginalAreaName;
    private readonly JsonArrayFormat ServicesFormat;
}
