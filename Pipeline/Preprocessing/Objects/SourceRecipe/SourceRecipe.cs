namespace Preprocessor;

public class SourceRecipe
{
    public SourceRecipe(RawSourceRecipe rawSourceRecipe)
    {
        if (rawSourceRecipe.entries is RawSourceRecipeEntry[] RawEntries)
        {
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
        else
            throw new PreprocessorException(this);
    }

    public SourceRecipeEntry[] Entries { get; set; }

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
