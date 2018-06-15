namespace PgJsonObjects
{
    public interface IPgQuestObjectiveGiveGift
    {
        float MinFavorReceived { get; }
        float? RawMinFavorReceived { get; }
        float MaxFavorReceived { get; }
        float? RawMaxFavorReceived { get; }
    }
}
