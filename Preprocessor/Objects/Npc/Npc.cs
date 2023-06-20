namespace Preprocessor;

internal class Npc
{
    public Npc(RawNpc rawNpc)
    {
        AreaFriendlyName = rawNpc.AreaFriendlyName;
        AreaName = rawNpc.AreaName;
        Name = rawNpc.Name;

        if (rawNpc.Preferences is null)
            Preferences = null;
        else
        {
            Preferences = new NpcPreference[rawNpc.Preferences.Length];

            for (int i = 0; i < rawNpc.Preferences.Length; i++)
                Preferences[i] = new NpcPreference()
                {
                    Desire = rawNpc.Preferences[i].Desire,
                    Favor = rawNpc.Preferences[i].Favor,
                    Keywords = rawNpc.Preferences[i].Keywords,
                    Pref = rawNpc.Preferences[i].Pref,
                };
        }
    }

    public string? AreaFriendlyName { get; set; }
    public string? AreaName { get; set; }
    public string? Name { get; set; }
    public NpcPreference[]? Preferences { get; set; }

    public RawNpc ToRawNpc()
    {
        RawNpc Result = new();

        Result.AreaFriendlyName = AreaFriendlyName;
        Result.AreaName = AreaName;
        Result.Name = Name;

        if (Preferences is null)
            Result.Preferences = null;
        else
        {
            Result.Preferences = new RawNpcPreference[Preferences.Length];

            for (int i = 0; i < Preferences.Length; i++)
                Result.Preferences[i] = new RawNpcPreference()
                {
                    Desire = Preferences[i].Desire,
                    Favor = Preferences[i].Favor,
                    Keywords = Preferences[i].Keywords,
                    Pref = Preferences[i].Pref,
                };
        }

        return Result;
    }
}
