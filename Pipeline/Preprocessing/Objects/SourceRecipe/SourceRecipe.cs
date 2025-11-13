namespace Preprocessor;

using System;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class SourceRecipe : IHasKey<int>
{
    public SourceRecipe(int key)
    {
        Key = key;
    }

    public SourceRecipe(int key, RawSourceRecipe rawSourceRecipe)
        : this(key)
    {
        if (rawSourceRecipe.entries is not RawSourceRecipeEntry[] RawEntries)
            throw new PreprocessorException(this);

        Entries = new SourceRecipeEntry[RawEntries.Length];
        for (int i = 0; i < Entries.Length; i++)
            Entries[i] = new SourceRecipeEntry()
            {
                HangOutId = RawEntries[i].hangOutId,
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

    [Navigate(nameof(SourceRecipeEntry.Key))]
    public SourceRecipeEntry[] Entries { get; set; } = Array.Empty<SourceRecipeEntry>();

    public RawSourceRecipe ToRawSourceRecipe()
    {
        RawSourceRecipe Result = new();

        Result.entries = new RawSourceRecipeEntry[Entries.Length];
        for (int i = 0; i < Entries.Length; i++)
            Result.entries[i] = new RawSourceRecipeEntry()
            {
                hangOutId = Entries[i].HangOutId,
                itemTypeId = Entries[i].ItemTypeId,
                npc = Entries[i].Npc,
                questId = Entries[i].QuestId,
                skill = Entries[i].Skill,
                type = Entries[i].Type,
            };

        return Result;
    }
}
