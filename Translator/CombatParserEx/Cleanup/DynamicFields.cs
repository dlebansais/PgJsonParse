namespace Translator;

internal enum DynamicFields
{
    None = 0,
    AbilityList = 0x00000001,
    Data = 0x00000002,
    DataValue = 0x00000004,
    DataValuePositive = 0x00000008,
    DataPercent = 0x00000010,
    DamageType = 0x00000020,
    DamageCategory = 0x00000040,
    CombatSkill = 0x00000080,
    RandomChance = 0x00000100,
    DelayInSeconds = 0x00000200,
    DurationInSeconds = 0x00000400,
    RecurringDelay = 0x00000800,
    Target = 0x00001000,
    TargetRange = 0x00002000,
    TargetAbilityList = 0x00004000,
    ConditionList = 0x00008000,
    ConditionAbilityList = 0x00010000,
    ConditionValue = 0x00020000,
    ConditionPercentage = 0x00040000,
    IsEveryOtherUse = 0x00080000,
}
