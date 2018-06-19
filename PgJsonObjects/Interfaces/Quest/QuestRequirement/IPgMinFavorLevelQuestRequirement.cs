namespace PgJsonObjects
{
    public interface IPgMinFavorLevelQuestRequirement
    {
        IPgGameNpc FavorNpc { get; }
        bool IsEmpty { get; }
        bool? RawIsEmpty { get; }
        Favor FavorLevel { get; }
    }
}
