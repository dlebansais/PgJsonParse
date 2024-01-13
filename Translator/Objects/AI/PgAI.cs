namespace PgObjects
{
    public class PgAI
    {
        public string Key { get; set; } = string.Empty;
        public PgAIAbilitySet Abilities { get; set; } = null!;
        public int BoolValues { get; set; }
        public string Comment { get; set; } = string.Empty;
        public const int IsUncontrolledPetNotNull = 1 << 2;
        public const int IsUncontrolledPetIsTrue = 1 << 3;
        public bool IsUncontrolledPet { get { return (BoolValues & (IsUncontrolledPetNotNull + IsUncontrolledPetIsTrue)) == (IsUncontrolledPetNotNull + IsUncontrolledPetIsTrue); } }
        public bool? RawIsUncontrolledPet { get { return ((BoolValues & IsUncontrolledPetNotNull) != 0) ? (BoolValues & IsUncontrolledPetIsTrue) != 0 : null; } }
        public void SetIsUncontrolledPet(bool value) { BoolValues |= (BoolValues & ~(IsUncontrolledPetNotNull + IsUncontrolledPetIsTrue)) | ((value ? IsUncontrolledPetIsTrue : 0) + IsUncontrolledPetNotNull); }
        public const int IsServerDrivenNotNull = 1 << 4;
        public const int IsServerDrivenIsTrue = 1 << 5;
        public bool IsServerDriven { get { return (BoolValues & (IsServerDrivenNotNull + IsServerDrivenIsTrue)) == (IsServerDrivenNotNull + IsServerDrivenIsTrue); } }
        public bool? RawIsServerDriven { get { return ((BoolValues & IsServerDrivenNotNull) != 0) ? (BoolValues & IsServerDrivenIsTrue) != 0 : null; } }
        public void SetIsServerDriven(bool value) { BoolValues |= (BoolValues & ~(IsServerDrivenNotNull + IsServerDrivenIsTrue)) | ((value ? IsServerDrivenIsTrue : 0) + IsServerDrivenNotNull); }
        public float MinDelayBetweenAbilities { get { return RawMinDelayBetweenAbilities.HasValue ? RawMinDelayBetweenAbilities.Value : 0; } }
        public float? RawMinDelayBetweenAbilities { get; set; }
        public const int UseAbilitiesWithoutEnemyTargetNotNull = 1 << 6;
        public const int UseAbilitiesWithoutEnemyTargetIsTrue = 1 << 7;
        public bool UseAbilitiesWithoutEnemyTarget { get { return (BoolValues & (UseAbilitiesWithoutEnemyTargetNotNull + UseAbilitiesWithoutEnemyTargetIsTrue)) == (UseAbilitiesWithoutEnemyTargetNotNull + UseAbilitiesWithoutEnemyTargetIsTrue); } }
        public bool? RawUseAbilitiesWithoutEnemyTarget { get { return ((BoolValues & UseAbilitiesWithoutEnemyTargetNotNull) != 0) ? (BoolValues & UseAbilitiesWithoutEnemyTargetIsTrue) != 0 : null; } }
        public void SetUseAbilitiesWithoutEnemyTarget(bool value) { BoolValues |= (BoolValues & ~(UseAbilitiesWithoutEnemyTargetNotNull + UseAbilitiesWithoutEnemyTargetIsTrue)) | ((value ? UseAbilitiesWithoutEnemyTargetIsTrue : 0) + UseAbilitiesWithoutEnemyTargetNotNull); }
        public const int IsSwimmingNotNull = 1 << 8;
        public const int IsSwimmingIsTrue = 1 << 9;
        public bool IsSwimming { get { return (BoolValues & (IsSwimmingNotNull + IsSwimmingIsTrue)) == (IsSwimmingNotNull + IsSwimmingIsTrue); } }
        public bool? RawIsSwimming { get { return ((BoolValues & IsSwimmingNotNull) != 0) ? (BoolValues & IsSwimmingIsTrue) != 0 : null; } }
        public void SetIsSwimming(bool value) { BoolValues |= (BoolValues & ~(IsSwimmingNotNull + IsSwimmingIsTrue)) | ((value ? IsSwimmingIsTrue : 0) + IsSwimmingNotNull); }
        public MobilityType MobilityType { get; set; }
        public Strategy Strategy { get; set; }
        public const int IsFlyingNotNull = 1 << 10;
        public const int IsFlyingIsTrue = 1 << 11;
        public bool IsFlying { get { return (BoolValues & (IsFlyingNotNull + IsFlyingIsTrue)) == (IsFlyingNotNull + IsFlyingIsTrue); } }
        public bool? RawIsFlying { get { return ((BoolValues & IsFlyingNotNull) != 0) ? (BoolValues & IsFlyingIsTrue) != 0 : null; } }
        public void SetIsFlying(bool value) { BoolValues |= (BoolValues & ~(IsFlyingNotNull + IsFlyingIsTrue)) | ((value ? IsFlyingIsTrue : 0) + IsFlyingNotNull); }
        public string Description { get; set; } = string.Empty;
    }
}
