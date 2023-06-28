namespace Preprocessor;

internal class QuestReward
{
    public string? Ability { get; set; }
    public int? Amount { get; set; }
    public int? Credits { get; set; }
    public string? Currency { get; set; }
    public string? Effect { get; set; }
    public int? Favor { get; set; }
    public string? InteractionFlag { get; set; }
    public int? Level { get; set; }
    public string? LoreBook { get; set; }
    public string? NamedLootProfile { get; set; }
    public string? Npc { get; set; }
    public string? Recipe { get; set; }
    public string? Skill { get; set; }
    public string? T { get; set; }
    public int? Title { get; set; }
    public int? Xp { get; set; }

    public void SetObjectiveCompleteOrDone(string text)
    {
        ObjectiveCompleteOrDone = text;
    }

    public string? GetObjectiveCompleteOrDone()
    {
        return ObjectiveCompleteOrDone;
    }

    private string? ObjectiveCompleteOrDone;
}
