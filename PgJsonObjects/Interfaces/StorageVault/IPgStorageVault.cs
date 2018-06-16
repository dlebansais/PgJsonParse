namespace PgJsonObjects
{
    public interface IPgStorageVault
    {
        int Id { get; }
        int? RawId { get; }
        GameNpc MatchingNpc { get; }
        int NumSlots { get; }
        int? RawNumSlots { get; }
        string RequirementDescription { get; }
        string InteractionFlagRequirement { get; }
        ItemKeyword RequiredItemKeyword { get; }
        MapAreaName Grouping { get; }
        bool HasAssociatedNpc { get; }
        bool? RawHasAssociatedNpc { get; }
    }
}
