namespace Preprocessor;

public class SourceAbility
{
    public SourceAbility(RawSourceAbility rawSourceAbility)
    {
        if (rawSourceAbility.entries is RawSourceAbilityEntry[] RawEntries)
        {
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
        else
            throw new PreprocessorException(this);
    }

    public SourceAbilityEntry[] Entries { get; set; }

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
