namespace PgObjects
{
#pragma warning disable SA1025 // Code should not contain multiple whitespace in a row
    public enum GameDamageType
    {
        Internal_None,
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
        //Demonic     = 0x1000,
    }
#pragma warning restore SA1025 // Code should not contain multiple whitespace in a row
}
