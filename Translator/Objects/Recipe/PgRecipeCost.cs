namespace PgJsonObjects
{
    public class PgRecipeCost
    {
        public float Price { get { return RawPrice.HasValue ? RawPrice.Value : 0; } }
        public float? RawPrice { get; set; }
        public RecipeCurrency Currency { get; set; }
    }
}
