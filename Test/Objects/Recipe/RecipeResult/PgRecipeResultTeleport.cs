namespace PgObjects
{
    public class PgRecipeResultTeleport : PgRecipeResultEffect
    {
        public MapAreaName MapAreaName { get; set; }
        public string Other { get; set; } = string.Empty;
    }
}
