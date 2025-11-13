namespace Preprocessor;

using System;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class SourceItem : IHasKey<int>
{
    public SourceItem(int key)
    {
        Key = key;
    }

    public SourceItem(int key, RawSourceItem rawSourceItem)
        : this(key)
    {
        if (rawSourceItem.entries is not RawSourceItemEntry[] RawEntries)
            throw new PreprocessorException(this);

        Entries = new SourceItemEntry[RawEntries.Length];
        for (int i = 0; i < Entries.Length; i++)
            Entries[i] = new SourceItemEntry()
            {
                FriendlyName = RawEntries[i].friendlyName,
                HangOutId = RawEntries[i].hangOutId,
                ItemTypeId = RawEntries[i].itemTypeId,
                Npc = RawEntries[i].npc,
                QuestId = RawEntries[i].questId,
                RecipeId = RawEntries[i].recipeId,
                Type = RawEntries[i].type,
            };
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    public SourceItemEntry[] Entries { get; set; } = Array.Empty<SourceItemEntry>();

    public RawSourceItem ToRawSourceItem()
    {
        RawSourceItem Result = new();

        Result.entries = new RawSourceItemEntry[Entries.Length];
        for (int i = 0; i < Entries.Length; i++)
            Result.entries[i] = new RawSourceItemEntry()
            {
                friendlyName = Entries[i].FriendlyName,
                hangOutId = Entries[i].HangOutId,
                itemTypeId = Entries[i].ItemTypeId,
                npc = Entries[i].Npc,
                questId = Entries[i].QuestId,
                recipeId = Entries[i].RecipeId,
                type = Entries[i].Type,
            };

        return Result;
    }
}
