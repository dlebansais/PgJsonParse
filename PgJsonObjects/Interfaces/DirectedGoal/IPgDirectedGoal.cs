namespace PgJsonObjects
{
    public interface IPgDirectedGoal : IJsonKey, IObjectContentGenerator, IBackLinkable
    {
        int Id { get; }
        int? RawId { get; }
        string Label { get; }
        string Zone { get; }
        string LargeHint { get; }
        string SmallHint { get; }
        int CategoryGateId { get; }
        int? RawCategoryGateId { get; }
        bool IsCategoryGate { get; }
        bool? RawIsCategoryGate { get; }
    }
}
