namespace Preprocessor;

internal class DirectedGoal
{
    public DirectedGoal(RawDirectedGoal fromRawDirectedGoal)
    {
        CategoryGateId = fromRawDirectedGoal.CategoryGateId;
        ForRaces = fromRawDirectedGoal.ForRaces;
        Id = fromRawDirectedGoal.Id;
        IsCategoryGate = fromRawDirectedGoal.IsCategoryGate;
        Label = fromRawDirectedGoal.Label;
        LargeHint = fromRawDirectedGoal.LargeHint;
        SmallHint = fromRawDirectedGoal.SmallHint;
        Zone = fromRawDirectedGoal.Zone;
    }

    public int? CategoryGateId { get; set; }
    public string[]? ForRaces { get; set; }
    public bool? IsCategoryGate { get; set; }
    public string? Label { get; set; }
    public string? LargeHint { get; set; }
    public string? SmallHint { get; set; }
    public string? Zone { get; set; }

    public RawDirectedGoal ToRawDirectedGoal()
    {
        RawDirectedGoal Result = new();

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

    private readonly int Id;
}
