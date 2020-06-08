namespace Translator
{
    using PgJsonObjects;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class EnumStringMap
    {
        public static readonly Dictionary<AbilityItemKeyword, string> AbilityItemKeywordTable = new Dictionary<AbilityItemKeyword, string>()
        {
            { AbilityItemKeyword.form_Deer, "form:Deer" },
            { AbilityItemKeyword.form_PotbellyPig, "form:PotbellyPig" },
            { AbilityItemKeyword.form_Cow, "form:Cow" },
            { AbilityItemKeyword.form_Spider, "form:Spider" },
            { AbilityItemKeyword.form_GiantBat, "form:GiantBat" },
            { AbilityItemKeyword.form_Rabbit, "form:Rabbit" },
        };

        public static readonly Dictionary<EffectStackingType, string> StackingTypeTable = new Dictionary<EffectStackingType, string>()
        {
            { EffectStackingType.LamiasGaze, "Lamia's Gaze" },
            { EffectStackingType.One, "1" },
        };

        public static readonly Dictionary<EffectKeyword, string> EffectKeywordTable = new Dictionary<EffectKeyword, string>()
        {
            { EffectKeyword.Hyphen, "-" },
        };

        public static readonly Dictionary<EffectParticle, string> EffectParticleTable = new Dictionary<EffectParticle, string>()
        {
            { EffectParticle.OnFireGreen, "OnFire-Green"},
        };

        public static readonly Dictionary<ItemKeyword, string> ItemKeywordTable = new Dictionary<ItemKeyword, string>()
        {
            { ItemKeyword.MinRarity_Uncommon, "MinRarity:Uncommon" },
        };

        public static readonly Dictionary<ItemUseVerb, string> ItemUseVerbTable = new Dictionary<ItemUseVerb, string>()
        {
            { ItemUseVerb.Place, "Place"},
            { ItemUseVerb.Fill, "Fill"},
            { ItemUseVerb.Drink, "Drink"},
            { ItemUseVerb.Learn, "Learn"},
            { ItemUseVerb.LearnWord, "Learn Word"},
            { ItemUseVerb.BlowWhistle, "Blow Whistle"},
            { ItemUseVerb.Read, "Read"},
            { ItemUseVerb.Eat, "Eat"},
            { ItemUseVerb.Delouse, "Delouse"},
            { ItemUseVerb.RubGem, "Rub Gem"},
            { ItemUseVerb.PlayGame, "Play Game"},
            { ItemUseVerb.EmptyPurse, "Empty Purse"},
            { ItemUseVerb.ActivateRune, "Activate Rune"},
            { ItemUseVerb.EmptySack, "Empty Sack"},
            { ItemUseVerb.ReadBook, "Read Book"},
            { ItemUseVerb.Plant, "Plant"},
            { ItemUseVerb.Appreciate, "Appreciate"},
            { ItemUseVerb.Deploy, "Deploy"},
            { ItemUseVerb.Appraise, "Appraise"},
            { ItemUseVerb.StudyPoetry, "Study Poetry"},
            { ItemUseVerb.LaunchFirework, "Launch Firework"},
            { ItemUseVerb.PopConfetti, "Pop Confetti"},
            { ItemUseVerb.BreakDown, "Break Down"},
            { ItemUseVerb.Apply, "Apply"},
            { ItemUseVerb.Swallow, "Swallow"},
            { ItemUseVerb.Inhale, "Inhale"},
            { ItemUseVerb.Inject, "Inject"},
            { ItemUseVerb.Fish, "Fish"},
            { ItemUseVerb.AgeCheese, "Age Cheese"},
            { ItemUseVerb.AgeCream, "Age Cream"},
            { ItemUseVerb.CheckSurvey, "Check Survey"},
            { ItemUseVerb.CheckNotes, "Check Notes"},
            { ItemUseVerb.CheckMap, "Check Map"},
            { ItemUseVerb.Translate, "Translate"},
            { ItemUseVerb.MemorizeWorkOrder, "Memorize Work Order"},
            { ItemUseVerb.Equip, "Equip"},
            { ItemUseVerb.HuddleForWarmth, "Huddle For Warmth"},
            { ItemUseVerb.AgeLiquor, "Age Liquor"},
            { ItemUseVerb.CleanOffDust, "Clean Off Dust" },
            { ItemUseVerb.TakeSoilSample, "Take Soil Sample" },
            { ItemUseVerb.EmptyBottle, "Empty Bottle" },
            { ItemUseVerb.ObtainTitle, "Obtain Title" },
            { ItemUseVerb.HugBear, "Hug Bear" },
            { ItemUseVerb.HugMonkey, "Hug Monkey" },
            { ItemUseVerb.Chew, "Chew" },
            { ItemUseVerb.TakeWaterSample, "Take Water Sample" },
            { ItemUseVerb.BreakIntoSmallerParts, "Break Into Smaller Parts" },
            { ItemUseVerb.TurnIntoBoneMeal, "Turn into Bone Meal" },
            { ItemUseVerb.OpenBagWithDyingBreath, "Open Bag With Dying Breath" },
            { ItemUseVerb.Salvage, "Salvage" },
            { ItemUseVerb.OpenCrate, "Open Crate" },
        };

        public static readonly Dictionary<AppearanceSkin, string> AppearanceSkinTable = new Dictionary<AppearanceSkin, string>()
        {
            { AppearanceSkin.Halloween_CofferItems, "Halloween-CofferItems" },
            { AppearanceSkin.Medieval_Fruits, "Medieval-Fruits" },
            { AppearanceSkin.Medieval_Vegetables, "Medieval-Vegetables" },
            { AppearanceSkin.GF_Food, "GF-Food" },
            { AppearanceSkin.GF_Dishes, "GF-Dishes" },
        };

        public static readonly Dictionary<MapAreaName, string> MapAreaNameTable = new Dictionary<MapAreaName, string>()
        {
            { MapAreaName.Internal_None, "None" },
            { MapAreaName.Several, "*" },
            { MapAreaName.Serbule, "Serbule" },
            { MapAreaName.Eltibule, "Eltibule" },
            { MapAreaName.Tomb1, "Khyrulek's Crypt" },
            { MapAreaName.KurMountains, "Kur Mountains" },
            { MapAreaName.NewbieIsland, "Anagoge Island" },
            { MapAreaName.SerbuleCaves, "Serbule Caves" },
            { MapAreaName.Rahu, "Rahu" },
            { MapAreaName.Serbule2, "Serbule Hills" },
            { MapAreaName.Desert1, "Ilmari Desert" },
            { MapAreaName.SunVale, "Sun Vale" },
            { MapAreaName.Cave1, "Hogan's Keep" },
            { MapAreaName.GazlukCaves, "Gazluk Dungeons" },
            { MapAreaName.Gazluk, "Gazluk Plateau" },
            { MapAreaName.Cave2, "Wolf Cave" },
            { MapAreaName.MyconianCave, "Myconian Caverns" },
            { MapAreaName.GazlukKeep, "Gazluk Keep" },
            { MapAreaName.Dungeons, "Dungeons" },
            { MapAreaName.RanalonDen, "Ranalon Den" },
            { MapAreaName.Casino, "Red Wing Casino" },
            { MapAreaName.RahuCaves, "Rahu Caves" },
            { MapAreaName.RahuSewer, "Rahu Sewer" },
            { MapAreaName.RahuSewers, "Rahu Sewers" },
            { MapAreaName.FaeRealm, "Fae Realm" },
            { MapAreaName.SacredGrotto, "Sacred Grotto" },
            { MapAreaName.ANewLife, "A New Life" },
        };

        public static readonly Dictionary<QuestObjectiveKillTarget, string> QuestObjectiveKillTargetTable = new Dictionary<QuestObjectiveKillTarget, string>()
        {
            { QuestObjectiveKillTarget.Any, "*" },
        };

        public static Dictionary<Type, IDictionary> Tables = new Dictionary<Type, IDictionary>()
        {
            {  typeof(AbilityItemKeyword), AbilityItemKeywordTable },
            {  typeof(EffectStackingType), StackingTypeTable },
            {  typeof(EffectKeyword), EffectKeywordTable },
            {  typeof(EffectParticle), EffectParticleTable },
            {  typeof(ItemKeyword), ItemKeywordTable },
            {  typeof(ItemUseVerb), ItemUseVerbTable },
            {  typeof(AppearanceSkin), AppearanceSkinTable },
            {  typeof(MapAreaName), MapAreaNameTable },
            {  typeof(QuestObjectiveKillTarget), QuestObjectiveKillTargetTable },
        };
    }
}
