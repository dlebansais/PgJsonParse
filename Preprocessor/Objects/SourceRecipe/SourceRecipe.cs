namespace Preprocessor;

internal class SourceRecipe
{
    public SourceRecipe(RawSourceRecipe rawSourceRecipe)
    {
        if (rawSourceRecipe.entries is null)
            Entries = null;
        else
        {
            Entries = new SourceRecipeEntry[rawSourceRecipe.entries.Length];
            for (int i = 0; i < Entries.Length; i++)
                Entries[i] = new SourceRecipeEntry()
                {
                    HangOutId = rawSourceRecipe.entries[i].hangOutId,
                    ItemTypeId = rawSourceRecipe.entries[i].itemTypeId,
                    Npc = rawSourceRecipe.entries[i].npc,
                    QuestId = rawSourceRecipe.entries[i].questId,
                    Skill = rawSourceRecipe.entries[i].skill,
                    Type = rawSourceRecipe.entries[i].type,
                };
        }
    }

    public SourceRecipeEntry[]? Entries { get; set; }

    public RawSourceRecipe ToRawSourceRecipe()
    {
        RawSourceRecipe Result = new();

        if (Entries is null)
            Result.entries = null;
        else
        {
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
        }

        return Result;
    }
}
