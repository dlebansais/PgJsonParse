namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Npc
{
    public Npc(RawNpc rawNpc)
    {
        AreaFriendlyName = rawNpc.AreaFriendlyName;
        (AreaName, OriginalAreaName) = ParseAreaName(rawNpc.AreaName);
        Name = rawNpc.Name;
        (UnsortedPreferences, Preferences) = ParsePreferences(rawNpc.Preferences);
    }

    private const string AreaHeader = "Area";

    private static (string?, string?) ParseAreaName(string? content)
    {
        if (content is null)
            return (null, null);

        if (!content.StartsWith(AreaHeader))
            PreprocessorException.Throw();

        string? AreaName = content.Substring(AreaHeader.Length);
        AreaName = Area.FromRawAreaName(AreaName, out string? OriginalAreaName);

        return (AreaName, OriginalAreaName);
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
    public string? Name { get; set; }
    public NpcPreference[]? Preferences { get; set; }

    public RawNpc ToRawNpc()
    {
        RawNpc Result = new();

        Result.AreaFriendlyName = AreaFriendlyName;
        Result.AreaName = ToRawAreaName(AreaName, OriginalAreaName);
        Result.Name = Name;
        Result.Preferences = ToRawNpcPreferences(UnsortedPreferences);

        return Result;
    }

    private static string? ToRawAreaName(string? areaName, string? originalAreaName)
    {
        if (areaName is null)
            return null;

        return $"{AreaHeader}{Area.ToRawAreaName(areaName, originalAreaName)}";
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
}
