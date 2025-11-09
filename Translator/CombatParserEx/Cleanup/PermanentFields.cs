namespace Translator;

internal enum PermanentFields
{
    None = 0,
    Data = 0x00000002,
    DataValue = 0x00000004,
    DataValuePositive = 0x00000008,
    DataPercent = 0x00000010,
    DamageType = 0x00000020,
    RandomChance = 0x00000100,
    DelayInSeconds = 0x00000200,
    DurationInSeconds = 0x00000400,
    RecurringDelay = 0x00000800,
    Target = 0x00001000,
    ConditionList = 0x00008000,
    ConditionAbilityList = 0x00010000,
}
