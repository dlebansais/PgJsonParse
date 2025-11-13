namespace Preprocessor;

using System;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class SourceAbility : IHasKey<int>
{
    public SourceAbility(int key)
    {
        Key = key;
    }

    public SourceAbility(int key, RawSourceAbility rawSourceAbility)
        : this(key)
    {
        if (rawSourceAbility.entries is not RawSourceAbilityEntry[] RawEntries)
            throw new PreprocessorException(this);

        Entries = new SourceAbilityEntry[RawEntries.Length];
        for (int i = 0; i < Entries.Length; i++)
            Entries[i] = new SourceAbilityEntry()
            {
                ItemTypeId = RawEntries[i].itemTypeId,
                Npc = RawEntries[i].npc,
                QuestId = RawEntries[i].questId,
                Skill = RawEntries[i].skill,
                Type = RawEntries[i].type,
            };
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    public SourceAbilityEntry[] Entries { get; set; } = Array.Empty<SourceAbilityEntry>();

    public RawSourceAbility ToRawSourceAbility()
    {
        RawSourceAbility Result = new();

        Result.entries = new RawSourceAbilityEntry[Entries.Length];
        for (int i = 0; i < Entries.Length; i++)
            Result.entries[i] = new RawSourceAbilityEntry()
            {
                itemTypeId = Entries[i].ItemTypeId,
                npc = Entries[i].Npc,
                questId = Entries[i].QuestId,
                skill = Entries[i].Skill,
                type = Entries[i].Type,
            };

        return Result;
    }
}
