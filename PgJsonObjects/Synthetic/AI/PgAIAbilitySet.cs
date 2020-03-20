using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAIAbilitySet : GenericPgObject<PgAIAbilitySet>, IPgAIAbilitySet
    {
        public PgAIAbilitySet(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAIAbilitySet CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAIAbilitySet CreateNew(byte[] data, ref int offset)
        {
            return new PgAIAbilitySet(data, ref offset);
        }

        private PgAIAbility[] Abilities = new PgAIAbility[816];
        private IPgAIAbility GetAbilityObject(int index) { return GetObject<PgAIAbility>(8 + (index * 4), ref Abilities[index], PgAIAbility.CreateNew); }

        public override string Key { get { return GetString(0); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(4, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public IPgAIAbility AnimalBite { get { return GetAbilityObject(0); } }
        public IPgAIAbility AnimalClaw { get { return GetAbilityObject(1); } }
        public IPgAIAbility AnimalOmegaBite { get { return GetAbilityObject(2); } }
        public IPgAIAbility AnimalOmegaBite2 { get { return GetAbilityObject(3); } }
        public IPgAIAbility AnimalHoofFrontKick { get { return GetAbilityObject(4); } }
        public IPgAIAbility AnimalHoofRageKick { get { return GetAbilityObject(5); } }
        public IPgAIAbility AnimalHoofRageKick2 { get { return GetAbilityObject(6); } }
        public IPgAIAbility AnimalHoofFieryFrontKick { get { return GetAbilityObject(7); } }
        public IPgAIAbility AnimalHoofFieryFrontKick2 { get { return GetAbilityObject(8); } }
        public IPgAIAbility ElectricPigHitAndRun { get { return GetAbilityObject(9); } }
        public IPgAIAbility ElectricPigStun { get { return GetAbilityObject(10); } }
        public IPgAIAbility ElectricPigAoEStun { get { return GetAbilityObject(11); } }
        public IPgAIAbility LamiaMindControl { get { return GetAbilityObject(12); } }
        public IPgAIAbility LamiaRage { get { return GetAbilityObject(13); } }
        public IPgAIAbility SlimeKick { get { return GetAbilityObject(14); } }
        public IPgAIAbility SlimeKickB { get { return GetAbilityObject(15); } }
        public IPgAIAbility SlimeBite { get { return GetAbilityObject(16); } }
        public IPgAIAbility SlimeBiteB { get { return GetAbilityObject(17); } }
        public IPgAIAbility Slime_SummonSlime { get { return GetAbilityObject(18); } }
        public IPgAIAbility SlimeSpit { get { return GetAbilityObject(19); } }
        public IPgAIAbility SlimeSuperSpit { get { return GetAbilityObject(20); } }
        public IPgAIAbility IceSlimeKick { get { return GetAbilityObject(21); } }
        public IPgAIAbility IceSlimeKickB { get { return GetAbilityObject(22); } }
        public IPgAIAbility IceSlimeBite { get { return GetAbilityObject(23); } }
        public IPgAIAbility IceSlimeBiteB { get { return GetAbilityObject(24); } }
        public IPgAIAbility BossSlimeKick { get { return GetAbilityObject(25); } }
        public IPgAIAbility BossSlimeKickB { get { return GetAbilityObject(26); } }
        public IPgAIAbility BossSlime_SummonSlime1 { get { return GetAbilityObject(27); } }
        public IPgAIAbility BossSlimeKick2 { get { return GetAbilityObject(28); } }
        public IPgAIAbility BossSlimeKick2B { get { return GetAbilityObject(29); } }
        public IPgAIAbility BossSlime_SummonSlime4Elite { get { return GetAbilityObject(30); } }
        public IPgAIAbility AnimalHeal { get { return GetAbilityObject(31); } }
        public IPgAIAbility AnimalHeal2 { get { return GetAbilityObject(32); } }
        public IPgAIAbility AnimalHeal3 { get { return GetAbilityObject(33); } }
        public IPgAIAbility IceCockPeck { get { return GetAbilityObject(34); } }
        public IPgAIAbility IceCockFreeze { get { return GetAbilityObject(35); } }
        public IPgAIAbility NightmareHoof { get { return GetAbilityObject(36); } }
        public IPgAIAbility NightmareDarknessBomb { get { return GetAbilityObject(37); } }
        public IPgAIAbility CiervosNightmareHoof { get { return GetAbilityObject(38); } }
        public IPgAIAbility CiervosDarknessBomb { get { return GetAbilityObject(39); } }
        public IPgAIAbility Myconian_Bash { get { return GetAbilityObject(40); } }
        public IPgAIAbility Myconian_Mindspores { get { return GetAbilityObject(41); } }
        public IPgAIAbility Myconian_Drain { get { return GetAbilityObject(42); } }
        public IPgAIAbility Myconian_BossBash { get { return GetAbilityObject(43); } }
        public IPgAIAbility Myconian_Mindspores_Permanent { get { return GetAbilityObject(44); } }
        public IPgAIAbility Myconian_TidalCurse { get { return GetAbilityObject(45); } }
        public IPgAIAbility Myconian_Shock { get { return GetAbilityObject(46); } }
        public IPgAIAbility AlienDog_Punch { get { return GetAbilityObject(47); } }
        public IPgAIAbility AlienDog_Punch2 { get { return GetAbilityObject(48); } }
        public IPgAIAbility AlienDog_RagePunch { get { return GetAbilityObject(49); } }
        public IPgAIAbility MushroomMonster_Bite { get { return GetAbilityObject(50); } }
        public IPgAIAbility MushroomMonster_Spit1 { get { return GetAbilityObject(51); } }
        public IPgAIAbility MushroomMonster_Spit2 { get { return GetAbilityObject(52); } }
        public IPgAIAbility MushroomMonster_SummonMushroomSpawn1 { get { return GetAbilityObject(53); } }
        public IPgAIAbility MushroomMonster_SummonMushroomSpawn2 { get { return GetAbilityObject(54); } }
        public IPgAIAbility MushroomMonster_SummonMushroomSpawn3 { get { return GetAbilityObject(55); } }
        public IPgAIAbility MushroomMonster_SpawnSpit1 { get { return GetAbilityObject(56); } }
        public IPgAIAbility MushroomMonster_SpawnSpit2 { get { return GetAbilityObject(57); } }
        public IPgAIAbility MushroomMonster_SpawnSuperSpit1 { get { return GetAbilityObject(58); } }
        public IPgAIAbility MushroomMonster_SpawnSuperSpit2 { get { return GetAbilityObject(59); } }
        public IPgAIAbility MaronesaStomp { get { return GetAbilityObject(60); } }
        public IPgAIAbility MaronesaInfect { get { return GetAbilityObject(61); } }
        public IPgAIAbility WormBite1 { get { return GetAbilityObject(62); } }
        public IPgAIAbility WormSpit1 { get { return GetAbilityObject(63); } }
        public IPgAIAbility WormShove1 { get { return GetAbilityObject(64); } }
        public IPgAIAbility WormInfect1 { get { return GetAbilityObject(65); } }
        public IPgAIAbility WormSpit2 { get { return GetAbilityObject(66); } }
        public IPgAIAbility WormBossSpit { get { return GetAbilityObject(67); } }
        public IPgAIAbility WormBossAcidBurst { get { return GetAbilityObject(68); } }
        public IPgAIAbility GnasherBite { get { return GetAbilityObject(69); } }
        public IPgAIAbility GnasherRend { get { return GetAbilityObject(70); } }
        public IPgAIAbility Dinoslash { get { return GetAbilityObject(71); } }
        public IPgAIAbility Dinoslash2 { get { return GetAbilityObject(72); } }
        public IPgAIAbility Dinobite { get { return GetAbilityObject(73); } }
        public IPgAIAbility Dinobite2 { get { return GetAbilityObject(74); } }
        public IPgAIAbility Dinowhap { get { return GetAbilityObject(75); } }
        public IPgAIAbility Peck { get { return GetAbilityObject(76); } }
        public IPgAIAbility AnimalFlee { get { return GetAbilityObject(77); } }
        public IPgAIAbility HippogriffPeck { get { return GetAbilityObject(78); } }
        public IPgAIAbility HippogriffSlashes { get { return GetAbilityObject(79); } }
        public IPgAIAbility HippogriffBossSlashes { get { return GetAbilityObject(80); } }
        public IPgAIAbility RatBite { get { return GetAbilityObject(81); } }
        public IPgAIAbility RatClaw { get { return GetAbilityObject(82); } }
        public IPgAIAbility FireRatBite { get { return GetAbilityObject(83); } }
        public IPgAIAbility FireRatClaw { get { return GetAbilityObject(84); } }
        public IPgAIAbility GoblinZapBall { get { return GetAbilityObject(85); } }
        public IPgAIAbility GoblinHateZapBall { get { return GetAbilityObject(86); } }
        public IPgAIAbility GoblinHateZapBall2 { get { return GetAbilityObject(87); } }
        public IPgAIAbility RhinoHorn { get { return GetAbilityObject(88); } }
        public IPgAIAbility RhinoRage { get { return GetAbilityObject(89); } }
        public IPgAIAbility RhinoFireball { get { return GetAbilityObject(90); } }
        public IPgAIAbility RhinoBossRage { get { return GetAbilityObject(91); } }
        public IPgAIAbility YetiPunch { get { return GetAbilityObject(92); } }
        public IPgAIAbility YetiEncase { get { return GetAbilityObject(93); } }
        public IPgAIAbility YetiDebuff { get { return GetAbilityObject(94); } }
        public IPgAIAbility YetiBoulderThrow { get { return GetAbilityObject(95); } }
        public IPgAIAbility YetiBarrage { get { return GetAbilityObject(96); } }
        public IPgAIAbility YetiRoarStun { get { return GetAbilityObject(97); } }
        public IPgAIAbility YetiFlingAway { get { return GetAbilityObject(98); } }
        public IPgAIAbility YetiIceBallThrow { get { return GetAbilityObject(99); } }
        public IPgAIAbility YetiIceSpear { get { return GetAbilityObject(100); } }
        public IPgAIAbility YetiColdOrb { get { return GetAbilityObject(101); } }
        public IPgAIAbility YetiFrostRing { get { return GetAbilityObject(102); } }
        public IPgAIAbility WorgBite { get { return GetAbilityObject(103); } }
        public IPgAIAbility WorgOmegaBite { get { return GetAbilityObject(104); } }
        public IPgAIAbility BearBite { get { return GetAbilityObject(105); } }
        public IPgAIAbility BearCrush { get { return GetAbilityObject(106); } }
        public IPgAIAbility BrainBite { get { return GetAbilityObject(107); } }
        public IPgAIAbility BrainDrain { get { return GetAbilityObject(108); } }
        public IPgAIAbility BrainDrain2 { get { return GetAbilityObject(109); } }
        public IPgAIAbility BigCatClaw { get { return GetAbilityObject(110); } }
        public IPgAIAbility BigCatPounce { get { return GetAbilityObject(111); } }
        public IPgAIAbility BigCatRagePounce { get { return GetAbilityObject(112); } }
        public IPgAIAbility BigCatDebuff { get { return GetAbilityObject(113); } }
        public IPgAIAbility SpiderBite { get { return GetAbilityObject(114); } }
        public IPgAIAbility SpiderFireball { get { return GetAbilityObject(115); } }
        public IPgAIAbility SpiderBossFreePin { get { return GetAbilityObject(116); } }
        public IPgAIAbility SpiderKill2 { get { return GetAbilityObject(117); } }
        public IPgAIAbility SpiderInject { get { return GetAbilityObject(118); } }
        public IPgAIAbility SpiderKill { get { return GetAbilityObject(119); } }
        public IPgAIAbility AcidBall1 { get { return GetAbilityObject(120); } }
        public IPgAIAbility SpiderPin { get { return GetAbilityObject(121); } }
        public IPgAIAbility SpiderKill3 { get { return GetAbilityObject(122); } }
        public IPgAIAbility AcidBall2 { get { return GetAbilityObject(123); } }
        public IPgAIAbility AcidSpew1 { get { return GetAbilityObject(124); } }
        public IPgAIAbility AcidExplosion1 { get { return GetAbilityObject(125); } }
        public IPgAIAbility AcidExplosion2 { get { return GetAbilityObject(126); } }
        public IPgAIAbility SpiderIncubate { get { return GetAbilityObject(127); } }
        public IPgAIAbility MantisClaw { get { return GetAbilityObject(128); } }
        public IPgAIAbility MantisRage { get { return GetAbilityObject(129); } }
        public IPgAIAbility MantisAcidBurst { get { return GetAbilityObject(130); } }
        public IPgAIAbility SherzatClaw { get { return GetAbilityObject(131); } }
        public IPgAIAbility SherzatAcidSpit { get { return GetAbilityObject(132); } }
        public IPgAIAbility SherzatDisintegrate { get { return GetAbilityObject(133); } }
        public IPgAIAbility MantisSwipe { get { return GetAbilityObject(134); } }
        public IPgAIAbility MantisBlast { get { return GetAbilityObject(135); } }
        public IPgAIAbility SnailStrike { get { return GetAbilityObject(136); } }
        public IPgAIAbility SnailRage { get { return GetAbilityObject(137); } }
        public IPgAIAbility SnailRageB { get { return GetAbilityObject(138); } }
        public IPgAIAbility SnailRageC { get { return GetAbilityObject(139); } }
        public IPgAIAbility HookClaw { get { return GetAbilityObject(140); } }
        public IPgAIAbility HookAcid { get { return GetAbilityObject(141); } }
        public IPgAIAbility HookRage { get { return GetAbilityObject(142); } }
        public IPgAIAbility UndeadSword1 { get { return GetAbilityObject(143); } }
        public IPgAIAbility UndeadSword2 { get { return GetAbilityObject(144); } }
        public IPgAIAbility UndeadSwordAngry { get { return GetAbilityObject(145); } }
        public IPgAIAbility UndeadMegaSword1 { get { return GetAbilityObject(146); } }
        public IPgAIAbility UndeadMegaSword2 { get { return GetAbilityObject(147); } }
        public IPgAIAbility UndeadMegaSwordAngry { get { return GetAbilityObject(148); } }
        public IPgAIAbility UndeadLightningSmite { get { return GetAbilityObject(149); } }
        public IPgAIAbility UndeadPhysicalShield { get { return GetAbilityObject(150); } }
        public IPgAIAbility UndeadSword1B { get { return GetAbilityObject(151); } }
        public IPgAIAbility UndeadFireballA { get { return GetAbilityObject(152); } }
        public IPgAIAbility UndeadFireballB { get { return GetAbilityObject(153); } }
        public IPgAIAbility UndeadFireballB2 { get { return GetAbilityObject(154); } }
        public IPgAIAbility UndeadIceBall1 { get { return GetAbilityObject(155); } }
        public IPgAIAbility UndeadFreezeBall { get { return GetAbilityObject(156); } }
        public IPgAIAbility UndeadFireballLongA { get { return GetAbilityObject(157); } }
        public IPgAIAbility UndeadFireballLongB { get { return GetAbilityObject(158); } }
        public IPgAIAbility UndeadFireballLongB2 { get { return GetAbilityObject(159); } }
        public IPgAIAbility KhyrulekCurseBall { get { return GetAbilityObject(160); } }
        public IPgAIAbility UrsulaFireball1 { get { return GetAbilityObject(161); } }
        public IPgAIAbility UrsulaFireball1B { get { return GetAbilityObject(162); } }
        public IPgAIAbility UrsulaFireball2 { get { return GetAbilityObject(163); } }
        public IPgAIAbility UrsulaIceball1 { get { return GetAbilityObject(164); } }
        public IPgAIAbility UrsulaIceball1B { get { return GetAbilityObject(165); } }
        public IPgAIAbility UrsulaIceball2 { get { return GetAbilityObject(166); } }
        public IPgAIAbility UrsulaSummon { get { return GetAbilityObject(167); } }
        public IPgAIAbility UrsulaRage { get { return GetAbilityObject(168); } }
        public IPgAIAbility UndeadFireballA2 { get { return GetAbilityObject(169); } }
        public IPgAIAbility BigHeadCurseball { get { return GetAbilityObject(170); } }
        public IPgAIAbility UndeadArrow1 { get { return GetAbilityObject(171); } }
        public IPgAIAbility UndeadArrow2 { get { return GetAbilityObject(172); } }
        public IPgAIAbility UndeadOmegaArrow { get { return GetAbilityObject(173); } }
        public IPgAIAbility PetUndeadArrow1 { get { return GetAbilityObject(174); } }
        public IPgAIAbility PetUndeadArrow2 { get { return GetAbilityObject(175); } }
        public IPgAIAbility PetUndeadOmegaArrow { get { return GetAbilityObject(176); } }
        public IPgAIAbility UndeadGrappleArrow1 { get { return GetAbilityObject(177); } }
        public IPgAIAbility UndeadSelfDestruct { get { return GetAbilityObject(178); } }
        public IPgAIAbility UndeadDarknessBall { get { return GetAbilityObject(179); } }
        public IPgAIAbility UndeadBoneWhirlwind { get { return GetAbilityObject(180); } }
        public IPgAIAbility PetUndeadSword1 { get { return GetAbilityObject(181); } }
        public IPgAIAbility PetUndeadSword2 { get { return GetAbilityObject(182); } }
        public IPgAIAbility PetUndeadSwordAngry { get { return GetAbilityObject(183); } }
        public IPgAIAbility PetUndeadFireballA { get { return GetAbilityObject(184); } }
        public IPgAIAbility PetUndeadFireballB { get { return GetAbilityObject(185); } }
        public IPgAIAbility PetUndeadDefensiveBurst { get { return GetAbilityObject(186); } }
        public IPgAIAbility PetUndeadPunch1 { get { return GetAbilityObject(187); } }
        public IPgAIAbility ZombiePunch { get { return GetAbilityObject(188); } }
        public IPgAIAbility ZombieBite { get { return GetAbilityObject(189); } }
        public IPgAIAbility GoblinSpear1 { get { return GetAbilityObject(190); } }
        public IPgAIAbility GoblinSpear2 { get { return GetAbilityObject(191); } }
        public IPgAIAbility GoblinRageSpear1 { get { return GetAbilityObject(192); } }
        public IPgAIAbility GoblinRageSpear2 { get { return GetAbilityObject(193); } }
        public IPgAIAbility GoblinHeal1 { get { return GetAbilityObject(194); } }
        public IPgAIAbility GoblinHeal2 { get { return GetAbilityObject(195); } }
        public IPgAIAbility GoblinPunch { get { return GetAbilityObject(196); } }
        public IPgAIAbility GoblinArrow1 { get { return GetAbilityObject(197); } }
        public IPgAIAbility GoblinArrow2 { get { return GetAbilityObject(198); } }
        public IPgAIAbility GoblinRageArrow1 { get { return GetAbilityObject(199); } }
        public IPgAIAbility GoblinRageArrow2 { get { return GetAbilityObject(200); } }
        public IPgAIAbility GoblinSpreadZapBall { get { return GetAbilityObject(201); } }
        public IPgAIAbility GoblinBossLightning { get { return GetAbilityObject(202); } }
        public IPgAIAbility GoblinArmorBuff { get { return GetAbilityObject(203); } }
        public IPgAIAbility MummySlamA { get { return GetAbilityObject(204); } }
        public IPgAIAbility MummySlamB { get { return GetAbilityObject(205); } }
        public IPgAIAbility MummySlamCombo { get { return GetAbilityObject(206); } }
        public IPgAIAbility MummyWrapA { get { return GetAbilityObject(207); } }
        public IPgAIAbility MummyWrapB { get { return GetAbilityObject(208); } }
        public IPgAIAbility MummyWrapRage { get { return GetAbilityObject(209); } }
        public IPgAIAbility BarutiWrapA { get { return GetAbilityObject(210); } }
        public IPgAIAbility BarutiWrapB { get { return GetAbilityObject(211); } }
        public IPgAIAbility BarutiWrapRage { get { return GetAbilityObject(212); } }
        public IPgAIAbility FireWallAttack1 { get { return GetAbilityObject(213); } }
        public IPgAIAbility FireWallDotAttack1 { get { return GetAbilityObject(214); } }
        public IPgAIAbility FireSnakeExplosion1 { get { return GetAbilityObject(215); } }
        public IPgAIAbility FireTrapAttack1 { get { return GetAbilityObject(216); } }
        public IPgAIAbility HealingAura1 { get { return GetAbilityObject(217); } }
        public IPgAIAbility HealingAura2 { get { return GetAbilityObject(218); } }
        public IPgAIAbility HealingAura3 { get { return GetAbilityObject(219); } }
        public IPgAIAbility HealingAura4 { get { return GetAbilityObject(220); } }
        public IPgAIAbility DruidHealingSanctuaryHeal { get { return GetAbilityObject(221); } }
        public IPgAIAbility AcidAuraBall1 { get { return GetAbilityObject(222); } }
        public IPgAIAbility AcidAuraBall2 { get { return GetAbilityObject(223); } }
        public IPgAIAbility AcidAuraBall3 { get { return GetAbilityObject(224); } }
        public IPgAIAbility AcidAuraBall4 { get { return GetAbilityObject(225); } }
        public IPgAIAbility ElectricityAura1 { get { return GetAbilityObject(226); } }
        public IPgAIAbility ElectricityAuraBolt1 { get { return GetAbilityObject(227); } }
        public IPgAIAbility ReboundAura1 { get { return GetAbilityObject(228); } }
        public IPgAIAbility ColdAuraBurst { get { return GetAbilityObject(229); } }
        public IPgAIAbility WebStick { get { return GetAbilityObject(230); } }
        public IPgAIAbility IcySlam { get { return GetAbilityObject(231); } }
        public IPgAIAbility IcyCocoon { get { return GetAbilityObject(232); } }
        public IPgAIAbility IcyCocoon2 { get { return GetAbilityObject(233); } }
        public IPgAIAbility ElementalSlam { get { return GetAbilityObject(234); } }
        public IPgAIAbility ElementalBees { get { return GetAbilityObject(235); } }
        public IPgAIAbility ElementalBees2 { get { return GetAbilityObject(236); } }
        public IPgAIAbility FaeLightningSmite { get { return GetAbilityObject(237); } }
        public IPgAIAbility TotalHorrorAttack { get { return GetAbilityObject(238); } }
        public IPgAIAbility TotalHorrorStretch { get { return GetAbilityObject(239); } }
        public IPgAIAbility TotalHorrorHeal { get { return GetAbilityObject(240); } }
        public IPgAIAbility TotalHorrorHeal2 { get { return GetAbilityObject(241); } }
        public IPgAIAbility SheepBomb1 { get { return GetAbilityObject(242); } }
        public IPgAIAbility SlugPoisonBite { get { return GetAbilityObject(243); } }
        public IPgAIAbility SlugPoisonRage { get { return GetAbilityObject(244); } }
        public IPgAIAbility SlugPoisonBite2 { get { return GetAbilityObject(245); } }
        public IPgAIAbility SlugPoisonRage2 { get { return GetAbilityObject(246); } }
        public IPgAIAbility SlugPoisonBite3 { get { return GetAbilityObject(247); } }
        public IPgAIAbility SlugPoisonRage3 { get { return GetAbilityObject(248); } }
        public IPgAIAbility TornadoJolt1 { get { return GetAbilityObject(249); } }
        public IPgAIAbility TornadoFling { get { return GetAbilityObject(250); } }
        public IPgAIAbility TornadoToss { get { return GetAbilityObject(251); } }
        public IPgAIAbility TheFogCurse { get { return GetAbilityObject(252); } }
        public IPgAIAbility MonsterWerewolfPouncingRake { get { return GetAbilityObject(253); } }
        public IPgAIAbility MonsterWerewolfPackAttack { get { return GetAbilityObject(254); } }
        public IPgAIAbility MonsterWerewolfHowl { get { return GetAbilityObject(255); } }
        public IPgAIAbility Werewolf_Summon_Rage { get { return GetAbilityObject(256); } }
        public IPgAIAbility Werewolf_Summon_Opener { get { return GetAbilityObject(257); } }
        public IPgAIAbility BleddynHowl { get { return GetAbilityObject(258); } }
        public IPgAIAbility OrcSwordSlash { get { return GetAbilityObject(259); } }
        public IPgAIAbility OrcParry { get { return GetAbilityObject(260); } }
        public IPgAIAbility OrcFinishingBlow { get { return GetAbilityObject(261); } }
        public IPgAIAbility OrcHipThrow { get { return GetAbilityObject(262); } }
        public IPgAIAbility OrcKneeKick { get { return GetAbilityObject(263); } }
        public IPgAIAbility OrcPunch { get { return GetAbilityObject(264); } }
        public IPgAIAbility OrcArrow1 { get { return GetAbilityObject(265); } }
        public IPgAIAbility OrcArrow2 { get { return GetAbilityObject(266); } }
        public IPgAIAbility OrcStaffSmash { get { return GetAbilityObject(267); } }
        public IPgAIAbility OrcFireball { get { return GetAbilityObject(268); } }
        public IPgAIAbility OrcHeal1 { get { return GetAbilityObject(269); } }
        public IPgAIAbility OrcHeal2 { get { return GetAbilityObject(270); } }
        public IPgAIAbility OrcEvasionBubble { get { return GetAbilityObject(271); } }
        public IPgAIAbility OrcElectricStun { get { return GetAbilityObject(272); } }
        public IPgAIAbility OrcFireBolts { get { return GetAbilityObject(273); } }
        public IPgAIAbility OrcKnockbackBolt { get { return GetAbilityObject(274); } }
        public IPgAIAbility OrcSummonUrak2 { get { return GetAbilityObject(275); } }
        public IPgAIAbility OrcSwordSlashFire { get { return GetAbilityObject(276); } }
        public IPgAIAbility OrcParryFire { get { return GetAbilityObject(277); } }
        public IPgAIAbility OrcFinishingBlowFire { get { return GetAbilityObject(278); } }
        public IPgAIAbility OrcSummonSigil1 { get { return GetAbilityObject(279); } }
        public IPgAIAbility OrcSpearAttack { get { return GetAbilityObject(280); } }
        public IPgAIAbility OrcHalberdAttack { get { return GetAbilityObject(281); } }
        public IPgAIAbility OrcAreaHalberdAttack { get { return GetAbilityObject(282); } }
        public IPgAIAbility OrcDebuffArrow { get { return GetAbilityObject(283); } }
        public IPgAIAbility OrcSlice { get { return GetAbilityObject(284); } }
        public IPgAIAbility OrcVenomstrike1 { get { return GetAbilityObject(285); } }
        public IPgAIAbility OrcVenomstrike0 { get { return GetAbilityObject(286); } }
        public IPgAIAbility OrcLieutenantDebuffTaunt { get { return GetAbilityObject(287); } }
        public IPgAIAbility OrcAreaHalberdBoss { get { return GetAbilityObject(288); } }
        public IPgAIAbility OrcDeathsHold { get { return GetAbilityObject(289); } }
        public IPgAIAbility GazlukPriest1Special { get { return GetAbilityObject(290); } }
        public IPgAIAbility GazlukPriest2Special { get { return GetAbilityObject(291); } }
        public IPgAIAbility GazlukPriest3Special { get { return GetAbilityObject(292); } }
        public IPgAIAbility OrcExtinguishLife { get { return GetAbilityObject(293); } }
        public IPgAIAbility OrcDarknessBall { get { return GetAbilityObject(294); } }
        public IPgAIAbility OrcWaveOfDarkness { get { return GetAbilityObject(295); } }
        public IPgAIAbility EnemyMinigolemPunch { get { return GetAbilityObject(296); } }
        public IPgAIAbility EnemyMinigolemHeal { get { return GetAbilityObject(297); } }
        public IPgAIAbility EnemyMinigolemExplode { get { return GetAbilityObject(298); } }
        public IPgAIAbility MinigolemBombToss { get { return GetAbilityObject(299); } }
        public IPgAIAbility MinigolemBombToss2 { get { return GetAbilityObject(300); } }
        public IPgAIAbility MinigolemBombToss3 { get { return GetAbilityObject(301); } }
        public IPgAIAbility MinigolemBombToss4 { get { return GetAbilityObject(302); } }
        public IPgAIAbility MinigolemBombToss5 { get { return GetAbilityObject(303); } }
        public IPgAIAbility MinigolemAoEHeal { get { return GetAbilityObject(304); } }
        public IPgAIAbility MinigolemAoEHeal2 { get { return GetAbilityObject(305); } }
        public IPgAIAbility MinigolemAoEHeal3 { get { return GetAbilityObject(306); } }
        public IPgAIAbility MinigolemAoEHeal4 { get { return GetAbilityObject(307); } }
        public IPgAIAbility MinigolemAoEHeal5 { get { return GetAbilityObject(308); } }
        public IPgAIAbility MinigolemSelfDestruct { get { return GetAbilityObject(309); } }
        public IPgAIAbility MinigolemSelfDestruct2 { get { return GetAbilityObject(310); } }
        public IPgAIAbility MinigolemSelfDestruct3 { get { return GetAbilityObject(311); } }
        public IPgAIAbility MinigolemSelfDestruct4 { get { return GetAbilityObject(312); } }
        public IPgAIAbility MinigolemSelfDestruct5 { get { return GetAbilityObject(313); } }
        public IPgAIAbility MinigolemAoEPower { get { return GetAbilityObject(314); } }
        public IPgAIAbility MinigolemAoEPower2 { get { return GetAbilityObject(315); } }
        public IPgAIAbility MinigolemAoEPower3 { get { return GetAbilityObject(316); } }
        public IPgAIAbility MinigolemAoEPower4 { get { return GetAbilityObject(317); } }
        public IPgAIAbility MinigolemAoEPower5 { get { return GetAbilityObject(318); } }
        public IPgAIAbility MinigolemHeal { get { return GetAbilityObject(319); } }
        public IPgAIAbility MinigolemHeal2 { get { return GetAbilityObject(320); } }
        public IPgAIAbility MinigolemHeal3 { get { return GetAbilityObject(321); } }
        public IPgAIAbility MinigolemHeal4 { get { return GetAbilityObject(322); } }
        public IPgAIAbility MinigolemHeal5 { get { return GetAbilityObject(323); } }
        public IPgAIAbility MinigolemDoomAdmixture { get { return GetAbilityObject(324); } }
        public IPgAIAbility MinigolemDoomAdmixture2 { get { return GetAbilityObject(325); } }
        public IPgAIAbility MinigolemDoomAdmixture3 { get { return GetAbilityObject(326); } }
        public IPgAIAbility MinigolemDoomAdmixture4 { get { return GetAbilityObject(327); } }
        public IPgAIAbility MinigolemDoomAdmixture5 { get { return GetAbilityObject(328); } }
        public IPgAIAbility MinigolemSelfSacrifice { get { return GetAbilityObject(329); } }
        public IPgAIAbility MinigolemSelfSacrifice2 { get { return GetAbilityObject(330); } }
        public IPgAIAbility MinigolemSelfSacrifice3 { get { return GetAbilityObject(331); } }
        public IPgAIAbility MinigolemSelfSacrifice4 { get { return GetAbilityObject(332); } }
        public IPgAIAbility MinigolemSelfSacrifice5 { get { return GetAbilityObject(333); } }
        public IPgAIAbility MinigolemPunch { get { return GetAbilityObject(334); } }
        public IPgAIAbility MinigolemPunch2 { get { return GetAbilityObject(335); } }
        public IPgAIAbility MinigolemPunch3 { get { return GetAbilityObject(336); } }
        public IPgAIAbility MinigolemPunch4 { get { return GetAbilityObject(337); } }
        public IPgAIAbility MinigolemPunch5 { get { return GetAbilityObject(338); } }
        public IPgAIAbility MinigolemHasteConcoction1 { get { return GetAbilityObject(339); } }
        public IPgAIAbility MinigolemHasteConcoction2 { get { return GetAbilityObject(340); } }
        public IPgAIAbility MinigolemHasteConcoction3 { get { return GetAbilityObject(341); } }
        public IPgAIAbility MinigolemFireBalm1 { get { return GetAbilityObject(342); } }
        public IPgAIAbility MinigolemFireBalm2 { get { return GetAbilityObject(343); } }
        public IPgAIAbility MinigolemFireBalm3 { get { return GetAbilityObject(344); } }
        public IPgAIAbility MinigolemFireBalm4 { get { return GetAbilityObject(345); } }
        public IPgAIAbility MinigolemFireBalm5 { get { return GetAbilityObject(346); } }
        public IPgAIAbility MinigolemRageAoEHeal1 { get { return GetAbilityObject(347); } }
        public IPgAIAbility MinigolemRageAoEHeal2 { get { return GetAbilityObject(348); } }
        public IPgAIAbility MinigolemRageAoEHeal3 { get { return GetAbilityObject(349); } }
        public IPgAIAbility MinigolemRageAoEHeal4 { get { return GetAbilityObject(350); } }
        public IPgAIAbility MinigolemRageAoEHeal5 { get { return GetAbilityObject(351); } }
        public IPgAIAbility MinigolemRageAcidToss1 { get { return GetAbilityObject(352); } }
        public IPgAIAbility MinigolemRageAcidToss2 { get { return GetAbilityObject(353); } }
        public IPgAIAbility MinigolemRageAcidToss3 { get { return GetAbilityObject(354); } }
        public IPgAIAbility MinigolemRageAcidToss4 { get { return GetAbilityObject(355); } }
        public IPgAIAbility MinigolemRageAcidToss5 { get { return GetAbilityObject(356); } }
        public IPgAIAbility TrainingGolemPunch { get { return GetAbilityObject(357); } }
        public IPgAIAbility TrainingGolemStun { get { return GetAbilityObject(358); } }
        public IPgAIAbility TrainingGolemHeal { get { return GetAbilityObject(359); } }
        public IPgAIAbility TrainingGolemHealB { get { return GetAbilityObject(360); } }
        public IPgAIAbility TrainingGolemFireBreath { get { return GetAbilityObject(361); } }
        public IPgAIAbility TrainingGolemFireBurst { get { return GetAbilityObject(362); } }
        public IPgAIAbility GrimalkinClaw { get { return GetAbilityObject(363); } }
        public IPgAIAbility GrimalkinBite { get { return GetAbilityObject(364); } }
        public IPgAIAbility GrimalkinPuncture { get { return GetAbilityObject(365); } }
        public IPgAIAbility WerewolfSword1 { get { return GetAbilityObject(366); } }
        public IPgAIAbility WerewolfSword2 { get { return GetAbilityObject(367); } }
        public IPgAIAbility WerewolfSwordStun { get { return GetAbilityObject(368); } }
        public IPgAIAbility WerewolfArrow1 { get { return GetAbilityObject(369); } }
        public IPgAIAbility WerewolfArrow2 { get { return GetAbilityObject(370); } }
        public IPgAIAbility WerewolfOmegaArrow { get { return GetAbilityObject(371); } }
        public IPgAIAbility NpcSmash { get { return GetAbilityObject(372); } }
        public IPgAIAbility NpcDoubleHitCurse { get { return GetAbilityObject(373); } }
        public IPgAIAbility NpcBlockingStance { get { return GetAbilityObject(374); } }
        public IPgAIAbility NpcHeadcracker { get { return GetAbilityObject(375); } }
        public IPgAIAbility StrigaClawA { get { return GetAbilityObject(376); } }
        public IPgAIAbility StrigaClawB { get { return GetAbilityObject(377); } }
        public IPgAIAbility StrigaReap { get { return GetAbilityObject(378); } }
        public IPgAIAbility StrigaReap2 { get { return GetAbilityObject(379); } }
        public IPgAIAbility StrigaFireBreath { get { return GetAbilityObject(380); } }
        public IPgAIAbility StrigaBuff { get { return GetAbilityObject(381); } }
        public IPgAIAbility GhostlyPunchA { get { return GetAbilityObject(382); } }
        public IPgAIAbility GhostlyPunchB { get { return GetAbilityObject(383); } }
        public IPgAIAbility GhostlyBurst { get { return GetAbilityObject(384); } }
        public IPgAIAbility GhostlyBossPunchA { get { return GetAbilityObject(385); } }
        public IPgAIAbility GhostlyBossPunchB { get { return GetAbilityObject(386); } }
        public IPgAIAbility GhostlyBossBurst { get { return GetAbilityObject(387); } }
        public IPgAIAbility GhostlyBolt { get { return GetAbilityObject(388); } }
        public IPgAIAbility InjectorBugBite { get { return GetAbilityObject(389); } }
        public IPgAIAbility InjectorBugInject { get { return GetAbilityObject(390); } }
        public IPgAIAbility InjectorBugInject2 { get { return GetAbilityObject(391); } }
        public IPgAIAbility FaceOfDeathKill { get { return GetAbilityObject(392); } }
        public IPgAIAbility WatcherFireball { get { return GetAbilityObject(393); } }
        public IPgAIAbility WatcherSlap { get { return GetAbilityObject(394); } }
        public IPgAIAbility WatcherAcidball { get { return GetAbilityObject(395); } }
        public IPgAIAbility RedCrystalBlast { get { return GetAbilityObject(396); } }
        public IPgAIAbility RedCrystalBurst { get { return GetAbilityObject(397); } }
        public IPgAIAbility TurretCrystalZap { get { return GetAbilityObject(398); } }
        public IPgAIAbility TurretCrystalZap2 { get { return GetAbilityObject(399); } }
        public IPgAIAbility TurretCrystalZapLongRange { get { return GetAbilityObject(400); } }
        public IPgAIAbility TurretCrystalZapLongRange2 { get { return GetAbilityObject(401); } }
        public IPgAIAbility DeathRay { get { return GetAbilityObject(402); } }
        public IPgAIAbility SpyPortalZap { get { return GetAbilityObject(403); } }
        public IPgAIAbility SpyPortalZap2 { get { return GetAbilityObject(404); } }
        public IPgAIAbility BitingVineBite { get { return GetAbilityObject(405); } }
        public IPgAIAbility BitingVineSpit { get { return GetAbilityObject(406); } }
        public IPgAIAbility BitingVineSpitB { get { return GetAbilityObject(407); } }
        public IPgAIAbility BitingVineCast { get { return GetAbilityObject(408); } }
        public IPgAIAbility BitingVineAppear { get { return GetAbilityObject(409); } }
        public IPgAIAbility BitingVineDisappear { get { return GetAbilityObject(410); } }
        public IPgAIAbility TrollClubA { get { return GetAbilityObject(411); } }
        public IPgAIAbility TrollClubB { get { return GetAbilityObject(412); } }
        public IPgAIAbility TrollKnockdown { get { return GetAbilityObject(413); } }
        public IPgAIAbility OgreClubA { get { return GetAbilityObject(414); } }
        public IPgAIAbility OgreClubB { get { return GetAbilityObject(415); } }
        public IPgAIAbility OgreThrow { get { return GetAbilityObject(416); } }
        public IPgAIAbility OgreStun { get { return GetAbilityObject(417); } }
        public IPgAIAbility FaeSwordA { get { return GetAbilityObject(418); } }
        public IPgAIAbility FaeSwordB { get { return GetAbilityObject(419); } }
        public IPgAIAbility FaeSwordKill { get { return GetAbilityObject(420); } }
        public IPgAIAbility DementiaPuckCurse { get { return GetAbilityObject(421); } }
        public IPgAIAbility FaeLightningSmiteHidden { get { return GetAbilityObject(422); } }
        public IPgAIAbility NecroSpark { get { return GetAbilityObject(423); } }
        public IPgAIAbility NecroDarknessWave { get { return GetAbilityObject(424); } }
        public IPgAIAbility NecroPainBubble { get { return GetAbilityObject(425); } }
        public IPgAIAbility NecroSparkPerching { get { return GetAbilityObject(426); } }
        public IPgAIAbility NecroDeathsHold { get { return GetAbilityObject(427); } }
        public IPgAIAbility DroachBiteA { get { return GetAbilityObject(428); } }
        public IPgAIAbility DroachBiteB { get { return GetAbilityObject(429); } }
        public IPgAIAbility DroachFireball { get { return GetAbilityObject(430); } }
        public IPgAIAbility DroachFireballPerching { get { return GetAbilityObject(431); } }
        public IPgAIAbility DroachBreatheFire { get { return GetAbilityObject(432); } }
        public IPgAIAbility DroachLightning { get { return GetAbilityObject(433); } }
        public IPgAIAbility DroachLightningPerching { get { return GetAbilityObject(434); } }
        public IPgAIAbility DroachShockingKnockback { get { return GetAbilityObject(435); } }
        public IPgAIAbility BasiliskClawA { get { return GetAbilityObject(436); } }
        public IPgAIAbility BasiliskClawB { get { return GetAbilityObject(437); } }
        public IPgAIAbility BasiliskToxicBite { get { return GetAbilityObject(438); } }
        public IPgAIAbility BasiliskDebuff { get { return GetAbilityObject(439); } }
        public IPgAIAbility BasiliskCastPerching { get { return GetAbilityObject(440); } }
        public IPgAIAbility CultistArrow1 { get { return GetAbilityObject(441); } }
        public IPgAIAbility CultistArrow2 { get { return GetAbilityObject(442); } }
        public IPgAIAbility CultistOmegaArrow { get { return GetAbilityObject(443); } }
        public IPgAIAbility CultistSword1 { get { return GetAbilityObject(444); } }
        public IPgAIAbility CultistSword2 { get { return GetAbilityObject(445); } }
        public IPgAIAbility CultistSwordStun { get { return GetAbilityObject(446); } }
        public IPgAIAbility BossMegaSword1 { get { return GetAbilityObject(447); } }
        public IPgAIAbility BossMegaSword2 { get { return GetAbilityObject(448); } }
        public IPgAIAbility SedgewickMegaSwordAngry { get { return GetAbilityObject(449); } }
        public IPgAIAbility BossMegaHammer { get { return GetAbilityObject(450); } }
        public IPgAIAbility BossMegaHammer2 { get { return GetAbilityObject(451); } }
        public IPgAIAbility BossMegaRageHammer { get { return GetAbilityObject(452); } }
        public IPgAIAbility ClaudiaTundraSpikes { get { return GetAbilityObject(453); } }
        public IPgAIAbility ClaudiaIceSpear { get { return GetAbilityObject(454); } }
        public IPgAIAbility ClaudiaBlizzard { get { return GetAbilityObject(455); } }
        public IPgAIAbility BigGolemHitA { get { return GetAbilityObject(456); } }
        public IPgAIAbility BigGolemHitB { get { return GetAbilityObject(457); } }
        public IPgAIAbility BigGolemFlingBoss { get { return GetAbilityObject(458); } }
        public IPgAIAbility BigGolemPerchFix { get { return GetAbilityObject(459); } }
        public IPgAIAbility BigGolemFlingBoss2 { get { return GetAbilityObject(460); } }
        public IPgAIAbility BigGolemSummonFireSnake { get { return GetAbilityObject(461); } }
        public IPgAIAbility BigGolemHitB_NoDisable { get { return GetAbilityObject(462); } }
        public IPgAIAbility BigGolemHitA_NoDisable { get { return GetAbilityObject(463); } }
        public IPgAIAbility BigGolemFling { get { return GetAbilityObject(464); } }
        public IPgAIAbility GhoulClawA { get { return GetAbilityObject(465); } }
        public IPgAIAbility GhoulClawB { get { return GetAbilityObject(466); } }
        public IPgAIAbility GhoulSelfBuff { get { return GetAbilityObject(467); } }
        public IPgAIAbility GhoulHammerA { get { return GetAbilityObject(468); } }
        public IPgAIAbility GhoulHammerB { get { return GetAbilityObject(469); } }
        public IPgAIAbility DragonWormSpitElectricity { get { return GetAbilityObject(470); } }
        public IPgAIAbility DragonWormBite { get { return GetAbilityObject(471); } }
        public IPgAIAbility DragonWormSmack { get { return GetAbilityObject(472); } }
        public IPgAIAbility DragonWormRage { get { return GetAbilityObject(473); } }
        public IPgAIAbility DragonWormEscape { get { return GetAbilityObject(474); } }
        public IPgAIAbility DragonWormSpitFire { get { return GetAbilityObject(475); } }
        public IPgAIAbility ColdSphereBurst { get { return GetAbilityObject(476); } }
        public IPgAIAbility ColdSphereFreezeBurst { get { return GetAbilityObject(477); } }
        public IPgAIAbility ManticoreBite { get { return GetAbilityObject(478); } }
        public IPgAIAbility ManticoreClaw { get { return GetAbilityObject(479); } }
        public IPgAIAbility ManticoreSting1 { get { return GetAbilityObject(480); } }
        public IPgAIAbility Manticoresting2 { get { return GetAbilityObject(481); } }
        public IPgAIAbility RakStaffHit { get { return GetAbilityObject(482); } }
        public IPgAIAbility RakStaffPin { get { return GetAbilityObject(483); } }
        public IPgAIAbility RakStaffBlock { get { return GetAbilityObject(484); } }
        public IPgAIAbility RakStaffHeavy { get { return GetAbilityObject(485); } }
        public IPgAIAbility RakSlash { get { return GetAbilityObject(486); } }
        public IPgAIAbility RakKnee { get { return GetAbilityObject(487); } }
        public IPgAIAbility RakKick { get { return GetAbilityObject(488); } }
        public IPgAIAbility RakBarrage { get { return GetAbilityObject(489); } }
        public IPgAIAbility RakSwordSlash { get { return GetAbilityObject(490); } }
        public IPgAIAbility RakHackingBlade { get { return GetAbilityObject(491); } }
        public IPgAIAbility RakDecapitate { get { return GetAbilityObject(492); } }
        public IPgAIAbility RakFireball { get { return GetAbilityObject(493); } }
        public IPgAIAbility RakBreatheFire { get { return GetAbilityObject(494); } }
        public IPgAIAbility RakRingOfFire { get { return GetAbilityObject(495); } }
        public IPgAIAbility RakToxinBomb { get { return GetAbilityObject(496); } }
        public IPgAIAbility RakAcidBomb { get { return GetAbilityObject(497); } }
        public IPgAIAbility RakHealingMist { get { return GetAbilityObject(498); } }
        public IPgAIAbility RakBasicShot { get { return GetAbilityObject(499); } }
        public IPgAIAbility RakHookShot { get { return GetAbilityObject(500); } }
        public IPgAIAbility RakBowBash { get { return GetAbilityObject(501); } }
        public IPgAIAbility RakAimedShot { get { return GetAbilityObject(502); } }
        public IPgAIAbility RakPoisonArrow { get { return GetAbilityObject(503); } }
        public IPgAIAbility RakMindreave { get { return GetAbilityObject(504); } }
        public IPgAIAbility RakPainBubble { get { return GetAbilityObject(505); } }
        public IPgAIAbility RakPanicCharge { get { return GetAbilityObject(506); } }
        public IPgAIAbility RakRevitalize { get { return GetAbilityObject(507); } }
        public IPgAIAbility RakReconstruct { get { return GetAbilityObject(508); } }
        public IPgAIAbility RakBossSlow { get { return GetAbilityObject(509); } }
        public IPgAIAbility RakBossPerchSlow { get { return GetAbilityObject(510); } }
        public IPgAIAbility FlapSkullBite { get { return GetAbilityObject(511); } }
        public IPgAIAbility FlapSkullBigBite { get { return GetAbilityObject(512); } }
        public IPgAIAbility MinotaurClub { get { return GetAbilityObject(513); } }
        public IPgAIAbility MinotaurRageClub { get { return GetAbilityObject(514); } }
        public IPgAIAbility MinotaurBoulder { get { return GetAbilityObject(515); } }
        public IPgAIAbility MinotaurBossRageClub { get { return GetAbilityObject(516); } }
        public IPgAIAbility CockatricePeck { get { return GetAbilityObject(517); } }
        public IPgAIAbility CockatriceTailWhip { get { return GetAbilityObject(518); } }
        public IPgAIAbility CockatriceParalyze { get { return GetAbilityObject(519); } }
        public IPgAIAbility GiantBeetleBite { get { return GetAbilityObject(520); } }
        public IPgAIAbility GiantBeetleInject { get { return GetAbilityObject(521); } }
        public IPgAIAbility GiantBeetleBoulderSpit { get { return GetAbilityObject(522); } }
        public IPgAIAbility BatIllusionSlashA { get { return GetAbilityObject(523); } }
        public IPgAIAbility BatIllusionSlashB { get { return GetAbilityObject(524); } }
        public IPgAIAbility BatIllusionBite { get { return GetAbilityObject(525); } }
        public IPgAIAbility GiantBatSlashA { get { return GetAbilityObject(526); } }
        public IPgAIAbility GiantBatSlashB { get { return GetAbilityObject(527); } }
        public IPgAIAbility GiantBatBite { get { return GetAbilityObject(528); } }
        public IPgAIAbility HagAgingTouch { get { return GetAbilityObject(529); } }
        public IPgAIAbility HagAgingScream { get { return GetAbilityObject(530); } }
        public IPgAIAbility TriffidClawA { get { return GetAbilityObject(531); } }
        public IPgAIAbility TriffidClawB { get { return GetAbilityObject(532); } }
        public IPgAIAbility TriffidTongue { get { return GetAbilityObject(533); } }
        public IPgAIAbility TriffidSpore { get { return GetAbilityObject(534); } }
        public IPgAIAbility TriffidShot { get { return GetAbilityObject(535); } }
        public IPgAIAbility TriffidTongueElite { get { return GetAbilityObject(536); } }
        public IPgAIAbility GiantScorpionClawA { get { return GetAbilityObject(537); } }
        public IPgAIAbility GiantScorpionClawB { get { return GetAbilityObject(538); } }
        public IPgAIAbility GiantScorpionSting { get { return GetAbilityObject(539); } }
        public IPgAIAbility KrakenBeak { get { return GetAbilityObject(540); } }
        public IPgAIAbility KrakenSlam { get { return GetAbilityObject(541); } }
        public IPgAIAbility KrakenRage { get { return GetAbilityObject(542); } }
        public IPgAIAbility KrakenBabyBeak { get { return GetAbilityObject(543); } }
        public IPgAIAbility KrakenBabySlam { get { return GetAbilityObject(544); } }
        public IPgAIAbility KrakenBabyRage { get { return GetAbilityObject(545); } }
        public IPgAIAbility RanalonHit { get { return GetAbilityObject(546); } }
        public IPgAIAbility RanalonHitB { get { return GetAbilityObject(547); } }
        public IPgAIAbility RanalonKick { get { return GetAbilityObject(548); } }
        public IPgAIAbility RanalonTongue { get { return GetAbilityObject(549); } }
        public IPgAIAbility RanalonZap { get { return GetAbilityObject(550); } }
        public IPgAIAbility RanalonZapB { get { return GetAbilityObject(551); } }
        public IPgAIAbility RanalonHeal { get { return GetAbilityObject(552); } }
        public IPgAIAbility RanalonRoot { get { return GetAbilityObject(553); } }
        public IPgAIAbility RanalonSelfBuff { get { return GetAbilityObject(554); } }
        public IPgAIAbility RanalonSelfBuffElite { get { return GetAbilityObject(555); } }
        public IPgAIAbility RanalonGuardianStab { get { return GetAbilityObject(556); } }
        public IPgAIAbility RanalonGuardianStabB { get { return GetAbilityObject(557); } }
        public IPgAIAbility RanalonGuardianBite { get { return GetAbilityObject(558); } }
        public IPgAIAbility RanalonGuardianBlind { get { return GetAbilityObject(559); } }
        public IPgAIAbility RanalonDoctrineKeeperStab { get { return GetAbilityObject(560); } }
        public IPgAIAbility RanalonDoctrineKeeperBlind { get { return GetAbilityObject(561); } }
        public IPgAIAbility BarghestBiteA { get { return GetAbilityObject(562); } }
        public IPgAIAbility BarghestBiteB { get { return GetAbilityObject(563); } }
        public IPgAIAbility BarghestDebuff { get { return GetAbilityObject(564); } }
        public IPgAIAbility WorghestDebuff { get { return GetAbilityObject(565); } }
        public IPgAIAbility BallistaFire { get { return GetAbilityObject(566); } }
        public IPgAIAbility BallistaFire_Long { get { return GetAbilityObject(567); } }
        public IPgAIAbility GargoyleSlamA { get { return GetAbilityObject(568); } }
        public IPgAIAbility GargoyleSlamB { get { return GetAbilityObject(569); } }
        public IPgAIAbility GargoyleStun { get { return GetAbilityObject(570); } }
        public IPgAIAbility GargoyleBossStun { get { return GetAbilityObject(571); } }
        public IPgAIAbility ScrayBite { get { return GetAbilityObject(572); } }
        public IPgAIAbility ScrayStab { get { return GetAbilityObject(573); } }
        public IPgAIAbility HippoBite { get { return GetAbilityObject(574); } }
        public IPgAIAbility HippoBiteAndHeal1 { get { return GetAbilityObject(575); } }
        public IPgAIAbility BigCatClaw_Pet { get { return GetAbilityObject(576); } }
        public IPgAIAbility BigCatPounce_Pet { get { return GetAbilityObject(577); } }
        public IPgAIAbility BigCatKill_Pet1 { get { return GetAbilityObject(578); } }
        public IPgAIAbility BigCatKill_Pet2 { get { return GetAbilityObject(579); } }
        public IPgAIAbility BigCatKill_Pet3 { get { return GetAbilityObject(580); } }
        public IPgAIAbility BigCatKill_Pet4 { get { return GetAbilityObject(581); } }
        public IPgAIAbility BigCatKill_Pet5 { get { return GetAbilityObject(582); } }
        public IPgAIAbility BigCatKill_Pet6 { get { return GetAbilityObject(583); } }
        public IPgAIAbility BigCatUltraKill_Pet1 { get { return GetAbilityObject(584); } }
        public IPgAIAbility BigCatUltraKill_Pet2 { get { return GetAbilityObject(585); } }
        public IPgAIAbility BigCatUltraKill_Pet3 { get { return GetAbilityObject(586); } }
        public IPgAIAbility BigCatUltraKill_Pet4 { get { return GetAbilityObject(587); } }
        public IPgAIAbility BigCatUltraKill_Pet5 { get { return GetAbilityObject(588); } }
        public IPgAIAbility BigCatUltraKill_Pet6 { get { return GetAbilityObject(589); } }
        public IPgAIAbility BigCatRoot_Pet1 { get { return GetAbilityObject(590); } }
        public IPgAIAbility BigCatRoot_Pet2 { get { return GetAbilityObject(591); } }
        public IPgAIAbility BigCatRoot_Pet3 { get { return GetAbilityObject(592); } }
        public IPgAIAbility BigCatRoot_Pet4 { get { return GetAbilityObject(593); } }
        public IPgAIAbility BigCatVuln_Pet1 { get { return GetAbilityObject(594); } }
        public IPgAIAbility BigCatVuln_Pet2 { get { return GetAbilityObject(595); } }
        public IPgAIAbility BigCatVuln_Pet3 { get { return GetAbilityObject(596); } }
        public IPgAIAbility BigCatVuln_Pet4 { get { return GetAbilityObject(597); } }
        public IPgAIAbility BigCatHeal_Pet1 { get { return GetAbilityObject(598); } }
        public IPgAIAbility BigCatHeal_Pet2 { get { return GetAbilityObject(599); } }
        public IPgAIAbility BigCatHeal_Pet3 { get { return GetAbilityObject(600); } }
        public IPgAIAbility BigCatHeal_Pet4 { get { return GetAbilityObject(601); } }
        public IPgAIAbility BigCatHeal_Pet5 { get { return GetAbilityObject(602); } }
        public IPgAIAbility BigCatHeal_Pet6 { get { return GetAbilityObject(603); } }
        public IPgAIAbility GrimalkinPuncture_Pet1 { get { return GetAbilityObject(604); } }
        public IPgAIAbility GrimalkinPuncture_Pet2 { get { return GetAbilityObject(605); } }
        public IPgAIAbility GrimalkinPuncture_Pet3 { get { return GetAbilityObject(606); } }
        public IPgAIAbility GrimalkinPuncture_Pet4 { get { return GetAbilityObject(607); } }
        public IPgAIAbility GrimalkinPuncture_Pet5 { get { return GetAbilityObject(608); } }
        public IPgAIAbility GrimalkinPuncture_Pet6 { get { return GetAbilityObject(609); } }
        public IPgAIAbility GrimalkinFlee_Pet1 { get { return GetAbilityObject(610); } }
        public IPgAIAbility GrimalkinFlee_Pet2 { get { return GetAbilityObject(611); } }
        public IPgAIAbility GrimalkinFlee_Pet3 { get { return GetAbilityObject(612); } }
        public IPgAIAbility RatBite_Pet { get { return GetAbilityObject(613); } }
        public IPgAIAbility RatClaw_Pet { get { return GetAbilityObject(614); } }
        public IPgAIAbility RatDeRage_Pet1 { get { return GetAbilityObject(615); } }
        public IPgAIAbility RatDeRage_Pet2 { get { return GetAbilityObject(616); } }
        public IPgAIAbility RatDeRage_Pet3 { get { return GetAbilityObject(617); } }
        public IPgAIAbility RatDeRage_Pet4 { get { return GetAbilityObject(618); } }
        public IPgAIAbility RatDeRage_Pet5 { get { return GetAbilityObject(619); } }
        public IPgAIAbility RatDeRage_Pet6 { get { return GetAbilityObject(620); } }
        public IPgAIAbility RatHeal_Pet1 { get { return GetAbilityObject(621); } }
        public IPgAIAbility RatHeal_Pet2 { get { return GetAbilityObject(622); } }
        public IPgAIAbility RatHeal_Pet3 { get { return GetAbilityObject(623); } }
        public IPgAIAbility RatHeal_Pet4 { get { return GetAbilityObject(624); } }
        public IPgAIAbility RatHeal_Pet5 { get { return GetAbilityObject(625); } }
        public IPgAIAbility RatHeal_Pet6 { get { return GetAbilityObject(626); } }
        public IPgAIAbility RatVuln_Pet1 { get { return GetAbilityObject(627); } }
        public IPgAIAbility RatVuln_Pet2 { get { return GetAbilityObject(628); } }
        public IPgAIAbility RatVuln_Pet3 { get { return GetAbilityObject(629); } }
        public IPgAIAbility RatVuln_Pet4 { get { return GetAbilityObject(630); } }
        public IPgAIAbility FireRatBite_Pet { get { return GetAbilityObject(631); } }
        public IPgAIAbility FireRatClaw_Pet { get { return GetAbilityObject(632); } }
        public IPgAIAbility RatBurn_Pet1 { get { return GetAbilityObject(633); } }
        public IPgAIAbility RatBurn_Pet2 { get { return GetAbilityObject(634); } }
        public IPgAIAbility RatBurn_Pet3 { get { return GetAbilityObject(635); } }
        public IPgAIAbility RatBurn_Pet4 { get { return GetAbilityObject(636); } }
        public IPgAIAbility RatBurn_Pet5 { get { return GetAbilityObject(637); } }
        public IPgAIAbility RatBurn_Pet6 { get { return GetAbilityObject(638); } }
        public IPgAIAbility BearBite_Pet { get { return GetAbilityObject(639); } }
        public IPgAIAbility BearClaw_Pet { get { return GetAbilityObject(640); } }
        public IPgAIAbility BearTaunt_Pet1 { get { return GetAbilityObject(641); } }
        public IPgAIAbility BearTaunt_Pet2 { get { return GetAbilityObject(642); } }
        public IPgAIAbility BearTaunt_Pet3 { get { return GetAbilityObject(643); } }
        public IPgAIAbility BearTaunt_Pet4 { get { return GetAbilityObject(644); } }
        public IPgAIAbility BearTaunt_Pet5 { get { return GetAbilityObject(645); } }
        public IPgAIAbility BearTaunt_Pet6 { get { return GetAbilityObject(646); } }
        public IPgAIAbility BearStun_Pet1 { get { return GetAbilityObject(647); } }
        public IPgAIAbility BearStun_Pet2 { get { return GetAbilityObject(648); } }
        public IPgAIAbility BearStun_Pet3 { get { return GetAbilityObject(649); } }
        public IPgAIAbility BearStun_Pet4 { get { return GetAbilityObject(650); } }
        public IPgAIAbility BearWarmth_Pet1 { get { return GetAbilityObject(651); } }
        public IPgAIAbility BearWarmth_Pet2 { get { return GetAbilityObject(652); } }
        public IPgAIAbility BearWarmth_Pet3 { get { return GetAbilityObject(653); } }
        public IPgAIAbility BearWarmth_Pet4 { get { return GetAbilityObject(654); } }
        public IPgAIAbility BearWarmth_Pet5 { get { return GetAbilityObject(655); } }
        public IPgAIAbility BearWarmth_Pet6 { get { return GetAbilityObject(656); } }
        public IPgAIAbility BearSelfHeal_Pet1 { get { return GetAbilityObject(657); } }
        public IPgAIAbility BearSelfHeal_Pet2 { get { return GetAbilityObject(658); } }
        public IPgAIAbility BearSelfHeal_Pet3 { get { return GetAbilityObject(659); } }
        public IPgAIAbility BearSelfHeal_Pet4 { get { return GetAbilityObject(660); } }
        public IPgAIAbility BearSelfHeal_Pet5 { get { return GetAbilityObject(661); } }
        public IPgAIAbility BearSelfHeal_Pet6 { get { return GetAbilityObject(662); } }
        public IPgAIAbility BearUltra_Pet1 { get { return GetAbilityObject(663); } }
        public IPgAIAbility BearUltra_Pet2 { get { return GetAbilityObject(664); } }
        public IPgAIAbility BearUltra_Pet3 { get { return GetAbilityObject(665); } }
        public IPgAIAbility BearUltra_Pet4 { get { return GetAbilityObject(666); } }
        public IPgAIAbility BearUltra_Pet5 { get { return GetAbilityObject(667); } }
        public IPgAIAbility BearUltra_Pet6 { get { return GetAbilityObject(668); } }
        public IPgAIAbility PumpkinMimicBite { get { return GetAbilityObject(669); } }
        public IPgAIAbility PumpkinMimicRage { get { return GetAbilityObject(670); } }
        public IPgAIAbility WorldBossCiervosNightmareHoof { get { return GetAbilityObject(671); } }
        public IPgAIAbility PumpkinBomb { get { return GetAbilityObject(672); } }
        public IPgAIAbility PumpkinFire { get { return GetAbilityObject(673); } }
        public IPgAIAbility WorldBossKarnag_Opener { get { return GetAbilityObject(674); } }
        public IPgAIAbility WorldBossKarnag_Rage { get { return GetAbilityObject(675); } }
        public IPgAIAbility WorldBossClaudiaTundraSpikes { get { return GetAbilityObject(676); } }
        public IPgAIAbility WorldBossClaudiaIceSpear { get { return GetAbilityObject(677); } }
        public IPgAIAbility WorldBossClaudiaBlizzard { get { return GetAbilityObject(678); } }
        public IPgAIAbility DeerFrontKick { get { return GetAbilityObject(679); } }
        public IPgAIAbility DeerRageKick { get { return GetAbilityObject(680); } }
        public IPgAIAbility PetUndeadReapPunch { get { return GetAbilityObject(681); } }
        public IPgAIAbility PetUndeadRageReap { get { return GetAbilityObject(682); } }
        public IPgAIAbility ViperSpitPoison { get { return GetAbilityObject(683); } }
        public IPgAIAbility ViperBite { get { return GetAbilityObject(684); } }
        public IPgAIAbility Slime_SummonSlime6 { get { return GetAbilityObject(685); } }
        public IPgAIAbility BossSlime_SummonSlime7Elite { get { return GetAbilityObject(686); } }
        public IPgAIAbility SpiderBite_ProblemSpider { get { return GetAbilityObject(687); } }
        public IPgAIAbility PetRatkinUndeadElectricityA { get { return GetAbilityObject(688); } }
        public IPgAIAbility PetRatkinUndeadElectricityB { get { return GetAbilityObject(689); } }
        public IPgAIAbility PetRatkinUndeadElectricityBurst { get { return GetAbilityObject(690); } }
        public IPgAIAbility RakToxinBomb_Tolmar { get { return GetAbilityObject(691); } }
        public IPgAIAbility HippoBiteAndHeal { get { return GetAbilityObject(692); } }
        public IPgAIAbility TreantSlashA { get { return GetAbilityObject(693); } }
        public IPgAIAbility TreantSlashB { get { return GetAbilityObject(694); } }
        public IPgAIAbility TreantStomp { get { return GetAbilityObject(695); } }
        public IPgAIAbility TreantDoT1 { get { return GetAbilityObject(696); } }
        public IPgAIAbility RatTrapAttack { get { return GetAbilityObject(697); } }
        public IPgAIAbility RatkinSwordSlash { get { return GetAbilityObject(698); } }
        public IPgAIAbility RatkinSwordPierce { get { return GetAbilityObject(699); } }
        public IPgAIAbility RatkinSwordFinisher { get { return GetAbilityObject(700); } }
        public IPgAIAbility RatkinDeathsHold { get { return GetAbilityObject(701); } }
        public IPgAIAbility RatkinDarknessBolt { get { return GetAbilityObject(702); } }
        public IPgAIAbility RatkinLifeSteal { get { return GetAbilityObject(703); } }
        public IPgAIAbility RatkinBossWaveOfDarkness { get { return GetAbilityObject(704); } }
        public IPgAIAbility RatkinReverberatingStrike70 { get { return GetAbilityObject(705); } }
        public IPgAIAbility RatkinHammer { get { return GetAbilityObject(706); } }
        public IPgAIAbility RatkinHammerStun { get { return GetAbilityObject(707); } }
        public IPgAIAbility RatkinUndeadElectricityA { get { return GetAbilityObject(708); } }
        public IPgAIAbility RatkinUndeadElectricityB { get { return GetAbilityObject(709); } }
        public IPgAIAbility RatkinUndeadElectricityBurst { get { return GetAbilityObject(710); } }
        public IPgAIAbility RatBuffDarkness_Pet1 { get { return GetAbilityObject(711); } }
        public IPgAIAbility RatBuffDarkness_Pet2 { get { return GetAbilityObject(712); } }
        public IPgAIAbility RatBuffDarkness_Pet3 { get { return GetAbilityObject(713); } }
        public IPgAIAbility RatBuffDarkness_Pet4 { get { return GetAbilityObject(714); } }
        public IPgAIAbility RatBuffDarkness_Pet5 { get { return GetAbilityObject(715); } }
        public IPgAIAbility RatBuffDarkness_Pet6 { get { return GetAbilityObject(716); } }
        public IPgAIAbility ViperRageBite { get { return GetAbilityObject(717); } }
        public IPgAIAbility SlugBite { get { return GetAbilityObject(718); } }
        public IPgAIAbility SlugWebDebuff { get { return GetAbilityObject(719); } }
        public IPgAIAbility SmallishScorpionClawA { get { return GetAbilityObject(720); } }
        public IPgAIAbility SmallishScorpionClawB { get { return GetAbilityObject(721); } }
        public IPgAIAbility SmallishScorpionSting { get { return GetAbilityObject(722); } }
        public IPgAIAbility RootTrap { get { return GetAbilityObject(723); } }
        public IPgAIAbility ElementalFireSlam { get { return GetAbilityObject(724); } }
        public IPgAIAbility ElementalFireball { get { return GetAbilityObject(725); } }
        public IPgAIAbility ElementalHateFireball { get { return GetAbilityObject(726); } }
        public IPgAIAbility ElementalHateFireball2 { get { return GetAbilityObject(727); } }
        public IPgAIAbility ElementalHateFireball3 { get { return GetAbilityObject(728); } }
        public IPgAIAbility ElementalFireballB { get { return GetAbilityObject(729); } }
        public IPgAIAbility FaeIceSpearA { get { return GetAbilityObject(730); } }
        public IPgAIAbility FaeIceSpearB { get { return GetAbilityObject(731); } }
        public IPgAIAbility FaeColdSphere { get { return GetAbilityObject(732); } }
        public IPgAIAbility LethargyPuckFrostbite { get { return GetAbilityObject(733); } }
        public IPgAIAbility LethargyPuckRage { get { return GetAbilityObject(734); } }
        public IPgAIAbility BeeStab_Pet { get { return GetAbilityObject(735); } }
        public IPgAIAbility BeeInject_Pet1 { get { return GetAbilityObject(736); } }
        public IPgAIAbility BeeInject_Pet2 { get { return GetAbilityObject(737); } }
        public IPgAIAbility BeeInject_Pet3 { get { return GetAbilityObject(738); } }
        public IPgAIAbility BeeInject_Pet4 { get { return GetAbilityObject(739); } }
        public IPgAIAbility BeeInject_Pet5 { get { return GetAbilityObject(740); } }
        public IPgAIAbility BeeInject_Pet6 { get { return GetAbilityObject(741); } }
        public IPgAIAbility BeeVuln_Pet1 { get { return GetAbilityObject(742); } }
        public IPgAIAbility BeeVuln_Pet2 { get { return GetAbilityObject(743); } }
        public IPgAIAbility BeeVuln_Pet3 { get { return GetAbilityObject(744); } }
        public IPgAIAbility BeeVuln_Pet4 { get { return GetAbilityObject(745); } }
        public IPgAIAbility WaspShoot_Pet { get { return GetAbilityObject(746); } }
        public IPgAIAbility WaspBlast_Pet1 { get { return GetAbilityObject(747); } }
        public IPgAIAbility WaspBlast_Pet2 { get { return GetAbilityObject(748); } }
        public IPgAIAbility WaspBlast_Pet3 { get { return GetAbilityObject(749); } }
        public IPgAIAbility WaspBlast_Pet4 { get { return GetAbilityObject(750); } }
        public IPgAIAbility WaspBlast_Pet5 { get { return GetAbilityObject(751); } }
        public IPgAIAbility WaspBlast_Pet6 { get { return GetAbilityObject(752); } }
        public IPgAIAbility WaspSlow_Pet1 { get { return GetAbilityObject(753); } }
        public IPgAIAbility WaspSlow_Pet2 { get { return GetAbilityObject(754); } }
        public IPgAIAbility WaspSlow_Pet3 { get { return GetAbilityObject(755); } }
        public IPgAIAbility WaspSlow_Pet4 { get { return GetAbilityObject(756); } }
        public IPgAIAbility WaspStab_Pet { get { return GetAbilityObject(757); } }
        public IPgAIAbility WaspDebuff_Pet1 { get { return GetAbilityObject(758); } }
        public IPgAIAbility WaspDebuff_Pet2 { get { return GetAbilityObject(759); } }
        public IPgAIAbility WaspDebuff_Pet3 { get { return GetAbilityObject(760); } }
        public IPgAIAbility WaspDebuff_Pet4 { get { return GetAbilityObject(761); } }
        public IPgAIAbility WaspDebuff_Pet5 { get { return GetAbilityObject(762); } }
        public IPgAIAbility WaspDebuff_Pet6 { get { return GetAbilityObject(763); } }
        public IPgAIAbility WaspBurst_Pet1 { get { return GetAbilityObject(764); } }
        public IPgAIAbility WaspBurst_Pet2 { get { return GetAbilityObject(765); } }
        public IPgAIAbility WaspBurst_Pet3 { get { return GetAbilityObject(766); } }
        public IPgAIAbility WaspBurst_Pet4 { get { return GetAbilityObject(767); } }
        public IPgAIAbility PhoenixPeck { get { return GetAbilityObject(768); } }
        public IPgAIAbility PhoenixFireball { get { return GetAbilityObject(769); } }
        public IPgAIAbility PhoenixBlast { get { return GetAbilityObject(770); } }
        public IPgAIAbility BeeStab { get { return GetAbilityObject(771); } }
        public IPgAIAbility BeeInject { get { return GetAbilityObject(772); } }
        public IPgAIAbility BeeShoot { get { return GetAbilityObject(773); } }
        public IPgAIAbility BeeShootB { get { return GetAbilityObject(774); } }
        public IPgAIAbility WaspStab { get { return GetAbilityObject(775); } }
        public IPgAIAbility WaspRageStab { get { return GetAbilityObject(776); } }
        public IPgAIAbility WaspRangedDebuff { get { return GetAbilityObject(777); } }
        public IPgAIAbility WaspRangedDebuffB { get { return GetAbilityObject(778); } }
        public IPgAIAbility WaspRangedDebuffC { get { return GetAbilityObject(779); } }
        public IPgAIAbility WaspShoot { get { return GetAbilityObject(780); } }
        public IPgAIAbility WaspRageBurst { get { return GetAbilityObject(781); } }
        public IPgAIAbility WaspSlowA { get { return GetAbilityObject(782); } }
        public IPgAIAbility WaspSlowB { get { return GetAbilityObject(783); } }
        public IPgAIAbility PixieSlash { get { return GetAbilityObject(784); } }
        public IPgAIAbility PixieRageSlash { get { return GetAbilityObject(785); } }
        public IPgAIAbility StunTrapExplode { get { return GetAbilityObject(786); } }
        public IPgAIAbility SpiderIncubate1 { get { return GetAbilityObject(787); } }
        public IPgAIAbility SpiderIncubate2 { get { return GetAbilityObject(788); } }
        public IPgAIAbility RanalonHeal1 { get { return GetAbilityObject(789); } }
        public IPgAIAbility RanalonHeal2 { get { return GetAbilityObject(790); } }
        public IPgAIAbility RanalonSelfBuff1 { get { return GetAbilityObject(791); } }
        public IPgAIAbility RanalonSelfBuff2 { get { return GetAbilityObject(792); } }
        public IPgAIAbility ScrayStab1 { get { return GetAbilityObject(793); } }
        public IPgAIAbility ScrayStab2 { get { return GetAbilityObject(794); } }
        public IPgAIAbility WaspIceStab_Pet { get { return GetAbilityObject(795); } }
        public IPgAIAbility WaspIceSlow_Pet1 { get { return GetAbilityObject(796); } }
        public IPgAIAbility WaspIceSlow_Pet2 { get { return GetAbilityObject(797); } }
        public IPgAIAbility WaspIceSlow_Pet3 { get { return GetAbilityObject(798); } }
        public IPgAIAbility WaspIceSlow_Pet4 { get { return GetAbilityObject(799); } }
        public IPgAIAbility WaspIceStab { get { return GetAbilityObject(800); } }
        public IPgAIAbility WaspIceRageStab { get { return GetAbilityObject(801); } }
        public IPgAIAbility WaspIceSlowA { get { return GetAbilityObject(802); } }
        public IPgAIAbility WaspIceSlowB { get { return GetAbilityObject(803); } }
        public IPgAIAbility PhoenixFlash { get { return GetAbilityObject(804); } }
        public IPgAIAbility KrakenRageCurse { get { return GetAbilityObject(805); } }
        public IPgAIAbility HealingAura { get { return GetAbilityObject(806); } }
        public IPgAIAbility FaeConduitHeal { get { return GetAbilityObject(807); } }
        public IPgAIAbility BigCatRoot_Pet5 { get { return GetAbilityObject(808); } }
        public IPgAIAbility BigCatRoot_Pet6 { get { return GetAbilityObject(809); } }
        public IPgAIAbility BearStun_Pet5 { get { return GetAbilityObject(810); } }
        public IPgAIAbility BearStun_Pet6 { get { return GetAbilityObject(811); } }
        public IPgAIAbility PhoenixClaw { get { return GetAbilityObject(812); } }
        public IPgAIAbility GoatBite { get { return GetAbilityObject(813); } }
        public IPgAIAbility GoatKick { get { return GetAbilityObject(814); } }
        public IPgAIAbility GoatSmite { get { return GetAbilityObject(815); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
           { "AcidAuraBall1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AcidAuraBall1 as IObjectContentGenerator } },
           { "AcidAuraBall2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AcidAuraBall2 as IObjectContentGenerator } },
           { "AcidAuraBall3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AcidAuraBall3 as IObjectContentGenerator } },
           { "AcidAuraBall4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AcidAuraBall4 as IObjectContentGenerator } },
           { "AcidBall1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AcidBall1 as IObjectContentGenerator } },
           { "AcidBall2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AcidBall2 as IObjectContentGenerator } },
           { "AcidExplosion1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AcidExplosion1 as IObjectContentGenerator } },
           { "AcidExplosion2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AcidExplosion2 as IObjectContentGenerator } },
           { "AcidSpew1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AcidSpew1 as IObjectContentGenerator } },
           { "AlienDog_Punch", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AlienDog_Punch as IObjectContentGenerator } },
           { "AlienDog_Punch2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AlienDog_Punch2 as IObjectContentGenerator } },
           { "AlienDog_RagePunch", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AlienDog_RagePunch as IObjectContentGenerator } },
           { "AnimalBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AnimalBite as IObjectContentGenerator } },
           { "AnimalClaw", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AnimalClaw as IObjectContentGenerator } },
           { "AnimalFlee", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AnimalFlee as IObjectContentGenerator } },
           { "AnimalHeal", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AnimalHeal as IObjectContentGenerator } },
           { "AnimalHeal2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AnimalHeal2 as IObjectContentGenerator } },
           { "AnimalHeal3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AnimalHeal3 as IObjectContentGenerator } },
           { "AnimalHoofFieryFrontKick", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AnimalHoofFieryFrontKick as IObjectContentGenerator } },
           { "AnimalHoofFieryFrontKick2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AnimalHoofFieryFrontKick2 as IObjectContentGenerator } },
           { "AnimalHoofFrontKick", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AnimalHoofFrontKick as IObjectContentGenerator } },
           { "AnimalHoofRageKick", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AnimalHoofRageKick as IObjectContentGenerator } },
           { "AnimalHoofRageKick2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AnimalHoofRageKick2 as IObjectContentGenerator } },
           { "AnimalOmegaBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AnimalOmegaBite as IObjectContentGenerator } },
           { "AnimalOmegaBite2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => AnimalOmegaBite2 as IObjectContentGenerator } },
           { "BallistaFire", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BallistaFire as IObjectContentGenerator } },
           { "BallistaFire_Long", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BallistaFire_Long as IObjectContentGenerator } },
           { "BarghestBiteA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BarghestBiteA as IObjectContentGenerator } },
           { "BarghestBiteB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BarghestBiteB as IObjectContentGenerator } },
           { "BarghestDebuff", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BarghestDebuff as IObjectContentGenerator } },
           { "BarutiWrapA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BarutiWrapA as IObjectContentGenerator } },
           { "BarutiWrapB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BarutiWrapB as IObjectContentGenerator } },
           { "BarutiWrapRage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BarutiWrapRage as IObjectContentGenerator } },
           { "BasiliskCastPerching", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BasiliskCastPerching as IObjectContentGenerator } },
           { "BasiliskClawA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BasiliskClawA as IObjectContentGenerator } },
           { "BasiliskClawB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BasiliskClawB as IObjectContentGenerator } },
           { "BasiliskDebuff", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BasiliskDebuff as IObjectContentGenerator } },
           { "BasiliskToxicBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BasiliskToxicBite as IObjectContentGenerator } },
           { "BatIllusionBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BatIllusionBite as IObjectContentGenerator } },
           { "BatIllusionSlashA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BatIllusionSlashA as IObjectContentGenerator } },
           { "BatIllusionSlashB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BatIllusionSlashB as IObjectContentGenerator } },
           { "BearBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearBite as IObjectContentGenerator } },
           { "BearBite_Pet", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearBite_Pet as IObjectContentGenerator } },
           { "BearClaw_Pet", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearClaw_Pet as IObjectContentGenerator } },
           { "BearCrush", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearCrush as IObjectContentGenerator } },
           { "BearSelfHeal_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearSelfHeal_Pet1 as IObjectContentGenerator } },
           { "BearSelfHeal_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearSelfHeal_Pet2 as IObjectContentGenerator } },
           { "BearSelfHeal_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearSelfHeal_Pet3 as IObjectContentGenerator } },
           { "BearSelfHeal_Pet4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearSelfHeal_Pet4 as IObjectContentGenerator } },
           { "BearSelfHeal_Pet5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearSelfHeal_Pet5 as IObjectContentGenerator } },
           { "BearSelfHeal_Pet6", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearSelfHeal_Pet6 as IObjectContentGenerator } },
           { "BearStun_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearStun_Pet1 as IObjectContentGenerator } },
           { "BearStun_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearStun_Pet2 as IObjectContentGenerator } },
           { "BearStun_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearStun_Pet3 as IObjectContentGenerator } },
           { "BearStun_Pet4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearStun_Pet4 as IObjectContentGenerator } },
           { "BearTaunt_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearTaunt_Pet1 as IObjectContentGenerator } },
           { "BearTaunt_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearTaunt_Pet2 as IObjectContentGenerator } },
           { "BearTaunt_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearTaunt_Pet3 as IObjectContentGenerator } },
           { "BearTaunt_Pet4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearTaunt_Pet4 as IObjectContentGenerator } },
           { "BearTaunt_Pet5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearTaunt_Pet5 as IObjectContentGenerator } },
           { "BearTaunt_Pet6", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearTaunt_Pet6 as IObjectContentGenerator } },
           { "BearUltra_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearUltra_Pet1 as IObjectContentGenerator } },
           { "BearUltra_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearUltra_Pet2 as IObjectContentGenerator } },
           { "BearUltra_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearUltra_Pet3 as IObjectContentGenerator } },
           { "BearUltra_Pet4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearUltra_Pet4 as IObjectContentGenerator } },
           { "BearUltra_Pet5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearUltra_Pet5 as IObjectContentGenerator } },
           { "BearUltra_Pet6", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearUltra_Pet6 as IObjectContentGenerator } },
           { "BearWarmth_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearWarmth_Pet1 as IObjectContentGenerator } },
           { "BearWarmth_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearWarmth_Pet2 as IObjectContentGenerator } },
           { "BearWarmth_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearWarmth_Pet3 as IObjectContentGenerator } },
           { "BearWarmth_Pet4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearWarmth_Pet4 as IObjectContentGenerator } },
           { "BearWarmth_Pet5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearWarmth_Pet5 as IObjectContentGenerator } },
           { "BearWarmth_Pet6", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BearWarmth_Pet6 as IObjectContentGenerator } },
           { "BigCatClaw", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatClaw as IObjectContentGenerator } },
           { "BigCatClaw_Pet", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatClaw_Pet as IObjectContentGenerator } },
           { "BigCatDebuff", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatDebuff as IObjectContentGenerator } },
           { "BigCatHeal_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatHeal_Pet1 as IObjectContentGenerator } },
           { "BigCatHeal_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatHeal_Pet2 as IObjectContentGenerator } },
           { "BigCatHeal_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatHeal_Pet3 as IObjectContentGenerator } },
           { "BigCatHeal_Pet4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatHeal_Pet4 as IObjectContentGenerator } },
           { "BigCatHeal_Pet5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatHeal_Pet5 as IObjectContentGenerator } },
           { "BigCatHeal_Pet6", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatHeal_Pet6 as IObjectContentGenerator } },
           { "BigCatKill_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatKill_Pet1 as IObjectContentGenerator } },
           { "BigCatKill_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatKill_Pet2 as IObjectContentGenerator } },
           { "BigCatKill_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatKill_Pet3 as IObjectContentGenerator } },
           { "BigCatKill_Pet4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatKill_Pet4 as IObjectContentGenerator } },
           { "BigCatKill_Pet5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatKill_Pet5 as IObjectContentGenerator } },
           { "BigCatKill_Pet6", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatKill_Pet6 as IObjectContentGenerator } },
           { "BigCatPounce", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatPounce as IObjectContentGenerator } },
           { "BigCatPounce_Pet", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatPounce_Pet as IObjectContentGenerator } },
           { "BigCatRagePounce", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatRagePounce as IObjectContentGenerator } },
           { "BigCatRoot_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatRoot_Pet1 as IObjectContentGenerator } },
           { "BigCatRoot_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatRoot_Pet2 as IObjectContentGenerator } },
           { "BigCatRoot_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatRoot_Pet3 as IObjectContentGenerator } },
           { "BigCatRoot_Pet4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatRoot_Pet4 as IObjectContentGenerator } },
           { "BigCatUltraKill_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatUltraKill_Pet1 as IObjectContentGenerator } },
           { "BigCatUltraKill_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatUltraKill_Pet2 as IObjectContentGenerator } },
           { "BigCatUltraKill_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatUltraKill_Pet3 as IObjectContentGenerator } },
           { "BigCatUltraKill_Pet4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatUltraKill_Pet4 as IObjectContentGenerator } },
           { "BigCatUltraKill_Pet5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatUltraKill_Pet5 as IObjectContentGenerator } },
           { "BigCatUltraKill_Pet6", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatUltraKill_Pet6 as IObjectContentGenerator } },
           { "BigCatVuln_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatVuln_Pet1 as IObjectContentGenerator } },
           { "BigCatVuln_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatVuln_Pet2 as IObjectContentGenerator } },
           { "BigCatVuln_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatVuln_Pet3 as IObjectContentGenerator } },
           { "BigCatVuln_Pet4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigCatVuln_Pet4 as IObjectContentGenerator } },
           { "BigGolemFling", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigGolemFling as IObjectContentGenerator } },
           { "BigGolemFlingBoss", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigGolemFlingBoss as IObjectContentGenerator } },
           { "BigGolemFlingBoss2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigGolemFlingBoss2 as IObjectContentGenerator } },
           { "BigGolemHitA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigGolemHitA as IObjectContentGenerator } },
           { "BigGolemHitA-NoDisable", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigGolemHitA_NoDisable as IObjectContentGenerator } },
           { "BigGolemHitB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigGolemHitB as IObjectContentGenerator } },
           { "BigGolemHitB-NoDisable", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigGolemHitB_NoDisable as IObjectContentGenerator } },
           { "BigGolemPerchFix", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigGolemPerchFix as IObjectContentGenerator } },
           { "BigGolemSummonFireSnake", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigGolemSummonFireSnake as IObjectContentGenerator } },
           { "BigHeadCurseball", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BigHeadCurseball as IObjectContentGenerator } },
           { "BitingVineAppear", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BitingVineAppear as IObjectContentGenerator } },
           { "BitingVineBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BitingVineBite as IObjectContentGenerator } },
           { "BitingVineCast", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BitingVineCast as IObjectContentGenerator } },
           { "BitingVineDisappear", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BitingVineDisappear as IObjectContentGenerator } },
           { "BitingVineSpit", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BitingVineSpit as IObjectContentGenerator } },
           { "BitingVineSpitB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BitingVineSpitB as IObjectContentGenerator } },
           { "BleddynHowl", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BleddynHowl as IObjectContentGenerator } },
           { "BossMegaHammer", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BossMegaHammer as IObjectContentGenerator } },
           { "BossMegaHammer2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BossMegaHammer2 as IObjectContentGenerator } },
           { "BossMegaRageHammer", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BossMegaRageHammer as IObjectContentGenerator } },
           { "BossMegaSword1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BossMegaSword1 as IObjectContentGenerator } },
           { "BossMegaSword2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BossMegaSword2 as IObjectContentGenerator } },
           { "BossSlime_SummonSlime1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BossSlime_SummonSlime1 as IObjectContentGenerator } },
           { "BossSlime_SummonSlime4Elite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BossSlime_SummonSlime4Elite as IObjectContentGenerator } },
           { "BossSlimeKick", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BossSlimeKick as IObjectContentGenerator } },
           { "BossSlimeKick2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BossSlimeKick2 as IObjectContentGenerator } },
           { "BossSlimeKick2B", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BossSlimeKick2B as IObjectContentGenerator } },
           { "BossSlimeKickB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BossSlimeKickB as IObjectContentGenerator } },
           { "BrainBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BrainBite as IObjectContentGenerator } },
           { "BrainDrain", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BrainDrain as IObjectContentGenerator } },
           { "BrainDrain2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => BrainDrain2 as IObjectContentGenerator } },
           { "CiervosDarknessBomb", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => CiervosDarknessBomb as IObjectContentGenerator } },
           { "CiervosNightmareHoof", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => CiervosNightmareHoof as IObjectContentGenerator } },
           { "ClaudiaBlizzard", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ClaudiaBlizzard as IObjectContentGenerator } },
           { "ClaudiaIceSpear", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ClaudiaIceSpear as IObjectContentGenerator } },
           { "ClaudiaTundraSpikes", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ClaudiaTundraSpikes as IObjectContentGenerator } },
           { "CockatriceParalyze", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => CockatriceParalyze as IObjectContentGenerator } },
           { "CockatricePeck", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => CockatricePeck as IObjectContentGenerator } },
           { "CockatriceTailWhip", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => CockatriceTailWhip as IObjectContentGenerator } },
           { "ColdAuraBurst", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ColdAuraBurst as IObjectContentGenerator } },
           { "ColdSphereBurst", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ColdSphereBurst as IObjectContentGenerator } },
           { "ColdSphereFreezeBurst", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ColdSphereFreezeBurst as IObjectContentGenerator } },
           { "CultistArrow1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => CultistArrow1 as IObjectContentGenerator } },
           { "CultistArrow2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => CultistArrow2 as IObjectContentGenerator } },
           { "CultistOmegaArrow", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => CultistOmegaArrow as IObjectContentGenerator } },
           { "CultistSword1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => CultistSword1 as IObjectContentGenerator } },
           { "CultistSword2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => CultistSword2 as IObjectContentGenerator } },
           { "CultistSwordStun", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => CultistSwordStun as IObjectContentGenerator } },
           { "DeathRay", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DeathRay as IObjectContentGenerator } },
           { "DementiaPuckCurse", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DementiaPuckCurse as IObjectContentGenerator } },
           { "Dinobite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Dinobite as IObjectContentGenerator } },
           { "Dinobite2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Dinobite2 as IObjectContentGenerator } },
           { "Dinoslash", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Dinoslash as IObjectContentGenerator } },
           { "Dinoslash2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Dinoslash2 as IObjectContentGenerator } },
           { "Dinowhap", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Dinowhap as IObjectContentGenerator } },
           { "DragonWormBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DragonWormBite as IObjectContentGenerator } },
           { "DragonWormEscape", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DragonWormEscape as IObjectContentGenerator } },
           { "DragonWormRage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DragonWormRage as IObjectContentGenerator } },
           { "DragonWormSmack", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DragonWormSmack as IObjectContentGenerator } },
           { "DragonWormSpitElectricity", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DragonWormSpitElectricity as IObjectContentGenerator } },
           { "DragonWormSpitFire", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DragonWormSpitFire as IObjectContentGenerator } },
           { "DroachBiteA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DroachBiteA as IObjectContentGenerator } },
           { "DroachBiteB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DroachBiteB as IObjectContentGenerator } },
           { "DroachBreatheFire", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DroachBreatheFire as IObjectContentGenerator } },
           { "DroachFireball", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DroachFireball as IObjectContentGenerator } },
           { "DroachFireballPerching", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DroachFireballPerching as IObjectContentGenerator } },
           { "DroachLightning", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DroachLightning as IObjectContentGenerator } },
           { "DroachLightningPerching", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DroachLightningPerching as IObjectContentGenerator } },
           { "DroachShockingKnockback", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DroachShockingKnockback as IObjectContentGenerator } },
           { "DruidHealingSanctuaryHeal", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => DruidHealingSanctuaryHeal as IObjectContentGenerator } },
           { "ElectricityAura1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ElectricityAura1 as IObjectContentGenerator } },
           { "ElectricityAuraBolt1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ElectricityAuraBolt1 as IObjectContentGenerator } },
           { "ElectricPigAoEStun", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ElectricPigAoEStun as IObjectContentGenerator } },
           { "ElectricPigHitAndRun", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ElectricPigHitAndRun as IObjectContentGenerator } },
           { "ElectricPigStun", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ElectricPigStun as IObjectContentGenerator } },
           { "ElementalBees", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ElementalBees as IObjectContentGenerator } },
           { "ElementalBees2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ElementalBees2 as IObjectContentGenerator } },
           { "ElementalSlam", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ElementalSlam as IObjectContentGenerator } },
           { "EnemyMinigolemExplode", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => EnemyMinigolemExplode as IObjectContentGenerator } },
           { "EnemyMinigolemHeal", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => EnemyMinigolemHeal as IObjectContentGenerator } },
           { "EnemyMinigolemPunch", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => EnemyMinigolemPunch as IObjectContentGenerator } },
           { "FaceOfDeathKill", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FaceOfDeathKill as IObjectContentGenerator } },
           { "FaeLightningSmite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FaeLightningSmite as IObjectContentGenerator } },
           { "FaeLightningSmiteHidden", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FaeLightningSmiteHidden as IObjectContentGenerator } },
           { "FaeSwordA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FaeSwordA as IObjectContentGenerator } },
           { "FaeSwordB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FaeSwordB as IObjectContentGenerator } },
           { "FaeSwordKill", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FaeSwordKill as IObjectContentGenerator } },
           { "FireRatBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FireRatBite as IObjectContentGenerator } },
           { "FireRatBite_Pet", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FireRatBite_Pet as IObjectContentGenerator } },
           { "FireRatClaw", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FireRatClaw as IObjectContentGenerator } },
           { "FireRatClaw_Pet", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FireRatClaw_Pet as IObjectContentGenerator } },
           { "FireSnakeExplosion1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FireSnakeExplosion1 as IObjectContentGenerator } },
           { "FireTrapAttack1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FireTrapAttack1 as IObjectContentGenerator } },
           { "FireWallAttack1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FireWallAttack1 as IObjectContentGenerator } },
           { "FireWallDotAttack1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FireWallDotAttack1 as IObjectContentGenerator } },
           { "FlapSkullBigBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FlapSkullBigBite as IObjectContentGenerator } },
           { "FlapSkullBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => FlapSkullBite as IObjectContentGenerator } },
           { "GargoyleBossStun", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GargoyleBossStun as IObjectContentGenerator } },
           { "GargoyleSlamA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GargoyleSlamA as IObjectContentGenerator } },
           { "GargoyleSlamB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GargoyleSlamB as IObjectContentGenerator } },
           { "GargoyleStun", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GargoyleStun as IObjectContentGenerator } },
           { "GazlukPriest1Special", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GazlukPriest1Special as IObjectContentGenerator } },
           { "GazlukPriest2Special", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GazlukPriest2Special as IObjectContentGenerator } },
           { "GazlukPriest3Special", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GazlukPriest3Special as IObjectContentGenerator } },
           { "GhostlyBolt", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GhostlyBolt as IObjectContentGenerator } },
           { "GhostlyBossBurst", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GhostlyBossBurst as IObjectContentGenerator } },
           { "GhostlyBossPunchA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GhostlyBossPunchA as IObjectContentGenerator } },
           { "GhostlyBossPunchB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GhostlyBossPunchB as IObjectContentGenerator } },
           { "GhostlyBurst", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GhostlyBurst as IObjectContentGenerator } },
           { "GhostlyPunchA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GhostlyPunchA as IObjectContentGenerator } },
           { "GhostlyPunchB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GhostlyPunchB as IObjectContentGenerator } },
           { "GhoulClawA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GhoulClawA as IObjectContentGenerator } },
           { "GhoulClawB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GhoulClawB as IObjectContentGenerator } },
           { "GhoulHammerA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GhoulHammerA as IObjectContentGenerator } },
           { "GhoulHammerB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GhoulHammerB as IObjectContentGenerator } },
           { "GhoulSelfBuff", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GhoulSelfBuff as IObjectContentGenerator } },
           { "GiantBatBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GiantBatBite as IObjectContentGenerator } },
           { "GiantBatSlashA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GiantBatSlashA as IObjectContentGenerator } },
           { "GiantBatSlashB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GiantBatSlashB as IObjectContentGenerator } },
           { "GiantBeetleBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GiantBeetleBite as IObjectContentGenerator } },
           { "GiantBeetleBoulderSpit", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GiantBeetleBoulderSpit as IObjectContentGenerator } },
           { "GiantBeetleInject", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GiantBeetleInject as IObjectContentGenerator } },
           { "GiantScorpionClawA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GiantScorpionClawA as IObjectContentGenerator } },
           { "GiantScorpionClawB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GiantScorpionClawB as IObjectContentGenerator } },
           { "GiantScorpionSting", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GiantScorpionSting as IObjectContentGenerator } },
           { "GnasherBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GnasherBite as IObjectContentGenerator } },
           { "GnasherRend", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GnasherRend as IObjectContentGenerator } },
           { "GoblinArmorBuff", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinArmorBuff as IObjectContentGenerator } },
           { "GoblinArrow1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinArrow1 as IObjectContentGenerator } },
           { "GoblinArrow2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinArrow2 as IObjectContentGenerator } },
           { "GoblinBossLightning", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinBossLightning as IObjectContentGenerator } },
           { "GoblinHateZapBall", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinHateZapBall as IObjectContentGenerator } },
           { "GoblinHateZapBall2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinHateZapBall2 as IObjectContentGenerator } },
           { "GoblinHeal1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinHeal1 as IObjectContentGenerator } },
           { "GoblinHeal2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinHeal2 as IObjectContentGenerator } },
           { "GoblinPunch", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinPunch as IObjectContentGenerator } },
           { "GoblinRageArrow1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinRageArrow1 as IObjectContentGenerator } },
           { "GoblinRageArrow2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinRageArrow2 as IObjectContentGenerator } },
           { "GoblinRageSpear1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinRageSpear1 as IObjectContentGenerator } },
           { "GoblinRageSpear2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinRageSpear2 as IObjectContentGenerator } },
           { "GoblinSpear1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinSpear1 as IObjectContentGenerator } },
           { "GoblinSpear2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinSpear2 as IObjectContentGenerator } },
           { "GoblinSpreadZapBall", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinSpreadZapBall as IObjectContentGenerator } },
           { "GoblinZapBall", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GoblinZapBall as IObjectContentGenerator } },
           { "GrimalkinBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GrimalkinBite as IObjectContentGenerator } },
           { "GrimalkinClaw", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GrimalkinClaw as IObjectContentGenerator } },
           { "GrimalkinFlee_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GrimalkinFlee_Pet1 as IObjectContentGenerator } },
           { "GrimalkinFlee_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GrimalkinFlee_Pet2 as IObjectContentGenerator } },
           { "GrimalkinFlee_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GrimalkinFlee_Pet3 as IObjectContentGenerator } },
           { "GrimalkinPuncture", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GrimalkinPuncture as IObjectContentGenerator } },
           { "GrimalkinPuncture_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GrimalkinPuncture_Pet1 as IObjectContentGenerator } },
           { "GrimalkinPuncture_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GrimalkinPuncture_Pet2 as IObjectContentGenerator } },
           { "GrimalkinPuncture_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GrimalkinPuncture_Pet3 as IObjectContentGenerator } },
           { "GrimalkinPuncture_Pet4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GrimalkinPuncture_Pet4 as IObjectContentGenerator } },
           { "GrimalkinPuncture_Pet5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GrimalkinPuncture_Pet5 as IObjectContentGenerator } },
           { "GrimalkinPuncture_Pet6", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => GrimalkinPuncture_Pet6 as IObjectContentGenerator } },
           { "HagAgingScream", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => HagAgingScream as IObjectContentGenerator } },
           { "HagAgingTouch", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => HagAgingTouch as IObjectContentGenerator } },
           { "HealingAura1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => HealingAura1 as IObjectContentGenerator } },
           { "HealingAura2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => HealingAura2 as IObjectContentGenerator } },
           { "HealingAura3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => HealingAura3 as IObjectContentGenerator } },
           { "HealingAura4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => HealingAura4 as IObjectContentGenerator } },
           { "HippoBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => HippoBite as IObjectContentGenerator } },
           { "HippoBiteAndHeal1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => HippoBiteAndHeal1 as IObjectContentGenerator } },
           { "HippogriffBossSlashes", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => HippogriffBossSlashes as IObjectContentGenerator } },
           { "HippogriffPeck", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => HippogriffPeck as IObjectContentGenerator } },
           { "HippogriffSlashes", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => HippogriffSlashes as IObjectContentGenerator } },
           { "HookAcid", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => HookAcid as IObjectContentGenerator } },
           { "HookClaw", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => HookClaw as IObjectContentGenerator } },
           { "HookRage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => HookRage as IObjectContentGenerator } },
           { "IceCockFreeze", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => IceCockFreeze as IObjectContentGenerator } },
           { "IceCockPeck", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => IceCockPeck as IObjectContentGenerator } },
           { "IceSlimeBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => IceSlimeBite as IObjectContentGenerator } },
           { "IceSlimeBiteB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => IceSlimeBiteB as IObjectContentGenerator } },
           { "IceSlimeKick", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => IceSlimeKick as IObjectContentGenerator } },
           { "IceSlimeKickB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => IceSlimeKickB as IObjectContentGenerator } },
           { "IcyCocoon", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => IcyCocoon as IObjectContentGenerator } },
           { "IcyCocoon2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => IcyCocoon2 as IObjectContentGenerator } },
           { "IcySlam", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => IcySlam as IObjectContentGenerator } },
           { "InjectorBugBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => InjectorBugBite as IObjectContentGenerator } },
           { "InjectorBugInject", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => InjectorBugInject as IObjectContentGenerator } },
           { "InjectorBugInject2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => InjectorBugInject2 as IObjectContentGenerator } },
           { "KhyrulekCurseBall", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => KhyrulekCurseBall as IObjectContentGenerator } },
           { "KrakenBabyBeak", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => KrakenBabyBeak as IObjectContentGenerator } },
           { "KrakenBabyRage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => KrakenBabyRage as IObjectContentGenerator } },
           { "KrakenBabySlam", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => KrakenBabySlam as IObjectContentGenerator } },
           { "KrakenBeak", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => KrakenBeak as IObjectContentGenerator } },
           { "KrakenRage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => KrakenRage as IObjectContentGenerator } },
           { "KrakenSlam", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => KrakenSlam as IObjectContentGenerator } },
           { "LamiaMindControl", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => LamiaMindControl as IObjectContentGenerator } },
           { "LamiaRage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => LamiaRage as IObjectContentGenerator } },
           { "ManticoreBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ManticoreBite as IObjectContentGenerator } },
           { "ManticoreClaw", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ManticoreClaw as IObjectContentGenerator } },
           { "ManticoreSting1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ManticoreSting1 as IObjectContentGenerator } },
           { "Manticoresting2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Manticoresting2 as IObjectContentGenerator } },
           { "MantisAcidBurst", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MantisAcidBurst as IObjectContentGenerator } },
           { "MantisBlast", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MantisBlast as IObjectContentGenerator } },
           { "MantisClaw", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MantisClaw as IObjectContentGenerator } },
           { "MantisRage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MantisRage as IObjectContentGenerator } },
           { "MantisSwipe", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MantisSwipe as IObjectContentGenerator } },
           { "MaronesaInfect", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MaronesaInfect as IObjectContentGenerator } },
           { "MaronesaStomp", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MaronesaStomp as IObjectContentGenerator } },
           { "MinigolemAoEHeal", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemAoEHeal as IObjectContentGenerator } },
           { "MinigolemAoEHeal2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemAoEHeal2 as IObjectContentGenerator } },
           { "MinigolemAoEHeal3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemAoEHeal3 as IObjectContentGenerator } },
           { "MinigolemAoEHeal4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemAoEHeal4 as IObjectContentGenerator } },
           { "MinigolemAoEHeal5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemAoEHeal5 as IObjectContentGenerator } },
           { "MinigolemAoEPower", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemAoEPower as IObjectContentGenerator } },
           { "MinigolemAoEPower2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemAoEPower2 as IObjectContentGenerator } },
           { "MinigolemAoEPower3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemAoEPower3 as IObjectContentGenerator } },
           { "MinigolemAoEPower4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemAoEPower4 as IObjectContentGenerator } },
           { "MinigolemAoEPower5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemAoEPower5 as IObjectContentGenerator } },
           { "MinigolemBombToss", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemBombToss as IObjectContentGenerator } },
           { "MinigolemBombToss2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemBombToss2 as IObjectContentGenerator } },
           { "MinigolemBombToss3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemBombToss3 as IObjectContentGenerator } },
           { "MinigolemBombToss4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemBombToss4 as IObjectContentGenerator } },
           { "MinigolemBombToss5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemBombToss5 as IObjectContentGenerator } },
           { "MinigolemDoomAdmixture", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemDoomAdmixture as IObjectContentGenerator } },
           { "MinigolemDoomAdmixture2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemDoomAdmixture2 as IObjectContentGenerator } },
           { "MinigolemDoomAdmixture3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemDoomAdmixture3 as IObjectContentGenerator } },
           { "MinigolemDoomAdmixture4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemDoomAdmixture4 as IObjectContentGenerator } },
           { "MinigolemDoomAdmixture5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemDoomAdmixture5 as IObjectContentGenerator } },
           { "MinigolemFireBalm1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemFireBalm1 as IObjectContentGenerator } },
           { "MinigolemFireBalm2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemFireBalm2 as IObjectContentGenerator } },
           { "MinigolemFireBalm3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemFireBalm3 as IObjectContentGenerator } },
           { "MinigolemFireBalm4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemFireBalm4 as IObjectContentGenerator } },
           { "MinigolemFireBalm5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemFireBalm5 as IObjectContentGenerator } },
           { "MinigolemHasteConcoction1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemHasteConcoction1 as IObjectContentGenerator } },
           { "MinigolemHasteConcoction2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemHasteConcoction2 as IObjectContentGenerator } },
           { "MinigolemHasteConcoction3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemHasteConcoction3 as IObjectContentGenerator } },
           { "MinigolemHeal", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemHeal as IObjectContentGenerator } },
           { "MinigolemHeal2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemHeal2 as IObjectContentGenerator } },
           { "MinigolemHeal3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemHeal3 as IObjectContentGenerator } },
           { "MinigolemHeal4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemHeal4 as IObjectContentGenerator } },
           { "MinigolemHeal5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemHeal5 as IObjectContentGenerator } },
           { "MinigolemPunch", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemPunch as IObjectContentGenerator } },
           { "MinigolemPunch2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemPunch2 as IObjectContentGenerator } },
           { "MinigolemPunch3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemPunch3 as IObjectContentGenerator } },
           { "MinigolemPunch4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemPunch4 as IObjectContentGenerator } },
           { "MinigolemPunch5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemPunch5 as IObjectContentGenerator } },
           { "MinigolemRageAcidToss1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemRageAcidToss1 as IObjectContentGenerator } },
           { "MinigolemRageAcidToss2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemRageAcidToss2 as IObjectContentGenerator } },
           { "MinigolemRageAcidToss3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemRageAcidToss3 as IObjectContentGenerator } },
           { "MinigolemRageAcidToss4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemRageAcidToss4 as IObjectContentGenerator } },
           { "MinigolemRageAcidToss5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemRageAcidToss5 as IObjectContentGenerator } },
           { "MinigolemRageAoEHeal1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemRageAoEHeal1 as IObjectContentGenerator } },
           { "MinigolemRageAoEHeal2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemRageAoEHeal2 as IObjectContentGenerator } },
           { "MinigolemRageAoEHeal3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemRageAoEHeal3 as IObjectContentGenerator } },
           { "MinigolemRageAoEHeal4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemRageAoEHeal4 as IObjectContentGenerator } },
           { "MinigolemRageAoEHeal5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemRageAoEHeal5 as IObjectContentGenerator } },
           { "MinigolemSelfDestruct", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemSelfDestruct as IObjectContentGenerator } },
           { "MinigolemSelfDestruct2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemSelfDestruct2 as IObjectContentGenerator } },
           { "MinigolemSelfDestruct3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemSelfDestruct3 as IObjectContentGenerator } },
           { "MinigolemSelfDestruct4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemSelfDestruct4 as IObjectContentGenerator } },
           { "MinigolemSelfDestruct5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemSelfDestruct5 as IObjectContentGenerator } },
           { "MinigolemSelfSacrifice", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemSelfSacrifice as IObjectContentGenerator } },
           { "MinigolemSelfSacrifice2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemSelfSacrifice2 as IObjectContentGenerator } },
           { "MinigolemSelfSacrifice3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemSelfSacrifice3 as IObjectContentGenerator } },
           { "MinigolemSelfSacrifice4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemSelfSacrifice4 as IObjectContentGenerator } },
           { "MinigolemSelfSacrifice5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinigolemSelfSacrifice5 as IObjectContentGenerator } },
           { "MinotaurBossRageClub", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinotaurBossRageClub as IObjectContentGenerator } },
           { "MinotaurBoulder", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinotaurBoulder as IObjectContentGenerator } },
           { "MinotaurClub", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinotaurClub as IObjectContentGenerator } },
           { "MinotaurRageClub", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MinotaurRageClub as IObjectContentGenerator } },
           { "MonsterWerewolfHowl", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MonsterWerewolfHowl as IObjectContentGenerator } },
           { "MonsterWerewolfPackAttack", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MonsterWerewolfPackAttack as IObjectContentGenerator } },
           { "MonsterWerewolfPouncingRake", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MonsterWerewolfPouncingRake as IObjectContentGenerator } },
           { "MummySlamA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MummySlamA as IObjectContentGenerator } },
           { "MummySlamB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MummySlamB as IObjectContentGenerator } },
           { "MummySlamCombo", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MummySlamCombo as IObjectContentGenerator } },
           { "MummyWrapA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MummyWrapA as IObjectContentGenerator } },
           { "MummyWrapB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MummyWrapB as IObjectContentGenerator } },
           { "MummyWrapRage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MummyWrapRage as IObjectContentGenerator } },
           { "MushroomMonster_Bite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MushroomMonster_Bite as IObjectContentGenerator } },
           { "MushroomMonster_SpawnSpit1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MushroomMonster_SpawnSpit1 as IObjectContentGenerator } },
           { "MushroomMonster_SpawnSpit2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MushroomMonster_SpawnSpit2 as IObjectContentGenerator } },
           { "MushroomMonster_SpawnSuperSpit1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MushroomMonster_SpawnSuperSpit1 as IObjectContentGenerator } },
           { "MushroomMonster_SpawnSuperSpit2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MushroomMonster_SpawnSuperSpit2 as IObjectContentGenerator } },
           { "MushroomMonster_Spit1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MushroomMonster_Spit1 as IObjectContentGenerator } },
           { "MushroomMonster_Spit2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MushroomMonster_Spit2 as IObjectContentGenerator } },
           { "MushroomMonster_SummonMushroomSpawn1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MushroomMonster_SummonMushroomSpawn1 as IObjectContentGenerator } },
           { "MushroomMonster_SummonMushroomSpawn2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MushroomMonster_SummonMushroomSpawn2 as IObjectContentGenerator } },
           { "MushroomMonster_SummonMushroomSpawn3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => MushroomMonster_SummonMushroomSpawn3 as IObjectContentGenerator } },
           { "Myconian_Bash", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Myconian_Bash as IObjectContentGenerator } },
           { "Myconian_BossBash", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Myconian_BossBash as IObjectContentGenerator } },
           { "Myconian_Drain", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Myconian_Drain as IObjectContentGenerator } },
           { "Myconian_Mindspores", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Myconian_Mindspores as IObjectContentGenerator } },
           { "Myconian_Mindspores_Permanent", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Myconian_Mindspores_Permanent as IObjectContentGenerator } },
           { "Myconian_Shock", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Myconian_Shock as IObjectContentGenerator } },
           { "Myconian_TidalCurse", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Myconian_TidalCurse as IObjectContentGenerator } },
           { "NecroDarknessWave", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => NecroDarknessWave as IObjectContentGenerator } },
           { "NecroDeathsHold", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => NecroDeathsHold as IObjectContentGenerator } },
           { "NecroPainBubble", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => NecroPainBubble as IObjectContentGenerator } },
           { "NecroSpark", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => NecroSpark as IObjectContentGenerator } },
           { "NecroSparkPerching", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => NecroSparkPerching as IObjectContentGenerator } },
           { "NightmareDarknessBomb", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => NightmareDarknessBomb as IObjectContentGenerator } },
           { "NightmareHoof", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => NightmareHoof as IObjectContentGenerator } },
           { "NpcBlockingStance", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => NpcBlockingStance as IObjectContentGenerator } },
           { "NpcDoubleHitCurse", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => NpcDoubleHitCurse as IObjectContentGenerator } },
           { "NpcHeadcracker", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => NpcHeadcracker as IObjectContentGenerator } },
           { "NpcSmash", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => NpcSmash as IObjectContentGenerator } },
           { "OgreClubA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OgreClubA as IObjectContentGenerator } },
           { "OgreClubB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OgreClubB as IObjectContentGenerator } },
           { "OgreStun", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OgreStun as IObjectContentGenerator } },
           { "OgreThrow", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OgreThrow as IObjectContentGenerator } },
           { "OrcAreaHalberdAttack", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcAreaHalberdAttack as IObjectContentGenerator } },
           { "OrcAreaHalberdBoss", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcAreaHalberdBoss as IObjectContentGenerator } },
           { "OrcArrow1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcArrow1 as IObjectContentGenerator } },
           { "OrcArrow2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcArrow2 as IObjectContentGenerator } },
           { "OrcDarknessBall", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcDarknessBall as IObjectContentGenerator } },
           { "OrcDeathsHold", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcDeathsHold as IObjectContentGenerator } },
           { "OrcDebuffArrow", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcDebuffArrow as IObjectContentGenerator } },
           { "OrcElectricStun", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcElectricStun as IObjectContentGenerator } },
           { "OrcEvasionBubble", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcEvasionBubble as IObjectContentGenerator } },
           { "OrcExtinguishLife", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcExtinguishLife as IObjectContentGenerator } },
           { "OrcFinishingBlow", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcFinishingBlow as IObjectContentGenerator } },
           { "OrcFinishingBlowFire", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcFinishingBlowFire as IObjectContentGenerator } },
           { "OrcFireball", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcFireball as IObjectContentGenerator } },
           { "OrcFireBolts", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcFireBolts as IObjectContentGenerator } },
           { "OrcHalberdAttack", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcHalberdAttack as IObjectContentGenerator } },
           { "OrcHeal1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcHeal1 as IObjectContentGenerator } },
           { "OrcHeal2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcHeal2 as IObjectContentGenerator } },
           { "OrcHipThrow", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcHipThrow as IObjectContentGenerator } },
           { "OrcKneeKick", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcKneeKick as IObjectContentGenerator } },
           { "OrcKnockbackBolt", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcKnockbackBolt as IObjectContentGenerator } },
           { "OrcLieutenantDebuffTaunt", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcLieutenantDebuffTaunt as IObjectContentGenerator } },
           { "OrcParry", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcParry as IObjectContentGenerator } },
           { "OrcParryFire", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcParryFire as IObjectContentGenerator } },
           { "OrcPunch", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcPunch as IObjectContentGenerator } },
           { "OrcSlice", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcSlice as IObjectContentGenerator } },
           { "OrcSpearAttack", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcSpearAttack as IObjectContentGenerator } },
           { "OrcStaffSmash", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcStaffSmash as IObjectContentGenerator } },
           { "OrcSummonSigil1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcSummonSigil1 as IObjectContentGenerator } },
           { "OrcSummonUrak2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcSummonUrak2 as IObjectContentGenerator } },
           { "OrcSwordSlash", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcSwordSlash as IObjectContentGenerator } },
           { "OrcSwordSlashFire", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcSwordSlashFire as IObjectContentGenerator } },
           { "OrcVenomstrike0", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcVenomstrike0 as IObjectContentGenerator } },
           { "OrcVenomstrike1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcVenomstrike1 as IObjectContentGenerator } },
           { "OrcWaveOfDarkness", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => OrcWaveOfDarkness as IObjectContentGenerator } },
           { "Peck", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Peck as IObjectContentGenerator } },
           { "PetUndeadArrow1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => PetUndeadArrow1 as IObjectContentGenerator } },
           { "PetUndeadArrow2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => PetUndeadArrow2 as IObjectContentGenerator } },
           { "PetUndeadDefensiveBurst", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => PetUndeadDefensiveBurst as IObjectContentGenerator } },
           { "PetUndeadFireballA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => PetUndeadFireballA as IObjectContentGenerator } },
           { "PetUndeadFireballB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => PetUndeadFireballB as IObjectContentGenerator } },
           { "PetUndeadOmegaArrow", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => PetUndeadOmegaArrow as IObjectContentGenerator } },
           { "PetUndeadPunch1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => PetUndeadPunch1 as IObjectContentGenerator } },
           { "PetUndeadSword1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => PetUndeadSword1 as IObjectContentGenerator } },
           { "PetUndeadSword2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => PetUndeadSword2 as IObjectContentGenerator } },
           { "PetUndeadSwordAngry", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => PetUndeadSwordAngry as IObjectContentGenerator } },
           { "RakAcidBomb", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakAcidBomb as IObjectContentGenerator } },
           { "RakAimedShot", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakAimedShot as IObjectContentGenerator } },
           { "RakBarrage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakBarrage as IObjectContentGenerator } },
           { "RakBasicShot", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakBasicShot as IObjectContentGenerator } },
           { "RakBossPerchSlow", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakBossPerchSlow as IObjectContentGenerator } },
           { "RakBossSlow", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakBossSlow as IObjectContentGenerator } },
           { "RakBowBash", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakBowBash as IObjectContentGenerator } },
           { "RakBreatheFire", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakBreatheFire as IObjectContentGenerator } },
           { "RakDecapitate", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakDecapitate as IObjectContentGenerator } },
           { "RakFireball", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakFireball as IObjectContentGenerator } },
           { "RakHackingBlade", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakHackingBlade as IObjectContentGenerator } },
           { "RakHealingMist", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakHealingMist as IObjectContentGenerator } },
           { "RakHookShot", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakHookShot as IObjectContentGenerator } },
           { "RakKick", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakKick as IObjectContentGenerator } },
           { "RakKnee", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakKnee as IObjectContentGenerator } },
           { "RakMindreave", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakMindreave as IObjectContentGenerator } },
           { "RakPainBubble", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakPainBubble as IObjectContentGenerator } },
           { "RakPanicCharge", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakPanicCharge as IObjectContentGenerator } },
           { "RakPoisonArrow", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakPoisonArrow as IObjectContentGenerator } },
           { "RakReconstruct", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakReconstruct as IObjectContentGenerator } },
           { "RakRevitalize", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakRevitalize as IObjectContentGenerator } },
           { "RakRingOfFire", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakRingOfFire as IObjectContentGenerator } },
           { "RakSlash", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakSlash as IObjectContentGenerator } },
           { "RakStaffBlock", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakStaffBlock as IObjectContentGenerator } },
           { "RakStaffHeavy", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakStaffHeavy as IObjectContentGenerator } },
           { "RakStaffHit", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakStaffHit as IObjectContentGenerator } },
           { "RakStaffPin", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakStaffPin as IObjectContentGenerator } },
           { "RakSwordSlash", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakSwordSlash as IObjectContentGenerator } },
           { "RakToxinBomb", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RakToxinBomb as IObjectContentGenerator } },
           { "RanalonDoctrineKeeperBlind", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonDoctrineKeeperBlind as IObjectContentGenerator } },
           { "RanalonDoctrineKeeperStab", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonDoctrineKeeperStab as IObjectContentGenerator } },
           { "RanalonGuardianBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonGuardianBite as IObjectContentGenerator } },
           { "RanalonGuardianBlind", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonGuardianBlind as IObjectContentGenerator } },
           { "RanalonGuardianStab", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonGuardianStab as IObjectContentGenerator } },
           { "RanalonGuardianStabB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonGuardianStabB as IObjectContentGenerator } },
           { "RanalonHeal", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonHeal as IObjectContentGenerator } },
           { "RanalonHit", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonHit as IObjectContentGenerator } },
           { "RanalonHitB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonHitB as IObjectContentGenerator } },
           { "RanalonKick", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonKick as IObjectContentGenerator } },
           { "RanalonRoot", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonRoot as IObjectContentGenerator } },
           { "RanalonSelfBuff", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonSelfBuff as IObjectContentGenerator } },
           { "RanalonSelfBuffElite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonSelfBuffElite as IObjectContentGenerator } },
           { "RanalonTongue", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonTongue as IObjectContentGenerator } },
           { "RanalonZap", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonZap as IObjectContentGenerator } },
           { "RanalonZapB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RanalonZapB as IObjectContentGenerator } },
           { "RatBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatBite as IObjectContentGenerator } },
           { "RatBite_Pet", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatBite_Pet as IObjectContentGenerator } },
           { "RatBurn_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatBurn_Pet1 as IObjectContentGenerator } },
           { "RatBurn_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatBurn_Pet2 as IObjectContentGenerator } },
           { "RatBurn_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatBurn_Pet3 as IObjectContentGenerator } },
           { "RatBurn_Pet4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatBurn_Pet4 as IObjectContentGenerator } },
           { "RatBurn_Pet5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatBurn_Pet5 as IObjectContentGenerator } },
           { "RatBurn_Pet6", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatBurn_Pet6 as IObjectContentGenerator } },
           { "RatClaw", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatClaw as IObjectContentGenerator } },
           { "RatClaw_Pet", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatClaw_Pet as IObjectContentGenerator } },
           { "RatDeRage_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatDeRage_Pet1 as IObjectContentGenerator } },
           { "RatDeRage_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatDeRage_Pet2 as IObjectContentGenerator } },
           { "RatDeRage_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatDeRage_Pet3 as IObjectContentGenerator } },
           { "RatDeRage_Pet4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatDeRage_Pet4 as IObjectContentGenerator } },
           { "RatDeRage_Pet5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatDeRage_Pet5 as IObjectContentGenerator } },
           { "RatDeRage_Pet6", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatDeRage_Pet6 as IObjectContentGenerator } },
           { "RatHeal_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatHeal_Pet1 as IObjectContentGenerator } },
           { "RatHeal_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatHeal_Pet2 as IObjectContentGenerator } },
           { "RatHeal_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatHeal_Pet3 as IObjectContentGenerator } },
           { "RatHeal_Pet4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatHeal_Pet4 as IObjectContentGenerator } },
           { "RatHeal_Pet5", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatHeal_Pet5 as IObjectContentGenerator } },
           { "RatHeal_Pet6", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatHeal_Pet6 as IObjectContentGenerator } },
           { "RatVuln_Pet1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatVuln_Pet1 as IObjectContentGenerator } },
           { "RatVuln_Pet2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatVuln_Pet2 as IObjectContentGenerator } },
           { "RatVuln_Pet3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatVuln_Pet3 as IObjectContentGenerator } },
           { "RatVuln_Pet4", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RatVuln_Pet4 as IObjectContentGenerator } },
           { "ReboundAura1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ReboundAura1 as IObjectContentGenerator } },
           { "RedCrystalBlast", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RedCrystalBlast as IObjectContentGenerator } },
           { "RedCrystalBurst", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RedCrystalBurst as IObjectContentGenerator } },
           { "RhinoBossRage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RhinoBossRage as IObjectContentGenerator } },
           { "RhinoFireball", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RhinoFireball as IObjectContentGenerator } },
           { "RhinoHorn", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RhinoHorn as IObjectContentGenerator } },
           { "RhinoRage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => RhinoRage as IObjectContentGenerator } },
           { "ScrayBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ScrayBite as IObjectContentGenerator } },
           { "ScrayStab", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ScrayStab as IObjectContentGenerator } },
           { "SedgewickMegaSwordAngry", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SedgewickMegaSwordAngry as IObjectContentGenerator } },
           { "SheepBomb1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SheepBomb1 as IObjectContentGenerator } },
           { "SherzatAcidSpit", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SherzatAcidSpit as IObjectContentGenerator } },
           { "SherzatClaw", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SherzatClaw as IObjectContentGenerator } },
           { "SherzatDisintegrate", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SherzatDisintegrate as IObjectContentGenerator } },
           { "Slime_SummonSlime", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Slime_SummonSlime as IObjectContentGenerator } },
           { "SlimeBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SlimeBite as IObjectContentGenerator } },
           { "SlimeBiteB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SlimeBiteB as IObjectContentGenerator } },
           { "SlimeKick", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SlimeKick as IObjectContentGenerator } },
           { "SlimeKickB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SlimeKickB as IObjectContentGenerator } },
           { "SlimeSpit", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SlimeSpit as IObjectContentGenerator } },
           { "SlimeSuperSpit", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SlimeSuperSpit as IObjectContentGenerator } },
           { "SlugPoisonBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SlugPoisonBite as IObjectContentGenerator } },
           { "SlugPoisonBite2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SlugPoisonBite2 as IObjectContentGenerator } },
           { "SlugPoisonBite3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SlugPoisonBite3 as IObjectContentGenerator } },
           { "SlugPoisonRage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SlugPoisonRage as IObjectContentGenerator } },
           { "SlugPoisonRage2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SlugPoisonRage2 as IObjectContentGenerator } },
           { "SlugPoisonRage3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SlugPoisonRage3 as IObjectContentGenerator } },
           { "SnailRage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SnailRage as IObjectContentGenerator } },
           { "SnailRageB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SnailRageB as IObjectContentGenerator } },
           { "SnailRageC", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SnailRageC as IObjectContentGenerator } },
           { "SnailStrike", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SnailStrike as IObjectContentGenerator } },
           { "SpiderBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SpiderBite as IObjectContentGenerator } },
           { "SpiderBossFreePin", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SpiderBossFreePin as IObjectContentGenerator } },
           { "SpiderFireball", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SpiderFireball as IObjectContentGenerator } },
           { "SpiderIncubate", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SpiderIncubate as IObjectContentGenerator } },
           { "SpiderInject", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SpiderInject as IObjectContentGenerator } },
           { "SpiderKill", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SpiderKill as IObjectContentGenerator } },
           { "SpiderKill2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SpiderKill2 as IObjectContentGenerator } },
           { "SpiderKill3", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SpiderKill3 as IObjectContentGenerator } },
           { "SpiderPin", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SpiderPin as IObjectContentGenerator } },
           { "SpyPortalZap", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SpyPortalZap as IObjectContentGenerator } },
           { "SpyPortalZap2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => SpyPortalZap2 as IObjectContentGenerator } },
           { "StrigaBuff", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => StrigaBuff as IObjectContentGenerator } },
           { "StrigaClawA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => StrigaClawA as IObjectContentGenerator } },
           { "StrigaClawB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => StrigaClawB as IObjectContentGenerator } },
           { "StrigaFireBreath", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => StrigaFireBreath as IObjectContentGenerator } },
           { "StrigaReap", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => StrigaReap as IObjectContentGenerator } },
           { "StrigaReap2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => StrigaReap2 as IObjectContentGenerator } },
           { "TheFogCurse", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TheFogCurse as IObjectContentGenerator } },
           { "TornadoFling", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TornadoFling as IObjectContentGenerator } },
           { "TornadoJolt1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TornadoJolt1 as IObjectContentGenerator } },
           { "TornadoToss", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TornadoToss as IObjectContentGenerator } },
           { "TotalHorrorAttack", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TotalHorrorAttack as IObjectContentGenerator } },
           { "TotalHorrorHeal", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TotalHorrorHeal as IObjectContentGenerator } },
           { "TotalHorrorHeal2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TotalHorrorHeal2 as IObjectContentGenerator } },
           { "TotalHorrorStretch", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TotalHorrorStretch as IObjectContentGenerator } },
           { "TrainingGolemFireBreath", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TrainingGolemFireBreath as IObjectContentGenerator } },
           { "TrainingGolemFireBurst", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TrainingGolemFireBurst as IObjectContentGenerator } },
           { "TrainingGolemHeal", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TrainingGolemHeal as IObjectContentGenerator } },
           { "TrainingGolemHealB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TrainingGolemHealB as IObjectContentGenerator } },
           { "TrainingGolemPunch", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TrainingGolemPunch as IObjectContentGenerator } },
           { "TrainingGolemStun", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TrainingGolemStun as IObjectContentGenerator } },
           { "TriffidClawA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TriffidClawA as IObjectContentGenerator } },
           { "TriffidClawB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TriffidClawB as IObjectContentGenerator } },
           { "TriffidShot", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TriffidShot as IObjectContentGenerator } },
           { "TriffidSpore", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TriffidSpore as IObjectContentGenerator } },
           { "TriffidTongue", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TriffidTongue as IObjectContentGenerator } },
           { "TriffidTongueElite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TriffidTongueElite as IObjectContentGenerator } },
           { "TrollClubA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TrollClubA as IObjectContentGenerator } },
           { "TrollClubB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TrollClubB as IObjectContentGenerator } },
           { "TrollKnockdown", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TrollKnockdown as IObjectContentGenerator } },
           { "TurretCrystalZap", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TurretCrystalZap as IObjectContentGenerator } },
           { "TurretCrystalZap2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TurretCrystalZap2 as IObjectContentGenerator } },
           { "TurretCrystalZapLongRange", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TurretCrystalZapLongRange as IObjectContentGenerator } },
           { "TurretCrystalZapLongRange2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => TurretCrystalZapLongRange2 as IObjectContentGenerator } },
           { "UndeadArrow1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadArrow1 as IObjectContentGenerator } },
           { "UndeadArrow2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadArrow2 as IObjectContentGenerator } },
           { "UndeadBoneWhirlwind", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadBoneWhirlwind as IObjectContentGenerator } },
           { "UndeadDarknessBall", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadDarknessBall as IObjectContentGenerator } },
           { "UndeadFireballA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadFireballA as IObjectContentGenerator } },
           { "UndeadFireballA2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadFireballA2 as IObjectContentGenerator } },
           { "UndeadFireballB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadFireballB as IObjectContentGenerator } },
           { "UndeadFireballB2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadFireballB2 as IObjectContentGenerator } },
           { "UndeadFireballLongA", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadFireballLongA as IObjectContentGenerator } },
           { "UndeadFireballLongB", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadFireballLongB as IObjectContentGenerator } },
           { "UndeadFireballLongB2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadFireballLongB2 as IObjectContentGenerator } },
           { "UndeadFreezeBall", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadFreezeBall as IObjectContentGenerator } },
           { "UndeadGrappleArrow1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadGrappleArrow1 as IObjectContentGenerator } },
           { "UndeadIceBall1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadIceBall1 as IObjectContentGenerator } },
           { "UndeadLightningSmite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadLightningSmite as IObjectContentGenerator } },
           { "UndeadMegaSword1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadMegaSword1 as IObjectContentGenerator } },
           { "UndeadMegaSword2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadMegaSword2 as IObjectContentGenerator } },
           { "UndeadMegaSwordAngry", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadMegaSwordAngry as IObjectContentGenerator } },
           { "UndeadOmegaArrow", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadOmegaArrow as IObjectContentGenerator } },
           { "UndeadPhysicalShield", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadPhysicalShield as IObjectContentGenerator } },
           { "UndeadSelfDestruct", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadSelfDestruct as IObjectContentGenerator } },
           { "UndeadSword1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadSword1 as IObjectContentGenerator } },
           { "UndeadSword1B", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadSword1B as IObjectContentGenerator } },
           { "UndeadSword2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadSword2 as IObjectContentGenerator } },
           { "UndeadSwordAngry", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UndeadSwordAngry as IObjectContentGenerator } },
           { "UrsulaFireball1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UrsulaFireball1 as IObjectContentGenerator } },
           { "UrsulaFireball1B", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UrsulaFireball1B as IObjectContentGenerator } },
           { "UrsulaFireball2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UrsulaFireball2 as IObjectContentGenerator } },
           { "UrsulaIceball1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UrsulaIceball1 as IObjectContentGenerator } },
           { "UrsulaIceball1B", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UrsulaIceball1B as IObjectContentGenerator } },
           { "UrsulaIceball2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UrsulaIceball2 as IObjectContentGenerator } },
           { "UrsulaRage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UrsulaRage as IObjectContentGenerator } },
           { "UrsulaSummon", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => UrsulaSummon as IObjectContentGenerator } },
           { "WatcherAcidball", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WatcherAcidball as IObjectContentGenerator } },
           { "WatcherFireball", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WatcherFireball as IObjectContentGenerator } },
           { "WatcherSlap", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WatcherSlap as IObjectContentGenerator } },
           { "WebStick", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WebStick as IObjectContentGenerator } },
           { "Werewolf_Summon_Opener", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Werewolf_Summon_Opener as IObjectContentGenerator } },
           { "Werewolf_Summon_Rage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => Werewolf_Summon_Rage as IObjectContentGenerator } },
           { "WerewolfArrow1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WerewolfArrow1 as IObjectContentGenerator } },
           { "WerewolfArrow2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WerewolfArrow2 as IObjectContentGenerator } },
           { "WerewolfOmegaArrow", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WerewolfOmegaArrow as IObjectContentGenerator } },
           { "WerewolfSword1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WerewolfSword1 as IObjectContentGenerator } },
           { "WerewolfSword2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WerewolfSword2 as IObjectContentGenerator } },
           { "WerewolfSwordStun", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WerewolfSwordStun as IObjectContentGenerator } },
           { "WorgBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WorgBite as IObjectContentGenerator } },
           { "WorghestDebuff", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WorghestDebuff as IObjectContentGenerator } },
           { "WorgOmegaBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WorgOmegaBite as IObjectContentGenerator } },
           { "WormBite1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WormBite1 as IObjectContentGenerator } },
           { "WormBossAcidBurst", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WormBossAcidBurst as IObjectContentGenerator } },
           { "WormBossSpit", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WormBossSpit as IObjectContentGenerator } },
           { "WormInfect1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WormInfect1 as IObjectContentGenerator } },
           { "WormShove1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WormShove1 as IObjectContentGenerator } },
           { "WormSpit1", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WormSpit1 as IObjectContentGenerator } },
           { "WormSpit2", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WormSpit2 as IObjectContentGenerator } },
           { "YetiBarrage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => YetiBarrage as IObjectContentGenerator } },
           { "YetiBoulderThrow", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => YetiBoulderThrow as IObjectContentGenerator } },
           { "YetiColdOrb", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => YetiColdOrb as IObjectContentGenerator } },
           { "YetiDebuff", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => YetiDebuff as IObjectContentGenerator } },
           { "YetiEncase", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => YetiEncase as IObjectContentGenerator } },
           { "YetiFlingAway", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => YetiFlingAway as IObjectContentGenerator } },
           { "YetiFrostRing", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => YetiFrostRing as IObjectContentGenerator } },
           { "YetiIceBallThrow", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => YetiIceBallThrow as IObjectContentGenerator } },
           { "YetiIceSpear", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => YetiIceSpear as IObjectContentGenerator } },
           { "YetiPunch", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => YetiPunch as IObjectContentGenerator } },
           { "YetiRoarStun", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => YetiRoarStun as IObjectContentGenerator } },
           { "ZombieBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ZombieBite as IObjectContentGenerator } },
           { "ZombiePunch", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => ZombiePunch as IObjectContentGenerator } },
           { "PumpkinMimicBite", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => PumpkinMimicBite as IObjectContentGenerator } },
           { "PumpkinMimicRage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => PumpkinMimicRage as IObjectContentGenerator } },
           { "WorldBossCiervosNightmareHoof", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WorldBossCiervosNightmareHoof as IObjectContentGenerator } },
           { "PumpkinBomb", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => PumpkinBomb as IObjectContentGenerator } },
           { "PumpkinFire", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => PumpkinFire as IObjectContentGenerator } },
           { "WorldBossKarnag_Opener", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WorldBossKarnag_Opener as IObjectContentGenerator } },
           { "WorldBossKarnag_Rage", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WorldBossKarnag_Rage as IObjectContentGenerator } },
           { "WorldBossClaudiaTundraSpikes", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WorldBossClaudiaTundraSpikes as IObjectContentGenerator } },
           { "WorldBossClaudiaIceSpear", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WorldBossClaudiaIceSpear as IObjectContentGenerator } },
           { "WorldBossClaudiaBlizzard", new FieldParser() {
               Type = FieldType.Object, 
                GetObject = () => WorldBossClaudiaBlizzard as IObjectContentGenerator } },
           { "DeerFrontKick", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => DeerFrontKick as IObjectContentGenerator } },
           { "DeerRageKick", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => DeerRageKick as IObjectContentGenerator } },
           { "PetUndeadReapPunch", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => PetUndeadReapPunch as IObjectContentGenerator } },
           { "PetUndeadRageReap", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => PetUndeadRageReap as IObjectContentGenerator } },
           { "ViperSpitPoison", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => ViperSpitPoison as IObjectContentGenerator } },
           { "ViperBite", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => ViperBite as IObjectContentGenerator } },
           { "Slime_SummonSlime6", new FieldParser() {
               Type = FieldType.Object,
               GetObject = () => Slime_SummonSlime6 as IObjectContentGenerator } },
           { "BossSlime_SummonSlime7Elite", new FieldParser() {
               Type = FieldType.Object,
               GetObject = () => BossSlime_SummonSlime7Elite as IObjectContentGenerator } },
           { "SpiderBite_ProblemSpider", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => SpiderBite_ProblemSpider as IObjectContentGenerator } },
           { "PetRatkinUndeadElectricityA", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => PetRatkinUndeadElectricityA as IObjectContentGenerator } },
           { "PetRatkinUndeadElectricityB", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => PetRatkinUndeadElectricityB as IObjectContentGenerator } },
           { "PetRatkinUndeadElectricityBurst", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => PetRatkinUndeadElectricityBurst as IObjectContentGenerator } },
           { "RakToxinBomb_Tolmar", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RakToxinBomb_Tolmar as IObjectContentGenerator } },
           { "HippoBiteAndHeal", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => HippoBiteAndHeal as IObjectContentGenerator } },
           { "TreantSlashA", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => TreantSlashA as IObjectContentGenerator } },
           { "TreantSlashB", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => TreantSlashB as IObjectContentGenerator } },
           { "TreantStomp", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => TreantStomp as IObjectContentGenerator } },
           { "TreantDoT1", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => TreantDoT1 as IObjectContentGenerator } },
           { "RatTrapAttack", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatTrapAttack as IObjectContentGenerator } },
           { "RatkinSwordSlash", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatkinSwordSlash as IObjectContentGenerator } },
           { "RatkinSwordPierce", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatkinSwordPierce as IObjectContentGenerator } },
           { "RatkinSwordFinisher", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatkinSwordFinisher as IObjectContentGenerator } },
           { "RatkinDeathsHold", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatkinDeathsHold as IObjectContentGenerator } },
           { "RatkinDarknessBolt", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatkinDarknessBolt as IObjectContentGenerator } },
           { "RatkinLifeSteal", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatkinLifeSteal as IObjectContentGenerator } },
           { "RatkinBossWaveOfDarkness", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatkinBossWaveOfDarkness as IObjectContentGenerator } },
           { "RatkinReverberatingStrike70", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatkinReverberatingStrike70 as IObjectContentGenerator } },
           { "RatkinHammer", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatkinHammer as IObjectContentGenerator } },
           { "RatkinHammerStun", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatkinHammerStun as IObjectContentGenerator } },
           { "RatkinUndeadElectricityA", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatkinUndeadElectricityA as IObjectContentGenerator } },
           { "RatkinUndeadElectricityB", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatkinUndeadElectricityB as IObjectContentGenerator } },
           { "RatkinUndeadElectricityBurst", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatkinUndeadElectricityBurst as IObjectContentGenerator } },
           { "RatBuffDarkness_Pet1", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatBuffDarkness_Pet1 as IObjectContentGenerator } },
           { "RatBuffDarkness_Pet2", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatBuffDarkness_Pet2 as IObjectContentGenerator } },
           { "RatBuffDarkness_Pet3", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatBuffDarkness_Pet3 as IObjectContentGenerator } },
           { "RatBuffDarkness_Pet4", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatBuffDarkness_Pet4 as IObjectContentGenerator } },
           { "RatBuffDarkness_Pet5", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatBuffDarkness_Pet5 as IObjectContentGenerator } },
           { "RatBuffDarkness_Pet6", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RatBuffDarkness_Pet6 as IObjectContentGenerator } },
           { "ViperRageBite", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => ViperRageBite as IObjectContentGenerator } },
           { "SlugBite", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => SlugBite as IObjectContentGenerator } },
           { "SlugWebDebuff", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => SlugWebDebuff as IObjectContentGenerator } },
           { "SmallishScorpionClawA", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => SmallishScorpionClawA as IObjectContentGenerator } },
           { "SmallishScorpionClawB", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => SmallishScorpionClawB as IObjectContentGenerator } },
           { "SmallishScorpionSting", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => SmallishScorpionSting as IObjectContentGenerator } },
           { "RootTrap", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RootTrap as IObjectContentGenerator } },

           { "ElementalFireSlam", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => ElementalFireSlam as IObjectContentGenerator } },
           { "ElementalFireball", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => ElementalFireball as IObjectContentGenerator } },
           { "ElementalHateFireball", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => ElementalHateFireball as IObjectContentGenerator } },
           { "ElementalHateFireball2", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => ElementalHateFireball2 as IObjectContentGenerator } },
           { "ElementalHateFireball3", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => ElementalHateFireball3 as IObjectContentGenerator } },
           { "ElementalFireballB", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => ElementalFireballB as IObjectContentGenerator } },
           { "FaeIceSpearA", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => FaeIceSpearA as IObjectContentGenerator } },
           { "FaeIceSpearB", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => FaeIceSpearB as IObjectContentGenerator } },
           { "FaeColdSphere", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => FaeColdSphere as IObjectContentGenerator } },
           { "LethargyPuckFrostbite", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => LethargyPuckFrostbite as IObjectContentGenerator } },
           { "LethargyPuckRage", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => LethargyPuckRage as IObjectContentGenerator } },
           { "BeeStab_Pet", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => BeeStab_Pet as IObjectContentGenerator } },
           { "BeeInject_Pet1", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => BeeInject_Pet1 as IObjectContentGenerator } },
           { "BeeInject_Pet2", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => BeeInject_Pet2 as IObjectContentGenerator } },
           { "BeeInject_Pet3", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => BeeInject_Pet3 as IObjectContentGenerator } },
           { "BeeInject_Pet4", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => BeeInject_Pet4 as IObjectContentGenerator } },
           { "BeeInject_Pet5", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => BeeInject_Pet5 as IObjectContentGenerator } },
           { "BeeInject_Pet6", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => BeeInject_Pet6 as IObjectContentGenerator } },
           { "BeeVuln_Pet1", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => BeeVuln_Pet1 as IObjectContentGenerator } },
           { "BeeVuln_Pet2", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => BeeVuln_Pet2 as IObjectContentGenerator } },
           { "BeeVuln_Pet3", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => BeeVuln_Pet3 as IObjectContentGenerator } },
           { "BeeVuln_Pet4", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => BeeVuln_Pet4 as IObjectContentGenerator } },
           { "WaspShoot_Pet", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspShoot_Pet as IObjectContentGenerator } },
           { "WaspBlast_Pet1", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspBlast_Pet1 as IObjectContentGenerator } },
           { "WaspBlast_Pet2", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspBlast_Pet2 as IObjectContentGenerator } },
           { "WaspBlast_Pet3", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspBlast_Pet3 as IObjectContentGenerator } },
           { "WaspBlast_Pet4", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspBlast_Pet4 as IObjectContentGenerator } },
           { "WaspBlast_Pet5", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspBlast_Pet5 as IObjectContentGenerator } },
           { "WaspBlast_Pet6", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspBlast_Pet6 as IObjectContentGenerator } },
           { "WaspSlow_Pet1", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspSlow_Pet1 as IObjectContentGenerator } },
           { "WaspSlow_Pet2", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspSlow_Pet2 as IObjectContentGenerator } },
           { "WaspSlow_Pet3", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspSlow_Pet3 as IObjectContentGenerator } },
           { "WaspSlow_Pet4", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspSlow_Pet4 as IObjectContentGenerator } },
           { "WaspStab_Pet", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspStab_Pet as IObjectContentGenerator } },
           { "WaspDebuff_Pet1", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspDebuff_Pet1 as IObjectContentGenerator } },
           { "WaspDebuff_Pet2", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspDebuff_Pet2 as IObjectContentGenerator } },
           { "WaspDebuff_Pet3", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspDebuff_Pet3 as IObjectContentGenerator } },
           { "WaspDebuff_Pet4", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspDebuff_Pet4 as IObjectContentGenerator } },
           { "WaspDebuff_Pet5", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspDebuff_Pet5 as IObjectContentGenerator } },
           { "WaspDebuff_Pet6", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspDebuff_Pet6 as IObjectContentGenerator } },
           { "WaspBurst_Pet1", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspBurst_Pet1 as IObjectContentGenerator } },
           { "WaspBurst_Pet2", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspBurst_Pet2 as IObjectContentGenerator } },
           { "WaspBurst_Pet3", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspBurst_Pet3 as IObjectContentGenerator } },
           { "WaspBurst_Pet4", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspBurst_Pet4 as IObjectContentGenerator } },
           { "PhoenixPeck", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => PhoenixPeck as IObjectContentGenerator } },
           { "PhoenixFireball", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => PhoenixFireball as IObjectContentGenerator } },
           { "PhoenixBlast", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => PhoenixBlast as IObjectContentGenerator } },
           { "BeeStab", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => BeeStab as IObjectContentGenerator } },
           { "BeeInject", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => BeeInject as IObjectContentGenerator } },
           { "BeeShoot", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => BeeShoot as IObjectContentGenerator } },
           { "BeeShootB", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => BeeShootB as IObjectContentGenerator } },
           { "WaspStab", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspStab as IObjectContentGenerator } },
           { "WaspRageStab", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspRageStab as IObjectContentGenerator } },
           { "WaspRangedDebuff", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspRangedDebuff as IObjectContentGenerator } },
           { "WaspRangedDebuffB", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspRangedDebuffB as IObjectContentGenerator } },
           { "WaspRangedDebuffC", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspRangedDebuffC as IObjectContentGenerator } },
           { "WaspShoot", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspShoot as IObjectContentGenerator } },
           { "WaspRageBurst", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspRageBurst as IObjectContentGenerator } },
           { "WaspSlowA", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspSlowA as IObjectContentGenerator } },
           { "WaspSlowB", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspSlowB as IObjectContentGenerator } },
           { "PixieSlash", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => PixieSlash as IObjectContentGenerator } },
           { "PixieRageSlash", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => PixieRageSlash as IObjectContentGenerator } },
           { "StunTrapExplode", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => StunTrapExplode as IObjectContentGenerator } },
           { "SpiderIncubate1", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => SpiderIncubate1 as IObjectContentGenerator } },
           { "SpiderIncubate2", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => SpiderIncubate2 as IObjectContentGenerator } },
           { "RanalonHeal1", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RanalonHeal1 as IObjectContentGenerator } },
           { "RanalonHeal2", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RanalonHeal2 as IObjectContentGenerator } },
           { "RanalonSelfBuff1", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RanalonSelfBuff1 as IObjectContentGenerator } },
           { "RanalonSelfBuff2", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => RanalonSelfBuff2 as IObjectContentGenerator } },
           { "ScrayStab1", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => ScrayStab1 as IObjectContentGenerator } },
           { "ScrayStab2", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => ScrayStab2 as IObjectContentGenerator } },
           { "WaspIceStab_Pet", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspIceStab_Pet as IObjectContentGenerator } },
           { "WaspIceSlow_Pet1", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspIceSlow_Pet1 as IObjectContentGenerator } },
           { "WaspIceSlow_Pet2", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspIceSlow_Pet2 as IObjectContentGenerator } },
           { "WaspIceSlow_Pet3", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspIceSlow_Pet3 as IObjectContentGenerator } },
           { "WaspIceSlow_Pet4", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspIceSlow_Pet4 as IObjectContentGenerator } },
           { "WaspIceStab", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspIceStab as IObjectContentGenerator } },
           { "WaspIceRageStab", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspIceRageStab as IObjectContentGenerator } },
           { "WaspIceSlowA", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspIceSlowA as IObjectContentGenerator } },
           { "WaspIceSlowB", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => WaspIceSlowB as IObjectContentGenerator } },
           { "PhoenixFlash", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => PhoenixFlash as IObjectContentGenerator } },
           { "KrakenRageCurse", new FieldParser() {
               Type = FieldType.Object,
                GetObject = () => KrakenRageCurse as IObjectContentGenerator } },
           { "HealingAura", new FieldParser() {
               Type = FieldType.Object,
               GetObject = () => HealingAura as IObjectContentGenerator } },
           { "FaeConduitHeal", new FieldParser() {
               Type = FieldType.Object,
               GetObject = () => FaeConduitHeal as IObjectContentGenerator } },
           { "BigCatRoot_Pet5", new FieldParser() {
               Type = FieldType.Object,
               GetObject = () => BigCatRoot_Pet5 as IObjectContentGenerator } },
           { "BigCatRoot_Pet6", new FieldParser() {
               Type = FieldType.Object,
               GetObject = () => BigCatRoot_Pet6 as IObjectContentGenerator } },
           { "BearStun_Pet5", new FieldParser() {
               Type = FieldType.Object,
               GetObject = () => BearStun_Pet5 as IObjectContentGenerator } },
           { "BearStun_Pet6", new FieldParser() {
               Type = FieldType.Object,
               GetObject = () => BearStun_Pet6 as IObjectContentGenerator } },
           { "PhoenixClaw", new FieldParser() {
               Type = FieldType.Object,
               GetObject = () => PhoenixClaw as IObjectContentGenerator } },
           { "GoatBite", new FieldParser() {
               Type = FieldType.Object,
               GetObject = () => GoatBite as IObjectContentGenerator } },
           { "GoatKick", new FieldParser() {
               Type = FieldType.Object,
               GetObject = () => GoatKick as IObjectContentGenerator } },
           { "GoatSmite", new FieldParser() {
               Type = FieldType.Object,
               GetObject = () => GoatSmite as IObjectContentGenerator } },
        }; } }

        #region Indirect Properties
        public override string SortingName { get { return Key; } }
        #endregion
    }
}
