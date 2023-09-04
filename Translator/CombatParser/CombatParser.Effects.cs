namespace Translator;

using System.Collections.Generic;
using PgObjects;

public partial class CombatParser
{
    private static Dictionary<int, AdditionalEffect[]> HardcodedEffectAllTiersTable = new()
    {
        #region Animal Handling
        { 12012, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.MonstrousRage.ToString(), Effect = 14311, Target = "Self" },
            }
        },
        { 12013, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.MonstrousRage.ToString(), Effect = 14312, Target = "Self" },
            }
        },
        { 12014, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.MonstrousRage.ToString(), Effect = 14316, Target = "Pet" },
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.UnnaturalWrath.ToString(), Effect = 14316, Target = "Pet" },
            }
        },
        { 12021, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SicEm.ToString(), Effect = 14313, Target = "Self" },
            }
        },
        { 12022, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SicEm.ToString(), Effect = 14314, Target = "Pet" },
            }
        },
        { 12023, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SicEm.ToString(), Effect = 14310, Target = "Pet" },
            }
        },
        { 12024, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SicEm.ToString(), Effect = 14309, Target = "Self" },
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SicEm.ToString(), Effect = 14309, Target = "Pet" },
            }
        },
        { 12025, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SicEm.ToString(), Effect = 14308, Target = "Pet" },
            }
        },
        { 12051, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.GetItOffMe.ToString(), Effect = 14315, Target = "Pet" },
            }
        },
        { 12053, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.GetItOffMe.ToString(), Effect = 14909, Target = "Pet" },
            }
        },
        { 12091, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.FeedPet.ToString(), Effect = 14918, Target = "Pet" },
            }
        },
        { 12105, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.UnnaturalWrath.ToString(), Effect = 14475, Target = "Pet" },
            }
        },
        { 12106, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.UnnaturalWrath.ToString(), Effect = 14460, Target = "Pet" },
            }
        },
        { 12121, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.WildEndurance.ToString(), Effect = 14477, Target = "Pet" },
            }
        },
        { 12122, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.FeedPet.ToString(), Effect = 14479, Target = "Self" },
            }
        },
        { 12141, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.NimbleLimbs.ToString(), Effect = 14480, Target = "Pet" },
            }
        },
        { 12304, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SicEm.ToString(), Effect = 15913, Target = "Pet" },
            }
        },
        { 12314, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.NimbleLimbs.ToString(), Effect = 15963, Target = "Pet" },
            }
        },
        #endregion

        #region Archery
        { 10042, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.AimedShot.ToString(), Effect = 16633, Target = "Foe" },
            }
        },
        { 10043, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.AimedShot.ToString(), Effect = 16631, Target = "Self" },
            }
        },
        { 10044, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.AimedShot.ToString(), Effect = 16632, Target = "Self" },
            }
        },
        { 10082, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.MultiShot.ToString(), Effect = 14906, Target = "Self" },
            }
        },
        { 10122, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.LongShot.ToString(), Effect = 16634, Target = "Self" },
            }
        },
        { 10124, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.LongShot.ToString(), Effect = 14906, Target = "Self" },
            }
        },
        { 10125, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.LongShot.ToString(), Effect = 16629, Target = "Foe" },
            }
        },
        { 10162, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.BlitzShot.ToString(), Effect = 16635, Target = "Self" },
            }
        },
        { 10306, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.PoisonArrow.ToString(), Effect = 16636, Target = "Foe" },
            }
        },
        { 10308, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.FireArrow.ToString(), Effect = 15368, Target = "Foe" },
            }
        },
        { 10309, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.PoisonArrow.ToString(), Effect = 14363, Target = "Foe" },
            }
        },
        { 10401, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SnareArrow.ToString(), Effect = 16638, Target = "Self" },
            }
        },
        { 10403, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SnareArrow.ToString(), Effect = 16637, Target = "Foe" },
            }
        },
        { 10456, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.HeavyMultiShot.ToString(), Effect = 14621, Target = "Self" },
            }
        },
        { 10501, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ManglingShot.ToString(), Effect = 15151, Target = "Foe" },
            }
        },
        { 10502, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ManglingShot.ToString(), Effect = 16626, Target = "Foe" },
            }
        },
        { 10503, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ManglingShot.ToString(), Effect = 16627, Target = "Foe" },
            }
        },
        { 10504, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ManglingShot.ToString(), Effect = 16871, Target = "Foe" },
            }
        },
        { 10508, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.RestorativeArrow.ToString(), Effect = 16628, Target = "Self" },
            }
        },
        { 10552, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.HookShot.ToString(), Effect = 16646, Target = "Foe" },
            }
        },
        { 10554, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.HookShot.ToString(), Effect = 16648, Target = "Foe" },
            }
        },
        #endregion

        #region Bard
        { 17045, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SongOfDiscordActivation.ToString(), Effect = 16821, Target = "Self" },
            }
        },
        { 17081, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SongOfBravery.ToString(), Effect = 16793, Target = "Allies" },
            }
        },
        { 17082, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SongOfBravery.ToString(), Effect = 16794, Target = "Allies" },
            }
        },
        { 17162, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.BlastOfDespair.ToString(), Effect = 16798, Target = "Self" },
            }
        },
        { 17203, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ThunderousNote.ToString(), Effect = 16797, Target = "Foe" },
            }
        },
        { 17223, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Rally.ToString(), Effect = 14757, Target = "Ally" },
            }
        },
        { 17241, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.AnthemOfAvoidance.ToString(), Effect = 16799, Target = "Allies" },
            }
        },
        { 17242, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.AnthemOfAvoidance.ToString(), Effect = 16800, Target = "Allies" },
            }
        },
        { 17243, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.AnthemOfAvoidance.ToString(), Effect = 16801, Target = "Allies" },
            }
        },
        { 17262, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.EntrancingLullaby.ToString(), Effect = 16802, Target = "Foe" },
            }
        },
        { 17302, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.MomentOfResolve.ToString(), Effect = 16803, Target = "Allies" },
            }
        },
        { 17303, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.MomentOfResolve.ToString(), Effect = 15445, Target = "Allies" },
            }
        },
        { 17321, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Disharmony.ToString(), Effect = 16808, Target = "Foe" },
            }
        },
        { 17322, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Disharmony.ToString(), Effect = 16809, Target = "Foe" },
            }
        },
        #endregion

        #region Battle Chemistry
        { 7021, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ToxicIrritant.ToString(), Effect = 16699, Target = "Self" },
            }
        },
        { 7206, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.HealingInjection.ToString(), Effect = 14918, Target = "Self" },
            }
        },
        { 7431, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Mutation_ExtraSkin.ToString(), Effect = 16772, Target = "Ally" },
            }
        },
        { 7432, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Mutation_ExtraSkin.ToString(), Effect = 16773, Target = "Ally" },
            }
        },
        { 7433, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Mutation_ExtraSkin.ToString(), Effect = 16774, Target = "Ally" },
            }
        },
        { 7471, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Mutation_ExtraHeart.ToString(), Effect = 16775, Target = "Ally" },
            }
        },
        #endregion

        #region Cow
        { 20012, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CowFrontKick.ToString(), Effect = 20594, Target = "Self" },
            }
        },
        { 20013, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CowBash.ToString(), Effect = 16591, Target = "Foe" },
            }
        },
        { 20014, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CowBash.ToString(), Effect = 15205, Target = "Foe" },
            }
        },
        { 20016, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CowStampede.ToString(), Effect = 14245, Target = "Self" },
            }
        },
        { 20019, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CowStampede.ToString(), Effect = 14266, Target = "Self" },
            }
        },
        { 20044, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.MooOfCalm.ToString(), Effect = 79, Target = "Self" },
            }
        },
        { 20061, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Graze.ToString(), Effect = 15165, Target = "Self" },
            }
        },
        { 20062, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ChewCud.ToString(), Effect = 14241, Target = "Self" },
            }
        },
        { 20065, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ChewCud.ToString(), Effect = 14241, Target = "Self" },
            }
        },
        { 20067, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ChewCud.ToString(), Effect = 14244, Target = "Self" },
            }
        },
        { 20104, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ClobberingHoof.ToString(), Effect = 15206, Target = "Foe" },
            }
        },
        { 20105, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ClobberingHoof.ToString(), Effect = 20593, Target = "Foe" },
            }
        },
        { 20302, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.MooOfDetermination.ToString(), Effect = 13704, Target = "Self" },
            }
        },
        { 20307, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.MooOfDetermination.ToString(), Effect = 78, Target = "Self" },
            }
        },
        { 20353, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ToughHoof.ToString(), Effect = 15214, Target = "Self" },
            }
        },
        { 20406, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.DeadlyEmission.ToString(), Effect = 20592, Target = "Foe" },
            }
        },
        #endregion

        #region Deer
        { 21002, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.DeerKick.ToString(), Effect = 14954, Target = "Self" },
            }
        },
        { 21004, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.DeerKick.ToString(), Effect = 14943, Target = "Foe" },
            }
        },
        { 21007, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.DeerKick.ToString(), Effect = 14943, Target = "Foe" },
            }
        },
        { 21041, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.DoeEyes.ToString(), Effect = 14941, Target = "Self" },
            }
        },
        { 21062, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CutenessOverload.ToString(), Effect = 15164, Target = "Self" },
            }
        },
        { 21066, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CutenessOverload.ToString(), Effect = 14945, Target = "Foe" },
            }
        },
        { 21083, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.KingOfTheForest.ToString(), Effect = 14947, Target = "Self" },
            }
        },
        { 21254, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.BoundingEscape.ToString(), Effect = 14953, Target = "Self" },
            }
        },
        { 21255, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.BoundingEscape.ToString(), Effect = 15445, Target = "Self" },
            }
        },
        { 21353, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ForestChallenge.ToString(), Effect = 14944, Target = "Self" },
            }
        },
        #endregion

        #region Druid
        { 14012, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.HeartThorn.ToString(), Effect = 16652, Target = "Foe" },
            }
        },
        { 14013, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.HeartThorn.ToString(), Effect = 16653, Target = "Foe" },
            }
        },
        { 14017, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.PulseOfLife.ToString(), Effect = 16640, Target = "Self" },
            }
        },
        { 14052, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Rotskin.ToString(), Effect = 14710, Target = "Foe" },
            }
        },
        { 14055, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Rotskin.ToString(), Effect = 16651, Target = "Self" },
            }
        },
        { 14056, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Rotskin.ToString(), Effect = 15074, Target = "Foe" },
            }
        },
        { 14152, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Brambleskin.ToString(), Effect = 14378, Target = "Self" },
            }
        },
        { 14153, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Brambleskin.ToString(), Effect = 14379, Target = "Self" },
            }
        },
        { 14154, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Brambleskin.ToString(), Effect = 14379, Target = "Self" },
            }
        },
        { 14155, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Brambleskin.ToString(), Effect = 14380, Target = "Self" },
            }
        },
        { 14156, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Brambleskin.ToString(), Effect = 14380, Target = "Self" },
            }
        },
        { 14202, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CosmicStrike.ToString(), Effect = 16654, Target = "Self" },
            }
        },
        { 14252, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.FillWithBile.ToString(), Effect = 14384, Target = "Ally" },
            }
        },
        { 14253, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.FillWithBile.ToString(), Effect = 14385, Target = "Ally" },
            }
        },
        { 14254, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.FillWithBile.ToString(), Effect = 14946, Target = "Ally" },
            }
        },
        { 14354, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Regrowth.ToString(), Effect = 16650, Target = "Self" },
            }
        },
        { 14353, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Regrowth.ToString(), Effect = 419, Target = "Ally" },
            }
        },
        { 14402, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Energize.ToString(), Effect = 419, Target = "Ally" },
            }
        },
        { 14501, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CloudSight.ToString(), Effect = 16649, Target = "Foe" },
            }
        },
        { 14604, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.FillWithBile.ToString(), Effect = 14946, Target = "Ally" },
            }
        },
        #endregion

        #region Fire Magic
        { 2005, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.FireBreath.ToString(), Effect = 14712, Target = "Foe" },
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SuperFireball.ToString(), Effect = 14712, Target = "Foe" },
            }
        },
        { 2009, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.FireBreath.ToString(), Effect = 16514, Target = "Self" },
            }
        },
        { 2015, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Frostball.ToString(), Effect = 16655, Target = "Self" },
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ScintillatingFrost.ToString(), Effect = 16655, Target = "Self" },
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.DefensiveChill.ToString(), Effect = 16655, Target = "Self" },
            }
        },
        { 2016, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Calefaction.ToString(), Effect = 16517, Target = "Foe" },
            }
        },
        { 2017, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CrushingBall.ToString(), Effect = 14969, Target = "Foe" },
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.DefensiveBurst.ToString(), Effect = 14969, Target = "Foe" },
            }
        },
        { 2018, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SuperFireball.ToString(), Effect = 16516, Target = "Foe" },
            }
        },
        { 2019, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.FleshToFuel.ToString(), Effect = 16512, Target = "Self" },
            }
        },
        { 2022, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.FleshToFuel.ToString(), Effect = 14318, Target = "Self" },
            }
        },
        { 2023, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CrushingBall.ToString(), Effect = 15316, Target = "Foe" },
            }
        },
        { 2025, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ScintillatingFlame.ToString(), Effect = 16518, Target = "Foe" },
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.MoltenVeins.ToString(), Effect = 16518, Target = "Foe" },
            }
        },
        { 2104, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.MoltenVeins.ToString(), Effect = 15820, Target = "Foe" },
            }
        },
        { 2154, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Calefaction.ToString(), Effect = 15364, Target = "Foe" },
            }
        },
        { 2203, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Frostball.ToString(), Effect = 15151, Target = "Foe" },
            }
        },
        { 2204, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.DefensiveBurst.ToString(), Effect = 16515, Target = "Self" },
            }
        },
        { 2207, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Frostball.ToString(), Effect = 16513, Target = "Self" },
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ScintillatingFrost.ToString(), Effect = 16513, Target = "Self" },
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.DefensiveChill.ToString(), Effect = 16513, Target = "Self" },
            }
        },
        { 2209, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.DefensiveChill.ToString(), Effect = 14611, Target = "Self" },
            }
        },
        #endregion

        #region Hammer
        { 13003, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CoreAttack.ToString(), Effect = 14301, Target = "Self" },
            }
        },
        { 13015, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.WayOfTheHammer.ToString(), Effect = 16681, Target = "Self" },
            }
        },
        { 13056, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.WayOfTheHammer.ToString(), Effect = 16687, Target = "Allies" },
            }
        },
        { 13103, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.LookAtMyHammer.ToString(), Effect = 16688, Target = "Self" },
            }
        },
        { 13104, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.LookAtMyHammer.ToString(), Effect = 14414, Target = "Self" },
            }
        },
        { 13105, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.LookAtMyHammer.ToString(), Effect = 14415, Target = "Self" },
            }
        },
        { 13106, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.LookAtMyHammer.ToString(), Effect = 14416, Target = "Self" },
            }
        },
        { 13205, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.WayOfTheHammer.ToString(), Effect = 16683, Target = "Allies" },
            }
        },
        { 13152, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.LeapingSmash.ToString(), Effect = 16685, Target = "Self" },
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.LatentCharge.ToString(), Effect = 16685, Target = "Self" },
            }
        },
        { 13303, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.DischargingStrike.ToString(), Effect = 16686, Target = "Self" },
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.LatentCharge.ToString(), Effect = 16686, Target = "Self" },
            }
        },
        { 13305, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.HurlLightning.ToString(), Effect = 16963, Target = "Foe" },
            }
        },
        { 13351, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.RecklessSlam.ToString(), Effect = 14371, Target = "Self" },
            }
        },
        { 13356, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.RecklessSlam.ToString(), Effect = 16691, Target = "Self" },
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ReverberatingStrike.ToString(), Effect = 16691, Target = "Self" },
            }
        },
        { 13401, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.LatentCharge.ToString(), Effect = 16260, Target = "Foe" },
            }
        },
        { 13402, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ReverberatingStrike.ToString(), Effect = 15867, Target = "Self" },
            }
        },
        { 13403, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.LatentCharge.ToString(), Effect = 16660, Target = "Foe" },
            }
        },
        #endregion

        #region Spider
        { 23003, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.PremeditatedDoom.ToString(), Effect = 14670, Target = "Self" },
            }
        },
        { 23005, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.InfiniteLegs.ToString(), Effect = 15051, Target = "Self" },
            }
        },
        { 23101, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.WebTrap.ToString(), Effect = 15167, Target = "Self" },
            }
        },
        { 23401, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SpiderAcid.ToString(), Effect = 15196, Target = "Self" },
            }
        },
        { 23501, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SpiderFear.ToString(), Effect = 15167, Target = "Self" },
            }
        },
        { 23504, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.SpiderFear.ToString(), Effect = 14673, Target = "Foe" },
            }
        },
        { 23251, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.InfiniteLegs.ToString(), Effect = 15051, Target = "Self" },
            }
        },
        { 23254, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.InfiniteLegs.ToString(), Effect = 15057, Target = "Self" },
            }
        },
        { 23551, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.PremeditatedDoom.ToString(), Effect = 14671, Target = "Self" },
            }
        },
        { 23554, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.PremeditatedDoom.ToString(), Effect = 14667, Target = "Self" },
            }
        },
        { 23602, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.GrapplingWeb.ToString(), Effect = 15322, Target = "Foe" },
            }
        },
        { 23603, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.GrapplingWeb.ToString(), Effect = 14674, Target = "Self" },
            }
        },
        { 23604, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.GrapplingWeb.ToString(), Effect = 14675, Target = "Foe" },
            }
        },
        #endregion

        #region Sword
        { 1021, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.ManyCuts.ToString(), Effect = 15311, Target = "Foe" },
            }
        },
        { 1061, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.WindStrike.ToString(), Effect = 15132, Target = "Self" },
            }
        },
        { 1063, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.WindStrike.ToString(), Effect = 16510, Target = "Self" },
            }
        },
        { 1064, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.FinishingBlow.ToString(), Effect = 14753, Target = "Self" },
            }
        },
        { 1065, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.WindStrike.ToString(), Effect = 14754, Target = "Self" },
            }
        },
        { 1082, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.FlashingStrike.ToString(), Effect = 14755, Target = "Self" },
            }
        },
        { 1081, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Decapitate.ToString(), Effect = 14058, Target = "Foe" },
            }
        },
        { 1086, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.FinishingBlow.ToString(), Effect = 15400, Target = "Foe" },
            }
        },
        { 1202, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.Decapitate.ToString(), Effect = 15360, Target = "Self" },
            }
        },
        { 1354, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.PrecisionPierce.ToString(), Effect = 16509, Target = "Self" },
            }
        },
        { 1502, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.DebilitatingBlow.ToString(), Effect = 16502, Target = "Self" },
            }
        },
        { 1503, new AdditionalEffect[]
            {
                new AdditionalEffect() { AbilityTrigger = AbilityKeyword.DebilitatingBlow.ToString(), Effect = 15399, Target = "Foe" },
            }
        },
        #endregion
    };
}
