namespace PgJsonObjects
{
    public interface IPgXpTableLevel
    {
        int Level { get; }
        int? RawLevel { get; }
        int Xp { get; }
        int? RawXp { get; }
        int TotalXp { get; }
        int? RawTotalXp { get; }
    }
}
