namespace PgObjects
{
    public class PgRecipeResultTeleport : PgRecipeResultEffect
    {
        public MapAreaName Area { get; set; }
        public string Other { get; set; } = string.Empty;
    }
}
