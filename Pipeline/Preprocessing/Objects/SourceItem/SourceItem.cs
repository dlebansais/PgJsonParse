namespace Preprocessor;

public class SourceItem
{
    public SourceItem(RawSourceItem rawSourceItem)
    {
        if (rawSourceItem.entries is RawSourceItemEntry[] RawEntries)
        {
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
        else
            throw new PreprocessorException(this);
    }

    public SourceItemEntry[] Entries { get; set; }

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
