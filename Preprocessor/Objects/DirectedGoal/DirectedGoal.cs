namespace Preprocessor;

using System.Text.Json.Serialization;

internal class DirectedGoal
{
    public DirectedGoal(DirectedGoal1 fromRawDirectedGoal1)
    {
        CategoryGateId = fromRawDirectedGoal1.CategoryGateId;
        ForRaces = fromRawDirectedGoal1.ForRaces;
        Id = fromRawDirectedGoal1.Id;
        IsCategoryGate = fromRawDirectedGoal1.IsCategoryGate;
        Label = fromRawDirectedGoal1.Label;
        LargeHint = fromRawDirectedGoal1.LargeHint;
        SmallHint = fromRawDirectedGoal1.SmallHint;
        Zone = fromRawDirectedGoal1.Zone;
    }

    public int? CategoryGateId { get; set; }
    public string[]? ForRaces { get; set; }
    public bool? IsCategoryGate { get; set; }
    public string? Label { get; set; }
    public string? LargeHint { get; set; }
    public string? SmallHint { get; set; }
    public string? Zone { get; set; }

    public DirectedGoal1 ToRawDirectedGoal1()
    {
        DirectedGoal1 Result = new();

        Result.CategoryGateId = CategoryGateId;
        Result.ForRaces = ForRaces;
        Result.Id = Id;
        Result.IsCategoryGate = IsCategoryGate;
        Result.Label = Label;
        Result.LargeHint = LargeHint;
        Result.SmallHint = SmallHint;
        Result.Zone = Zone;

        return Result;
    }

    [JsonIgnore]
    public int Id { get; }
}
