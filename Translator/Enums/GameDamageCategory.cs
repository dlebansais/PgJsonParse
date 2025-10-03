namespace PgObjects;

public enum GameDamageCategory
{
    Internal_None,
    Melee       = 0x0001,
    Ranged      = 0x0002,
    Burst       = 0x0004,
    Direct      = 0x0008,
    Indirect    = 0x0010,
    Reflect     = 0x0020,
}

