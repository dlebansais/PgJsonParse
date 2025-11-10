namespace PgObjects;

using MemoryPack;

[MemoryPackable]
public partial class PgTargetParticle : PgAbilityParticle
{
    public AbilityTargetParticle Particle { get; set; }
}
