namespace Preprocessor;

using System;

internal class Npc
{
    public Npc(RawNpc rawNpc)
    {
        AreaFriendlyName = rawNpc.AreaFriendlyName;
        AreaName = ParseAreaName(rawNpc.AreaName);
        Name = rawNpc.Name;
        Preferences = ParsePreferences(rawNpc.Preferences);
    }

    private const string AreaHeader = "Area";

    private static string? ParseAreaName(string? content)
    {
        if (content is null)
            return null;

        if (!content.StartsWith(AreaHeader))
            throw new InvalidCastException();

        return content.Substring(AreaHeader.Length);
    }

    private static NpcPreference[]? ParsePreferences(RawNpcPreference[]? content)
    {
        if (content is null)
            return null;

        NpcPreference[] Result = new NpcPreference[content.Length];

        for (int i = 0; i < content.Length; i++)
            Result[i] = new NpcPreference(content[i]);

        return Result;
    }

    public string? AreaFriendlyName { get; set; }
    public string? AreaName { get; set; }
    public string? Name { get; set; }
    public NpcPreference[]? Preferences { get; set; }

    public RawNpc ToRawNpc()
    {
        RawNpc Result = new();

        Result.AreaFriendlyName = AreaFriendlyName;
        Result.AreaName = ToRawAreaName(AreaName);
        Result.Name = Name;
        Result.Preferences = ToRawNpcPreferences(Preferences);

        return Result;
    }

    private static string? ToRawAreaName(string? areaName)
    {
        if (areaName is null)
            return null;

        return $"{AreaHeader}{areaName}";
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
}
