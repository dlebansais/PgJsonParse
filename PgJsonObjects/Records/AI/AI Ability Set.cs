using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AIAbilitySet : GenericJsonObject<AIAbilitySet>, IPgAIAbilitySet
    {
        #region Direct Properties
        public AIAbility AnimalBite { get; private set; }
        public AIAbility AnimalClaw { get; private set; }
        public AIAbility AnimalOmegaBite { get; private set; }
        public AIAbility AnimalOmegaBite2 { get; private set; }
        public AIAbility AnimalHoofFrontKick { get; private set; }
        public AIAbility AnimalHoofRageKick { get; private set; }
        public AIAbility AnimalHoofRageKick2 { get; private set; }
        public AIAbility AnimalHoofFieryFrontKick { get; private set; }
        public AIAbility AnimalHoofFieryFrontKick2 { get; private set; }
        public AIAbility ElectricPigHitAndRun { get; private set; }
        public AIAbility ElectricPigStun { get; private set; }
        public AIAbility ElectricPigAoEStun { get; private set; }
        public AIAbility LamiaMindControl { get; private set; }
        public AIAbility LamiaRage { get; private set; }
        public AIAbility SlimeKick { get; private set; }
        public AIAbility SlimeKickB { get; private set; }
        public AIAbility SlimeBite { get; private set; }
        public AIAbility SlimeBiteB { get; private set; }
        public AIAbility Slime_SummonSlime { get; private set; }
        public AIAbility SlimeSpit { get; private set; }
        public AIAbility SlimeSuperSpit { get; private set; }
        public AIAbility IceSlimeKick { get; private set; }
        public AIAbility IceSlimeKickB { get; private set; }
        public AIAbility IceSlimeBite { get; private set; }
        public AIAbility IceSlimeBiteB { get; private set; }
        public AIAbility BossSlimeKick { get; private set; }
        public AIAbility BossSlimeKickB { get; private set; }
        public AIAbility BossSlime_SummonSlime1 { get; private set; }
        public AIAbility BossSlimeKick2 { get; private set; }
        public AIAbility BossSlimeKick2B { get; private set; }
        public AIAbility BossSlime_SummonSlime4Elite { get; private set; }
        public AIAbility AnimalHeal { get; private set; }
        public AIAbility AnimalHeal2 { get; private set; }
        public AIAbility AnimalHeal3 { get; private set; }
        public AIAbility IceCockPeck { get; private set; }
        public AIAbility IceCockFreeze { get; private set; }
        public AIAbility NightmareHoof { get; private set; }
        public AIAbility NightmareDarknessBomb { get; private set; }
        public AIAbility CiervosNightmareHoof { get; private set; }
        public AIAbility CiervosDarknessBomb { get; private set; }
        public AIAbility Myconian_Bash { get; private set; }
        public AIAbility Myconian_Mindspores { get; private set; }
        public AIAbility Myconian_Drain { get; private set; }
        public AIAbility Myconian_BossBash { get; private set; }
        public AIAbility Myconian_Mindspores_Permanent { get; private set; }
        public AIAbility Myconian_TidalCurse { get; private set; }
        public AIAbility Myconian_Shock { get; private set; }
        public AIAbility AlienDog_Punch { get; private set; }
        public AIAbility AlienDog_Punch2 { get; private set; }
        public AIAbility AlienDog_RagePunch { get; private set; }
        public AIAbility MushroomMonster_Bite { get; private set; }
        public AIAbility MushroomMonster_Spit1 { get; private set; }
        public AIAbility MushroomMonster_Spit2 { get; private set; }
        public AIAbility MushroomMonster_SummonMushroomSpawn1 { get; private set; }
        public AIAbility MushroomMonster_SummonMushroomSpawn2 { get; private set; }
        public AIAbility MushroomMonster_SummonMushroomSpawn3 { get; private set; }
        public AIAbility MushroomMonster_SpawnSpit1 { get; private set; }
        public AIAbility MushroomMonster_SpawnSpit2 { get; private set; }
        public AIAbility MushroomMonster_SpawnSuperSpit1 { get; private set; }
        public AIAbility MushroomMonster_SpawnSuperSpit2 { get; private set; }
        public AIAbility MaronesaStomp { get; private set; }
        public AIAbility MaronesaInfect { get; private set; }
        public AIAbility WormBite1 { get; private set; }
        public AIAbility WormSpit1 { get; private set; }
        public AIAbility WormShove1 { get; private set; }
        public AIAbility WormInfect1 { get; private set; }
        public AIAbility WormSpit2 { get; private set; }
        public AIAbility WormBossSpit { get; private set; }
        public AIAbility WormBossAcidBurst { get; private set; }
        public AIAbility GnasherBite { get; private set; }
        public AIAbility GnasherRend { get; private set; }
        public AIAbility Dinoslash { get; private set; }
        public AIAbility Dinoslash2 { get; private set; }
        public AIAbility Dinobite { get; private set; }
        public AIAbility Dinobite2 { get; private set; }
        public AIAbility Dinowhap { get; private set; }
        public AIAbility Peck { get; private set; }
        public AIAbility AnimalFlee { get; private set; }
        public AIAbility HippogriffPeck { get; private set; }
        public AIAbility HippogriffSlashes { get; private set; }
        public AIAbility HippogriffBossSlashes { get; private set; }
        public AIAbility RatBite { get; private set; }
        public AIAbility RatClaw { get; private set; }
        public AIAbility FireRatBite { get; private set; }
        public AIAbility FireRatClaw { get; private set; }
        public AIAbility GoblinZapBall { get; private set; }
        public AIAbility GoblinHateZapBall { get; private set; }
        public AIAbility GoblinHateZapBall2 { get; private set; }
        public AIAbility RhinoHorn { get; private set; }
        public AIAbility RhinoRage { get; private set; }
        public AIAbility RhinoFireball { get; private set; }
        public AIAbility RhinoBossRage { get; private set; }
        public AIAbility YetiPunch { get; private set; }
        public AIAbility YetiEncase { get; private set; }
        public AIAbility YetiDebuff { get; private set; }
        public AIAbility YetiBoulderThrow { get; private set; }
        public AIAbility YetiBarrage { get; private set; }
        public AIAbility YetiRoarStun { get; private set; }
        public AIAbility YetiFlingAway { get; private set; }
        public AIAbility YetiIceBallThrow { get; private set; }
        public AIAbility YetiIceSpear { get; private set; }
        public AIAbility YetiColdOrb { get; private set; }
        public AIAbility YetiFrostRing { get; private set; }
        public AIAbility WorgBite { get; private set; }
        public AIAbility WorgOmegaBite { get; private set; }
        public AIAbility BearBite { get; private set; }
        public AIAbility BearCrush { get; private set; }
        public AIAbility BrainBite { get; private set; }
        public AIAbility BrainDrain { get; private set; }
        public AIAbility BrainDrain2 { get; private set; }
        public AIAbility BigCatClaw { get; private set; }
        public AIAbility BigCatPounce { get; private set; }
        public AIAbility BigCatRagePounce { get; private set; }
        public AIAbility BigCatDebuff { get; private set; }
        public AIAbility SpiderBite { get; private set; }
        public AIAbility SpiderFireball { get; private set; }
        public AIAbility SpiderBossFreePin { get; private set; }
        public AIAbility SpiderKill2 { get; private set; }
        public AIAbility SpiderInject { get; private set; }
        public AIAbility SpiderKill { get; private set; }
        public AIAbility AcidBall1 { get; private set; }
        public AIAbility SpiderPin { get; private set; }
        public AIAbility SpiderKill3 { get; private set; }
        public AIAbility AcidBall2 { get; private set; }
        public AIAbility AcidSpew1 { get; private set; }
        public AIAbility AcidExplosion1 { get; private set; }
        public AIAbility AcidExplosion2 { get; private set; }
        public AIAbility SpiderIncubate { get; private set; }
        public AIAbility MantisClaw { get; private set; }
        public AIAbility MantisRage { get; private set; }
        public AIAbility MantisAcidBurst { get; private set; }
        public AIAbility SherzatClaw { get; private set; }
        public AIAbility SherzatAcidSpit { get; private set; }
        public AIAbility SherzatDisintegrate { get; private set; }
        public AIAbility MantisSwipe { get; private set; }
        public AIAbility MantisBlast { get; private set; }
        public AIAbility SnailStrike { get; private set; }
        public AIAbility SnailRage { get; private set; }
        public AIAbility SnailRageB { get; private set; }
        public AIAbility SnailRageC { get; private set; }
        public AIAbility HookClaw { get; private set; }
        public AIAbility HookAcid { get; private set; }
        public AIAbility HookRage { get; private set; }
        public AIAbility UndeadSword1 { get; private set; }
        public AIAbility UndeadSword2 { get; private set; }
        public AIAbility UndeadSwordAngry { get; private set; }
        public AIAbility UndeadMegaSword1 { get; private set; }
        public AIAbility UndeadMegaSword2 { get; private set; }
        public AIAbility UndeadMegaSwordAngry { get; private set; }
        public AIAbility UndeadLightningSmite { get; private set; }
        public AIAbility UndeadPhysicalShield { get; private set; }
        public AIAbility UndeadSword1B { get; private set; }
        public AIAbility UndeadFireballA { get; private set; }
        public AIAbility UndeadFireballB { get; private set; }
        public AIAbility UndeadFireballB2 { get; private set; }
        public AIAbility UndeadIceBall1 { get; private set; }
        public AIAbility UndeadFreezeBall { get; private set; }
        public AIAbility UndeadFireballLongA { get; private set; }
        public AIAbility UndeadFireballLongB { get; private set; }
        public AIAbility UndeadFireballLongB2 { get; private set; }
        public AIAbility KhyrulekCurseBall { get; private set; }
        public AIAbility UrsulaFireball1 { get; private set; }
        public AIAbility UrsulaFireball1B { get; private set; }
        public AIAbility UrsulaFireball2 { get; private set; }
        public AIAbility UrsulaIceball1 { get; private set; }
        public AIAbility UrsulaIceball1B { get; private set; }
        public AIAbility UrsulaIceball2 { get; private set; }
        public AIAbility UrsulaSummon { get; private set; }
        public AIAbility UrsulaRage { get; private set; }
        public AIAbility UndeadFireballA2 { get; private set; }
        public AIAbility BigHeadCurseball { get; private set; }
        public AIAbility UndeadArrow1 { get; private set; }
        public AIAbility UndeadArrow2 { get; private set; }
        public AIAbility UndeadOmegaArrow { get; private set; }
        public AIAbility PetUndeadArrow1 { get; private set; }
        public AIAbility PetUndeadArrow2 { get; private set; }
        public AIAbility PetUndeadOmegaArrow { get; private set; }
        public AIAbility UndeadGrappleArrow1 { get; private set; }
        public AIAbility UndeadSelfDestruct { get; private set; }
        public AIAbility UndeadDarknessBall { get; private set; }
        public AIAbility UndeadBoneWhirlwind { get; private set; }
        public AIAbility PetUndeadSword1 { get; private set; }
        public AIAbility PetUndeadSword2 { get; private set; }
        public AIAbility PetUndeadSwordAngry { get; private set; }
        public AIAbility PetUndeadFireballA { get; private set; }
        public AIAbility PetUndeadFireballB { get; private set; }
        public AIAbility PetUndeadDefensiveBurst { get; private set; }
        public AIAbility PetUndeadPunch1 { get; private set; }
        public AIAbility ZombiePunch { get; private set; }
        public AIAbility ZombieBite { get; private set; }
        public AIAbility GoblinSpear1 { get; private set; }
        public AIAbility GoblinSpear2 { get; private set; }
        public AIAbility GoblinRageSpear1 { get; private set; }
        public AIAbility GoblinRageSpear2 { get; private set; }
        public AIAbility GoblinHeal1 { get; private set; }
        public AIAbility GoblinHeal2 { get; private set; }
        public AIAbility GoblinPunch { get; private set; }
        public AIAbility GoblinArrow1 { get; private set; }
        public AIAbility GoblinArrow2 { get; private set; }
        public AIAbility GoblinRageArrow1 { get; private set; }
        public AIAbility GoblinRageArrow2 { get; private set; }
        public AIAbility GoblinSpreadZapBall { get; private set; }
        public AIAbility GoblinBossLightning { get; private set; }
        public AIAbility GoblinArmorBuff { get; private set; }
        public AIAbility MummySlamA { get; private set; }
        public AIAbility MummySlamB { get; private set; }
        public AIAbility MummySlamCombo { get; private set; }
        public AIAbility MummyWrapA { get; private set; }
        public AIAbility MummyWrapB { get; private set; }
        public AIAbility MummyWrapRage { get; private set; }
        public AIAbility BarutiWrapA { get; private set; }
        public AIAbility BarutiWrapB { get; private set; }
        public AIAbility BarutiWrapRage { get; private set; }
        public AIAbility FireWallAttack1 { get; private set; }
        public AIAbility FireWallDotAttack1 { get; private set; }
        public AIAbility FireSnakeExplosion1 { get; private set; }
        public AIAbility FireTrapAttack1 { get; private set; }
        public AIAbility HealingAura1 { get; private set; }
        public AIAbility HealingAura2 { get; private set; }
        public AIAbility HealingAura3 { get; private set; }
        public AIAbility HealingAura4 { get; private set; }
        public AIAbility DruidHealingSanctuaryHeal { get; private set; }
        public AIAbility AcidAuraBall1 { get; private set; }
        public AIAbility AcidAuraBall2 { get; private set; }
        public AIAbility AcidAuraBall3 { get; private set; }
        public AIAbility AcidAuraBall4 { get; private set; }
        public AIAbility ElectricityAura1 { get; private set; }
        public AIAbility ElectricityAuraBolt1 { get; private set; }
        public AIAbility ReboundAura1 { get; private set; }
        public AIAbility ColdAuraBurst { get; private set; }
        public AIAbility WebStick { get; private set; }
        public AIAbility IcySlam { get; private set; }
        public AIAbility IcyCocoon { get; private set; }
        public AIAbility IcyCocoon2 { get; private set; }
        public AIAbility ElementalSlam { get; private set; }
        public AIAbility ElementalBees { get; private set; }
        public AIAbility ElementalBees2 { get; private set; }
        public AIAbility FaeLightningSmite { get; private set; }
        public AIAbility TotalHorrorAttack { get; private set; }
        public AIAbility TotalHorrorStretch { get; private set; }
        public AIAbility TotalHorrorHeal { get; private set; }
        public AIAbility TotalHorrorHeal2 { get; private set; }
        public AIAbility SheepBomb1 { get; private set; }
        public AIAbility SlugPoisonBite { get; private set; }
        public AIAbility SlugPoisonRage { get; private set; }
        public AIAbility SlugPoisonBite2 { get; private set; }
        public AIAbility SlugPoisonRage2 { get; private set; }
        public AIAbility SlugPoisonBite3 { get; private set; }
        public AIAbility SlugPoisonRage3 { get; private set; }
        public AIAbility TornadoJolt1 { get; private set; }
        public AIAbility TornadoFling { get; private set; }
        public AIAbility TornadoToss { get; private set; }
        public AIAbility TheFogCurse { get; private set; }
        public AIAbility MonsterWerewolfPouncingRake { get; private set; }
        public AIAbility MonsterWerewolfPackAttack { get; private set; }
        public AIAbility MonsterWerewolfHowl { get; private set; }
        public AIAbility Werewolf_Summon_Rage { get; private set; }
        public AIAbility Werewolf_Summon_Opener { get; private set; }
        public AIAbility BleddynHowl { get; private set; }
        public AIAbility OrcSwordSlash { get; private set; }
        public AIAbility OrcParry { get; private set; }
        public AIAbility OrcFinishingBlow { get; private set; }
        public AIAbility OrcHipThrow { get; private set; }
        public AIAbility OrcKneeKick { get; private set; }
        public AIAbility OrcPunch { get; private set; }
        public AIAbility OrcArrow1 { get; private set; }
        public AIAbility OrcArrow2 { get; private set; }
        public AIAbility OrcStaffSmash { get; private set; }
        public AIAbility OrcFireball { get; private set; }
        public AIAbility OrcHeal1 { get; private set; }
        public AIAbility OrcHeal2 { get; private set; }
        public AIAbility OrcEvasionBubble { get; private set; }
        public AIAbility OrcElectricStun { get; private set; }
        public AIAbility OrcFireBolts { get; private set; }
        public AIAbility OrcKnockbackBolt { get; private set; }
        public AIAbility OrcSummonUrak2 { get; private set; }
        public AIAbility OrcSwordSlashFire { get; private set; }
        public AIAbility OrcParryFire { get; private set; }
        public AIAbility OrcFinishingBlowFire { get; private set; }
        public AIAbility OrcSummonSigil1 { get; private set; }
        public AIAbility OrcSpearAttack { get; private set; }
        public AIAbility OrcHalberdAttack { get; private set; }
        public AIAbility OrcAreaHalberdAttack { get; private set; }
        public AIAbility OrcDebuffArrow { get; private set; }
        public AIAbility OrcSlice { get; private set; }
        public AIAbility OrcVenomstrike1 { get; private set; }
        public AIAbility OrcVenomstrike0 { get; private set; }
        public AIAbility OrcLieutenantDebuffTaunt { get; private set; }
        public AIAbility OrcAreaHalberdBoss { get; private set; }
        public AIAbility OrcDeathsHold { get; private set; }
        public AIAbility GazlukPriest1Special { get; private set; }
        public AIAbility GazlukPriest2Special { get; private set; }
        public AIAbility GazlukPriest3Special { get; private set; }
        public AIAbility OrcExtinguishLife { get; private set; }
        public AIAbility OrcDarknessBall { get; private set; }
        public AIAbility OrcWaveOfDarkness { get; private set; }
        public AIAbility EnemyMinigolemPunch { get; private set; }
        public AIAbility EnemyMinigolemHeal { get; private set; }
        public AIAbility EnemyMinigolemExplode { get; private set; }
        public AIAbility MinigolemBombToss { get; private set; }
        public AIAbility MinigolemBombToss2 { get; private set; }
        public AIAbility MinigolemBombToss3 { get; private set; }
        public AIAbility MinigolemBombToss4 { get; private set; }
        public AIAbility MinigolemBombToss5 { get; private set; }
        public AIAbility MinigolemAoEHeal { get; private set; }
        public AIAbility MinigolemAoEHeal2 { get; private set; }
        public AIAbility MinigolemAoEHeal3 { get; private set; }
        public AIAbility MinigolemAoEHeal4 { get; private set; }
        public AIAbility MinigolemAoEHeal5 { get; private set; }
        public AIAbility MinigolemSelfDestruct { get; private set; }
        public AIAbility MinigolemSelfDestruct2 { get; private set; }
        public AIAbility MinigolemSelfDestruct3 { get; private set; }
        public AIAbility MinigolemSelfDestruct4 { get; private set; }
        public AIAbility MinigolemSelfDestruct5 { get; private set; }
        public AIAbility MinigolemAoEPower { get; private set; }
        public AIAbility MinigolemAoEPower2 { get; private set; }
        public AIAbility MinigolemAoEPower3 { get; private set; }
        public AIAbility MinigolemAoEPower4 { get; private set; }
        public AIAbility MinigolemAoEPower5 { get; private set; }
        public AIAbility MinigolemHeal { get; private set; }
        public AIAbility MinigolemHeal2 { get; private set; }
        public AIAbility MinigolemHeal3 { get; private set; }
        public AIAbility MinigolemHeal4 { get; private set; }
        public AIAbility MinigolemHeal5 { get; private set; }
        public AIAbility MinigolemDoomAdmixture { get; private set; }
        public AIAbility MinigolemDoomAdmixture2 { get; private set; }
        public AIAbility MinigolemDoomAdmixture3 { get; private set; }
        public AIAbility MinigolemDoomAdmixture4 { get; private set; }
        public AIAbility MinigolemDoomAdmixture5 { get; private set; }
        public AIAbility MinigolemSelfSacrifice { get; private set; }
        public AIAbility MinigolemSelfSacrifice2 { get; private set; }
        public AIAbility MinigolemSelfSacrifice3 { get; private set; }
        public AIAbility MinigolemSelfSacrifice4 { get; private set; }
        public AIAbility MinigolemSelfSacrifice5 { get; private set; }
        public AIAbility MinigolemPunch { get; private set; }
        public AIAbility MinigolemPunch2 { get; private set; }
        public AIAbility MinigolemPunch3 { get; private set; }
        public AIAbility MinigolemPunch4 { get; private set; }
        public AIAbility MinigolemPunch5 { get; private set; }
        public AIAbility MinigolemHasteConcoction1 { get; private set; }
        public AIAbility MinigolemHasteConcoction2 { get; private set; }
        public AIAbility MinigolemHasteConcoction3 { get; private set; }
        public AIAbility MinigolemFireBalm1 { get; private set; }
        public AIAbility MinigolemFireBalm2 { get; private set; }
        public AIAbility MinigolemFireBalm3 { get; private set; }
        public AIAbility MinigolemFireBalm4 { get; private set; }
        public AIAbility MinigolemFireBalm5 { get; private set; }
        public AIAbility MinigolemRageAoEHeal1 { get; private set; }
        public AIAbility MinigolemRageAoEHeal2 { get; private set; }
        public AIAbility MinigolemRageAoEHeal3 { get; private set; }
        public AIAbility MinigolemRageAoEHeal4 { get; private set; }
        public AIAbility MinigolemRageAoEHeal5 { get; private set; }
        public AIAbility MinigolemRageAcidToss1 { get; private set; }
        public AIAbility MinigolemRageAcidToss2 { get; private set; }
        public AIAbility MinigolemRageAcidToss3 { get; private set; }
        public AIAbility MinigolemRageAcidToss4 { get; private set; }
        public AIAbility MinigolemRageAcidToss5 { get; private set; }
        public AIAbility TrainingGolemPunch { get; private set; }
        public AIAbility TrainingGolemStun { get; private set; }
        public AIAbility TrainingGolemHeal { get; private set; }
        public AIAbility TrainingGolemHealB { get; private set; }
        public AIAbility TrainingGolemFireBreath { get; private set; }
        public AIAbility TrainingGolemFireBurst { get; private set; }
        public AIAbility GrimalkinClaw { get; private set; }
        public AIAbility GrimalkinBite { get; private set; }
        public AIAbility GrimalkinPuncture { get; private set; }
        public AIAbility WerewolfSword1 { get; private set; }
        public AIAbility WerewolfSword2 { get; private set; }
        public AIAbility WerewolfSwordStun { get; private set; }
        public AIAbility WerewolfArrow1 { get; private set; }
        public AIAbility WerewolfArrow2 { get; private set; }
        public AIAbility WerewolfOmegaArrow { get; private set; }
        public AIAbility NpcSmash { get; private set; }
        public AIAbility NpcDoubleHitCurse { get; private set; }
        public AIAbility NpcBlockingStance { get; private set; }
        public AIAbility NpcHeadcracker { get; private set; }
        public AIAbility StrigaClawA { get; private set; }
        public AIAbility StrigaClawB { get; private set; }
        public AIAbility StrigaReap { get; private set; }
        public AIAbility StrigaReap2 { get; private set; }
        public AIAbility StrigaFireBreath { get; private set; }
        public AIAbility StrigaBuff { get; private set; }
        public AIAbility GhostlyPunchA { get; private set; }
        public AIAbility GhostlyPunchB { get; private set; }
        public AIAbility GhostlyBurst { get; private set; }
        public AIAbility GhostlyBossPunchA { get; private set; }
        public AIAbility GhostlyBossPunchB { get; private set; }
        public AIAbility GhostlyBossBurst { get; private set; }
        public AIAbility GhostlyBolt { get; private set; }
        public AIAbility InjectorBugBite { get; private set; }
        public AIAbility InjectorBugInject { get; private set; }
        public AIAbility InjectorBugInject2 { get; private set; }
        public AIAbility FaceOfDeathKill { get; private set; }
        public AIAbility WatcherFireball { get; private set; }
        public AIAbility WatcherSlap { get; private set; }
        public AIAbility WatcherAcidball { get; private set; }
        public AIAbility RedCrystalBlast { get; private set; }
        public AIAbility RedCrystalBurst { get; private set; }
        public AIAbility TurretCrystalZap { get; private set; }
        public AIAbility TurretCrystalZap2 { get; private set; }
        public AIAbility TurretCrystalZapLongRange { get; private set; }
        public AIAbility TurretCrystalZapLongRange2 { get; private set; }
        public AIAbility DeathRay { get; private set; }
        public AIAbility SpyPortalZap { get; private set; }
        public AIAbility SpyPortalZap2 { get; private set; }
        public AIAbility BitingVineBite { get; private set; }
        public AIAbility BitingVineSpit { get; private set; }
        public AIAbility BitingVineSpitB { get; private set; }
        public AIAbility BitingVineCast { get; private set; }
        public AIAbility BitingVineAppear { get; private set; }
        public AIAbility BitingVineDisappear { get; private set; }
        public AIAbility TrollClubA { get; private set; }
        public AIAbility TrollClubB { get; private set; }
        public AIAbility TrollKnockdown { get; private set; }
        public AIAbility OgreClubA { get; private set; }
        public AIAbility OgreClubB { get; private set; }
        public AIAbility OgreThrow { get; private set; }
        public AIAbility OgreStun { get; private set; }
        public AIAbility FaeSwordA { get; private set; }
        public AIAbility FaeSwordB { get; private set; }
        public AIAbility FaeSwordKill { get; private set; }
        public AIAbility DementiaPuckCurse { get; private set; }
        public AIAbility FaeLightningSmiteHidden { get; private set; }
        public AIAbility NecroSpark { get; private set; }
        public AIAbility NecroDarknessWave { get; private set; }
        public AIAbility NecroPainBubble { get; private set; }
        public AIAbility NecroSparkPerching { get; private set; }
        public AIAbility NecroDeathsHold { get; private set; }
        public AIAbility DroachBiteA { get; private set; }
        public AIAbility DroachBiteB { get; private set; }
        public AIAbility DroachFireball { get; private set; }
        public AIAbility DroachFireballPerching { get; private set; }
        public AIAbility DroachBreatheFire { get; private set; }
        public AIAbility DroachLightning { get; private set; }
        public AIAbility DroachLightningPerching { get; private set; }
        public AIAbility DroachShockingKnockback { get; private set; }
        public AIAbility BasiliskClawA { get; private set; }
        public AIAbility BasiliskClawB { get; private set; }
        public AIAbility BasiliskToxicBite { get; private set; }
        public AIAbility BasiliskDebuff { get; private set; }
        public AIAbility BasiliskCastPerching { get; private set; }
        public AIAbility CultistArrow1 { get; private set; }
        public AIAbility CultistArrow2 { get; private set; }
        public AIAbility CultistOmegaArrow { get; private set; }
        public AIAbility CultistSword1 { get; private set; }
        public AIAbility CultistSword2 { get; private set; }
        public AIAbility CultistSwordStun { get; private set; }
        public AIAbility BossMegaSword1 { get; private set; }
        public AIAbility BossMegaSword2 { get; private set; }
        public AIAbility SedgewickMegaSwordAngry { get; private set; }
        public AIAbility BossMegaHammer { get; private set; }
        public AIAbility BossMegaHammer2 { get; private set; }
        public AIAbility BossMegaRageHammer { get; private set; }
        public AIAbility ClaudiaTundraSpikes { get; private set; }
        public AIAbility ClaudiaIceSpear { get; private set; }
        public AIAbility ClaudiaBlizzard { get; private set; }
        public AIAbility BigGolemHitA { get; private set; }
        public AIAbility BigGolemHitB { get; private set; }
        public AIAbility BigGolemFlingBoss { get; private set; }
        public AIAbility BigGolemPerchFix { get; private set; }
        public AIAbility BigGolemFlingBoss2 { get; private set; }
        public AIAbility BigGolemSummonFireSnake { get; private set; }
        public AIAbility BigGolemHitB_NoDisable { get; private set; }
        public AIAbility BigGolemHitA_NoDisable { get; private set; }
        public AIAbility BigGolemFling { get; private set; }
        public AIAbility GhoulClawA { get; private set; }
        public AIAbility GhoulClawB { get; private set; }
        public AIAbility GhoulSelfBuff { get; private set; }
        public AIAbility GhoulHammerA { get; private set; }
        public AIAbility GhoulHammerB { get; private set; }
        public AIAbility DragonWormSpitElectricity { get; private set; }
        public AIAbility DragonWormBite { get; private set; }
        public AIAbility DragonWormSmack { get; private set; }
        public AIAbility DragonWormRage { get; private set; }
        public AIAbility DragonWormEscape { get; private set; }
        public AIAbility DragonWormSpitFire { get; private set; }
        public AIAbility ColdSphereBurst { get; private set; }
        public AIAbility ColdSphereFreezeBurst { get; private set; }
        public AIAbility ManticoreBite { get; private set; }
        public AIAbility ManticoreClaw { get; private set; }
        public AIAbility ManticoreSting1 { get; private set; }
        public AIAbility Manticoresting2 { get; private set; }
        public AIAbility RakStaffHit { get; private set; }
        public AIAbility RakStaffPin { get; private set; }
        public AIAbility RakStaffBlock { get; private set; }
        public AIAbility RakStaffHeavy { get; private set; }
        public AIAbility RakSlash { get; private set; }
        public AIAbility RakKnee { get; private set; }
        public AIAbility RakKick { get; private set; }
        public AIAbility RakBarrage { get; private set; }
        public AIAbility RakSwordSlash { get; private set; }
        public AIAbility RakHackingBlade { get; private set; }
        public AIAbility RakDecapitate { get; private set; }
        public AIAbility RakFireball { get; private set; }
        public AIAbility RakBreatheFire { get; private set; }
        public AIAbility RakRingOfFire { get; private set; }
        public AIAbility RakToxinBomb { get; private set; }
        public AIAbility RakAcidBomb { get; private set; }
        public AIAbility RakHealingMist { get; private set; }
        public AIAbility RakBasicShot { get; private set; }
        public AIAbility RakHookShot { get; private set; }
        public AIAbility RakBowBash { get; private set; }
        public AIAbility RakAimedShot { get; private set; }
        public AIAbility RakPoisonArrow { get; private set; }
        public AIAbility RakMindreave { get; private set; }
        public AIAbility RakPainBubble { get; private set; }
        public AIAbility RakPanicCharge { get; private set; }
        public AIAbility RakRevitalize { get; private set; }
        public AIAbility RakReconstruct { get; private set; }
        public AIAbility RakBossSlow { get; private set; }
        public AIAbility RakBossPerchSlow { get; private set; }
        public AIAbility FlapSkullBite { get; private set; }
        public AIAbility FlapSkullBigBite { get; private set; }
        public AIAbility MinotaurClub { get; private set; }
        public AIAbility MinotaurRageClub { get; private set; }
        public AIAbility MinotaurBoulder { get; private set; }
        public AIAbility MinotaurBossRageClub { get; private set; }
        public AIAbility CockatricePeck { get; private set; }
        public AIAbility CockatriceTailWhip { get; private set; }
        public AIAbility CockatriceParalyze { get; private set; }
        public AIAbility GiantBeetleBite { get; private set; }
        public AIAbility GiantBeetleInject { get; private set; }
        public AIAbility GiantBeetleBoulderSpit { get; private set; }
        public AIAbility BatIllusionSlashA { get; private set; }
        public AIAbility BatIllusionSlashB { get; private set; }
        public AIAbility BatIllusionBite { get; private set; }
        public AIAbility GiantBatSlashA { get; private set; }
        public AIAbility GiantBatSlashB { get; private set; }
        public AIAbility GiantBatBite { get; private set; }
        public AIAbility HagAgingTouch { get; private set; }
        public AIAbility HagAgingScream { get; private set; }
        public AIAbility TriffidClawA { get; private set; }
        public AIAbility TriffidClawB { get; private set; }
        public AIAbility TriffidTongue { get; private set; }
        public AIAbility TriffidSpore { get; private set; }
        public AIAbility TriffidShot { get; private set; }
        public AIAbility TriffidTongueElite { get; private set; }
        public AIAbility GiantScorpionClawA { get; private set; }
        public AIAbility GiantScorpionClawB { get; private set; }
        public AIAbility GiantScorpionSting { get; private set; }
        public AIAbility KrakenBeak { get; private set; }
        public AIAbility KrakenSlam { get; private set; }
        public AIAbility KrakenRage { get; private set; }
        public AIAbility KrakenBabyBeak { get; private set; }
        public AIAbility KrakenBabySlam { get; private set; }
        public AIAbility KrakenBabyRage { get; private set; }
        public AIAbility RanalonHit { get; private set; }
        public AIAbility RanalonHitB { get; private set; }
        public AIAbility RanalonKick { get; private set; }
        public AIAbility RanalonTongue { get; private set; }
        public AIAbility RanalonZap { get; private set; }
        public AIAbility RanalonZapB { get; private set; }
        public AIAbility RanalonHeal { get; private set; }
        public AIAbility RanalonRoot { get; private set; }
        public AIAbility RanalonSelfBuff { get; private set; }
        public AIAbility RanalonSelfBuffElite { get; private set; }
        public AIAbility RanalonGuardianStab { get; private set; }
        public AIAbility RanalonGuardianStabB { get; private set; }
        public AIAbility RanalonGuardianBite { get; private set; }
        public AIAbility RanalonGuardianBlind { get; private set; }
        public AIAbility RanalonDoctrineKeeperStab { get; private set; }
        public AIAbility RanalonDoctrineKeeperBlind { get; private set; }
        public AIAbility BarghestBiteA { get; private set; }
        public AIAbility BarghestBiteB { get; private set; }
        public AIAbility BarghestDebuff { get; private set; }
        public AIAbility WorghestDebuff { get; private set; }
        public AIAbility BallistaFire { get; private set; }
        public AIAbility BallistaFire_Long { get; private set; }
        public AIAbility GargoyleSlamA { get; private set; }
        public AIAbility GargoyleSlamB { get; private set; }
        public AIAbility GargoyleStun { get; private set; }
        public AIAbility GargoyleBossStun { get; private set; }
        public AIAbility ScrayBite { get; private set; }
        public AIAbility ScrayStab { get; private set; }
        public AIAbility HippoBite { get; private set; }
        public AIAbility HippoBiteAndHeal1 { get; private set; }
        public AIAbility BigCatClaw_Pet { get; private set; }
        public AIAbility BigCatPounce_Pet { get; private set; }
        public AIAbility BigCatKill_Pet1 { get; private set; }
        public AIAbility BigCatKill_Pet2 { get; private set; }
        public AIAbility BigCatKill_Pet3 { get; private set; }
        public AIAbility BigCatKill_Pet4 { get; private set; }
        public AIAbility BigCatKill_Pet5 { get; private set; }
        public AIAbility BigCatKill_Pet6 { get; private set; }
        public AIAbility BigCatUltraKill_Pet1 { get; private set; }
        public AIAbility BigCatUltraKill_Pet2 { get; private set; }
        public AIAbility BigCatUltraKill_Pet3 { get; private set; }
        public AIAbility BigCatUltraKill_Pet4 { get; private set; }
        public AIAbility BigCatUltraKill_Pet5 { get; private set; }
        public AIAbility BigCatUltraKill_Pet6 { get; private set; }
        public AIAbility BigCatRoot_Pet1 { get; private set; }
        public AIAbility BigCatRoot_Pet2 { get; private set; }
        public AIAbility BigCatRoot_Pet3 { get; private set; }
        public AIAbility BigCatRoot_Pet4 { get; private set; }
        public AIAbility BigCatVuln_Pet1 { get; private set; }
        public AIAbility BigCatVuln_Pet2 { get; private set; }
        public AIAbility BigCatVuln_Pet3 { get; private set; }
        public AIAbility BigCatVuln_Pet4 { get; private set; }
        public AIAbility BigCatHeal_Pet1 { get; private set; }
        public AIAbility BigCatHeal_Pet2 { get; private set; }
        public AIAbility BigCatHeal_Pet3 { get; private set; }
        public AIAbility BigCatHeal_Pet4 { get; private set; }
        public AIAbility BigCatHeal_Pet5 { get; private set; }
        public AIAbility BigCatHeal_Pet6 { get; private set; }
        public AIAbility GrimalkinPuncture_Pet1 { get; private set; }
        public AIAbility GrimalkinPuncture_Pet2 { get; private set; }
        public AIAbility GrimalkinPuncture_Pet3 { get; private set; }
        public AIAbility GrimalkinPuncture_Pet4 { get; private set; }
        public AIAbility GrimalkinPuncture_Pet5 { get; private set; }
        public AIAbility GrimalkinPuncture_Pet6 { get; private set; }
        public AIAbility GrimalkinFlee_Pet1 { get; private set; }
        public AIAbility GrimalkinFlee_Pet2 { get; private set; }
        public AIAbility GrimalkinFlee_Pet3 { get; private set; }
        public AIAbility RatBite_Pet { get; private set; }
        public AIAbility RatClaw_Pet { get; private set; }
        public AIAbility RatDeRage_Pet1 { get; private set; }
        public AIAbility RatDeRage_Pet2 { get; private set; }
        public AIAbility RatDeRage_Pet3 { get; private set; }
        public AIAbility RatDeRage_Pet4 { get; private set; }
        public AIAbility RatDeRage_Pet5 { get; private set; }
        public AIAbility RatDeRage_Pet6 { get; private set; }
        public AIAbility RatHeal_Pet1 { get; private set; }
        public AIAbility RatHeal_Pet2 { get; private set; }
        public AIAbility RatHeal_Pet3 { get; private set; }
        public AIAbility RatHeal_Pet4 { get; private set; }
        public AIAbility RatHeal_Pet5 { get; private set; }
        public AIAbility RatHeal_Pet6 { get; private set; }
        public AIAbility RatVuln_Pet1 { get; private set; }
        public AIAbility RatVuln_Pet2 { get; private set; }
        public AIAbility RatVuln_Pet3 { get; private set; }
        public AIAbility RatVuln_Pet4 { get; private set; }
        public AIAbility FireRatBite_Pet { get; private set; }
        public AIAbility FireRatClaw_Pet { get; private set; }
        public AIAbility RatBurn_Pet1 { get; private set; }
        public AIAbility RatBurn_Pet2 { get; private set; }
        public AIAbility RatBurn_Pet3 { get; private set; }
        public AIAbility RatBurn_Pet4 { get; private set; }
        public AIAbility RatBurn_Pet5 { get; private set; }
        public AIAbility RatBurn_Pet6 { get; private set; }
        public AIAbility BearBite_Pet { get; private set; }
        public AIAbility BearClaw_Pet { get; private set; }
        public AIAbility BearTaunt_Pet1 { get; private set; }
        public AIAbility BearTaunt_Pet2 { get; private set; }
        public AIAbility BearTaunt_Pet3 { get; private set; }
        public AIAbility BearTaunt_Pet4 { get; private set; }
        public AIAbility BearTaunt_Pet5 { get; private set; }
        public AIAbility BearTaunt_Pet6 { get; private set; }
        public AIAbility BearStun_Pet1 { get; private set; }
        public AIAbility BearStun_Pet2 { get; private set; }
        public AIAbility BearStun_Pet3 { get; private set; }
        public AIAbility BearStun_Pet4 { get; private set; }
        public AIAbility BearWarmth_Pet1 { get; private set; }
        public AIAbility BearWarmth_Pet2 { get; private set; }
        public AIAbility BearWarmth_Pet3 { get; private set; }
        public AIAbility BearWarmth_Pet4 { get; private set; }
        public AIAbility BearWarmth_Pet5 { get; private set; }
        public AIAbility BearWarmth_Pet6 { get; private set; }
        public AIAbility BearSelfHeal_Pet1 { get; private set; }
        public AIAbility BearSelfHeal_Pet2 { get; private set; }
        public AIAbility BearSelfHeal_Pet3 { get; private set; }
        public AIAbility BearSelfHeal_Pet4 { get; private set; }
        public AIAbility BearSelfHeal_Pet5 { get; private set; }
        public AIAbility BearSelfHeal_Pet6 { get; private set; }
        public AIAbility BearUltra_Pet1 { get; private set; }
        public AIAbility BearUltra_Pet2 { get; private set; }
        public AIAbility BearUltra_Pet3 { get; private set; }
        public AIAbility BearUltra_Pet4 { get; private set; }
        public AIAbility BearUltra_Pet5 { get; private set; }
        public AIAbility BearUltra_Pet6 { get; private set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return Key; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
           { "AcidAuraBall1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidAuraBall1 = 
                   JsonObjectParser<AIAbility>.Parse("AcidAuraBall1", value, errorInfo),
                GetObject = () => AcidAuraBall1 } },
           { "AcidAuraBall2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidAuraBall2 = 
                   JsonObjectParser<AIAbility>.Parse("AcidAuraBall2", value, errorInfo),
                GetObject = () => AcidAuraBall2 } },
           { "AcidAuraBall3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidAuraBall3 = 
                   JsonObjectParser<AIAbility>.Parse("AcidAuraBall3", value, errorInfo),
                GetObject = () => AcidAuraBall3 } },
           { "AcidAuraBall4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidAuraBall4 = 
                   JsonObjectParser<AIAbility>.Parse("AcidAuraBall4", value, errorInfo),
                GetObject = () => AcidAuraBall4 } },
           { "AcidBall1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidBall1 = 
                   JsonObjectParser<AIAbility>.Parse("AcidBall1", value, errorInfo),
                GetObject = () => AcidBall1 } },
           { "AcidBall2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidBall2 = 
                   JsonObjectParser<AIAbility>.Parse("AcidBall2", value, errorInfo),
                GetObject = () => AcidBall2 } },
           { "AcidExplosion1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidExplosion1 = 
                   JsonObjectParser<AIAbility>.Parse("AcidExplosion1", value, errorInfo),
                GetObject = () => AcidExplosion1 } },
           { "AcidExplosion2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidExplosion2 = 
                   JsonObjectParser<AIAbility>.Parse("AcidExplosion2", value, errorInfo),
                GetObject = () => AcidExplosion2 } },
           { "AcidSpew1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidSpew1 = 
                   JsonObjectParser<AIAbility>.Parse("AcidSpew1", value, errorInfo),
                GetObject = () => AcidSpew1 } },
           { "AlienDog_Punch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AlienDog_Punch = 
                   JsonObjectParser<AIAbility>.Parse("AlienDog_Punch", value, errorInfo),
                GetObject = () => AlienDog_Punch } },
           { "AlienDog_Punch2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AlienDog_Punch2 = 
                   JsonObjectParser<AIAbility>.Parse("AlienDog_Punch2", value, errorInfo),
                GetObject = () => AlienDog_Punch2 } },
           { "AlienDog_RagePunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AlienDog_RagePunch = 
                   JsonObjectParser<AIAbility>.Parse("AlienDog_RagePunch", value, errorInfo),
                GetObject = () => AlienDog_RagePunch } },
           { "AnimalBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalBite = 
                   JsonObjectParser<AIAbility>.Parse("AnimalBite", value, errorInfo),
                GetObject = () => AnimalBite } },
           { "AnimalClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalClaw = 
                   JsonObjectParser<AIAbility>.Parse("AnimalClaw", value, errorInfo),
                GetObject = () => AnimalClaw } },
           { "AnimalFlee", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalFlee = 
                   JsonObjectParser<AIAbility>.Parse("AnimalFlee", value, errorInfo),
                GetObject = () => AnimalFlee } },
           { "AnimalHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHeal = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHeal", value, errorInfo),
                GetObject = () => AnimalHeal } },
           { "AnimalHeal2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHeal2 = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHeal2", value, errorInfo),
                GetObject = () => AnimalHeal2 } },
           { "AnimalHeal3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHeal3 = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHeal3", value, errorInfo),
                GetObject = () => AnimalHeal3 } },
           { "AnimalHoofFieryFrontKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHoofFieryFrontKick = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHoofFieryFrontKick", value, errorInfo),
                GetObject = () => AnimalHoofFieryFrontKick } },
           { "AnimalHoofFieryFrontKick2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHoofFieryFrontKick2 = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHoofFieryFrontKick2", value, errorInfo),
                GetObject = () => AnimalHoofFieryFrontKick2 } },
           { "AnimalHoofFrontKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHoofFrontKick = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHoofFrontKick", value, errorInfo),
                GetObject = () => AnimalHoofFrontKick } },
           { "AnimalHoofRageKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHoofRageKick = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHoofRageKick", value, errorInfo),
                GetObject = () => AnimalHoofRageKick } },
           { "AnimalHoofRageKick2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHoofRageKick2 = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHoofRageKick2", value, errorInfo),
                GetObject = () => AnimalHoofRageKick2 } },
           { "AnimalOmegaBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalOmegaBite = 
                   JsonObjectParser<AIAbility>.Parse("AnimalOmegaBite", value, errorInfo),
                GetObject = () => AnimalOmegaBite } },
           { "AnimalOmegaBite2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalOmegaBite2 = 
                   JsonObjectParser<AIAbility>.Parse("AnimalOmegaBite2", value, errorInfo),
                GetObject = () => AnimalOmegaBite2 } },
           { "BallistaFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BallistaFire = 
                   JsonObjectParser<AIAbility>.Parse("BallistaFire", value, errorInfo),
                GetObject = () => BallistaFire } },
           { "BallistaFire_Long", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BallistaFire_Long = 
                   JsonObjectParser<AIAbility>.Parse("BallistaFire_Long", value, errorInfo),
                GetObject = () => BallistaFire_Long } },
           { "BarghestBiteA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BarghestBiteA = 
                   JsonObjectParser<AIAbility>.Parse("BarghestBiteA", value, errorInfo),
                GetObject = () => BarghestBiteA } },
           { "BarghestBiteB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BarghestBiteB = 
                   JsonObjectParser<AIAbility>.Parse("BarghestBiteB", value, errorInfo),
                GetObject = () => BarghestBiteB } },
           { "BarghestDebuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BarghestDebuff = 
                   JsonObjectParser<AIAbility>.Parse("BarghestDebuff", value, errorInfo),
                GetObject = () => BarghestDebuff } },
           { "BarutiWrapA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BarutiWrapA = 
                   JsonObjectParser<AIAbility>.Parse("BarutiWrapA", value, errorInfo),
                GetObject = () => BarutiWrapA } },
           { "BarutiWrapB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BarutiWrapB = 
                   JsonObjectParser<AIAbility>.Parse("BarutiWrapB", value, errorInfo),
                GetObject = () => BarutiWrapB } },
           { "BarutiWrapRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BarutiWrapRage = 
                   JsonObjectParser<AIAbility>.Parse("BarutiWrapRage", value, errorInfo),
                GetObject = () => BarutiWrapRage } },
           { "BasiliskCastPerching", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BasiliskCastPerching = 
                   JsonObjectParser<AIAbility>.Parse("BasiliskCastPerching", value, errorInfo),
                GetObject = () => BasiliskCastPerching } },
           { "BasiliskClawA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BasiliskClawA = 
                   JsonObjectParser<AIAbility>.Parse("BasiliskClawA", value, errorInfo),
                GetObject = () => BasiliskClawA } },
           { "BasiliskClawB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BasiliskClawB = 
                   JsonObjectParser<AIAbility>.Parse("BasiliskClawB", value, errorInfo),
                GetObject = () => BasiliskClawB } },
           { "BasiliskDebuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BasiliskDebuff = 
                   JsonObjectParser<AIAbility>.Parse("BasiliskDebuff", value, errorInfo),
                GetObject = () => BasiliskDebuff } },
           { "BasiliskToxicBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BasiliskToxicBite = 
                   JsonObjectParser<AIAbility>.Parse("BasiliskToxicBite", value, errorInfo),
                GetObject = () => BasiliskToxicBite } },
           { "BatIllusionBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BatIllusionBite = 
                   JsonObjectParser<AIAbility>.Parse("BatIllusionBite", value, errorInfo),
                GetObject = () => BatIllusionBite } },
           { "BatIllusionSlashA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BatIllusionSlashA = 
                   JsonObjectParser<AIAbility>.Parse("BatIllusionSlashA", value, errorInfo),
                GetObject = () => BatIllusionSlashA } },
           { "BatIllusionSlashB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BatIllusionSlashB = 
                   JsonObjectParser<AIAbility>.Parse("BatIllusionSlashB", value, errorInfo),
                GetObject = () => BatIllusionSlashB } },
           { "BearBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearBite = 
                   JsonObjectParser<AIAbility>.Parse("BearBite", value, errorInfo),
                GetObject = () => BearBite } },
           { "BearBite_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearBite_Pet = 
                   JsonObjectParser<AIAbility>.Parse("BearBite_Pet", value, errorInfo),
                GetObject = () => BearBite_Pet } },
           { "BearClaw_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearClaw_Pet = 
                   JsonObjectParser<AIAbility>.Parse("BearClaw_Pet", value, errorInfo),
                GetObject = () => BearClaw_Pet } },
           { "BearCrush", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearCrush = 
                   JsonObjectParser<AIAbility>.Parse("BearCrush", value, errorInfo),
                GetObject = () => BearCrush } },
           { "BearSelfHeal_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearSelfHeal_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BearSelfHeal_Pet1", value, errorInfo),
                GetObject = () => BearSelfHeal_Pet1 } },
           { "BearSelfHeal_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearSelfHeal_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BearSelfHeal_Pet2", value, errorInfo),
                GetObject = () => BearSelfHeal_Pet2 } },
           { "BearSelfHeal_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearSelfHeal_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BearSelfHeal_Pet3", value, errorInfo),
                GetObject = () => BearSelfHeal_Pet3 } },
           { "BearSelfHeal_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearSelfHeal_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BearSelfHeal_Pet4", value, errorInfo),
                GetObject = () => BearSelfHeal_Pet4 } },
           { "BearSelfHeal_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearSelfHeal_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("BearSelfHeal_Pet5", value, errorInfo),
                GetObject = () => BearSelfHeal_Pet5 } },
           { "BearSelfHeal_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearSelfHeal_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("BearSelfHeal_Pet6", value, errorInfo),
                GetObject = () => BearSelfHeal_Pet6 } },
           { "BearStun_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearStun_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BearStun_Pet1", value, errorInfo),
                GetObject = () => BearStun_Pet1 } },
           { "BearStun_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearStun_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BearStun_Pet2", value, errorInfo),
                GetObject = () => BearStun_Pet2 } },
           { "BearStun_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearStun_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BearStun_Pet3", value, errorInfo),
                GetObject = () => BearStun_Pet3 } },
           { "BearStun_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearStun_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BearStun_Pet4", value, errorInfo),
                GetObject = () => BearStun_Pet4 } },
           { "BearTaunt_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearTaunt_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BearTaunt_Pet1", value, errorInfo),
                GetObject = () => BearTaunt_Pet1 } },
           { "BearTaunt_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearTaunt_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BearTaunt_Pet2", value, errorInfo),
                GetObject = () => BearTaunt_Pet2 } },
           { "BearTaunt_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearTaunt_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BearTaunt_Pet3", value, errorInfo),
                GetObject = () => BearTaunt_Pet3 } },
           { "BearTaunt_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearTaunt_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BearTaunt_Pet4", value, errorInfo),
                GetObject = () => BearTaunt_Pet4 } },
           { "BearTaunt_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearTaunt_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("BearTaunt_Pet5", value, errorInfo),
                GetObject = () => BearTaunt_Pet5 } },
           { "BearTaunt_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearTaunt_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("BearTaunt_Pet6", value, errorInfo),
                GetObject = () => BearTaunt_Pet6 } },
           { "BearUltra_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearUltra_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BearUltra_Pet1", value, errorInfo),
                GetObject = () => BearUltra_Pet1 } },
           { "BearUltra_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearUltra_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BearUltra_Pet2", value, errorInfo),
                GetObject = () => BearUltra_Pet2 } },
           { "BearUltra_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearUltra_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BearUltra_Pet3", value, errorInfo),
                GetObject = () => BearUltra_Pet3 } },
           { "BearUltra_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearUltra_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BearUltra_Pet4", value, errorInfo),
                GetObject = () => BearUltra_Pet4 } },
           { "BearUltra_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearUltra_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("BearUltra_Pet5", value, errorInfo),
                GetObject = () => BearUltra_Pet5 } },
           { "BearUltra_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearUltra_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("BearUltra_Pet6", value, errorInfo),
                GetObject = () => BearUltra_Pet6 } },
           { "BearWarmth_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearWarmth_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BearWarmth_Pet1", value, errorInfo),
                GetObject = () => BearWarmth_Pet1 } },
           { "BearWarmth_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearWarmth_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BearWarmth_Pet2", value, errorInfo),
                GetObject = () => BearWarmth_Pet2 } },
           { "BearWarmth_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearWarmth_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BearWarmth_Pet3", value, errorInfo),
                GetObject = () => BearWarmth_Pet3 } },
           { "BearWarmth_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearWarmth_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BearWarmth_Pet4", value, errorInfo),
                GetObject = () => BearWarmth_Pet4 } },
           { "BearWarmth_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearWarmth_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("BearWarmth_Pet5", value, errorInfo),
                GetObject = () => BearWarmth_Pet5 } },
           { "BearWarmth_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearWarmth_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("BearWarmth_Pet6", value, errorInfo),
                GetObject = () => BearWarmth_Pet6 } },
           { "BigCatClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatClaw = 
                   JsonObjectParser<AIAbility>.Parse("BigCatClaw", value, errorInfo),
                GetObject = () => BigCatClaw } },
           { "BigCatClaw_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatClaw_Pet = 
                   JsonObjectParser<AIAbility>.Parse("BigCatClaw_Pet", value, errorInfo),
                GetObject = () => BigCatClaw_Pet } },
           { "BigCatDebuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatDebuff = 
                   JsonObjectParser<AIAbility>.Parse("BigCatDebuff", value, errorInfo),
                GetObject = () => BigCatDebuff } },
           { "BigCatHeal_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatHeal_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatHeal_Pet1", value, errorInfo),
                GetObject = () => BigCatHeal_Pet1 } },
           { "BigCatHeal_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatHeal_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatHeal_Pet2", value, errorInfo),
                GetObject = () => BigCatHeal_Pet2 } },
           { "BigCatHeal_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatHeal_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatHeal_Pet3", value, errorInfo),
                GetObject = () => BigCatHeal_Pet3 } },
           { "BigCatHeal_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatHeal_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatHeal_Pet4", value, errorInfo),
                GetObject = () => BigCatHeal_Pet4 } },
           { "BigCatHeal_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatHeal_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatHeal_Pet5", value, errorInfo),
                GetObject = () => BigCatHeal_Pet5 } },
           { "BigCatHeal_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatHeal_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatHeal_Pet6", value, errorInfo),
                GetObject = () => BigCatHeal_Pet6 } },
           { "BigCatKill_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatKill_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatKill_Pet1", value, errorInfo),
                GetObject = () => BigCatKill_Pet1 } },
           { "BigCatKill_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatKill_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatKill_Pet2", value, errorInfo),
                GetObject = () => BigCatKill_Pet2 } },
           { "BigCatKill_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatKill_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatKill_Pet3", value, errorInfo),
                GetObject = () => BigCatKill_Pet3 } },
           { "BigCatKill_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatKill_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatKill_Pet4", value, errorInfo),
                GetObject = () => BigCatKill_Pet4 } },
           { "BigCatKill_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatKill_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatKill_Pet5", value, errorInfo),
                GetObject = () => BigCatKill_Pet5 } },
           { "BigCatKill_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatKill_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatKill_Pet6", value, errorInfo),
                GetObject = () => BigCatKill_Pet6 } },
           { "BigCatPounce", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatPounce = 
                   JsonObjectParser<AIAbility>.Parse("BigCatPounce", value, errorInfo),
                GetObject = () => BigCatPounce } },
           { "BigCatPounce_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatPounce_Pet = 
                   JsonObjectParser<AIAbility>.Parse("BigCatPounce_Pet", value, errorInfo),
                GetObject = () => BigCatPounce_Pet } },
           { "BigCatRagePounce", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatRagePounce = 
                   JsonObjectParser<AIAbility>.Parse("BigCatRagePounce", value, errorInfo),
                GetObject = () => BigCatRagePounce } },
           { "BigCatRoot_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatRoot_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatRoot_Pet1", value, errorInfo),
                GetObject = () => BigCatRoot_Pet1 } },
           { "BigCatRoot_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatRoot_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatRoot_Pet2", value, errorInfo),
                GetObject = () => BigCatRoot_Pet2 } },
           { "BigCatRoot_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatRoot_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatRoot_Pet3", value, errorInfo),
                GetObject = () => BigCatRoot_Pet3 } },
           { "BigCatRoot_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatRoot_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatRoot_Pet4", value, errorInfo),
                GetObject = () => BigCatRoot_Pet4 } },
           { "BigCatUltraKill_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatUltraKill_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatUltraKill_Pet1", value, errorInfo),
                GetObject = () => BigCatUltraKill_Pet1 } },
           { "BigCatUltraKill_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatUltraKill_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatUltraKill_Pet2", value, errorInfo),
                GetObject = () => BigCatUltraKill_Pet2 } },
           { "BigCatUltraKill_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatUltraKill_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatUltraKill_Pet3", value, errorInfo),
                GetObject = () => BigCatUltraKill_Pet3 } },
           { "BigCatUltraKill_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatUltraKill_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatUltraKill_Pet4", value, errorInfo),
                GetObject = () => BigCatUltraKill_Pet4 } },
           { "BigCatUltraKill_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatUltraKill_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatUltraKill_Pet5", value, errorInfo),
                GetObject = () => BigCatUltraKill_Pet5 } },
           { "BigCatUltraKill_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatUltraKill_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatUltraKill_Pet6", value, errorInfo),
                GetObject = () => BigCatUltraKill_Pet6 } },
           { "BigCatVuln_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatVuln_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatVuln_Pet1", value, errorInfo),
                GetObject = () => BigCatVuln_Pet1 } },
           { "BigCatVuln_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatVuln_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatVuln_Pet2", value, errorInfo),
                GetObject = () => BigCatVuln_Pet2 } },
           { "BigCatVuln_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatVuln_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatVuln_Pet3", value, errorInfo),
                GetObject = () => BigCatVuln_Pet3 } },
           { "BigCatVuln_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatVuln_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatVuln_Pet4", value, errorInfo),
                GetObject = () => BigCatVuln_Pet4 } },
           { "BigGolemFling", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemFling = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemFling", value, errorInfo),
                GetObject = () => BigGolemFling } },
           { "BigGolemFlingBoss", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemFlingBoss = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemFlingBoss", value, errorInfo),
                GetObject = () => BigGolemFlingBoss } },
           { "BigGolemFlingBoss2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemFlingBoss2 = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemFlingBoss2", value, errorInfo),
                GetObject = () => BigGolemFlingBoss2 } },
           { "BigGolemHitA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemHitA = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemHitA", value, errorInfo),
                GetObject = () => BigGolemHitA } },
           { "BigGolemHitA-NoDisable", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemHitA_NoDisable = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemHitA-NoDisable", value, errorInfo),
                GetObject = () => BigGolemHitA_NoDisable } },
           { "BigGolemHitB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemHitB = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemHitB", value, errorInfo),
                GetObject = () => BigGolemHitB } },
           { "BigGolemHitB-NoDisable", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemHitB_NoDisable = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemHitB-NoDisable", value, errorInfo),
                GetObject = () => BigGolemHitB_NoDisable } },
           { "BigGolemPerchFix", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemPerchFix = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemPerchFix", value, errorInfo),
                GetObject = () => BigGolemPerchFix } },
           { "BigGolemSummonFireSnake", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemSummonFireSnake = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemSummonFireSnake", value, errorInfo),
                GetObject = () => BigGolemSummonFireSnake } },
           { "BigHeadCurseball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigHeadCurseball = 
                   JsonObjectParser<AIAbility>.Parse("BigHeadCurseball", value, errorInfo),
                GetObject = () => BigHeadCurseball } },
           { "BitingVineAppear", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BitingVineAppear = 
                   JsonObjectParser<AIAbility>.Parse("BitingVineAppear", value, errorInfo),
                GetObject = () => BitingVineAppear } },
           { "BitingVineBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BitingVineBite = 
                   JsonObjectParser<AIAbility>.Parse("BitingVineBite", value, errorInfo),
                GetObject = () => BitingVineBite } },
           { "BitingVineCast", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BitingVineCast = 
                   JsonObjectParser<AIAbility>.Parse("BitingVineCast", value, errorInfo),
                GetObject = () => BitingVineCast } },
           { "BitingVineDisappear", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BitingVineDisappear = 
                   JsonObjectParser<AIAbility>.Parse("BitingVineDisappear", value, errorInfo),
                GetObject = () => BitingVineDisappear } },
           { "BitingVineSpit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BitingVineSpit = 
                   JsonObjectParser<AIAbility>.Parse("BitingVineSpit", value, errorInfo),
                GetObject = () => BitingVineSpit } },
           { "BitingVineSpitB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BitingVineSpitB = 
                   JsonObjectParser<AIAbility>.Parse("BitingVineSpitB", value, errorInfo),
                GetObject = () => BitingVineSpitB } },
           { "BleddynHowl", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BleddynHowl = 
                   JsonObjectParser<AIAbility>.Parse("BleddynHowl", value, errorInfo),
                GetObject = () => BleddynHowl } },
           { "BossMegaHammer", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossMegaHammer = 
                   JsonObjectParser<AIAbility>.Parse("BossMegaHammer", value, errorInfo),
                GetObject = () => BossMegaHammer } },
           { "BossMegaHammer2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossMegaHammer2 = 
                   JsonObjectParser<AIAbility>.Parse("BossMegaHammer2", value, errorInfo),
                GetObject = () => BossMegaHammer2 } },
           { "BossMegaRageHammer", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossMegaRageHammer = 
                   JsonObjectParser<AIAbility>.Parse("BossMegaRageHammer", value, errorInfo),
                GetObject = () => BossMegaRageHammer } },
           { "BossMegaSword1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossMegaSword1 = 
                   JsonObjectParser<AIAbility>.Parse("BossMegaSword1", value, errorInfo),
                GetObject = () => BossMegaSword1 } },
           { "BossMegaSword2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossMegaSword2 = 
                   JsonObjectParser<AIAbility>.Parse("BossMegaSword2", value, errorInfo),
                GetObject = () => BossMegaSword2 } },
           { "BossSlime_SummonSlime1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossSlime_SummonSlime1 = 
                   JsonObjectParser<AIAbility>.Parse("BossSlime_SummonSlime1", value, errorInfo),
                GetObject = () => BossSlime_SummonSlime1 } },
           { "BossSlime_SummonSlime4Elite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossSlime_SummonSlime4Elite = 
                   JsonObjectParser<AIAbility>.Parse("BossSlime_SummonSlime4Elite", value, errorInfo),
                GetObject = () => BossSlime_SummonSlime4Elite } },
           { "BossSlimeKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossSlimeKick = 
                   JsonObjectParser<AIAbility>.Parse("BossSlimeKick", value, errorInfo),
                GetObject = () => BossSlimeKick } },
           { "BossSlimeKick2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossSlimeKick2 = 
                   JsonObjectParser<AIAbility>.Parse("BossSlimeKick2", value, errorInfo),
                GetObject = () => BossSlimeKick2 } },
           { "BossSlimeKick2B", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossSlimeKick2B = 
                   JsonObjectParser<AIAbility>.Parse("BossSlimeKick2B", value, errorInfo),
                GetObject = () => BossSlimeKick2B } },
           { "BossSlimeKickB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossSlimeKickB = 
                   JsonObjectParser<AIAbility>.Parse("BossSlimeKickB", value, errorInfo),
                GetObject = () => BossSlimeKickB } },
           { "BrainBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BrainBite = 
                   JsonObjectParser<AIAbility>.Parse("BrainBite", value, errorInfo),
                GetObject = () => BrainBite } },
           { "BrainDrain", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BrainDrain = 
                   JsonObjectParser<AIAbility>.Parse("BrainDrain", value, errorInfo),
                GetObject = () => BrainDrain } },
           { "BrainDrain2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BrainDrain2 = 
                   JsonObjectParser<AIAbility>.Parse("BrainDrain2", value, errorInfo),
                GetObject = () => BrainDrain2 } },
           { "CiervosDarknessBomb", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CiervosDarknessBomb = 
                   JsonObjectParser<AIAbility>.Parse("CiervosDarknessBomb", value, errorInfo),
                GetObject = () => CiervosDarknessBomb } },
           { "CiervosNightmareHoof", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CiervosNightmareHoof = 
                   JsonObjectParser<AIAbility>.Parse("CiervosNightmareHoof", value, errorInfo),
                GetObject = () => CiervosNightmareHoof } },
           { "ClaudiaBlizzard", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ClaudiaBlizzard = 
                   JsonObjectParser<AIAbility>.Parse("ClaudiaBlizzard", value, errorInfo),
                GetObject = () => ClaudiaBlizzard } },
           { "ClaudiaIceSpear", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ClaudiaIceSpear = 
                   JsonObjectParser<AIAbility>.Parse("ClaudiaIceSpear", value, errorInfo),
                GetObject = () => ClaudiaIceSpear } },
           { "ClaudiaTundraSpikes", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ClaudiaTundraSpikes = 
                   JsonObjectParser<AIAbility>.Parse("ClaudiaTundraSpikes", value, errorInfo),
                GetObject = () => ClaudiaTundraSpikes } },
           { "CockatriceParalyze", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CockatriceParalyze = 
                   JsonObjectParser<AIAbility>.Parse("CockatriceParalyze", value, errorInfo),
                GetObject = () => CockatriceParalyze } },
           { "CockatricePeck", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CockatricePeck = 
                   JsonObjectParser<AIAbility>.Parse("CockatricePeck", value, errorInfo),
                GetObject = () => CockatricePeck } },
           { "CockatriceTailWhip", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CockatriceTailWhip = 
                   JsonObjectParser<AIAbility>.Parse("CockatriceTailWhip", value, errorInfo),
                GetObject = () => CockatriceTailWhip } },
           { "ColdAuraBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ColdAuraBurst = 
                   JsonObjectParser<AIAbility>.Parse("ColdAuraBurst", value, errorInfo),
                GetObject = () => ColdAuraBurst } },
           { "ColdSphereBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ColdSphereBurst = 
                   JsonObjectParser<AIAbility>.Parse("ColdSphereBurst", value, errorInfo),
                GetObject = () => ColdSphereBurst } },
           { "ColdSphereFreezeBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ColdSphereFreezeBurst = 
                   JsonObjectParser<AIAbility>.Parse("ColdSphereFreezeBurst", value, errorInfo),
                GetObject = () => ColdSphereFreezeBurst } },
           { "CultistArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CultistArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("CultistArrow1", value, errorInfo),
                GetObject = () => CultistArrow1 } },
           { "CultistArrow2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CultistArrow2 = 
                   JsonObjectParser<AIAbility>.Parse("CultistArrow2", value, errorInfo),
                GetObject = () => CultistArrow2 } },
           { "CultistOmegaArrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CultistOmegaArrow = 
                   JsonObjectParser<AIAbility>.Parse("CultistOmegaArrow", value, errorInfo),
                GetObject = () => CultistOmegaArrow } },
           { "CultistSword1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CultistSword1 = 
                   JsonObjectParser<AIAbility>.Parse("CultistSword1", value, errorInfo),
                GetObject = () => CultistSword1 } },
           { "CultistSword2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CultistSword2 = 
                   JsonObjectParser<AIAbility>.Parse("CultistSword2", value, errorInfo),
                GetObject = () => CultistSword2 } },
           { "CultistSwordStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CultistSwordStun = 
                   JsonObjectParser<AIAbility>.Parse("CultistSwordStun", value, errorInfo),
                GetObject = () => CultistSwordStun } },
           { "DeathRay", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DeathRay = 
                   JsonObjectParser<AIAbility>.Parse("DeathRay", value, errorInfo),
                GetObject = () => DeathRay } },
           { "DementiaPuckCurse", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DementiaPuckCurse = 
                   JsonObjectParser<AIAbility>.Parse("DementiaPuckCurse", value, errorInfo),
                GetObject = () => DementiaPuckCurse } },
           { "Dinobite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Dinobite = 
                   JsonObjectParser<AIAbility>.Parse("Dinobite", value, errorInfo),
                GetObject = () => Dinobite } },
           { "Dinobite2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Dinobite2 = 
                   JsonObjectParser<AIAbility>.Parse("Dinobite2", value, errorInfo),
                GetObject = () => Dinobite2 } },
           { "Dinoslash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Dinoslash = 
                   JsonObjectParser<AIAbility>.Parse("Dinoslash", value, errorInfo),
                GetObject = () => Dinoslash } },
           { "Dinoslash2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Dinoslash2 = 
                   JsonObjectParser<AIAbility>.Parse("Dinoslash2", value, errorInfo),
                GetObject = () => Dinoslash2 } },
           { "Dinowhap", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Dinowhap = 
                   JsonObjectParser<AIAbility>.Parse("Dinowhap", value, errorInfo),
                GetObject = () => Dinowhap } },
           { "DragonWormBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DragonWormBite = 
                   JsonObjectParser<AIAbility>.Parse("DragonWormBite", value, errorInfo),
                GetObject = () => DragonWormBite } },
           { "DragonWormEscape", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DragonWormEscape = 
                   JsonObjectParser<AIAbility>.Parse("DragonWormEscape", value, errorInfo),
                GetObject = () => DragonWormEscape } },
           { "DragonWormRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DragonWormRage = 
                   JsonObjectParser<AIAbility>.Parse("DragonWormRage", value, errorInfo),
                GetObject = () => DragonWormRage } },
           { "DragonWormSmack", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DragonWormSmack = 
                   JsonObjectParser<AIAbility>.Parse("DragonWormSmack", value, errorInfo),
                GetObject = () => DragonWormSmack } },
           { "DragonWormSpitElectricity", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DragonWormSpitElectricity = 
                   JsonObjectParser<AIAbility>.Parse("DragonWormSpitElectricity", value, errorInfo),
                GetObject = () => DragonWormSpitElectricity } },
           { "DragonWormSpitFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DragonWormSpitFire = 
                   JsonObjectParser<AIAbility>.Parse("DragonWormSpitFire", value, errorInfo),
                GetObject = () => DragonWormSpitFire } },
           { "DroachBiteA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachBiteA = 
                   JsonObjectParser<AIAbility>.Parse("DroachBiteA", value, errorInfo),
                GetObject = () => DroachBiteA } },
           { "DroachBiteB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachBiteB = 
                   JsonObjectParser<AIAbility>.Parse("DroachBiteB", value, errorInfo),
                GetObject = () => DroachBiteB } },
           { "DroachBreatheFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachBreatheFire = 
                   JsonObjectParser<AIAbility>.Parse("DroachBreatheFire", value, errorInfo),
                GetObject = () => DroachBreatheFire } },
           { "DroachFireball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachFireball = 
                   JsonObjectParser<AIAbility>.Parse("DroachFireball", value, errorInfo),
                GetObject = () => DroachFireball } },
           { "DroachFireballPerching", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachFireballPerching = 
                   JsonObjectParser<AIAbility>.Parse("DroachFireballPerching", value, errorInfo),
                GetObject = () => DroachFireballPerching } },
           { "DroachLightning", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachLightning = 
                   JsonObjectParser<AIAbility>.Parse("DroachLightning", value, errorInfo),
                GetObject = () => DroachLightning } },
           { "DroachLightningPerching", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachLightningPerching = 
                   JsonObjectParser<AIAbility>.Parse("DroachLightningPerching", value, errorInfo),
                GetObject = () => DroachLightningPerching } },
           { "DroachShockingKnockback", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachShockingKnockback = 
                   JsonObjectParser<AIAbility>.Parse("DroachShockingKnockback", value, errorInfo),
                GetObject = () => DroachShockingKnockback } },
           { "DruidHealingSanctuaryHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DruidHealingSanctuaryHeal = 
                   JsonObjectParser<AIAbility>.Parse("DruidHealingSanctuaryHeal", value, errorInfo),
                GetObject = () => DruidHealingSanctuaryHeal } },
           { "ElectricityAura1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElectricityAura1 = 
                   JsonObjectParser<AIAbility>.Parse("ElectricityAura1", value, errorInfo),
                GetObject = () => ElectricityAura1 } },
           { "ElectricityAuraBolt1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElectricityAuraBolt1 = 
                   JsonObjectParser<AIAbility>.Parse("ElectricityAuraBolt1", value, errorInfo),
                GetObject = () => ElectricityAuraBolt1 } },
           { "ElectricPigAoEStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElectricPigAoEStun = 
                   JsonObjectParser<AIAbility>.Parse("ElectricPigAoEStun", value, errorInfo),
                GetObject = () => ElectricPigAoEStun } },
           { "ElectricPigHitAndRun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElectricPigHitAndRun = 
                   JsonObjectParser<AIAbility>.Parse("ElectricPigHitAndRun", value, errorInfo),
                GetObject = () => ElectricPigHitAndRun } },
           { "ElectricPigStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElectricPigStun = 
                   JsonObjectParser<AIAbility>.Parse("ElectricPigStun", value, errorInfo),
                GetObject = () => ElectricPigStun } },
           { "ElementalBees", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElementalBees = 
                   JsonObjectParser<AIAbility>.Parse("ElementalBees", value, errorInfo),
                GetObject = () => ElementalBees } },
           { "ElementalBees2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElementalBees2 = 
                   JsonObjectParser<AIAbility>.Parse("ElementalBees2", value, errorInfo),
                GetObject = () => ElementalBees2 } },
           { "ElementalSlam", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElementalSlam = 
                   JsonObjectParser<AIAbility>.Parse("ElementalSlam", value, errorInfo),
                GetObject = () => ElementalSlam } },
           { "EnemyMinigolemExplode", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => EnemyMinigolemExplode = 
                   JsonObjectParser<AIAbility>.Parse("EnemyMinigolemExplode", value, errorInfo),
                GetObject = () => EnemyMinigolemExplode } },
           { "EnemyMinigolemHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => EnemyMinigolemHeal = 
                   JsonObjectParser<AIAbility>.Parse("EnemyMinigolemHeal", value, errorInfo),
                GetObject = () => EnemyMinigolemHeal } },
           { "EnemyMinigolemPunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => EnemyMinigolemPunch = 
                   JsonObjectParser<AIAbility>.Parse("EnemyMinigolemPunch", value, errorInfo),
                GetObject = () => EnemyMinigolemPunch } },
           { "FaceOfDeathKill", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FaceOfDeathKill = 
                   JsonObjectParser<AIAbility>.Parse("FaceOfDeathKill", value, errorInfo),
                GetObject = () => FaceOfDeathKill } },
           { "FaeLightningSmite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FaeLightningSmite = 
                   JsonObjectParser<AIAbility>.Parse("FaeLightningSmite", value, errorInfo),
                GetObject = () => FaeLightningSmite } },
           { "FaeLightningSmiteHidden", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FaeLightningSmiteHidden = 
                   JsonObjectParser<AIAbility>.Parse("FaeLightningSmiteHidden", value, errorInfo),
                GetObject = () => FaeLightningSmiteHidden } },
           { "FaeSwordA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FaeSwordA = 
                   JsonObjectParser<AIAbility>.Parse("FaeSwordA", value, errorInfo),
                GetObject = () => FaeSwordA } },
           { "FaeSwordB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FaeSwordB = 
                   JsonObjectParser<AIAbility>.Parse("FaeSwordB", value, errorInfo),
                GetObject = () => FaeSwordB } },
           { "FaeSwordKill", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FaeSwordKill = 
                   JsonObjectParser<AIAbility>.Parse("FaeSwordKill", value, errorInfo),
                GetObject = () => FaeSwordKill } },
           { "FireRatBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireRatBite = 
                   JsonObjectParser<AIAbility>.Parse("FireRatBite", value, errorInfo),
                GetObject = () => FireRatBite } },
           { "FireRatBite_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireRatBite_Pet = 
                   JsonObjectParser<AIAbility>.Parse("FireRatBite_Pet", value, errorInfo),
                GetObject = () => FireRatBite_Pet } },
           { "FireRatClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireRatClaw = 
                   JsonObjectParser<AIAbility>.Parse("FireRatClaw", value, errorInfo),
                GetObject = () => FireRatClaw } },
           { "FireRatClaw_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireRatClaw_Pet = 
                   JsonObjectParser<AIAbility>.Parse("FireRatClaw_Pet", value, errorInfo),
                GetObject = () => FireRatClaw_Pet } },
           { "FireSnakeExplosion1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireSnakeExplosion1 = 
                   JsonObjectParser<AIAbility>.Parse("FireSnakeExplosion1", value, errorInfo),
                GetObject = () => FireSnakeExplosion1 } },
           { "FireTrapAttack1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireTrapAttack1 = 
                   JsonObjectParser<AIAbility>.Parse("FireTrapAttack1", value, errorInfo),
                GetObject = () => FireTrapAttack1 } },
           { "FireWallAttack1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireWallAttack1 = 
                   JsonObjectParser<AIAbility>.Parse("FireWallAttack1", value, errorInfo),
                GetObject = () => FireWallAttack1 } },
           { "FireWallDotAttack1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireWallDotAttack1 = 
                   JsonObjectParser<AIAbility>.Parse("FireWallDotAttack1", value, errorInfo),
                GetObject = () => FireWallDotAttack1 } },
           { "FlapSkullBigBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FlapSkullBigBite = 
                   JsonObjectParser<AIAbility>.Parse("FlapSkullBigBite", value, errorInfo),
                GetObject = () => FlapSkullBigBite } },
           { "FlapSkullBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FlapSkullBite = 
                   JsonObjectParser<AIAbility>.Parse("FlapSkullBite", value, errorInfo),
                GetObject = () => FlapSkullBite } },
           { "GargoyleBossStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GargoyleBossStun = 
                   JsonObjectParser<AIAbility>.Parse("GargoyleBossStun", value, errorInfo),
                GetObject = () => GargoyleBossStun } },
           { "GargoyleSlamA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GargoyleSlamA = 
                   JsonObjectParser<AIAbility>.Parse("GargoyleSlamA", value, errorInfo),
                GetObject = () => GargoyleSlamA } },
           { "GargoyleSlamB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GargoyleSlamB = 
                   JsonObjectParser<AIAbility>.Parse("GargoyleSlamB", value, errorInfo),
                GetObject = () => GargoyleSlamB } },
           { "GargoyleStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GargoyleStun = 
                   JsonObjectParser<AIAbility>.Parse("GargoyleStun", value, errorInfo),
                GetObject = () => GargoyleStun } },
           { "GazlukPriest1Special", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GazlukPriest1Special = 
                   JsonObjectParser<AIAbility>.Parse("GazlukPriest1Special", value, errorInfo),
                GetObject = () => GazlukPriest1Special } },
           { "GazlukPriest2Special", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GazlukPriest2Special = 
                   JsonObjectParser<AIAbility>.Parse("GazlukPriest2Special", value, errorInfo),
                GetObject = () => GazlukPriest2Special } },
           { "GazlukPriest3Special", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GazlukPriest3Special = 
                   JsonObjectParser<AIAbility>.Parse("GazlukPriest3Special", value, errorInfo),
                GetObject = () => GazlukPriest3Special } },
           { "GhostlyBolt", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhostlyBolt = 
                   JsonObjectParser<AIAbility>.Parse("GhostlyBolt", value, errorInfo),
                GetObject = () => GhostlyBolt } },
           { "GhostlyBossBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhostlyBossBurst = 
                   JsonObjectParser<AIAbility>.Parse("GhostlyBossBurst", value, errorInfo),
                GetObject = () => GhostlyBossBurst } },
           { "GhostlyBossPunchA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhostlyBossPunchA = 
                   JsonObjectParser<AIAbility>.Parse("GhostlyBossPunchA", value, errorInfo),
                GetObject = () => GhostlyBossPunchA } },
           { "GhostlyBossPunchB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhostlyBossPunchB = 
                   JsonObjectParser<AIAbility>.Parse("GhostlyBossPunchB", value, errorInfo),
                GetObject = () => GhostlyBossPunchB } },
           { "GhostlyBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhostlyBurst = 
                   JsonObjectParser<AIAbility>.Parse("GhostlyBurst", value, errorInfo),
                GetObject = () => GhostlyBurst } },
           { "GhostlyPunchA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhostlyPunchA = 
                   JsonObjectParser<AIAbility>.Parse("GhostlyPunchA", value, errorInfo),
                GetObject = () => GhostlyPunchA } },
           { "GhostlyPunchB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhostlyPunchB = 
                   JsonObjectParser<AIAbility>.Parse("GhostlyPunchB", value, errorInfo),
                GetObject = () => GhostlyPunchB } },
           { "GhoulClawA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhoulClawA = 
                   JsonObjectParser<AIAbility>.Parse("GhoulClawA", value, errorInfo),
                GetObject = () => GhoulClawA } },
           { "GhoulClawB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhoulClawB = 
                   JsonObjectParser<AIAbility>.Parse("GhoulClawB", value, errorInfo),
                GetObject = () => GhoulClawB } },
           { "GhoulHammerA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhoulHammerA = 
                   JsonObjectParser<AIAbility>.Parse("GhoulHammerA", value, errorInfo),
                GetObject = () => GhoulHammerA } },
           { "GhoulHammerB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhoulHammerB = 
                   JsonObjectParser<AIAbility>.Parse("GhoulHammerB", value, errorInfo),
                GetObject = () => GhoulHammerB } },
           { "GhoulSelfBuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhoulSelfBuff = 
                   JsonObjectParser<AIAbility>.Parse("GhoulSelfBuff", value, errorInfo),
                GetObject = () => GhoulSelfBuff } },
           { "GiantBatBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantBatBite = 
                   JsonObjectParser<AIAbility>.Parse("GiantBatBite", value, errorInfo),
                GetObject = () => GiantBatBite } },
           { "GiantBatSlashA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantBatSlashA = 
                   JsonObjectParser<AIAbility>.Parse("GiantBatSlashA", value, errorInfo),
                GetObject = () => GiantBatSlashA } },
           { "GiantBatSlashB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantBatSlashB = 
                   JsonObjectParser<AIAbility>.Parse("GiantBatSlashB", value, errorInfo),
                GetObject = () => GiantBatSlashB } },
           { "GiantBeetleBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantBeetleBite = 
                   JsonObjectParser<AIAbility>.Parse("GiantBeetleBite", value, errorInfo),
                GetObject = () => GiantBeetleBite } },
           { "GiantBeetleBoulderSpit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantBeetleBoulderSpit = 
                   JsonObjectParser<AIAbility>.Parse("GiantBeetleBoulderSpit", value, errorInfo),
                GetObject = () => GiantBeetleBoulderSpit } },
           { "GiantBeetleInject", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantBeetleInject = 
                   JsonObjectParser<AIAbility>.Parse("GiantBeetleInject", value, errorInfo),
                GetObject = () => GiantBeetleInject } },
           { "GiantScorpionClawA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantScorpionClawA = 
                   JsonObjectParser<AIAbility>.Parse("GiantScorpionClawA", value, errorInfo),
                GetObject = () => GiantScorpionClawA } },
           { "GiantScorpionClawB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantScorpionClawB = 
                   JsonObjectParser<AIAbility>.Parse("GiantScorpionClawB", value, errorInfo),
                GetObject = () => GiantScorpionClawB } },
           { "GiantScorpionSting", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantScorpionSting = 
                   JsonObjectParser<AIAbility>.Parse("GiantScorpionSting", value, errorInfo),
                GetObject = () => GiantScorpionSting } },
           { "GnasherBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GnasherBite = 
                   JsonObjectParser<AIAbility>.Parse("GnasherBite", value, errorInfo),
                GetObject = () => GnasherBite } },
           { "GnasherRend", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GnasherRend = 
                   JsonObjectParser<AIAbility>.Parse("GnasherRend", value, errorInfo),
                GetObject = () => GnasherRend } },
           { "GoblinArmorBuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinArmorBuff = 
                   JsonObjectParser<AIAbility>.Parse("GoblinArmorBuff", value, errorInfo),
                GetObject = () => GoblinArmorBuff } },
           { "GoblinArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinArrow1", value, errorInfo),
                GetObject = () => GoblinArrow1 } },
           { "GoblinArrow2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinArrow2 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinArrow2", value, errorInfo),
                GetObject = () => GoblinArrow2 } },
           { "GoblinBossLightning", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinBossLightning = 
                   JsonObjectParser<AIAbility>.Parse("GoblinBossLightning", value, errorInfo),
                GetObject = () => GoblinBossLightning } },
           { "GoblinHateZapBall", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinHateZapBall = 
                   JsonObjectParser<AIAbility>.Parse("GoblinHateZapBall", value, errorInfo),
                GetObject = () => GoblinHateZapBall } },
           { "GoblinHateZapBall2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinHateZapBall2 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinHateZapBall2", value, errorInfo),
                GetObject = () => GoblinHateZapBall2 } },
           { "GoblinHeal1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinHeal1 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinHeal1", value, errorInfo),
                GetObject = () => GoblinHeal1 } },
           { "GoblinHeal2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinHeal2 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinHeal2", value, errorInfo),
                GetObject = () => GoblinHeal2 } },
           { "GoblinPunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinPunch = 
                   JsonObjectParser<AIAbility>.Parse("GoblinPunch", value, errorInfo),
                GetObject = () => GoblinPunch } },
           { "GoblinRageArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinRageArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinRageArrow1", value, errorInfo),
                GetObject = () => GoblinRageArrow1 } },
           { "GoblinRageArrow2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinRageArrow2 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinRageArrow2", value, errorInfo),
                GetObject = () => GoblinRageArrow2 } },
           { "GoblinRageSpear1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinRageSpear1 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinRageSpear1", value, errorInfo),
                GetObject = () => GoblinRageSpear1 } },
           { "GoblinRageSpear2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinRageSpear2 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinRageSpear2", value, errorInfo),
                GetObject = () => GoblinRageSpear2 } },
           { "GoblinSpear1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinSpear1 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinSpear1", value, errorInfo),
                GetObject = () => GoblinSpear1 } },
           { "GoblinSpear2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinSpear2 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinSpear2", value, errorInfo),
                GetObject = () => GoblinSpear2 } },
           { "GoblinSpreadZapBall", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinSpreadZapBall = 
                   JsonObjectParser<AIAbility>.Parse("GoblinSpreadZapBall", value, errorInfo),
                GetObject = () => GoblinSpreadZapBall } },
           { "GoblinZapBall", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinZapBall = 
                   JsonObjectParser<AIAbility>.Parse("GoblinZapBall", value, errorInfo),
                GetObject = () => GoblinZapBall } },
           { "GrimalkinBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinBite = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinBite", value, errorInfo),
                GetObject = () => GrimalkinBite } },
           { "GrimalkinClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinClaw = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinClaw", value, errorInfo),
                GetObject = () => GrimalkinClaw } },
           { "GrimalkinFlee_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinFlee_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinFlee_Pet1", value, errorInfo),
                GetObject = () => GrimalkinFlee_Pet1 } },
           { "GrimalkinFlee_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinFlee_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinFlee_Pet2", value, errorInfo),
                GetObject = () => GrimalkinFlee_Pet2 } },
           { "GrimalkinFlee_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinFlee_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinFlee_Pet3", value, errorInfo),
                GetObject = () => GrimalkinFlee_Pet3 } },
           { "GrimalkinPuncture", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinPuncture = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinPuncture", value, errorInfo),
                GetObject = () => GrimalkinPuncture } },
           { "GrimalkinPuncture_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinPuncture_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinPuncture_Pet1", value, errorInfo),
                GetObject = () => GrimalkinPuncture_Pet1 } },
           { "GrimalkinPuncture_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinPuncture_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinPuncture_Pet2", value, errorInfo),
                GetObject = () => GrimalkinPuncture_Pet2 } },
           { "GrimalkinPuncture_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinPuncture_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinPuncture_Pet3", value, errorInfo),
                GetObject = () => GrimalkinPuncture_Pet3 } },
           { "GrimalkinPuncture_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinPuncture_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinPuncture_Pet4", value, errorInfo),
                GetObject = () => GrimalkinPuncture_Pet4 } },
           { "GrimalkinPuncture_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinPuncture_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinPuncture_Pet5", value, errorInfo),
                GetObject = () => GrimalkinPuncture_Pet5 } },
           { "GrimalkinPuncture_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinPuncture_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinPuncture_Pet6", value, errorInfo),
                GetObject = () => GrimalkinPuncture_Pet6 } },
           { "HagAgingScream", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HagAgingScream = 
                   JsonObjectParser<AIAbility>.Parse("HagAgingScream", value, errorInfo),
                GetObject = () => HagAgingScream } },
           { "HagAgingTouch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HagAgingTouch = 
                   JsonObjectParser<AIAbility>.Parse("HagAgingTouch", value, errorInfo),
                GetObject = () => HagAgingTouch } },
           { "HealingAura1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HealingAura1 = 
                   JsonObjectParser<AIAbility>.Parse("HealingAura1", value, errorInfo),
                GetObject = () => HealingAura1 } },
           { "HealingAura2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HealingAura2 = 
                   JsonObjectParser<AIAbility>.Parse("HealingAura2", value, errorInfo),
                GetObject = () => HealingAura2 } },
           { "HealingAura3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HealingAura3 = 
                   JsonObjectParser<AIAbility>.Parse("HealingAura3", value, errorInfo),
                GetObject = () => HealingAura3 } },
           { "HealingAura4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HealingAura4 = 
                   JsonObjectParser<AIAbility>.Parse("HealingAura4", value, errorInfo),
                GetObject = () => HealingAura4 } },
           { "HippoBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HippoBite = 
                   JsonObjectParser<AIAbility>.Parse("HippoBite", value, errorInfo),
                GetObject = () => HippoBite } },
           { "HippoBiteAndHeal1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HippoBiteAndHeal1 = 
                   JsonObjectParser<AIAbility>.Parse("HippoBiteAndHeal1", value, errorInfo),
                GetObject = () => HippoBiteAndHeal1 } },
           { "HippogriffBossSlashes", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HippogriffBossSlashes = 
                   JsonObjectParser<AIAbility>.Parse("HippogriffBossSlashes", value, errorInfo),
                GetObject = () => HippogriffBossSlashes } },
           { "HippogriffPeck", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HippogriffPeck = 
                   JsonObjectParser<AIAbility>.Parse("HippogriffPeck", value, errorInfo),
                GetObject = () => HippogriffPeck } },
           { "HippogriffSlashes", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HippogriffSlashes = 
                   JsonObjectParser<AIAbility>.Parse("HippogriffSlashes", value, errorInfo),
                GetObject = () => HippogriffSlashes } },
           { "HookAcid", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HookAcid = 
                   JsonObjectParser<AIAbility>.Parse("HookAcid", value, errorInfo),
                GetObject = () => HookAcid } },
           { "HookClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HookClaw = 
                   JsonObjectParser<AIAbility>.Parse("HookClaw", value, errorInfo),
                GetObject = () => HookClaw } },
           { "HookRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HookRage = 
                   JsonObjectParser<AIAbility>.Parse("HookRage", value, errorInfo),
                GetObject = () => HookRage } },
           { "IceCockFreeze", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IceCockFreeze = 
                   JsonObjectParser<AIAbility>.Parse("IceCockFreeze", value, errorInfo),
                GetObject = () => IceCockFreeze } },
           { "IceCockPeck", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IceCockPeck = 
                   JsonObjectParser<AIAbility>.Parse("IceCockPeck", value, errorInfo),
                GetObject = () => IceCockPeck } },
           { "IceSlimeBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IceSlimeBite = 
                   JsonObjectParser<AIAbility>.Parse("IceSlimeBite", value, errorInfo),
                GetObject = () => IceSlimeBite } },
           { "IceSlimeBiteB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IceSlimeBiteB = 
                   JsonObjectParser<AIAbility>.Parse("IceSlimeBiteB", value, errorInfo),
                GetObject = () => IceSlimeBiteB } },
           { "IceSlimeKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IceSlimeKick = 
                   JsonObjectParser<AIAbility>.Parse("IceSlimeKick", value, errorInfo),
                GetObject = () => IceSlimeKick } },
           { "IceSlimeKickB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IceSlimeKickB = 
                   JsonObjectParser<AIAbility>.Parse("IceSlimeKickB", value, errorInfo),
                GetObject = () => IceSlimeKickB } },
           { "IcyCocoon", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IcyCocoon = 
                   JsonObjectParser<AIAbility>.Parse("IcyCocoon", value, errorInfo),
                GetObject = () => IcyCocoon } },
           { "IcyCocoon2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IcyCocoon2 = 
                   JsonObjectParser<AIAbility>.Parse("IcyCocoon2", value, errorInfo),
                GetObject = () => IcyCocoon2 } },
           { "IcySlam", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IcySlam = 
                   JsonObjectParser<AIAbility>.Parse("IcySlam", value, errorInfo),
                GetObject = () => IcySlam } },
           { "InjectorBugBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => InjectorBugBite = 
                   JsonObjectParser<AIAbility>.Parse("InjectorBugBite", value, errorInfo),
                GetObject = () => InjectorBugBite } },
           { "InjectorBugInject", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => InjectorBugInject = 
                   JsonObjectParser<AIAbility>.Parse("InjectorBugInject", value, errorInfo),
                GetObject = () => InjectorBugInject } },
           { "InjectorBugInject2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => InjectorBugInject2 = 
                   JsonObjectParser<AIAbility>.Parse("InjectorBugInject2", value, errorInfo),
                GetObject = () => InjectorBugInject2 } },
           { "KhyrulekCurseBall", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => KhyrulekCurseBall = 
                   JsonObjectParser<AIAbility>.Parse("KhyrulekCurseBall", value, errorInfo),
                GetObject = () => KhyrulekCurseBall } },
           { "KrakenBabyBeak", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => KrakenBabyBeak = 
                   JsonObjectParser<AIAbility>.Parse("KrakenBabyBeak", value, errorInfo),
                GetObject = () => KrakenBabyBeak } },
           { "KrakenBabyRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => KrakenBabyRage = 
                   JsonObjectParser<AIAbility>.Parse("KrakenBabyRage", value, errorInfo),
                GetObject = () => KrakenBabyRage } },
           { "KrakenBabySlam", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => KrakenBabySlam = 
                   JsonObjectParser<AIAbility>.Parse("KrakenBabySlam", value, errorInfo),
                GetObject = () => KrakenBabySlam } },
           { "KrakenBeak", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => KrakenBeak = 
                   JsonObjectParser<AIAbility>.Parse("KrakenBeak", value, errorInfo),
                GetObject = () => KrakenBeak } },
           { "KrakenRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => KrakenRage = 
                   JsonObjectParser<AIAbility>.Parse("KrakenRage", value, errorInfo),
                GetObject = () => KrakenRage } },
           { "KrakenSlam", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => KrakenSlam = 
                   JsonObjectParser<AIAbility>.Parse("KrakenSlam", value, errorInfo),
                GetObject = () => KrakenSlam } },
           { "LamiaMindControl", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => LamiaMindControl = 
                   JsonObjectParser<AIAbility>.Parse("LamiaMindControl", value, errorInfo),
                GetObject = () => LamiaMindControl } },
           { "LamiaRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => LamiaRage = 
                   JsonObjectParser<AIAbility>.Parse("LamiaRage", value, errorInfo),
                GetObject = () => LamiaRage } },
           { "ManticoreBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ManticoreBite = 
                   JsonObjectParser<AIAbility>.Parse("ManticoreBite", value, errorInfo),
                GetObject = () => ManticoreBite } },
           { "ManticoreClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ManticoreClaw = 
                   JsonObjectParser<AIAbility>.Parse("ManticoreClaw", value, errorInfo),
                GetObject = () => ManticoreClaw } },
           { "ManticoreSting1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ManticoreSting1 = 
                   JsonObjectParser<AIAbility>.Parse("ManticoreSting1", value, errorInfo),
                GetObject = () => ManticoreSting1 } },
           { "Manticoresting2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Manticoresting2 = 
                   JsonObjectParser<AIAbility>.Parse("Manticoresting2", value, errorInfo),
                GetObject = () => Manticoresting2 } },
           { "MantisAcidBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MantisAcidBurst = 
                   JsonObjectParser<AIAbility>.Parse("MantisAcidBurst", value, errorInfo),
                GetObject = () => MantisAcidBurst } },
           { "MantisBlast", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MantisBlast = 
                   JsonObjectParser<AIAbility>.Parse("MantisBlast", value, errorInfo),
                GetObject = () => MantisBlast } },
           { "MantisClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MantisClaw = 
                   JsonObjectParser<AIAbility>.Parse("MantisClaw", value, errorInfo),
                GetObject = () => MantisClaw } },
           { "MantisRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MantisRage = 
                   JsonObjectParser<AIAbility>.Parse("MantisRage", value, errorInfo),
                GetObject = () => MantisRage } },
           { "MantisSwipe", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MantisSwipe = 
                   JsonObjectParser<AIAbility>.Parse("MantisSwipe", value, errorInfo),
                GetObject = () => MantisSwipe } },
           { "MaronesaInfect", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MaronesaInfect = 
                   JsonObjectParser<AIAbility>.Parse("MaronesaInfect", value, errorInfo),
                GetObject = () => MaronesaInfect } },
           { "MaronesaStomp", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MaronesaStomp = 
                   JsonObjectParser<AIAbility>.Parse("MaronesaStomp", value, errorInfo),
                GetObject = () => MaronesaStomp } },
           { "MinigolemAoEHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEHeal = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEHeal", value, errorInfo),
                GetObject = () => MinigolemAoEHeal } },
           { "MinigolemAoEHeal2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEHeal2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEHeal2", value, errorInfo),
                GetObject = () => MinigolemAoEHeal2 } },
           { "MinigolemAoEHeal3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEHeal3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEHeal3", value, errorInfo),
                GetObject = () => MinigolemAoEHeal3 } },
           { "MinigolemAoEHeal4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEHeal4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEHeal4", value, errorInfo),
                GetObject = () => MinigolemAoEHeal4 } },
           { "MinigolemAoEHeal5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEHeal5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEHeal5", value, errorInfo),
                GetObject = () => MinigolemAoEHeal5 } },
           { "MinigolemAoEPower", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEPower = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEPower", value, errorInfo),
                GetObject = () => MinigolemAoEPower } },
           { "MinigolemAoEPower2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEPower2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEPower2", value, errorInfo),
                GetObject = () => MinigolemAoEPower2 } },
           { "MinigolemAoEPower3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEPower3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEPower3", value, errorInfo),
                GetObject = () => MinigolemAoEPower3 } },
           { "MinigolemAoEPower4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEPower4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEPower4", value, errorInfo),
                GetObject = () => MinigolemAoEPower4 } },
           { "MinigolemAoEPower5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEPower5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEPower5", value, errorInfo),
                GetObject = () => MinigolemAoEPower5 } },
           { "MinigolemBombToss", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemBombToss = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemBombToss", value, errorInfo),
                GetObject = () => MinigolemBombToss } },
           { "MinigolemBombToss2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemBombToss2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemBombToss2", value, errorInfo),
                GetObject = () => MinigolemBombToss2 } },
           { "MinigolemBombToss3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemBombToss3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemBombToss3", value, errorInfo),
                GetObject = () => MinigolemBombToss3 } },
           { "MinigolemBombToss4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemBombToss4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemBombToss4", value, errorInfo),
                GetObject = () => MinigolemBombToss4 } },
           { "MinigolemBombToss5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemBombToss5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemBombToss5", value, errorInfo),
                GetObject = () => MinigolemBombToss5 } },
           { "MinigolemDoomAdmixture", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemDoomAdmixture = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemDoomAdmixture", value, errorInfo),
                GetObject = () => MinigolemDoomAdmixture } },
           { "MinigolemDoomAdmixture2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemDoomAdmixture2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemDoomAdmixture2", value, errorInfo),
                GetObject = () => MinigolemDoomAdmixture2 } },
           { "MinigolemDoomAdmixture3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemDoomAdmixture3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemDoomAdmixture3", value, errorInfo),
                GetObject = () => MinigolemDoomAdmixture3 } },
           { "MinigolemDoomAdmixture4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemDoomAdmixture4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemDoomAdmixture4", value, errorInfo),
                GetObject = () => MinigolemDoomAdmixture4 } },
           { "MinigolemDoomAdmixture5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemDoomAdmixture5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemDoomAdmixture5", value, errorInfo),
                GetObject = () => MinigolemDoomAdmixture5 } },
           { "MinigolemFireBalm1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemFireBalm1 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemFireBalm1", value, errorInfo),
                GetObject = () => MinigolemFireBalm1 } },
           { "MinigolemFireBalm2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemFireBalm2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemFireBalm2", value, errorInfo),
                GetObject = () => MinigolemFireBalm2 } },
           { "MinigolemFireBalm3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemFireBalm3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemFireBalm3", value, errorInfo),
                GetObject = () => MinigolemFireBalm3 } },
           { "MinigolemFireBalm4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemFireBalm4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemFireBalm4", value, errorInfo),
                GetObject = () => MinigolemFireBalm4 } },
           { "MinigolemFireBalm5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemFireBalm5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemFireBalm5", value, errorInfo),
                GetObject = () => MinigolemFireBalm5 } },
           { "MinigolemHasteConcoction1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHasteConcoction1 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHasteConcoction1", value, errorInfo),
                GetObject = () => MinigolemHasteConcoction1 } },
           { "MinigolemHasteConcoction2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHasteConcoction2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHasteConcoction2", value, errorInfo),
                GetObject = () => MinigolemHasteConcoction2 } },
           { "MinigolemHasteConcoction3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHasteConcoction3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHasteConcoction3", value, errorInfo),
                GetObject = () => MinigolemHasteConcoction3 } },
           { "MinigolemHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHeal = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHeal", value, errorInfo),
                GetObject = () => MinigolemHeal } },
           { "MinigolemHeal2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHeal2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHeal2", value, errorInfo),
                GetObject = () => MinigolemHeal2 } },
           { "MinigolemHeal3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHeal3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHeal3", value, errorInfo),
                GetObject = () => MinigolemHeal3 } },
           { "MinigolemHeal4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHeal4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHeal4", value, errorInfo),
                GetObject = () => MinigolemHeal4 } },
           { "MinigolemHeal5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHeal5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHeal5", value, errorInfo),
                GetObject = () => MinigolemHeal5 } },
           { "MinigolemPunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemPunch = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemPunch", value, errorInfo),
                GetObject = () => MinigolemPunch } },
           { "MinigolemPunch2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemPunch2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemPunch2", value, errorInfo),
                GetObject = () => MinigolemPunch2 } },
           { "MinigolemPunch3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemPunch3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemPunch3", value, errorInfo),
                GetObject = () => MinigolemPunch3 } },
           { "MinigolemPunch4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemPunch4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemPunch4", value, errorInfo),
                GetObject = () => MinigolemPunch4 } },
           { "MinigolemPunch5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemPunch5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemPunch5", value, errorInfo),
                GetObject = () => MinigolemPunch5 } },
           { "MinigolemRageAcidToss1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAcidToss1 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAcidToss1", value, errorInfo),
                GetObject = () => MinigolemRageAcidToss1 } },
           { "MinigolemRageAcidToss2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAcidToss2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAcidToss2", value, errorInfo),
                GetObject = () => MinigolemRageAcidToss2 } },
           { "MinigolemRageAcidToss3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAcidToss3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAcidToss3", value, errorInfo),
                GetObject = () => MinigolemRageAcidToss3 } },
           { "MinigolemRageAcidToss4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAcidToss4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAcidToss4", value, errorInfo),
                GetObject = () => MinigolemRageAcidToss4 } },
           { "MinigolemRageAcidToss5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAcidToss5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAcidToss5", value, errorInfo),
                GetObject = () => MinigolemRageAcidToss5 } },
           { "MinigolemRageAoEHeal1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAoEHeal1 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAoEHeal1", value, errorInfo),
                GetObject = () => MinigolemRageAoEHeal1 } },
           { "MinigolemRageAoEHeal2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAoEHeal2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAoEHeal2", value, errorInfo),
                GetObject = () => MinigolemRageAoEHeal2 } },
           { "MinigolemRageAoEHeal3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAoEHeal3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAoEHeal3", value, errorInfo),
                GetObject = () => MinigolemRageAoEHeal3 } },
           { "MinigolemRageAoEHeal4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAoEHeal4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAoEHeal4", value, errorInfo),
                GetObject = () => MinigolemRageAoEHeal4 } },
           { "MinigolemRageAoEHeal5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAoEHeal5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAoEHeal5", value, errorInfo),
                GetObject = () => MinigolemRageAoEHeal5 } },
           { "MinigolemSelfDestruct", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfDestruct = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfDestruct", value, errorInfo),
                GetObject = () => MinigolemSelfDestruct } },
           { "MinigolemSelfDestruct2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfDestruct2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfDestruct2", value, errorInfo),
                GetObject = () => MinigolemSelfDestruct2 } },
           { "MinigolemSelfDestruct3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfDestruct3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfDestruct3", value, errorInfo),
                GetObject = () => MinigolemSelfDestruct3 } },
           { "MinigolemSelfDestruct4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfDestruct4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfDestruct4", value, errorInfo),
                GetObject = () => MinigolemSelfDestruct4 } },
           { "MinigolemSelfDestruct5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfDestruct5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfDestruct5", value, errorInfo),
                GetObject = () => MinigolemSelfDestruct5 } },
           { "MinigolemSelfSacrifice", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfSacrifice = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfSacrifice", value, errorInfo),
                GetObject = () => MinigolemSelfSacrifice } },
           { "MinigolemSelfSacrifice2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfSacrifice2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfSacrifice2", value, errorInfo),
                GetObject = () => MinigolemSelfSacrifice2 } },
           { "MinigolemSelfSacrifice3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfSacrifice3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfSacrifice3", value, errorInfo),
                GetObject = () => MinigolemSelfSacrifice3 } },
           { "MinigolemSelfSacrifice4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfSacrifice4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfSacrifice4", value, errorInfo),
                GetObject = () => MinigolemSelfSacrifice4 } },
           { "MinigolemSelfSacrifice5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfSacrifice5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfSacrifice5", value, errorInfo),
                GetObject = () => MinigolemSelfSacrifice5 } },
           { "MinotaurBossRageClub", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinotaurBossRageClub = 
                   JsonObjectParser<AIAbility>.Parse("MinotaurBossRageClub", value, errorInfo),
                GetObject = () => MinotaurBossRageClub } },
           { "MinotaurBoulder", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinotaurBoulder = 
                   JsonObjectParser<AIAbility>.Parse("MinotaurBoulder", value, errorInfo),
                GetObject = () => MinotaurBoulder } },
           { "MinotaurClub", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinotaurClub = 
                   JsonObjectParser<AIAbility>.Parse("MinotaurClub", value, errorInfo),
                GetObject = () => MinotaurClub } },
           { "MinotaurRageClub", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinotaurRageClub = 
                   JsonObjectParser<AIAbility>.Parse("MinotaurRageClub", value, errorInfo),
                GetObject = () => MinotaurRageClub } },
           { "MonsterWerewolfHowl", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MonsterWerewolfHowl = 
                   JsonObjectParser<AIAbility>.Parse("MonsterWerewolfHowl", value, errorInfo),
                GetObject = () => MonsterWerewolfHowl } },
           { "MonsterWerewolfPackAttack", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MonsterWerewolfPackAttack = 
                   JsonObjectParser<AIAbility>.Parse("MonsterWerewolfPackAttack", value, errorInfo),
                GetObject = () => MonsterWerewolfPackAttack } },
           { "MonsterWerewolfPouncingRake", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MonsterWerewolfPouncingRake = 
                   JsonObjectParser<AIAbility>.Parse("MonsterWerewolfPouncingRake", value, errorInfo),
                GetObject = () => MonsterWerewolfPouncingRake } },
           { "MummySlamA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MummySlamA = 
                   JsonObjectParser<AIAbility>.Parse("MummySlamA", value, errorInfo),
                GetObject = () => MummySlamA } },
           { "MummySlamB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MummySlamB = 
                   JsonObjectParser<AIAbility>.Parse("MummySlamB", value, errorInfo),
                GetObject = () => MummySlamB } },
           { "MummySlamCombo", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MummySlamCombo = 
                   JsonObjectParser<AIAbility>.Parse("MummySlamCombo", value, errorInfo),
                GetObject = () => MummySlamCombo } },
           { "MummyWrapA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MummyWrapA = 
                   JsonObjectParser<AIAbility>.Parse("MummyWrapA", value, errorInfo),
                GetObject = () => MummyWrapA } },
           { "MummyWrapB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MummyWrapB = 
                   JsonObjectParser<AIAbility>.Parse("MummyWrapB", value, errorInfo),
                GetObject = () => MummyWrapB } },
           { "MummyWrapRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MummyWrapRage = 
                   JsonObjectParser<AIAbility>.Parse("MummyWrapRage", value, errorInfo),
                GetObject = () => MummyWrapRage } },
           { "MushroomMonster_Bite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_Bite = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_Bite", value, errorInfo),
                GetObject = () => MushroomMonster_Bite } },
           { "MushroomMonster_SpawnSpit1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_SpawnSpit1 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_SpawnSpit1", value, errorInfo),
                GetObject = () => MushroomMonster_SpawnSpit1 } },
           { "MushroomMonster_SpawnSpit2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_SpawnSpit2 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_SpawnSpit2", value, errorInfo),
                GetObject = () => MushroomMonster_SpawnSpit2 } },
           { "MushroomMonster_SpawnSuperSpit1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_SpawnSuperSpit1 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_SpawnSuperSpit1", value, errorInfo),
                GetObject = () => MushroomMonster_SpawnSuperSpit1 } },
           { "MushroomMonster_SpawnSuperSpit2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_SpawnSuperSpit2 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_SpawnSuperSpit2", value, errorInfo),
                GetObject = () => MushroomMonster_SpawnSuperSpit2 } },
           { "MushroomMonster_Spit1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_Spit1 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_Spit1", value, errorInfo),
                GetObject = () => MushroomMonster_Spit1 } },
           { "MushroomMonster_Spit2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_Spit2 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_Spit2", value, errorInfo),
                GetObject = () => MushroomMonster_Spit2 } },
           { "MushroomMonster_SummonMushroomSpawn1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_SummonMushroomSpawn1 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_SummonMushroomSpawn1", value, errorInfo),
                GetObject = () => MushroomMonster_SummonMushroomSpawn1 } },
           { "MushroomMonster_SummonMushroomSpawn2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_SummonMushroomSpawn2 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_SummonMushroomSpawn2", value, errorInfo),
                GetObject = () => MushroomMonster_SummonMushroomSpawn2 } },
           { "MushroomMonster_SummonMushroomSpawn3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_SummonMushroomSpawn3 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_SummonMushroomSpawn3", value, errorInfo),
                GetObject = () => MushroomMonster_SummonMushroomSpawn3 } },
           { "Myconian_Bash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Myconian_Bash = 
                   JsonObjectParser<AIAbility>.Parse("Myconian_Bash", value, errorInfo),
                GetObject = () => Myconian_Bash } },
           { "Myconian_BossBash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Myconian_BossBash = 
                   JsonObjectParser<AIAbility>.Parse("Myconian_BossBash", value, errorInfo),
                GetObject = () => Myconian_BossBash } },
           { "Myconian_Drain", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Myconian_Drain = 
                   JsonObjectParser<AIAbility>.Parse("Myconian_Drain", value, errorInfo),
                GetObject = () => Myconian_Drain } },
           { "Myconian_Mindspores", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Myconian_Mindspores = 
                   JsonObjectParser<AIAbility>.Parse("Myconian_Mindspores", value, errorInfo),
                GetObject = () => Myconian_Mindspores } },
           { "Myconian_Mindspores_Permanent", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Myconian_Mindspores_Permanent = 
                   JsonObjectParser<AIAbility>.Parse("Myconian_Mindspores_Permanent", value, errorInfo),
                GetObject = () => Myconian_Mindspores_Permanent } },
           { "Myconian_Shock", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Myconian_Shock = 
                   JsonObjectParser<AIAbility>.Parse("Myconian_Shock", value, errorInfo),
                GetObject = () => Myconian_Shock } },
           { "Myconian_TidalCurse", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Myconian_TidalCurse = 
                   JsonObjectParser<AIAbility>.Parse("Myconian_TidalCurse", value, errorInfo),
                GetObject = () => Myconian_TidalCurse } },
           { "NecroDarknessWave", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NecroDarknessWave = 
                   JsonObjectParser<AIAbility>.Parse("NecroDarknessWave", value, errorInfo),
                GetObject = () => NecroDarknessWave } },
           { "NecroDeathsHold", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NecroDeathsHold = 
                   JsonObjectParser<AIAbility>.Parse("NecroDeathsHold", value, errorInfo),
                GetObject = () => NecroDeathsHold } },
           { "NecroPainBubble", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NecroPainBubble = 
                   JsonObjectParser<AIAbility>.Parse("NecroPainBubble", value, errorInfo),
                GetObject = () => NecroPainBubble } },
           { "NecroSpark", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NecroSpark = 
                   JsonObjectParser<AIAbility>.Parse("NecroSpark", value, errorInfo),
                GetObject = () => NecroSpark } },
           { "NecroSparkPerching", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NecroSparkPerching = 
                   JsonObjectParser<AIAbility>.Parse("NecroSparkPerching", value, errorInfo),
                GetObject = () => NecroSparkPerching } },
           { "NightmareDarknessBomb", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NightmareDarknessBomb = 
                   JsonObjectParser<AIAbility>.Parse("NightmareDarknessBomb", value, errorInfo),
                GetObject = () => NightmareDarknessBomb } },
           { "NightmareHoof", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NightmareHoof = 
                   JsonObjectParser<AIAbility>.Parse("NightmareHoof", value, errorInfo),
                GetObject = () => NightmareHoof } },
           { "NpcBlockingStance", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NpcBlockingStance = 
                   JsonObjectParser<AIAbility>.Parse("NpcBlockingStance", value, errorInfo),
                GetObject = () => NpcBlockingStance } },
           { "NpcDoubleHitCurse", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NpcDoubleHitCurse = 
                   JsonObjectParser<AIAbility>.Parse("NpcDoubleHitCurse", value, errorInfo),
                GetObject = () => NpcDoubleHitCurse } },
           { "NpcHeadcracker", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NpcHeadcracker = 
                   JsonObjectParser<AIAbility>.Parse("NpcHeadcracker", value, errorInfo),
                GetObject = () => NpcHeadcracker } },
           { "NpcSmash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NpcSmash = 
                   JsonObjectParser<AIAbility>.Parse("NpcSmash", value, errorInfo),
                GetObject = () => NpcSmash } },
           { "OgreClubA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OgreClubA = 
                   JsonObjectParser<AIAbility>.Parse("OgreClubA", value, errorInfo),
                GetObject = () => OgreClubA } },
           { "OgreClubB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OgreClubB = 
                   JsonObjectParser<AIAbility>.Parse("OgreClubB", value, errorInfo),
                GetObject = () => OgreClubB } },
           { "OgreStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OgreStun = 
                   JsonObjectParser<AIAbility>.Parse("OgreStun", value, errorInfo),
                GetObject = () => OgreStun } },
           { "OgreThrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OgreThrow = 
                   JsonObjectParser<AIAbility>.Parse("OgreThrow", value, errorInfo),
                GetObject = () => OgreThrow } },
           { "OrcAreaHalberdAttack", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcAreaHalberdAttack = 
                   JsonObjectParser<AIAbility>.Parse("OrcAreaHalberdAttack", value, errorInfo),
                GetObject = () => OrcAreaHalberdAttack } },
           { "OrcAreaHalberdBoss", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcAreaHalberdBoss = 
                   JsonObjectParser<AIAbility>.Parse("OrcAreaHalberdBoss", value, errorInfo),
                GetObject = () => OrcAreaHalberdBoss } },
           { "OrcArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("OrcArrow1", value, errorInfo),
                GetObject = () => OrcArrow1 } },
           { "OrcArrow2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcArrow2 = 
                   JsonObjectParser<AIAbility>.Parse("OrcArrow2", value, errorInfo),
                GetObject = () => OrcArrow2 } },
           { "OrcDarknessBall", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcDarknessBall = 
                   JsonObjectParser<AIAbility>.Parse("OrcDarknessBall", value, errorInfo),
                GetObject = () => OrcDarknessBall } },
           { "OrcDeathsHold", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcDeathsHold = 
                   JsonObjectParser<AIAbility>.Parse("OrcDeathsHold", value, errorInfo),
                GetObject = () => OrcDeathsHold } },
           { "OrcDebuffArrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcDebuffArrow = 
                   JsonObjectParser<AIAbility>.Parse("OrcDebuffArrow", value, errorInfo),
                GetObject = () => OrcDebuffArrow } },
           { "OrcElectricStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcElectricStun = 
                   JsonObjectParser<AIAbility>.Parse("OrcElectricStun", value, errorInfo),
                GetObject = () => OrcElectricStun } },
           { "OrcEvasionBubble", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcEvasionBubble = 
                   JsonObjectParser<AIAbility>.Parse("OrcEvasionBubble", value, errorInfo),
                GetObject = () => OrcEvasionBubble } },
           { "OrcExtinguishLife", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcExtinguishLife = 
                   JsonObjectParser<AIAbility>.Parse("OrcExtinguishLife", value, errorInfo),
                GetObject = () => OrcExtinguishLife } },
           { "OrcFinishingBlow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcFinishingBlow = 
                   JsonObjectParser<AIAbility>.Parse("OrcFinishingBlow", value, errorInfo),
                GetObject = () => OrcFinishingBlow } },
           { "OrcFinishingBlowFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcFinishingBlowFire = 
                   JsonObjectParser<AIAbility>.Parse("OrcFinishingBlowFire", value, errorInfo),
                GetObject = () => OrcFinishingBlowFire } },
           { "OrcFireball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcFireball = 
                   JsonObjectParser<AIAbility>.Parse("OrcFireball", value, errorInfo),
                GetObject = () => OrcFireball } },
           { "OrcFireBolts", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcFireBolts = 
                   JsonObjectParser<AIAbility>.Parse("OrcFireBolts", value, errorInfo),
                GetObject = () => OrcFireBolts } },
           { "OrcHalberdAttack", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcHalberdAttack = 
                   JsonObjectParser<AIAbility>.Parse("OrcHalberdAttack", value, errorInfo),
                GetObject = () => OrcHalberdAttack } },
           { "OrcHeal1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcHeal1 = 
                   JsonObjectParser<AIAbility>.Parse("OrcHeal1", value, errorInfo),
                GetObject = () => OrcHeal1 } },
           { "OrcHeal2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcHeal2 = 
                   JsonObjectParser<AIAbility>.Parse("OrcHeal2", value, errorInfo),
                GetObject = () => OrcHeal2 } },
           { "OrcHipThrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcHipThrow = 
                   JsonObjectParser<AIAbility>.Parse("OrcHipThrow", value, errorInfo),
                GetObject = () => OrcHipThrow } },
           { "OrcKneeKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcKneeKick = 
                   JsonObjectParser<AIAbility>.Parse("OrcKneeKick", value, errorInfo),
                GetObject = () => OrcKneeKick } },
           { "OrcKnockbackBolt", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcKnockbackBolt = 
                   JsonObjectParser<AIAbility>.Parse("OrcKnockbackBolt", value, errorInfo),
                GetObject = () => OrcKnockbackBolt } },
           { "OrcLieutenantDebuffTaunt", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcLieutenantDebuffTaunt = 
                   JsonObjectParser<AIAbility>.Parse("OrcLieutenantDebuffTaunt", value, errorInfo),
                GetObject = () => OrcLieutenantDebuffTaunt } },
           { "OrcParry", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcParry = 
                   JsonObjectParser<AIAbility>.Parse("OrcParry", value, errorInfo),
                GetObject = () => OrcParry } },
           { "OrcParryFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcParryFire = 
                   JsonObjectParser<AIAbility>.Parse("OrcParryFire", value, errorInfo),
                GetObject = () => OrcParryFire } },
           { "OrcPunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcPunch = 
                   JsonObjectParser<AIAbility>.Parse("OrcPunch", value, errorInfo),
                GetObject = () => OrcPunch } },
           { "OrcSlice", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcSlice = 
                   JsonObjectParser<AIAbility>.Parse("OrcSlice", value, errorInfo),
                GetObject = () => OrcSlice } },
           { "OrcSpearAttack", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcSpearAttack = 
                   JsonObjectParser<AIAbility>.Parse("OrcSpearAttack", value, errorInfo),
                GetObject = () => OrcSpearAttack } },
           { "OrcStaffSmash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcStaffSmash = 
                   JsonObjectParser<AIAbility>.Parse("OrcStaffSmash", value, errorInfo),
                GetObject = () => OrcStaffSmash } },
           { "OrcSummonSigil1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcSummonSigil1 = 
                   JsonObjectParser<AIAbility>.Parse("OrcSummonSigil1", value, errorInfo),
                GetObject = () => OrcSummonSigil1 } },
           { "OrcSummonUrak2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcSummonUrak2 = 
                   JsonObjectParser<AIAbility>.Parse("OrcSummonUrak2", value, errorInfo),
                GetObject = () => OrcSummonUrak2 } },
           { "OrcSwordSlash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcSwordSlash = 
                   JsonObjectParser<AIAbility>.Parse("OrcSwordSlash", value, errorInfo),
                GetObject = () => OrcSwordSlash } },
           { "OrcSwordSlashFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcSwordSlashFire = 
                   JsonObjectParser<AIAbility>.Parse("OrcSwordSlashFire", value, errorInfo),
                GetObject = () => OrcSwordSlashFire } },
           { "OrcVenomstrike0", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcVenomstrike0 = 
                   JsonObjectParser<AIAbility>.Parse("OrcVenomstrike0", value, errorInfo),
                GetObject = () => OrcVenomstrike0 } },
           { "OrcVenomstrike1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcVenomstrike1 = 
                   JsonObjectParser<AIAbility>.Parse("OrcVenomstrike1", value, errorInfo),
                GetObject = () => OrcVenomstrike1 } },
           { "OrcWaveOfDarkness", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcWaveOfDarkness = 
                   JsonObjectParser<AIAbility>.Parse("OrcWaveOfDarkness", value, errorInfo),
                GetObject = () => OrcWaveOfDarkness } },
           { "Peck", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Peck = 
                   JsonObjectParser<AIAbility>.Parse("Peck", value, errorInfo),
                GetObject = () => Peck } },
           { "PetUndeadArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadArrow1", value, errorInfo),
                GetObject = () => PetUndeadArrow1 } },
           { "PetUndeadArrow2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadArrow2 = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadArrow2", value, errorInfo),
                GetObject = () => PetUndeadArrow2 } },
           { "PetUndeadDefensiveBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadDefensiveBurst = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadDefensiveBurst", value, errorInfo),
                GetObject = () => PetUndeadDefensiveBurst } },
           { "PetUndeadFireballA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadFireballA = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadFireballA", value, errorInfo),
                GetObject = () => PetUndeadFireballA } },
           { "PetUndeadFireballB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadFireballB = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadFireballB", value, errorInfo),
                GetObject = () => PetUndeadFireballB } },
           { "PetUndeadOmegaArrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadOmegaArrow = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadOmegaArrow", value, errorInfo),
                GetObject = () => PetUndeadOmegaArrow } },
           { "PetUndeadPunch1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadPunch1 = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadPunch1", value, errorInfo),
                GetObject = () => PetUndeadPunch1 } },
           { "PetUndeadSword1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadSword1 = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadSword1", value, errorInfo),
                GetObject = () => PetUndeadSword1 } },
           { "PetUndeadSword2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadSword2 = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadSword2", value, errorInfo),
                GetObject = () => PetUndeadSword2 } },
           { "PetUndeadSwordAngry", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadSwordAngry = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadSwordAngry", value, errorInfo),
                GetObject = () => PetUndeadSwordAngry } },
           { "RakAcidBomb", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakAcidBomb = 
                   JsonObjectParser<AIAbility>.Parse("RakAcidBomb", value, errorInfo),
                GetObject = () => RakAcidBomb } },
           { "RakAimedShot", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakAimedShot = 
                   JsonObjectParser<AIAbility>.Parse("RakAimedShot", value, errorInfo),
                GetObject = () => RakAimedShot } },
           { "RakBarrage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakBarrage = 
                   JsonObjectParser<AIAbility>.Parse("RakBarrage", value, errorInfo),
                GetObject = () => RakBarrage } },
           { "RakBasicShot", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakBasicShot = 
                   JsonObjectParser<AIAbility>.Parse("RakBasicShot", value, errorInfo),
                GetObject = () => RakBasicShot } },
           { "RakBossPerchSlow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakBossPerchSlow = 
                   JsonObjectParser<AIAbility>.Parse("RakBossPerchSlow", value, errorInfo),
                GetObject = () => RakBossPerchSlow } },
           { "RakBossSlow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakBossSlow = 
                   JsonObjectParser<AIAbility>.Parse("RakBossSlow", value, errorInfo),
                GetObject = () => RakBossSlow } },
           { "RakBowBash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakBowBash = 
                   JsonObjectParser<AIAbility>.Parse("RakBowBash", value, errorInfo),
                GetObject = () => RakBowBash } },
           { "RakBreatheFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakBreatheFire = 
                   JsonObjectParser<AIAbility>.Parse("RakBreatheFire", value, errorInfo),
                GetObject = () => RakBreatheFire } },
           { "RakDecapitate", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakDecapitate = 
                   JsonObjectParser<AIAbility>.Parse("RakDecapitate", value, errorInfo),
                GetObject = () => RakDecapitate } },
           { "RakFireball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakFireball = 
                   JsonObjectParser<AIAbility>.Parse("RakFireball", value, errorInfo),
                GetObject = () => RakFireball } },
           { "RakHackingBlade", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakHackingBlade = 
                   JsonObjectParser<AIAbility>.Parse("RakHackingBlade", value, errorInfo),
                GetObject = () => RakHackingBlade } },
           { "RakHealingMist", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakHealingMist = 
                   JsonObjectParser<AIAbility>.Parse("RakHealingMist", value, errorInfo),
                GetObject = () => RakHealingMist } },
           { "RakHookShot", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakHookShot = 
                   JsonObjectParser<AIAbility>.Parse("RakHookShot", value, errorInfo),
                GetObject = () => RakHookShot } },
           { "RakKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakKick = 
                   JsonObjectParser<AIAbility>.Parse("RakKick", value, errorInfo),
                GetObject = () => RakKick } },
           { "RakKnee", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakKnee = 
                   JsonObjectParser<AIAbility>.Parse("RakKnee", value, errorInfo),
                GetObject = () => RakKnee } },
           { "RakMindreave", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakMindreave = 
                   JsonObjectParser<AIAbility>.Parse("RakMindreave", value, errorInfo),
                GetObject = () => RakMindreave } },
           { "RakPainBubble", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakPainBubble = 
                   JsonObjectParser<AIAbility>.Parse("RakPainBubble", value, errorInfo),
                GetObject = () => RakPainBubble } },
           { "RakPanicCharge", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakPanicCharge = 
                   JsonObjectParser<AIAbility>.Parse("RakPanicCharge", value, errorInfo),
                GetObject = () => RakPanicCharge } },
           { "RakPoisonArrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakPoisonArrow = 
                   JsonObjectParser<AIAbility>.Parse("RakPoisonArrow", value, errorInfo),
                GetObject = () => RakPoisonArrow } },
           { "RakReconstruct", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakReconstruct = 
                   JsonObjectParser<AIAbility>.Parse("RakReconstruct", value, errorInfo),
                GetObject = () => RakReconstruct } },
           { "RakRevitalize", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakRevitalize = 
                   JsonObjectParser<AIAbility>.Parse("RakRevitalize", value, errorInfo),
                GetObject = () => RakRevitalize } },
           { "RakRingOfFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakRingOfFire = 
                   JsonObjectParser<AIAbility>.Parse("RakRingOfFire", value, errorInfo),
                GetObject = () => RakRingOfFire } },
           { "RakSlash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakSlash = 
                   JsonObjectParser<AIAbility>.Parse("RakSlash", value, errorInfo),
                GetObject = () => RakSlash } },
           { "RakStaffBlock", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakStaffBlock = 
                   JsonObjectParser<AIAbility>.Parse("RakStaffBlock", value, errorInfo),
                GetObject = () => RakStaffBlock } },
           { "RakStaffHeavy", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakStaffHeavy = 
                   JsonObjectParser<AIAbility>.Parse("RakStaffHeavy", value, errorInfo),
                GetObject = () => RakStaffHeavy } },
           { "RakStaffHit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakStaffHit = 
                   JsonObjectParser<AIAbility>.Parse("RakStaffHit", value, errorInfo),
                GetObject = () => RakStaffHit } },
           { "RakStaffPin", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakStaffPin = 
                   JsonObjectParser<AIAbility>.Parse("RakStaffPin", value, errorInfo),
                GetObject = () => RakStaffPin } },
           { "RakSwordSlash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakSwordSlash = 
                   JsonObjectParser<AIAbility>.Parse("RakSwordSlash", value, errorInfo),
                GetObject = () => RakSwordSlash } },
           { "RakToxinBomb", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakToxinBomb = 
                   JsonObjectParser<AIAbility>.Parse("RakToxinBomb", value, errorInfo),
                GetObject = () => RakToxinBomb } },
           { "RanalonDoctrineKeeperBlind", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonDoctrineKeeperBlind = 
                   JsonObjectParser<AIAbility>.Parse("RanalonDoctrineKeeperBlind", value, errorInfo),
                GetObject = () => RanalonDoctrineKeeperBlind } },
           { "RanalonDoctrineKeeperStab", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonDoctrineKeeperStab = 
                   JsonObjectParser<AIAbility>.Parse("RanalonDoctrineKeeperStab", value, errorInfo),
                GetObject = () => RanalonDoctrineKeeperStab } },
           { "RanalonGuardianBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonGuardianBite = 
                   JsonObjectParser<AIAbility>.Parse("RanalonGuardianBite", value, errorInfo),
                GetObject = () => RanalonGuardianBite } },
           { "RanalonGuardianBlind", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonGuardianBlind = 
                   JsonObjectParser<AIAbility>.Parse("RanalonGuardianBlind", value, errorInfo),
                GetObject = () => RanalonGuardianBlind } },
           { "RanalonGuardianStab", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonGuardianStab = 
                   JsonObjectParser<AIAbility>.Parse("RanalonGuardianStab", value, errorInfo),
                GetObject = () => RanalonGuardianStab } },
           { "RanalonGuardianStabB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonGuardianStabB = 
                   JsonObjectParser<AIAbility>.Parse("RanalonGuardianStabB", value, errorInfo),
                GetObject = () => RanalonGuardianStabB } },
           { "RanalonHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonHeal = 
                   JsonObjectParser<AIAbility>.Parse("RanalonHeal", value, errorInfo),
                GetObject = () => RanalonHeal } },
           { "RanalonHit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonHit = 
                   JsonObjectParser<AIAbility>.Parse("RanalonHit", value, errorInfo),
                GetObject = () => RanalonHit } },
           { "RanalonHitB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonHitB = 
                   JsonObjectParser<AIAbility>.Parse("RanalonHitB", value, errorInfo),
                GetObject = () => RanalonHitB } },
           { "RanalonKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonKick = 
                   JsonObjectParser<AIAbility>.Parse("RanalonKick", value, errorInfo),
                GetObject = () => RanalonKick } },
           { "RanalonRoot", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonRoot = 
                   JsonObjectParser<AIAbility>.Parse("RanalonRoot", value, errorInfo),
                GetObject = () => RanalonRoot } },
           { "RanalonSelfBuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonSelfBuff = 
                   JsonObjectParser<AIAbility>.Parse("RanalonSelfBuff", value, errorInfo),
                GetObject = () => RanalonSelfBuff } },
           { "RanalonSelfBuffElite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonSelfBuffElite = 
                   JsonObjectParser<AIAbility>.Parse("RanalonSelfBuffElite", value, errorInfo),
                GetObject = () => RanalonSelfBuffElite } },
           { "RanalonTongue", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonTongue = 
                   JsonObjectParser<AIAbility>.Parse("RanalonTongue", value, errorInfo),
                GetObject = () => RanalonTongue } },
           { "RanalonZap", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonZap = 
                   JsonObjectParser<AIAbility>.Parse("RanalonZap", value, errorInfo),
                GetObject = () => RanalonZap } },
           { "RanalonZapB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonZapB = 
                   JsonObjectParser<AIAbility>.Parse("RanalonZapB", value, errorInfo),
                GetObject = () => RanalonZapB } },
           { "RatBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBite = 
                   JsonObjectParser<AIAbility>.Parse("RatBite", value, errorInfo),
                GetObject = () => RatBite } },
           { "RatBite_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBite_Pet = 
                   JsonObjectParser<AIAbility>.Parse("RatBite_Pet", value, errorInfo),
                GetObject = () => RatBite_Pet } },
           { "RatBurn_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBurn_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("RatBurn_Pet1", value, errorInfo),
                GetObject = () => RatBurn_Pet1 } },
           { "RatBurn_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBurn_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("RatBurn_Pet2", value, errorInfo),
                GetObject = () => RatBurn_Pet2 } },
           { "RatBurn_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBurn_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("RatBurn_Pet3", value, errorInfo),
                GetObject = () => RatBurn_Pet3 } },
           { "RatBurn_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBurn_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("RatBurn_Pet4", value, errorInfo),
                GetObject = () => RatBurn_Pet4 } },
           { "RatBurn_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBurn_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("RatBurn_Pet5", value, errorInfo),
                GetObject = () => RatBurn_Pet5 } },
           { "RatBurn_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBurn_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("RatBurn_Pet6", value, errorInfo),
                GetObject = () => RatBurn_Pet6 } },
           { "RatClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatClaw = 
                   JsonObjectParser<AIAbility>.Parse("RatClaw", value, errorInfo),
                GetObject = () => RatClaw } },
           { "RatClaw_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatClaw_Pet = 
                   JsonObjectParser<AIAbility>.Parse("RatClaw_Pet", value, errorInfo),
                GetObject = () => RatClaw_Pet } },
           { "RatDeRage_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatDeRage_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("RatDeRage_Pet1", value, errorInfo),
                GetObject = () => RatDeRage_Pet1 } },
           { "RatDeRage_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatDeRage_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("RatDeRage_Pet2", value, errorInfo),
                GetObject = () => RatDeRage_Pet2 } },
           { "RatDeRage_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatDeRage_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("RatDeRage_Pet3", value, errorInfo),
                GetObject = () => RatDeRage_Pet3 } },
           { "RatDeRage_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatDeRage_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("RatDeRage_Pet4", value, errorInfo),
                GetObject = () => RatDeRage_Pet4 } },
           { "RatDeRage_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatDeRage_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("RatDeRage_Pet5", value, errorInfo),
                GetObject = () => RatDeRage_Pet5 } },
           { "RatDeRage_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatDeRage_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("RatDeRage_Pet6", value, errorInfo),
                GetObject = () => RatDeRage_Pet6 } },
           { "RatHeal_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatHeal_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("RatHeal_Pet1", value, errorInfo),
                GetObject = () => RatHeal_Pet1 } },
           { "RatHeal_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatHeal_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("RatHeal_Pet2", value, errorInfo),
                GetObject = () => RatHeal_Pet2 } },
           { "RatHeal_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatHeal_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("RatHeal_Pet3", value, errorInfo),
                GetObject = () => RatHeal_Pet3 } },
           { "RatHeal_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatHeal_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("RatHeal_Pet4", value, errorInfo),
                GetObject = () => RatHeal_Pet4 } },
           { "RatHeal_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatHeal_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("RatHeal_Pet5", value, errorInfo),
                GetObject = () => RatHeal_Pet5 } },
           { "RatHeal_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatHeal_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("RatHeal_Pet6", value, errorInfo),
                GetObject = () => RatHeal_Pet6 } },
           { "RatVuln_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatVuln_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("RatVuln_Pet1", value, errorInfo),
                GetObject = () => RatVuln_Pet1 } },
           { "RatVuln_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatVuln_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("RatVuln_Pet2", value, errorInfo),
                GetObject = () => RatVuln_Pet2 } },
           { "RatVuln_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatVuln_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("RatVuln_Pet3", value, errorInfo),
                GetObject = () => RatVuln_Pet3 } },
           { "RatVuln_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatVuln_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("RatVuln_Pet4", value, errorInfo),
                GetObject = () => RatVuln_Pet4 } },
           { "ReboundAura1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ReboundAura1 = 
                   JsonObjectParser<AIAbility>.Parse("ReboundAura1", value, errorInfo),
                GetObject = () => ReboundAura1 } },
           { "RedCrystalBlast", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RedCrystalBlast = 
                   JsonObjectParser<AIAbility>.Parse("RedCrystalBlast", value, errorInfo),
                GetObject = () => RedCrystalBlast } },
           { "RedCrystalBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RedCrystalBurst = 
                   JsonObjectParser<AIAbility>.Parse("RedCrystalBurst", value, errorInfo),
                GetObject = () => RedCrystalBurst } },
           { "RhinoBossRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RhinoBossRage = 
                   JsonObjectParser<AIAbility>.Parse("RhinoBossRage", value, errorInfo),
                GetObject = () => RhinoBossRage } },
           { "RhinoFireball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RhinoFireball = 
                   JsonObjectParser<AIAbility>.Parse("RhinoFireball", value, errorInfo),
                GetObject = () => RhinoFireball } },
           { "RhinoHorn", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RhinoHorn = 
                   JsonObjectParser<AIAbility>.Parse("RhinoHorn", value, errorInfo),
                GetObject = () => RhinoHorn } },
           { "RhinoRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RhinoRage = 
                   JsonObjectParser<AIAbility>.Parse("RhinoRage", value, errorInfo),
                GetObject = () => RhinoRage } },
           { "ScrayBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ScrayBite = 
                   JsonObjectParser<AIAbility>.Parse("ScrayBite", value, errorInfo),
                GetObject = () => ScrayBite } },
           { "ScrayStab", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ScrayStab = 
                   JsonObjectParser<AIAbility>.Parse("ScrayStab", value, errorInfo),
                GetObject = () => ScrayStab } },
           { "SedgewickMegaSwordAngry", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SedgewickMegaSwordAngry = 
                   JsonObjectParser<AIAbility>.Parse("SedgewickMegaSwordAngry", value, errorInfo),
                GetObject = () => SedgewickMegaSwordAngry } },
           { "SheepBomb1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SheepBomb1 = 
                   JsonObjectParser<AIAbility>.Parse("SheepBomb1", value, errorInfo),
                GetObject = () => SheepBomb1 } },
           { "SherzatAcidSpit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SherzatAcidSpit = 
                   JsonObjectParser<AIAbility>.Parse("SherzatAcidSpit", value, errorInfo),
                GetObject = () => SherzatAcidSpit } },
           { "SherzatClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SherzatClaw = 
                   JsonObjectParser<AIAbility>.Parse("SherzatClaw", value, errorInfo),
                GetObject = () => SherzatClaw } },
           { "SherzatDisintegrate", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SherzatDisintegrate = 
                   JsonObjectParser<AIAbility>.Parse("SherzatDisintegrate", value, errorInfo),
                GetObject = () => SherzatDisintegrate } },
           { "Slime_SummonSlime", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Slime_SummonSlime = 
                   JsonObjectParser<AIAbility>.Parse("Slime_SummonSlime", value, errorInfo),
                GetObject = () => Slime_SummonSlime } },
           { "SlimeBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlimeBite = 
                   JsonObjectParser<AIAbility>.Parse("SlimeBite", value, errorInfo),
                GetObject = () => SlimeBite } },
           { "SlimeBiteB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlimeBiteB = 
                   JsonObjectParser<AIAbility>.Parse("SlimeBiteB", value, errorInfo),
                GetObject = () => SlimeBiteB } },
           { "SlimeKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlimeKick = 
                   JsonObjectParser<AIAbility>.Parse("SlimeKick", value, errorInfo),
                GetObject = () => SlimeKick } },
           { "SlimeKickB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlimeKickB = 
                   JsonObjectParser<AIAbility>.Parse("SlimeKickB", value, errorInfo),
                GetObject = () => SlimeKickB } },
           { "SlimeSpit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlimeSpit = 
                   JsonObjectParser<AIAbility>.Parse("SlimeSpit", value, errorInfo),
                GetObject = () => SlimeSpit } },
           { "SlimeSuperSpit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlimeSuperSpit = 
                   JsonObjectParser<AIAbility>.Parse("SlimeSuperSpit", value, errorInfo),
                GetObject = () => SlimeSuperSpit } },
           { "SlugPoisonBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlugPoisonBite = 
                   JsonObjectParser<AIAbility>.Parse("SlugPoisonBite", value, errorInfo),
                GetObject = () => SlugPoisonBite } },
           { "SlugPoisonBite2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlugPoisonBite2 = 
                   JsonObjectParser<AIAbility>.Parse("SlugPoisonBite2", value, errorInfo),
                GetObject = () => SlugPoisonBite2 } },
           { "SlugPoisonBite3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlugPoisonBite3 = 
                   JsonObjectParser<AIAbility>.Parse("SlugPoisonBite3", value, errorInfo),
                GetObject = () => SlugPoisonBite3 } },
           { "SlugPoisonRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlugPoisonRage = 
                   JsonObjectParser<AIAbility>.Parse("SlugPoisonRage", value, errorInfo),
                GetObject = () => SlugPoisonRage } },
           { "SlugPoisonRage2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlugPoisonRage2 = 
                   JsonObjectParser<AIAbility>.Parse("SlugPoisonRage2", value, errorInfo),
                GetObject = () => SlugPoisonRage2 } },
           { "SlugPoisonRage3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlugPoisonRage3 = 
                   JsonObjectParser<AIAbility>.Parse("SlugPoisonRage3", value, errorInfo),
                GetObject = () => SlugPoisonRage3 } },
           { "SnailRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SnailRage = 
                   JsonObjectParser<AIAbility>.Parse("SnailRage", value, errorInfo),
                GetObject = () => SnailRage } },
           { "SnailRageB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SnailRageB = 
                   JsonObjectParser<AIAbility>.Parse("SnailRageB", value, errorInfo),
                GetObject = () => SnailRageB } },
           { "SnailRageC", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SnailRageC = 
                   JsonObjectParser<AIAbility>.Parse("SnailRageC", value, errorInfo),
                GetObject = () => SnailRageC } },
           { "SnailStrike", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SnailStrike = 
                   JsonObjectParser<AIAbility>.Parse("SnailStrike", value, errorInfo),
                GetObject = () => SnailStrike } },
           { "SpiderBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderBite = 
                   JsonObjectParser<AIAbility>.Parse("SpiderBite", value, errorInfo),
                GetObject = () => SpiderBite } },
           { "SpiderBossFreePin", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderBossFreePin = 
                   JsonObjectParser<AIAbility>.Parse("SpiderBossFreePin", value, errorInfo),
                GetObject = () => SpiderBossFreePin } },
           { "SpiderFireball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderFireball = 
                   JsonObjectParser<AIAbility>.Parse("SpiderFireball", value, errorInfo),
                GetObject = () => SpiderFireball } },
           { "SpiderIncubate", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderIncubate = 
                   JsonObjectParser<AIAbility>.Parse("SpiderIncubate", value, errorInfo),
                GetObject = () => SpiderIncubate } },
           { "SpiderInject", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderInject = 
                   JsonObjectParser<AIAbility>.Parse("SpiderInject", value, errorInfo),
                GetObject = () => SpiderInject } },
           { "SpiderKill", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderKill = 
                   JsonObjectParser<AIAbility>.Parse("SpiderKill", value, errorInfo),
                GetObject = () => SpiderKill } },
           { "SpiderKill2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderKill2 = 
                   JsonObjectParser<AIAbility>.Parse("SpiderKill2", value, errorInfo),
                GetObject = () => SpiderKill2 } },
           { "SpiderKill3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderKill3 = 
                   JsonObjectParser<AIAbility>.Parse("SpiderKill3", value, errorInfo),
                GetObject = () => SpiderKill3 } },
           { "SpiderPin", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderPin = 
                   JsonObjectParser<AIAbility>.Parse("SpiderPin", value, errorInfo),
                GetObject = () => SpiderPin } },
           { "SpyPortalZap", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpyPortalZap = 
                   JsonObjectParser<AIAbility>.Parse("SpyPortalZap", value, errorInfo),
                GetObject = () => SpyPortalZap } },
           { "SpyPortalZap2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpyPortalZap2 = 
                   JsonObjectParser<AIAbility>.Parse("SpyPortalZap2", value, errorInfo),
                GetObject = () => SpyPortalZap2 } },
           { "StrigaBuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => StrigaBuff = 
                   JsonObjectParser<AIAbility>.Parse("StrigaBuff", value, errorInfo),
                GetObject = () => StrigaBuff } },
           { "StrigaClawA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => StrigaClawA = 
                   JsonObjectParser<AIAbility>.Parse("StrigaClawA", value, errorInfo),
                GetObject = () => StrigaClawA } },
           { "StrigaClawB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => StrigaClawB = 
                   JsonObjectParser<AIAbility>.Parse("StrigaClawB", value, errorInfo),
                GetObject = () => StrigaClawB } },
           { "StrigaFireBreath", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => StrigaFireBreath = 
                   JsonObjectParser<AIAbility>.Parse("StrigaFireBreath", value, errorInfo),
                GetObject = () => StrigaFireBreath } },
           { "StrigaReap", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => StrigaReap = 
                   JsonObjectParser<AIAbility>.Parse("StrigaReap", value, errorInfo),
                GetObject = () => StrigaReap } },
           { "StrigaReap2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => StrigaReap2 = 
                   JsonObjectParser<AIAbility>.Parse("StrigaReap2", value, errorInfo),
                GetObject = () => StrigaReap2 } },
           { "TheFogCurse", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TheFogCurse = 
                   JsonObjectParser<AIAbility>.Parse("TheFogCurse", value, errorInfo),
                GetObject = () => TheFogCurse } },
           { "TornadoFling", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TornadoFling = 
                   JsonObjectParser<AIAbility>.Parse("TornadoFling", value, errorInfo),
                GetObject = () => TornadoFling } },
           { "TornadoJolt1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TornadoJolt1 = 
                   JsonObjectParser<AIAbility>.Parse("TornadoJolt1", value, errorInfo),
                GetObject = () => TornadoJolt1 } },
           { "TornadoToss", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TornadoToss = 
                   JsonObjectParser<AIAbility>.Parse("TornadoToss", value, errorInfo),
                GetObject = () => TornadoToss } },
           { "TotalHorrorAttack", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TotalHorrorAttack = 
                   JsonObjectParser<AIAbility>.Parse("TotalHorrorAttack", value, errorInfo),
                GetObject = () => TotalHorrorAttack } },
           { "TotalHorrorHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TotalHorrorHeal = 
                   JsonObjectParser<AIAbility>.Parse("TotalHorrorHeal", value, errorInfo),
                GetObject = () => TotalHorrorHeal } },
           { "TotalHorrorHeal2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TotalHorrorHeal2 = 
                   JsonObjectParser<AIAbility>.Parse("TotalHorrorHeal2", value, errorInfo),
                GetObject = () => TotalHorrorHeal2 } },
           { "TotalHorrorStretch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TotalHorrorStretch = 
                   JsonObjectParser<AIAbility>.Parse("TotalHorrorStretch", value, errorInfo),
                GetObject = () => TotalHorrorStretch } },
           { "TrainingGolemFireBreath", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrainingGolemFireBreath = 
                   JsonObjectParser<AIAbility>.Parse("TrainingGolemFireBreath", value, errorInfo),
                GetObject = () => TrainingGolemFireBreath } },
           { "TrainingGolemFireBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrainingGolemFireBurst = 
                   JsonObjectParser<AIAbility>.Parse("TrainingGolemFireBurst", value, errorInfo),
                GetObject = () => TrainingGolemFireBurst } },
           { "TrainingGolemHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrainingGolemHeal = 
                   JsonObjectParser<AIAbility>.Parse("TrainingGolemHeal", value, errorInfo),
                GetObject = () => TrainingGolemHeal } },
           { "TrainingGolemHealB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrainingGolemHealB = 
                   JsonObjectParser<AIAbility>.Parse("TrainingGolemHealB", value, errorInfo),
                GetObject = () => TrainingGolemHealB } },
           { "TrainingGolemPunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrainingGolemPunch = 
                   JsonObjectParser<AIAbility>.Parse("TrainingGolemPunch", value, errorInfo),
                GetObject = () => TrainingGolemPunch } },
           { "TrainingGolemStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrainingGolemStun = 
                   JsonObjectParser<AIAbility>.Parse("TrainingGolemStun", value, errorInfo),
                GetObject = () => TrainingGolemStun } },
           { "TriffidClawA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TriffidClawA = 
                   JsonObjectParser<AIAbility>.Parse("TriffidClawA", value, errorInfo),
                GetObject = () => TriffidClawA } },
           { "TriffidClawB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TriffidClawB = 
                   JsonObjectParser<AIAbility>.Parse("TriffidClawB", value, errorInfo),
                GetObject = () => TriffidClawB } },
           { "TriffidShot", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TriffidShot = 
                   JsonObjectParser<AIAbility>.Parse("TriffidShot", value, errorInfo),
                GetObject = () => TriffidShot } },
           { "TriffidSpore", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TriffidSpore = 
                   JsonObjectParser<AIAbility>.Parse("TriffidSpore", value, errorInfo),
                GetObject = () => TriffidSpore } },
           { "TriffidTongue", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TriffidTongue = 
                   JsonObjectParser<AIAbility>.Parse("TriffidTongue", value, errorInfo),
                GetObject = () => TriffidTongue } },
           { "TriffidTongueElite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TriffidTongueElite = 
                   JsonObjectParser<AIAbility>.Parse("TriffidTongueElite", value, errorInfo),
                GetObject = () => TriffidTongueElite } },
           { "TrollClubA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrollClubA = 
                   JsonObjectParser<AIAbility>.Parse("TrollClubA", value, errorInfo),
                GetObject = () => TrollClubA } },
           { "TrollClubB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrollClubB = 
                   JsonObjectParser<AIAbility>.Parse("TrollClubB", value, errorInfo),
                GetObject = () => TrollClubB } },
           { "TrollKnockdown", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrollKnockdown = 
                   JsonObjectParser<AIAbility>.Parse("TrollKnockdown", value, errorInfo),
                GetObject = () => TrollKnockdown } },
           { "TurretCrystalZap", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TurretCrystalZap = 
                   JsonObjectParser<AIAbility>.Parse("TurretCrystalZap", value, errorInfo),
                GetObject = () => TurretCrystalZap } },
           { "TurretCrystalZap2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TurretCrystalZap2 = 
                   JsonObjectParser<AIAbility>.Parse("TurretCrystalZap2", value, errorInfo),
                GetObject = () => TurretCrystalZap2 } },
           { "TurretCrystalZapLongRange", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TurretCrystalZapLongRange = 
                   JsonObjectParser<AIAbility>.Parse("TurretCrystalZapLongRange", value, errorInfo),
                GetObject = () => TurretCrystalZapLongRange } },
           { "TurretCrystalZapLongRange2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TurretCrystalZapLongRange2 = 
                   JsonObjectParser<AIAbility>.Parse("TurretCrystalZapLongRange2", value, errorInfo),
                GetObject = () => TurretCrystalZapLongRange2 } },
           { "UndeadArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadArrow1", value, errorInfo),
                GetObject = () => UndeadArrow1 } },
           { "UndeadArrow2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadArrow2 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadArrow2", value, errorInfo),
                GetObject = () => UndeadArrow2 } },
           { "UndeadBoneWhirlwind", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadBoneWhirlwind = 
                   JsonObjectParser<AIAbility>.Parse("UndeadBoneWhirlwind", value, errorInfo),
                GetObject = () => UndeadBoneWhirlwind } },
           { "UndeadDarknessBall", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadDarknessBall = 
                   JsonObjectParser<AIAbility>.Parse("UndeadDarknessBall", value, errorInfo),
                GetObject = () => UndeadDarknessBall } },
           { "UndeadFireballA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFireballA = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFireballA", value, errorInfo),
                GetObject = () => UndeadFireballA } },
           { "UndeadFireballA2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFireballA2 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFireballA2", value, errorInfo),
                GetObject = () => UndeadFireballA2 } },
           { "UndeadFireballB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFireballB = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFireballB", value, errorInfo),
                GetObject = () => UndeadFireballB } },
           { "UndeadFireballB2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFireballB2 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFireballB2", value, errorInfo),
                GetObject = () => UndeadFireballB2 } },
           { "UndeadFireballLongA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFireballLongA = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFireballLongA", value, errorInfo),
                GetObject = () => UndeadFireballLongA } },
           { "UndeadFireballLongB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFireballLongB = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFireballLongB", value, errorInfo),
                GetObject = () => UndeadFireballLongB } },
           { "UndeadFireballLongB2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFireballLongB2 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFireballLongB2", value, errorInfo),
                GetObject = () => UndeadFireballLongB2 } },
           { "UndeadFreezeBall", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFreezeBall = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFreezeBall", value, errorInfo),
                GetObject = () => UndeadFreezeBall } },
           { "UndeadGrappleArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadGrappleArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadGrappleArrow1", value, errorInfo),
                GetObject = () => UndeadGrappleArrow1 } },
           { "UndeadIceBall1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadIceBall1 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadIceBall1", value, errorInfo),
                GetObject = () => UndeadIceBall1 } },
           { "UndeadLightningSmite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadLightningSmite = 
                   JsonObjectParser<AIAbility>.Parse("UndeadLightningSmite", value, errorInfo),
                GetObject = () => UndeadLightningSmite } },
           { "UndeadMegaSword1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadMegaSword1 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadMegaSword1", value, errorInfo),
                GetObject = () => UndeadMegaSword1 } },
           { "UndeadMegaSword2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadMegaSword2 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadMegaSword2", value, errorInfo),
                GetObject = () => UndeadMegaSword2 } },
           { "UndeadMegaSwordAngry", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadMegaSwordAngry = 
                   JsonObjectParser<AIAbility>.Parse("UndeadMegaSwordAngry", value, errorInfo),
                GetObject = () => UndeadMegaSwordAngry } },
           { "UndeadOmegaArrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadOmegaArrow = 
                   JsonObjectParser<AIAbility>.Parse("UndeadOmegaArrow", value, errorInfo),
                GetObject = () => UndeadOmegaArrow } },
           { "UndeadPhysicalShield", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadPhysicalShield = 
                   JsonObjectParser<AIAbility>.Parse("UndeadPhysicalShield", value, errorInfo),
                GetObject = () => UndeadPhysicalShield } },
           { "UndeadSelfDestruct", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadSelfDestruct = 
                   JsonObjectParser<AIAbility>.Parse("UndeadSelfDestruct", value, errorInfo),
                GetObject = () => UndeadSelfDestruct } },
           { "UndeadSword1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadSword1 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadSword1", value, errorInfo),
                GetObject = () => UndeadSword1 } },
           { "UndeadSword1B", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadSword1B = 
                   JsonObjectParser<AIAbility>.Parse("UndeadSword1B", value, errorInfo),
                GetObject = () => UndeadSword1B } },
           { "UndeadSword2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadSword2 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadSword2", value, errorInfo),
                GetObject = () => UndeadSword2 } },
           { "UndeadSwordAngry", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadSwordAngry = 
                   JsonObjectParser<AIAbility>.Parse("UndeadSwordAngry", value, errorInfo),
                GetObject = () => UndeadSwordAngry } },
           { "UrsulaFireball1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaFireball1 = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaFireball1", value, errorInfo),
                GetObject = () => UrsulaFireball1 } },
           { "UrsulaFireball1B", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaFireball1B = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaFireball1B", value, errorInfo),
                GetObject = () => UrsulaFireball1B } },
           { "UrsulaFireball2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaFireball2 = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaFireball2", value, errorInfo),
                GetObject = () => UrsulaFireball2 } },
           { "UrsulaIceball1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaIceball1 = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaIceball1", value, errorInfo),
                GetObject = () => UrsulaIceball1 } },
           { "UrsulaIceball1B", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaIceball1B = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaIceball1B", value, errorInfo),
                GetObject = () => UrsulaIceball1B } },
           { "UrsulaIceball2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaIceball2 = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaIceball2", value, errorInfo),
                GetObject = () => UrsulaIceball2 } },
           { "UrsulaRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaRage = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaRage", value, errorInfo),
                GetObject = () => UrsulaRage } },
           { "UrsulaSummon", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaSummon = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaSummon", value, errorInfo),
                GetObject = () => UrsulaSummon } },
           { "WatcherAcidball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WatcherAcidball = 
                   JsonObjectParser<AIAbility>.Parse("WatcherAcidball", value, errorInfo),
                GetObject = () => WatcherAcidball } },
           { "WatcherFireball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WatcherFireball = 
                   JsonObjectParser<AIAbility>.Parse("WatcherFireball", value, errorInfo),
                GetObject = () => WatcherFireball } },
           { "WatcherSlap", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WatcherSlap = 
                   JsonObjectParser<AIAbility>.Parse("WatcherSlap", value, errorInfo),
                GetObject = () => WatcherSlap } },
           { "WebStick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WebStick = 
                   JsonObjectParser<AIAbility>.Parse("WebStick", value, errorInfo),
                GetObject = () => WebStick } },
           { "Werewolf_Summon_Opener", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Werewolf_Summon_Opener = 
                   JsonObjectParser<AIAbility>.Parse("Werewolf_Summon_Opener", value, errorInfo),
                GetObject = () => Werewolf_Summon_Opener } },
           { "Werewolf_Summon_Rage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Werewolf_Summon_Rage = 
                   JsonObjectParser<AIAbility>.Parse("Werewolf_Summon_Rage", value, errorInfo),
                GetObject = () => Werewolf_Summon_Rage } },
           { "WerewolfArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WerewolfArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("WerewolfArrow1", value, errorInfo),
                GetObject = () => WerewolfArrow1 } },
           { "WerewolfArrow2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WerewolfArrow2 = 
                   JsonObjectParser<AIAbility>.Parse("WerewolfArrow2", value, errorInfo),
                GetObject = () => WerewolfArrow2 } },
           { "WerewolfOmegaArrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WerewolfOmegaArrow = 
                   JsonObjectParser<AIAbility>.Parse("WerewolfOmegaArrow", value, errorInfo),
                GetObject = () => WerewolfOmegaArrow } },
           { "WerewolfSword1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WerewolfSword1 = 
                   JsonObjectParser<AIAbility>.Parse("WerewolfSword1", value, errorInfo),
                GetObject = () => WerewolfSword1 } },
           { "WerewolfSword2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WerewolfSword2 = 
                   JsonObjectParser<AIAbility>.Parse("WerewolfSword2", value, errorInfo),
                GetObject = () => WerewolfSword2 } },
           { "WerewolfSwordStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WerewolfSwordStun = 
                   JsonObjectParser<AIAbility>.Parse("WerewolfSwordStun", value, errorInfo),
                GetObject = () => WerewolfSwordStun } },
           { "WorgBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WorgBite = 
                   JsonObjectParser<AIAbility>.Parse("WorgBite", value, errorInfo),
                GetObject = () => WorgBite } },
           { "WorghestDebuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WorghestDebuff = 
                   JsonObjectParser<AIAbility>.Parse("WorghestDebuff", value, errorInfo),
                GetObject = () => WorghestDebuff } },
           { "WorgOmegaBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WorgOmegaBite = 
                   JsonObjectParser<AIAbility>.Parse("WorgOmegaBite", value, errorInfo),
                GetObject = () => WorgOmegaBite } },
           { "WormBite1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WormBite1 = 
                   JsonObjectParser<AIAbility>.Parse("WormBite1", value, errorInfo),
                GetObject = () => WormBite1 } },
           { "WormBossAcidBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WormBossAcidBurst = 
                   JsonObjectParser<AIAbility>.Parse("WormBossAcidBurst", value, errorInfo),
                GetObject = () => WormBossAcidBurst } },
           { "WormBossSpit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WormBossSpit = 
                   JsonObjectParser<AIAbility>.Parse("WormBossSpit", value, errorInfo),
                GetObject = () => WormBossSpit } },
           { "WormInfect1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WormInfect1 = 
                   JsonObjectParser<AIAbility>.Parse("WormInfect1", value, errorInfo),
                GetObject = () => WormInfect1 } },
           { "WormShove1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WormShove1 = 
                   JsonObjectParser<AIAbility>.Parse("WormShove1", value, errorInfo),
                GetObject = () => WormShove1 } },
           { "WormSpit1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WormSpit1 = 
                   JsonObjectParser<AIAbility>.Parse("WormSpit1", value, errorInfo),
                GetObject = () => WormSpit1 } },
           { "WormSpit2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WormSpit2 = 
                   JsonObjectParser<AIAbility>.Parse("WormSpit2", value, errorInfo),
                GetObject = () => WormSpit2 } },
           { "YetiBarrage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiBarrage = 
                   JsonObjectParser<AIAbility>.Parse("YetiBarrage", value, errorInfo),
                GetObject = () => YetiBarrage } },
           { "YetiBoulderThrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiBoulderThrow = 
                   JsonObjectParser<AIAbility>.Parse("YetiBoulderThrow", value, errorInfo),
                GetObject = () => YetiBoulderThrow } },
           { "YetiColdOrb", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiColdOrb = 
                   JsonObjectParser<AIAbility>.Parse("YetiColdOrb", value, errorInfo),
                GetObject = () => YetiColdOrb } },
           { "YetiDebuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiDebuff = 
                   JsonObjectParser<AIAbility>.Parse("YetiDebuff", value, errorInfo),
                GetObject = () => YetiDebuff } },
           { "YetiEncase", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiEncase = 
                   JsonObjectParser<AIAbility>.Parse("YetiEncase", value, errorInfo),
                GetObject = () => YetiEncase } },
           { "YetiFlingAway", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiFlingAway = 
                   JsonObjectParser<AIAbility>.Parse("YetiFlingAway", value, errorInfo),
                GetObject = () => YetiFlingAway } },
           { "YetiFrostRing", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiFrostRing = 
                   JsonObjectParser<AIAbility>.Parse("YetiFrostRing", value, errorInfo),
                GetObject = () => YetiFrostRing } },
           { "YetiIceBallThrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiIceBallThrow = 
                   JsonObjectParser<AIAbility>.Parse("YetiIceBallThrow", value, errorInfo),
                GetObject = () => YetiIceBallThrow } },
           { "YetiIceSpear", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiIceSpear = 
                   JsonObjectParser<AIAbility>.Parse("YetiIceSpear", value, errorInfo),
                GetObject = () => YetiIceSpear } },
           { "YetiPunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiPunch = 
                   JsonObjectParser<AIAbility>.Parse("YetiPunch", value, errorInfo),
                GetObject = () => YetiPunch } },
           { "YetiRoarStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiRoarStun = 
                   JsonObjectParser<AIAbility>.Parse("YetiRoarStun", value, errorInfo),
                GetObject = () => YetiRoarStun } },
           { "ZombieBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ZombieBite = 
                   JsonObjectParser<AIAbility>.Parse("ZombieBite", value, errorInfo),
                GetObject = () => ZombieBite } },
           { "ZombiePunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ZombiePunch = 
                   JsonObjectParser<AIAbility>.Parse("ZombiePunch", value, errorInfo),
                GetObject = () => ZombiePunch } },
        }; } }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "AI Abilities"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddObject(AnimalBite, data, ref offset, BaseOffset, 0 * 4, StoredObjectTable);
            AddObject(AnimalClaw, data, ref offset, BaseOffset, 1 * 4, StoredObjectTable);
            AddObject(AnimalOmegaBite, data, ref offset, BaseOffset, 2 * 4, StoredObjectTable);
            AddObject(AnimalOmegaBite2, data, ref offset, BaseOffset, 3 * 4, StoredObjectTable);
            AddObject(AnimalHoofFrontKick, data, ref offset, BaseOffset, 4 * 4, StoredObjectTable);
            AddObject(AnimalHoofRageKick, data, ref offset, BaseOffset, 5 * 4, StoredObjectTable);
            AddObject(AnimalHoofRageKick2, data, ref offset, BaseOffset, 6 * 4, StoredObjectTable);
            AddObject(AnimalHoofFieryFrontKick, data, ref offset, BaseOffset, 7 * 4, StoredObjectTable);
            AddObject(AnimalHoofFieryFrontKick2, data, ref offset, BaseOffset, 8 * 4, StoredObjectTable);
            AddObject(ElectricPigHitAndRun, data, ref offset, BaseOffset, 9 * 4, StoredObjectTable);
            AddObject(ElectricPigStun, data, ref offset, BaseOffset, 10 * 4, StoredObjectTable);
            AddObject(ElectricPigAoEStun, data, ref offset, BaseOffset, 11 * 4, StoredObjectTable);
            AddObject(LamiaMindControl, data, ref offset, BaseOffset, 12 * 4, StoredObjectTable);
            AddObject(LamiaRage, data, ref offset, BaseOffset, 13 * 4, StoredObjectTable);
            AddObject(SlimeKick, data, ref offset, BaseOffset, 14 * 4, StoredObjectTable);
            AddObject(SlimeKickB, data, ref offset, BaseOffset, 15 * 4, StoredObjectTable);
            AddObject(SlimeBite, data, ref offset, BaseOffset, 16 * 4, StoredObjectTable);
            AddObject(SlimeBiteB, data, ref offset, BaseOffset, 17 * 4, StoredObjectTable);
            AddObject(Slime_SummonSlime, data, ref offset, BaseOffset, 18 * 4, StoredObjectTable);
            AddObject(SlimeSpit, data, ref offset, BaseOffset, 19 * 4, StoredObjectTable);
            AddObject(SlimeSuperSpit, data, ref offset, BaseOffset, 20 * 4, StoredObjectTable);
            AddObject(IceSlimeKick, data, ref offset, BaseOffset, 21 * 4, StoredObjectTable);
            AddObject(IceSlimeKickB, data, ref offset, BaseOffset, 22 * 4, StoredObjectTable);
            AddObject(IceSlimeBite, data, ref offset, BaseOffset, 23 * 4, StoredObjectTable);
            AddObject(IceSlimeBiteB, data, ref offset, BaseOffset, 24 * 4, StoredObjectTable);
            AddObject(BossSlimeKick, data, ref offset, BaseOffset, 25 * 4, StoredObjectTable);
            AddObject(BossSlimeKickB, data, ref offset, BaseOffset, 26 * 4, StoredObjectTable);
            AddObject(BossSlime_SummonSlime1, data, ref offset, BaseOffset, 27 * 4, StoredObjectTable);
            AddObject(BossSlimeKick2, data, ref offset, BaseOffset, 28 * 4, StoredObjectTable);
            AddObject(BossSlimeKick2B, data, ref offset, BaseOffset, 29 * 4, StoredObjectTable);
            AddObject(BossSlime_SummonSlime4Elite, data, ref offset, BaseOffset, 30 * 4, StoredObjectTable);
            AddObject(AnimalHeal, data, ref offset, BaseOffset, 31 * 4, StoredObjectTable);
            AddObject(AnimalHeal2, data, ref offset, BaseOffset, 32 * 4, StoredObjectTable);
            AddObject(AnimalHeal3, data, ref offset, BaseOffset, 33 * 4, StoredObjectTable);
            AddObject(IceCockPeck, data, ref offset, BaseOffset, 34 * 4, StoredObjectTable);
            AddObject(IceCockFreeze, data, ref offset, BaseOffset, 35 * 4, StoredObjectTable);
            AddObject(NightmareHoof, data, ref offset, BaseOffset, 36 * 4, StoredObjectTable);
            AddObject(NightmareDarknessBomb, data, ref offset, BaseOffset, 37 * 4, StoredObjectTable);
            AddObject(CiervosNightmareHoof, data, ref offset, BaseOffset, 38 * 4, StoredObjectTable);
            AddObject(CiervosDarknessBomb, data, ref offset, BaseOffset, 39 * 4, StoredObjectTable);
            AddObject(Myconian_Bash, data, ref offset, BaseOffset, 40 * 4, StoredObjectTable);
            AddObject(Myconian_Mindspores, data, ref offset, BaseOffset, 41 * 4, StoredObjectTable);
            AddObject(Myconian_Drain, data, ref offset, BaseOffset, 42 * 4, StoredObjectTable);
            AddObject(Myconian_BossBash, data, ref offset, BaseOffset, 43 * 4, StoredObjectTable);
            AddObject(Myconian_Mindspores_Permanent, data, ref offset, BaseOffset, 44 * 4, StoredObjectTable);
            AddObject(Myconian_TidalCurse, data, ref offset, BaseOffset, 45 * 4, StoredObjectTable);
            AddObject(Myconian_Shock, data, ref offset, BaseOffset, 46 * 4, StoredObjectTable);
            AddObject(AlienDog_Punch, data, ref offset, BaseOffset, 47 * 4, StoredObjectTable);
            AddObject(AlienDog_Punch2, data, ref offset, BaseOffset, 48 * 4, StoredObjectTable);
            AddObject(AlienDog_RagePunch, data, ref offset, BaseOffset, 49 * 4, StoredObjectTable);
            AddObject(MushroomMonster_Bite, data, ref offset, BaseOffset, 50 * 4, StoredObjectTable);
            AddObject(MushroomMonster_Spit1, data, ref offset, BaseOffset, 51 * 4, StoredObjectTable);
            AddObject(MushroomMonster_Spit2, data, ref offset, BaseOffset, 52 * 4, StoredObjectTable);
            AddObject(MushroomMonster_SummonMushroomSpawn1, data, ref offset, BaseOffset, 53 * 4, StoredObjectTable);
            AddObject(MushroomMonster_SummonMushroomSpawn2, data, ref offset, BaseOffset, 54 * 4, StoredObjectTable);
            AddObject(MushroomMonster_SummonMushroomSpawn3, data, ref offset, BaseOffset, 55 * 4, StoredObjectTable);
            AddObject(MushroomMonster_SpawnSpit1, data, ref offset, BaseOffset, 56 * 4, StoredObjectTable);
            AddObject(MushroomMonster_SpawnSpit2, data, ref offset, BaseOffset, 57 * 4, StoredObjectTable);
            AddObject(MushroomMonster_SpawnSuperSpit1, data, ref offset, BaseOffset, 58 * 4, StoredObjectTable);
            AddObject(MushroomMonster_SpawnSuperSpit2, data, ref offset, BaseOffset, 59 * 4, StoredObjectTable);
            AddObject(MaronesaStomp, data, ref offset, BaseOffset, 60 * 4, StoredObjectTable);
            AddObject(MaronesaInfect, data, ref offset, BaseOffset, 61 * 4, StoredObjectTable);
            AddObject(WormBite1, data, ref offset, BaseOffset, 62 * 4, StoredObjectTable);
            AddObject(WormSpit1, data, ref offset, BaseOffset, 63 * 4, StoredObjectTable);
            AddObject(WormShove1, data, ref offset, BaseOffset, 64 * 4, StoredObjectTable);
            AddObject(WormInfect1, data, ref offset, BaseOffset, 65 * 4, StoredObjectTable);
            AddObject(WormSpit2, data, ref offset, BaseOffset, 66 * 4, StoredObjectTable);
            AddObject(WormBossSpit, data, ref offset, BaseOffset, 67 * 4, StoredObjectTable);
            AddObject(WormBossAcidBurst, data, ref offset, BaseOffset, 68 * 4, StoredObjectTable);
            AddObject(GnasherBite, data, ref offset, BaseOffset, 69 * 4, StoredObjectTable);
            AddObject(GnasherRend, data, ref offset, BaseOffset, 70 * 4, StoredObjectTable);
            AddObject(Dinoslash, data, ref offset, BaseOffset, 71 * 4, StoredObjectTable);
            AddObject(Dinoslash2, data, ref offset, BaseOffset, 72 * 4, StoredObjectTable);
            AddObject(Dinobite, data, ref offset, BaseOffset, 73 * 4, StoredObjectTable);
            AddObject(Dinobite2, data, ref offset, BaseOffset, 74 * 4, StoredObjectTable);
            AddObject(Dinowhap, data, ref offset, BaseOffset, 75 * 4, StoredObjectTable);
            AddObject(Peck, data, ref offset, BaseOffset, 76 * 4, StoredObjectTable);
            AddObject(AnimalFlee, data, ref offset, BaseOffset, 77 * 4, StoredObjectTable);
            AddObject(HippogriffPeck, data, ref offset, BaseOffset, 78 * 4, StoredObjectTable);
            AddObject(HippogriffSlashes, data, ref offset, BaseOffset, 79 * 4, StoredObjectTable);
            AddObject(HippogriffBossSlashes, data, ref offset, BaseOffset, 80 * 4, StoredObjectTable);
            AddObject(RatBite, data, ref offset, BaseOffset, 81 * 4, StoredObjectTable);
            AddObject(RatClaw, data, ref offset, BaseOffset, 82 * 4, StoredObjectTable);
            AddObject(FireRatBite, data, ref offset, BaseOffset, 83 * 4, StoredObjectTable);
            AddObject(FireRatClaw, data, ref offset, BaseOffset, 84 * 4, StoredObjectTable);
            AddObject(GoblinZapBall, data, ref offset, BaseOffset, 85 * 4, StoredObjectTable);
            AddObject(GoblinHateZapBall, data, ref offset, BaseOffset, 86 * 4, StoredObjectTable);
            AddObject(GoblinHateZapBall2, data, ref offset, BaseOffset, 87 * 4, StoredObjectTable);
            AddObject(RhinoHorn, data, ref offset, BaseOffset, 88 * 4, StoredObjectTable);
            AddObject(RhinoRage, data, ref offset, BaseOffset, 89 * 4, StoredObjectTable);
            AddObject(RhinoFireball, data, ref offset, BaseOffset, 90 * 4, StoredObjectTable);
            AddObject(RhinoBossRage, data, ref offset, BaseOffset, 91 * 4, StoredObjectTable);
            AddObject(YetiPunch, data, ref offset, BaseOffset, 92 * 4, StoredObjectTable);
            AddObject(YetiEncase, data, ref offset, BaseOffset, 93 * 4, StoredObjectTable);
            AddObject(YetiDebuff, data, ref offset, BaseOffset, 94 * 4, StoredObjectTable);
            AddObject(YetiBoulderThrow, data, ref offset, BaseOffset, 95 * 4, StoredObjectTable);
            AddObject(YetiBarrage, data, ref offset, BaseOffset, 96 * 4, StoredObjectTable);
            AddObject(YetiRoarStun, data, ref offset, BaseOffset, 97 * 4, StoredObjectTable);
            AddObject(YetiFlingAway, data, ref offset, BaseOffset, 98 * 4, StoredObjectTable);
            AddObject(YetiIceBallThrow, data, ref offset, BaseOffset, 99 * 4, StoredObjectTable);
            AddObject(YetiIceSpear, data, ref offset, BaseOffset, 100 * 4, StoredObjectTable);
            AddObject(YetiColdOrb, data, ref offset, BaseOffset, 101 * 4, StoredObjectTable);
            AddObject(YetiFrostRing, data, ref offset, BaseOffset, 102 * 4, StoredObjectTable);
            AddObject(WorgBite, data, ref offset, BaseOffset, 103 * 4, StoredObjectTable);
            AddObject(WorgOmegaBite, data, ref offset, BaseOffset, 104 * 4, StoredObjectTable);
            AddObject(BearBite, data, ref offset, BaseOffset, 105 * 4, StoredObjectTable);
            AddObject(BearCrush, data, ref offset, BaseOffset, 106 * 4, StoredObjectTable);
            AddObject(BrainBite, data, ref offset, BaseOffset, 107 * 4, StoredObjectTable);
            AddObject(BrainDrain, data, ref offset, BaseOffset, 108 * 4, StoredObjectTable);
            AddObject(BrainDrain2, data, ref offset, BaseOffset, 109 * 4, StoredObjectTable);
            AddObject(BigCatClaw, data, ref offset, BaseOffset, 110 * 4, StoredObjectTable);
            AddObject(BigCatPounce, data, ref offset, BaseOffset, 111 * 4, StoredObjectTable);
            AddObject(BigCatRagePounce, data, ref offset, BaseOffset, 112 * 4, StoredObjectTable);
            AddObject(BigCatDebuff, data, ref offset, BaseOffset, 113 * 4, StoredObjectTable);
            AddObject(SpiderBite, data, ref offset, BaseOffset, 114 * 4, StoredObjectTable);
            AddObject(SpiderFireball, data, ref offset, BaseOffset, 115 * 4, StoredObjectTable);
            AddObject(SpiderBossFreePin, data, ref offset, BaseOffset, 116 * 4, StoredObjectTable);
            AddObject(SpiderKill2, data, ref offset, BaseOffset, 117 * 4, StoredObjectTable);
            AddObject(SpiderInject, data, ref offset, BaseOffset, 118 * 4, StoredObjectTable);
            AddObject(SpiderKill, data, ref offset, BaseOffset, 119 * 4, StoredObjectTable);
            AddObject(AcidBall1, data, ref offset, BaseOffset, 120 * 4, StoredObjectTable);
            AddObject(SpiderPin, data, ref offset, BaseOffset, 121 * 4, StoredObjectTable);
            AddObject(SpiderKill3, data, ref offset, BaseOffset, 122 * 4, StoredObjectTable);
            AddObject(AcidBall2, data, ref offset, BaseOffset, 123 * 4, StoredObjectTable);
            AddObject(AcidSpew1, data, ref offset, BaseOffset, 124 * 4, StoredObjectTable);
            AddObject(AcidExplosion1, data, ref offset, BaseOffset, 125 * 4, StoredObjectTable);
            AddObject(AcidExplosion2, data, ref offset, BaseOffset, 126 * 4, StoredObjectTable);
            AddObject(SpiderIncubate, data, ref offset, BaseOffset, 127 * 4, StoredObjectTable);
            AddObject(MantisClaw, data, ref offset, BaseOffset, 128 * 4, StoredObjectTable);
            AddObject(MantisRage, data, ref offset, BaseOffset, 129 * 4, StoredObjectTable);
            AddObject(MantisAcidBurst, data, ref offset, BaseOffset, 130 * 4, StoredObjectTable);
            AddObject(SherzatClaw, data, ref offset, BaseOffset, 131 * 4, StoredObjectTable);
            AddObject(SherzatAcidSpit, data, ref offset, BaseOffset, 132 * 4, StoredObjectTable);
            AddObject(SherzatDisintegrate, data, ref offset, BaseOffset, 133 * 4, StoredObjectTable);
            AddObject(MantisSwipe, data, ref offset, BaseOffset, 134 * 4, StoredObjectTable);
            AddObject(MantisBlast, data, ref offset, BaseOffset, 135 * 4, StoredObjectTable);
            AddObject(SnailStrike, data, ref offset, BaseOffset, 136 * 4, StoredObjectTable);
            AddObject(SnailRage, data, ref offset, BaseOffset, 137 * 4, StoredObjectTable);
            AddObject(SnailRageB, data, ref offset, BaseOffset, 138 * 4, StoredObjectTable);
            AddObject(SnailRageC, data, ref offset, BaseOffset, 139 * 4, StoredObjectTable);
            AddObject(HookClaw, data, ref offset, BaseOffset, 140 * 4, StoredObjectTable);
            AddObject(HookAcid, data, ref offset, BaseOffset, 141 * 4, StoredObjectTable);
            AddObject(HookRage, data, ref offset, BaseOffset, 142 * 4, StoredObjectTable);
            AddObject(UndeadSword1, data, ref offset, BaseOffset, 143 * 4, StoredObjectTable);
            AddObject(UndeadSword2, data, ref offset, BaseOffset, 144 * 4, StoredObjectTable);
            AddObject(UndeadSwordAngry, data, ref offset, BaseOffset, 145 * 4, StoredObjectTable);
            AddObject(UndeadMegaSword1, data, ref offset, BaseOffset, 146 * 4, StoredObjectTable);
            AddObject(UndeadMegaSword2, data, ref offset, BaseOffset, 147 * 4, StoredObjectTable);
            AddObject(UndeadMegaSwordAngry, data, ref offset, BaseOffset, 148 * 4, StoredObjectTable);
            AddObject(UndeadLightningSmite, data, ref offset, BaseOffset, 149 * 4, StoredObjectTable);
            AddObject(UndeadPhysicalShield, data, ref offset, BaseOffset, 150 * 4, StoredObjectTable);
            AddObject(UndeadSword1B, data, ref offset, BaseOffset, 151 * 4, StoredObjectTable);
            AddObject(UndeadFireballA, data, ref offset, BaseOffset, 152 * 4, StoredObjectTable);
            AddObject(UndeadFireballB, data, ref offset, BaseOffset, 153 * 4, StoredObjectTable);
            AddObject(UndeadFireballB2, data, ref offset, BaseOffset, 154 * 4, StoredObjectTable);
            AddObject(UndeadIceBall1, data, ref offset, BaseOffset, 155 * 4, StoredObjectTable);
            AddObject(UndeadFreezeBall, data, ref offset, BaseOffset, 156 * 4, StoredObjectTable);
            AddObject(UndeadFireballLongA, data, ref offset, BaseOffset, 157 * 4, StoredObjectTable);
            AddObject(UndeadFireballLongB, data, ref offset, BaseOffset, 158 * 4, StoredObjectTable);
            AddObject(UndeadFireballLongB2, data, ref offset, BaseOffset, 159 * 4, StoredObjectTable);
            AddObject(KhyrulekCurseBall, data, ref offset, BaseOffset, 160 * 4, StoredObjectTable);
            AddObject(UrsulaFireball1, data, ref offset, BaseOffset, 161 * 4, StoredObjectTable);
            AddObject(UrsulaFireball1B, data, ref offset, BaseOffset, 162 * 4, StoredObjectTable);
            AddObject(UrsulaFireball2, data, ref offset, BaseOffset, 163 * 4, StoredObjectTable);
            AddObject(UrsulaIceball1, data, ref offset, BaseOffset, 164 * 4, StoredObjectTable);
            AddObject(UrsulaIceball1B, data, ref offset, BaseOffset, 165 * 4, StoredObjectTable);
            AddObject(UrsulaIceball2, data, ref offset, BaseOffset, 166 * 4, StoredObjectTable);
            AddObject(UrsulaSummon, data, ref offset, BaseOffset, 167 * 4, StoredObjectTable);
            AddObject(UrsulaRage, data, ref offset, BaseOffset, 168 * 4, StoredObjectTable);
            AddObject(UndeadFireballA2, data, ref offset, BaseOffset, 169 * 4, StoredObjectTable);
            AddObject(BigHeadCurseball, data, ref offset, BaseOffset, 170 * 4, StoredObjectTable);
            AddObject(UndeadArrow1, data, ref offset, BaseOffset, 171 * 4, StoredObjectTable);
            AddObject(UndeadArrow2, data, ref offset, BaseOffset, 172 * 4, StoredObjectTable);
            AddObject(UndeadOmegaArrow, data, ref offset, BaseOffset, 173 * 4, StoredObjectTable);
            AddObject(PetUndeadArrow1, data, ref offset, BaseOffset, 174 * 4, StoredObjectTable);
            AddObject(PetUndeadArrow2, data, ref offset, BaseOffset, 175 * 4, StoredObjectTable);
            AddObject(PetUndeadOmegaArrow, data, ref offset, BaseOffset, 176 * 4, StoredObjectTable);
            AddObject(UndeadGrappleArrow1, data, ref offset, BaseOffset, 177 * 4, StoredObjectTable);
            AddObject(UndeadSelfDestruct, data, ref offset, BaseOffset, 178 * 4, StoredObjectTable);
            AddObject(UndeadDarknessBall, data, ref offset, BaseOffset, 179 * 4, StoredObjectTable);
            AddObject(UndeadBoneWhirlwind, data, ref offset, BaseOffset, 180 * 4, StoredObjectTable);
            AddObject(PetUndeadSword1, data, ref offset, BaseOffset, 181 * 4, StoredObjectTable);
            AddObject(PetUndeadSword2, data, ref offset, BaseOffset, 182 * 4, StoredObjectTable);
            AddObject(PetUndeadSwordAngry, data, ref offset, BaseOffset, 183 * 4, StoredObjectTable);
            AddObject(PetUndeadFireballA, data, ref offset, BaseOffset, 184 * 4, StoredObjectTable);
            AddObject(PetUndeadFireballB, data, ref offset, BaseOffset, 185 * 4, StoredObjectTable);
            AddObject(PetUndeadDefensiveBurst, data, ref offset, BaseOffset, 186 * 4, StoredObjectTable);
            AddObject(PetUndeadPunch1, data, ref offset, BaseOffset, 187 * 4, StoredObjectTable);
            AddObject(ZombiePunch, data, ref offset, BaseOffset, 188 * 4, StoredObjectTable);
            AddObject(ZombieBite, data, ref offset, BaseOffset, 189 * 4, StoredObjectTable);
            AddObject(GoblinSpear1, data, ref offset, BaseOffset, 190 * 4, StoredObjectTable);
            AddObject(GoblinSpear2, data, ref offset, BaseOffset, 191 * 4, StoredObjectTable);
            AddObject(GoblinRageSpear1, data, ref offset, BaseOffset, 192 * 4, StoredObjectTable);
            AddObject(GoblinRageSpear2, data, ref offset, BaseOffset, 193 * 4, StoredObjectTable);
            AddObject(GoblinHeal1, data, ref offset, BaseOffset, 194 * 4, StoredObjectTable);
            AddObject(GoblinHeal2, data, ref offset, BaseOffset, 195 * 4, StoredObjectTable);
            AddObject(GoblinPunch, data, ref offset, BaseOffset, 196 * 4, StoredObjectTable);
            AddObject(GoblinArrow1, data, ref offset, BaseOffset, 197 * 4, StoredObjectTable);
            AddObject(GoblinArrow2, data, ref offset, BaseOffset, 198 * 4, StoredObjectTable);
            AddObject(GoblinRageArrow1, data, ref offset, BaseOffset, 199 * 4, StoredObjectTable);
            AddObject(GoblinRageArrow2, data, ref offset, BaseOffset, 200 * 4, StoredObjectTable);
            AddObject(GoblinSpreadZapBall, data, ref offset, BaseOffset, 201 * 4, StoredObjectTable);
            AddObject(GoblinBossLightning, data, ref offset, BaseOffset, 202 * 4, StoredObjectTable);
            AddObject(GoblinArmorBuff, data, ref offset, BaseOffset, 203 * 4, StoredObjectTable);
            AddObject(MummySlamA, data, ref offset, BaseOffset, 204 * 4, StoredObjectTable);
            AddObject(MummySlamB, data, ref offset, BaseOffset, 205 * 4, StoredObjectTable);
            AddObject(MummySlamCombo, data, ref offset, BaseOffset, 206 * 4, StoredObjectTable);
            AddObject(MummyWrapA, data, ref offset, BaseOffset, 207 * 4, StoredObjectTable);
            AddObject(MummyWrapB, data, ref offset, BaseOffset, 208 * 4, StoredObjectTable);
            AddObject(MummyWrapRage, data, ref offset, BaseOffset, 209 * 4, StoredObjectTable);
            AddObject(BarutiWrapA, data, ref offset, BaseOffset, 210 * 4, StoredObjectTable);
            AddObject(BarutiWrapB, data, ref offset, BaseOffset, 211 * 4, StoredObjectTable);
            AddObject(BarutiWrapRage, data, ref offset, BaseOffset, 212 * 4, StoredObjectTable);
            AddObject(FireWallAttack1, data, ref offset, BaseOffset, 213 * 4, StoredObjectTable);
            AddObject(FireWallDotAttack1, data, ref offset, BaseOffset, 214 * 4, StoredObjectTable);
            AddObject(FireSnakeExplosion1, data, ref offset, BaseOffset, 215 * 4, StoredObjectTable);
            AddObject(FireTrapAttack1, data, ref offset, BaseOffset, 216 * 4, StoredObjectTable);
            AddObject(HealingAura1, data, ref offset, BaseOffset, 217 * 4, StoredObjectTable);
            AddObject(HealingAura2, data, ref offset, BaseOffset, 218 * 4, StoredObjectTable);
            AddObject(HealingAura3, data, ref offset, BaseOffset, 219 * 4, StoredObjectTable);
            AddObject(HealingAura4, data, ref offset, BaseOffset, 220 * 4, StoredObjectTable);
            AddObject(DruidHealingSanctuaryHeal, data, ref offset, BaseOffset, 221 * 4, StoredObjectTable);
            AddObject(AcidAuraBall1, data, ref offset, BaseOffset, 222 * 4, StoredObjectTable);
            AddObject(AcidAuraBall2, data, ref offset, BaseOffset, 223 * 4, StoredObjectTable);
            AddObject(AcidAuraBall3, data, ref offset, BaseOffset, 224 * 4, StoredObjectTable);
            AddObject(AcidAuraBall4, data, ref offset, BaseOffset, 225 * 4, StoredObjectTable);
            AddObject(ElectricityAura1, data, ref offset, BaseOffset, 226 * 4, StoredObjectTable);
            AddObject(ElectricityAuraBolt1, data, ref offset, BaseOffset, 227 * 4, StoredObjectTable);
            AddObject(ReboundAura1, data, ref offset, BaseOffset, 228 * 4, StoredObjectTable);
            AddObject(ColdAuraBurst, data, ref offset, BaseOffset, 229 * 4, StoredObjectTable);
            AddObject(WebStick, data, ref offset, BaseOffset, 230 * 4, StoredObjectTable);
            AddObject(IcySlam, data, ref offset, BaseOffset, 231 * 4, StoredObjectTable);
            AddObject(IcyCocoon, data, ref offset, BaseOffset, 232 * 4, StoredObjectTable);
            AddObject(IcyCocoon2, data, ref offset, BaseOffset, 233 * 4, StoredObjectTable);
            AddObject(ElementalSlam, data, ref offset, BaseOffset, 234 * 4, StoredObjectTable);
            AddObject(ElementalBees, data, ref offset, BaseOffset, 235 * 4, StoredObjectTable);
            AddObject(ElementalBees2, data, ref offset, BaseOffset, 236 * 4, StoredObjectTable);
            AddObject(FaeLightningSmite, data, ref offset, BaseOffset, 237 * 4, StoredObjectTable);
            AddObject(TotalHorrorAttack, data, ref offset, BaseOffset, 238 * 4, StoredObjectTable);
            AddObject(TotalHorrorStretch, data, ref offset, BaseOffset, 239 * 4, StoredObjectTable);
            AddObject(TotalHorrorHeal, data, ref offset, BaseOffset, 240 * 4, StoredObjectTable);
            AddObject(TotalHorrorHeal2, data, ref offset, BaseOffset, 241 * 4, StoredObjectTable);
            AddObject(SheepBomb1, data, ref offset, BaseOffset, 242 * 4, StoredObjectTable);
            AddObject(SlugPoisonBite, data, ref offset, BaseOffset, 243 * 4, StoredObjectTable);
            AddObject(SlugPoisonRage, data, ref offset, BaseOffset, 244 * 4, StoredObjectTable);
            AddObject(SlugPoisonBite2, data, ref offset, BaseOffset, 245 * 4, StoredObjectTable);
            AddObject(SlugPoisonRage2, data, ref offset, BaseOffset, 246 * 4, StoredObjectTable);
            AddObject(SlugPoisonBite3, data, ref offset, BaseOffset, 247 * 4, StoredObjectTable);
            AddObject(SlugPoisonRage3, data, ref offset, BaseOffset, 248 * 4, StoredObjectTable);
            AddObject(TornadoJolt1, data, ref offset, BaseOffset, 249 * 4, StoredObjectTable);
            AddObject(TornadoFling, data, ref offset, BaseOffset, 250 * 4, StoredObjectTable);
            AddObject(TornadoToss, data, ref offset, BaseOffset, 251 * 4, StoredObjectTable);
            AddObject(TheFogCurse, data, ref offset, BaseOffset, 252 * 4, StoredObjectTable);
            AddObject(MonsterWerewolfPouncingRake, data, ref offset, BaseOffset, 253 * 4, StoredObjectTable);
            AddObject(MonsterWerewolfPackAttack, data, ref offset, BaseOffset, 254 * 4, StoredObjectTable);
            AddObject(MonsterWerewolfHowl, data, ref offset, BaseOffset, 255 * 4, StoredObjectTable);
            AddObject(Werewolf_Summon_Rage, data, ref offset, BaseOffset, 256 * 4, StoredObjectTable);
            AddObject(Werewolf_Summon_Opener, data, ref offset, BaseOffset, 257 * 4, StoredObjectTable);
            AddObject(BleddynHowl, data, ref offset, BaseOffset, 258 * 4, StoredObjectTable);
            AddObject(OrcSwordSlash, data, ref offset, BaseOffset, 259 * 4, StoredObjectTable);
            AddObject(OrcParry, data, ref offset, BaseOffset, 260 * 4, StoredObjectTable);
            AddObject(OrcFinishingBlow, data, ref offset, BaseOffset, 261 * 4, StoredObjectTable);
            AddObject(OrcHipThrow, data, ref offset, BaseOffset, 262 * 4, StoredObjectTable);
            AddObject(OrcKneeKick, data, ref offset, BaseOffset, 263 * 4, StoredObjectTable);
            AddObject(OrcPunch, data, ref offset, BaseOffset, 264 * 4, StoredObjectTable);
            AddObject(OrcArrow1, data, ref offset, BaseOffset, 265 * 4, StoredObjectTable);
            AddObject(OrcArrow2, data, ref offset, BaseOffset, 266 * 4, StoredObjectTable);
            AddObject(OrcStaffSmash, data, ref offset, BaseOffset, 267 * 4, StoredObjectTable);
            AddObject(OrcFireball, data, ref offset, BaseOffset, 268 * 4, StoredObjectTable);
            AddObject(OrcHeal1, data, ref offset, BaseOffset, 269 * 4, StoredObjectTable);
            AddObject(OrcHeal2, data, ref offset, BaseOffset, 270 * 4, StoredObjectTable);
            AddObject(OrcEvasionBubble, data, ref offset, BaseOffset, 271 * 4, StoredObjectTable);
            AddObject(OrcElectricStun, data, ref offset, BaseOffset, 272 * 4, StoredObjectTable);
            AddObject(OrcFireBolts, data, ref offset, BaseOffset, 273 * 4, StoredObjectTable);
            AddObject(OrcKnockbackBolt, data, ref offset, BaseOffset, 274 * 4, StoredObjectTable);
            AddObject(OrcSummonUrak2, data, ref offset, BaseOffset, 275 * 4, StoredObjectTable);
            AddObject(OrcSwordSlashFire, data, ref offset, BaseOffset, 276 * 4, StoredObjectTable);
            AddObject(OrcParryFire, data, ref offset, BaseOffset, 277 * 4, StoredObjectTable);
            AddObject(OrcFinishingBlowFire, data, ref offset, BaseOffset, 278 * 4, StoredObjectTable);
            AddObject(OrcSummonSigil1, data, ref offset, BaseOffset, 279 * 4, StoredObjectTable);
            AddObject(OrcSpearAttack, data, ref offset, BaseOffset, 280 * 4, StoredObjectTable);
            AddObject(OrcHalberdAttack, data, ref offset, BaseOffset, 281 * 4, StoredObjectTable);
            AddObject(OrcAreaHalberdAttack, data, ref offset, BaseOffset, 282 * 4, StoredObjectTable);
            AddObject(OrcDebuffArrow, data, ref offset, BaseOffset, 283 * 4, StoredObjectTable);
            AddObject(OrcSlice, data, ref offset, BaseOffset, 284 * 4, StoredObjectTable);
            AddObject(OrcVenomstrike1, data, ref offset, BaseOffset, 285 * 4, StoredObjectTable);
            AddObject(OrcVenomstrike0, data, ref offset, BaseOffset, 286 * 4, StoredObjectTable);
            AddObject(OrcLieutenantDebuffTaunt, data, ref offset, BaseOffset, 287 * 4, StoredObjectTable);
            AddObject(OrcAreaHalberdBoss, data, ref offset, BaseOffset, 288 * 4, StoredObjectTable);
            AddObject(OrcDeathsHold, data, ref offset, BaseOffset, 289 * 4, StoredObjectTable);
            AddObject(GazlukPriest1Special, data, ref offset, BaseOffset, 290 * 4, StoredObjectTable);
            AddObject(GazlukPriest2Special, data, ref offset, BaseOffset, 291 * 4, StoredObjectTable);
            AddObject(GazlukPriest3Special, data, ref offset, BaseOffset, 292 * 4, StoredObjectTable);
            AddObject(OrcExtinguishLife, data, ref offset, BaseOffset, 293 * 4, StoredObjectTable);
            AddObject(OrcDarknessBall, data, ref offset, BaseOffset, 294 * 4, StoredObjectTable);
            AddObject(OrcWaveOfDarkness, data, ref offset, BaseOffset, 295 * 4, StoredObjectTable);
            AddObject(EnemyMinigolemPunch, data, ref offset, BaseOffset, 296 * 4, StoredObjectTable);
            AddObject(EnemyMinigolemHeal, data, ref offset, BaseOffset, 297 * 4, StoredObjectTable);
            AddObject(EnemyMinigolemExplode, data, ref offset, BaseOffset, 298 * 4, StoredObjectTable);
            AddObject(MinigolemBombToss, data, ref offset, BaseOffset, 299 * 4, StoredObjectTable);
            AddObject(MinigolemBombToss2, data, ref offset, BaseOffset, 300 * 4, StoredObjectTable);
            AddObject(MinigolemBombToss3, data, ref offset, BaseOffset, 301 * 4, StoredObjectTable);
            AddObject(MinigolemBombToss4, data, ref offset, BaseOffset, 302 * 4, StoredObjectTable);
            AddObject(MinigolemBombToss5, data, ref offset, BaseOffset, 303 * 4, StoredObjectTable);
            AddObject(MinigolemAoEHeal, data, ref offset, BaseOffset, 304 * 4, StoredObjectTable);
            AddObject(MinigolemAoEHeal2, data, ref offset, BaseOffset, 305 * 4, StoredObjectTable);
            AddObject(MinigolemAoEHeal3, data, ref offset, BaseOffset, 306 * 4, StoredObjectTable);
            AddObject(MinigolemAoEHeal4, data, ref offset, BaseOffset, 307 * 4, StoredObjectTable);
            AddObject(MinigolemAoEHeal5, data, ref offset, BaseOffset, 308 * 4, StoredObjectTable);
            AddObject(MinigolemSelfDestruct, data, ref offset, BaseOffset, 309 * 4, StoredObjectTable);
            AddObject(MinigolemSelfDestruct2, data, ref offset, BaseOffset, 310 * 4, StoredObjectTable);
            AddObject(MinigolemSelfDestruct3, data, ref offset, BaseOffset, 311 * 4, StoredObjectTable);
            AddObject(MinigolemSelfDestruct4, data, ref offset, BaseOffset, 312 * 4, StoredObjectTable);
            AddObject(MinigolemSelfDestruct5, data, ref offset, BaseOffset, 313 * 4, StoredObjectTable);
            AddObject(MinigolemAoEPower, data, ref offset, BaseOffset, 314 * 4, StoredObjectTable);
            AddObject(MinigolemAoEPower2, data, ref offset, BaseOffset, 315 * 4, StoredObjectTable);
            AddObject(MinigolemAoEPower3, data, ref offset, BaseOffset, 316 * 4, StoredObjectTable);
            AddObject(MinigolemAoEPower4, data, ref offset, BaseOffset, 317 * 4, StoredObjectTable);
            AddObject(MinigolemAoEPower5, data, ref offset, BaseOffset, 318 * 4, StoredObjectTable);
            AddObject(MinigolemHeal, data, ref offset, BaseOffset, 319 * 4, StoredObjectTable);
            AddObject(MinigolemHeal2, data, ref offset, BaseOffset, 320 * 4, StoredObjectTable);
            AddObject(MinigolemHeal3, data, ref offset, BaseOffset, 321 * 4, StoredObjectTable);
            AddObject(MinigolemHeal4, data, ref offset, BaseOffset, 322 * 4, StoredObjectTable);
            AddObject(MinigolemHeal5, data, ref offset, BaseOffset, 323 * 4, StoredObjectTable);
            AddObject(MinigolemDoomAdmixture, data, ref offset, BaseOffset, 324 * 4, StoredObjectTable);
            AddObject(MinigolemDoomAdmixture2, data, ref offset, BaseOffset, 325 * 4, StoredObjectTable);
            AddObject(MinigolemDoomAdmixture3, data, ref offset, BaseOffset, 326 * 4, StoredObjectTable);
            AddObject(MinigolemDoomAdmixture4, data, ref offset, BaseOffset, 327 * 4, StoredObjectTable);
            AddObject(MinigolemDoomAdmixture5, data, ref offset, BaseOffset, 328 * 4, StoredObjectTable);
            AddObject(MinigolemSelfSacrifice, data, ref offset, BaseOffset, 329 * 4, StoredObjectTable);
            AddObject(MinigolemSelfSacrifice2, data, ref offset, BaseOffset, 330 * 4, StoredObjectTable);
            AddObject(MinigolemSelfSacrifice3, data, ref offset, BaseOffset, 331 * 4, StoredObjectTable);
            AddObject(MinigolemSelfSacrifice4, data, ref offset, BaseOffset, 332 * 4, StoredObjectTable);
            AddObject(MinigolemSelfSacrifice5, data, ref offset, BaseOffset, 333 * 4, StoredObjectTable);
            AddObject(MinigolemPunch, data, ref offset, BaseOffset, 334 * 4, StoredObjectTable);
            AddObject(MinigolemPunch2, data, ref offset, BaseOffset, 335 * 4, StoredObjectTable);
            AddObject(MinigolemPunch3, data, ref offset, BaseOffset, 336 * 4, StoredObjectTable);
            AddObject(MinigolemPunch4, data, ref offset, BaseOffset, 337 * 4, StoredObjectTable);
            AddObject(MinigolemPunch5, data, ref offset, BaseOffset, 338 * 4, StoredObjectTable);
            AddObject(MinigolemHasteConcoction1, data, ref offset, BaseOffset, 339 * 4, StoredObjectTable);
            AddObject(MinigolemHasteConcoction2, data, ref offset, BaseOffset, 340 * 4, StoredObjectTable);
            AddObject(MinigolemHasteConcoction3, data, ref offset, BaseOffset, 341 * 4, StoredObjectTable);
            AddObject(MinigolemFireBalm1, data, ref offset, BaseOffset, 342 * 4, StoredObjectTable);
            AddObject(MinigolemFireBalm2, data, ref offset, BaseOffset, 343 * 4, StoredObjectTable);
            AddObject(MinigolemFireBalm3, data, ref offset, BaseOffset, 344 * 4, StoredObjectTable);
            AddObject(MinigolemFireBalm4, data, ref offset, BaseOffset, 345 * 4, StoredObjectTable);
            AddObject(MinigolemFireBalm5, data, ref offset, BaseOffset, 346 * 4, StoredObjectTable);
            AddObject(MinigolemRageAoEHeal1, data, ref offset, BaseOffset, 347 * 4, StoredObjectTable);
            AddObject(MinigolemRageAoEHeal2, data, ref offset, BaseOffset, 348 * 4, StoredObjectTable);
            AddObject(MinigolemRageAoEHeal3, data, ref offset, BaseOffset, 349 * 4, StoredObjectTable);
            AddObject(MinigolemRageAoEHeal4, data, ref offset, BaseOffset, 350 * 4, StoredObjectTable);
            AddObject(MinigolemRageAoEHeal5, data, ref offset, BaseOffset, 351 * 4, StoredObjectTable);
            AddObject(MinigolemRageAcidToss1, data, ref offset, BaseOffset, 352 * 4, StoredObjectTable);
            AddObject(MinigolemRageAcidToss2, data, ref offset, BaseOffset, 353 * 4, StoredObjectTable);
            AddObject(MinigolemRageAcidToss3, data, ref offset, BaseOffset, 354 * 4, StoredObjectTable);
            AddObject(MinigolemRageAcidToss4, data, ref offset, BaseOffset, 355 * 4, StoredObjectTable);
            AddObject(MinigolemRageAcidToss5, data, ref offset, BaseOffset, 356 * 4, StoredObjectTable);
            AddObject(TrainingGolemPunch, data, ref offset, BaseOffset, 357 * 4, StoredObjectTable);
            AddObject(TrainingGolemStun, data, ref offset, BaseOffset, 358 * 4, StoredObjectTable);
            AddObject(TrainingGolemHeal, data, ref offset, BaseOffset, 359 * 4, StoredObjectTable);
            AddObject(TrainingGolemHealB, data, ref offset, BaseOffset, 360 * 4, StoredObjectTable);
            AddObject(TrainingGolemFireBreath, data, ref offset, BaseOffset, 361 * 4, StoredObjectTable);
            AddObject(TrainingGolemFireBurst, data, ref offset, BaseOffset, 362 * 4, StoredObjectTable);
            AddObject(GrimalkinClaw, data, ref offset, BaseOffset, 363 * 4, StoredObjectTable);
            AddObject(GrimalkinBite, data, ref offset, BaseOffset, 364 * 4, StoredObjectTable);
            AddObject(GrimalkinPuncture, data, ref offset, BaseOffset, 365 * 4, StoredObjectTable);
            AddObject(WerewolfSword1, data, ref offset, BaseOffset, 366 * 4, StoredObjectTable);
            AddObject(WerewolfSword2, data, ref offset, BaseOffset, 367 * 4, StoredObjectTable);
            AddObject(WerewolfSwordStun, data, ref offset, BaseOffset, 368 * 4, StoredObjectTable);
            AddObject(WerewolfArrow1, data, ref offset, BaseOffset, 369 * 4, StoredObjectTable);
            AddObject(WerewolfArrow2, data, ref offset, BaseOffset, 370 * 4, StoredObjectTable);
            AddObject(WerewolfOmegaArrow, data, ref offset, BaseOffset, 371 * 4, StoredObjectTable);
            AddObject(NpcSmash, data, ref offset, BaseOffset, 372 * 4, StoredObjectTable);
            AddObject(NpcDoubleHitCurse, data, ref offset, BaseOffset, 373 * 4, StoredObjectTable);
            AddObject(NpcBlockingStance, data, ref offset, BaseOffset, 374 * 4, StoredObjectTable);
            AddObject(NpcHeadcracker, data, ref offset, BaseOffset, 375 * 4, StoredObjectTable);
            AddObject(StrigaClawA, data, ref offset, BaseOffset, 376 * 4, StoredObjectTable);
            AddObject(StrigaClawB, data, ref offset, BaseOffset, 377 * 4, StoredObjectTable);
            AddObject(StrigaReap, data, ref offset, BaseOffset, 378 * 4, StoredObjectTable);
            AddObject(StrigaReap2, data, ref offset, BaseOffset, 379 * 4, StoredObjectTable);
            AddObject(StrigaFireBreath, data, ref offset, BaseOffset, 380 * 4, StoredObjectTable);
            AddObject(StrigaBuff, data, ref offset, BaseOffset, 381 * 4, StoredObjectTable);
            AddObject(GhostlyPunchA, data, ref offset, BaseOffset, 382 * 4, StoredObjectTable);
            AddObject(GhostlyPunchB, data, ref offset, BaseOffset, 383 * 4, StoredObjectTable);
            AddObject(GhostlyBurst, data, ref offset, BaseOffset, 384 * 4, StoredObjectTable);
            AddObject(GhostlyBossPunchA, data, ref offset, BaseOffset, 385 * 4, StoredObjectTable);
            AddObject(GhostlyBossPunchB, data, ref offset, BaseOffset, 386 * 4, StoredObjectTable);
            AddObject(GhostlyBossBurst, data, ref offset, BaseOffset, 387 * 4, StoredObjectTable);
            AddObject(GhostlyBolt, data, ref offset, BaseOffset, 388 * 4, StoredObjectTable);
            AddObject(InjectorBugBite, data, ref offset, BaseOffset, 389 * 4, StoredObjectTable);
            AddObject(InjectorBugInject, data, ref offset, BaseOffset, 390 * 4, StoredObjectTable);
            AddObject(InjectorBugInject2, data, ref offset, BaseOffset, 391 * 4, StoredObjectTable);
            AddObject(FaceOfDeathKill, data, ref offset, BaseOffset, 392 * 4, StoredObjectTable);
            AddObject(WatcherFireball, data, ref offset, BaseOffset, 393 * 4, StoredObjectTable);
            AddObject(WatcherSlap, data, ref offset, BaseOffset, 394 * 4, StoredObjectTable);
            AddObject(WatcherAcidball, data, ref offset, BaseOffset, 395 * 4, StoredObjectTable);
            AddObject(RedCrystalBlast, data, ref offset, BaseOffset, 396 * 4, StoredObjectTable);
            AddObject(RedCrystalBurst, data, ref offset, BaseOffset, 397 * 4, StoredObjectTable);
            AddObject(TurretCrystalZap, data, ref offset, BaseOffset, 398 * 4, StoredObjectTable);
            AddObject(TurretCrystalZap2, data, ref offset, BaseOffset, 399 * 4, StoredObjectTable);
            AddObject(TurretCrystalZapLongRange, data, ref offset, BaseOffset, 400 * 4, StoredObjectTable);
            AddObject(TurretCrystalZapLongRange2, data, ref offset, BaseOffset, 401 * 4, StoredObjectTable);
            AddObject(DeathRay, data, ref offset, BaseOffset, 402 * 4, StoredObjectTable);
            AddObject(SpyPortalZap, data, ref offset, BaseOffset, 403 * 4, StoredObjectTable);
            AddObject(SpyPortalZap2, data, ref offset, BaseOffset, 404 * 4, StoredObjectTable);
            AddObject(BitingVineBite, data, ref offset, BaseOffset, 405 * 4, StoredObjectTable);
            AddObject(BitingVineSpit, data, ref offset, BaseOffset, 406 * 4, StoredObjectTable);
            AddObject(BitingVineSpitB, data, ref offset, BaseOffset, 407 * 4, StoredObjectTable);
            AddObject(BitingVineCast, data, ref offset, BaseOffset, 408 * 4, StoredObjectTable);
            AddObject(BitingVineAppear, data, ref offset, BaseOffset, 409 * 4, StoredObjectTable);
            AddObject(BitingVineDisappear, data, ref offset, BaseOffset, 410 * 4, StoredObjectTable);
            AddObject(TrollClubA, data, ref offset, BaseOffset, 411 * 4, StoredObjectTable);
            AddObject(TrollClubB, data, ref offset, BaseOffset, 412 * 4, StoredObjectTable);
            AddObject(TrollKnockdown, data, ref offset, BaseOffset, 413 * 4, StoredObjectTable);
            AddObject(OgreClubA, data, ref offset, BaseOffset, 414 * 4, StoredObjectTable);
            AddObject(OgreClubB, data, ref offset, BaseOffset, 415 * 4, StoredObjectTable);
            AddObject(OgreThrow, data, ref offset, BaseOffset, 416 * 4, StoredObjectTable);
            AddObject(OgreStun, data, ref offset, BaseOffset, 417 * 4, StoredObjectTable);
            AddObject(FaeSwordA, data, ref offset, BaseOffset, 418 * 4, StoredObjectTable);
            AddObject(FaeSwordB, data, ref offset, BaseOffset, 419 * 4, StoredObjectTable);
            AddObject(FaeSwordKill, data, ref offset, BaseOffset, 420 * 4, StoredObjectTable);
            AddObject(DementiaPuckCurse, data, ref offset, BaseOffset, 421 * 4, StoredObjectTable);
            AddObject(FaeLightningSmiteHidden, data, ref offset, BaseOffset, 422 * 4, StoredObjectTable);
            AddObject(NecroSpark, data, ref offset, BaseOffset, 423 * 4, StoredObjectTable);
            AddObject(NecroDarknessWave, data, ref offset, BaseOffset, 424 * 4, StoredObjectTable);
            AddObject(NecroPainBubble, data, ref offset, BaseOffset, 425 * 4, StoredObjectTable);
            AddObject(NecroSparkPerching, data, ref offset, BaseOffset, 426 * 4, StoredObjectTable);
            AddObject(NecroDeathsHold, data, ref offset, BaseOffset, 427 * 4, StoredObjectTable);
            AddObject(DroachBiteA, data, ref offset, BaseOffset, 428 * 4, StoredObjectTable);
            AddObject(DroachBiteB, data, ref offset, BaseOffset, 429 * 4, StoredObjectTable);
            AddObject(DroachFireball, data, ref offset, BaseOffset, 430 * 4, StoredObjectTable);
            AddObject(DroachFireballPerching, data, ref offset, BaseOffset, 431 * 4, StoredObjectTable);
            AddObject(DroachBreatheFire, data, ref offset, BaseOffset, 432 * 4, StoredObjectTable);
            AddObject(DroachLightning, data, ref offset, BaseOffset, 433 * 4, StoredObjectTable);
            AddObject(DroachLightningPerching, data, ref offset, BaseOffset, 434 * 4, StoredObjectTable);
            AddObject(DroachShockingKnockback, data, ref offset, BaseOffset, 435 * 4, StoredObjectTable);
            AddObject(BasiliskClawA, data, ref offset, BaseOffset, 436 * 4, StoredObjectTable);
            AddObject(BasiliskClawB, data, ref offset, BaseOffset, 437 * 4, StoredObjectTable);
            AddObject(BasiliskToxicBite, data, ref offset, BaseOffset, 438 * 4, StoredObjectTable);
            AddObject(BasiliskDebuff, data, ref offset, BaseOffset, 439 * 4, StoredObjectTable);
            AddObject(BasiliskCastPerching, data, ref offset, BaseOffset, 440 * 4, StoredObjectTable);
            AddObject(CultistArrow1, data, ref offset, BaseOffset, 441 * 4, StoredObjectTable);
            AddObject(CultistArrow2, data, ref offset, BaseOffset, 442 * 4, StoredObjectTable);
            AddObject(CultistOmegaArrow, data, ref offset, BaseOffset, 443 * 4, StoredObjectTable);
            AddObject(CultistSword1, data, ref offset, BaseOffset, 444 * 4, StoredObjectTable);
            AddObject(CultistSword2, data, ref offset, BaseOffset, 445 * 4, StoredObjectTable);
            AddObject(CultistSwordStun, data, ref offset, BaseOffset, 446 * 4, StoredObjectTable);
            AddObject(BossMegaSword1, data, ref offset, BaseOffset, 447 * 4, StoredObjectTable);
            AddObject(BossMegaSword2, data, ref offset, BaseOffset, 448 * 4, StoredObjectTable);
            AddObject(SedgewickMegaSwordAngry, data, ref offset, BaseOffset, 449 * 4, StoredObjectTable);
            AddObject(BossMegaHammer, data, ref offset, BaseOffset, 450 * 4, StoredObjectTable);
            AddObject(BossMegaHammer2, data, ref offset, BaseOffset, 451 * 4, StoredObjectTable);
            AddObject(BossMegaRageHammer, data, ref offset, BaseOffset, 452 * 4, StoredObjectTable);
            AddObject(ClaudiaTundraSpikes, data, ref offset, BaseOffset, 453 * 4, StoredObjectTable);
            AddObject(ClaudiaIceSpear, data, ref offset, BaseOffset, 454 * 4, StoredObjectTable);
            AddObject(ClaudiaBlizzard, data, ref offset, BaseOffset, 455 * 4, StoredObjectTable);
            AddObject(BigGolemHitA, data, ref offset, BaseOffset, 456 * 4, StoredObjectTable);
            AddObject(BigGolemHitB, data, ref offset, BaseOffset, 457 * 4, StoredObjectTable);
            AddObject(BigGolemFlingBoss, data, ref offset, BaseOffset, 458 * 4, StoredObjectTable);
            AddObject(BigGolemPerchFix, data, ref offset, BaseOffset, 459 * 4, StoredObjectTable);
            AddObject(BigGolemFlingBoss2, data, ref offset, BaseOffset, 460 * 4, StoredObjectTable);
            AddObject(BigGolemSummonFireSnake, data, ref offset, BaseOffset, 461 * 4, StoredObjectTable);
            AddObject(BigGolemHitB_NoDisable, data, ref offset, BaseOffset, 462 * 4, StoredObjectTable);
            AddObject(BigGolemHitA_NoDisable, data, ref offset, BaseOffset, 463 * 4, StoredObjectTable);
            AddObject(BigGolemFling, data, ref offset, BaseOffset, 464 * 4, StoredObjectTable);
            AddObject(GhoulClawA, data, ref offset, BaseOffset, 465 * 4, StoredObjectTable);
            AddObject(GhoulClawB, data, ref offset, BaseOffset, 466 * 4, StoredObjectTable);
            AddObject(GhoulSelfBuff, data, ref offset, BaseOffset, 467 * 4, StoredObjectTable);
            AddObject(GhoulHammerA, data, ref offset, BaseOffset, 468 * 4, StoredObjectTable);
            AddObject(GhoulHammerB, data, ref offset, BaseOffset, 469 * 4, StoredObjectTable);
            AddObject(DragonWormSpitElectricity, data, ref offset, BaseOffset, 470 * 4, StoredObjectTable);
            AddObject(DragonWormBite, data, ref offset, BaseOffset, 471 * 4, StoredObjectTable);
            AddObject(DragonWormSmack, data, ref offset, BaseOffset, 472 * 4, StoredObjectTable);
            AddObject(DragonWormRage, data, ref offset, BaseOffset, 473 * 4, StoredObjectTable);
            AddObject(DragonWormEscape, data, ref offset, BaseOffset, 474 * 4, StoredObjectTable);
            AddObject(DragonWormSpitFire, data, ref offset, BaseOffset, 475 * 4, StoredObjectTable);
            AddObject(ColdSphereBurst, data, ref offset, BaseOffset, 476 * 4, StoredObjectTable);
            AddObject(ColdSphereFreezeBurst, data, ref offset, BaseOffset, 477 * 4, StoredObjectTable);
            AddObject(ManticoreBite, data, ref offset, BaseOffset, 478 * 4, StoredObjectTable);
            AddObject(ManticoreClaw, data, ref offset, BaseOffset, 479 * 4, StoredObjectTable);
            AddObject(ManticoreSting1, data, ref offset, BaseOffset, 480 * 4, StoredObjectTable);
            AddObject(Manticoresting2, data, ref offset, BaseOffset, 481 * 4, StoredObjectTable);
            AddObject(RakStaffHit, data, ref offset, BaseOffset, 482 * 4, StoredObjectTable);
            AddObject(RakStaffPin, data, ref offset, BaseOffset, 483 * 4, StoredObjectTable);
            AddObject(RakStaffBlock, data, ref offset, BaseOffset, 484 * 4, StoredObjectTable);
            AddObject(RakStaffHeavy, data, ref offset, BaseOffset, 485 * 4, StoredObjectTable);
            AddObject(RakSlash, data, ref offset, BaseOffset, 486 * 4, StoredObjectTable);
            AddObject(RakKnee, data, ref offset, BaseOffset, 487 * 4, StoredObjectTable);
            AddObject(RakKick, data, ref offset, BaseOffset, 488 * 4, StoredObjectTable);
            AddObject(RakBarrage, data, ref offset, BaseOffset, 489 * 4, StoredObjectTable);
            AddObject(RakSwordSlash, data, ref offset, BaseOffset, 490 * 4, StoredObjectTable);
            AddObject(RakHackingBlade, data, ref offset, BaseOffset, 491 * 4, StoredObjectTable);
            AddObject(RakDecapitate, data, ref offset, BaseOffset, 492 * 4, StoredObjectTable);
            AddObject(RakFireball, data, ref offset, BaseOffset, 493 * 4, StoredObjectTable);
            AddObject(RakBreatheFire, data, ref offset, BaseOffset, 494 * 4, StoredObjectTable);
            AddObject(RakRingOfFire, data, ref offset, BaseOffset, 495 * 4, StoredObjectTable);
            AddObject(RakToxinBomb, data, ref offset, BaseOffset, 496 * 4, StoredObjectTable);
            AddObject(RakAcidBomb, data, ref offset, BaseOffset, 497 * 4, StoredObjectTable);
            AddObject(RakHealingMist, data, ref offset, BaseOffset, 498 * 4, StoredObjectTable);
            AddObject(RakBasicShot, data, ref offset, BaseOffset, 499 * 4, StoredObjectTable);
            AddObject(RakHookShot, data, ref offset, BaseOffset, 500 * 4, StoredObjectTable);
            AddObject(RakBowBash, data, ref offset, BaseOffset, 501 * 4, StoredObjectTable);
            AddObject(RakAimedShot, data, ref offset, BaseOffset, 502 * 4, StoredObjectTable);
            AddObject(RakPoisonArrow, data, ref offset, BaseOffset, 503 * 4, StoredObjectTable);
            AddObject(RakMindreave, data, ref offset, BaseOffset, 504 * 4, StoredObjectTable);
            AddObject(RakPainBubble, data, ref offset, BaseOffset, 505 * 4, StoredObjectTable);
            AddObject(RakPanicCharge, data, ref offset, BaseOffset, 506 * 4, StoredObjectTable);
            AddObject(RakRevitalize, data, ref offset, BaseOffset, 507 * 4, StoredObjectTable);
            AddObject(RakReconstruct, data, ref offset, BaseOffset, 508 * 4, StoredObjectTable);
            AddObject(RakBossSlow, data, ref offset, BaseOffset, 509 * 4, StoredObjectTable);
            AddObject(RakBossPerchSlow, data, ref offset, BaseOffset, 510 * 4, StoredObjectTable);
            AddObject(FlapSkullBite, data, ref offset, BaseOffset, 511 * 4, StoredObjectTable);
            AddObject(FlapSkullBigBite, data, ref offset, BaseOffset, 512 * 4, StoredObjectTable);
            AddObject(MinotaurClub, data, ref offset, BaseOffset, 513 * 4, StoredObjectTable);
            AddObject(MinotaurRageClub, data, ref offset, BaseOffset, 514 * 4, StoredObjectTable);
            AddObject(MinotaurBoulder, data, ref offset, BaseOffset, 515 * 4, StoredObjectTable);
            AddObject(MinotaurBossRageClub, data, ref offset, BaseOffset, 516 * 4, StoredObjectTable);
            AddObject(CockatricePeck, data, ref offset, BaseOffset, 517 * 4, StoredObjectTable);
            AddObject(CockatriceTailWhip, data, ref offset, BaseOffset, 518 * 4, StoredObjectTable);
            AddObject(CockatriceParalyze, data, ref offset, BaseOffset, 519 * 4, StoredObjectTable);
            AddObject(GiantBeetleBite, data, ref offset, BaseOffset, 520 * 4, StoredObjectTable);
            AddObject(GiantBeetleInject, data, ref offset, BaseOffset, 521 * 4, StoredObjectTable);
            AddObject(GiantBeetleBoulderSpit, data, ref offset, BaseOffset, 522 * 4, StoredObjectTable);
            AddObject(BatIllusionSlashA, data, ref offset, BaseOffset, 523 * 4, StoredObjectTable);
            AddObject(BatIllusionSlashB, data, ref offset, BaseOffset, 524 * 4, StoredObjectTable);
            AddObject(BatIllusionBite, data, ref offset, BaseOffset, 525 * 4, StoredObjectTable);
            AddObject(GiantBatSlashA, data, ref offset, BaseOffset, 526 * 4, StoredObjectTable);
            AddObject(GiantBatSlashB, data, ref offset, BaseOffset, 527 * 4, StoredObjectTable);
            AddObject(GiantBatBite, data, ref offset, BaseOffset, 528 * 4, StoredObjectTable);
            AddObject(HagAgingTouch, data, ref offset, BaseOffset, 529 * 4, StoredObjectTable);
            AddObject(HagAgingScream, data, ref offset, BaseOffset, 530 * 4, StoredObjectTable);
            AddObject(TriffidClawA, data, ref offset, BaseOffset, 531 * 4, StoredObjectTable);
            AddObject(TriffidClawB, data, ref offset, BaseOffset, 532 * 4, StoredObjectTable);
            AddObject(TriffidTongue, data, ref offset, BaseOffset, 533 * 4, StoredObjectTable);
            AddObject(TriffidSpore, data, ref offset, BaseOffset, 534 * 4, StoredObjectTable);
            AddObject(TriffidShot, data, ref offset, BaseOffset, 535 * 4, StoredObjectTable);
            AddObject(TriffidTongueElite, data, ref offset, BaseOffset, 536 * 4, StoredObjectTable);
            AddObject(GiantScorpionClawA, data, ref offset, BaseOffset, 537 * 4, StoredObjectTable);
            AddObject(GiantScorpionClawB, data, ref offset, BaseOffset, 538 * 4, StoredObjectTable);
            AddObject(GiantScorpionSting, data, ref offset, BaseOffset, 539 * 4, StoredObjectTable);
            AddObject(KrakenBeak, data, ref offset, BaseOffset, 540 * 4, StoredObjectTable);
            AddObject(KrakenSlam, data, ref offset, BaseOffset, 541 * 4, StoredObjectTable);
            AddObject(KrakenRage, data, ref offset, BaseOffset, 542 * 4, StoredObjectTable);
            AddObject(KrakenBabyBeak, data, ref offset, BaseOffset, 543 * 4, StoredObjectTable);
            AddObject(KrakenBabySlam, data, ref offset, BaseOffset, 544 * 4, StoredObjectTable);
            AddObject(KrakenBabyRage, data, ref offset, BaseOffset, 545 * 4, StoredObjectTable);
            AddObject(RanalonHit, data, ref offset, BaseOffset, 546 * 4, StoredObjectTable);
            AddObject(RanalonHitB, data, ref offset, BaseOffset, 547 * 4, StoredObjectTable);
            AddObject(RanalonKick, data, ref offset, BaseOffset, 548 * 4, StoredObjectTable);
            AddObject(RanalonTongue, data, ref offset, BaseOffset, 549 * 4, StoredObjectTable);
            AddObject(RanalonZap, data, ref offset, BaseOffset, 550 * 4, StoredObjectTable);
            AddObject(RanalonZapB, data, ref offset, BaseOffset, 551 * 4, StoredObjectTable);
            AddObject(RanalonHeal, data, ref offset, BaseOffset, 552 * 4, StoredObjectTable);
            AddObject(RanalonRoot, data, ref offset, BaseOffset, 553 * 4, StoredObjectTable);
            AddObject(RanalonSelfBuff, data, ref offset, BaseOffset, 554 * 4, StoredObjectTable);
            AddObject(RanalonSelfBuffElite, data, ref offset, BaseOffset, 555 * 4, StoredObjectTable);
            AddObject(RanalonGuardianStab, data, ref offset, BaseOffset, 556 * 4, StoredObjectTable);
            AddObject(RanalonGuardianStabB, data, ref offset, BaseOffset, 557 * 4, StoredObjectTable);
            AddObject(RanalonGuardianBite, data, ref offset, BaseOffset, 558 * 4, StoredObjectTable);
            AddObject(RanalonGuardianBlind, data, ref offset, BaseOffset, 559 * 4, StoredObjectTable);
            AddObject(RanalonDoctrineKeeperStab, data, ref offset, BaseOffset, 560 * 4, StoredObjectTable);
            AddObject(RanalonDoctrineKeeperBlind, data, ref offset, BaseOffset, 561 * 4, StoredObjectTable);
            AddObject(BarghestBiteA, data, ref offset, BaseOffset, 562 * 4, StoredObjectTable);
            AddObject(BarghestBiteB, data, ref offset, BaseOffset, 563 * 4, StoredObjectTable);
            AddObject(BarghestDebuff, data, ref offset, BaseOffset, 564 * 4, StoredObjectTable);
            AddObject(WorghestDebuff, data, ref offset, BaseOffset, 565 * 4, StoredObjectTable);
            AddObject(BallistaFire, data, ref offset, BaseOffset, 566 * 4, StoredObjectTable);
            AddObject(BallistaFire_Long, data, ref offset, BaseOffset, 567 * 4, StoredObjectTable);
            AddObject(GargoyleSlamA, data, ref offset, BaseOffset, 568 * 4, StoredObjectTable);
            AddObject(GargoyleSlamB, data, ref offset, BaseOffset, 569 * 4, StoredObjectTable);
            AddObject(GargoyleStun, data, ref offset, BaseOffset, 570 * 4, StoredObjectTable);
            AddObject(GargoyleBossStun, data, ref offset, BaseOffset, 571 * 4, StoredObjectTable);
            AddObject(ScrayBite, data, ref offset, BaseOffset, 572 * 4, StoredObjectTable);
            AddObject(ScrayStab, data, ref offset, BaseOffset, 573 * 4, StoredObjectTable);
            AddObject(HippoBite, data, ref offset, BaseOffset, 574 * 4, StoredObjectTable);
            AddObject(HippoBiteAndHeal1, data, ref offset, BaseOffset, 575 * 4, StoredObjectTable);
            AddObject(BigCatClaw_Pet, data, ref offset, BaseOffset, 576 * 4, StoredObjectTable);
            AddObject(BigCatPounce_Pet, data, ref offset, BaseOffset, 577 * 4, StoredObjectTable);
            AddObject(BigCatKill_Pet1, data, ref offset, BaseOffset, 578 * 4, StoredObjectTable);
            AddObject(BigCatKill_Pet2, data, ref offset, BaseOffset, 579 * 4, StoredObjectTable);
            AddObject(BigCatKill_Pet3, data, ref offset, BaseOffset, 580 * 4, StoredObjectTable);
            AddObject(BigCatKill_Pet4, data, ref offset, BaseOffset, 581 * 4, StoredObjectTable);
            AddObject(BigCatKill_Pet5, data, ref offset, BaseOffset, 582 * 4, StoredObjectTable);
            AddObject(BigCatKill_Pet6, data, ref offset, BaseOffset, 583 * 4, StoredObjectTable);
            AddObject(BigCatUltraKill_Pet1, data, ref offset, BaseOffset, 584 * 4, StoredObjectTable);
            AddObject(BigCatUltraKill_Pet2, data, ref offset, BaseOffset, 585 * 4, StoredObjectTable);
            AddObject(BigCatUltraKill_Pet3, data, ref offset, BaseOffset, 586 * 4, StoredObjectTable);
            AddObject(BigCatUltraKill_Pet4, data, ref offset, BaseOffset, 587 * 4, StoredObjectTable);
            AddObject(BigCatUltraKill_Pet5, data, ref offset, BaseOffset, 588 * 4, StoredObjectTable);
            AddObject(BigCatUltraKill_Pet6, data, ref offset, BaseOffset, 589 * 4, StoredObjectTable);
            AddObject(BigCatRoot_Pet1, data, ref offset, BaseOffset, 590 * 4, StoredObjectTable);
            AddObject(BigCatRoot_Pet2, data, ref offset, BaseOffset, 591 * 4, StoredObjectTable);
            AddObject(BigCatRoot_Pet3, data, ref offset, BaseOffset, 592 * 4, StoredObjectTable);
            AddObject(BigCatRoot_Pet4, data, ref offset, BaseOffset, 593 * 4, StoredObjectTable);
            AddObject(BigCatVuln_Pet1, data, ref offset, BaseOffset, 594 * 4, StoredObjectTable);
            AddObject(BigCatVuln_Pet2, data, ref offset, BaseOffset, 595 * 4, StoredObjectTable);
            AddObject(BigCatVuln_Pet3, data, ref offset, BaseOffset, 596 * 4, StoredObjectTable);
            AddObject(BigCatVuln_Pet4, data, ref offset, BaseOffset, 597 * 4, StoredObjectTable);
            AddObject(BigCatHeal_Pet1, data, ref offset, BaseOffset, 598 * 4, StoredObjectTable);
            AddObject(BigCatHeal_Pet2, data, ref offset, BaseOffset, 599 * 4, StoredObjectTable);
            AddObject(BigCatHeal_Pet3, data, ref offset, BaseOffset, 600 * 4, StoredObjectTable);
            AddObject(BigCatHeal_Pet4, data, ref offset, BaseOffset, 601 * 4, StoredObjectTable);
            AddObject(BigCatHeal_Pet5, data, ref offset, BaseOffset, 602 * 4, StoredObjectTable);
            AddObject(BigCatHeal_Pet6, data, ref offset, BaseOffset, 603 * 4, StoredObjectTable);
            AddObject(GrimalkinPuncture_Pet1, data, ref offset, BaseOffset, 604 * 4, StoredObjectTable);
            AddObject(GrimalkinPuncture_Pet2, data, ref offset, BaseOffset, 605 * 4, StoredObjectTable);
            AddObject(GrimalkinPuncture_Pet3, data, ref offset, BaseOffset, 606 * 4, StoredObjectTable);
            AddObject(GrimalkinPuncture_Pet4, data, ref offset, BaseOffset, 607 * 4, StoredObjectTable);
            AddObject(GrimalkinPuncture_Pet5, data, ref offset, BaseOffset, 608 * 4, StoredObjectTable);
            AddObject(GrimalkinPuncture_Pet6, data, ref offset, BaseOffset, 609 * 4, StoredObjectTable);
            AddObject(GrimalkinFlee_Pet1, data, ref offset, BaseOffset, 610 * 4, StoredObjectTable);
            AddObject(GrimalkinFlee_Pet2, data, ref offset, BaseOffset, 611 * 4, StoredObjectTable);
            AddObject(GrimalkinFlee_Pet3, data, ref offset, BaseOffset, 612 * 4, StoredObjectTable);
            AddObject(RatBite_Pet, data, ref offset, BaseOffset, 613 * 4, StoredObjectTable);
            AddObject(RatClaw_Pet, data, ref offset, BaseOffset, 614 * 4, StoredObjectTable);
            AddObject(RatDeRage_Pet1, data, ref offset, BaseOffset, 615 * 4, StoredObjectTable);
            AddObject(RatDeRage_Pet2, data, ref offset, BaseOffset, 616 * 4, StoredObjectTable);
            AddObject(RatDeRage_Pet3, data, ref offset, BaseOffset, 617 * 4, StoredObjectTable);
            AddObject(RatDeRage_Pet4, data, ref offset, BaseOffset, 618 * 4, StoredObjectTable);
            AddObject(RatDeRage_Pet5, data, ref offset, BaseOffset, 619 * 4, StoredObjectTable);
            AddObject(RatDeRage_Pet6, data, ref offset, BaseOffset, 620 * 4, StoredObjectTable);
            AddObject(RatHeal_Pet1, data, ref offset, BaseOffset, 621 * 4, StoredObjectTable);
            AddObject(RatHeal_Pet2, data, ref offset, BaseOffset, 622 * 4, StoredObjectTable);
            AddObject(RatHeal_Pet3, data, ref offset, BaseOffset, 623 * 4, StoredObjectTable);
            AddObject(RatHeal_Pet4, data, ref offset, BaseOffset, 624 * 4, StoredObjectTable);
            AddObject(RatHeal_Pet5, data, ref offset, BaseOffset, 625 * 4, StoredObjectTable);
            AddObject(RatHeal_Pet6, data, ref offset, BaseOffset, 626 * 4, StoredObjectTable);
            AddObject(RatVuln_Pet1, data, ref offset, BaseOffset, 627 * 4, StoredObjectTable);
            AddObject(RatVuln_Pet2, data, ref offset, BaseOffset, 628 * 4, StoredObjectTable);
            AddObject(RatVuln_Pet3, data, ref offset, BaseOffset, 629 * 4, StoredObjectTable);
            AddObject(RatVuln_Pet4, data, ref offset, BaseOffset, 630 * 4, StoredObjectTable);
            AddObject(FireRatBite_Pet, data, ref offset, BaseOffset, 631 * 4, StoredObjectTable);
            AddObject(FireRatClaw_Pet, data, ref offset, BaseOffset, 632 * 4, StoredObjectTable);
            AddObject(RatBurn_Pet1, data, ref offset, BaseOffset, 633 * 4, StoredObjectTable);
            AddObject(RatBurn_Pet2, data, ref offset, BaseOffset, 634 * 4, StoredObjectTable);
            AddObject(RatBurn_Pet3, data, ref offset, BaseOffset, 635 * 4, StoredObjectTable);
            AddObject(RatBurn_Pet4, data, ref offset, BaseOffset, 636 * 4, StoredObjectTable);
            AddObject(RatBurn_Pet5, data, ref offset, BaseOffset, 637 * 4, StoredObjectTable);
            AddObject(RatBurn_Pet6, data, ref offset, BaseOffset, 638 * 4, StoredObjectTable);
            AddObject(BearBite_Pet, data, ref offset, BaseOffset, 639 * 4, StoredObjectTable);
            AddObject(BearClaw_Pet, data, ref offset, BaseOffset, 640 * 4, StoredObjectTable);
            AddObject(BearTaunt_Pet1, data, ref offset, BaseOffset, 641 * 4, StoredObjectTable);
            AddObject(BearTaunt_Pet2, data, ref offset, BaseOffset, 642 * 4, StoredObjectTable);
            AddObject(BearTaunt_Pet3, data, ref offset, BaseOffset, 643 * 4, StoredObjectTable);
            AddObject(BearTaunt_Pet4, data, ref offset, BaseOffset, 644 * 4, StoredObjectTable);
            AddObject(BearTaunt_Pet5, data, ref offset, BaseOffset, 645 * 4, StoredObjectTable);
            AddObject(BearTaunt_Pet6, data, ref offset, BaseOffset, 646 * 4, StoredObjectTable);
            AddObject(BearStun_Pet1, data, ref offset, BaseOffset, 647 * 4, StoredObjectTable);
            AddObject(BearStun_Pet2, data, ref offset, BaseOffset, 648 * 4, StoredObjectTable);
            AddObject(BearStun_Pet3, data, ref offset, BaseOffset, 649 * 4, StoredObjectTable);
            AddObject(BearStun_Pet4, data, ref offset, BaseOffset, 650 * 4, StoredObjectTable);
            AddObject(BearWarmth_Pet1, data, ref offset, BaseOffset, 651 * 4, StoredObjectTable);
            AddObject(BearWarmth_Pet2, data, ref offset, BaseOffset, 652 * 4, StoredObjectTable);
            AddObject(BearWarmth_Pet3, data, ref offset, BaseOffset, 653 * 4, StoredObjectTable);
            AddObject(BearWarmth_Pet4, data, ref offset, BaseOffset, 654 * 4, StoredObjectTable);
            AddObject(BearWarmth_Pet5, data, ref offset, BaseOffset, 655 * 4, StoredObjectTable);
            AddObject(BearWarmth_Pet6, data, ref offset, BaseOffset, 656 * 4, StoredObjectTable);
            AddObject(BearSelfHeal_Pet1, data, ref offset, BaseOffset, 657 * 4, StoredObjectTable);
            AddObject(BearSelfHeal_Pet2, data, ref offset, BaseOffset, 658 * 4, StoredObjectTable);
            AddObject(BearSelfHeal_Pet3, data, ref offset, BaseOffset, 659 * 4, StoredObjectTable);
            AddObject(BearSelfHeal_Pet4, data, ref offset, BaseOffset, 660 * 4, StoredObjectTable);
            AddObject(BearSelfHeal_Pet5, data, ref offset, BaseOffset, 661 * 4, StoredObjectTable);
            AddObject(BearSelfHeal_Pet6, data, ref offset, BaseOffset, 662 * 4, StoredObjectTable);
            AddObject(BearUltra_Pet1, data, ref offset, BaseOffset, 663 * 4, StoredObjectTable);
            AddObject(BearUltra_Pet2, data, ref offset, BaseOffset, 664 * 4, StoredObjectTable);
            AddObject(BearUltra_Pet3, data, ref offset, BaseOffset, 665 * 4, StoredObjectTable);
            AddObject(BearUltra_Pet4, data, ref offset, BaseOffset, 666 * 4, StoredObjectTable);
            AddObject(BearUltra_Pet5, data, ref offset, BaseOffset, 667 * 4, StoredObjectTable);
            AddObject(BearUltra_Pet6, data, ref offset, BaseOffset, 668 * 4, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 669 * 4, null, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
