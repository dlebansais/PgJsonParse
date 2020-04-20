namespace PgBuilder
{
    public enum GameDamageType
    {
        None = 0,
        Crushing    = 0x0001,
        Slashing    = 0x0002,
        Nature      = 0x0004,
        Fire        = 0x0008,
        Cold        = 0x0010,
        Piercing    = 0x0020,
        Psychic     = 0x0040,
        Trauma      = 0x0080,
        Electricity = 0x0100,
        Poison      = 0x0200,
        Acid        = 0x0400,
        Darkness    = 0x0800,
    }
}
