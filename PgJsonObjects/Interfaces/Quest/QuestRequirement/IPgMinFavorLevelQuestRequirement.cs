namespace PgJsonObjects
{
    public interface IPgMinFavorLevelQuestRequirement
    {
        GameNpc FavorNpc { get; }
        bool IsEmpty { get; }
        bool? RawIsEmpty { get; }
        Favor FavorLevel { get; }
    }
}
