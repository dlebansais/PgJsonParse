using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AIAbilitySet : GenericJsonObject<AIAbilitySet>, IPgAIAbilitySet
    {
        #region Direct Properties
        public IPgAIAbility AnimalBite { get; private set; }
        public IPgAIAbility AnimalClaw { get; private set; }
        public IPgAIAbility AnimalOmegaBite { get; private set; }
        public IPgAIAbility AnimalOmegaBite2 { get; private set; }
        public IPgAIAbility AnimalHoofFrontKick { get; private set; }
        public IPgAIAbility AnimalHoofRageKick { get; private set; }
        public IPgAIAbility AnimalHoofRageKick2 { get; private set; }
        public IPgAIAbility AnimalHoofFieryFrontKick { get; private set; }
        public IPgAIAbility AnimalHoofFieryFrontKick2 { get; private set; }
        public IPgAIAbility ElectricPigHitAndRun { get; private set; }
        public IPgAIAbility ElectricPigStun { get; private set; }
        public IPgAIAbility ElectricPigAoEStun { get; private set; }
        public IPgAIAbility LamiaMindControl { get; private set; }
        public IPgAIAbility LamiaRage { get; private set; }
        public IPgAIAbility SlimeKick { get; private set; }
        public IPgAIAbility SlimeKickB { get; private set; }
        public IPgAIAbility SlimeBite { get; private set; }
        public IPgAIAbility SlimeBiteB { get; private set; }
        public IPgAIAbility Slime_SummonSlime { get; private set; }
        public IPgAIAbility SlimeSpit { get; private set; }
        public IPgAIAbility SlimeSuperSpit { get; private set; }
        public IPgAIAbility IceSlimeKick { get; private set; }
        public IPgAIAbility IceSlimeKickB { get; private set; }
        public IPgAIAbility IceSlimeBite { get; private set; }
        public IPgAIAbility IceSlimeBiteB { get; private set; }
        public IPgAIAbility BossSlimeKick { get; private set; }
        public IPgAIAbility BossSlimeKickB { get; private set; }
        public IPgAIAbility BossSlime_SummonSlime1 { get; private set; }
        public IPgAIAbility BossSlimeKick2 { get; private set; }
        public IPgAIAbility BossSlimeKick2B { get; private set; }
        public IPgAIAbility BossSlime_SummonSlime4Elite { get; private set; }
        public IPgAIAbility AnimalHeal { get; private set; }
        public IPgAIAbility AnimalHeal2 { get; private set; }
        public IPgAIAbility AnimalHeal3 { get; private set; }
        public IPgAIAbility IceCockPeck { get; private set; }
        public IPgAIAbility IceCockFreeze { get; private set; }
        public IPgAIAbility NightmareHoof { get; private set; }
        public IPgAIAbility NightmareDarknessBomb { get; private set; }
        public IPgAIAbility CiervosNightmareHoof { get; private set; }
        public IPgAIAbility CiervosDarknessBomb { get; private set; }
        public IPgAIAbility Myconian_Bash { get; private set; }
        public IPgAIAbility Myconian_Mindspores { get; private set; }
        public IPgAIAbility Myconian_Drain { get; private set; }
        public IPgAIAbility Myconian_BossBash { get; private set; }
        public IPgAIAbility Myconian_Mindspores_Permanent { get; private set; }
        public IPgAIAbility Myconian_TidalCurse { get; private set; }
        public IPgAIAbility Myconian_Shock { get; private set; }
        public IPgAIAbility AlienDog_Punch { get; private set; }
        public IPgAIAbility AlienDog_Punch2 { get; private set; }
        public IPgAIAbility AlienDog_RagePunch { get; private set; }
        public IPgAIAbility MushroomMonster_Bite { get; private set; }
        public IPgAIAbility MushroomMonster_Spit1 { get; private set; }
        public IPgAIAbility MushroomMonster_Spit2 { get; private set; }
        public IPgAIAbility MushroomMonster_SummonMushroomSpawn1 { get; private set; }
        public IPgAIAbility MushroomMonster_SummonMushroomSpawn2 { get; private set; }
        public IPgAIAbility MushroomMonster_SummonMushroomSpawn3 { get; private set; }
        public IPgAIAbility MushroomMonster_SpawnSpit1 { get; private set; }
        public IPgAIAbility MushroomMonster_SpawnSpit2 { get; private set; }
        public IPgAIAbility MushroomMonster_SpawnSuperSpit1 { get; private set; }
        public IPgAIAbility MushroomMonster_SpawnSuperSpit2 { get; private set; }
        public IPgAIAbility MaronesaStomp { get; private set; }
        public IPgAIAbility MaronesaInfect { get; private set; }
        public IPgAIAbility WormBite1 { get; private set; }
        public IPgAIAbility WormSpit1 { get; private set; }
        public IPgAIAbility WormShove1 { get; private set; }
        public IPgAIAbility WormInfect1 { get; private set; }
        public IPgAIAbility WormSpit2 { get; private set; }
        public IPgAIAbility WormBossSpit { get; private set; }
        public IPgAIAbility WormBossAcidBurst { get; private set; }
        public IPgAIAbility GnasherBite { get; private set; }
        public IPgAIAbility GnasherRend { get; private set; }
        public IPgAIAbility Dinoslash { get; private set; }
        public IPgAIAbility Dinoslash2 { get; private set; }
        public IPgAIAbility Dinobite { get; private set; }
        public IPgAIAbility Dinobite2 { get; private set; }
        public IPgAIAbility Dinowhap { get; private set; }
        public IPgAIAbility Peck { get; private set; }
        public IPgAIAbility AnimalFlee { get; private set; }
        public IPgAIAbility HippogriffPeck { get; private set; }
        public IPgAIAbility HippogriffSlashes { get; private set; }
        public IPgAIAbility HippogriffBossSlashes { get; private set; }
        public IPgAIAbility RatBite { get; private set; }
        public IPgAIAbility RatClaw { get; private set; }
        public IPgAIAbility FireRatBite { get; private set; }
        public IPgAIAbility FireRatClaw { get; private set; }
        public IPgAIAbility GoblinZapBall { get; private set; }
        public IPgAIAbility GoblinHateZapBall { get; private set; }
        public IPgAIAbility GoblinHateZapBall2 { get; private set; }
        public IPgAIAbility RhinoHorn { get; private set; }
        public IPgAIAbility RhinoRage { get; private set; }
        public IPgAIAbility RhinoFireball { get; private set; }
        public IPgAIAbility RhinoBossRage { get; private set; }
        public IPgAIAbility YetiPunch { get; private set; }
        public IPgAIAbility YetiEncase { get; private set; }
        public IPgAIAbility YetiDebuff { get; private set; }
        public IPgAIAbility YetiBoulderThrow { get; private set; }
        public IPgAIAbility YetiBarrage { get; private set; }
        public IPgAIAbility YetiRoarStun { get; private set; }
        public IPgAIAbility YetiFlingAway { get; private set; }
        public IPgAIAbility YetiIceBallThrow { get; private set; }
        public IPgAIAbility YetiIceSpear { get; private set; }
        public IPgAIAbility YetiColdOrb { get; private set; }
        public IPgAIAbility YetiFrostRing { get; private set; }
        public IPgAIAbility WorgBite { get; private set; }
        public IPgAIAbility WorgOmegaBite { get; private set; }
        public IPgAIAbility BearBite { get; private set; }
        public IPgAIAbility BearCrush { get; private set; }
        public IPgAIAbility BrainBite { get; private set; }
        public IPgAIAbility BrainDrain { get; private set; }
        public IPgAIAbility BrainDrain2 { get; private set; }
        public IPgAIAbility BigCatClaw { get; private set; }
        public IPgAIAbility BigCatPounce { get; private set; }
        public IPgAIAbility BigCatRagePounce { get; private set; }
        public IPgAIAbility BigCatDebuff { get; private set; }
        public IPgAIAbility SpiderBite { get; private set; }
        public IPgAIAbility SpiderFireball { get; private set; }
        public IPgAIAbility SpiderBossFreePin { get; private set; }
        public IPgAIAbility SpiderKill2 { get; private set; }
        public IPgAIAbility SpiderInject { get; private set; }
        public IPgAIAbility SpiderKill { get; private set; }
        public IPgAIAbility AcidBall1 { get; private set; }
        public IPgAIAbility SpiderPin { get; private set; }
        public IPgAIAbility SpiderKill3 { get; private set; }
        public IPgAIAbility AcidBall2 { get; private set; }
        public IPgAIAbility AcidSpew1 { get; private set; }
        public IPgAIAbility AcidExplosion1 { get; private set; }
        public IPgAIAbility AcidExplosion2 { get; private set; }
        public IPgAIAbility SpiderIncubate { get; private set; }
        public IPgAIAbility MantisClaw { get; private set; }
        public IPgAIAbility MantisRage { get; private set; }
        public IPgAIAbility MantisAcidBurst { get; private set; }
        public IPgAIAbility SherzatClaw { get; private set; }
        public IPgAIAbility SherzatAcidSpit { get; private set; }
        public IPgAIAbility SherzatDisintegrate { get; private set; }
        public IPgAIAbility MantisSwipe { get; private set; }
        public IPgAIAbility MantisBlast { get; private set; }
        public IPgAIAbility SnailStrike { get; private set; }
        public IPgAIAbility SnailRage { get; private set; }
        public IPgAIAbility SnailRageB { get; private set; }
        public IPgAIAbility SnailRageC { get; private set; }
        public IPgAIAbility HookClaw { get; private set; }
        public IPgAIAbility HookAcid { get; private set; }
        public IPgAIAbility HookRage { get; private set; }
        public IPgAIAbility UndeadSword1 { get; private set; }
        public IPgAIAbility UndeadSword2 { get; private set; }
        public IPgAIAbility UndeadSwordAngry { get; private set; }
        public IPgAIAbility UndeadMegaSword1 { get; private set; }
        public IPgAIAbility UndeadMegaSword2 { get; private set; }
        public IPgAIAbility UndeadMegaSwordAngry { get; private set; }
        public IPgAIAbility UndeadLightningSmite { get; private set; }
        public IPgAIAbility UndeadPhysicalShield { get; private set; }
        public IPgAIAbility UndeadSword1B { get; private set; }
        public IPgAIAbility UndeadFireballA { get; private set; }
        public IPgAIAbility UndeadFireballB { get; private set; }
        public IPgAIAbility UndeadFireballB2 { get; private set; }
        public IPgAIAbility UndeadIceBall1 { get; private set; }
        public IPgAIAbility UndeadFreezeBall { get; private set; }
        public IPgAIAbility UndeadFireballLongA { get; private set; }
        public IPgAIAbility UndeadFireballLongB { get; private set; }
        public IPgAIAbility UndeadFireballLongB2 { get; private set; }
        public IPgAIAbility KhyrulekCurseBall { get; private set; }
        public IPgAIAbility UrsulaFireball1 { get; private set; }
        public IPgAIAbility UrsulaFireball1B { get; private set; }
        public IPgAIAbility UrsulaFireball2 { get; private set; }
        public IPgAIAbility UrsulaIceball1 { get; private set; }
        public IPgAIAbility UrsulaIceball1B { get; private set; }
        public IPgAIAbility UrsulaIceball2 { get; private set; }
        public IPgAIAbility UrsulaSummon { get; private set; }
        public IPgAIAbility UrsulaRage { get; private set; }
        public IPgAIAbility UndeadFireballA2 { get; private set; }
        public IPgAIAbility BigHeadCurseball { get; private set; }
        public IPgAIAbility UndeadArrow1 { get; private set; }
        public IPgAIAbility UndeadArrow2 { get; private set; }
        public IPgAIAbility UndeadOmegaArrow { get; private set; }
        public IPgAIAbility PetUndeadArrow1 { get; private set; }
        public IPgAIAbility PetUndeadArrow2 { get; private set; }
        public IPgAIAbility PetUndeadOmegaArrow { get; private set; }
        public IPgAIAbility UndeadGrappleArrow1 { get; private set; }
        public IPgAIAbility UndeadSelfDestruct { get; private set; }
        public IPgAIAbility UndeadDarknessBall { get; private set; }
        public IPgAIAbility UndeadBoneWhirlwind { get; private set; }
        public IPgAIAbility PetUndeadSword1 { get; private set; }
        public IPgAIAbility PetUndeadSword2 { get; private set; }
        public IPgAIAbility PetUndeadSwordAngry { get; private set; }
        public IPgAIAbility PetUndeadFireballA { get; private set; }
        public IPgAIAbility PetUndeadFireballB { get; private set; }
        public IPgAIAbility PetUndeadDefensiveBurst { get; private set; }
        public IPgAIAbility PetUndeadPunch1 { get; private set; }
        public IPgAIAbility ZombiePunch { get; private set; }
        public IPgAIAbility ZombieBite { get; private set; }
        public IPgAIAbility GoblinSpear1 { get; private set; }
        public IPgAIAbility GoblinSpear2 { get; private set; }
        public IPgAIAbility GoblinRageSpear1 { get; private set; }
        public IPgAIAbility GoblinRageSpear2 { get; private set; }
        public IPgAIAbility GoblinHeal1 { get; private set; }
        public IPgAIAbility GoblinHeal2 { get; private set; }
        public IPgAIAbility GoblinPunch { get; private set; }
        public IPgAIAbility GoblinArrow1 { get; private set; }
        public IPgAIAbility GoblinArrow2 { get; private set; }
        public IPgAIAbility GoblinRageArrow1 { get; private set; }
        public IPgAIAbility GoblinRageArrow2 { get; private set; }
        public IPgAIAbility GoblinSpreadZapBall { get; private set; }
        public IPgAIAbility GoblinBossLightning { get; private set; }
        public IPgAIAbility GoblinArmorBuff { get; private set; }
        public IPgAIAbility MummySlamA { get; private set; }
        public IPgAIAbility MummySlamB { get; private set; }
        public IPgAIAbility MummySlamCombo { get; private set; }
        public IPgAIAbility MummyWrapA { get; private set; }
        public IPgAIAbility MummyWrapB { get; private set; }
        public IPgAIAbility MummyWrapRage { get; private set; }
        public IPgAIAbility BarutiWrapA { get; private set; }
        public IPgAIAbility BarutiWrapB { get; private set; }
        public IPgAIAbility BarutiWrapRage { get; private set; }
        public IPgAIAbility FireWallAttack1 { get; private set; }
        public IPgAIAbility FireWallDotAttack1 { get; private set; }
        public IPgAIAbility FireSnakeExplosion1 { get; private set; }
        public IPgAIAbility FireTrapAttack1 { get; private set; }
        public IPgAIAbility HealingAura1 { get; private set; }
        public IPgAIAbility HealingAura2 { get; private set; }
        public IPgAIAbility HealingAura3 { get; private set; }
        public IPgAIAbility HealingAura4 { get; private set; }
        public IPgAIAbility DruidHealingSanctuaryHeal { get; private set; }
        public IPgAIAbility AcidAuraBall1 { get; private set; }
        public IPgAIAbility AcidAuraBall2 { get; private set; }
        public IPgAIAbility AcidAuraBall3 { get; private set; }
        public IPgAIAbility AcidAuraBall4 { get; private set; }
        public IPgAIAbility ElectricityAura1 { get; private set; }
        public IPgAIAbility ElectricityAuraBolt1 { get; private set; }
        public IPgAIAbility ReboundAura1 { get; private set; }
        public IPgAIAbility ColdAuraBurst { get; private set; }
        public IPgAIAbility WebStick { get; private set; }
        public IPgAIAbility IcySlam { get; private set; }
        public IPgAIAbility IcyCocoon { get; private set; }
        public IPgAIAbility IcyCocoon2 { get; private set; }
        public IPgAIAbility ElementalSlam { get; private set; }
        public IPgAIAbility ElementalBees { get; private set; }
        public IPgAIAbility ElementalBees2 { get; private set; }
        public IPgAIAbility FaeLightningSmite { get; private set; }
        public IPgAIAbility TotalHorrorAttack { get; private set; }
        public IPgAIAbility TotalHorrorStretch { get; private set; }
        public IPgAIAbility TotalHorrorHeal { get; private set; }
        public IPgAIAbility TotalHorrorHeal2 { get; private set; }
        public IPgAIAbility SheepBomb1 { get; private set; }
        public IPgAIAbility SlugPoisonBite { get; private set; }
        public IPgAIAbility SlugPoisonRage { get; private set; }
        public IPgAIAbility SlugPoisonBite2 { get; private set; }
        public IPgAIAbility SlugPoisonRage2 { get; private set; }
        public IPgAIAbility SlugPoisonBite3 { get; private set; }
        public IPgAIAbility SlugPoisonRage3 { get; private set; }
        public IPgAIAbility TornadoJolt1 { get; private set; }
        public IPgAIAbility TornadoFling { get; private set; }
        public IPgAIAbility TornadoToss { get; private set; }
        public IPgAIAbility TheFogCurse { get; private set; }
        public IPgAIAbility MonsterWerewolfPouncingRake { get; private set; }
        public IPgAIAbility MonsterWerewolfPackAttack { get; private set; }
        public IPgAIAbility MonsterWerewolfHowl { get; private set; }
        public IPgAIAbility Werewolf_Summon_Rage { get; private set; }
        public IPgAIAbility Werewolf_Summon_Opener { get; private set; }
        public IPgAIAbility BleddynHowl { get; private set; }
        public IPgAIAbility OrcSwordSlash { get; private set; }
        public IPgAIAbility OrcParry { get; private set; }
        public IPgAIAbility OrcFinishingBlow { get; private set; }
        public IPgAIAbility OrcHipThrow { get; private set; }
        public IPgAIAbility OrcKneeKick { get; private set; }
        public IPgAIAbility OrcPunch { get; private set; }
        public IPgAIAbility OrcArrow1 { get; private set; }
        public IPgAIAbility OrcArrow2 { get; private set; }
        public IPgAIAbility OrcStaffSmash { get; private set; }
        public IPgAIAbility OrcFireball { get; private set; }
        public IPgAIAbility OrcHeal1 { get; private set; }
        public IPgAIAbility OrcHeal2 { get; private set; }
        public IPgAIAbility OrcEvasionBubble { get; private set; }
        public IPgAIAbility OrcElectricStun { get; private set; }
        public IPgAIAbility OrcFireBolts { get; private set; }
        public IPgAIAbility OrcKnockbackBolt { get; private set; }
        public IPgAIAbility OrcSummonUrak2 { get; private set; }
        public IPgAIAbility OrcSwordSlashFire { get; private set; }
        public IPgAIAbility OrcParryFire { get; private set; }
        public IPgAIAbility OrcFinishingBlowFire { get; private set; }
        public IPgAIAbility OrcSummonSigil1 { get; private set; }
        public IPgAIAbility OrcSpearAttack { get; private set; }
        public IPgAIAbility OrcHalberdAttack { get; private set; }
        public IPgAIAbility OrcAreaHalberdAttack { get; private set; }
        public IPgAIAbility OrcDebuffArrow { get; private set; }
        public IPgAIAbility OrcSlice { get; private set; }
        public IPgAIAbility OrcVenomstrike1 { get; private set; }
        public IPgAIAbility OrcVenomstrike0 { get; private set; }
        public IPgAIAbility OrcLieutenantDebuffTaunt { get; private set; }
        public IPgAIAbility OrcAreaHalberdBoss { get; private set; }
        public IPgAIAbility OrcDeathsHold { get; private set; }
        public IPgAIAbility GazlukPriest1Special { get; private set; }
        public IPgAIAbility GazlukPriest2Special { get; private set; }
        public IPgAIAbility GazlukPriest3Special { get; private set; }
        public IPgAIAbility OrcExtinguishLife { get; private set; }
        public IPgAIAbility OrcDarknessBall { get; private set; }
        public IPgAIAbility OrcWaveOfDarkness { get; private set; }
        public IPgAIAbility EnemyMinigolemPunch { get; private set; }
        public IPgAIAbility EnemyMinigolemHeal { get; private set; }
        public IPgAIAbility EnemyMinigolemExplode { get; private set; }
        public IPgAIAbility MinigolemBombToss { get; private set; }
        public IPgAIAbility MinigolemBombToss2 { get; private set; }
        public IPgAIAbility MinigolemBombToss3 { get; private set; }
        public IPgAIAbility MinigolemBombToss4 { get; private set; }
        public IPgAIAbility MinigolemBombToss5 { get; private set; }
        public IPgAIAbility MinigolemAoEHeal { get; private set; }
        public IPgAIAbility MinigolemAoEHeal2 { get; private set; }
        public IPgAIAbility MinigolemAoEHeal3 { get; private set; }
        public IPgAIAbility MinigolemAoEHeal4 { get; private set; }
        public IPgAIAbility MinigolemAoEHeal5 { get; private set; }
        public IPgAIAbility MinigolemSelfDestruct { get; private set; }
        public IPgAIAbility MinigolemSelfDestruct2 { get; private set; }
        public IPgAIAbility MinigolemSelfDestruct3 { get; private set; }
        public IPgAIAbility MinigolemSelfDestruct4 { get; private set; }
        public IPgAIAbility MinigolemSelfDestruct5 { get; private set; }
        public IPgAIAbility MinigolemAoEPower { get; private set; }
        public IPgAIAbility MinigolemAoEPower2 { get; private set; }
        public IPgAIAbility MinigolemAoEPower3 { get; private set; }
        public IPgAIAbility MinigolemAoEPower4 { get; private set; }
        public IPgAIAbility MinigolemAoEPower5 { get; private set; }
        public IPgAIAbility MinigolemHeal { get; private set; }
        public IPgAIAbility MinigolemHeal2 { get; private set; }
        public IPgAIAbility MinigolemHeal3 { get; private set; }
        public IPgAIAbility MinigolemHeal4 { get; private set; }
        public IPgAIAbility MinigolemHeal5 { get; private set; }
        public IPgAIAbility MinigolemDoomAdmixture { get; private set; }
        public IPgAIAbility MinigolemDoomAdmixture2 { get; private set; }
        public IPgAIAbility MinigolemDoomAdmixture3 { get; private set; }
        public IPgAIAbility MinigolemDoomAdmixture4 { get; private set; }
        public IPgAIAbility MinigolemDoomAdmixture5 { get; private set; }
        public IPgAIAbility MinigolemSelfSacrifice { get; private set; }
        public IPgAIAbility MinigolemSelfSacrifice2 { get; private set; }
        public IPgAIAbility MinigolemSelfSacrifice3 { get; private set; }
        public IPgAIAbility MinigolemSelfSacrifice4 { get; private set; }
        public IPgAIAbility MinigolemSelfSacrifice5 { get; private set; }
        public IPgAIAbility MinigolemPunch { get; private set; }
        public IPgAIAbility MinigolemPunch2 { get; private set; }
        public IPgAIAbility MinigolemPunch3 { get; private set; }
        public IPgAIAbility MinigolemPunch4 { get; private set; }
        public IPgAIAbility MinigolemPunch5 { get; private set; }
        public IPgAIAbility MinigolemHasteConcoction1 { get; private set; }
        public IPgAIAbility MinigolemHasteConcoction2 { get; private set; }
        public IPgAIAbility MinigolemHasteConcoction3 { get; private set; }
        public IPgAIAbility MinigolemFireBalm1 { get; private set; }
        public IPgAIAbility MinigolemFireBalm2 { get; private set; }
        public IPgAIAbility MinigolemFireBalm3 { get; private set; }
        public IPgAIAbility MinigolemFireBalm4 { get; private set; }
        public IPgAIAbility MinigolemFireBalm5 { get; private set; }
        public IPgAIAbility MinigolemRageAoEHeal1 { get; private set; }
        public IPgAIAbility MinigolemRageAoEHeal2 { get; private set; }
        public IPgAIAbility MinigolemRageAoEHeal3 { get; private set; }
        public IPgAIAbility MinigolemRageAoEHeal4 { get; private set; }
        public IPgAIAbility MinigolemRageAoEHeal5 { get; private set; }
        public IPgAIAbility MinigolemRageAcidToss1 { get; private set; }
        public IPgAIAbility MinigolemRageAcidToss2 { get; private set; }
        public IPgAIAbility MinigolemRageAcidToss3 { get; private set; }
        public IPgAIAbility MinigolemRageAcidToss4 { get; private set; }
        public IPgAIAbility MinigolemRageAcidToss5 { get; private set; }
        public IPgAIAbility TrainingGolemPunch { get; private set; }
        public IPgAIAbility TrainingGolemStun { get; private set; }
        public IPgAIAbility TrainingGolemHeal { get; private set; }
        public IPgAIAbility TrainingGolemHealB { get; private set; }
        public IPgAIAbility TrainingGolemFireBreath { get; private set; }
        public IPgAIAbility TrainingGolemFireBurst { get; private set; }
        public IPgAIAbility GrimalkinClaw { get; private set; }
        public IPgAIAbility GrimalkinBite { get; private set; }
        public IPgAIAbility GrimalkinPuncture { get; private set; }
        public IPgAIAbility WerewolfSword1 { get; private set; }
        public IPgAIAbility WerewolfSword2 { get; private set; }
        public IPgAIAbility WerewolfSwordStun { get; private set; }
        public IPgAIAbility WerewolfArrow1 { get; private set; }
        public IPgAIAbility WerewolfArrow2 { get; private set; }
        public IPgAIAbility WerewolfOmegaArrow { get; private set; }
        public IPgAIAbility NpcSmash { get; private set; }
        public IPgAIAbility NpcDoubleHitCurse { get; private set; }
        public IPgAIAbility NpcBlockingStance { get; private set; }
        public IPgAIAbility NpcHeadcracker { get; private set; }
        public IPgAIAbility StrigaClawA { get; private set; }
        public IPgAIAbility StrigaClawB { get; private set; }
        public IPgAIAbility StrigaReap { get; private set; }
        public IPgAIAbility StrigaReap2 { get; private set; }
        public IPgAIAbility StrigaFireBreath { get; private set; }
        public IPgAIAbility StrigaBuff { get; private set; }
        public IPgAIAbility GhostlyPunchA { get; private set; }
        public IPgAIAbility GhostlyPunchB { get; private set; }
        public IPgAIAbility GhostlyBurst { get; private set; }
        public IPgAIAbility GhostlyBossPunchA { get; private set; }
        public IPgAIAbility GhostlyBossPunchB { get; private set; }
        public IPgAIAbility GhostlyBossBurst { get; private set; }
        public IPgAIAbility GhostlyBolt { get; private set; }
        public IPgAIAbility InjectorBugBite { get; private set; }
        public IPgAIAbility InjectorBugInject { get; private set; }
        public IPgAIAbility InjectorBugInject2 { get; private set; }
        public IPgAIAbility FaceOfDeathKill { get; private set; }
        public IPgAIAbility WatcherFireball { get; private set; }
        public IPgAIAbility WatcherSlap { get; private set; }
        public IPgAIAbility WatcherAcidball { get; private set; }
        public IPgAIAbility RedCrystalBlast { get; private set; }
        public IPgAIAbility RedCrystalBurst { get; private set; }
        public IPgAIAbility TurretCrystalZap { get; private set; }
        public IPgAIAbility TurretCrystalZap2 { get; private set; }
        public IPgAIAbility TurretCrystalZapLongRange { get; private set; }
        public IPgAIAbility TurretCrystalZapLongRange2 { get; private set; }
        public IPgAIAbility DeathRay { get; private set; }
        public IPgAIAbility SpyPortalZap { get; private set; }
        public IPgAIAbility SpyPortalZap2 { get; private set; }
        public IPgAIAbility BitingVineBite { get; private set; }
        public IPgAIAbility BitingVineSpit { get; private set; }
        public IPgAIAbility BitingVineSpitB { get; private set; }
        public IPgAIAbility BitingVineCast { get; private set; }
        public IPgAIAbility BitingVineAppear { get; private set; }
        public IPgAIAbility BitingVineDisappear { get; private set; }
        public IPgAIAbility TrollClubA { get; private set; }
        public IPgAIAbility TrollClubB { get; private set; }
        public IPgAIAbility TrollKnockdown { get; private set; }
        public IPgAIAbility OgreClubA { get; private set; }
        public IPgAIAbility OgreClubB { get; private set; }
        public IPgAIAbility OgreThrow { get; private set; }
        public IPgAIAbility OgreStun { get; private set; }
        public IPgAIAbility FaeSwordA { get; private set; }
        public IPgAIAbility FaeSwordB { get; private set; }
        public IPgAIAbility FaeSwordKill { get; private set; }
        public IPgAIAbility DementiaPuckCurse { get; private set; }
        public IPgAIAbility FaeLightningSmiteHidden { get; private set; }
        public IPgAIAbility NecroSpark { get; private set; }
        public IPgAIAbility NecroDarknessWave { get; private set; }
        public IPgAIAbility NecroPainBubble { get; private set; }
        public IPgAIAbility NecroSparkPerching { get; private set; }
        public IPgAIAbility NecroDeathsHold { get; private set; }
        public IPgAIAbility DroachBiteA { get; private set; }
        public IPgAIAbility DroachBiteB { get; private set; }
        public IPgAIAbility DroachFireball { get; private set; }
        public IPgAIAbility DroachFireballPerching { get; private set; }
        public IPgAIAbility DroachBreatheFire { get; private set; }
        public IPgAIAbility DroachLightning { get; private set; }
        public IPgAIAbility DroachLightningPerching { get; private set; }
        public IPgAIAbility DroachShockingKnockback { get; private set; }
        public IPgAIAbility BasiliskClawA { get; private set; }
        public IPgAIAbility BasiliskClawB { get; private set; }
        public IPgAIAbility BasiliskToxicBite { get; private set; }
        public IPgAIAbility BasiliskDebuff { get; private set; }
        public IPgAIAbility BasiliskCastPerching { get; private set; }
        public IPgAIAbility CultistArrow1 { get; private set; }
        public IPgAIAbility CultistArrow2 { get; private set; }
        public IPgAIAbility CultistOmegaArrow { get; private set; }
        public IPgAIAbility CultistSword1 { get; private set; }
        public IPgAIAbility CultistSword2 { get; private set; }
        public IPgAIAbility CultistSwordStun { get; private set; }
        public IPgAIAbility BossMegaSword1 { get; private set; }
        public IPgAIAbility BossMegaSword2 { get; private set; }
        public IPgAIAbility SedgewickMegaSwordAngry { get; private set; }
        public IPgAIAbility BossMegaHammer { get; private set; }
        public IPgAIAbility BossMegaHammer2 { get; private set; }
        public IPgAIAbility BossMegaRageHammer { get; private set; }
        public IPgAIAbility ClaudiaTundraSpikes { get; private set; }
        public IPgAIAbility ClaudiaIceSpear { get; private set; }
        public IPgAIAbility ClaudiaBlizzard { get; private set; }
        public IPgAIAbility BigGolemHitA { get; private set; }
        public IPgAIAbility BigGolemHitB { get; private set; }
        public IPgAIAbility BigGolemFlingBoss { get; private set; }
        public IPgAIAbility BigGolemPerchFix { get; private set; }
        public IPgAIAbility BigGolemFlingBoss2 { get; private set; }
        public IPgAIAbility BigGolemSummonFireSnake { get; private set; }
        public IPgAIAbility BigGolemHitB_NoDisable { get; private set; }
        public IPgAIAbility BigGolemHitA_NoDisable { get; private set; }
        public IPgAIAbility BigGolemFling { get; private set; }
        public IPgAIAbility GhoulClawA { get; private set; }
        public IPgAIAbility GhoulClawB { get; private set; }
        public IPgAIAbility GhoulSelfBuff { get; private set; }
        public IPgAIAbility GhoulHammerA { get; private set; }
        public IPgAIAbility GhoulHammerB { get; private set; }
        public IPgAIAbility DragonWormSpitElectricity { get; private set; }
        public IPgAIAbility DragonWormBite { get; private set; }
        public IPgAIAbility DragonWormSmack { get; private set; }
        public IPgAIAbility DragonWormRage { get; private set; }
        public IPgAIAbility DragonWormEscape { get; private set; }
        public IPgAIAbility DragonWormSpitFire { get; private set; }
        public IPgAIAbility ColdSphereBurst { get; private set; }
        public IPgAIAbility ColdSphereFreezeBurst { get; private set; }
        public IPgAIAbility ManticoreBite { get; private set; }
        public IPgAIAbility ManticoreClaw { get; private set; }
        public IPgAIAbility ManticoreSting1 { get; private set; }
        public IPgAIAbility Manticoresting2 { get; private set; }
        public IPgAIAbility RakStaffHit { get; private set; }
        public IPgAIAbility RakStaffPin { get; private set; }
        public IPgAIAbility RakStaffBlock { get; private set; }
        public IPgAIAbility RakStaffHeavy { get; private set; }
        public IPgAIAbility RakSlash { get; private set; }
        public IPgAIAbility RakKnee { get; private set; }
        public IPgAIAbility RakKick { get; private set; }
        public IPgAIAbility RakBarrage { get; private set; }
        public IPgAIAbility RakSwordSlash { get; private set; }
        public IPgAIAbility RakHackingBlade { get; private set; }
        public IPgAIAbility RakDecapitate { get; private set; }
        public IPgAIAbility RakFireball { get; private set; }
        public IPgAIAbility RakBreatheFire { get; private set; }
        public IPgAIAbility RakRingOfFire { get; private set; }
        public IPgAIAbility RakToxinBomb { get; private set; }
        public IPgAIAbility RakAcidBomb { get; private set; }
        public IPgAIAbility RakHealingMist { get; private set; }
        public IPgAIAbility RakBasicShot { get; private set; }
        public IPgAIAbility RakHookShot { get; private set; }
        public IPgAIAbility RakBowBash { get; private set; }
        public IPgAIAbility RakAimedShot { get; private set; }
        public IPgAIAbility RakPoisonArrow { get; private set; }
        public IPgAIAbility RakMindreave { get; private set; }
        public IPgAIAbility RakPainBubble { get; private set; }
        public IPgAIAbility RakPanicCharge { get; private set; }
        public IPgAIAbility RakRevitalize { get; private set; }
        public IPgAIAbility RakReconstruct { get; private set; }
        public IPgAIAbility RakBossSlow { get; private set; }
        public IPgAIAbility RakBossPerchSlow { get; private set; }
        public IPgAIAbility FlapSkullBite { get; private set; }
        public IPgAIAbility FlapSkullBigBite { get; private set; }
        public IPgAIAbility MinotaurClub { get; private set; }
        public IPgAIAbility MinotaurRageClub { get; private set; }
        public IPgAIAbility MinotaurBoulder { get; private set; }
        public IPgAIAbility MinotaurBossRageClub { get; private set; }
        public IPgAIAbility CockatricePeck { get; private set; }
        public IPgAIAbility CockatriceTailWhip { get; private set; }
        public IPgAIAbility CockatriceParalyze { get; private set; }
        public IPgAIAbility GiantBeetleBite { get; private set; }
        public IPgAIAbility GiantBeetleInject { get; private set; }
        public IPgAIAbility GiantBeetleBoulderSpit { get; private set; }
        public IPgAIAbility BatIllusionSlashA { get; private set; }
        public IPgAIAbility BatIllusionSlashB { get; private set; }
        public IPgAIAbility BatIllusionBite { get; private set; }
        public IPgAIAbility GiantBatSlashA { get; private set; }
        public IPgAIAbility GiantBatSlashB { get; private set; }
        public IPgAIAbility GiantBatBite { get; private set; }
        public IPgAIAbility HagAgingTouch { get; private set; }
        public IPgAIAbility HagAgingScream { get; private set; }
        public IPgAIAbility TriffidClawA { get; private set; }
        public IPgAIAbility TriffidClawB { get; private set; }
        public IPgAIAbility TriffidTongue { get; private set; }
        public IPgAIAbility TriffidSpore { get; private set; }
        public IPgAIAbility TriffidShot { get; private set; }
        public IPgAIAbility TriffidTongueElite { get; private set; }
        public IPgAIAbility GiantScorpionClawA { get; private set; }
        public IPgAIAbility GiantScorpionClawB { get; private set; }
        public IPgAIAbility GiantScorpionSting { get; private set; }
        public IPgAIAbility KrakenBeak { get; private set; }
        public IPgAIAbility KrakenSlam { get; private set; }
        public IPgAIAbility KrakenRage { get; private set; }
        public IPgAIAbility KrakenBabyBeak { get; private set; }
        public IPgAIAbility KrakenBabySlam { get; private set; }
        public IPgAIAbility KrakenBabyRage { get; private set; }
        public IPgAIAbility RanalonHit { get; private set; }
        public IPgAIAbility RanalonHitB { get; private set; }
        public IPgAIAbility RanalonKick { get; private set; }
        public IPgAIAbility RanalonTongue { get; private set; }
        public IPgAIAbility RanalonZap { get; private set; }
        public IPgAIAbility RanalonZapB { get; private set; }
        public IPgAIAbility RanalonHeal { get; private set; }
        public IPgAIAbility RanalonRoot { get; private set; }
        public IPgAIAbility RanalonSelfBuff { get; private set; }
        public IPgAIAbility RanalonSelfBuffElite { get; private set; }
        public IPgAIAbility RanalonGuardianStab { get; private set; }
        public IPgAIAbility RanalonGuardianStabB { get; private set; }
        public IPgAIAbility RanalonGuardianBite { get; private set; }
        public IPgAIAbility RanalonGuardianBlind { get; private set; }
        public IPgAIAbility RanalonDoctrineKeeperStab { get; private set; }
        public IPgAIAbility RanalonDoctrineKeeperBlind { get; private set; }
        public IPgAIAbility BarghestBiteA { get; private set; }
        public IPgAIAbility BarghestBiteB { get; private set; }
        public IPgAIAbility BarghestDebuff { get; private set; }
        public IPgAIAbility WorghestDebuff { get; private set; }
        public IPgAIAbility BallistaFire { get; private set; }
        public IPgAIAbility BallistaFire_Long { get; private set; }
        public IPgAIAbility GargoyleSlamA { get; private set; }
        public IPgAIAbility GargoyleSlamB { get; private set; }
        public IPgAIAbility GargoyleStun { get; private set; }
        public IPgAIAbility GargoyleBossStun { get; private set; }
        public IPgAIAbility ScrayBite { get; private set; }
        public IPgAIAbility ScrayStab { get; private set; }
        public IPgAIAbility HippoBite { get; private set; }
        public IPgAIAbility HippoBiteAndHeal1 { get; private set; }
        public IPgAIAbility BigCatClaw_Pet { get; private set; }
        public IPgAIAbility BigCatPounce_Pet { get; private set; }
        public IPgAIAbility BigCatKill_Pet1 { get; private set; }
        public IPgAIAbility BigCatKill_Pet2 { get; private set; }
        public IPgAIAbility BigCatKill_Pet3 { get; private set; }
        public IPgAIAbility BigCatKill_Pet4 { get; private set; }
        public IPgAIAbility BigCatKill_Pet5 { get; private set; }
        public IPgAIAbility BigCatKill_Pet6 { get; private set; }
        public IPgAIAbility BigCatUltraKill_Pet1 { get; private set; }
        public IPgAIAbility BigCatUltraKill_Pet2 { get; private set; }
        public IPgAIAbility BigCatUltraKill_Pet3 { get; private set; }
        public IPgAIAbility BigCatUltraKill_Pet4 { get; private set; }
        public IPgAIAbility BigCatUltraKill_Pet5 { get; private set; }
        public IPgAIAbility BigCatUltraKill_Pet6 { get; private set; }
        public IPgAIAbility BigCatRoot_Pet1 { get; private set; }
        public IPgAIAbility BigCatRoot_Pet2 { get; private set; }
        public IPgAIAbility BigCatRoot_Pet3 { get; private set; }
        public IPgAIAbility BigCatRoot_Pet4 { get; private set; }
        public IPgAIAbility BigCatVuln_Pet1 { get; private set; }
        public IPgAIAbility BigCatVuln_Pet2 { get; private set; }
        public IPgAIAbility BigCatVuln_Pet3 { get; private set; }
        public IPgAIAbility BigCatVuln_Pet4 { get; private set; }
        public IPgAIAbility BigCatHeal_Pet1 { get; private set; }
        public IPgAIAbility BigCatHeal_Pet2 { get; private set; }
        public IPgAIAbility BigCatHeal_Pet3 { get; private set; }
        public IPgAIAbility BigCatHeal_Pet4 { get; private set; }
        public IPgAIAbility BigCatHeal_Pet5 { get; private set; }
        public IPgAIAbility BigCatHeal_Pet6 { get; private set; }
        public IPgAIAbility GrimalkinPuncture_Pet1 { get; private set; }
        public IPgAIAbility GrimalkinPuncture_Pet2 { get; private set; }
        public IPgAIAbility GrimalkinPuncture_Pet3 { get; private set; }
        public IPgAIAbility GrimalkinPuncture_Pet4 { get; private set; }
        public IPgAIAbility GrimalkinPuncture_Pet5 { get; private set; }
        public IPgAIAbility GrimalkinPuncture_Pet6 { get; private set; }
        public IPgAIAbility GrimalkinFlee_Pet1 { get; private set; }
        public IPgAIAbility GrimalkinFlee_Pet2 { get; private set; }
        public IPgAIAbility GrimalkinFlee_Pet3 { get; private set; }
        public IPgAIAbility RatBite_Pet { get; private set; }
        public IPgAIAbility RatClaw_Pet { get; private set; }
        public IPgAIAbility RatDeRage_Pet1 { get; private set; }
        public IPgAIAbility RatDeRage_Pet2 { get; private set; }
        public IPgAIAbility RatDeRage_Pet3 { get; private set; }
        public IPgAIAbility RatDeRage_Pet4 { get; private set; }
        public IPgAIAbility RatDeRage_Pet5 { get; private set; }
        public IPgAIAbility RatDeRage_Pet6 { get; private set; }
        public IPgAIAbility RatHeal_Pet1 { get; private set; }
        public IPgAIAbility RatHeal_Pet2 { get; private set; }
        public IPgAIAbility RatHeal_Pet3 { get; private set; }
        public IPgAIAbility RatHeal_Pet4 { get; private set; }
        public IPgAIAbility RatHeal_Pet5 { get; private set; }
        public IPgAIAbility RatHeal_Pet6 { get; private set; }
        public IPgAIAbility RatVuln_Pet1 { get; private set; }
        public IPgAIAbility RatVuln_Pet2 { get; private set; }
        public IPgAIAbility RatVuln_Pet3 { get; private set; }
        public IPgAIAbility RatVuln_Pet4 { get; private set; }
        public IPgAIAbility FireRatBite_Pet { get; private set; }
        public IPgAIAbility FireRatClaw_Pet { get; private set; }
        public IPgAIAbility RatBurn_Pet1 { get; private set; }
        public IPgAIAbility RatBurn_Pet2 { get; private set; }
        public IPgAIAbility RatBurn_Pet3 { get; private set; }
        public IPgAIAbility RatBurn_Pet4 { get; private set; }
        public IPgAIAbility RatBurn_Pet5 { get; private set; }
        public IPgAIAbility RatBurn_Pet6 { get; private set; }
        public IPgAIAbility BearBite_Pet { get; private set; }
        public IPgAIAbility BearClaw_Pet { get; private set; }
        public IPgAIAbility BearTaunt_Pet1 { get; private set; }
        public IPgAIAbility BearTaunt_Pet2 { get; private set; }
        public IPgAIAbility BearTaunt_Pet3 { get; private set; }
        public IPgAIAbility BearTaunt_Pet4 { get; private set; }
        public IPgAIAbility BearTaunt_Pet5 { get; private set; }
        public IPgAIAbility BearTaunt_Pet6 { get; private set; }
        public IPgAIAbility BearStun_Pet1 { get; private set; }
        public IPgAIAbility BearStun_Pet2 { get; private set; }
        public IPgAIAbility BearStun_Pet3 { get; private set; }
        public IPgAIAbility BearStun_Pet4 { get; private set; }
        public IPgAIAbility BearWarmth_Pet1 { get; private set; }
        public IPgAIAbility BearWarmth_Pet2 { get; private set; }
        public IPgAIAbility BearWarmth_Pet3 { get; private set; }
        public IPgAIAbility BearWarmth_Pet4 { get; private set; }
        public IPgAIAbility BearWarmth_Pet5 { get; private set; }
        public IPgAIAbility BearWarmth_Pet6 { get; private set; }
        public IPgAIAbility BearSelfHeal_Pet1 { get; private set; }
        public IPgAIAbility BearSelfHeal_Pet2 { get; private set; }
        public IPgAIAbility BearSelfHeal_Pet3 { get; private set; }
        public IPgAIAbility BearSelfHeal_Pet4 { get; private set; }
        public IPgAIAbility BearSelfHeal_Pet5 { get; private set; }
        public IPgAIAbility BearSelfHeal_Pet6 { get; private set; }
        public IPgAIAbility BearUltra_Pet1 { get; private set; }
        public IPgAIAbility BearUltra_Pet2 { get; private set; }
        public IPgAIAbility BearUltra_Pet3 { get; private set; }
        public IPgAIAbility BearUltra_Pet4 { get; private set; }
        public IPgAIAbility BearUltra_Pet5 { get; private set; }
        public IPgAIAbility BearUltra_Pet6 { get; private set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return Key; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
           { "AcidAuraBall1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidAuraBall1 = 
                   JsonObjectParser<AIAbility>.Parse("AcidAuraBall1", value, errorInfo),
                GetObject = () => AcidAuraBall1 as IObjectContentGenerator } },
           { "AcidAuraBall2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidAuraBall2 = 
                   JsonObjectParser<AIAbility>.Parse("AcidAuraBall2", value, errorInfo),
                GetObject = () => AcidAuraBall2 as IObjectContentGenerator } },
           { "AcidAuraBall3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidAuraBall3 = 
                   JsonObjectParser<AIAbility>.Parse("AcidAuraBall3", value, errorInfo),
                GetObject = () => AcidAuraBall3 as IObjectContentGenerator } },
           { "AcidAuraBall4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidAuraBall4 = 
                   JsonObjectParser<AIAbility>.Parse("AcidAuraBall4", value, errorInfo),
                GetObject = () => AcidAuraBall4 as IObjectContentGenerator } },
           { "AcidBall1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidBall1 = 
                   JsonObjectParser<AIAbility>.Parse("AcidBall1", value, errorInfo),
                GetObject = () => AcidBall1 as IObjectContentGenerator } },
           { "AcidBall2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidBall2 = 
                   JsonObjectParser<AIAbility>.Parse("AcidBall2", value, errorInfo),
                GetObject = () => AcidBall2 as IObjectContentGenerator } },
           { "AcidExplosion1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidExplosion1 = 
                   JsonObjectParser<AIAbility>.Parse("AcidExplosion1", value, errorInfo),
                GetObject = () => AcidExplosion1 as IObjectContentGenerator } },
           { "AcidExplosion2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidExplosion2 = 
                   JsonObjectParser<AIAbility>.Parse("AcidExplosion2", value, errorInfo),
                GetObject = () => AcidExplosion2 as IObjectContentGenerator } },
           { "AcidSpew1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AcidSpew1 = 
                   JsonObjectParser<AIAbility>.Parse("AcidSpew1", value, errorInfo),
                GetObject = () => AcidSpew1 as IObjectContentGenerator } },
           { "AlienDog_Punch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AlienDog_Punch = 
                   JsonObjectParser<AIAbility>.Parse("AlienDog_Punch", value, errorInfo),
                GetObject = () => AlienDog_Punch as IObjectContentGenerator } },
           { "AlienDog_Punch2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AlienDog_Punch2 = 
                   JsonObjectParser<AIAbility>.Parse("AlienDog_Punch2", value, errorInfo),
                GetObject = () => AlienDog_Punch2 as IObjectContentGenerator } },
           { "AlienDog_RagePunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AlienDog_RagePunch = 
                   JsonObjectParser<AIAbility>.Parse("AlienDog_RagePunch", value, errorInfo),
                GetObject = () => AlienDog_RagePunch as IObjectContentGenerator } },
           { "AnimalBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalBite = 
                   JsonObjectParser<AIAbility>.Parse("AnimalBite", value, errorInfo),
                GetObject = () => AnimalBite as IObjectContentGenerator } },
           { "AnimalClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalClaw = 
                   JsonObjectParser<AIAbility>.Parse("AnimalClaw", value, errorInfo),
                GetObject = () => AnimalClaw as IObjectContentGenerator } },
           { "AnimalFlee", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalFlee = 
                   JsonObjectParser<AIAbility>.Parse("AnimalFlee", value, errorInfo),
                GetObject = () => AnimalFlee as IObjectContentGenerator } },
           { "AnimalHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHeal = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHeal", value, errorInfo),
                GetObject = () => AnimalHeal as IObjectContentGenerator } },
           { "AnimalHeal2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHeal2 = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHeal2", value, errorInfo),
                GetObject = () => AnimalHeal2 as IObjectContentGenerator } },
           { "AnimalHeal3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHeal3 = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHeal3", value, errorInfo),
                GetObject = () => AnimalHeal3 as IObjectContentGenerator } },
           { "AnimalHoofFieryFrontKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHoofFieryFrontKick = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHoofFieryFrontKick", value, errorInfo),
                GetObject = () => AnimalHoofFieryFrontKick as IObjectContentGenerator } },
           { "AnimalHoofFieryFrontKick2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHoofFieryFrontKick2 = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHoofFieryFrontKick2", value, errorInfo),
                GetObject = () => AnimalHoofFieryFrontKick2 as IObjectContentGenerator } },
           { "AnimalHoofFrontKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHoofFrontKick = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHoofFrontKick", value, errorInfo),
                GetObject = () => AnimalHoofFrontKick as IObjectContentGenerator } },
           { "AnimalHoofRageKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHoofRageKick = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHoofRageKick", value, errorInfo),
                GetObject = () => AnimalHoofRageKick as IObjectContentGenerator } },
           { "AnimalHoofRageKick2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalHoofRageKick2 = 
                   JsonObjectParser<AIAbility>.Parse("AnimalHoofRageKick2", value, errorInfo),
                GetObject = () => AnimalHoofRageKick2 as IObjectContentGenerator } },
           { "AnimalOmegaBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalOmegaBite = 
                   JsonObjectParser<AIAbility>.Parse("AnimalOmegaBite", value, errorInfo),
                GetObject = () => AnimalOmegaBite as IObjectContentGenerator } },
           { "AnimalOmegaBite2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => AnimalOmegaBite2 = 
                   JsonObjectParser<AIAbility>.Parse("AnimalOmegaBite2", value, errorInfo),
                GetObject = () => AnimalOmegaBite2 as IObjectContentGenerator } },
           { "BallistaFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BallistaFire = 
                   JsonObjectParser<AIAbility>.Parse("BallistaFire", value, errorInfo),
                GetObject = () => BallistaFire as IObjectContentGenerator } },
           { "BallistaFire_Long", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BallistaFire_Long = 
                   JsonObjectParser<AIAbility>.Parse("BallistaFire_Long", value, errorInfo),
                GetObject = () => BallistaFire_Long as IObjectContentGenerator } },
           { "BarghestBiteA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BarghestBiteA = 
                   JsonObjectParser<AIAbility>.Parse("BarghestBiteA", value, errorInfo),
                GetObject = () => BarghestBiteA as IObjectContentGenerator } },
           { "BarghestBiteB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BarghestBiteB = 
                   JsonObjectParser<AIAbility>.Parse("BarghestBiteB", value, errorInfo),
                GetObject = () => BarghestBiteB as IObjectContentGenerator } },
           { "BarghestDebuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BarghestDebuff = 
                   JsonObjectParser<AIAbility>.Parse("BarghestDebuff", value, errorInfo),
                GetObject = () => BarghestDebuff as IObjectContentGenerator } },
           { "BarutiWrapA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BarutiWrapA = 
                   JsonObjectParser<AIAbility>.Parse("BarutiWrapA", value, errorInfo),
                GetObject = () => BarutiWrapA as IObjectContentGenerator } },
           { "BarutiWrapB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BarutiWrapB = 
                   JsonObjectParser<AIAbility>.Parse("BarutiWrapB", value, errorInfo),
                GetObject = () => BarutiWrapB as IObjectContentGenerator } },
           { "BarutiWrapRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BarutiWrapRage = 
                   JsonObjectParser<AIAbility>.Parse("BarutiWrapRage", value, errorInfo),
                GetObject = () => BarutiWrapRage as IObjectContentGenerator } },
           { "BasiliskCastPerching", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BasiliskCastPerching = 
                   JsonObjectParser<AIAbility>.Parse("BasiliskCastPerching", value, errorInfo),
                GetObject = () => BasiliskCastPerching as IObjectContentGenerator } },
           { "BasiliskClawA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BasiliskClawA = 
                   JsonObjectParser<AIAbility>.Parse("BasiliskClawA", value, errorInfo),
                GetObject = () => BasiliskClawA as IObjectContentGenerator } },
           { "BasiliskClawB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BasiliskClawB = 
                   JsonObjectParser<AIAbility>.Parse("BasiliskClawB", value, errorInfo),
                GetObject = () => BasiliskClawB as IObjectContentGenerator } },
           { "BasiliskDebuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BasiliskDebuff = 
                   JsonObjectParser<AIAbility>.Parse("BasiliskDebuff", value, errorInfo),
                GetObject = () => BasiliskDebuff as IObjectContentGenerator } },
           { "BasiliskToxicBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BasiliskToxicBite = 
                   JsonObjectParser<AIAbility>.Parse("BasiliskToxicBite", value, errorInfo),
                GetObject = () => BasiliskToxicBite as IObjectContentGenerator } },
           { "BatIllusionBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BatIllusionBite = 
                   JsonObjectParser<AIAbility>.Parse("BatIllusionBite", value, errorInfo),
                GetObject = () => BatIllusionBite as IObjectContentGenerator } },
           { "BatIllusionSlashA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BatIllusionSlashA = 
                   JsonObjectParser<AIAbility>.Parse("BatIllusionSlashA", value, errorInfo),
                GetObject = () => BatIllusionSlashA as IObjectContentGenerator } },
           { "BatIllusionSlashB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BatIllusionSlashB = 
                   JsonObjectParser<AIAbility>.Parse("BatIllusionSlashB", value, errorInfo),
                GetObject = () => BatIllusionSlashB as IObjectContentGenerator } },
           { "BearBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearBite = 
                   JsonObjectParser<AIAbility>.Parse("BearBite", value, errorInfo),
                GetObject = () => BearBite as IObjectContentGenerator } },
           { "BearBite_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearBite_Pet = 
                   JsonObjectParser<AIAbility>.Parse("BearBite_Pet", value, errorInfo),
                GetObject = () => BearBite_Pet as IObjectContentGenerator } },
           { "BearClaw_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearClaw_Pet = 
                   JsonObjectParser<AIAbility>.Parse("BearClaw_Pet", value, errorInfo),
                GetObject = () => BearClaw_Pet as IObjectContentGenerator } },
           { "BearCrush", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearCrush = 
                   JsonObjectParser<AIAbility>.Parse("BearCrush", value, errorInfo),
                GetObject = () => BearCrush as IObjectContentGenerator } },
           { "BearSelfHeal_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearSelfHeal_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BearSelfHeal_Pet1", value, errorInfo),
                GetObject = () => BearSelfHeal_Pet1 as IObjectContentGenerator } },
           { "BearSelfHeal_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearSelfHeal_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BearSelfHeal_Pet2", value, errorInfo),
                GetObject = () => BearSelfHeal_Pet2 as IObjectContentGenerator } },
           { "BearSelfHeal_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearSelfHeal_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BearSelfHeal_Pet3", value, errorInfo),
                GetObject = () => BearSelfHeal_Pet3 as IObjectContentGenerator } },
           { "BearSelfHeal_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearSelfHeal_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BearSelfHeal_Pet4", value, errorInfo),
                GetObject = () => BearSelfHeal_Pet4 as IObjectContentGenerator } },
           { "BearSelfHeal_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearSelfHeal_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("BearSelfHeal_Pet5", value, errorInfo),
                GetObject = () => BearSelfHeal_Pet5 as IObjectContentGenerator } },
           { "BearSelfHeal_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearSelfHeal_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("BearSelfHeal_Pet6", value, errorInfo),
                GetObject = () => BearSelfHeal_Pet6 as IObjectContentGenerator } },
           { "BearStun_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearStun_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BearStun_Pet1", value, errorInfo),
                GetObject = () => BearStun_Pet1 as IObjectContentGenerator } },
           { "BearStun_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearStun_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BearStun_Pet2", value, errorInfo),
                GetObject = () => BearStun_Pet2 as IObjectContentGenerator } },
           { "BearStun_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearStun_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BearStun_Pet3", value, errorInfo),
                GetObject = () => BearStun_Pet3 as IObjectContentGenerator } },
           { "BearStun_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearStun_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BearStun_Pet4", value, errorInfo),
                GetObject = () => BearStun_Pet4 as IObjectContentGenerator } },
           { "BearTaunt_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearTaunt_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BearTaunt_Pet1", value, errorInfo),
                GetObject = () => BearTaunt_Pet1 as IObjectContentGenerator } },
           { "BearTaunt_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearTaunt_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BearTaunt_Pet2", value, errorInfo),
                GetObject = () => BearTaunt_Pet2 as IObjectContentGenerator } },
           { "BearTaunt_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearTaunt_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BearTaunt_Pet3", value, errorInfo),
                GetObject = () => BearTaunt_Pet3 as IObjectContentGenerator } },
           { "BearTaunt_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearTaunt_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BearTaunt_Pet4", value, errorInfo),
                GetObject = () => BearTaunt_Pet4 as IObjectContentGenerator } },
           { "BearTaunt_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearTaunt_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("BearTaunt_Pet5", value, errorInfo),
                GetObject = () => BearTaunt_Pet5 as IObjectContentGenerator } },
           { "BearTaunt_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearTaunt_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("BearTaunt_Pet6", value, errorInfo),
                GetObject = () => BearTaunt_Pet6 as IObjectContentGenerator } },
           { "BearUltra_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearUltra_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BearUltra_Pet1", value, errorInfo),
                GetObject = () => BearUltra_Pet1 as IObjectContentGenerator } },
           { "BearUltra_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearUltra_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BearUltra_Pet2", value, errorInfo),
                GetObject = () => BearUltra_Pet2 as IObjectContentGenerator } },
           { "BearUltra_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearUltra_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BearUltra_Pet3", value, errorInfo),
                GetObject = () => BearUltra_Pet3 as IObjectContentGenerator } },
           { "BearUltra_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearUltra_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BearUltra_Pet4", value, errorInfo),
                GetObject = () => BearUltra_Pet4 as IObjectContentGenerator } },
           { "BearUltra_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearUltra_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("BearUltra_Pet5", value, errorInfo),
                GetObject = () => BearUltra_Pet5 as IObjectContentGenerator } },
           { "BearUltra_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearUltra_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("BearUltra_Pet6", value, errorInfo),
                GetObject = () => BearUltra_Pet6 as IObjectContentGenerator } },
           { "BearWarmth_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearWarmth_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BearWarmth_Pet1", value, errorInfo),
                GetObject = () => BearWarmth_Pet1 as IObjectContentGenerator } },
           { "BearWarmth_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearWarmth_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BearWarmth_Pet2", value, errorInfo),
                GetObject = () => BearWarmth_Pet2 as IObjectContentGenerator } },
           { "BearWarmth_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearWarmth_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BearWarmth_Pet3", value, errorInfo),
                GetObject = () => BearWarmth_Pet3 as IObjectContentGenerator } },
           { "BearWarmth_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearWarmth_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BearWarmth_Pet4", value, errorInfo),
                GetObject = () => BearWarmth_Pet4 as IObjectContentGenerator } },
           { "BearWarmth_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearWarmth_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("BearWarmth_Pet5", value, errorInfo),
                GetObject = () => BearWarmth_Pet5 as IObjectContentGenerator } },
           { "BearWarmth_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BearWarmth_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("BearWarmth_Pet6", value, errorInfo),
                GetObject = () => BearWarmth_Pet6 as IObjectContentGenerator } },
           { "BigCatClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatClaw = 
                   JsonObjectParser<AIAbility>.Parse("BigCatClaw", value, errorInfo),
                GetObject = () => BigCatClaw as IObjectContentGenerator } },
           { "BigCatClaw_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatClaw_Pet = 
                   JsonObjectParser<AIAbility>.Parse("BigCatClaw_Pet", value, errorInfo),
                GetObject = () => BigCatClaw_Pet as IObjectContentGenerator } },
           { "BigCatDebuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatDebuff = 
                   JsonObjectParser<AIAbility>.Parse("BigCatDebuff", value, errorInfo),
                GetObject = () => BigCatDebuff as IObjectContentGenerator } },
           { "BigCatHeal_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatHeal_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatHeal_Pet1", value, errorInfo),
                GetObject = () => BigCatHeal_Pet1 as IObjectContentGenerator } },
           { "BigCatHeal_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatHeal_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatHeal_Pet2", value, errorInfo),
                GetObject = () => BigCatHeal_Pet2 as IObjectContentGenerator } },
           { "BigCatHeal_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatHeal_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatHeal_Pet3", value, errorInfo),
                GetObject = () => BigCatHeal_Pet3 as IObjectContentGenerator } },
           { "BigCatHeal_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatHeal_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatHeal_Pet4", value, errorInfo),
                GetObject = () => BigCatHeal_Pet4 as IObjectContentGenerator } },
           { "BigCatHeal_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatHeal_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatHeal_Pet5", value, errorInfo),
                GetObject = () => BigCatHeal_Pet5 as IObjectContentGenerator } },
           { "BigCatHeal_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatHeal_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatHeal_Pet6", value, errorInfo),
                GetObject = () => BigCatHeal_Pet6 as IObjectContentGenerator } },
           { "BigCatKill_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatKill_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatKill_Pet1", value, errorInfo),
                GetObject = () => BigCatKill_Pet1 as IObjectContentGenerator } },
           { "BigCatKill_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatKill_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatKill_Pet2", value, errorInfo),
                GetObject = () => BigCatKill_Pet2 as IObjectContentGenerator } },
           { "BigCatKill_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatKill_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatKill_Pet3", value, errorInfo),
                GetObject = () => BigCatKill_Pet3 as IObjectContentGenerator } },
           { "BigCatKill_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatKill_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatKill_Pet4", value, errorInfo),
                GetObject = () => BigCatKill_Pet4 as IObjectContentGenerator } },
           { "BigCatKill_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatKill_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatKill_Pet5", value, errorInfo),
                GetObject = () => BigCatKill_Pet5 as IObjectContentGenerator } },
           { "BigCatKill_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatKill_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatKill_Pet6", value, errorInfo),
                GetObject = () => BigCatKill_Pet6 as IObjectContentGenerator } },
           { "BigCatPounce", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatPounce = 
                   JsonObjectParser<AIAbility>.Parse("BigCatPounce", value, errorInfo),
                GetObject = () => BigCatPounce as IObjectContentGenerator } },
           { "BigCatPounce_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatPounce_Pet = 
                   JsonObjectParser<AIAbility>.Parse("BigCatPounce_Pet", value, errorInfo),
                GetObject = () => BigCatPounce_Pet as IObjectContentGenerator } },
           { "BigCatRagePounce", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatRagePounce = 
                   JsonObjectParser<AIAbility>.Parse("BigCatRagePounce", value, errorInfo),
                GetObject = () => BigCatRagePounce as IObjectContentGenerator } },
           { "BigCatRoot_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatRoot_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatRoot_Pet1", value, errorInfo),
                GetObject = () => BigCatRoot_Pet1 as IObjectContentGenerator } },
           { "BigCatRoot_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatRoot_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatRoot_Pet2", value, errorInfo),
                GetObject = () => BigCatRoot_Pet2 as IObjectContentGenerator } },
           { "BigCatRoot_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatRoot_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatRoot_Pet3", value, errorInfo),
                GetObject = () => BigCatRoot_Pet3 as IObjectContentGenerator } },
           { "BigCatRoot_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatRoot_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatRoot_Pet4", value, errorInfo),
                GetObject = () => BigCatRoot_Pet4 as IObjectContentGenerator } },
           { "BigCatUltraKill_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatUltraKill_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatUltraKill_Pet1", value, errorInfo),
                GetObject = () => BigCatUltraKill_Pet1 as IObjectContentGenerator } },
           { "BigCatUltraKill_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatUltraKill_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatUltraKill_Pet2", value, errorInfo),
                GetObject = () => BigCatUltraKill_Pet2 as IObjectContentGenerator } },
           { "BigCatUltraKill_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatUltraKill_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatUltraKill_Pet3", value, errorInfo),
                GetObject = () => BigCatUltraKill_Pet3 as IObjectContentGenerator } },
           { "BigCatUltraKill_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatUltraKill_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatUltraKill_Pet4", value, errorInfo),
                GetObject = () => BigCatUltraKill_Pet4 as IObjectContentGenerator } },
           { "BigCatUltraKill_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatUltraKill_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatUltraKill_Pet5", value, errorInfo),
                GetObject = () => BigCatUltraKill_Pet5 as IObjectContentGenerator } },
           { "BigCatUltraKill_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatUltraKill_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatUltraKill_Pet6", value, errorInfo),
                GetObject = () => BigCatUltraKill_Pet6 as IObjectContentGenerator } },
           { "BigCatVuln_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatVuln_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatVuln_Pet1", value, errorInfo),
                GetObject = () => BigCatVuln_Pet1 as IObjectContentGenerator } },
           { "BigCatVuln_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatVuln_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatVuln_Pet2", value, errorInfo),
                GetObject = () => BigCatVuln_Pet2 as IObjectContentGenerator } },
           { "BigCatVuln_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatVuln_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatVuln_Pet3", value, errorInfo),
                GetObject = () => BigCatVuln_Pet3 as IObjectContentGenerator } },
           { "BigCatVuln_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigCatVuln_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("BigCatVuln_Pet4", value, errorInfo),
                GetObject = () => BigCatVuln_Pet4 as IObjectContentGenerator } },
           { "BigGolemFling", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemFling = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemFling", value, errorInfo),
                GetObject = () => BigGolemFling as IObjectContentGenerator } },
           { "BigGolemFlingBoss", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemFlingBoss = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemFlingBoss", value, errorInfo),
                GetObject = () => BigGolemFlingBoss as IObjectContentGenerator } },
           { "BigGolemFlingBoss2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemFlingBoss2 = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemFlingBoss2", value, errorInfo),
                GetObject = () => BigGolemFlingBoss2 as IObjectContentGenerator } },
           { "BigGolemHitA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemHitA = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemHitA", value, errorInfo),
                GetObject = () => BigGolemHitA as IObjectContentGenerator } },
           { "BigGolemHitA-NoDisable", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemHitA_NoDisable = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemHitA-NoDisable", value, errorInfo),
                GetObject = () => BigGolemHitA_NoDisable as IObjectContentGenerator } },
           { "BigGolemHitB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemHitB = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemHitB", value, errorInfo),
                GetObject = () => BigGolemHitB as IObjectContentGenerator } },
           { "BigGolemHitB-NoDisable", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemHitB_NoDisable = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemHitB-NoDisable", value, errorInfo),
                GetObject = () => BigGolemHitB_NoDisable as IObjectContentGenerator } },
           { "BigGolemPerchFix", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemPerchFix = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemPerchFix", value, errorInfo),
                GetObject = () => BigGolemPerchFix as IObjectContentGenerator } },
           { "BigGolemSummonFireSnake", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigGolemSummonFireSnake = 
                   JsonObjectParser<AIAbility>.Parse("BigGolemSummonFireSnake", value, errorInfo),
                GetObject = () => BigGolemSummonFireSnake as IObjectContentGenerator } },
           { "BigHeadCurseball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BigHeadCurseball = 
                   JsonObjectParser<AIAbility>.Parse("BigHeadCurseball", value, errorInfo),
                GetObject = () => BigHeadCurseball as IObjectContentGenerator } },
           { "BitingVineAppear", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BitingVineAppear = 
                   JsonObjectParser<AIAbility>.Parse("BitingVineAppear", value, errorInfo),
                GetObject = () => BitingVineAppear as IObjectContentGenerator } },
           { "BitingVineBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BitingVineBite = 
                   JsonObjectParser<AIAbility>.Parse("BitingVineBite", value, errorInfo),
                GetObject = () => BitingVineBite as IObjectContentGenerator } },
           { "BitingVineCast", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BitingVineCast = 
                   JsonObjectParser<AIAbility>.Parse("BitingVineCast", value, errorInfo),
                GetObject = () => BitingVineCast as IObjectContentGenerator } },
           { "BitingVineDisappear", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BitingVineDisappear = 
                   JsonObjectParser<AIAbility>.Parse("BitingVineDisappear", value, errorInfo),
                GetObject = () => BitingVineDisappear as IObjectContentGenerator } },
           { "BitingVineSpit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BitingVineSpit = 
                   JsonObjectParser<AIAbility>.Parse("BitingVineSpit", value, errorInfo),
                GetObject = () => BitingVineSpit as IObjectContentGenerator } },
           { "BitingVineSpitB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BitingVineSpitB = 
                   JsonObjectParser<AIAbility>.Parse("BitingVineSpitB", value, errorInfo),
                GetObject = () => BitingVineSpitB as IObjectContentGenerator } },
           { "BleddynHowl", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BleddynHowl = 
                   JsonObjectParser<AIAbility>.Parse("BleddynHowl", value, errorInfo),
                GetObject = () => BleddynHowl as IObjectContentGenerator } },
           { "BossMegaHammer", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossMegaHammer = 
                   JsonObjectParser<AIAbility>.Parse("BossMegaHammer", value, errorInfo),
                GetObject = () => BossMegaHammer as IObjectContentGenerator } },
           { "BossMegaHammer2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossMegaHammer2 = 
                   JsonObjectParser<AIAbility>.Parse("BossMegaHammer2", value, errorInfo),
                GetObject = () => BossMegaHammer2 as IObjectContentGenerator } },
           { "BossMegaRageHammer", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossMegaRageHammer = 
                   JsonObjectParser<AIAbility>.Parse("BossMegaRageHammer", value, errorInfo),
                GetObject = () => BossMegaRageHammer as IObjectContentGenerator } },
           { "BossMegaSword1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossMegaSword1 = 
                   JsonObjectParser<AIAbility>.Parse("BossMegaSword1", value, errorInfo),
                GetObject = () => BossMegaSword1 as IObjectContentGenerator } },
           { "BossMegaSword2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossMegaSword2 = 
                   JsonObjectParser<AIAbility>.Parse("BossMegaSword2", value, errorInfo),
                GetObject = () => BossMegaSword2 as IObjectContentGenerator } },
           { "BossSlime_SummonSlime1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossSlime_SummonSlime1 = 
                   JsonObjectParser<AIAbility>.Parse("BossSlime_SummonSlime1", value, errorInfo),
                GetObject = () => BossSlime_SummonSlime1 as IObjectContentGenerator } },
           { "BossSlime_SummonSlime4Elite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossSlime_SummonSlime4Elite = 
                   JsonObjectParser<AIAbility>.Parse("BossSlime_SummonSlime4Elite", value, errorInfo),
                GetObject = () => BossSlime_SummonSlime4Elite as IObjectContentGenerator } },
           { "BossSlimeKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossSlimeKick = 
                   JsonObjectParser<AIAbility>.Parse("BossSlimeKick", value, errorInfo),
                GetObject = () => BossSlimeKick as IObjectContentGenerator } },
           { "BossSlimeKick2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossSlimeKick2 = 
                   JsonObjectParser<AIAbility>.Parse("BossSlimeKick2", value, errorInfo),
                GetObject = () => BossSlimeKick2 as IObjectContentGenerator } },
           { "BossSlimeKick2B", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossSlimeKick2B = 
                   JsonObjectParser<AIAbility>.Parse("BossSlimeKick2B", value, errorInfo),
                GetObject = () => BossSlimeKick2B as IObjectContentGenerator } },
           { "BossSlimeKickB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BossSlimeKickB = 
                   JsonObjectParser<AIAbility>.Parse("BossSlimeKickB", value, errorInfo),
                GetObject = () => BossSlimeKickB as IObjectContentGenerator } },
           { "BrainBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BrainBite = 
                   JsonObjectParser<AIAbility>.Parse("BrainBite", value, errorInfo),
                GetObject = () => BrainBite as IObjectContentGenerator } },
           { "BrainDrain", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BrainDrain = 
                   JsonObjectParser<AIAbility>.Parse("BrainDrain", value, errorInfo),
                GetObject = () => BrainDrain as IObjectContentGenerator } },
           { "BrainDrain2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => BrainDrain2 = 
                   JsonObjectParser<AIAbility>.Parse("BrainDrain2", value, errorInfo),
                GetObject = () => BrainDrain2 as IObjectContentGenerator } },
           { "CiervosDarknessBomb", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CiervosDarknessBomb = 
                   JsonObjectParser<AIAbility>.Parse("CiervosDarknessBomb", value, errorInfo),
                GetObject = () => CiervosDarknessBomb as IObjectContentGenerator } },
           { "CiervosNightmareHoof", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CiervosNightmareHoof = 
                   JsonObjectParser<AIAbility>.Parse("CiervosNightmareHoof", value, errorInfo),
                GetObject = () => CiervosNightmareHoof as IObjectContentGenerator } },
           { "ClaudiaBlizzard", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ClaudiaBlizzard = 
                   JsonObjectParser<AIAbility>.Parse("ClaudiaBlizzard", value, errorInfo),
                GetObject = () => ClaudiaBlizzard as IObjectContentGenerator } },
           { "ClaudiaIceSpear", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ClaudiaIceSpear = 
                   JsonObjectParser<AIAbility>.Parse("ClaudiaIceSpear", value, errorInfo),
                GetObject = () => ClaudiaIceSpear as IObjectContentGenerator } },
           { "ClaudiaTundraSpikes", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ClaudiaTundraSpikes = 
                   JsonObjectParser<AIAbility>.Parse("ClaudiaTundraSpikes", value, errorInfo),
                GetObject = () => ClaudiaTundraSpikes as IObjectContentGenerator } },
           { "CockatriceParalyze", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CockatriceParalyze = 
                   JsonObjectParser<AIAbility>.Parse("CockatriceParalyze", value, errorInfo),
                GetObject = () => CockatriceParalyze as IObjectContentGenerator } },
           { "CockatricePeck", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CockatricePeck = 
                   JsonObjectParser<AIAbility>.Parse("CockatricePeck", value, errorInfo),
                GetObject = () => CockatricePeck as IObjectContentGenerator } },
           { "CockatriceTailWhip", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CockatriceTailWhip = 
                   JsonObjectParser<AIAbility>.Parse("CockatriceTailWhip", value, errorInfo),
                GetObject = () => CockatriceTailWhip as IObjectContentGenerator } },
           { "ColdAuraBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ColdAuraBurst = 
                   JsonObjectParser<AIAbility>.Parse("ColdAuraBurst", value, errorInfo),
                GetObject = () => ColdAuraBurst as IObjectContentGenerator } },
           { "ColdSphereBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ColdSphereBurst = 
                   JsonObjectParser<AIAbility>.Parse("ColdSphereBurst", value, errorInfo),
                GetObject = () => ColdSphereBurst as IObjectContentGenerator } },
           { "ColdSphereFreezeBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ColdSphereFreezeBurst = 
                   JsonObjectParser<AIAbility>.Parse("ColdSphereFreezeBurst", value, errorInfo),
                GetObject = () => ColdSphereFreezeBurst as IObjectContentGenerator } },
           { "CultistArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CultistArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("CultistArrow1", value, errorInfo),
                GetObject = () => CultistArrow1 as IObjectContentGenerator } },
           { "CultistArrow2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CultistArrow2 = 
                   JsonObjectParser<AIAbility>.Parse("CultistArrow2", value, errorInfo),
                GetObject = () => CultistArrow2 as IObjectContentGenerator } },
           { "CultistOmegaArrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CultistOmegaArrow = 
                   JsonObjectParser<AIAbility>.Parse("CultistOmegaArrow", value, errorInfo),
                GetObject = () => CultistOmegaArrow as IObjectContentGenerator } },
           { "CultistSword1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CultistSword1 = 
                   JsonObjectParser<AIAbility>.Parse("CultistSword1", value, errorInfo),
                GetObject = () => CultistSword1 as IObjectContentGenerator } },
           { "CultistSword2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CultistSword2 = 
                   JsonObjectParser<AIAbility>.Parse("CultistSword2", value, errorInfo),
                GetObject = () => CultistSword2 as IObjectContentGenerator } },
           { "CultistSwordStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => CultistSwordStun = 
                   JsonObjectParser<AIAbility>.Parse("CultistSwordStun", value, errorInfo),
                GetObject = () => CultistSwordStun as IObjectContentGenerator } },
           { "DeathRay", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DeathRay = 
                   JsonObjectParser<AIAbility>.Parse("DeathRay", value, errorInfo),
                GetObject = () => DeathRay as IObjectContentGenerator } },
           { "DementiaPuckCurse", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DementiaPuckCurse = 
                   JsonObjectParser<AIAbility>.Parse("DementiaPuckCurse", value, errorInfo),
                GetObject = () => DementiaPuckCurse as IObjectContentGenerator } },
           { "Dinobite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Dinobite = 
                   JsonObjectParser<AIAbility>.Parse("Dinobite", value, errorInfo),
                GetObject = () => Dinobite as IObjectContentGenerator } },
           { "Dinobite2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Dinobite2 = 
                   JsonObjectParser<AIAbility>.Parse("Dinobite2", value, errorInfo),
                GetObject = () => Dinobite2 as IObjectContentGenerator } },
           { "Dinoslash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Dinoslash = 
                   JsonObjectParser<AIAbility>.Parse("Dinoslash", value, errorInfo),
                GetObject = () => Dinoslash as IObjectContentGenerator } },
           { "Dinoslash2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Dinoslash2 = 
                   JsonObjectParser<AIAbility>.Parse("Dinoslash2", value, errorInfo),
                GetObject = () => Dinoslash2 as IObjectContentGenerator } },
           { "Dinowhap", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Dinowhap = 
                   JsonObjectParser<AIAbility>.Parse("Dinowhap", value, errorInfo),
                GetObject = () => Dinowhap as IObjectContentGenerator } },
           { "DragonWormBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DragonWormBite = 
                   JsonObjectParser<AIAbility>.Parse("DragonWormBite", value, errorInfo),
                GetObject = () => DragonWormBite as IObjectContentGenerator } },
           { "DragonWormEscape", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DragonWormEscape = 
                   JsonObjectParser<AIAbility>.Parse("DragonWormEscape", value, errorInfo),
                GetObject = () => DragonWormEscape as IObjectContentGenerator } },
           { "DragonWormRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DragonWormRage = 
                   JsonObjectParser<AIAbility>.Parse("DragonWormRage", value, errorInfo),
                GetObject = () => DragonWormRage as IObjectContentGenerator } },
           { "DragonWormSmack", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DragonWormSmack = 
                   JsonObjectParser<AIAbility>.Parse("DragonWormSmack", value, errorInfo),
                GetObject = () => DragonWormSmack as IObjectContentGenerator } },
           { "DragonWormSpitElectricity", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DragonWormSpitElectricity = 
                   JsonObjectParser<AIAbility>.Parse("DragonWormSpitElectricity", value, errorInfo),
                GetObject = () => DragonWormSpitElectricity as IObjectContentGenerator } },
           { "DragonWormSpitFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DragonWormSpitFire = 
                   JsonObjectParser<AIAbility>.Parse("DragonWormSpitFire", value, errorInfo),
                GetObject = () => DragonWormSpitFire as IObjectContentGenerator } },
           { "DroachBiteA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachBiteA = 
                   JsonObjectParser<AIAbility>.Parse("DroachBiteA", value, errorInfo),
                GetObject = () => DroachBiteA as IObjectContentGenerator } },
           { "DroachBiteB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachBiteB = 
                   JsonObjectParser<AIAbility>.Parse("DroachBiteB", value, errorInfo),
                GetObject = () => DroachBiteB as IObjectContentGenerator } },
           { "DroachBreatheFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachBreatheFire = 
                   JsonObjectParser<AIAbility>.Parse("DroachBreatheFire", value, errorInfo),
                GetObject = () => DroachBreatheFire as IObjectContentGenerator } },
           { "DroachFireball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachFireball = 
                   JsonObjectParser<AIAbility>.Parse("DroachFireball", value, errorInfo),
                GetObject = () => DroachFireball as IObjectContentGenerator } },
           { "DroachFireballPerching", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachFireballPerching = 
                   JsonObjectParser<AIAbility>.Parse("DroachFireballPerching", value, errorInfo),
                GetObject = () => DroachFireballPerching as IObjectContentGenerator } },
           { "DroachLightning", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachLightning = 
                   JsonObjectParser<AIAbility>.Parse("DroachLightning", value, errorInfo),
                GetObject = () => DroachLightning as IObjectContentGenerator } },
           { "DroachLightningPerching", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachLightningPerching = 
                   JsonObjectParser<AIAbility>.Parse("DroachLightningPerching", value, errorInfo),
                GetObject = () => DroachLightningPerching as IObjectContentGenerator } },
           { "DroachShockingKnockback", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DroachShockingKnockback = 
                   JsonObjectParser<AIAbility>.Parse("DroachShockingKnockback", value, errorInfo),
                GetObject = () => DroachShockingKnockback as IObjectContentGenerator } },
           { "DruidHealingSanctuaryHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => DruidHealingSanctuaryHeal = 
                   JsonObjectParser<AIAbility>.Parse("DruidHealingSanctuaryHeal", value, errorInfo),
                GetObject = () => DruidHealingSanctuaryHeal as IObjectContentGenerator } },
           { "ElectricityAura1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElectricityAura1 = 
                   JsonObjectParser<AIAbility>.Parse("ElectricityAura1", value, errorInfo),
                GetObject = () => ElectricityAura1 as IObjectContentGenerator } },
           { "ElectricityAuraBolt1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElectricityAuraBolt1 = 
                   JsonObjectParser<AIAbility>.Parse("ElectricityAuraBolt1", value, errorInfo),
                GetObject = () => ElectricityAuraBolt1 as IObjectContentGenerator } },
           { "ElectricPigAoEStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElectricPigAoEStun = 
                   JsonObjectParser<AIAbility>.Parse("ElectricPigAoEStun", value, errorInfo),
                GetObject = () => ElectricPigAoEStun as IObjectContentGenerator } },
           { "ElectricPigHitAndRun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElectricPigHitAndRun = 
                   JsonObjectParser<AIAbility>.Parse("ElectricPigHitAndRun", value, errorInfo),
                GetObject = () => ElectricPigHitAndRun as IObjectContentGenerator } },
           { "ElectricPigStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElectricPigStun = 
                   JsonObjectParser<AIAbility>.Parse("ElectricPigStun", value, errorInfo),
                GetObject = () => ElectricPigStun as IObjectContentGenerator } },
           { "ElementalBees", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElementalBees = 
                   JsonObjectParser<AIAbility>.Parse("ElementalBees", value, errorInfo),
                GetObject = () => ElementalBees as IObjectContentGenerator } },
           { "ElementalBees2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElementalBees2 = 
                   JsonObjectParser<AIAbility>.Parse("ElementalBees2", value, errorInfo),
                GetObject = () => ElementalBees2 as IObjectContentGenerator } },
           { "ElementalSlam", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ElementalSlam = 
                   JsonObjectParser<AIAbility>.Parse("ElementalSlam", value, errorInfo),
                GetObject = () => ElementalSlam as IObjectContentGenerator } },
           { "EnemyMinigolemExplode", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => EnemyMinigolemExplode = 
                   JsonObjectParser<AIAbility>.Parse("EnemyMinigolemExplode", value, errorInfo),
                GetObject = () => EnemyMinigolemExplode as IObjectContentGenerator } },
           { "EnemyMinigolemHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => EnemyMinigolemHeal = 
                   JsonObjectParser<AIAbility>.Parse("EnemyMinigolemHeal", value, errorInfo),
                GetObject = () => EnemyMinigolemHeal as IObjectContentGenerator } },
           { "EnemyMinigolemPunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => EnemyMinigolemPunch = 
                   JsonObjectParser<AIAbility>.Parse("EnemyMinigolemPunch", value, errorInfo),
                GetObject = () => EnemyMinigolemPunch as IObjectContentGenerator } },
           { "FaceOfDeathKill", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FaceOfDeathKill = 
                   JsonObjectParser<AIAbility>.Parse("FaceOfDeathKill", value, errorInfo),
                GetObject = () => FaceOfDeathKill as IObjectContentGenerator } },
           { "FaeLightningSmite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FaeLightningSmite = 
                   JsonObjectParser<AIAbility>.Parse("FaeLightningSmite", value, errorInfo),
                GetObject = () => FaeLightningSmite as IObjectContentGenerator } },
           { "FaeLightningSmiteHidden", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FaeLightningSmiteHidden = 
                   JsonObjectParser<AIAbility>.Parse("FaeLightningSmiteHidden", value, errorInfo),
                GetObject = () => FaeLightningSmiteHidden as IObjectContentGenerator } },
           { "FaeSwordA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FaeSwordA = 
                   JsonObjectParser<AIAbility>.Parse("FaeSwordA", value, errorInfo),
                GetObject = () => FaeSwordA as IObjectContentGenerator } },
           { "FaeSwordB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FaeSwordB = 
                   JsonObjectParser<AIAbility>.Parse("FaeSwordB", value, errorInfo),
                GetObject = () => FaeSwordB as IObjectContentGenerator } },
           { "FaeSwordKill", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FaeSwordKill = 
                   JsonObjectParser<AIAbility>.Parse("FaeSwordKill", value, errorInfo),
                GetObject = () => FaeSwordKill as IObjectContentGenerator } },
           { "FireRatBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireRatBite = 
                   JsonObjectParser<AIAbility>.Parse("FireRatBite", value, errorInfo),
                GetObject = () => FireRatBite as IObjectContentGenerator } },
           { "FireRatBite_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireRatBite_Pet = 
                   JsonObjectParser<AIAbility>.Parse("FireRatBite_Pet", value, errorInfo),
                GetObject = () => FireRatBite_Pet as IObjectContentGenerator } },
           { "FireRatClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireRatClaw = 
                   JsonObjectParser<AIAbility>.Parse("FireRatClaw", value, errorInfo),
                GetObject = () => FireRatClaw as IObjectContentGenerator } },
           { "FireRatClaw_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireRatClaw_Pet = 
                   JsonObjectParser<AIAbility>.Parse("FireRatClaw_Pet", value, errorInfo),
                GetObject = () => FireRatClaw_Pet as IObjectContentGenerator } },
           { "FireSnakeExplosion1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireSnakeExplosion1 = 
                   JsonObjectParser<AIAbility>.Parse("FireSnakeExplosion1", value, errorInfo),
                GetObject = () => FireSnakeExplosion1 as IObjectContentGenerator } },
           { "FireTrapAttack1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireTrapAttack1 = 
                   JsonObjectParser<AIAbility>.Parse("FireTrapAttack1", value, errorInfo),
                GetObject = () => FireTrapAttack1 as IObjectContentGenerator } },
           { "FireWallAttack1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireWallAttack1 = 
                   JsonObjectParser<AIAbility>.Parse("FireWallAttack1", value, errorInfo),
                GetObject = () => FireWallAttack1 as IObjectContentGenerator } },
           { "FireWallDotAttack1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FireWallDotAttack1 = 
                   JsonObjectParser<AIAbility>.Parse("FireWallDotAttack1", value, errorInfo),
                GetObject = () => FireWallDotAttack1 as IObjectContentGenerator } },
           { "FlapSkullBigBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FlapSkullBigBite = 
                   JsonObjectParser<AIAbility>.Parse("FlapSkullBigBite", value, errorInfo),
                GetObject = () => FlapSkullBigBite as IObjectContentGenerator } },
           { "FlapSkullBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => FlapSkullBite = 
                   JsonObjectParser<AIAbility>.Parse("FlapSkullBite", value, errorInfo),
                GetObject = () => FlapSkullBite as IObjectContentGenerator } },
           { "GargoyleBossStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GargoyleBossStun = 
                   JsonObjectParser<AIAbility>.Parse("GargoyleBossStun", value, errorInfo),
                GetObject = () => GargoyleBossStun as IObjectContentGenerator } },
           { "GargoyleSlamA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GargoyleSlamA = 
                   JsonObjectParser<AIAbility>.Parse("GargoyleSlamA", value, errorInfo),
                GetObject = () => GargoyleSlamA as IObjectContentGenerator } },
           { "GargoyleSlamB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GargoyleSlamB = 
                   JsonObjectParser<AIAbility>.Parse("GargoyleSlamB", value, errorInfo),
                GetObject = () => GargoyleSlamB as IObjectContentGenerator } },
           { "GargoyleStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GargoyleStun = 
                   JsonObjectParser<AIAbility>.Parse("GargoyleStun", value, errorInfo),
                GetObject = () => GargoyleStun as IObjectContentGenerator } },
           { "GazlukPriest1Special", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GazlukPriest1Special = 
                   JsonObjectParser<AIAbility>.Parse("GazlukPriest1Special", value, errorInfo),
                GetObject = () => GazlukPriest1Special as IObjectContentGenerator } },
           { "GazlukPriest2Special", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GazlukPriest2Special = 
                   JsonObjectParser<AIAbility>.Parse("GazlukPriest2Special", value, errorInfo),
                GetObject = () => GazlukPriest2Special as IObjectContentGenerator } },
           { "GazlukPriest3Special", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GazlukPriest3Special = 
                   JsonObjectParser<AIAbility>.Parse("GazlukPriest3Special", value, errorInfo),
                GetObject = () => GazlukPriest3Special as IObjectContentGenerator } },
           { "GhostlyBolt", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhostlyBolt = 
                   JsonObjectParser<AIAbility>.Parse("GhostlyBolt", value, errorInfo),
                GetObject = () => GhostlyBolt as IObjectContentGenerator } },
           { "GhostlyBossBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhostlyBossBurst = 
                   JsonObjectParser<AIAbility>.Parse("GhostlyBossBurst", value, errorInfo),
                GetObject = () => GhostlyBossBurst as IObjectContentGenerator } },
           { "GhostlyBossPunchA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhostlyBossPunchA = 
                   JsonObjectParser<AIAbility>.Parse("GhostlyBossPunchA", value, errorInfo),
                GetObject = () => GhostlyBossPunchA as IObjectContentGenerator } },
           { "GhostlyBossPunchB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhostlyBossPunchB = 
                   JsonObjectParser<AIAbility>.Parse("GhostlyBossPunchB", value, errorInfo),
                GetObject = () => GhostlyBossPunchB as IObjectContentGenerator } },
           { "GhostlyBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhostlyBurst = 
                   JsonObjectParser<AIAbility>.Parse("GhostlyBurst", value, errorInfo),
                GetObject = () => GhostlyBurst as IObjectContentGenerator } },
           { "GhostlyPunchA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhostlyPunchA = 
                   JsonObjectParser<AIAbility>.Parse("GhostlyPunchA", value, errorInfo),
                GetObject = () => GhostlyPunchA as IObjectContentGenerator } },
           { "GhostlyPunchB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhostlyPunchB = 
                   JsonObjectParser<AIAbility>.Parse("GhostlyPunchB", value, errorInfo),
                GetObject = () => GhostlyPunchB as IObjectContentGenerator } },
           { "GhoulClawA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhoulClawA = 
                   JsonObjectParser<AIAbility>.Parse("GhoulClawA", value, errorInfo),
                GetObject = () => GhoulClawA as IObjectContentGenerator } },
           { "GhoulClawB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhoulClawB = 
                   JsonObjectParser<AIAbility>.Parse("GhoulClawB", value, errorInfo),
                GetObject = () => GhoulClawB as IObjectContentGenerator } },
           { "GhoulHammerA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhoulHammerA = 
                   JsonObjectParser<AIAbility>.Parse("GhoulHammerA", value, errorInfo),
                GetObject = () => GhoulHammerA as IObjectContentGenerator } },
           { "GhoulHammerB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhoulHammerB = 
                   JsonObjectParser<AIAbility>.Parse("GhoulHammerB", value, errorInfo),
                GetObject = () => GhoulHammerB as IObjectContentGenerator } },
           { "GhoulSelfBuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GhoulSelfBuff = 
                   JsonObjectParser<AIAbility>.Parse("GhoulSelfBuff", value, errorInfo),
                GetObject = () => GhoulSelfBuff as IObjectContentGenerator } },
           { "GiantBatBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantBatBite = 
                   JsonObjectParser<AIAbility>.Parse("GiantBatBite", value, errorInfo),
                GetObject = () => GiantBatBite as IObjectContentGenerator } },
           { "GiantBatSlashA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantBatSlashA = 
                   JsonObjectParser<AIAbility>.Parse("GiantBatSlashA", value, errorInfo),
                GetObject = () => GiantBatSlashA as IObjectContentGenerator } },
           { "GiantBatSlashB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantBatSlashB = 
                   JsonObjectParser<AIAbility>.Parse("GiantBatSlashB", value, errorInfo),
                GetObject = () => GiantBatSlashB as IObjectContentGenerator } },
           { "GiantBeetleBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantBeetleBite = 
                   JsonObjectParser<AIAbility>.Parse("GiantBeetleBite", value, errorInfo),
                GetObject = () => GiantBeetleBite as IObjectContentGenerator } },
           { "GiantBeetleBoulderSpit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantBeetleBoulderSpit = 
                   JsonObjectParser<AIAbility>.Parse("GiantBeetleBoulderSpit", value, errorInfo),
                GetObject = () => GiantBeetleBoulderSpit as IObjectContentGenerator } },
           { "GiantBeetleInject", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantBeetleInject = 
                   JsonObjectParser<AIAbility>.Parse("GiantBeetleInject", value, errorInfo),
                GetObject = () => GiantBeetleInject as IObjectContentGenerator } },
           { "GiantScorpionClawA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantScorpionClawA = 
                   JsonObjectParser<AIAbility>.Parse("GiantScorpionClawA", value, errorInfo),
                GetObject = () => GiantScorpionClawA as IObjectContentGenerator } },
           { "GiantScorpionClawB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantScorpionClawB = 
                   JsonObjectParser<AIAbility>.Parse("GiantScorpionClawB", value, errorInfo),
                GetObject = () => GiantScorpionClawB as IObjectContentGenerator } },
           { "GiantScorpionSting", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GiantScorpionSting = 
                   JsonObjectParser<AIAbility>.Parse("GiantScorpionSting", value, errorInfo),
                GetObject = () => GiantScorpionSting as IObjectContentGenerator } },
           { "GnasherBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GnasherBite = 
                   JsonObjectParser<AIAbility>.Parse("GnasherBite", value, errorInfo),
                GetObject = () => GnasherBite as IObjectContentGenerator } },
           { "GnasherRend", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GnasherRend = 
                   JsonObjectParser<AIAbility>.Parse("GnasherRend", value, errorInfo),
                GetObject = () => GnasherRend as IObjectContentGenerator } },
           { "GoblinArmorBuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinArmorBuff = 
                   JsonObjectParser<AIAbility>.Parse("GoblinArmorBuff", value, errorInfo),
                GetObject = () => GoblinArmorBuff as IObjectContentGenerator } },
           { "GoblinArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinArrow1", value, errorInfo),
                GetObject = () => GoblinArrow1 as IObjectContentGenerator } },
           { "GoblinArrow2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinArrow2 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinArrow2", value, errorInfo),
                GetObject = () => GoblinArrow2 as IObjectContentGenerator } },
           { "GoblinBossLightning", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinBossLightning = 
                   JsonObjectParser<AIAbility>.Parse("GoblinBossLightning", value, errorInfo),
                GetObject = () => GoblinBossLightning as IObjectContentGenerator } },
           { "GoblinHateZapBall", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinHateZapBall = 
                   JsonObjectParser<AIAbility>.Parse("GoblinHateZapBall", value, errorInfo),
                GetObject = () => GoblinHateZapBall as IObjectContentGenerator } },
           { "GoblinHateZapBall2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinHateZapBall2 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinHateZapBall2", value, errorInfo),
                GetObject = () => GoblinHateZapBall2 as IObjectContentGenerator } },
           { "GoblinHeal1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinHeal1 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinHeal1", value, errorInfo),
                GetObject = () => GoblinHeal1 as IObjectContentGenerator } },
           { "GoblinHeal2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinHeal2 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinHeal2", value, errorInfo),
                GetObject = () => GoblinHeal2 as IObjectContentGenerator } },
           { "GoblinPunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinPunch = 
                   JsonObjectParser<AIAbility>.Parse("GoblinPunch", value, errorInfo),
                GetObject = () => GoblinPunch as IObjectContentGenerator } },
           { "GoblinRageArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinRageArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinRageArrow1", value, errorInfo),
                GetObject = () => GoblinRageArrow1 as IObjectContentGenerator } },
           { "GoblinRageArrow2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinRageArrow2 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinRageArrow2", value, errorInfo),
                GetObject = () => GoblinRageArrow2 as IObjectContentGenerator } },
           { "GoblinRageSpear1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinRageSpear1 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinRageSpear1", value, errorInfo),
                GetObject = () => GoblinRageSpear1 as IObjectContentGenerator } },
           { "GoblinRageSpear2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinRageSpear2 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinRageSpear2", value, errorInfo),
                GetObject = () => GoblinRageSpear2 as IObjectContentGenerator } },
           { "GoblinSpear1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinSpear1 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinSpear1", value, errorInfo),
                GetObject = () => GoblinSpear1 as IObjectContentGenerator } },
           { "GoblinSpear2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinSpear2 = 
                   JsonObjectParser<AIAbility>.Parse("GoblinSpear2", value, errorInfo),
                GetObject = () => GoblinSpear2 as IObjectContentGenerator } },
           { "GoblinSpreadZapBall", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinSpreadZapBall = 
                   JsonObjectParser<AIAbility>.Parse("GoblinSpreadZapBall", value, errorInfo),
                GetObject = () => GoblinSpreadZapBall as IObjectContentGenerator } },
           { "GoblinZapBall", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GoblinZapBall = 
                   JsonObjectParser<AIAbility>.Parse("GoblinZapBall", value, errorInfo),
                GetObject = () => GoblinZapBall as IObjectContentGenerator } },
           { "GrimalkinBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinBite = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinBite", value, errorInfo),
                GetObject = () => GrimalkinBite as IObjectContentGenerator } },
           { "GrimalkinClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinClaw = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinClaw", value, errorInfo),
                GetObject = () => GrimalkinClaw as IObjectContentGenerator } },
           { "GrimalkinFlee_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinFlee_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinFlee_Pet1", value, errorInfo),
                GetObject = () => GrimalkinFlee_Pet1 as IObjectContentGenerator } },
           { "GrimalkinFlee_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinFlee_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinFlee_Pet2", value, errorInfo),
                GetObject = () => GrimalkinFlee_Pet2 as IObjectContentGenerator } },
           { "GrimalkinFlee_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinFlee_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinFlee_Pet3", value, errorInfo),
                GetObject = () => GrimalkinFlee_Pet3 as IObjectContentGenerator } },
           { "GrimalkinPuncture", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinPuncture = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinPuncture", value, errorInfo),
                GetObject = () => GrimalkinPuncture as IObjectContentGenerator } },
           { "GrimalkinPuncture_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinPuncture_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinPuncture_Pet1", value, errorInfo),
                GetObject = () => GrimalkinPuncture_Pet1 as IObjectContentGenerator } },
           { "GrimalkinPuncture_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinPuncture_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinPuncture_Pet2", value, errorInfo),
                GetObject = () => GrimalkinPuncture_Pet2 as IObjectContentGenerator } },
           { "GrimalkinPuncture_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinPuncture_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinPuncture_Pet3", value, errorInfo),
                GetObject = () => GrimalkinPuncture_Pet3 as IObjectContentGenerator } },
           { "GrimalkinPuncture_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinPuncture_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinPuncture_Pet4", value, errorInfo),
                GetObject = () => GrimalkinPuncture_Pet4 as IObjectContentGenerator } },
           { "GrimalkinPuncture_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinPuncture_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinPuncture_Pet5", value, errorInfo),
                GetObject = () => GrimalkinPuncture_Pet5 as IObjectContentGenerator } },
           { "GrimalkinPuncture_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => GrimalkinPuncture_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("GrimalkinPuncture_Pet6", value, errorInfo),
                GetObject = () => GrimalkinPuncture_Pet6 as IObjectContentGenerator } },
           { "HagAgingScream", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HagAgingScream = 
                   JsonObjectParser<AIAbility>.Parse("HagAgingScream", value, errorInfo),
                GetObject = () => HagAgingScream as IObjectContentGenerator } },
           { "HagAgingTouch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HagAgingTouch = 
                   JsonObjectParser<AIAbility>.Parse("HagAgingTouch", value, errorInfo),
                GetObject = () => HagAgingTouch as IObjectContentGenerator } },
           { "HealingAura1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HealingAura1 = 
                   JsonObjectParser<AIAbility>.Parse("HealingAura1", value, errorInfo),
                GetObject = () => HealingAura1 as IObjectContentGenerator } },
           { "HealingAura2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HealingAura2 = 
                   JsonObjectParser<AIAbility>.Parse("HealingAura2", value, errorInfo),
                GetObject = () => HealingAura2 as IObjectContentGenerator } },
           { "HealingAura3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HealingAura3 = 
                   JsonObjectParser<AIAbility>.Parse("HealingAura3", value, errorInfo),
                GetObject = () => HealingAura3 as IObjectContentGenerator } },
           { "HealingAura4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HealingAura4 = 
                   JsonObjectParser<AIAbility>.Parse("HealingAura4", value, errorInfo),
                GetObject = () => HealingAura4 as IObjectContentGenerator } },
           { "HippoBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HippoBite = 
                   JsonObjectParser<AIAbility>.Parse("HippoBite", value, errorInfo),
                GetObject = () => HippoBite as IObjectContentGenerator } },
           { "HippoBiteAndHeal1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HippoBiteAndHeal1 = 
                   JsonObjectParser<AIAbility>.Parse("HippoBiteAndHeal1", value, errorInfo),
                GetObject = () => HippoBiteAndHeal1 as IObjectContentGenerator } },
           { "HippogriffBossSlashes", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HippogriffBossSlashes = 
                   JsonObjectParser<AIAbility>.Parse("HippogriffBossSlashes", value, errorInfo),
                GetObject = () => HippogriffBossSlashes as IObjectContentGenerator } },
           { "HippogriffPeck", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HippogriffPeck = 
                   JsonObjectParser<AIAbility>.Parse("HippogriffPeck", value, errorInfo),
                GetObject = () => HippogriffPeck as IObjectContentGenerator } },
           { "HippogriffSlashes", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HippogriffSlashes = 
                   JsonObjectParser<AIAbility>.Parse("HippogriffSlashes", value, errorInfo),
                GetObject = () => HippogriffSlashes as IObjectContentGenerator } },
           { "HookAcid", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HookAcid = 
                   JsonObjectParser<AIAbility>.Parse("HookAcid", value, errorInfo),
                GetObject = () => HookAcid as IObjectContentGenerator } },
           { "HookClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HookClaw = 
                   JsonObjectParser<AIAbility>.Parse("HookClaw", value, errorInfo),
                GetObject = () => HookClaw as IObjectContentGenerator } },
           { "HookRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => HookRage = 
                   JsonObjectParser<AIAbility>.Parse("HookRage", value, errorInfo),
                GetObject = () => HookRage as IObjectContentGenerator } },
           { "IceCockFreeze", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IceCockFreeze = 
                   JsonObjectParser<AIAbility>.Parse("IceCockFreeze", value, errorInfo),
                GetObject = () => IceCockFreeze as IObjectContentGenerator } },
           { "IceCockPeck", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IceCockPeck = 
                   JsonObjectParser<AIAbility>.Parse("IceCockPeck", value, errorInfo),
                GetObject = () => IceCockPeck as IObjectContentGenerator } },
           { "IceSlimeBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IceSlimeBite = 
                   JsonObjectParser<AIAbility>.Parse("IceSlimeBite", value, errorInfo),
                GetObject = () => IceSlimeBite as IObjectContentGenerator } },
           { "IceSlimeBiteB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IceSlimeBiteB = 
                   JsonObjectParser<AIAbility>.Parse("IceSlimeBiteB", value, errorInfo),
                GetObject = () => IceSlimeBiteB as IObjectContentGenerator } },
           { "IceSlimeKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IceSlimeKick = 
                   JsonObjectParser<AIAbility>.Parse("IceSlimeKick", value, errorInfo),
                GetObject = () => IceSlimeKick as IObjectContentGenerator } },
           { "IceSlimeKickB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IceSlimeKickB = 
                   JsonObjectParser<AIAbility>.Parse("IceSlimeKickB", value, errorInfo),
                GetObject = () => IceSlimeKickB as IObjectContentGenerator } },
           { "IcyCocoon", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IcyCocoon = 
                   JsonObjectParser<AIAbility>.Parse("IcyCocoon", value, errorInfo),
                GetObject = () => IcyCocoon as IObjectContentGenerator } },
           { "IcyCocoon2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IcyCocoon2 = 
                   JsonObjectParser<AIAbility>.Parse("IcyCocoon2", value, errorInfo),
                GetObject = () => IcyCocoon2 as IObjectContentGenerator } },
           { "IcySlam", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => IcySlam = 
                   JsonObjectParser<AIAbility>.Parse("IcySlam", value, errorInfo),
                GetObject = () => IcySlam as IObjectContentGenerator } },
           { "InjectorBugBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => InjectorBugBite = 
                   JsonObjectParser<AIAbility>.Parse("InjectorBugBite", value, errorInfo),
                GetObject = () => InjectorBugBite as IObjectContentGenerator } },
           { "InjectorBugInject", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => InjectorBugInject = 
                   JsonObjectParser<AIAbility>.Parse("InjectorBugInject", value, errorInfo),
                GetObject = () => InjectorBugInject as IObjectContentGenerator } },
           { "InjectorBugInject2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => InjectorBugInject2 = 
                   JsonObjectParser<AIAbility>.Parse("InjectorBugInject2", value, errorInfo),
                GetObject = () => InjectorBugInject2 as IObjectContentGenerator } },
           { "KhyrulekCurseBall", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => KhyrulekCurseBall = 
                   JsonObjectParser<AIAbility>.Parse("KhyrulekCurseBall", value, errorInfo),
                GetObject = () => KhyrulekCurseBall as IObjectContentGenerator } },
           { "KrakenBabyBeak", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => KrakenBabyBeak = 
                   JsonObjectParser<AIAbility>.Parse("KrakenBabyBeak", value, errorInfo),
                GetObject = () => KrakenBabyBeak as IObjectContentGenerator } },
           { "KrakenBabyRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => KrakenBabyRage = 
                   JsonObjectParser<AIAbility>.Parse("KrakenBabyRage", value, errorInfo),
                GetObject = () => KrakenBabyRage as IObjectContentGenerator } },
           { "KrakenBabySlam", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => KrakenBabySlam = 
                   JsonObjectParser<AIAbility>.Parse("KrakenBabySlam", value, errorInfo),
                GetObject = () => KrakenBabySlam as IObjectContentGenerator } },
           { "KrakenBeak", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => KrakenBeak = 
                   JsonObjectParser<AIAbility>.Parse("KrakenBeak", value, errorInfo),
                GetObject = () => KrakenBeak as IObjectContentGenerator } },
           { "KrakenRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => KrakenRage = 
                   JsonObjectParser<AIAbility>.Parse("KrakenRage", value, errorInfo),
                GetObject = () => KrakenRage as IObjectContentGenerator } },
           { "KrakenSlam", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => KrakenSlam = 
                   JsonObjectParser<AIAbility>.Parse("KrakenSlam", value, errorInfo),
                GetObject = () => KrakenSlam as IObjectContentGenerator } },
           { "LamiaMindControl", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => LamiaMindControl = 
                   JsonObjectParser<AIAbility>.Parse("LamiaMindControl", value, errorInfo),
                GetObject = () => LamiaMindControl as IObjectContentGenerator } },
           { "LamiaRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => LamiaRage = 
                   JsonObjectParser<AIAbility>.Parse("LamiaRage", value, errorInfo),
                GetObject = () => LamiaRage as IObjectContentGenerator } },
           { "ManticoreBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ManticoreBite = 
                   JsonObjectParser<AIAbility>.Parse("ManticoreBite", value, errorInfo),
                GetObject = () => ManticoreBite as IObjectContentGenerator } },
           { "ManticoreClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ManticoreClaw = 
                   JsonObjectParser<AIAbility>.Parse("ManticoreClaw", value, errorInfo),
                GetObject = () => ManticoreClaw as IObjectContentGenerator } },
           { "ManticoreSting1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ManticoreSting1 = 
                   JsonObjectParser<AIAbility>.Parse("ManticoreSting1", value, errorInfo),
                GetObject = () => ManticoreSting1 as IObjectContentGenerator } },
           { "Manticoresting2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Manticoresting2 = 
                   JsonObjectParser<AIAbility>.Parse("Manticoresting2", value, errorInfo),
                GetObject = () => Manticoresting2 as IObjectContentGenerator } },
           { "MantisAcidBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MantisAcidBurst = 
                   JsonObjectParser<AIAbility>.Parse("MantisAcidBurst", value, errorInfo),
                GetObject = () => MantisAcidBurst as IObjectContentGenerator } },
           { "MantisBlast", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MantisBlast = 
                   JsonObjectParser<AIAbility>.Parse("MantisBlast", value, errorInfo),
                GetObject = () => MantisBlast as IObjectContentGenerator } },
           { "MantisClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MantisClaw = 
                   JsonObjectParser<AIAbility>.Parse("MantisClaw", value, errorInfo),
                GetObject = () => MantisClaw as IObjectContentGenerator } },
           { "MantisRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MantisRage = 
                   JsonObjectParser<AIAbility>.Parse("MantisRage", value, errorInfo),
                GetObject = () => MantisRage as IObjectContentGenerator } },
           { "MantisSwipe", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MantisSwipe = 
                   JsonObjectParser<AIAbility>.Parse("MantisSwipe", value, errorInfo),
                GetObject = () => MantisSwipe as IObjectContentGenerator } },
           { "MaronesaInfect", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MaronesaInfect = 
                   JsonObjectParser<AIAbility>.Parse("MaronesaInfect", value, errorInfo),
                GetObject = () => MaronesaInfect as IObjectContentGenerator } },
           { "MaronesaStomp", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MaronesaStomp = 
                   JsonObjectParser<AIAbility>.Parse("MaronesaStomp", value, errorInfo),
                GetObject = () => MaronesaStomp as IObjectContentGenerator } },
           { "MinigolemAoEHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEHeal = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEHeal", value, errorInfo),
                GetObject = () => MinigolemAoEHeal as IObjectContentGenerator } },
           { "MinigolemAoEHeal2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEHeal2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEHeal2", value, errorInfo),
                GetObject = () => MinigolemAoEHeal2 as IObjectContentGenerator } },
           { "MinigolemAoEHeal3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEHeal3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEHeal3", value, errorInfo),
                GetObject = () => MinigolemAoEHeal3 as IObjectContentGenerator } },
           { "MinigolemAoEHeal4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEHeal4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEHeal4", value, errorInfo),
                GetObject = () => MinigolemAoEHeal4 as IObjectContentGenerator } },
           { "MinigolemAoEHeal5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEHeal5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEHeal5", value, errorInfo),
                GetObject = () => MinigolemAoEHeal5 as IObjectContentGenerator } },
           { "MinigolemAoEPower", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEPower = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEPower", value, errorInfo),
                GetObject = () => MinigolemAoEPower as IObjectContentGenerator } },
           { "MinigolemAoEPower2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEPower2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEPower2", value, errorInfo),
                GetObject = () => MinigolemAoEPower2 as IObjectContentGenerator } },
           { "MinigolemAoEPower3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEPower3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEPower3", value, errorInfo),
                GetObject = () => MinigolemAoEPower3 as IObjectContentGenerator } },
           { "MinigolemAoEPower4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEPower4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEPower4", value, errorInfo),
                GetObject = () => MinigolemAoEPower4 as IObjectContentGenerator } },
           { "MinigolemAoEPower5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemAoEPower5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemAoEPower5", value, errorInfo),
                GetObject = () => MinigolemAoEPower5 as IObjectContentGenerator } },
           { "MinigolemBombToss", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemBombToss = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemBombToss", value, errorInfo),
                GetObject = () => MinigolemBombToss as IObjectContentGenerator } },
           { "MinigolemBombToss2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemBombToss2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemBombToss2", value, errorInfo),
                GetObject = () => MinigolemBombToss2 as IObjectContentGenerator } },
           { "MinigolemBombToss3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemBombToss3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemBombToss3", value, errorInfo),
                GetObject = () => MinigolemBombToss3 as IObjectContentGenerator } },
           { "MinigolemBombToss4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemBombToss4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemBombToss4", value, errorInfo),
                GetObject = () => MinigolemBombToss4 as IObjectContentGenerator } },
           { "MinigolemBombToss5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemBombToss5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemBombToss5", value, errorInfo),
                GetObject = () => MinigolemBombToss5 as IObjectContentGenerator } },
           { "MinigolemDoomAdmixture", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemDoomAdmixture = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemDoomAdmixture", value, errorInfo),
                GetObject = () => MinigolemDoomAdmixture as IObjectContentGenerator } },
           { "MinigolemDoomAdmixture2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemDoomAdmixture2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemDoomAdmixture2", value, errorInfo),
                GetObject = () => MinigolemDoomAdmixture2 as IObjectContentGenerator } },
           { "MinigolemDoomAdmixture3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemDoomAdmixture3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemDoomAdmixture3", value, errorInfo),
                GetObject = () => MinigolemDoomAdmixture3 as IObjectContentGenerator } },
           { "MinigolemDoomAdmixture4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemDoomAdmixture4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemDoomAdmixture4", value, errorInfo),
                GetObject = () => MinigolemDoomAdmixture4 as IObjectContentGenerator } },
           { "MinigolemDoomAdmixture5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemDoomAdmixture5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemDoomAdmixture5", value, errorInfo),
                GetObject = () => MinigolemDoomAdmixture5 as IObjectContentGenerator } },
           { "MinigolemFireBalm1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemFireBalm1 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemFireBalm1", value, errorInfo),
                GetObject = () => MinigolemFireBalm1 as IObjectContentGenerator } },
           { "MinigolemFireBalm2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemFireBalm2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemFireBalm2", value, errorInfo),
                GetObject = () => MinigolemFireBalm2 as IObjectContentGenerator } },
           { "MinigolemFireBalm3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemFireBalm3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemFireBalm3", value, errorInfo),
                GetObject = () => MinigolemFireBalm3 as IObjectContentGenerator } },
           { "MinigolemFireBalm4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemFireBalm4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemFireBalm4", value, errorInfo),
                GetObject = () => MinigolemFireBalm4 as IObjectContentGenerator } },
           { "MinigolemFireBalm5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemFireBalm5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemFireBalm5", value, errorInfo),
                GetObject = () => MinigolemFireBalm5 as IObjectContentGenerator } },
           { "MinigolemHasteConcoction1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHasteConcoction1 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHasteConcoction1", value, errorInfo),
                GetObject = () => MinigolemHasteConcoction1 as IObjectContentGenerator } },
           { "MinigolemHasteConcoction2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHasteConcoction2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHasteConcoction2", value, errorInfo),
                GetObject = () => MinigolemHasteConcoction2 as IObjectContentGenerator } },
           { "MinigolemHasteConcoction3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHasteConcoction3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHasteConcoction3", value, errorInfo),
                GetObject = () => MinigolemHasteConcoction3 as IObjectContentGenerator } },
           { "MinigolemHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHeal = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHeal", value, errorInfo),
                GetObject = () => MinigolemHeal as IObjectContentGenerator } },
           { "MinigolemHeal2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHeal2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHeal2", value, errorInfo),
                GetObject = () => MinigolemHeal2 as IObjectContentGenerator } },
           { "MinigolemHeal3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHeal3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHeal3", value, errorInfo),
                GetObject = () => MinigolemHeal3 as IObjectContentGenerator } },
           { "MinigolemHeal4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHeal4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHeal4", value, errorInfo),
                GetObject = () => MinigolemHeal4 as IObjectContentGenerator } },
           { "MinigolemHeal5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemHeal5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemHeal5", value, errorInfo),
                GetObject = () => MinigolemHeal5 as IObjectContentGenerator } },
           { "MinigolemPunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemPunch = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemPunch", value, errorInfo),
                GetObject = () => MinigolemPunch as IObjectContentGenerator } },
           { "MinigolemPunch2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemPunch2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemPunch2", value, errorInfo),
                GetObject = () => MinigolemPunch2 as IObjectContentGenerator } },
           { "MinigolemPunch3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemPunch3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemPunch3", value, errorInfo),
                GetObject = () => MinigolemPunch3 as IObjectContentGenerator } },
           { "MinigolemPunch4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemPunch4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemPunch4", value, errorInfo),
                GetObject = () => MinigolemPunch4 as IObjectContentGenerator } },
           { "MinigolemPunch5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemPunch5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemPunch5", value, errorInfo),
                GetObject = () => MinigolemPunch5 as IObjectContentGenerator } },
           { "MinigolemRageAcidToss1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAcidToss1 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAcidToss1", value, errorInfo),
                GetObject = () => MinigolemRageAcidToss1 as IObjectContentGenerator } },
           { "MinigolemRageAcidToss2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAcidToss2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAcidToss2", value, errorInfo),
                GetObject = () => MinigolemRageAcidToss2 as IObjectContentGenerator } },
           { "MinigolemRageAcidToss3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAcidToss3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAcidToss3", value, errorInfo),
                GetObject = () => MinigolemRageAcidToss3 as IObjectContentGenerator } },
           { "MinigolemRageAcidToss4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAcidToss4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAcidToss4", value, errorInfo),
                GetObject = () => MinigolemRageAcidToss4 as IObjectContentGenerator } },
           { "MinigolemRageAcidToss5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAcidToss5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAcidToss5", value, errorInfo),
                GetObject = () => MinigolemRageAcidToss5 as IObjectContentGenerator } },
           { "MinigolemRageAoEHeal1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAoEHeal1 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAoEHeal1", value, errorInfo),
                GetObject = () => MinigolemRageAoEHeal1 as IObjectContentGenerator } },
           { "MinigolemRageAoEHeal2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAoEHeal2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAoEHeal2", value, errorInfo),
                GetObject = () => MinigolemRageAoEHeal2 as IObjectContentGenerator } },
           { "MinigolemRageAoEHeal3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAoEHeal3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAoEHeal3", value, errorInfo),
                GetObject = () => MinigolemRageAoEHeal3 as IObjectContentGenerator } },
           { "MinigolemRageAoEHeal4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAoEHeal4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAoEHeal4", value, errorInfo),
                GetObject = () => MinigolemRageAoEHeal4 as IObjectContentGenerator } },
           { "MinigolemRageAoEHeal5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemRageAoEHeal5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemRageAoEHeal5", value, errorInfo),
                GetObject = () => MinigolemRageAoEHeal5 as IObjectContentGenerator } },
           { "MinigolemSelfDestruct", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfDestruct = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfDestruct", value, errorInfo),
                GetObject = () => MinigolemSelfDestruct as IObjectContentGenerator } },
           { "MinigolemSelfDestruct2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfDestruct2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfDestruct2", value, errorInfo),
                GetObject = () => MinigolemSelfDestruct2 as IObjectContentGenerator } },
           { "MinigolemSelfDestruct3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfDestruct3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfDestruct3", value, errorInfo),
                GetObject = () => MinigolemSelfDestruct3 as IObjectContentGenerator } },
           { "MinigolemSelfDestruct4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfDestruct4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfDestruct4", value, errorInfo),
                GetObject = () => MinigolemSelfDestruct4 as IObjectContentGenerator } },
           { "MinigolemSelfDestruct5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfDestruct5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfDestruct5", value, errorInfo),
                GetObject = () => MinigolemSelfDestruct5 as IObjectContentGenerator } },
           { "MinigolemSelfSacrifice", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfSacrifice = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfSacrifice", value, errorInfo),
                GetObject = () => MinigolemSelfSacrifice as IObjectContentGenerator } },
           { "MinigolemSelfSacrifice2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfSacrifice2 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfSacrifice2", value, errorInfo),
                GetObject = () => MinigolemSelfSacrifice2 as IObjectContentGenerator } },
           { "MinigolemSelfSacrifice3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfSacrifice3 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfSacrifice3", value, errorInfo),
                GetObject = () => MinigolemSelfSacrifice3 as IObjectContentGenerator } },
           { "MinigolemSelfSacrifice4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfSacrifice4 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfSacrifice4", value, errorInfo),
                GetObject = () => MinigolemSelfSacrifice4 as IObjectContentGenerator } },
           { "MinigolemSelfSacrifice5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinigolemSelfSacrifice5 = 
                   JsonObjectParser<AIAbility>.Parse("MinigolemSelfSacrifice5", value, errorInfo),
                GetObject = () => MinigolemSelfSacrifice5 as IObjectContentGenerator } },
           { "MinotaurBossRageClub", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinotaurBossRageClub = 
                   JsonObjectParser<AIAbility>.Parse("MinotaurBossRageClub", value, errorInfo),
                GetObject = () => MinotaurBossRageClub as IObjectContentGenerator } },
           { "MinotaurBoulder", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinotaurBoulder = 
                   JsonObjectParser<AIAbility>.Parse("MinotaurBoulder", value, errorInfo),
                GetObject = () => MinotaurBoulder as IObjectContentGenerator } },
           { "MinotaurClub", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinotaurClub = 
                   JsonObjectParser<AIAbility>.Parse("MinotaurClub", value, errorInfo),
                GetObject = () => MinotaurClub as IObjectContentGenerator } },
           { "MinotaurRageClub", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MinotaurRageClub = 
                   JsonObjectParser<AIAbility>.Parse("MinotaurRageClub", value, errorInfo),
                GetObject = () => MinotaurRageClub as IObjectContentGenerator } },
           { "MonsterWerewolfHowl", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MonsterWerewolfHowl = 
                   JsonObjectParser<AIAbility>.Parse("MonsterWerewolfHowl", value, errorInfo),
                GetObject = () => MonsterWerewolfHowl as IObjectContentGenerator } },
           { "MonsterWerewolfPackAttack", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MonsterWerewolfPackAttack = 
                   JsonObjectParser<AIAbility>.Parse("MonsterWerewolfPackAttack", value, errorInfo),
                GetObject = () => MonsterWerewolfPackAttack as IObjectContentGenerator } },
           { "MonsterWerewolfPouncingRake", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MonsterWerewolfPouncingRake = 
                   JsonObjectParser<AIAbility>.Parse("MonsterWerewolfPouncingRake", value, errorInfo),
                GetObject = () => MonsterWerewolfPouncingRake as IObjectContentGenerator } },
           { "MummySlamA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MummySlamA = 
                   JsonObjectParser<AIAbility>.Parse("MummySlamA", value, errorInfo),
                GetObject = () => MummySlamA as IObjectContentGenerator } },
           { "MummySlamB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MummySlamB = 
                   JsonObjectParser<AIAbility>.Parse("MummySlamB", value, errorInfo),
                GetObject = () => MummySlamB as IObjectContentGenerator } },
           { "MummySlamCombo", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MummySlamCombo = 
                   JsonObjectParser<AIAbility>.Parse("MummySlamCombo", value, errorInfo),
                GetObject = () => MummySlamCombo as IObjectContentGenerator } },
           { "MummyWrapA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MummyWrapA = 
                   JsonObjectParser<AIAbility>.Parse("MummyWrapA", value, errorInfo),
                GetObject = () => MummyWrapA as IObjectContentGenerator } },
           { "MummyWrapB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MummyWrapB = 
                   JsonObjectParser<AIAbility>.Parse("MummyWrapB", value, errorInfo),
                GetObject = () => MummyWrapB as IObjectContentGenerator } },
           { "MummyWrapRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MummyWrapRage = 
                   JsonObjectParser<AIAbility>.Parse("MummyWrapRage", value, errorInfo),
                GetObject = () => MummyWrapRage as IObjectContentGenerator } },
           { "MushroomMonster_Bite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_Bite = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_Bite", value, errorInfo),
                GetObject = () => MushroomMonster_Bite as IObjectContentGenerator } },
           { "MushroomMonster_SpawnSpit1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_SpawnSpit1 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_SpawnSpit1", value, errorInfo),
                GetObject = () => MushroomMonster_SpawnSpit1 as IObjectContentGenerator } },
           { "MushroomMonster_SpawnSpit2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_SpawnSpit2 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_SpawnSpit2", value, errorInfo),
                GetObject = () => MushroomMonster_SpawnSpit2 as IObjectContentGenerator } },
           { "MushroomMonster_SpawnSuperSpit1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_SpawnSuperSpit1 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_SpawnSuperSpit1", value, errorInfo),
                GetObject = () => MushroomMonster_SpawnSuperSpit1 as IObjectContentGenerator } },
           { "MushroomMonster_SpawnSuperSpit2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_SpawnSuperSpit2 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_SpawnSuperSpit2", value, errorInfo),
                GetObject = () => MushroomMonster_SpawnSuperSpit2 as IObjectContentGenerator } },
           { "MushroomMonster_Spit1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_Spit1 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_Spit1", value, errorInfo),
                GetObject = () => MushroomMonster_Spit1 as IObjectContentGenerator } },
           { "MushroomMonster_Spit2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_Spit2 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_Spit2", value, errorInfo),
                GetObject = () => MushroomMonster_Spit2 as IObjectContentGenerator } },
           { "MushroomMonster_SummonMushroomSpawn1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_SummonMushroomSpawn1 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_SummonMushroomSpawn1", value, errorInfo),
                GetObject = () => MushroomMonster_SummonMushroomSpawn1 as IObjectContentGenerator } },
           { "MushroomMonster_SummonMushroomSpawn2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_SummonMushroomSpawn2 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_SummonMushroomSpawn2", value, errorInfo),
                GetObject = () => MushroomMonster_SummonMushroomSpawn2 as IObjectContentGenerator } },
           { "MushroomMonster_SummonMushroomSpawn3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => MushroomMonster_SummonMushroomSpawn3 = 
                   JsonObjectParser<AIAbility>.Parse("MushroomMonster_SummonMushroomSpawn3", value, errorInfo),
                GetObject = () => MushroomMonster_SummonMushroomSpawn3 as IObjectContentGenerator } },
           { "Myconian_Bash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Myconian_Bash = 
                   JsonObjectParser<AIAbility>.Parse("Myconian_Bash", value, errorInfo),
                GetObject = () => Myconian_Bash as IObjectContentGenerator } },
           { "Myconian_BossBash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Myconian_BossBash = 
                   JsonObjectParser<AIAbility>.Parse("Myconian_BossBash", value, errorInfo),
                GetObject = () => Myconian_BossBash as IObjectContentGenerator } },
           { "Myconian_Drain", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Myconian_Drain = 
                   JsonObjectParser<AIAbility>.Parse("Myconian_Drain", value, errorInfo),
                GetObject = () => Myconian_Drain as IObjectContentGenerator } },
           { "Myconian_Mindspores", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Myconian_Mindspores = 
                   JsonObjectParser<AIAbility>.Parse("Myconian_Mindspores", value, errorInfo),
                GetObject = () => Myconian_Mindspores as IObjectContentGenerator } },
           { "Myconian_Mindspores_Permanent", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Myconian_Mindspores_Permanent = 
                   JsonObjectParser<AIAbility>.Parse("Myconian_Mindspores_Permanent", value, errorInfo),
                GetObject = () => Myconian_Mindspores_Permanent as IObjectContentGenerator } },
           { "Myconian_Shock", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Myconian_Shock = 
                   JsonObjectParser<AIAbility>.Parse("Myconian_Shock", value, errorInfo),
                GetObject = () => Myconian_Shock as IObjectContentGenerator } },
           { "Myconian_TidalCurse", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Myconian_TidalCurse = 
                   JsonObjectParser<AIAbility>.Parse("Myconian_TidalCurse", value, errorInfo),
                GetObject = () => Myconian_TidalCurse as IObjectContentGenerator } },
           { "NecroDarknessWave", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NecroDarknessWave = 
                   JsonObjectParser<AIAbility>.Parse("NecroDarknessWave", value, errorInfo),
                GetObject = () => NecroDarknessWave as IObjectContentGenerator } },
           { "NecroDeathsHold", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NecroDeathsHold = 
                   JsonObjectParser<AIAbility>.Parse("NecroDeathsHold", value, errorInfo),
                GetObject = () => NecroDeathsHold as IObjectContentGenerator } },
           { "NecroPainBubble", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NecroPainBubble = 
                   JsonObjectParser<AIAbility>.Parse("NecroPainBubble", value, errorInfo),
                GetObject = () => NecroPainBubble as IObjectContentGenerator } },
           { "NecroSpark", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NecroSpark = 
                   JsonObjectParser<AIAbility>.Parse("NecroSpark", value, errorInfo),
                GetObject = () => NecroSpark as IObjectContentGenerator } },
           { "NecroSparkPerching", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NecroSparkPerching = 
                   JsonObjectParser<AIAbility>.Parse("NecroSparkPerching", value, errorInfo),
                GetObject = () => NecroSparkPerching as IObjectContentGenerator } },
           { "NightmareDarknessBomb", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NightmareDarknessBomb = 
                   JsonObjectParser<AIAbility>.Parse("NightmareDarknessBomb", value, errorInfo),
                GetObject = () => NightmareDarknessBomb as IObjectContentGenerator } },
           { "NightmareHoof", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NightmareHoof = 
                   JsonObjectParser<AIAbility>.Parse("NightmareHoof", value, errorInfo),
                GetObject = () => NightmareHoof as IObjectContentGenerator } },
           { "NpcBlockingStance", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NpcBlockingStance = 
                   JsonObjectParser<AIAbility>.Parse("NpcBlockingStance", value, errorInfo),
                GetObject = () => NpcBlockingStance as IObjectContentGenerator } },
           { "NpcDoubleHitCurse", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NpcDoubleHitCurse = 
                   JsonObjectParser<AIAbility>.Parse("NpcDoubleHitCurse", value, errorInfo),
                GetObject = () => NpcDoubleHitCurse as IObjectContentGenerator } },
           { "NpcHeadcracker", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NpcHeadcracker = 
                   JsonObjectParser<AIAbility>.Parse("NpcHeadcracker", value, errorInfo),
                GetObject = () => NpcHeadcracker as IObjectContentGenerator } },
           { "NpcSmash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => NpcSmash = 
                   JsonObjectParser<AIAbility>.Parse("NpcSmash", value, errorInfo),
                GetObject = () => NpcSmash as IObjectContentGenerator } },
           { "OgreClubA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OgreClubA = 
                   JsonObjectParser<AIAbility>.Parse("OgreClubA", value, errorInfo),
                GetObject = () => OgreClubA as IObjectContentGenerator } },
           { "OgreClubB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OgreClubB = 
                   JsonObjectParser<AIAbility>.Parse("OgreClubB", value, errorInfo),
                GetObject = () => OgreClubB as IObjectContentGenerator } },
           { "OgreStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OgreStun = 
                   JsonObjectParser<AIAbility>.Parse("OgreStun", value, errorInfo),
                GetObject = () => OgreStun as IObjectContentGenerator } },
           { "OgreThrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OgreThrow = 
                   JsonObjectParser<AIAbility>.Parse("OgreThrow", value, errorInfo),
                GetObject = () => OgreThrow as IObjectContentGenerator } },
           { "OrcAreaHalberdAttack", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcAreaHalberdAttack = 
                   JsonObjectParser<AIAbility>.Parse("OrcAreaHalberdAttack", value, errorInfo),
                GetObject = () => OrcAreaHalberdAttack as IObjectContentGenerator } },
           { "OrcAreaHalberdBoss", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcAreaHalberdBoss = 
                   JsonObjectParser<AIAbility>.Parse("OrcAreaHalberdBoss", value, errorInfo),
                GetObject = () => OrcAreaHalberdBoss as IObjectContentGenerator } },
           { "OrcArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("OrcArrow1", value, errorInfo),
                GetObject = () => OrcArrow1 as IObjectContentGenerator } },
           { "OrcArrow2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcArrow2 = 
                   JsonObjectParser<AIAbility>.Parse("OrcArrow2", value, errorInfo),
                GetObject = () => OrcArrow2 as IObjectContentGenerator } },
           { "OrcDarknessBall", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcDarknessBall = 
                   JsonObjectParser<AIAbility>.Parse("OrcDarknessBall", value, errorInfo),
                GetObject = () => OrcDarknessBall as IObjectContentGenerator } },
           { "OrcDeathsHold", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcDeathsHold = 
                   JsonObjectParser<AIAbility>.Parse("OrcDeathsHold", value, errorInfo),
                GetObject = () => OrcDeathsHold as IObjectContentGenerator } },
           { "OrcDebuffArrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcDebuffArrow = 
                   JsonObjectParser<AIAbility>.Parse("OrcDebuffArrow", value, errorInfo),
                GetObject = () => OrcDebuffArrow as IObjectContentGenerator } },
           { "OrcElectricStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcElectricStun = 
                   JsonObjectParser<AIAbility>.Parse("OrcElectricStun", value, errorInfo),
                GetObject = () => OrcElectricStun as IObjectContentGenerator } },
           { "OrcEvasionBubble", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcEvasionBubble = 
                   JsonObjectParser<AIAbility>.Parse("OrcEvasionBubble", value, errorInfo),
                GetObject = () => OrcEvasionBubble as IObjectContentGenerator } },
           { "OrcExtinguishLife", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcExtinguishLife = 
                   JsonObjectParser<AIAbility>.Parse("OrcExtinguishLife", value, errorInfo),
                GetObject = () => OrcExtinguishLife as IObjectContentGenerator } },
           { "OrcFinishingBlow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcFinishingBlow = 
                   JsonObjectParser<AIAbility>.Parse("OrcFinishingBlow", value, errorInfo),
                GetObject = () => OrcFinishingBlow as IObjectContentGenerator } },
           { "OrcFinishingBlowFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcFinishingBlowFire = 
                   JsonObjectParser<AIAbility>.Parse("OrcFinishingBlowFire", value, errorInfo),
                GetObject = () => OrcFinishingBlowFire as IObjectContentGenerator } },
           { "OrcFireball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcFireball = 
                   JsonObjectParser<AIAbility>.Parse("OrcFireball", value, errorInfo),
                GetObject = () => OrcFireball as IObjectContentGenerator } },
           { "OrcFireBolts", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcFireBolts = 
                   JsonObjectParser<AIAbility>.Parse("OrcFireBolts", value, errorInfo),
                GetObject = () => OrcFireBolts as IObjectContentGenerator } },
           { "OrcHalberdAttack", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcHalberdAttack = 
                   JsonObjectParser<AIAbility>.Parse("OrcHalberdAttack", value, errorInfo),
                GetObject = () => OrcHalberdAttack as IObjectContentGenerator } },
           { "OrcHeal1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcHeal1 = 
                   JsonObjectParser<AIAbility>.Parse("OrcHeal1", value, errorInfo),
                GetObject = () => OrcHeal1 as IObjectContentGenerator } },
           { "OrcHeal2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcHeal2 = 
                   JsonObjectParser<AIAbility>.Parse("OrcHeal2", value, errorInfo),
                GetObject = () => OrcHeal2 as IObjectContentGenerator } },
           { "OrcHipThrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcHipThrow = 
                   JsonObjectParser<AIAbility>.Parse("OrcHipThrow", value, errorInfo),
                GetObject = () => OrcHipThrow as IObjectContentGenerator } },
           { "OrcKneeKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcKneeKick = 
                   JsonObjectParser<AIAbility>.Parse("OrcKneeKick", value, errorInfo),
                GetObject = () => OrcKneeKick as IObjectContentGenerator } },
           { "OrcKnockbackBolt", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcKnockbackBolt = 
                   JsonObjectParser<AIAbility>.Parse("OrcKnockbackBolt", value, errorInfo),
                GetObject = () => OrcKnockbackBolt as IObjectContentGenerator } },
           { "OrcLieutenantDebuffTaunt", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcLieutenantDebuffTaunt = 
                   JsonObjectParser<AIAbility>.Parse("OrcLieutenantDebuffTaunt", value, errorInfo),
                GetObject = () => OrcLieutenantDebuffTaunt as IObjectContentGenerator } },
           { "OrcParry", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcParry = 
                   JsonObjectParser<AIAbility>.Parse("OrcParry", value, errorInfo),
                GetObject = () => OrcParry as IObjectContentGenerator } },
           { "OrcParryFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcParryFire = 
                   JsonObjectParser<AIAbility>.Parse("OrcParryFire", value, errorInfo),
                GetObject = () => OrcParryFire as IObjectContentGenerator } },
           { "OrcPunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcPunch = 
                   JsonObjectParser<AIAbility>.Parse("OrcPunch", value, errorInfo),
                GetObject = () => OrcPunch as IObjectContentGenerator } },
           { "OrcSlice", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcSlice = 
                   JsonObjectParser<AIAbility>.Parse("OrcSlice", value, errorInfo),
                GetObject = () => OrcSlice as IObjectContentGenerator } },
           { "OrcSpearAttack", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcSpearAttack = 
                   JsonObjectParser<AIAbility>.Parse("OrcSpearAttack", value, errorInfo),
                GetObject = () => OrcSpearAttack as IObjectContentGenerator } },
           { "OrcStaffSmash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcStaffSmash = 
                   JsonObjectParser<AIAbility>.Parse("OrcStaffSmash", value, errorInfo),
                GetObject = () => OrcStaffSmash as IObjectContentGenerator } },
           { "OrcSummonSigil1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcSummonSigil1 = 
                   JsonObjectParser<AIAbility>.Parse("OrcSummonSigil1", value, errorInfo),
                GetObject = () => OrcSummonSigil1 as IObjectContentGenerator } },
           { "OrcSummonUrak2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcSummonUrak2 = 
                   JsonObjectParser<AIAbility>.Parse("OrcSummonUrak2", value, errorInfo),
                GetObject = () => OrcSummonUrak2 as IObjectContentGenerator } },
           { "OrcSwordSlash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcSwordSlash = 
                   JsonObjectParser<AIAbility>.Parse("OrcSwordSlash", value, errorInfo),
                GetObject = () => OrcSwordSlash as IObjectContentGenerator } },
           { "OrcSwordSlashFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcSwordSlashFire = 
                   JsonObjectParser<AIAbility>.Parse("OrcSwordSlashFire", value, errorInfo),
                GetObject = () => OrcSwordSlashFire as IObjectContentGenerator } },
           { "OrcVenomstrike0", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcVenomstrike0 = 
                   JsonObjectParser<AIAbility>.Parse("OrcVenomstrike0", value, errorInfo),
                GetObject = () => OrcVenomstrike0 as IObjectContentGenerator } },
           { "OrcVenomstrike1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcVenomstrike1 = 
                   JsonObjectParser<AIAbility>.Parse("OrcVenomstrike1", value, errorInfo),
                GetObject = () => OrcVenomstrike1 as IObjectContentGenerator } },
           { "OrcWaveOfDarkness", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => OrcWaveOfDarkness = 
                   JsonObjectParser<AIAbility>.Parse("OrcWaveOfDarkness", value, errorInfo),
                GetObject = () => OrcWaveOfDarkness as IObjectContentGenerator } },
           { "Peck", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Peck = 
                   JsonObjectParser<AIAbility>.Parse("Peck", value, errorInfo),
                GetObject = () => Peck as IObjectContentGenerator } },
           { "PetUndeadArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadArrow1", value, errorInfo),
                GetObject = () => PetUndeadArrow1 as IObjectContentGenerator } },
           { "PetUndeadArrow2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadArrow2 = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadArrow2", value, errorInfo),
                GetObject = () => PetUndeadArrow2 as IObjectContentGenerator } },
           { "PetUndeadDefensiveBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadDefensiveBurst = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadDefensiveBurst", value, errorInfo),
                GetObject = () => PetUndeadDefensiveBurst as IObjectContentGenerator } },
           { "PetUndeadFireballA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadFireballA = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadFireballA", value, errorInfo),
                GetObject = () => PetUndeadFireballA as IObjectContentGenerator } },
           { "PetUndeadFireballB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadFireballB = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadFireballB", value, errorInfo),
                GetObject = () => PetUndeadFireballB as IObjectContentGenerator } },
           { "PetUndeadOmegaArrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadOmegaArrow = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadOmegaArrow", value, errorInfo),
                GetObject = () => PetUndeadOmegaArrow as IObjectContentGenerator } },
           { "PetUndeadPunch1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadPunch1 = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadPunch1", value, errorInfo),
                GetObject = () => PetUndeadPunch1 as IObjectContentGenerator } },
           { "PetUndeadSword1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadSword1 = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadSword1", value, errorInfo),
                GetObject = () => PetUndeadSword1 as IObjectContentGenerator } },
           { "PetUndeadSword2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadSword2 = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadSword2", value, errorInfo),
                GetObject = () => PetUndeadSword2 as IObjectContentGenerator } },
           { "PetUndeadSwordAngry", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PetUndeadSwordAngry = 
                   JsonObjectParser<AIAbility>.Parse("PetUndeadSwordAngry", value, errorInfo),
                GetObject = () => PetUndeadSwordAngry as IObjectContentGenerator } },
           { "RakAcidBomb", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakAcidBomb = 
                   JsonObjectParser<AIAbility>.Parse("RakAcidBomb", value, errorInfo),
                GetObject = () => RakAcidBomb as IObjectContentGenerator } },
           { "RakAimedShot", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakAimedShot = 
                   JsonObjectParser<AIAbility>.Parse("RakAimedShot", value, errorInfo),
                GetObject = () => RakAimedShot as IObjectContentGenerator } },
           { "RakBarrage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakBarrage = 
                   JsonObjectParser<AIAbility>.Parse("RakBarrage", value, errorInfo),
                GetObject = () => RakBarrage as IObjectContentGenerator } },
           { "RakBasicShot", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakBasicShot = 
                   JsonObjectParser<AIAbility>.Parse("RakBasicShot", value, errorInfo),
                GetObject = () => RakBasicShot as IObjectContentGenerator } },
           { "RakBossPerchSlow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakBossPerchSlow = 
                   JsonObjectParser<AIAbility>.Parse("RakBossPerchSlow", value, errorInfo),
                GetObject = () => RakBossPerchSlow as IObjectContentGenerator } },
           { "RakBossSlow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakBossSlow = 
                   JsonObjectParser<AIAbility>.Parse("RakBossSlow", value, errorInfo),
                GetObject = () => RakBossSlow as IObjectContentGenerator } },
           { "RakBowBash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakBowBash = 
                   JsonObjectParser<AIAbility>.Parse("RakBowBash", value, errorInfo),
                GetObject = () => RakBowBash as IObjectContentGenerator } },
           { "RakBreatheFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakBreatheFire = 
                   JsonObjectParser<AIAbility>.Parse("RakBreatheFire", value, errorInfo),
                GetObject = () => RakBreatheFire as IObjectContentGenerator } },
           { "RakDecapitate", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakDecapitate = 
                   JsonObjectParser<AIAbility>.Parse("RakDecapitate", value, errorInfo),
                GetObject = () => RakDecapitate as IObjectContentGenerator } },
           { "RakFireball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakFireball = 
                   JsonObjectParser<AIAbility>.Parse("RakFireball", value, errorInfo),
                GetObject = () => RakFireball as IObjectContentGenerator } },
           { "RakHackingBlade", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakHackingBlade = 
                   JsonObjectParser<AIAbility>.Parse("RakHackingBlade", value, errorInfo),
                GetObject = () => RakHackingBlade as IObjectContentGenerator } },
           { "RakHealingMist", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakHealingMist = 
                   JsonObjectParser<AIAbility>.Parse("RakHealingMist", value, errorInfo),
                GetObject = () => RakHealingMist as IObjectContentGenerator } },
           { "RakHookShot", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakHookShot = 
                   JsonObjectParser<AIAbility>.Parse("RakHookShot", value, errorInfo),
                GetObject = () => RakHookShot as IObjectContentGenerator } },
           { "RakKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakKick = 
                   JsonObjectParser<AIAbility>.Parse("RakKick", value, errorInfo),
                GetObject = () => RakKick as IObjectContentGenerator } },
           { "RakKnee", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakKnee = 
                   JsonObjectParser<AIAbility>.Parse("RakKnee", value, errorInfo),
                GetObject = () => RakKnee as IObjectContentGenerator } },
           { "RakMindreave", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakMindreave = 
                   JsonObjectParser<AIAbility>.Parse("RakMindreave", value, errorInfo),
                GetObject = () => RakMindreave as IObjectContentGenerator } },
           { "RakPainBubble", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakPainBubble = 
                   JsonObjectParser<AIAbility>.Parse("RakPainBubble", value, errorInfo),
                GetObject = () => RakPainBubble as IObjectContentGenerator } },
           { "RakPanicCharge", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakPanicCharge = 
                   JsonObjectParser<AIAbility>.Parse("RakPanicCharge", value, errorInfo),
                GetObject = () => RakPanicCharge as IObjectContentGenerator } },
           { "RakPoisonArrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakPoisonArrow = 
                   JsonObjectParser<AIAbility>.Parse("RakPoisonArrow", value, errorInfo),
                GetObject = () => RakPoisonArrow as IObjectContentGenerator } },
           { "RakReconstruct", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakReconstruct = 
                   JsonObjectParser<AIAbility>.Parse("RakReconstruct", value, errorInfo),
                GetObject = () => RakReconstruct as IObjectContentGenerator } },
           { "RakRevitalize", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakRevitalize = 
                   JsonObjectParser<AIAbility>.Parse("RakRevitalize", value, errorInfo),
                GetObject = () => RakRevitalize as IObjectContentGenerator } },
           { "RakRingOfFire", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakRingOfFire = 
                   JsonObjectParser<AIAbility>.Parse("RakRingOfFire", value, errorInfo),
                GetObject = () => RakRingOfFire as IObjectContentGenerator } },
           { "RakSlash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakSlash = 
                   JsonObjectParser<AIAbility>.Parse("RakSlash", value, errorInfo),
                GetObject = () => RakSlash as IObjectContentGenerator } },
           { "RakStaffBlock", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakStaffBlock = 
                   JsonObjectParser<AIAbility>.Parse("RakStaffBlock", value, errorInfo),
                GetObject = () => RakStaffBlock as IObjectContentGenerator } },
           { "RakStaffHeavy", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakStaffHeavy = 
                   JsonObjectParser<AIAbility>.Parse("RakStaffHeavy", value, errorInfo),
                GetObject = () => RakStaffHeavy as IObjectContentGenerator } },
           { "RakStaffHit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakStaffHit = 
                   JsonObjectParser<AIAbility>.Parse("RakStaffHit", value, errorInfo),
                GetObject = () => RakStaffHit as IObjectContentGenerator } },
           { "RakStaffPin", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakStaffPin = 
                   JsonObjectParser<AIAbility>.Parse("RakStaffPin", value, errorInfo),
                GetObject = () => RakStaffPin as IObjectContentGenerator } },
           { "RakSwordSlash", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakSwordSlash = 
                   JsonObjectParser<AIAbility>.Parse("RakSwordSlash", value, errorInfo),
                GetObject = () => RakSwordSlash as IObjectContentGenerator } },
           { "RakToxinBomb", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RakToxinBomb = 
                   JsonObjectParser<AIAbility>.Parse("RakToxinBomb", value, errorInfo),
                GetObject = () => RakToxinBomb as IObjectContentGenerator } },
           { "RanalonDoctrineKeeperBlind", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonDoctrineKeeperBlind = 
                   JsonObjectParser<AIAbility>.Parse("RanalonDoctrineKeeperBlind", value, errorInfo),
                GetObject = () => RanalonDoctrineKeeperBlind as IObjectContentGenerator } },
           { "RanalonDoctrineKeeperStab", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonDoctrineKeeperStab = 
                   JsonObjectParser<AIAbility>.Parse("RanalonDoctrineKeeperStab", value, errorInfo),
                GetObject = () => RanalonDoctrineKeeperStab as IObjectContentGenerator } },
           { "RanalonGuardianBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonGuardianBite = 
                   JsonObjectParser<AIAbility>.Parse("RanalonGuardianBite", value, errorInfo),
                GetObject = () => RanalonGuardianBite as IObjectContentGenerator } },
           { "RanalonGuardianBlind", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonGuardianBlind = 
                   JsonObjectParser<AIAbility>.Parse("RanalonGuardianBlind", value, errorInfo),
                GetObject = () => RanalonGuardianBlind as IObjectContentGenerator } },
           { "RanalonGuardianStab", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonGuardianStab = 
                   JsonObjectParser<AIAbility>.Parse("RanalonGuardianStab", value, errorInfo),
                GetObject = () => RanalonGuardianStab as IObjectContentGenerator } },
           { "RanalonGuardianStabB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonGuardianStabB = 
                   JsonObjectParser<AIAbility>.Parse("RanalonGuardianStabB", value, errorInfo),
                GetObject = () => RanalonGuardianStabB as IObjectContentGenerator } },
           { "RanalonHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonHeal = 
                   JsonObjectParser<AIAbility>.Parse("RanalonHeal", value, errorInfo),
                GetObject = () => RanalonHeal as IObjectContentGenerator } },
           { "RanalonHit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonHit = 
                   JsonObjectParser<AIAbility>.Parse("RanalonHit", value, errorInfo),
                GetObject = () => RanalonHit as IObjectContentGenerator } },
           { "RanalonHitB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonHitB = 
                   JsonObjectParser<AIAbility>.Parse("RanalonHitB", value, errorInfo),
                GetObject = () => RanalonHitB as IObjectContentGenerator } },
           { "RanalonKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonKick = 
                   JsonObjectParser<AIAbility>.Parse("RanalonKick", value, errorInfo),
                GetObject = () => RanalonKick as IObjectContentGenerator } },
           { "RanalonRoot", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonRoot = 
                   JsonObjectParser<AIAbility>.Parse("RanalonRoot", value, errorInfo),
                GetObject = () => RanalonRoot as IObjectContentGenerator } },
           { "RanalonSelfBuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonSelfBuff = 
                   JsonObjectParser<AIAbility>.Parse("RanalonSelfBuff", value, errorInfo),
                GetObject = () => RanalonSelfBuff as IObjectContentGenerator } },
           { "RanalonSelfBuffElite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonSelfBuffElite = 
                   JsonObjectParser<AIAbility>.Parse("RanalonSelfBuffElite", value, errorInfo),
                GetObject = () => RanalonSelfBuffElite as IObjectContentGenerator } },
           { "RanalonTongue", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonTongue = 
                   JsonObjectParser<AIAbility>.Parse("RanalonTongue", value, errorInfo),
                GetObject = () => RanalonTongue as IObjectContentGenerator } },
           { "RanalonZap", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonZap = 
                   JsonObjectParser<AIAbility>.Parse("RanalonZap", value, errorInfo),
                GetObject = () => RanalonZap as IObjectContentGenerator } },
           { "RanalonZapB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RanalonZapB = 
                   JsonObjectParser<AIAbility>.Parse("RanalonZapB", value, errorInfo),
                GetObject = () => RanalonZapB as IObjectContentGenerator } },
           { "RatBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBite = 
                   JsonObjectParser<AIAbility>.Parse("RatBite", value, errorInfo),
                GetObject = () => RatBite as IObjectContentGenerator } },
           { "RatBite_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBite_Pet = 
                   JsonObjectParser<AIAbility>.Parse("RatBite_Pet", value, errorInfo),
                GetObject = () => RatBite_Pet as IObjectContentGenerator } },
           { "RatBurn_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBurn_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("RatBurn_Pet1", value, errorInfo),
                GetObject = () => RatBurn_Pet1 as IObjectContentGenerator } },
           { "RatBurn_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBurn_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("RatBurn_Pet2", value, errorInfo),
                GetObject = () => RatBurn_Pet2 as IObjectContentGenerator } },
           { "RatBurn_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBurn_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("RatBurn_Pet3", value, errorInfo),
                GetObject = () => RatBurn_Pet3 as IObjectContentGenerator } },
           { "RatBurn_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBurn_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("RatBurn_Pet4", value, errorInfo),
                GetObject = () => RatBurn_Pet4 as IObjectContentGenerator } },
           { "RatBurn_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBurn_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("RatBurn_Pet5", value, errorInfo),
                GetObject = () => RatBurn_Pet5 as IObjectContentGenerator } },
           { "RatBurn_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatBurn_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("RatBurn_Pet6", value, errorInfo),
                GetObject = () => RatBurn_Pet6 as IObjectContentGenerator } },
           { "RatClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatClaw = 
                   JsonObjectParser<AIAbility>.Parse("RatClaw", value, errorInfo),
                GetObject = () => RatClaw as IObjectContentGenerator } },
           { "RatClaw_Pet", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatClaw_Pet = 
                   JsonObjectParser<AIAbility>.Parse("RatClaw_Pet", value, errorInfo),
                GetObject = () => RatClaw_Pet as IObjectContentGenerator } },
           { "RatDeRage_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatDeRage_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("RatDeRage_Pet1", value, errorInfo),
                GetObject = () => RatDeRage_Pet1 as IObjectContentGenerator } },
           { "RatDeRage_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatDeRage_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("RatDeRage_Pet2", value, errorInfo),
                GetObject = () => RatDeRage_Pet2 as IObjectContentGenerator } },
           { "RatDeRage_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatDeRage_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("RatDeRage_Pet3", value, errorInfo),
                GetObject = () => RatDeRage_Pet3 as IObjectContentGenerator } },
           { "RatDeRage_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatDeRage_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("RatDeRage_Pet4", value, errorInfo),
                GetObject = () => RatDeRage_Pet4 as IObjectContentGenerator } },
           { "RatDeRage_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatDeRage_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("RatDeRage_Pet5", value, errorInfo),
                GetObject = () => RatDeRage_Pet5 as IObjectContentGenerator } },
           { "RatDeRage_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatDeRage_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("RatDeRage_Pet6", value, errorInfo),
                GetObject = () => RatDeRage_Pet6 as IObjectContentGenerator } },
           { "RatHeal_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatHeal_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("RatHeal_Pet1", value, errorInfo),
                GetObject = () => RatHeal_Pet1 as IObjectContentGenerator } },
           { "RatHeal_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatHeal_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("RatHeal_Pet2", value, errorInfo),
                GetObject = () => RatHeal_Pet2 as IObjectContentGenerator } },
           { "RatHeal_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatHeal_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("RatHeal_Pet3", value, errorInfo),
                GetObject = () => RatHeal_Pet3 as IObjectContentGenerator } },
           { "RatHeal_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatHeal_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("RatHeal_Pet4", value, errorInfo),
                GetObject = () => RatHeal_Pet4 as IObjectContentGenerator } },
           { "RatHeal_Pet5", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatHeal_Pet5 = 
                   JsonObjectParser<AIAbility>.Parse("RatHeal_Pet5", value, errorInfo),
                GetObject = () => RatHeal_Pet5 as IObjectContentGenerator } },
           { "RatHeal_Pet6", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatHeal_Pet6 = 
                   JsonObjectParser<AIAbility>.Parse("RatHeal_Pet6", value, errorInfo),
                GetObject = () => RatHeal_Pet6 as IObjectContentGenerator } },
           { "RatVuln_Pet1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatVuln_Pet1 = 
                   JsonObjectParser<AIAbility>.Parse("RatVuln_Pet1", value, errorInfo),
                GetObject = () => RatVuln_Pet1 as IObjectContentGenerator } },
           { "RatVuln_Pet2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatVuln_Pet2 = 
                   JsonObjectParser<AIAbility>.Parse("RatVuln_Pet2", value, errorInfo),
                GetObject = () => RatVuln_Pet2 as IObjectContentGenerator } },
           { "RatVuln_Pet3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatVuln_Pet3 = 
                   JsonObjectParser<AIAbility>.Parse("RatVuln_Pet3", value, errorInfo),
                GetObject = () => RatVuln_Pet3 as IObjectContentGenerator } },
           { "RatVuln_Pet4", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RatVuln_Pet4 = 
                   JsonObjectParser<AIAbility>.Parse("RatVuln_Pet4", value, errorInfo),
                GetObject = () => RatVuln_Pet4 as IObjectContentGenerator } },
           { "ReboundAura1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ReboundAura1 = 
                   JsonObjectParser<AIAbility>.Parse("ReboundAura1", value, errorInfo),
                GetObject = () => ReboundAura1 as IObjectContentGenerator } },
           { "RedCrystalBlast", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RedCrystalBlast = 
                   JsonObjectParser<AIAbility>.Parse("RedCrystalBlast", value, errorInfo),
                GetObject = () => RedCrystalBlast as IObjectContentGenerator } },
           { "RedCrystalBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RedCrystalBurst = 
                   JsonObjectParser<AIAbility>.Parse("RedCrystalBurst", value, errorInfo),
                GetObject = () => RedCrystalBurst as IObjectContentGenerator } },
           { "RhinoBossRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RhinoBossRage = 
                   JsonObjectParser<AIAbility>.Parse("RhinoBossRage", value, errorInfo),
                GetObject = () => RhinoBossRage as IObjectContentGenerator } },
           { "RhinoFireball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RhinoFireball = 
                   JsonObjectParser<AIAbility>.Parse("RhinoFireball", value, errorInfo),
                GetObject = () => RhinoFireball as IObjectContentGenerator } },
           { "RhinoHorn", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RhinoHorn = 
                   JsonObjectParser<AIAbility>.Parse("RhinoHorn", value, errorInfo),
                GetObject = () => RhinoHorn as IObjectContentGenerator } },
           { "RhinoRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => RhinoRage = 
                   JsonObjectParser<AIAbility>.Parse("RhinoRage", value, errorInfo),
                GetObject = () => RhinoRage as IObjectContentGenerator } },
           { "ScrayBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ScrayBite = 
                   JsonObjectParser<AIAbility>.Parse("ScrayBite", value, errorInfo),
                GetObject = () => ScrayBite as IObjectContentGenerator } },
           { "ScrayStab", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ScrayStab = 
                   JsonObjectParser<AIAbility>.Parse("ScrayStab", value, errorInfo),
                GetObject = () => ScrayStab as IObjectContentGenerator } },
           { "SedgewickMegaSwordAngry", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SedgewickMegaSwordAngry = 
                   JsonObjectParser<AIAbility>.Parse("SedgewickMegaSwordAngry", value, errorInfo),
                GetObject = () => SedgewickMegaSwordAngry as IObjectContentGenerator } },
           { "SheepBomb1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SheepBomb1 = 
                   JsonObjectParser<AIAbility>.Parse("SheepBomb1", value, errorInfo),
                GetObject = () => SheepBomb1 as IObjectContentGenerator } },
           { "SherzatAcidSpit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SherzatAcidSpit = 
                   JsonObjectParser<AIAbility>.Parse("SherzatAcidSpit", value, errorInfo),
                GetObject = () => SherzatAcidSpit as IObjectContentGenerator } },
           { "SherzatClaw", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SherzatClaw = 
                   JsonObjectParser<AIAbility>.Parse("SherzatClaw", value, errorInfo),
                GetObject = () => SherzatClaw as IObjectContentGenerator } },
           { "SherzatDisintegrate", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SherzatDisintegrate = 
                   JsonObjectParser<AIAbility>.Parse("SherzatDisintegrate", value, errorInfo),
                GetObject = () => SherzatDisintegrate as IObjectContentGenerator } },
           { "Slime_SummonSlime", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Slime_SummonSlime = 
                   JsonObjectParser<AIAbility>.Parse("Slime_SummonSlime", value, errorInfo),
                GetObject = () => Slime_SummonSlime as IObjectContentGenerator } },
           { "SlimeBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlimeBite = 
                   JsonObjectParser<AIAbility>.Parse("SlimeBite", value, errorInfo),
                GetObject = () => SlimeBite as IObjectContentGenerator } },
           { "SlimeBiteB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlimeBiteB = 
                   JsonObjectParser<AIAbility>.Parse("SlimeBiteB", value, errorInfo),
                GetObject = () => SlimeBiteB as IObjectContentGenerator } },
           { "SlimeKick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlimeKick = 
                   JsonObjectParser<AIAbility>.Parse("SlimeKick", value, errorInfo),
                GetObject = () => SlimeKick as IObjectContentGenerator } },
           { "SlimeKickB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlimeKickB = 
                   JsonObjectParser<AIAbility>.Parse("SlimeKickB", value, errorInfo),
                GetObject = () => SlimeKickB as IObjectContentGenerator } },
           { "SlimeSpit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlimeSpit = 
                   JsonObjectParser<AIAbility>.Parse("SlimeSpit", value, errorInfo),
                GetObject = () => SlimeSpit as IObjectContentGenerator } },
           { "SlimeSuperSpit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlimeSuperSpit = 
                   JsonObjectParser<AIAbility>.Parse("SlimeSuperSpit", value, errorInfo),
                GetObject = () => SlimeSuperSpit as IObjectContentGenerator } },
           { "SlugPoisonBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlugPoisonBite = 
                   JsonObjectParser<AIAbility>.Parse("SlugPoisonBite", value, errorInfo),
                GetObject = () => SlugPoisonBite as IObjectContentGenerator } },
           { "SlugPoisonBite2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlugPoisonBite2 = 
                   JsonObjectParser<AIAbility>.Parse("SlugPoisonBite2", value, errorInfo),
                GetObject = () => SlugPoisonBite2 as IObjectContentGenerator } },
           { "SlugPoisonBite3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlugPoisonBite3 = 
                   JsonObjectParser<AIAbility>.Parse("SlugPoisonBite3", value, errorInfo),
                GetObject = () => SlugPoisonBite3 as IObjectContentGenerator } },
           { "SlugPoisonRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlugPoisonRage = 
                   JsonObjectParser<AIAbility>.Parse("SlugPoisonRage", value, errorInfo),
                GetObject = () => SlugPoisonRage as IObjectContentGenerator } },
           { "SlugPoisonRage2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlugPoisonRage2 = 
                   JsonObjectParser<AIAbility>.Parse("SlugPoisonRage2", value, errorInfo),
                GetObject = () => SlugPoisonRage2 as IObjectContentGenerator } },
           { "SlugPoisonRage3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SlugPoisonRage3 = 
                   JsonObjectParser<AIAbility>.Parse("SlugPoisonRage3", value, errorInfo),
                GetObject = () => SlugPoisonRage3 as IObjectContentGenerator } },
           { "SnailRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SnailRage = 
                   JsonObjectParser<AIAbility>.Parse("SnailRage", value, errorInfo),
                GetObject = () => SnailRage as IObjectContentGenerator } },
           { "SnailRageB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SnailRageB = 
                   JsonObjectParser<AIAbility>.Parse("SnailRageB", value, errorInfo),
                GetObject = () => SnailRageB as IObjectContentGenerator } },
           { "SnailRageC", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SnailRageC = 
                   JsonObjectParser<AIAbility>.Parse("SnailRageC", value, errorInfo),
                GetObject = () => SnailRageC as IObjectContentGenerator } },
           { "SnailStrike", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SnailStrike = 
                   JsonObjectParser<AIAbility>.Parse("SnailStrike", value, errorInfo),
                GetObject = () => SnailStrike as IObjectContentGenerator } },
           { "SpiderBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderBite = 
                   JsonObjectParser<AIAbility>.Parse("SpiderBite", value, errorInfo),
                GetObject = () => SpiderBite as IObjectContentGenerator } },
           { "SpiderBossFreePin", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderBossFreePin = 
                   JsonObjectParser<AIAbility>.Parse("SpiderBossFreePin", value, errorInfo),
                GetObject = () => SpiderBossFreePin as IObjectContentGenerator } },
           { "SpiderFireball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderFireball = 
                   JsonObjectParser<AIAbility>.Parse("SpiderFireball", value, errorInfo),
                GetObject = () => SpiderFireball as IObjectContentGenerator } },
           { "SpiderIncubate", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderIncubate = 
                   JsonObjectParser<AIAbility>.Parse("SpiderIncubate", value, errorInfo),
                GetObject = () => SpiderIncubate as IObjectContentGenerator } },
           { "SpiderInject", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderInject = 
                   JsonObjectParser<AIAbility>.Parse("SpiderInject", value, errorInfo),
                GetObject = () => SpiderInject as IObjectContentGenerator } },
           { "SpiderKill", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderKill = 
                   JsonObjectParser<AIAbility>.Parse("SpiderKill", value, errorInfo),
                GetObject = () => SpiderKill as IObjectContentGenerator } },
           { "SpiderKill2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderKill2 = 
                   JsonObjectParser<AIAbility>.Parse("SpiderKill2", value, errorInfo),
                GetObject = () => SpiderKill2 as IObjectContentGenerator } },
           { "SpiderKill3", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderKill3 = 
                   JsonObjectParser<AIAbility>.Parse("SpiderKill3", value, errorInfo),
                GetObject = () => SpiderKill3 as IObjectContentGenerator } },
           { "SpiderPin", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpiderPin = 
                   JsonObjectParser<AIAbility>.Parse("SpiderPin", value, errorInfo),
                GetObject = () => SpiderPin as IObjectContentGenerator } },
           { "SpyPortalZap", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpyPortalZap = 
                   JsonObjectParser<AIAbility>.Parse("SpyPortalZap", value, errorInfo),
                GetObject = () => SpyPortalZap as IObjectContentGenerator } },
           { "SpyPortalZap2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => SpyPortalZap2 = 
                   JsonObjectParser<AIAbility>.Parse("SpyPortalZap2", value, errorInfo),
                GetObject = () => SpyPortalZap2 as IObjectContentGenerator } },
           { "StrigaBuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => StrigaBuff = 
                   JsonObjectParser<AIAbility>.Parse("StrigaBuff", value, errorInfo),
                GetObject = () => StrigaBuff as IObjectContentGenerator } },
           { "StrigaClawA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => StrigaClawA = 
                   JsonObjectParser<AIAbility>.Parse("StrigaClawA", value, errorInfo),
                GetObject = () => StrigaClawA as IObjectContentGenerator } },
           { "StrigaClawB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => StrigaClawB = 
                   JsonObjectParser<AIAbility>.Parse("StrigaClawB", value, errorInfo),
                GetObject = () => StrigaClawB as IObjectContentGenerator } },
           { "StrigaFireBreath", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => StrigaFireBreath = 
                   JsonObjectParser<AIAbility>.Parse("StrigaFireBreath", value, errorInfo),
                GetObject = () => StrigaFireBreath as IObjectContentGenerator } },
           { "StrigaReap", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => StrigaReap = 
                   JsonObjectParser<AIAbility>.Parse("StrigaReap", value, errorInfo),
                GetObject = () => StrigaReap as IObjectContentGenerator } },
           { "StrigaReap2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => StrigaReap2 = 
                   JsonObjectParser<AIAbility>.Parse("StrigaReap2", value, errorInfo),
                GetObject = () => StrigaReap2 as IObjectContentGenerator } },
           { "TheFogCurse", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TheFogCurse = 
                   JsonObjectParser<AIAbility>.Parse("TheFogCurse", value, errorInfo),
                GetObject = () => TheFogCurse as IObjectContentGenerator } },
           { "TornadoFling", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TornadoFling = 
                   JsonObjectParser<AIAbility>.Parse("TornadoFling", value, errorInfo),
                GetObject = () => TornadoFling as IObjectContentGenerator } },
           { "TornadoJolt1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TornadoJolt1 = 
                   JsonObjectParser<AIAbility>.Parse("TornadoJolt1", value, errorInfo),
                GetObject = () => TornadoJolt1 as IObjectContentGenerator } },
           { "TornadoToss", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TornadoToss = 
                   JsonObjectParser<AIAbility>.Parse("TornadoToss", value, errorInfo),
                GetObject = () => TornadoToss as IObjectContentGenerator } },
           { "TotalHorrorAttack", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TotalHorrorAttack = 
                   JsonObjectParser<AIAbility>.Parse("TotalHorrorAttack", value, errorInfo),
                GetObject = () => TotalHorrorAttack as IObjectContentGenerator } },
           { "TotalHorrorHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TotalHorrorHeal = 
                   JsonObjectParser<AIAbility>.Parse("TotalHorrorHeal", value, errorInfo),
                GetObject = () => TotalHorrorHeal as IObjectContentGenerator } },
           { "TotalHorrorHeal2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TotalHorrorHeal2 = 
                   JsonObjectParser<AIAbility>.Parse("TotalHorrorHeal2", value, errorInfo),
                GetObject = () => TotalHorrorHeal2 as IObjectContentGenerator } },
           { "TotalHorrorStretch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TotalHorrorStretch = 
                   JsonObjectParser<AIAbility>.Parse("TotalHorrorStretch", value, errorInfo),
                GetObject = () => TotalHorrorStretch as IObjectContentGenerator } },
           { "TrainingGolemFireBreath", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrainingGolemFireBreath = 
                   JsonObjectParser<AIAbility>.Parse("TrainingGolemFireBreath", value, errorInfo),
                GetObject = () => TrainingGolemFireBreath as IObjectContentGenerator } },
           { "TrainingGolemFireBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrainingGolemFireBurst = 
                   JsonObjectParser<AIAbility>.Parse("TrainingGolemFireBurst", value, errorInfo),
                GetObject = () => TrainingGolemFireBurst as IObjectContentGenerator } },
           { "TrainingGolemHeal", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrainingGolemHeal = 
                   JsonObjectParser<AIAbility>.Parse("TrainingGolemHeal", value, errorInfo),
                GetObject = () => TrainingGolemHeal as IObjectContentGenerator } },
           { "TrainingGolemHealB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrainingGolemHealB = 
                   JsonObjectParser<AIAbility>.Parse("TrainingGolemHealB", value, errorInfo),
                GetObject = () => TrainingGolemHealB as IObjectContentGenerator } },
           { "TrainingGolemPunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrainingGolemPunch = 
                   JsonObjectParser<AIAbility>.Parse("TrainingGolemPunch", value, errorInfo),
                GetObject = () => TrainingGolemPunch as IObjectContentGenerator } },
           { "TrainingGolemStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrainingGolemStun = 
                   JsonObjectParser<AIAbility>.Parse("TrainingGolemStun", value, errorInfo),
                GetObject = () => TrainingGolemStun as IObjectContentGenerator } },
           { "TriffidClawA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TriffidClawA = 
                   JsonObjectParser<AIAbility>.Parse("TriffidClawA", value, errorInfo),
                GetObject = () => TriffidClawA as IObjectContentGenerator } },
           { "TriffidClawB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TriffidClawB = 
                   JsonObjectParser<AIAbility>.Parse("TriffidClawB", value, errorInfo),
                GetObject = () => TriffidClawB as IObjectContentGenerator } },
           { "TriffidShot", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TriffidShot = 
                   JsonObjectParser<AIAbility>.Parse("TriffidShot", value, errorInfo),
                GetObject = () => TriffidShot as IObjectContentGenerator } },
           { "TriffidSpore", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TriffidSpore = 
                   JsonObjectParser<AIAbility>.Parse("TriffidSpore", value, errorInfo),
                GetObject = () => TriffidSpore as IObjectContentGenerator } },
           { "TriffidTongue", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TriffidTongue = 
                   JsonObjectParser<AIAbility>.Parse("TriffidTongue", value, errorInfo),
                GetObject = () => TriffidTongue as IObjectContentGenerator } },
           { "TriffidTongueElite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TriffidTongueElite = 
                   JsonObjectParser<AIAbility>.Parse("TriffidTongueElite", value, errorInfo),
                GetObject = () => TriffidTongueElite as IObjectContentGenerator } },
           { "TrollClubA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrollClubA = 
                   JsonObjectParser<AIAbility>.Parse("TrollClubA", value, errorInfo),
                GetObject = () => TrollClubA as IObjectContentGenerator } },
           { "TrollClubB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrollClubB = 
                   JsonObjectParser<AIAbility>.Parse("TrollClubB", value, errorInfo),
                GetObject = () => TrollClubB as IObjectContentGenerator } },
           { "TrollKnockdown", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TrollKnockdown = 
                   JsonObjectParser<AIAbility>.Parse("TrollKnockdown", value, errorInfo),
                GetObject = () => TrollKnockdown as IObjectContentGenerator } },
           { "TurretCrystalZap", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TurretCrystalZap = 
                   JsonObjectParser<AIAbility>.Parse("TurretCrystalZap", value, errorInfo),
                GetObject = () => TurretCrystalZap as IObjectContentGenerator } },
           { "TurretCrystalZap2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TurretCrystalZap2 = 
                   JsonObjectParser<AIAbility>.Parse("TurretCrystalZap2", value, errorInfo),
                GetObject = () => TurretCrystalZap2 as IObjectContentGenerator } },
           { "TurretCrystalZapLongRange", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TurretCrystalZapLongRange = 
                   JsonObjectParser<AIAbility>.Parse("TurretCrystalZapLongRange", value, errorInfo),
                GetObject = () => TurretCrystalZapLongRange as IObjectContentGenerator } },
           { "TurretCrystalZapLongRange2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => TurretCrystalZapLongRange2 = 
                   JsonObjectParser<AIAbility>.Parse("TurretCrystalZapLongRange2", value, errorInfo),
                GetObject = () => TurretCrystalZapLongRange2 as IObjectContentGenerator } },
           { "UndeadArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadArrow1", value, errorInfo),
                GetObject = () => UndeadArrow1 as IObjectContentGenerator } },
           { "UndeadArrow2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadArrow2 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadArrow2", value, errorInfo),
                GetObject = () => UndeadArrow2 as IObjectContentGenerator } },
           { "UndeadBoneWhirlwind", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadBoneWhirlwind = 
                   JsonObjectParser<AIAbility>.Parse("UndeadBoneWhirlwind", value, errorInfo),
                GetObject = () => UndeadBoneWhirlwind as IObjectContentGenerator } },
           { "UndeadDarknessBall", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadDarknessBall = 
                   JsonObjectParser<AIAbility>.Parse("UndeadDarknessBall", value, errorInfo),
                GetObject = () => UndeadDarknessBall as IObjectContentGenerator } },
           { "UndeadFireballA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFireballA = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFireballA", value, errorInfo),
                GetObject = () => UndeadFireballA as IObjectContentGenerator } },
           { "UndeadFireballA2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFireballA2 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFireballA2", value, errorInfo),
                GetObject = () => UndeadFireballA2 as IObjectContentGenerator } },
           { "UndeadFireballB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFireballB = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFireballB", value, errorInfo),
                GetObject = () => UndeadFireballB as IObjectContentGenerator } },
           { "UndeadFireballB2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFireballB2 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFireballB2", value, errorInfo),
                GetObject = () => UndeadFireballB2 as IObjectContentGenerator } },
           { "UndeadFireballLongA", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFireballLongA = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFireballLongA", value, errorInfo),
                GetObject = () => UndeadFireballLongA as IObjectContentGenerator } },
           { "UndeadFireballLongB", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFireballLongB = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFireballLongB", value, errorInfo),
                GetObject = () => UndeadFireballLongB as IObjectContentGenerator } },
           { "UndeadFireballLongB2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFireballLongB2 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFireballLongB2", value, errorInfo),
                GetObject = () => UndeadFireballLongB2 as IObjectContentGenerator } },
           { "UndeadFreezeBall", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadFreezeBall = 
                   JsonObjectParser<AIAbility>.Parse("UndeadFreezeBall", value, errorInfo),
                GetObject = () => UndeadFreezeBall as IObjectContentGenerator } },
           { "UndeadGrappleArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadGrappleArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadGrappleArrow1", value, errorInfo),
                GetObject = () => UndeadGrappleArrow1 as IObjectContentGenerator } },
           { "UndeadIceBall1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadIceBall1 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadIceBall1", value, errorInfo),
                GetObject = () => UndeadIceBall1 as IObjectContentGenerator } },
           { "UndeadLightningSmite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadLightningSmite = 
                   JsonObjectParser<AIAbility>.Parse("UndeadLightningSmite", value, errorInfo),
                GetObject = () => UndeadLightningSmite as IObjectContentGenerator } },
           { "UndeadMegaSword1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadMegaSword1 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadMegaSword1", value, errorInfo),
                GetObject = () => UndeadMegaSword1 as IObjectContentGenerator } },
           { "UndeadMegaSword2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadMegaSword2 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadMegaSword2", value, errorInfo),
                GetObject = () => UndeadMegaSword2 as IObjectContentGenerator } },
           { "UndeadMegaSwordAngry", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadMegaSwordAngry = 
                   JsonObjectParser<AIAbility>.Parse("UndeadMegaSwordAngry", value, errorInfo),
                GetObject = () => UndeadMegaSwordAngry as IObjectContentGenerator } },
           { "UndeadOmegaArrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadOmegaArrow = 
                   JsonObjectParser<AIAbility>.Parse("UndeadOmegaArrow", value, errorInfo),
                GetObject = () => UndeadOmegaArrow as IObjectContentGenerator } },
           { "UndeadPhysicalShield", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadPhysicalShield = 
                   JsonObjectParser<AIAbility>.Parse("UndeadPhysicalShield", value, errorInfo),
                GetObject = () => UndeadPhysicalShield as IObjectContentGenerator } },
           { "UndeadSelfDestruct", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadSelfDestruct = 
                   JsonObjectParser<AIAbility>.Parse("UndeadSelfDestruct", value, errorInfo),
                GetObject = () => UndeadSelfDestruct as IObjectContentGenerator } },
           { "UndeadSword1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadSword1 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadSword1", value, errorInfo),
                GetObject = () => UndeadSword1 as IObjectContentGenerator } },
           { "UndeadSword1B", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadSword1B = 
                   JsonObjectParser<AIAbility>.Parse("UndeadSword1B", value, errorInfo),
                GetObject = () => UndeadSword1B as IObjectContentGenerator } },
           { "UndeadSword2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadSword2 = 
                   JsonObjectParser<AIAbility>.Parse("UndeadSword2", value, errorInfo),
                GetObject = () => UndeadSword2 as IObjectContentGenerator } },
           { "UndeadSwordAngry", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UndeadSwordAngry = 
                   JsonObjectParser<AIAbility>.Parse("UndeadSwordAngry", value, errorInfo),
                GetObject = () => UndeadSwordAngry as IObjectContentGenerator } },
           { "UrsulaFireball1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaFireball1 = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaFireball1", value, errorInfo),
                GetObject = () => UrsulaFireball1 as IObjectContentGenerator } },
           { "UrsulaFireball1B", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaFireball1B = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaFireball1B", value, errorInfo),
                GetObject = () => UrsulaFireball1B as IObjectContentGenerator } },
           { "UrsulaFireball2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaFireball2 = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaFireball2", value, errorInfo),
                GetObject = () => UrsulaFireball2 as IObjectContentGenerator } },
           { "UrsulaIceball1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaIceball1 = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaIceball1", value, errorInfo),
                GetObject = () => UrsulaIceball1 as IObjectContentGenerator } },
           { "UrsulaIceball1B", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaIceball1B = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaIceball1B", value, errorInfo),
                GetObject = () => UrsulaIceball1B as IObjectContentGenerator } },
           { "UrsulaIceball2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaIceball2 = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaIceball2", value, errorInfo),
                GetObject = () => UrsulaIceball2 as IObjectContentGenerator } },
           { "UrsulaRage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaRage = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaRage", value, errorInfo),
                GetObject = () => UrsulaRage as IObjectContentGenerator } },
           { "UrsulaSummon", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => UrsulaSummon = 
                   JsonObjectParser<AIAbility>.Parse("UrsulaSummon", value, errorInfo),
                GetObject = () => UrsulaSummon as IObjectContentGenerator } },
           { "WatcherAcidball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WatcherAcidball = 
                   JsonObjectParser<AIAbility>.Parse("WatcherAcidball", value, errorInfo),
                GetObject = () => WatcherAcidball as IObjectContentGenerator } },
           { "WatcherFireball", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WatcherFireball = 
                   JsonObjectParser<AIAbility>.Parse("WatcherFireball", value, errorInfo),
                GetObject = () => WatcherFireball as IObjectContentGenerator } },
           { "WatcherSlap", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WatcherSlap = 
                   JsonObjectParser<AIAbility>.Parse("WatcherSlap", value, errorInfo),
                GetObject = () => WatcherSlap as IObjectContentGenerator } },
           { "WebStick", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WebStick = 
                   JsonObjectParser<AIAbility>.Parse("WebStick", value, errorInfo),
                GetObject = () => WebStick as IObjectContentGenerator } },
           { "Werewolf_Summon_Opener", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Werewolf_Summon_Opener = 
                   JsonObjectParser<AIAbility>.Parse("Werewolf_Summon_Opener", value, errorInfo),
                GetObject = () => Werewolf_Summon_Opener as IObjectContentGenerator } },
           { "Werewolf_Summon_Rage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Werewolf_Summon_Rage = 
                   JsonObjectParser<AIAbility>.Parse("Werewolf_Summon_Rage", value, errorInfo),
                GetObject = () => Werewolf_Summon_Rage as IObjectContentGenerator } },
           { "WerewolfArrow1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WerewolfArrow1 = 
                   JsonObjectParser<AIAbility>.Parse("WerewolfArrow1", value, errorInfo),
                GetObject = () => WerewolfArrow1 as IObjectContentGenerator } },
           { "WerewolfArrow2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WerewolfArrow2 = 
                   JsonObjectParser<AIAbility>.Parse("WerewolfArrow2", value, errorInfo),
                GetObject = () => WerewolfArrow2 as IObjectContentGenerator } },
           { "WerewolfOmegaArrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WerewolfOmegaArrow = 
                   JsonObjectParser<AIAbility>.Parse("WerewolfOmegaArrow", value, errorInfo),
                GetObject = () => WerewolfOmegaArrow as IObjectContentGenerator } },
           { "WerewolfSword1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WerewolfSword1 = 
                   JsonObjectParser<AIAbility>.Parse("WerewolfSword1", value, errorInfo),
                GetObject = () => WerewolfSword1 as IObjectContentGenerator } },
           { "WerewolfSword2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WerewolfSword2 = 
                   JsonObjectParser<AIAbility>.Parse("WerewolfSword2", value, errorInfo),
                GetObject = () => WerewolfSword2 as IObjectContentGenerator } },
           { "WerewolfSwordStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WerewolfSwordStun = 
                   JsonObjectParser<AIAbility>.Parse("WerewolfSwordStun", value, errorInfo),
                GetObject = () => WerewolfSwordStun as IObjectContentGenerator } },
           { "WorgBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WorgBite = 
                   JsonObjectParser<AIAbility>.Parse("WorgBite", value, errorInfo),
                GetObject = () => WorgBite as IObjectContentGenerator } },
           { "WorghestDebuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WorghestDebuff = 
                   JsonObjectParser<AIAbility>.Parse("WorghestDebuff", value, errorInfo),
                GetObject = () => WorghestDebuff as IObjectContentGenerator } },
           { "WorgOmegaBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WorgOmegaBite = 
                   JsonObjectParser<AIAbility>.Parse("WorgOmegaBite", value, errorInfo),
                GetObject = () => WorgOmegaBite as IObjectContentGenerator } },
           { "WormBite1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WormBite1 = 
                   JsonObjectParser<AIAbility>.Parse("WormBite1", value, errorInfo),
                GetObject = () => WormBite1 as IObjectContentGenerator } },
           { "WormBossAcidBurst", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WormBossAcidBurst = 
                   JsonObjectParser<AIAbility>.Parse("WormBossAcidBurst", value, errorInfo),
                GetObject = () => WormBossAcidBurst as IObjectContentGenerator } },
           { "WormBossSpit", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WormBossSpit = 
                   JsonObjectParser<AIAbility>.Parse("WormBossSpit", value, errorInfo),
                GetObject = () => WormBossSpit as IObjectContentGenerator } },
           { "WormInfect1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WormInfect1 = 
                   JsonObjectParser<AIAbility>.Parse("WormInfect1", value, errorInfo),
                GetObject = () => WormInfect1 as IObjectContentGenerator } },
           { "WormShove1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WormShove1 = 
                   JsonObjectParser<AIAbility>.Parse("WormShove1", value, errorInfo),
                GetObject = () => WormShove1 as IObjectContentGenerator } },
           { "WormSpit1", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WormSpit1 = 
                   JsonObjectParser<AIAbility>.Parse("WormSpit1", value, errorInfo),
                GetObject = () => WormSpit1 as IObjectContentGenerator } },
           { "WormSpit2", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => WormSpit2 = 
                   JsonObjectParser<AIAbility>.Parse("WormSpit2", value, errorInfo),
                GetObject = () => WormSpit2 as IObjectContentGenerator } },
           { "YetiBarrage", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiBarrage = 
                   JsonObjectParser<AIAbility>.Parse("YetiBarrage", value, errorInfo),
                GetObject = () => YetiBarrage as IObjectContentGenerator } },
           { "YetiBoulderThrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiBoulderThrow = 
                   JsonObjectParser<AIAbility>.Parse("YetiBoulderThrow", value, errorInfo),
                GetObject = () => YetiBoulderThrow as IObjectContentGenerator } },
           { "YetiColdOrb", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiColdOrb = 
                   JsonObjectParser<AIAbility>.Parse("YetiColdOrb", value, errorInfo),
                GetObject = () => YetiColdOrb as IObjectContentGenerator } },
           { "YetiDebuff", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiDebuff = 
                   JsonObjectParser<AIAbility>.Parse("YetiDebuff", value, errorInfo),
                GetObject = () => YetiDebuff as IObjectContentGenerator } },
           { "YetiEncase", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiEncase = 
                   JsonObjectParser<AIAbility>.Parse("YetiEncase", value, errorInfo),
                GetObject = () => YetiEncase as IObjectContentGenerator } },
           { "YetiFlingAway", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiFlingAway = 
                   JsonObjectParser<AIAbility>.Parse("YetiFlingAway", value, errorInfo),
                GetObject = () => YetiFlingAway as IObjectContentGenerator } },
           { "YetiFrostRing", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiFrostRing = 
                   JsonObjectParser<AIAbility>.Parse("YetiFrostRing", value, errorInfo),
                GetObject = () => YetiFrostRing as IObjectContentGenerator } },
           { "YetiIceBallThrow", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiIceBallThrow = 
                   JsonObjectParser<AIAbility>.Parse("YetiIceBallThrow", value, errorInfo),
                GetObject = () => YetiIceBallThrow as IObjectContentGenerator } },
           { "YetiIceSpear", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiIceSpear = 
                   JsonObjectParser<AIAbility>.Parse("YetiIceSpear", value, errorInfo),
                GetObject = () => YetiIceSpear as IObjectContentGenerator } },
           { "YetiPunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiPunch = 
                   JsonObjectParser<AIAbility>.Parse("YetiPunch", value, errorInfo),
                GetObject = () => YetiPunch as IObjectContentGenerator } },
           { "YetiRoarStun", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => YetiRoarStun = 
                   JsonObjectParser<AIAbility>.Parse("YetiRoarStun", value, errorInfo),
                GetObject = () => YetiRoarStun as IObjectContentGenerator } },
           { "ZombieBite", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ZombieBite = 
                   JsonObjectParser<AIAbility>.Parse("ZombieBite", value, errorInfo),
                GetObject = () => ZombieBite as IObjectContentGenerator } },
           { "ZombiePunch", new FieldParser() {
               Type = FieldType.Object, ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => ZombiePunch = 
                   JsonObjectParser<AIAbility>.Parse("ZombiePunch", value, errorInfo),
                GetObject = () => ZombiePunch as IObjectContentGenerator } },
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

            AddObject(AnimalBite as ISerializableJsonObject, data, ref offset, BaseOffset, 0 * 4, StoredObjectTable);
            AddObject(AnimalClaw as ISerializableJsonObject, data, ref offset, BaseOffset, 1 * 4, StoredObjectTable);
            AddObject(AnimalOmegaBite as ISerializableJsonObject, data, ref offset, BaseOffset, 2 * 4, StoredObjectTable);
            AddObject(AnimalOmegaBite2 as ISerializableJsonObject, data, ref offset, BaseOffset, 3 * 4, StoredObjectTable);
            AddObject(AnimalHoofFrontKick as ISerializableJsonObject, data, ref offset, BaseOffset, 4 * 4, StoredObjectTable);
            AddObject(AnimalHoofRageKick as ISerializableJsonObject, data, ref offset, BaseOffset, 5 * 4, StoredObjectTable);
            AddObject(AnimalHoofRageKick2 as ISerializableJsonObject, data, ref offset, BaseOffset, 6 * 4, StoredObjectTable);
            AddObject(AnimalHoofFieryFrontKick as ISerializableJsonObject, data, ref offset, BaseOffset, 7 * 4, StoredObjectTable);
            AddObject(AnimalHoofFieryFrontKick2 as ISerializableJsonObject, data, ref offset, BaseOffset, 8 * 4, StoredObjectTable);
            AddObject(ElectricPigHitAndRun as ISerializableJsonObject, data, ref offset, BaseOffset, 9 * 4, StoredObjectTable);
            AddObject(ElectricPigStun as ISerializableJsonObject, data, ref offset, BaseOffset, 10 * 4, StoredObjectTable);
            AddObject(ElectricPigAoEStun as ISerializableJsonObject, data, ref offset, BaseOffset, 11 * 4, StoredObjectTable);
            AddObject(LamiaMindControl as ISerializableJsonObject, data, ref offset, BaseOffset, 12 * 4, StoredObjectTable);
            AddObject(LamiaRage as ISerializableJsonObject, data, ref offset, BaseOffset, 13 * 4, StoredObjectTable);
            AddObject(SlimeKick as ISerializableJsonObject, data, ref offset, BaseOffset, 14 * 4, StoredObjectTable);
            AddObject(SlimeKickB as ISerializableJsonObject, data, ref offset, BaseOffset, 15 * 4, StoredObjectTable);
            AddObject(SlimeBite as ISerializableJsonObject, data, ref offset, BaseOffset, 16 * 4, StoredObjectTable);
            AddObject(SlimeBiteB as ISerializableJsonObject, data, ref offset, BaseOffset, 17 * 4, StoredObjectTable);
            AddObject(Slime_SummonSlime as ISerializableJsonObject, data, ref offset, BaseOffset, 18 * 4, StoredObjectTable);
            AddObject(SlimeSpit as ISerializableJsonObject, data, ref offset, BaseOffset, 19 * 4, StoredObjectTable);
            AddObject(SlimeSuperSpit as ISerializableJsonObject, data, ref offset, BaseOffset, 20 * 4, StoredObjectTable);
            AddObject(IceSlimeKick as ISerializableJsonObject, data, ref offset, BaseOffset, 21 * 4, StoredObjectTable);
            AddObject(IceSlimeKickB as ISerializableJsonObject, data, ref offset, BaseOffset, 22 * 4, StoredObjectTable);
            AddObject(IceSlimeBite as ISerializableJsonObject, data, ref offset, BaseOffset, 23 * 4, StoredObjectTable);
            AddObject(IceSlimeBiteB as ISerializableJsonObject, data, ref offset, BaseOffset, 24 * 4, StoredObjectTable);
            AddObject(BossSlimeKick as ISerializableJsonObject, data, ref offset, BaseOffset, 25 * 4, StoredObjectTable);
            AddObject(BossSlimeKickB as ISerializableJsonObject, data, ref offset, BaseOffset, 26 * 4, StoredObjectTable);
            AddObject(BossSlime_SummonSlime1 as ISerializableJsonObject, data, ref offset, BaseOffset, 27 * 4, StoredObjectTable);
            AddObject(BossSlimeKick2 as ISerializableJsonObject, data, ref offset, BaseOffset, 28 * 4, StoredObjectTable);
            AddObject(BossSlimeKick2B as ISerializableJsonObject, data, ref offset, BaseOffset, 29 * 4, StoredObjectTable);
            AddObject(BossSlime_SummonSlime4Elite as ISerializableJsonObject, data, ref offset, BaseOffset, 30 * 4, StoredObjectTable);
            AddObject(AnimalHeal as ISerializableJsonObject, data, ref offset, BaseOffset, 31 * 4, StoredObjectTable);
            AddObject(AnimalHeal2 as ISerializableJsonObject, data, ref offset, BaseOffset, 32 * 4, StoredObjectTable);
            AddObject(AnimalHeal3 as ISerializableJsonObject, data, ref offset, BaseOffset, 33 * 4, StoredObjectTable);
            AddObject(IceCockPeck as ISerializableJsonObject, data, ref offset, BaseOffset, 34 * 4, StoredObjectTable);
            AddObject(IceCockFreeze as ISerializableJsonObject, data, ref offset, BaseOffset, 35 * 4, StoredObjectTable);
            AddObject(NightmareHoof as ISerializableJsonObject, data, ref offset, BaseOffset, 36 * 4, StoredObjectTable);
            AddObject(NightmareDarknessBomb as ISerializableJsonObject, data, ref offset, BaseOffset, 37 * 4, StoredObjectTable);
            AddObject(CiervosNightmareHoof as ISerializableJsonObject, data, ref offset, BaseOffset, 38 * 4, StoredObjectTable);
            AddObject(CiervosDarknessBomb as ISerializableJsonObject, data, ref offset, BaseOffset, 39 * 4, StoredObjectTable);
            AddObject(Myconian_Bash as ISerializableJsonObject, data, ref offset, BaseOffset, 40 * 4, StoredObjectTable);
            AddObject(Myconian_Mindspores as ISerializableJsonObject, data, ref offset, BaseOffset, 41 * 4, StoredObjectTable);
            AddObject(Myconian_Drain as ISerializableJsonObject, data, ref offset, BaseOffset, 42 * 4, StoredObjectTable);
            AddObject(Myconian_BossBash as ISerializableJsonObject, data, ref offset, BaseOffset, 43 * 4, StoredObjectTable);
            AddObject(Myconian_Mindspores_Permanent as ISerializableJsonObject, data, ref offset, BaseOffset, 44 * 4, StoredObjectTable);
            AddObject(Myconian_TidalCurse as ISerializableJsonObject, data, ref offset, BaseOffset, 45 * 4, StoredObjectTable);
            AddObject(Myconian_Shock as ISerializableJsonObject, data, ref offset, BaseOffset, 46 * 4, StoredObjectTable);
            AddObject(AlienDog_Punch as ISerializableJsonObject, data, ref offset, BaseOffset, 47 * 4, StoredObjectTable);
            AddObject(AlienDog_Punch2 as ISerializableJsonObject, data, ref offset, BaseOffset, 48 * 4, StoredObjectTable);
            AddObject(AlienDog_RagePunch as ISerializableJsonObject, data, ref offset, BaseOffset, 49 * 4, StoredObjectTable);
            AddObject(MushroomMonster_Bite as ISerializableJsonObject, data, ref offset, BaseOffset, 50 * 4, StoredObjectTable);
            AddObject(MushroomMonster_Spit1 as ISerializableJsonObject, data, ref offset, BaseOffset, 51 * 4, StoredObjectTable);
            AddObject(MushroomMonster_Spit2 as ISerializableJsonObject, data, ref offset, BaseOffset, 52 * 4, StoredObjectTable);
            AddObject(MushroomMonster_SummonMushroomSpawn1 as ISerializableJsonObject, data, ref offset, BaseOffset, 53 * 4, StoredObjectTable);
            AddObject(MushroomMonster_SummonMushroomSpawn2 as ISerializableJsonObject, data, ref offset, BaseOffset, 54 * 4, StoredObjectTable);
            AddObject(MushroomMonster_SummonMushroomSpawn3 as ISerializableJsonObject, data, ref offset, BaseOffset, 55 * 4, StoredObjectTable);
            AddObject(MushroomMonster_SpawnSpit1 as ISerializableJsonObject, data, ref offset, BaseOffset, 56 * 4, StoredObjectTable);
            AddObject(MushroomMonster_SpawnSpit2 as ISerializableJsonObject, data, ref offset, BaseOffset, 57 * 4, StoredObjectTable);
            AddObject(MushroomMonster_SpawnSuperSpit1 as ISerializableJsonObject, data, ref offset, BaseOffset, 58 * 4, StoredObjectTable);
            AddObject(MushroomMonster_SpawnSuperSpit2 as ISerializableJsonObject, data, ref offset, BaseOffset, 59 * 4, StoredObjectTable);
            AddObject(MaronesaStomp as ISerializableJsonObject, data, ref offset, BaseOffset, 60 * 4, StoredObjectTable);
            AddObject(MaronesaInfect as ISerializableJsonObject, data, ref offset, BaseOffset, 61 * 4, StoredObjectTable);
            AddObject(WormBite1 as ISerializableJsonObject, data, ref offset, BaseOffset, 62 * 4, StoredObjectTable);
            AddObject(WormSpit1 as ISerializableJsonObject, data, ref offset, BaseOffset, 63 * 4, StoredObjectTable);
            AddObject(WormShove1 as ISerializableJsonObject, data, ref offset, BaseOffset, 64 * 4, StoredObjectTable);
            AddObject(WormInfect1 as ISerializableJsonObject, data, ref offset, BaseOffset, 65 * 4, StoredObjectTable);
            AddObject(WormSpit2 as ISerializableJsonObject, data, ref offset, BaseOffset, 66 * 4, StoredObjectTable);
            AddObject(WormBossSpit as ISerializableJsonObject, data, ref offset, BaseOffset, 67 * 4, StoredObjectTable);
            AddObject(WormBossAcidBurst as ISerializableJsonObject, data, ref offset, BaseOffset, 68 * 4, StoredObjectTable);
            AddObject(GnasherBite as ISerializableJsonObject, data, ref offset, BaseOffset, 69 * 4, StoredObjectTable);
            AddObject(GnasherRend as ISerializableJsonObject, data, ref offset, BaseOffset, 70 * 4, StoredObjectTable);
            AddObject(Dinoslash as ISerializableJsonObject, data, ref offset, BaseOffset, 71 * 4, StoredObjectTable);
            AddObject(Dinoslash2 as ISerializableJsonObject, data, ref offset, BaseOffset, 72 * 4, StoredObjectTable);
            AddObject(Dinobite as ISerializableJsonObject, data, ref offset, BaseOffset, 73 * 4, StoredObjectTable);
            AddObject(Dinobite2 as ISerializableJsonObject, data, ref offset, BaseOffset, 74 * 4, StoredObjectTable);
            AddObject(Dinowhap as ISerializableJsonObject, data, ref offset, BaseOffset, 75 * 4, StoredObjectTable);
            AddObject(Peck as ISerializableJsonObject, data, ref offset, BaseOffset, 76 * 4, StoredObjectTable);
            AddObject(AnimalFlee as ISerializableJsonObject, data, ref offset, BaseOffset, 77 * 4, StoredObjectTable);
            AddObject(HippogriffPeck as ISerializableJsonObject, data, ref offset, BaseOffset, 78 * 4, StoredObjectTable);
            AddObject(HippogriffSlashes as ISerializableJsonObject, data, ref offset, BaseOffset, 79 * 4, StoredObjectTable);
            AddObject(HippogriffBossSlashes as ISerializableJsonObject, data, ref offset, BaseOffset, 80 * 4, StoredObjectTable);
            AddObject(RatBite as ISerializableJsonObject, data, ref offset, BaseOffset, 81 * 4, StoredObjectTable);
            AddObject(RatClaw as ISerializableJsonObject, data, ref offset, BaseOffset, 82 * 4, StoredObjectTable);
            AddObject(FireRatBite as ISerializableJsonObject, data, ref offset, BaseOffset, 83 * 4, StoredObjectTable);
            AddObject(FireRatClaw as ISerializableJsonObject, data, ref offset, BaseOffset, 84 * 4, StoredObjectTable);
            AddObject(GoblinZapBall as ISerializableJsonObject, data, ref offset, BaseOffset, 85 * 4, StoredObjectTable);
            AddObject(GoblinHateZapBall as ISerializableJsonObject, data, ref offset, BaseOffset, 86 * 4, StoredObjectTable);
            AddObject(GoblinHateZapBall2 as ISerializableJsonObject, data, ref offset, BaseOffset, 87 * 4, StoredObjectTable);
            AddObject(RhinoHorn as ISerializableJsonObject, data, ref offset, BaseOffset, 88 * 4, StoredObjectTable);
            AddObject(RhinoRage as ISerializableJsonObject, data, ref offset, BaseOffset, 89 * 4, StoredObjectTable);
            AddObject(RhinoFireball as ISerializableJsonObject, data, ref offset, BaseOffset, 90 * 4, StoredObjectTable);
            AddObject(RhinoBossRage as ISerializableJsonObject, data, ref offset, BaseOffset, 91 * 4, StoredObjectTable);
            AddObject(YetiPunch as ISerializableJsonObject, data, ref offset, BaseOffset, 92 * 4, StoredObjectTable);
            AddObject(YetiEncase as ISerializableJsonObject, data, ref offset, BaseOffset, 93 * 4, StoredObjectTable);
            AddObject(YetiDebuff as ISerializableJsonObject, data, ref offset, BaseOffset, 94 * 4, StoredObjectTable);
            AddObject(YetiBoulderThrow as ISerializableJsonObject, data, ref offset, BaseOffset, 95 * 4, StoredObjectTable);
            AddObject(YetiBarrage as ISerializableJsonObject, data, ref offset, BaseOffset, 96 * 4, StoredObjectTable);
            AddObject(YetiRoarStun as ISerializableJsonObject, data, ref offset, BaseOffset, 97 * 4, StoredObjectTable);
            AddObject(YetiFlingAway as ISerializableJsonObject, data, ref offset, BaseOffset, 98 * 4, StoredObjectTable);
            AddObject(YetiIceBallThrow as ISerializableJsonObject, data, ref offset, BaseOffset, 99 * 4, StoredObjectTable);
            AddObject(YetiIceSpear as ISerializableJsonObject, data, ref offset, BaseOffset, 100 * 4, StoredObjectTable);
            AddObject(YetiColdOrb as ISerializableJsonObject, data, ref offset, BaseOffset, 101 * 4, StoredObjectTable);
            AddObject(YetiFrostRing as ISerializableJsonObject, data, ref offset, BaseOffset, 102 * 4, StoredObjectTable);
            AddObject(WorgBite as ISerializableJsonObject, data, ref offset, BaseOffset, 103 * 4, StoredObjectTable);
            AddObject(WorgOmegaBite as ISerializableJsonObject, data, ref offset, BaseOffset, 104 * 4, StoredObjectTable);
            AddObject(BearBite as ISerializableJsonObject, data, ref offset, BaseOffset, 105 * 4, StoredObjectTable);
            AddObject(BearCrush as ISerializableJsonObject, data, ref offset, BaseOffset, 106 * 4, StoredObjectTable);
            AddObject(BrainBite as ISerializableJsonObject, data, ref offset, BaseOffset, 107 * 4, StoredObjectTable);
            AddObject(BrainDrain as ISerializableJsonObject, data, ref offset, BaseOffset, 108 * 4, StoredObjectTable);
            AddObject(BrainDrain2 as ISerializableJsonObject, data, ref offset, BaseOffset, 109 * 4, StoredObjectTable);
            AddObject(BigCatClaw as ISerializableJsonObject, data, ref offset, BaseOffset, 110 * 4, StoredObjectTable);
            AddObject(BigCatPounce as ISerializableJsonObject, data, ref offset, BaseOffset, 111 * 4, StoredObjectTable);
            AddObject(BigCatRagePounce as ISerializableJsonObject, data, ref offset, BaseOffset, 112 * 4, StoredObjectTable);
            AddObject(BigCatDebuff as ISerializableJsonObject, data, ref offset, BaseOffset, 113 * 4, StoredObjectTable);
            AddObject(SpiderBite as ISerializableJsonObject, data, ref offset, BaseOffset, 114 * 4, StoredObjectTable);
            AddObject(SpiderFireball as ISerializableJsonObject, data, ref offset, BaseOffset, 115 * 4, StoredObjectTable);
            AddObject(SpiderBossFreePin as ISerializableJsonObject, data, ref offset, BaseOffset, 116 * 4, StoredObjectTable);
            AddObject(SpiderKill2 as ISerializableJsonObject, data, ref offset, BaseOffset, 117 * 4, StoredObjectTable);
            AddObject(SpiderInject as ISerializableJsonObject, data, ref offset, BaseOffset, 118 * 4, StoredObjectTable);
            AddObject(SpiderKill as ISerializableJsonObject, data, ref offset, BaseOffset, 119 * 4, StoredObjectTable);
            AddObject(AcidBall1 as ISerializableJsonObject, data, ref offset, BaseOffset, 120 * 4, StoredObjectTable);
            AddObject(SpiderPin as ISerializableJsonObject, data, ref offset, BaseOffset, 121 * 4, StoredObjectTable);
            AddObject(SpiderKill3 as ISerializableJsonObject, data, ref offset, BaseOffset, 122 * 4, StoredObjectTable);
            AddObject(AcidBall2 as ISerializableJsonObject, data, ref offset, BaseOffset, 123 * 4, StoredObjectTable);
            AddObject(AcidSpew1 as ISerializableJsonObject, data, ref offset, BaseOffset, 124 * 4, StoredObjectTable);
            AddObject(AcidExplosion1 as ISerializableJsonObject, data, ref offset, BaseOffset, 125 * 4, StoredObjectTable);
            AddObject(AcidExplosion2 as ISerializableJsonObject, data, ref offset, BaseOffset, 126 * 4, StoredObjectTable);
            AddObject(SpiderIncubate as ISerializableJsonObject, data, ref offset, BaseOffset, 127 * 4, StoredObjectTable);
            AddObject(MantisClaw as ISerializableJsonObject, data, ref offset, BaseOffset, 128 * 4, StoredObjectTable);
            AddObject(MantisRage as ISerializableJsonObject, data, ref offset, BaseOffset, 129 * 4, StoredObjectTable);
            AddObject(MantisAcidBurst as ISerializableJsonObject, data, ref offset, BaseOffset, 130 * 4, StoredObjectTable);
            AddObject(SherzatClaw as ISerializableJsonObject, data, ref offset, BaseOffset, 131 * 4, StoredObjectTable);
            AddObject(SherzatAcidSpit as ISerializableJsonObject, data, ref offset, BaseOffset, 132 * 4, StoredObjectTable);
            AddObject(SherzatDisintegrate as ISerializableJsonObject, data, ref offset, BaseOffset, 133 * 4, StoredObjectTable);
            AddObject(MantisSwipe as ISerializableJsonObject, data, ref offset, BaseOffset, 134 * 4, StoredObjectTable);
            AddObject(MantisBlast as ISerializableJsonObject, data, ref offset, BaseOffset, 135 * 4, StoredObjectTable);
            AddObject(SnailStrike as ISerializableJsonObject, data, ref offset, BaseOffset, 136 * 4, StoredObjectTable);
            AddObject(SnailRage as ISerializableJsonObject, data, ref offset, BaseOffset, 137 * 4, StoredObjectTable);
            AddObject(SnailRageB as ISerializableJsonObject, data, ref offset, BaseOffset, 138 * 4, StoredObjectTable);
            AddObject(SnailRageC as ISerializableJsonObject, data, ref offset, BaseOffset, 139 * 4, StoredObjectTable);
            AddObject(HookClaw as ISerializableJsonObject, data, ref offset, BaseOffset, 140 * 4, StoredObjectTable);
            AddObject(HookAcid as ISerializableJsonObject, data, ref offset, BaseOffset, 141 * 4, StoredObjectTable);
            AddObject(HookRage as ISerializableJsonObject, data, ref offset, BaseOffset, 142 * 4, StoredObjectTable);
            AddObject(UndeadSword1 as ISerializableJsonObject, data, ref offset, BaseOffset, 143 * 4, StoredObjectTable);
            AddObject(UndeadSword2 as ISerializableJsonObject, data, ref offset, BaseOffset, 144 * 4, StoredObjectTable);
            AddObject(UndeadSwordAngry as ISerializableJsonObject, data, ref offset, BaseOffset, 145 * 4, StoredObjectTable);
            AddObject(UndeadMegaSword1 as ISerializableJsonObject, data, ref offset, BaseOffset, 146 * 4, StoredObjectTable);
            AddObject(UndeadMegaSword2 as ISerializableJsonObject, data, ref offset, BaseOffset, 147 * 4, StoredObjectTable);
            AddObject(UndeadMegaSwordAngry as ISerializableJsonObject, data, ref offset, BaseOffset, 148 * 4, StoredObjectTable);
            AddObject(UndeadLightningSmite as ISerializableJsonObject, data, ref offset, BaseOffset, 149 * 4, StoredObjectTable);
            AddObject(UndeadPhysicalShield as ISerializableJsonObject, data, ref offset, BaseOffset, 150 * 4, StoredObjectTable);
            AddObject(UndeadSword1B as ISerializableJsonObject, data, ref offset, BaseOffset, 151 * 4, StoredObjectTable);
            AddObject(UndeadFireballA as ISerializableJsonObject, data, ref offset, BaseOffset, 152 * 4, StoredObjectTable);
            AddObject(UndeadFireballB as ISerializableJsonObject, data, ref offset, BaseOffset, 153 * 4, StoredObjectTable);
            AddObject(UndeadFireballB2 as ISerializableJsonObject, data, ref offset, BaseOffset, 154 * 4, StoredObjectTable);
            AddObject(UndeadIceBall1 as ISerializableJsonObject, data, ref offset, BaseOffset, 155 * 4, StoredObjectTable);
            AddObject(UndeadFreezeBall as ISerializableJsonObject, data, ref offset, BaseOffset, 156 * 4, StoredObjectTable);
            AddObject(UndeadFireballLongA as ISerializableJsonObject, data, ref offset, BaseOffset, 157 * 4, StoredObjectTable);
            AddObject(UndeadFireballLongB as ISerializableJsonObject, data, ref offset, BaseOffset, 158 * 4, StoredObjectTable);
            AddObject(UndeadFireballLongB2 as ISerializableJsonObject, data, ref offset, BaseOffset, 159 * 4, StoredObjectTable);
            AddObject(KhyrulekCurseBall as ISerializableJsonObject, data, ref offset, BaseOffset, 160 * 4, StoredObjectTable);
            AddObject(UrsulaFireball1 as ISerializableJsonObject, data, ref offset, BaseOffset, 161 * 4, StoredObjectTable);
            AddObject(UrsulaFireball1B as ISerializableJsonObject, data, ref offset, BaseOffset, 162 * 4, StoredObjectTable);
            AddObject(UrsulaFireball2 as ISerializableJsonObject, data, ref offset, BaseOffset, 163 * 4, StoredObjectTable);
            AddObject(UrsulaIceball1 as ISerializableJsonObject, data, ref offset, BaseOffset, 164 * 4, StoredObjectTable);
            AddObject(UrsulaIceball1B as ISerializableJsonObject, data, ref offset, BaseOffset, 165 * 4, StoredObjectTable);
            AddObject(UrsulaIceball2 as ISerializableJsonObject, data, ref offset, BaseOffset, 166 * 4, StoredObjectTable);
            AddObject(UrsulaSummon as ISerializableJsonObject, data, ref offset, BaseOffset, 167 * 4, StoredObjectTable);
            AddObject(UrsulaRage as ISerializableJsonObject, data, ref offset, BaseOffset, 168 * 4, StoredObjectTable);
            AddObject(UndeadFireballA2 as ISerializableJsonObject, data, ref offset, BaseOffset, 169 * 4, StoredObjectTable);
            AddObject(BigHeadCurseball as ISerializableJsonObject, data, ref offset, BaseOffset, 170 * 4, StoredObjectTable);
            AddObject(UndeadArrow1 as ISerializableJsonObject, data, ref offset, BaseOffset, 171 * 4, StoredObjectTable);
            AddObject(UndeadArrow2 as ISerializableJsonObject, data, ref offset, BaseOffset, 172 * 4, StoredObjectTable);
            AddObject(UndeadOmegaArrow as ISerializableJsonObject, data, ref offset, BaseOffset, 173 * 4, StoredObjectTable);
            AddObject(PetUndeadArrow1 as ISerializableJsonObject, data, ref offset, BaseOffset, 174 * 4, StoredObjectTable);
            AddObject(PetUndeadArrow2 as ISerializableJsonObject, data, ref offset, BaseOffset, 175 * 4, StoredObjectTable);
            AddObject(PetUndeadOmegaArrow as ISerializableJsonObject, data, ref offset, BaseOffset, 176 * 4, StoredObjectTable);
            AddObject(UndeadGrappleArrow1 as ISerializableJsonObject, data, ref offset, BaseOffset, 177 * 4, StoredObjectTable);
            AddObject(UndeadSelfDestruct as ISerializableJsonObject, data, ref offset, BaseOffset, 178 * 4, StoredObjectTable);
            AddObject(UndeadDarknessBall as ISerializableJsonObject, data, ref offset, BaseOffset, 179 * 4, StoredObjectTable);
            AddObject(UndeadBoneWhirlwind as ISerializableJsonObject, data, ref offset, BaseOffset, 180 * 4, StoredObjectTable);
            AddObject(PetUndeadSword1 as ISerializableJsonObject, data, ref offset, BaseOffset, 181 * 4, StoredObjectTable);
            AddObject(PetUndeadSword2 as ISerializableJsonObject, data, ref offset, BaseOffset, 182 * 4, StoredObjectTable);
            AddObject(PetUndeadSwordAngry as ISerializableJsonObject, data, ref offset, BaseOffset, 183 * 4, StoredObjectTable);
            AddObject(PetUndeadFireballA as ISerializableJsonObject, data, ref offset, BaseOffset, 184 * 4, StoredObjectTable);
            AddObject(PetUndeadFireballB as ISerializableJsonObject, data, ref offset, BaseOffset, 185 * 4, StoredObjectTable);
            AddObject(PetUndeadDefensiveBurst as ISerializableJsonObject, data, ref offset, BaseOffset, 186 * 4, StoredObjectTable);
            AddObject(PetUndeadPunch1 as ISerializableJsonObject, data, ref offset, BaseOffset, 187 * 4, StoredObjectTable);
            AddObject(ZombiePunch as ISerializableJsonObject, data, ref offset, BaseOffset, 188 * 4, StoredObjectTable);
            AddObject(ZombieBite as ISerializableJsonObject, data, ref offset, BaseOffset, 189 * 4, StoredObjectTable);
            AddObject(GoblinSpear1 as ISerializableJsonObject, data, ref offset, BaseOffset, 190 * 4, StoredObjectTable);
            AddObject(GoblinSpear2 as ISerializableJsonObject, data, ref offset, BaseOffset, 191 * 4, StoredObjectTable);
            AddObject(GoblinRageSpear1 as ISerializableJsonObject, data, ref offset, BaseOffset, 192 * 4, StoredObjectTable);
            AddObject(GoblinRageSpear2 as ISerializableJsonObject, data, ref offset, BaseOffset, 193 * 4, StoredObjectTable);
            AddObject(GoblinHeal1 as ISerializableJsonObject, data, ref offset, BaseOffset, 194 * 4, StoredObjectTable);
            AddObject(GoblinHeal2 as ISerializableJsonObject, data, ref offset, BaseOffset, 195 * 4, StoredObjectTable);
            AddObject(GoblinPunch as ISerializableJsonObject, data, ref offset, BaseOffset, 196 * 4, StoredObjectTable);
            AddObject(GoblinArrow1 as ISerializableJsonObject, data, ref offset, BaseOffset, 197 * 4, StoredObjectTable);
            AddObject(GoblinArrow2 as ISerializableJsonObject, data, ref offset, BaseOffset, 198 * 4, StoredObjectTable);
            AddObject(GoblinRageArrow1 as ISerializableJsonObject, data, ref offset, BaseOffset, 199 * 4, StoredObjectTable);
            AddObject(GoblinRageArrow2 as ISerializableJsonObject, data, ref offset, BaseOffset, 200 * 4, StoredObjectTable);
            AddObject(GoblinSpreadZapBall as ISerializableJsonObject, data, ref offset, BaseOffset, 201 * 4, StoredObjectTable);
            AddObject(GoblinBossLightning as ISerializableJsonObject, data, ref offset, BaseOffset, 202 * 4, StoredObjectTable);
            AddObject(GoblinArmorBuff as ISerializableJsonObject, data, ref offset, BaseOffset, 203 * 4, StoredObjectTable);
            AddObject(MummySlamA as ISerializableJsonObject, data, ref offset, BaseOffset, 204 * 4, StoredObjectTable);
            AddObject(MummySlamB as ISerializableJsonObject, data, ref offset, BaseOffset, 205 * 4, StoredObjectTable);
            AddObject(MummySlamCombo as ISerializableJsonObject, data, ref offset, BaseOffset, 206 * 4, StoredObjectTable);
            AddObject(MummyWrapA as ISerializableJsonObject, data, ref offset, BaseOffset, 207 * 4, StoredObjectTable);
            AddObject(MummyWrapB as ISerializableJsonObject, data, ref offset, BaseOffset, 208 * 4, StoredObjectTable);
            AddObject(MummyWrapRage as ISerializableJsonObject, data, ref offset, BaseOffset, 209 * 4, StoredObjectTable);
            AddObject(BarutiWrapA as ISerializableJsonObject, data, ref offset, BaseOffset, 210 * 4, StoredObjectTable);
            AddObject(BarutiWrapB as ISerializableJsonObject, data, ref offset, BaseOffset, 211 * 4, StoredObjectTable);
            AddObject(BarutiWrapRage as ISerializableJsonObject, data, ref offset, BaseOffset, 212 * 4, StoredObjectTable);
            AddObject(FireWallAttack1 as ISerializableJsonObject, data, ref offset, BaseOffset, 213 * 4, StoredObjectTable);
            AddObject(FireWallDotAttack1 as ISerializableJsonObject, data, ref offset, BaseOffset, 214 * 4, StoredObjectTable);
            AddObject(FireSnakeExplosion1 as ISerializableJsonObject, data, ref offset, BaseOffset, 215 * 4, StoredObjectTable);
            AddObject(FireTrapAttack1 as ISerializableJsonObject, data, ref offset, BaseOffset, 216 * 4, StoredObjectTable);
            AddObject(HealingAura1 as ISerializableJsonObject, data, ref offset, BaseOffset, 217 * 4, StoredObjectTable);
            AddObject(HealingAura2 as ISerializableJsonObject, data, ref offset, BaseOffset, 218 * 4, StoredObjectTable);
            AddObject(HealingAura3 as ISerializableJsonObject, data, ref offset, BaseOffset, 219 * 4, StoredObjectTable);
            AddObject(HealingAura4 as ISerializableJsonObject, data, ref offset, BaseOffset, 220 * 4, StoredObjectTable);
            AddObject(DruidHealingSanctuaryHeal as ISerializableJsonObject, data, ref offset, BaseOffset, 221 * 4, StoredObjectTable);
            AddObject(AcidAuraBall1 as ISerializableJsonObject, data, ref offset, BaseOffset, 222 * 4, StoredObjectTable);
            AddObject(AcidAuraBall2 as ISerializableJsonObject, data, ref offset, BaseOffset, 223 * 4, StoredObjectTable);
            AddObject(AcidAuraBall3 as ISerializableJsonObject, data, ref offset, BaseOffset, 224 * 4, StoredObjectTable);
            AddObject(AcidAuraBall4 as ISerializableJsonObject, data, ref offset, BaseOffset, 225 * 4, StoredObjectTable);
            AddObject(ElectricityAura1 as ISerializableJsonObject, data, ref offset, BaseOffset, 226 * 4, StoredObjectTable);
            AddObject(ElectricityAuraBolt1 as ISerializableJsonObject, data, ref offset, BaseOffset, 227 * 4, StoredObjectTable);
            AddObject(ReboundAura1 as ISerializableJsonObject, data, ref offset, BaseOffset, 228 * 4, StoredObjectTable);
            AddObject(ColdAuraBurst as ISerializableJsonObject, data, ref offset, BaseOffset, 229 * 4, StoredObjectTable);
            AddObject(WebStick as ISerializableJsonObject, data, ref offset, BaseOffset, 230 * 4, StoredObjectTable);
            AddObject(IcySlam as ISerializableJsonObject, data, ref offset, BaseOffset, 231 * 4, StoredObjectTable);
            AddObject(IcyCocoon as ISerializableJsonObject, data, ref offset, BaseOffset, 232 * 4, StoredObjectTable);
            AddObject(IcyCocoon2 as ISerializableJsonObject, data, ref offset, BaseOffset, 233 * 4, StoredObjectTable);
            AddObject(ElementalSlam as ISerializableJsonObject, data, ref offset, BaseOffset, 234 * 4, StoredObjectTable);
            AddObject(ElementalBees as ISerializableJsonObject, data, ref offset, BaseOffset, 235 * 4, StoredObjectTable);
            AddObject(ElementalBees2 as ISerializableJsonObject, data, ref offset, BaseOffset, 236 * 4, StoredObjectTable);
            AddObject(FaeLightningSmite as ISerializableJsonObject, data, ref offset, BaseOffset, 237 * 4, StoredObjectTable);
            AddObject(TotalHorrorAttack as ISerializableJsonObject, data, ref offset, BaseOffset, 238 * 4, StoredObjectTable);
            AddObject(TotalHorrorStretch as ISerializableJsonObject, data, ref offset, BaseOffset, 239 * 4, StoredObjectTable);
            AddObject(TotalHorrorHeal as ISerializableJsonObject, data, ref offset, BaseOffset, 240 * 4, StoredObjectTable);
            AddObject(TotalHorrorHeal2 as ISerializableJsonObject, data, ref offset, BaseOffset, 241 * 4, StoredObjectTable);
            AddObject(SheepBomb1 as ISerializableJsonObject, data, ref offset, BaseOffset, 242 * 4, StoredObjectTable);
            AddObject(SlugPoisonBite as ISerializableJsonObject, data, ref offset, BaseOffset, 243 * 4, StoredObjectTable);
            AddObject(SlugPoisonRage as ISerializableJsonObject, data, ref offset, BaseOffset, 244 * 4, StoredObjectTable);
            AddObject(SlugPoisonBite2 as ISerializableJsonObject, data, ref offset, BaseOffset, 245 * 4, StoredObjectTable);
            AddObject(SlugPoisonRage2 as ISerializableJsonObject, data, ref offset, BaseOffset, 246 * 4, StoredObjectTable);
            AddObject(SlugPoisonBite3 as ISerializableJsonObject, data, ref offset, BaseOffset, 247 * 4, StoredObjectTable);
            AddObject(SlugPoisonRage3 as ISerializableJsonObject, data, ref offset, BaseOffset, 248 * 4, StoredObjectTable);
            AddObject(TornadoJolt1 as ISerializableJsonObject, data, ref offset, BaseOffset, 249 * 4, StoredObjectTable);
            AddObject(TornadoFling as ISerializableJsonObject, data, ref offset, BaseOffset, 250 * 4, StoredObjectTable);
            AddObject(TornadoToss as ISerializableJsonObject, data, ref offset, BaseOffset, 251 * 4, StoredObjectTable);
            AddObject(TheFogCurse as ISerializableJsonObject, data, ref offset, BaseOffset, 252 * 4, StoredObjectTable);
            AddObject(MonsterWerewolfPouncingRake as ISerializableJsonObject, data, ref offset, BaseOffset, 253 * 4, StoredObjectTable);
            AddObject(MonsterWerewolfPackAttack as ISerializableJsonObject, data, ref offset, BaseOffset, 254 * 4, StoredObjectTable);
            AddObject(MonsterWerewolfHowl as ISerializableJsonObject, data, ref offset, BaseOffset, 255 * 4, StoredObjectTable);
            AddObject(Werewolf_Summon_Rage as ISerializableJsonObject, data, ref offset, BaseOffset, 256 * 4, StoredObjectTable);
            AddObject(Werewolf_Summon_Opener as ISerializableJsonObject, data, ref offset, BaseOffset, 257 * 4, StoredObjectTable);
            AddObject(BleddynHowl as ISerializableJsonObject, data, ref offset, BaseOffset, 258 * 4, StoredObjectTable);
            AddObject(OrcSwordSlash as ISerializableJsonObject, data, ref offset, BaseOffset, 259 * 4, StoredObjectTable);
            AddObject(OrcParry as ISerializableJsonObject, data, ref offset, BaseOffset, 260 * 4, StoredObjectTable);
            AddObject(OrcFinishingBlow as ISerializableJsonObject, data, ref offset, BaseOffset, 261 * 4, StoredObjectTable);
            AddObject(OrcHipThrow as ISerializableJsonObject, data, ref offset, BaseOffset, 262 * 4, StoredObjectTable);
            AddObject(OrcKneeKick as ISerializableJsonObject, data, ref offset, BaseOffset, 263 * 4, StoredObjectTable);
            AddObject(OrcPunch as ISerializableJsonObject, data, ref offset, BaseOffset, 264 * 4, StoredObjectTable);
            AddObject(OrcArrow1 as ISerializableJsonObject, data, ref offset, BaseOffset, 265 * 4, StoredObjectTable);
            AddObject(OrcArrow2 as ISerializableJsonObject, data, ref offset, BaseOffset, 266 * 4, StoredObjectTable);
            AddObject(OrcStaffSmash as ISerializableJsonObject, data, ref offset, BaseOffset, 267 * 4, StoredObjectTable);
            AddObject(OrcFireball as ISerializableJsonObject, data, ref offset, BaseOffset, 268 * 4, StoredObjectTable);
            AddObject(OrcHeal1 as ISerializableJsonObject, data, ref offset, BaseOffset, 269 * 4, StoredObjectTable);
            AddObject(OrcHeal2 as ISerializableJsonObject, data, ref offset, BaseOffset, 270 * 4, StoredObjectTable);
            AddObject(OrcEvasionBubble as ISerializableJsonObject, data, ref offset, BaseOffset, 271 * 4, StoredObjectTable);
            AddObject(OrcElectricStun as ISerializableJsonObject, data, ref offset, BaseOffset, 272 * 4, StoredObjectTable);
            AddObject(OrcFireBolts as ISerializableJsonObject, data, ref offset, BaseOffset, 273 * 4, StoredObjectTable);
            AddObject(OrcKnockbackBolt as ISerializableJsonObject, data, ref offset, BaseOffset, 274 * 4, StoredObjectTable);
            AddObject(OrcSummonUrak2 as ISerializableJsonObject, data, ref offset, BaseOffset, 275 * 4, StoredObjectTable);
            AddObject(OrcSwordSlashFire as ISerializableJsonObject, data, ref offset, BaseOffset, 276 * 4, StoredObjectTable);
            AddObject(OrcParryFire as ISerializableJsonObject, data, ref offset, BaseOffset, 277 * 4, StoredObjectTable);
            AddObject(OrcFinishingBlowFire as ISerializableJsonObject, data, ref offset, BaseOffset, 278 * 4, StoredObjectTable);
            AddObject(OrcSummonSigil1 as ISerializableJsonObject, data, ref offset, BaseOffset, 279 * 4, StoredObjectTable);
            AddObject(OrcSpearAttack as ISerializableJsonObject, data, ref offset, BaseOffset, 280 * 4, StoredObjectTable);
            AddObject(OrcHalberdAttack as ISerializableJsonObject, data, ref offset, BaseOffset, 281 * 4, StoredObjectTable);
            AddObject(OrcAreaHalberdAttack as ISerializableJsonObject, data, ref offset, BaseOffset, 282 * 4, StoredObjectTable);
            AddObject(OrcDebuffArrow as ISerializableJsonObject, data, ref offset, BaseOffset, 283 * 4, StoredObjectTable);
            AddObject(OrcSlice as ISerializableJsonObject, data, ref offset, BaseOffset, 284 * 4, StoredObjectTable);
            AddObject(OrcVenomstrike1 as ISerializableJsonObject, data, ref offset, BaseOffset, 285 * 4, StoredObjectTable);
            AddObject(OrcVenomstrike0 as ISerializableJsonObject, data, ref offset, BaseOffset, 286 * 4, StoredObjectTable);
            AddObject(OrcLieutenantDebuffTaunt as ISerializableJsonObject, data, ref offset, BaseOffset, 287 * 4, StoredObjectTable);
            AddObject(OrcAreaHalberdBoss as ISerializableJsonObject, data, ref offset, BaseOffset, 288 * 4, StoredObjectTable);
            AddObject(OrcDeathsHold as ISerializableJsonObject, data, ref offset, BaseOffset, 289 * 4, StoredObjectTable);
            AddObject(GazlukPriest1Special as ISerializableJsonObject, data, ref offset, BaseOffset, 290 * 4, StoredObjectTable);
            AddObject(GazlukPriest2Special as ISerializableJsonObject, data, ref offset, BaseOffset, 291 * 4, StoredObjectTable);
            AddObject(GazlukPriest3Special as ISerializableJsonObject, data, ref offset, BaseOffset, 292 * 4, StoredObjectTable);
            AddObject(OrcExtinguishLife as ISerializableJsonObject, data, ref offset, BaseOffset, 293 * 4, StoredObjectTable);
            AddObject(OrcDarknessBall as ISerializableJsonObject, data, ref offset, BaseOffset, 294 * 4, StoredObjectTable);
            AddObject(OrcWaveOfDarkness as ISerializableJsonObject, data, ref offset, BaseOffset, 295 * 4, StoredObjectTable);
            AddObject(EnemyMinigolemPunch as ISerializableJsonObject, data, ref offset, BaseOffset, 296 * 4, StoredObjectTable);
            AddObject(EnemyMinigolemHeal as ISerializableJsonObject, data, ref offset, BaseOffset, 297 * 4, StoredObjectTable);
            AddObject(EnemyMinigolemExplode as ISerializableJsonObject, data, ref offset, BaseOffset, 298 * 4, StoredObjectTable);
            AddObject(MinigolemBombToss as ISerializableJsonObject, data, ref offset, BaseOffset, 299 * 4, StoredObjectTable);
            AddObject(MinigolemBombToss2 as ISerializableJsonObject, data, ref offset, BaseOffset, 300 * 4, StoredObjectTable);
            AddObject(MinigolemBombToss3 as ISerializableJsonObject, data, ref offset, BaseOffset, 301 * 4, StoredObjectTable);
            AddObject(MinigolemBombToss4 as ISerializableJsonObject, data, ref offset, BaseOffset, 302 * 4, StoredObjectTable);
            AddObject(MinigolemBombToss5 as ISerializableJsonObject, data, ref offset, BaseOffset, 303 * 4, StoredObjectTable);
            AddObject(MinigolemAoEHeal as ISerializableJsonObject, data, ref offset, BaseOffset, 304 * 4, StoredObjectTable);
            AddObject(MinigolemAoEHeal2 as ISerializableJsonObject, data, ref offset, BaseOffset, 305 * 4, StoredObjectTable);
            AddObject(MinigolemAoEHeal3 as ISerializableJsonObject, data, ref offset, BaseOffset, 306 * 4, StoredObjectTable);
            AddObject(MinigolemAoEHeal4 as ISerializableJsonObject, data, ref offset, BaseOffset, 307 * 4, StoredObjectTable);
            AddObject(MinigolemAoEHeal5 as ISerializableJsonObject, data, ref offset, BaseOffset, 308 * 4, StoredObjectTable);
            AddObject(MinigolemSelfDestruct as ISerializableJsonObject, data, ref offset, BaseOffset, 309 * 4, StoredObjectTable);
            AddObject(MinigolemSelfDestruct2 as ISerializableJsonObject, data, ref offset, BaseOffset, 310 * 4, StoredObjectTable);
            AddObject(MinigolemSelfDestruct3 as ISerializableJsonObject, data, ref offset, BaseOffset, 311 * 4, StoredObjectTable);
            AddObject(MinigolemSelfDestruct4 as ISerializableJsonObject, data, ref offset, BaseOffset, 312 * 4, StoredObjectTable);
            AddObject(MinigolemSelfDestruct5 as ISerializableJsonObject, data, ref offset, BaseOffset, 313 * 4, StoredObjectTable);
            AddObject(MinigolemAoEPower as ISerializableJsonObject, data, ref offset, BaseOffset, 314 * 4, StoredObjectTable);
            AddObject(MinigolemAoEPower2 as ISerializableJsonObject, data, ref offset, BaseOffset, 315 * 4, StoredObjectTable);
            AddObject(MinigolemAoEPower3 as ISerializableJsonObject, data, ref offset, BaseOffset, 316 * 4, StoredObjectTable);
            AddObject(MinigolemAoEPower4 as ISerializableJsonObject, data, ref offset, BaseOffset, 317 * 4, StoredObjectTable);
            AddObject(MinigolemAoEPower5 as ISerializableJsonObject, data, ref offset, BaseOffset, 318 * 4, StoredObjectTable);
            AddObject(MinigolemHeal as ISerializableJsonObject, data, ref offset, BaseOffset, 319 * 4, StoredObjectTable);
            AddObject(MinigolemHeal2 as ISerializableJsonObject, data, ref offset, BaseOffset, 320 * 4, StoredObjectTable);
            AddObject(MinigolemHeal3 as ISerializableJsonObject, data, ref offset, BaseOffset, 321 * 4, StoredObjectTable);
            AddObject(MinigolemHeal4 as ISerializableJsonObject, data, ref offset, BaseOffset, 322 * 4, StoredObjectTable);
            AddObject(MinigolemHeal5 as ISerializableJsonObject, data, ref offset, BaseOffset, 323 * 4, StoredObjectTable);
            AddObject(MinigolemDoomAdmixture as ISerializableJsonObject, data, ref offset, BaseOffset, 324 * 4, StoredObjectTable);
            AddObject(MinigolemDoomAdmixture2 as ISerializableJsonObject, data, ref offset, BaseOffset, 325 * 4, StoredObjectTable);
            AddObject(MinigolemDoomAdmixture3 as ISerializableJsonObject, data, ref offset, BaseOffset, 326 * 4, StoredObjectTable);
            AddObject(MinigolemDoomAdmixture4 as ISerializableJsonObject, data, ref offset, BaseOffset, 327 * 4, StoredObjectTable);
            AddObject(MinigolemDoomAdmixture5 as ISerializableJsonObject, data, ref offset, BaseOffset, 328 * 4, StoredObjectTable);
            AddObject(MinigolemSelfSacrifice as ISerializableJsonObject, data, ref offset, BaseOffset, 329 * 4, StoredObjectTable);
            AddObject(MinigolemSelfSacrifice2 as ISerializableJsonObject, data, ref offset, BaseOffset, 330 * 4, StoredObjectTable);
            AddObject(MinigolemSelfSacrifice3 as ISerializableJsonObject, data, ref offset, BaseOffset, 331 * 4, StoredObjectTable);
            AddObject(MinigolemSelfSacrifice4 as ISerializableJsonObject, data, ref offset, BaseOffset, 332 * 4, StoredObjectTable);
            AddObject(MinigolemSelfSacrifice5 as ISerializableJsonObject, data, ref offset, BaseOffset, 333 * 4, StoredObjectTable);
            AddObject(MinigolemPunch as ISerializableJsonObject, data, ref offset, BaseOffset, 334 * 4, StoredObjectTable);
            AddObject(MinigolemPunch2 as ISerializableJsonObject, data, ref offset, BaseOffset, 335 * 4, StoredObjectTable);
            AddObject(MinigolemPunch3 as ISerializableJsonObject, data, ref offset, BaseOffset, 336 * 4, StoredObjectTable);
            AddObject(MinigolemPunch4 as ISerializableJsonObject, data, ref offset, BaseOffset, 337 * 4, StoredObjectTable);
            AddObject(MinigolemPunch5 as ISerializableJsonObject, data, ref offset, BaseOffset, 338 * 4, StoredObjectTable);
            AddObject(MinigolemHasteConcoction1 as ISerializableJsonObject, data, ref offset, BaseOffset, 339 * 4, StoredObjectTable);
            AddObject(MinigolemHasteConcoction2 as ISerializableJsonObject, data, ref offset, BaseOffset, 340 * 4, StoredObjectTable);
            AddObject(MinigolemHasteConcoction3 as ISerializableJsonObject, data, ref offset, BaseOffset, 341 * 4, StoredObjectTable);
            AddObject(MinigolemFireBalm1 as ISerializableJsonObject, data, ref offset, BaseOffset, 342 * 4, StoredObjectTable);
            AddObject(MinigolemFireBalm2 as ISerializableJsonObject, data, ref offset, BaseOffset, 343 * 4, StoredObjectTable);
            AddObject(MinigolemFireBalm3 as ISerializableJsonObject, data, ref offset, BaseOffset, 344 * 4, StoredObjectTable);
            AddObject(MinigolemFireBalm4 as ISerializableJsonObject, data, ref offset, BaseOffset, 345 * 4, StoredObjectTable);
            AddObject(MinigolemFireBalm5 as ISerializableJsonObject, data, ref offset, BaseOffset, 346 * 4, StoredObjectTable);
            AddObject(MinigolemRageAoEHeal1 as ISerializableJsonObject, data, ref offset, BaseOffset, 347 * 4, StoredObjectTable);
            AddObject(MinigolemRageAoEHeal2 as ISerializableJsonObject, data, ref offset, BaseOffset, 348 * 4, StoredObjectTable);
            AddObject(MinigolemRageAoEHeal3 as ISerializableJsonObject, data, ref offset, BaseOffset, 349 * 4, StoredObjectTable);
            AddObject(MinigolemRageAoEHeal4 as ISerializableJsonObject, data, ref offset, BaseOffset, 350 * 4, StoredObjectTable);
            AddObject(MinigolemRageAoEHeal5 as ISerializableJsonObject, data, ref offset, BaseOffset, 351 * 4, StoredObjectTable);
            AddObject(MinigolemRageAcidToss1 as ISerializableJsonObject, data, ref offset, BaseOffset, 352 * 4, StoredObjectTable);
            AddObject(MinigolemRageAcidToss2 as ISerializableJsonObject, data, ref offset, BaseOffset, 353 * 4, StoredObjectTable);
            AddObject(MinigolemRageAcidToss3 as ISerializableJsonObject, data, ref offset, BaseOffset, 354 * 4, StoredObjectTable);
            AddObject(MinigolemRageAcidToss4 as ISerializableJsonObject, data, ref offset, BaseOffset, 355 * 4, StoredObjectTable);
            AddObject(MinigolemRageAcidToss5 as ISerializableJsonObject, data, ref offset, BaseOffset, 356 * 4, StoredObjectTable);
            AddObject(TrainingGolemPunch as ISerializableJsonObject, data, ref offset, BaseOffset, 357 * 4, StoredObjectTable);
            AddObject(TrainingGolemStun as ISerializableJsonObject, data, ref offset, BaseOffset, 358 * 4, StoredObjectTable);
            AddObject(TrainingGolemHeal as ISerializableJsonObject, data, ref offset, BaseOffset, 359 * 4, StoredObjectTable);
            AddObject(TrainingGolemHealB as ISerializableJsonObject, data, ref offset, BaseOffset, 360 * 4, StoredObjectTable);
            AddObject(TrainingGolemFireBreath as ISerializableJsonObject, data, ref offset, BaseOffset, 361 * 4, StoredObjectTable);
            AddObject(TrainingGolemFireBurst as ISerializableJsonObject, data, ref offset, BaseOffset, 362 * 4, StoredObjectTable);
            AddObject(GrimalkinClaw as ISerializableJsonObject, data, ref offset, BaseOffset, 363 * 4, StoredObjectTable);
            AddObject(GrimalkinBite as ISerializableJsonObject, data, ref offset, BaseOffset, 364 * 4, StoredObjectTable);
            AddObject(GrimalkinPuncture as ISerializableJsonObject, data, ref offset, BaseOffset, 365 * 4, StoredObjectTable);
            AddObject(WerewolfSword1 as ISerializableJsonObject, data, ref offset, BaseOffset, 366 * 4, StoredObjectTable);
            AddObject(WerewolfSword2 as ISerializableJsonObject, data, ref offset, BaseOffset, 367 * 4, StoredObjectTable);
            AddObject(WerewolfSwordStun as ISerializableJsonObject, data, ref offset, BaseOffset, 368 * 4, StoredObjectTable);
            AddObject(WerewolfArrow1 as ISerializableJsonObject, data, ref offset, BaseOffset, 369 * 4, StoredObjectTable);
            AddObject(WerewolfArrow2 as ISerializableJsonObject, data, ref offset, BaseOffset, 370 * 4, StoredObjectTable);
            AddObject(WerewolfOmegaArrow as ISerializableJsonObject, data, ref offset, BaseOffset, 371 * 4, StoredObjectTable);
            AddObject(NpcSmash as ISerializableJsonObject, data, ref offset, BaseOffset, 372 * 4, StoredObjectTable);
            AddObject(NpcDoubleHitCurse as ISerializableJsonObject, data, ref offset, BaseOffset, 373 * 4, StoredObjectTable);
            AddObject(NpcBlockingStance as ISerializableJsonObject, data, ref offset, BaseOffset, 374 * 4, StoredObjectTable);
            AddObject(NpcHeadcracker as ISerializableJsonObject, data, ref offset, BaseOffset, 375 * 4, StoredObjectTable);
            AddObject(StrigaClawA as ISerializableJsonObject, data, ref offset, BaseOffset, 376 * 4, StoredObjectTable);
            AddObject(StrigaClawB as ISerializableJsonObject, data, ref offset, BaseOffset, 377 * 4, StoredObjectTable);
            AddObject(StrigaReap as ISerializableJsonObject, data, ref offset, BaseOffset, 378 * 4, StoredObjectTable);
            AddObject(StrigaReap2 as ISerializableJsonObject, data, ref offset, BaseOffset, 379 * 4, StoredObjectTable);
            AddObject(StrigaFireBreath as ISerializableJsonObject, data, ref offset, BaseOffset, 380 * 4, StoredObjectTable);
            AddObject(StrigaBuff as ISerializableJsonObject, data, ref offset, BaseOffset, 381 * 4, StoredObjectTable);
            AddObject(GhostlyPunchA as ISerializableJsonObject, data, ref offset, BaseOffset, 382 * 4, StoredObjectTable);
            AddObject(GhostlyPunchB as ISerializableJsonObject, data, ref offset, BaseOffset, 383 * 4, StoredObjectTable);
            AddObject(GhostlyBurst as ISerializableJsonObject, data, ref offset, BaseOffset, 384 * 4, StoredObjectTable);
            AddObject(GhostlyBossPunchA as ISerializableJsonObject, data, ref offset, BaseOffset, 385 * 4, StoredObjectTable);
            AddObject(GhostlyBossPunchB as ISerializableJsonObject, data, ref offset, BaseOffset, 386 * 4, StoredObjectTable);
            AddObject(GhostlyBossBurst as ISerializableJsonObject, data, ref offset, BaseOffset, 387 * 4, StoredObjectTable);
            AddObject(GhostlyBolt as ISerializableJsonObject, data, ref offset, BaseOffset, 388 * 4, StoredObjectTable);
            AddObject(InjectorBugBite as ISerializableJsonObject, data, ref offset, BaseOffset, 389 * 4, StoredObjectTable);
            AddObject(InjectorBugInject as ISerializableJsonObject, data, ref offset, BaseOffset, 390 * 4, StoredObjectTable);
            AddObject(InjectorBugInject2 as ISerializableJsonObject, data, ref offset, BaseOffset, 391 * 4, StoredObjectTable);
            AddObject(FaceOfDeathKill as ISerializableJsonObject, data, ref offset, BaseOffset, 392 * 4, StoredObjectTable);
            AddObject(WatcherFireball as ISerializableJsonObject, data, ref offset, BaseOffset, 393 * 4, StoredObjectTable);
            AddObject(WatcherSlap as ISerializableJsonObject, data, ref offset, BaseOffset, 394 * 4, StoredObjectTable);
            AddObject(WatcherAcidball as ISerializableJsonObject, data, ref offset, BaseOffset, 395 * 4, StoredObjectTable);
            AddObject(RedCrystalBlast as ISerializableJsonObject, data, ref offset, BaseOffset, 396 * 4, StoredObjectTable);
            AddObject(RedCrystalBurst as ISerializableJsonObject, data, ref offset, BaseOffset, 397 * 4, StoredObjectTable);
            AddObject(TurretCrystalZap as ISerializableJsonObject, data, ref offset, BaseOffset, 398 * 4, StoredObjectTable);
            AddObject(TurretCrystalZap2 as ISerializableJsonObject, data, ref offset, BaseOffset, 399 * 4, StoredObjectTable);
            AddObject(TurretCrystalZapLongRange as ISerializableJsonObject, data, ref offset, BaseOffset, 400 * 4, StoredObjectTable);
            AddObject(TurretCrystalZapLongRange2 as ISerializableJsonObject, data, ref offset, BaseOffset, 401 * 4, StoredObjectTable);
            AddObject(DeathRay as ISerializableJsonObject, data, ref offset, BaseOffset, 402 * 4, StoredObjectTable);
            AddObject(SpyPortalZap as ISerializableJsonObject, data, ref offset, BaseOffset, 403 * 4, StoredObjectTable);
            AddObject(SpyPortalZap2 as ISerializableJsonObject, data, ref offset, BaseOffset, 404 * 4, StoredObjectTable);
            AddObject(BitingVineBite as ISerializableJsonObject, data, ref offset, BaseOffset, 405 * 4, StoredObjectTable);
            AddObject(BitingVineSpit as ISerializableJsonObject, data, ref offset, BaseOffset, 406 * 4, StoredObjectTable);
            AddObject(BitingVineSpitB as ISerializableJsonObject, data, ref offset, BaseOffset, 407 * 4, StoredObjectTable);
            AddObject(BitingVineCast as ISerializableJsonObject, data, ref offset, BaseOffset, 408 * 4, StoredObjectTable);
            AddObject(BitingVineAppear as ISerializableJsonObject, data, ref offset, BaseOffset, 409 * 4, StoredObjectTable);
            AddObject(BitingVineDisappear as ISerializableJsonObject, data, ref offset, BaseOffset, 410 * 4, StoredObjectTable);
            AddObject(TrollClubA as ISerializableJsonObject, data, ref offset, BaseOffset, 411 * 4, StoredObjectTable);
            AddObject(TrollClubB as ISerializableJsonObject, data, ref offset, BaseOffset, 412 * 4, StoredObjectTable);
            AddObject(TrollKnockdown as ISerializableJsonObject, data, ref offset, BaseOffset, 413 * 4, StoredObjectTable);
            AddObject(OgreClubA as ISerializableJsonObject, data, ref offset, BaseOffset, 414 * 4, StoredObjectTable);
            AddObject(OgreClubB as ISerializableJsonObject, data, ref offset, BaseOffset, 415 * 4, StoredObjectTable);
            AddObject(OgreThrow as ISerializableJsonObject, data, ref offset, BaseOffset, 416 * 4, StoredObjectTable);
            AddObject(OgreStun as ISerializableJsonObject, data, ref offset, BaseOffset, 417 * 4, StoredObjectTable);
            AddObject(FaeSwordA as ISerializableJsonObject, data, ref offset, BaseOffset, 418 * 4, StoredObjectTable);
            AddObject(FaeSwordB as ISerializableJsonObject, data, ref offset, BaseOffset, 419 * 4, StoredObjectTable);
            AddObject(FaeSwordKill as ISerializableJsonObject, data, ref offset, BaseOffset, 420 * 4, StoredObjectTable);
            AddObject(DementiaPuckCurse as ISerializableJsonObject, data, ref offset, BaseOffset, 421 * 4, StoredObjectTable);
            AddObject(FaeLightningSmiteHidden as ISerializableJsonObject, data, ref offset, BaseOffset, 422 * 4, StoredObjectTable);
            AddObject(NecroSpark as ISerializableJsonObject, data, ref offset, BaseOffset, 423 * 4, StoredObjectTable);
            AddObject(NecroDarknessWave as ISerializableJsonObject, data, ref offset, BaseOffset, 424 * 4, StoredObjectTable);
            AddObject(NecroPainBubble as ISerializableJsonObject, data, ref offset, BaseOffset, 425 * 4, StoredObjectTable);
            AddObject(NecroSparkPerching as ISerializableJsonObject, data, ref offset, BaseOffset, 426 * 4, StoredObjectTable);
            AddObject(NecroDeathsHold as ISerializableJsonObject, data, ref offset, BaseOffset, 427 * 4, StoredObjectTable);
            AddObject(DroachBiteA as ISerializableJsonObject, data, ref offset, BaseOffset, 428 * 4, StoredObjectTable);
            AddObject(DroachBiteB as ISerializableJsonObject, data, ref offset, BaseOffset, 429 * 4, StoredObjectTable);
            AddObject(DroachFireball as ISerializableJsonObject, data, ref offset, BaseOffset, 430 * 4, StoredObjectTable);
            AddObject(DroachFireballPerching as ISerializableJsonObject, data, ref offset, BaseOffset, 431 * 4, StoredObjectTable);
            AddObject(DroachBreatheFire as ISerializableJsonObject, data, ref offset, BaseOffset, 432 * 4, StoredObjectTable);
            AddObject(DroachLightning as ISerializableJsonObject, data, ref offset, BaseOffset, 433 * 4, StoredObjectTable);
            AddObject(DroachLightningPerching as ISerializableJsonObject, data, ref offset, BaseOffset, 434 * 4, StoredObjectTable);
            AddObject(DroachShockingKnockback as ISerializableJsonObject, data, ref offset, BaseOffset, 435 * 4, StoredObjectTable);
            AddObject(BasiliskClawA as ISerializableJsonObject, data, ref offset, BaseOffset, 436 * 4, StoredObjectTable);
            AddObject(BasiliskClawB as ISerializableJsonObject, data, ref offset, BaseOffset, 437 * 4, StoredObjectTable);
            AddObject(BasiliskToxicBite as ISerializableJsonObject, data, ref offset, BaseOffset, 438 * 4, StoredObjectTable);
            AddObject(BasiliskDebuff as ISerializableJsonObject, data, ref offset, BaseOffset, 439 * 4, StoredObjectTable);
            AddObject(BasiliskCastPerching as ISerializableJsonObject, data, ref offset, BaseOffset, 440 * 4, StoredObjectTable);
            AddObject(CultistArrow1 as ISerializableJsonObject, data, ref offset, BaseOffset, 441 * 4, StoredObjectTable);
            AddObject(CultistArrow2 as ISerializableJsonObject, data, ref offset, BaseOffset, 442 * 4, StoredObjectTable);
            AddObject(CultistOmegaArrow as ISerializableJsonObject, data, ref offset, BaseOffset, 443 * 4, StoredObjectTable);
            AddObject(CultistSword1 as ISerializableJsonObject, data, ref offset, BaseOffset, 444 * 4, StoredObjectTable);
            AddObject(CultistSword2 as ISerializableJsonObject, data, ref offset, BaseOffset, 445 * 4, StoredObjectTable);
            AddObject(CultistSwordStun as ISerializableJsonObject, data, ref offset, BaseOffset, 446 * 4, StoredObjectTable);
            AddObject(BossMegaSword1 as ISerializableJsonObject, data, ref offset, BaseOffset, 447 * 4, StoredObjectTable);
            AddObject(BossMegaSword2 as ISerializableJsonObject, data, ref offset, BaseOffset, 448 * 4, StoredObjectTable);
            AddObject(SedgewickMegaSwordAngry as ISerializableJsonObject, data, ref offset, BaseOffset, 449 * 4, StoredObjectTable);
            AddObject(BossMegaHammer as ISerializableJsonObject, data, ref offset, BaseOffset, 450 * 4, StoredObjectTable);
            AddObject(BossMegaHammer2 as ISerializableJsonObject, data, ref offset, BaseOffset, 451 * 4, StoredObjectTable);
            AddObject(BossMegaRageHammer as ISerializableJsonObject, data, ref offset, BaseOffset, 452 * 4, StoredObjectTable);
            AddObject(ClaudiaTundraSpikes as ISerializableJsonObject, data, ref offset, BaseOffset, 453 * 4, StoredObjectTable);
            AddObject(ClaudiaIceSpear as ISerializableJsonObject, data, ref offset, BaseOffset, 454 * 4, StoredObjectTable);
            AddObject(ClaudiaBlizzard as ISerializableJsonObject, data, ref offset, BaseOffset, 455 * 4, StoredObjectTable);
            AddObject(BigGolemHitA as ISerializableJsonObject, data, ref offset, BaseOffset, 456 * 4, StoredObjectTable);
            AddObject(BigGolemHitB as ISerializableJsonObject, data, ref offset, BaseOffset, 457 * 4, StoredObjectTable);
            AddObject(BigGolemFlingBoss as ISerializableJsonObject, data, ref offset, BaseOffset, 458 * 4, StoredObjectTable);
            AddObject(BigGolemPerchFix as ISerializableJsonObject, data, ref offset, BaseOffset, 459 * 4, StoredObjectTable);
            AddObject(BigGolemFlingBoss2 as ISerializableJsonObject, data, ref offset, BaseOffset, 460 * 4, StoredObjectTable);
            AddObject(BigGolemSummonFireSnake as ISerializableJsonObject, data, ref offset, BaseOffset, 461 * 4, StoredObjectTable);
            AddObject(BigGolemHitB_NoDisable as ISerializableJsonObject, data, ref offset, BaseOffset, 462 * 4, StoredObjectTable);
            AddObject(BigGolemHitA_NoDisable as ISerializableJsonObject, data, ref offset, BaseOffset, 463 * 4, StoredObjectTable);
            AddObject(BigGolemFling as ISerializableJsonObject, data, ref offset, BaseOffset, 464 * 4, StoredObjectTable);
            AddObject(GhoulClawA as ISerializableJsonObject, data, ref offset, BaseOffset, 465 * 4, StoredObjectTable);
            AddObject(GhoulClawB as ISerializableJsonObject, data, ref offset, BaseOffset, 466 * 4, StoredObjectTable);
            AddObject(GhoulSelfBuff as ISerializableJsonObject, data, ref offset, BaseOffset, 467 * 4, StoredObjectTable);
            AddObject(GhoulHammerA as ISerializableJsonObject, data, ref offset, BaseOffset, 468 * 4, StoredObjectTable);
            AddObject(GhoulHammerB as ISerializableJsonObject, data, ref offset, BaseOffset, 469 * 4, StoredObjectTable);
            AddObject(DragonWormSpitElectricity as ISerializableJsonObject, data, ref offset, BaseOffset, 470 * 4, StoredObjectTable);
            AddObject(DragonWormBite as ISerializableJsonObject, data, ref offset, BaseOffset, 471 * 4, StoredObjectTable);
            AddObject(DragonWormSmack as ISerializableJsonObject, data, ref offset, BaseOffset, 472 * 4, StoredObjectTable);
            AddObject(DragonWormRage as ISerializableJsonObject, data, ref offset, BaseOffset, 473 * 4, StoredObjectTable);
            AddObject(DragonWormEscape as ISerializableJsonObject, data, ref offset, BaseOffset, 474 * 4, StoredObjectTable);
            AddObject(DragonWormSpitFire as ISerializableJsonObject, data, ref offset, BaseOffset, 475 * 4, StoredObjectTable);
            AddObject(ColdSphereBurst as ISerializableJsonObject, data, ref offset, BaseOffset, 476 * 4, StoredObjectTable);
            AddObject(ColdSphereFreezeBurst as ISerializableJsonObject, data, ref offset, BaseOffset, 477 * 4, StoredObjectTable);
            AddObject(ManticoreBite as ISerializableJsonObject, data, ref offset, BaseOffset, 478 * 4, StoredObjectTable);
            AddObject(ManticoreClaw as ISerializableJsonObject, data, ref offset, BaseOffset, 479 * 4, StoredObjectTable);
            AddObject(ManticoreSting1 as ISerializableJsonObject, data, ref offset, BaseOffset, 480 * 4, StoredObjectTable);
            AddObject(Manticoresting2 as ISerializableJsonObject, data, ref offset, BaseOffset, 481 * 4, StoredObjectTable);
            AddObject(RakStaffHit as ISerializableJsonObject, data, ref offset, BaseOffset, 482 * 4, StoredObjectTable);
            AddObject(RakStaffPin as ISerializableJsonObject, data, ref offset, BaseOffset, 483 * 4, StoredObjectTable);
            AddObject(RakStaffBlock as ISerializableJsonObject, data, ref offset, BaseOffset, 484 * 4, StoredObjectTable);
            AddObject(RakStaffHeavy as ISerializableJsonObject, data, ref offset, BaseOffset, 485 * 4, StoredObjectTable);
            AddObject(RakSlash as ISerializableJsonObject, data, ref offset, BaseOffset, 486 * 4, StoredObjectTable);
            AddObject(RakKnee as ISerializableJsonObject, data, ref offset, BaseOffset, 487 * 4, StoredObjectTable);
            AddObject(RakKick as ISerializableJsonObject, data, ref offset, BaseOffset, 488 * 4, StoredObjectTable);
            AddObject(RakBarrage as ISerializableJsonObject, data, ref offset, BaseOffset, 489 * 4, StoredObjectTable);
            AddObject(RakSwordSlash as ISerializableJsonObject, data, ref offset, BaseOffset, 490 * 4, StoredObjectTable);
            AddObject(RakHackingBlade as ISerializableJsonObject, data, ref offset, BaseOffset, 491 * 4, StoredObjectTable);
            AddObject(RakDecapitate as ISerializableJsonObject, data, ref offset, BaseOffset, 492 * 4, StoredObjectTable);
            AddObject(RakFireball as ISerializableJsonObject, data, ref offset, BaseOffset, 493 * 4, StoredObjectTable);
            AddObject(RakBreatheFire as ISerializableJsonObject, data, ref offset, BaseOffset, 494 * 4, StoredObjectTable);
            AddObject(RakRingOfFire as ISerializableJsonObject, data, ref offset, BaseOffset, 495 * 4, StoredObjectTable);
            AddObject(RakToxinBomb as ISerializableJsonObject, data, ref offset, BaseOffset, 496 * 4, StoredObjectTable);
            AddObject(RakAcidBomb as ISerializableJsonObject, data, ref offset, BaseOffset, 497 * 4, StoredObjectTable);
            AddObject(RakHealingMist as ISerializableJsonObject, data, ref offset, BaseOffset, 498 * 4, StoredObjectTable);
            AddObject(RakBasicShot as ISerializableJsonObject, data, ref offset, BaseOffset, 499 * 4, StoredObjectTable);
            AddObject(RakHookShot as ISerializableJsonObject, data, ref offset, BaseOffset, 500 * 4, StoredObjectTable);
            AddObject(RakBowBash as ISerializableJsonObject, data, ref offset, BaseOffset, 501 * 4, StoredObjectTable);
            AddObject(RakAimedShot as ISerializableJsonObject, data, ref offset, BaseOffset, 502 * 4, StoredObjectTable);
            AddObject(RakPoisonArrow as ISerializableJsonObject, data, ref offset, BaseOffset, 503 * 4, StoredObjectTable);
            AddObject(RakMindreave as ISerializableJsonObject, data, ref offset, BaseOffset, 504 * 4, StoredObjectTable);
            AddObject(RakPainBubble as ISerializableJsonObject, data, ref offset, BaseOffset, 505 * 4, StoredObjectTable);
            AddObject(RakPanicCharge as ISerializableJsonObject, data, ref offset, BaseOffset, 506 * 4, StoredObjectTable);
            AddObject(RakRevitalize as ISerializableJsonObject, data, ref offset, BaseOffset, 507 * 4, StoredObjectTable);
            AddObject(RakReconstruct as ISerializableJsonObject, data, ref offset, BaseOffset, 508 * 4, StoredObjectTable);
            AddObject(RakBossSlow as ISerializableJsonObject, data, ref offset, BaseOffset, 509 * 4, StoredObjectTable);
            AddObject(RakBossPerchSlow as ISerializableJsonObject, data, ref offset, BaseOffset, 510 * 4, StoredObjectTable);
            AddObject(FlapSkullBite as ISerializableJsonObject, data, ref offset, BaseOffset, 511 * 4, StoredObjectTable);
            AddObject(FlapSkullBigBite as ISerializableJsonObject, data, ref offset, BaseOffset, 512 * 4, StoredObjectTable);
            AddObject(MinotaurClub as ISerializableJsonObject, data, ref offset, BaseOffset, 513 * 4, StoredObjectTable);
            AddObject(MinotaurRageClub as ISerializableJsonObject, data, ref offset, BaseOffset, 514 * 4, StoredObjectTable);
            AddObject(MinotaurBoulder as ISerializableJsonObject, data, ref offset, BaseOffset, 515 * 4, StoredObjectTable);
            AddObject(MinotaurBossRageClub as ISerializableJsonObject, data, ref offset, BaseOffset, 516 * 4, StoredObjectTable);
            AddObject(CockatricePeck as ISerializableJsonObject, data, ref offset, BaseOffset, 517 * 4, StoredObjectTable);
            AddObject(CockatriceTailWhip as ISerializableJsonObject, data, ref offset, BaseOffset, 518 * 4, StoredObjectTable);
            AddObject(CockatriceParalyze as ISerializableJsonObject, data, ref offset, BaseOffset, 519 * 4, StoredObjectTable);
            AddObject(GiantBeetleBite as ISerializableJsonObject, data, ref offset, BaseOffset, 520 * 4, StoredObjectTable);
            AddObject(GiantBeetleInject as ISerializableJsonObject, data, ref offset, BaseOffset, 521 * 4, StoredObjectTable);
            AddObject(GiantBeetleBoulderSpit as ISerializableJsonObject, data, ref offset, BaseOffset, 522 * 4, StoredObjectTable);
            AddObject(BatIllusionSlashA as ISerializableJsonObject, data, ref offset, BaseOffset, 523 * 4, StoredObjectTable);
            AddObject(BatIllusionSlashB as ISerializableJsonObject, data, ref offset, BaseOffset, 524 * 4, StoredObjectTable);
            AddObject(BatIllusionBite as ISerializableJsonObject, data, ref offset, BaseOffset, 525 * 4, StoredObjectTable);
            AddObject(GiantBatSlashA as ISerializableJsonObject, data, ref offset, BaseOffset, 526 * 4, StoredObjectTable);
            AddObject(GiantBatSlashB as ISerializableJsonObject, data, ref offset, BaseOffset, 527 * 4, StoredObjectTable);
            AddObject(GiantBatBite as ISerializableJsonObject, data, ref offset, BaseOffset, 528 * 4, StoredObjectTable);
            AddObject(HagAgingTouch as ISerializableJsonObject, data, ref offset, BaseOffset, 529 * 4, StoredObjectTable);
            AddObject(HagAgingScream as ISerializableJsonObject, data, ref offset, BaseOffset, 530 * 4, StoredObjectTable);
            AddObject(TriffidClawA as ISerializableJsonObject, data, ref offset, BaseOffset, 531 * 4, StoredObjectTable);
            AddObject(TriffidClawB as ISerializableJsonObject, data, ref offset, BaseOffset, 532 * 4, StoredObjectTable);
            AddObject(TriffidTongue as ISerializableJsonObject, data, ref offset, BaseOffset, 533 * 4, StoredObjectTable);
            AddObject(TriffidSpore as ISerializableJsonObject, data, ref offset, BaseOffset, 534 * 4, StoredObjectTable);
            AddObject(TriffidShot as ISerializableJsonObject, data, ref offset, BaseOffset, 535 * 4, StoredObjectTable);
            AddObject(TriffidTongueElite as ISerializableJsonObject, data, ref offset, BaseOffset, 536 * 4, StoredObjectTable);
            AddObject(GiantScorpionClawA as ISerializableJsonObject, data, ref offset, BaseOffset, 537 * 4, StoredObjectTable);
            AddObject(GiantScorpionClawB as ISerializableJsonObject, data, ref offset, BaseOffset, 538 * 4, StoredObjectTable);
            AddObject(GiantScorpionSting as ISerializableJsonObject, data, ref offset, BaseOffset, 539 * 4, StoredObjectTable);
            AddObject(KrakenBeak as ISerializableJsonObject, data, ref offset, BaseOffset, 540 * 4, StoredObjectTable);
            AddObject(KrakenSlam as ISerializableJsonObject, data, ref offset, BaseOffset, 541 * 4, StoredObjectTable);
            AddObject(KrakenRage as ISerializableJsonObject, data, ref offset, BaseOffset, 542 * 4, StoredObjectTable);
            AddObject(KrakenBabyBeak as ISerializableJsonObject, data, ref offset, BaseOffset, 543 * 4, StoredObjectTable);
            AddObject(KrakenBabySlam as ISerializableJsonObject, data, ref offset, BaseOffset, 544 * 4, StoredObjectTable);
            AddObject(KrakenBabyRage as ISerializableJsonObject, data, ref offset, BaseOffset, 545 * 4, StoredObjectTable);
            AddObject(RanalonHit as ISerializableJsonObject, data, ref offset, BaseOffset, 546 * 4, StoredObjectTable);
            AddObject(RanalonHitB as ISerializableJsonObject, data, ref offset, BaseOffset, 547 * 4, StoredObjectTable);
            AddObject(RanalonKick as ISerializableJsonObject, data, ref offset, BaseOffset, 548 * 4, StoredObjectTable);
            AddObject(RanalonTongue as ISerializableJsonObject, data, ref offset, BaseOffset, 549 * 4, StoredObjectTable);
            AddObject(RanalonZap as ISerializableJsonObject, data, ref offset, BaseOffset, 550 * 4, StoredObjectTable);
            AddObject(RanalonZapB as ISerializableJsonObject, data, ref offset, BaseOffset, 551 * 4, StoredObjectTable);
            AddObject(RanalonHeal as ISerializableJsonObject, data, ref offset, BaseOffset, 552 * 4, StoredObjectTable);
            AddObject(RanalonRoot as ISerializableJsonObject, data, ref offset, BaseOffset, 553 * 4, StoredObjectTable);
            AddObject(RanalonSelfBuff as ISerializableJsonObject, data, ref offset, BaseOffset, 554 * 4, StoredObjectTable);
            AddObject(RanalonSelfBuffElite as ISerializableJsonObject, data, ref offset, BaseOffset, 555 * 4, StoredObjectTable);
            AddObject(RanalonGuardianStab as ISerializableJsonObject, data, ref offset, BaseOffset, 556 * 4, StoredObjectTable);
            AddObject(RanalonGuardianStabB as ISerializableJsonObject, data, ref offset, BaseOffset, 557 * 4, StoredObjectTable);
            AddObject(RanalonGuardianBite as ISerializableJsonObject, data, ref offset, BaseOffset, 558 * 4, StoredObjectTable);
            AddObject(RanalonGuardianBlind as ISerializableJsonObject, data, ref offset, BaseOffset, 559 * 4, StoredObjectTable);
            AddObject(RanalonDoctrineKeeperStab as ISerializableJsonObject, data, ref offset, BaseOffset, 560 * 4, StoredObjectTable);
            AddObject(RanalonDoctrineKeeperBlind as ISerializableJsonObject, data, ref offset, BaseOffset, 561 * 4, StoredObjectTable);
            AddObject(BarghestBiteA as ISerializableJsonObject, data, ref offset, BaseOffset, 562 * 4, StoredObjectTable);
            AddObject(BarghestBiteB as ISerializableJsonObject, data, ref offset, BaseOffset, 563 * 4, StoredObjectTable);
            AddObject(BarghestDebuff as ISerializableJsonObject, data, ref offset, BaseOffset, 564 * 4, StoredObjectTable);
            AddObject(WorghestDebuff as ISerializableJsonObject, data, ref offset, BaseOffset, 565 * 4, StoredObjectTable);
            AddObject(BallistaFire as ISerializableJsonObject, data, ref offset, BaseOffset, 566 * 4, StoredObjectTable);
            AddObject(BallistaFire_Long as ISerializableJsonObject, data, ref offset, BaseOffset, 567 * 4, StoredObjectTable);
            AddObject(GargoyleSlamA as ISerializableJsonObject, data, ref offset, BaseOffset, 568 * 4, StoredObjectTable);
            AddObject(GargoyleSlamB as ISerializableJsonObject, data, ref offset, BaseOffset, 569 * 4, StoredObjectTable);
            AddObject(GargoyleStun as ISerializableJsonObject, data, ref offset, BaseOffset, 570 * 4, StoredObjectTable);
            AddObject(GargoyleBossStun as ISerializableJsonObject, data, ref offset, BaseOffset, 571 * 4, StoredObjectTable);
            AddObject(ScrayBite as ISerializableJsonObject, data, ref offset, BaseOffset, 572 * 4, StoredObjectTable);
            AddObject(ScrayStab as ISerializableJsonObject, data, ref offset, BaseOffset, 573 * 4, StoredObjectTable);
            AddObject(HippoBite as ISerializableJsonObject, data, ref offset, BaseOffset, 574 * 4, StoredObjectTable);
            AddObject(HippoBiteAndHeal1 as ISerializableJsonObject, data, ref offset, BaseOffset, 575 * 4, StoredObjectTable);
            AddObject(BigCatClaw_Pet as ISerializableJsonObject, data, ref offset, BaseOffset, 576 * 4, StoredObjectTable);
            AddObject(BigCatPounce_Pet as ISerializableJsonObject, data, ref offset, BaseOffset, 577 * 4, StoredObjectTable);
            AddObject(BigCatKill_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 578 * 4, StoredObjectTable);
            AddObject(BigCatKill_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 579 * 4, StoredObjectTable);
            AddObject(BigCatKill_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 580 * 4, StoredObjectTable);
            AddObject(BigCatKill_Pet4 as ISerializableJsonObject, data, ref offset, BaseOffset, 581 * 4, StoredObjectTable);
            AddObject(BigCatKill_Pet5 as ISerializableJsonObject, data, ref offset, BaseOffset, 582 * 4, StoredObjectTable);
            AddObject(BigCatKill_Pet6 as ISerializableJsonObject, data, ref offset, BaseOffset, 583 * 4, StoredObjectTable);
            AddObject(BigCatUltraKill_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 584 * 4, StoredObjectTable);
            AddObject(BigCatUltraKill_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 585 * 4, StoredObjectTable);
            AddObject(BigCatUltraKill_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 586 * 4, StoredObjectTable);
            AddObject(BigCatUltraKill_Pet4 as ISerializableJsonObject, data, ref offset, BaseOffset, 587 * 4, StoredObjectTable);
            AddObject(BigCatUltraKill_Pet5 as ISerializableJsonObject, data, ref offset, BaseOffset, 588 * 4, StoredObjectTable);
            AddObject(BigCatUltraKill_Pet6 as ISerializableJsonObject, data, ref offset, BaseOffset, 589 * 4, StoredObjectTable);
            AddObject(BigCatRoot_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 590 * 4, StoredObjectTable);
            AddObject(BigCatRoot_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 591 * 4, StoredObjectTable);
            AddObject(BigCatRoot_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 592 * 4, StoredObjectTable);
            AddObject(BigCatRoot_Pet4 as ISerializableJsonObject, data, ref offset, BaseOffset, 593 * 4, StoredObjectTable);
            AddObject(BigCatVuln_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 594 * 4, StoredObjectTable);
            AddObject(BigCatVuln_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 595 * 4, StoredObjectTable);
            AddObject(BigCatVuln_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 596 * 4, StoredObjectTable);
            AddObject(BigCatVuln_Pet4 as ISerializableJsonObject, data, ref offset, BaseOffset, 597 * 4, StoredObjectTable);
            AddObject(BigCatHeal_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 598 * 4, StoredObjectTable);
            AddObject(BigCatHeal_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 599 * 4, StoredObjectTable);
            AddObject(BigCatHeal_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 600 * 4, StoredObjectTable);
            AddObject(BigCatHeal_Pet4 as ISerializableJsonObject, data, ref offset, BaseOffset, 601 * 4, StoredObjectTable);
            AddObject(BigCatHeal_Pet5 as ISerializableJsonObject, data, ref offset, BaseOffset, 602 * 4, StoredObjectTable);
            AddObject(BigCatHeal_Pet6 as ISerializableJsonObject, data, ref offset, BaseOffset, 603 * 4, StoredObjectTable);
            AddObject(GrimalkinPuncture_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 604 * 4, StoredObjectTable);
            AddObject(GrimalkinPuncture_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 605 * 4, StoredObjectTable);
            AddObject(GrimalkinPuncture_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 606 * 4, StoredObjectTable);
            AddObject(GrimalkinPuncture_Pet4 as ISerializableJsonObject, data, ref offset, BaseOffset, 607 * 4, StoredObjectTable);
            AddObject(GrimalkinPuncture_Pet5 as ISerializableJsonObject, data, ref offset, BaseOffset, 608 * 4, StoredObjectTable);
            AddObject(GrimalkinPuncture_Pet6 as ISerializableJsonObject, data, ref offset, BaseOffset, 609 * 4, StoredObjectTable);
            AddObject(GrimalkinFlee_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 610 * 4, StoredObjectTable);
            AddObject(GrimalkinFlee_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 611 * 4, StoredObjectTable);
            AddObject(GrimalkinFlee_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 612 * 4, StoredObjectTable);
            AddObject(RatBite_Pet as ISerializableJsonObject, data, ref offset, BaseOffset, 613 * 4, StoredObjectTable);
            AddObject(RatClaw_Pet as ISerializableJsonObject, data, ref offset, BaseOffset, 614 * 4, StoredObjectTable);
            AddObject(RatDeRage_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 615 * 4, StoredObjectTable);
            AddObject(RatDeRage_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 616 * 4, StoredObjectTable);
            AddObject(RatDeRage_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 617 * 4, StoredObjectTable);
            AddObject(RatDeRage_Pet4 as ISerializableJsonObject, data, ref offset, BaseOffset, 618 * 4, StoredObjectTable);
            AddObject(RatDeRage_Pet5 as ISerializableJsonObject, data, ref offset, BaseOffset, 619 * 4, StoredObjectTable);
            AddObject(RatDeRage_Pet6 as ISerializableJsonObject, data, ref offset, BaseOffset, 620 * 4, StoredObjectTable);
            AddObject(RatHeal_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 621 * 4, StoredObjectTable);
            AddObject(RatHeal_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 622 * 4, StoredObjectTable);
            AddObject(RatHeal_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 623 * 4, StoredObjectTable);
            AddObject(RatHeal_Pet4 as ISerializableJsonObject, data, ref offset, BaseOffset, 624 * 4, StoredObjectTable);
            AddObject(RatHeal_Pet5 as ISerializableJsonObject, data, ref offset, BaseOffset, 625 * 4, StoredObjectTable);
            AddObject(RatHeal_Pet6 as ISerializableJsonObject, data, ref offset, BaseOffset, 626 * 4, StoredObjectTable);
            AddObject(RatVuln_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 627 * 4, StoredObjectTable);
            AddObject(RatVuln_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 628 * 4, StoredObjectTable);
            AddObject(RatVuln_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 629 * 4, StoredObjectTable);
            AddObject(RatVuln_Pet4 as ISerializableJsonObject, data, ref offset, BaseOffset, 630 * 4, StoredObjectTable);
            AddObject(FireRatBite_Pet as ISerializableJsonObject, data, ref offset, BaseOffset, 631 * 4, StoredObjectTable);
            AddObject(FireRatClaw_Pet as ISerializableJsonObject, data, ref offset, BaseOffset, 632 * 4, StoredObjectTable);
            AddObject(RatBurn_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 633 * 4, StoredObjectTable);
            AddObject(RatBurn_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 634 * 4, StoredObjectTable);
            AddObject(RatBurn_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 635 * 4, StoredObjectTable);
            AddObject(RatBurn_Pet4 as ISerializableJsonObject, data, ref offset, BaseOffset, 636 * 4, StoredObjectTable);
            AddObject(RatBurn_Pet5 as ISerializableJsonObject, data, ref offset, BaseOffset, 637 * 4, StoredObjectTable);
            AddObject(RatBurn_Pet6 as ISerializableJsonObject, data, ref offset, BaseOffset, 638 * 4, StoredObjectTable);
            AddObject(BearBite_Pet as ISerializableJsonObject, data, ref offset, BaseOffset, 639 * 4, StoredObjectTable);
            AddObject(BearClaw_Pet as ISerializableJsonObject, data, ref offset, BaseOffset, 640 * 4, StoredObjectTable);
            AddObject(BearTaunt_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 641 * 4, StoredObjectTable);
            AddObject(BearTaunt_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 642 * 4, StoredObjectTable);
            AddObject(BearTaunt_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 643 * 4, StoredObjectTable);
            AddObject(BearTaunt_Pet4 as ISerializableJsonObject, data, ref offset, BaseOffset, 644 * 4, StoredObjectTable);
            AddObject(BearTaunt_Pet5 as ISerializableJsonObject, data, ref offset, BaseOffset, 645 * 4, StoredObjectTable);
            AddObject(BearTaunt_Pet6 as ISerializableJsonObject, data, ref offset, BaseOffset, 646 * 4, StoredObjectTable);
            AddObject(BearStun_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 647 * 4, StoredObjectTable);
            AddObject(BearStun_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 648 * 4, StoredObjectTable);
            AddObject(BearStun_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 649 * 4, StoredObjectTable);
            AddObject(BearStun_Pet4 as ISerializableJsonObject, data, ref offset, BaseOffset, 650 * 4, StoredObjectTable);
            AddObject(BearWarmth_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 651 * 4, StoredObjectTable);
            AddObject(BearWarmth_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 652 * 4, StoredObjectTable);
            AddObject(BearWarmth_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 653 * 4, StoredObjectTable);
            AddObject(BearWarmth_Pet4 as ISerializableJsonObject, data, ref offset, BaseOffset, 654 * 4, StoredObjectTable);
            AddObject(BearWarmth_Pet5 as ISerializableJsonObject, data, ref offset, BaseOffset, 655 * 4, StoredObjectTable);
            AddObject(BearWarmth_Pet6 as ISerializableJsonObject, data, ref offset, BaseOffset, 656 * 4, StoredObjectTable);
            AddObject(BearSelfHeal_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 657 * 4, StoredObjectTable);
            AddObject(BearSelfHeal_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 658 * 4, StoredObjectTable);
            AddObject(BearSelfHeal_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 659 * 4, StoredObjectTable);
            AddObject(BearSelfHeal_Pet4 as ISerializableJsonObject, data, ref offset, BaseOffset, 660 * 4, StoredObjectTable);
            AddObject(BearSelfHeal_Pet5 as ISerializableJsonObject, data, ref offset, BaseOffset, 661 * 4, StoredObjectTable);
            AddObject(BearSelfHeal_Pet6 as ISerializableJsonObject, data, ref offset, BaseOffset, 662 * 4, StoredObjectTable);
            AddObject(BearUltra_Pet1 as ISerializableJsonObject, data, ref offset, BaseOffset, 663 * 4, StoredObjectTable);
            AddObject(BearUltra_Pet2 as ISerializableJsonObject, data, ref offset, BaseOffset, 664 * 4, StoredObjectTable);
            AddObject(BearUltra_Pet3 as ISerializableJsonObject, data, ref offset, BaseOffset, 665 * 4, StoredObjectTable);
            AddObject(BearUltra_Pet4 as ISerializableJsonObject, data, ref offset, BaseOffset, 666 * 4, StoredObjectTable);
            AddObject(BearUltra_Pet5 as ISerializableJsonObject, data, ref offset, BaseOffset, 667 * 4, StoredObjectTable);
            AddObject(BearUltra_Pet6 as ISerializableJsonObject, data, ref offset, BaseOffset, 668 * 4, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 669 * 4, null, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
