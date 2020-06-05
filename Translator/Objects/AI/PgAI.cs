namespace PgJsonObjects
{
    public class PgAI
    {
        public PgAIAbilitySet Abilities { get; set; }
        public bool IsMelee { get { return RawIsMelee.HasValue && RawIsMelee.Value; } }
        public bool? RawIsMelee { get; set; }
        public string Comment { get; set; } = string.Empty;
        public bool IsUncontrolledPet { get { return RawIsUncontrolledPet.HasValue && RawIsUncontrolledPet.Value; } }
        public bool? RawIsUncontrolledPet { get; set; }
        public bool IsServerDriven { get { return RawIsServerDriven.HasValue && RawIsServerDriven.Value; } }
        public bool? RawIsServerDriven { get; set; }
        public float MinDelayBetweenAbilities { get { return RawMinDelayBetweenAbilities.HasValue ? RawMinDelayBetweenAbilities.Value : 0; } }
        public float? RawMinDelayBetweenAbilities { get; set; }
        public bool UseAbilitiesWithoutEnemyTarget { get { return RawUseAbilitiesWithoutEnemyTarget.HasValue && RawUseAbilitiesWithoutEnemyTarget.Value; } }
        public bool? RawUseAbilitiesWithoutEnemyTarget { get; set; }
        public bool IsSwimming { get { return RawIsSwimming.HasValue && RawIsSwimming.Value; } }
        public bool? RawIsSwimming { get; set; }
        public MobilityType MobilityType { get; set; }
        public bool IsFlying { get { return RawIsFlying.HasValue && RawIsFlying.Value; } }
        public bool? RawIsFlying { get; set; }
        public bool IsFollowClose { get { return RawIsFollowClose.HasValue && RawIsFollowClose.Value; } }
        public bool? RawIsFollowClose { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
