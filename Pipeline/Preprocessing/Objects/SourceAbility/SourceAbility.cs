namespace Preprocessor;

public class SourceAbility
{
    public SourceAbility(RawSourceAbility rawSourceAbility)
    {
        if (rawSourceAbility.entries is null)
            Entries = null;
        else
        {
            Entries = new SourceAbilityEntry[rawSourceAbility.entries.Length];
            for (int i = 0; i < Entries.Length; i++)
                Entries[i] = new SourceAbilityEntry()
                {
                    ItemTypeId = rawSourceAbility.entries[i].itemTypeId,
                    Npc = rawSourceAbility.entries[i].npc,
                    QuestId = rawSourceAbility.entries[i].questId,
                    Skill = rawSourceAbility.entries[i].skill,
                    Type = rawSourceAbility.entries[i].type,
                };
        }
    }

    public SourceAbilityEntry[]? Entries { get; set; }

    public RawSourceAbility ToRawSourceAbility()
    {
        RawSourceAbility Result = new();

        if (Entries is null)
            Result.entries = null;
        else
        {
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
        }

        return Result;
    }
}
