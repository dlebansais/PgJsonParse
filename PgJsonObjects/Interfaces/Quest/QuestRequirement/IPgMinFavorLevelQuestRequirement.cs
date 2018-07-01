namespace PgJsonObjects
{
    public interface IPgMinFavorLevelQuestRequirement
    {
        IPgGameNpc FavorNpc { get; }
        bool IsEmpty { get; }
        bool? RawIsEmpty { get; }
        Favor FavorLevel { get; }
        string FavorNpcId { get; }
        string FavorNpcName { get; }
        MapAreaName FavorNpcArea { get; }
    }
}
