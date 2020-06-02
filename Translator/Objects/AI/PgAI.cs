namespace PgJsonObjects
{
    public class PgAI
    {
        public PgAIAbilitySet Abilities { get; set; }
        public string Comment { get; set; } = string.Empty;
        public float? RawMinDelayBetweenAbilities { get; set; }
        public bool? RawIsMelee { get; set; }
        public bool? RawIsUncontrolledPet { get; set; }
        public bool? RawIsServerDriven { get; set; }
        public bool? RawUseAbilitiesWithoutEnemyTarget { get; set; }
        public bool? RawSwimming { get; set; }
        public bool? RawIsFlying { get; set; }
        public bool? RawIsFollowClose { get; set; }
        public MobilityType MobilityType { get; set; }
        public string Description { get; set; }
    }
}
