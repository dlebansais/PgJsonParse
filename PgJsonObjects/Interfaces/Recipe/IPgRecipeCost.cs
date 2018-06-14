namespace PgJsonObjects
{
    public interface IPgRecipeCost
    {
        double Price { get; }
        double? RawPrice { get; }
        RecipeCurrency Currency { get; }
    }
}
