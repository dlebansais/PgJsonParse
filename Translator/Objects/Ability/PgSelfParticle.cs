namespace PgObjects;

using MemoryPack;

[MemoryPackable]
public partial class PgSelfParticle : PgAbilityParticle
{
    public AbilitySelfParticle Particle { get; set; }
}
