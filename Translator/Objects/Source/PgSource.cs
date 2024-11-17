namespace PgObjects
{
    public abstract class PgSource
    {
        public string SourceKey { get; set; } = string.Empty;
        public bool IsAbility { get; set; }
        public bool IsItem { get; set; }
        public bool IsRecipe { get; set; }
    }
}
