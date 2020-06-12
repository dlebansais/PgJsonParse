namespace PgObjects
{
    public class PgRecipeCost
    {
        public RecipeCurrency Currency { get; set; }
        public float Price { get { return RawPrice.HasValue ? RawPrice.Value : 0; } }
        public float? RawPrice { get; set; }
    }
}
