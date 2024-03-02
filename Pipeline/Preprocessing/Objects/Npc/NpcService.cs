using System.Collections.Generic;

namespace Preprocessor;

public class NpcService
{
    public NpcService(RawNpcService rawNpcService)
    {
        AdditionalUnlocks = rawNpcService.AdditionalUnlocks;
        CapIncreases = rawNpcService.CapIncreases;
        Favor = rawNpcService.Favor;
        ItemDescriptions = rawNpcService.ItemDescs;
        ItemTypes = rawNpcService.ItemTypes;
        LevelRanges = ParseLevelRange(rawNpcService.LevelRange);
        Skills = rawNpcService.Skills;
        SpaceIncreases = rawNpcService.SpaceIncreases;
        Type = rawNpcService.Type;
        Unlocks = rawNpcService.Unlocks;
    }

    private static NpcServiceLevelRange[]? ParseLevelRange(string[]? rawLevelRange)
    {
        if (rawLevelRange is null)
            return null;

        List<NpcServiceLevelRange> Result = new();
        foreach (string LevelRange in rawLevelRange)
            Result.Add(ParseLevelRange(LevelRange));

        return Result.ToArray();
    }

    private static NpcServiceLevelRange ParseLevelRange(string rawLevelRange)
    {
        string[] Splitted = rawLevelRange.Split('-');
        if (Splitted.Length != 2)
            throw new PreprocessorException();

        if (!int.TryParse(Splitted[0], out int Min))
            throw new PreprocessorException();

        if (!int.TryParse(Splitted[1], out int Max))
            throw new PreprocessorException();

        NpcServiceLevelRange Result = new() { Min = Min, Max = Max };

        return Result;
    }

    public string[]? AdditionalUnlocks { get; set; }
    public string[]? CapIncreases { get; set; }
    public string? Favor { get; set; }
    public string[]? ItemDescriptions { get; set; }
    public string[]? ItemTypes { get; set; }
    public NpcServiceLevelRange[]? LevelRanges { get; set; }
    public string[]? Skills { get; set; }
    public string[]? SpaceIncreases { get; set; }
    public string? Type { get; set; }
    public string[]? Unlocks { get; set; }

    public RawNpcService ToRawNpcService()
    {
        RawNpcService Result = new();

        Result.AdditionalUnlocks = AdditionalUnlocks;
        Result.CapIncreases = CapIncreases;
        Result.Favor = Favor;
        Result.ItemDescs = ItemDescriptions;
        Result.ItemTypes = ItemTypes;
        Result.LevelRange = ToRawLevelRange(LevelRanges);
        Result.Skills = Skills;
        Result.SpaceIncreases = SpaceIncreases;
        Result.Type = Type;
        Result.Unlocks = Unlocks;

        return Result;
    }

    private static string[]? ToRawLevelRange(NpcServiceLevelRange[]? npcServiceLevelRanges)
    {
        if (npcServiceLevelRanges is null)
            return null;

        List<string> Result = new();

        foreach (NpcServiceLevelRange LevelRange in npcServiceLevelRanges)
            Result.Add($"{LevelRange.Min}-{LevelRange.Max}");

        return Result.ToArray();
    }
}
