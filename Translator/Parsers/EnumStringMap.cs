namespace Translator;

using System;
using System.Collections;
using System.Collections.Generic;
using PgObjects;

public class EnumStringMap
{
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
        { MapAreaName.WNSWintertide, "WNS Wintertide" },
        { MapAreaName.Povus, "Povus" },
        { MapAreaName.Staging, "Staging" },
    };

    public static readonly Dictionary<QuestObjectiveTarget, string> QuestObjectiveKillTargetTable = new Dictionary<QuestObjectiveTarget, string>()
    {
        { QuestObjectiveTarget.Any, "*" },
    };

    public static readonly Dictionary<RecipeAction, string> RecipeActionTable = new Dictionary<RecipeAction, string>()
    {
        { RecipeAction.DecomposeItem, "Decompose Item" },
        { RecipeAction.RemoveAugment, "Remove Augment" },
        { RecipeAction.DistillItem, "Distill Item" },
        { RecipeAction.RepairItem, "Repair Item" },
        { RecipeAction.CreateMap, "Create Map" },
        { RecipeAction.StudySkull, "Study Skull" },
        { RecipeAction.StudyEquipment, "Study Equipment" },
        { RecipeAction.SayTheSooth, "Say the Sooth" },
        { RecipeAction.MixDye, "Mix Dye" },
        { RecipeAction.InfuseSpirits, "Infuse Spirits" },
        { RecipeAction.CreateSpiritBelt, "Create Spirit Belt" },
        { RecipeAction.ApplyAugment, "Apply Augment" },
        { RecipeAction.Brew, "Brew" },
        { RecipeAction.PrepareCask, "Prepare Cask" },
        { RecipeAction.SortGrass, "Sort Grass" },
        { RecipeAction.TapKeg, "Tap Keg" },
        { RecipeAction.RendSpaceTime, "Rend Space-Time" },
        { RecipeAction.PerformTheRitual, "Perform the Ritual" },
        { RecipeAction.WaxShield, "Wax Shield" },
        { RecipeAction.AttuneMind, "Attune Mind" },
        { RecipeAction.CraftEnchantedKit, "Craft Enchanted Kit" },
        { RecipeAction.CraftIceShield, "Craft Ice Shield" },
        { RecipeAction.CraftIceSkinningKnife, "Craft Ice Skinning Knife" },
        { RecipeAction.CraftIceButcherKnife, "Craft Ice Butcher Knife" },
        { RecipeAction.CraftIceHandsaw, "Craft Ice Handsaw" },
        { RecipeAction.OpenPortal, "Open Portal" },
        { RecipeAction.DyeFur, "Dye Fur" },
        { RecipeAction.AssembleAndAnimate, "Assemble and Animate" },
        { RecipeAction.SendToSaddlebag, "Send to Saddlebag" },
        { RecipeAction.ApplyGlamour, "Apply Glamour" },
        { RecipeAction.PerformRitual, "Perform Ritual" },
        { RecipeAction.DrinkNectar, "Drink Nectar" },
        { RecipeAction.CutBait, "Cut Bait" },
    };

    public static readonly Dictionary<RecipeEffect, string> RecipeEffectTable = new Dictionary<RecipeEffect, string>()
    {
        { RecipeEffect.CreateGeologySurveyRedwall_GeologySurveySerbule0, "CreateGeologySurveyRedwall(GeologySurveySerbule0)" },
        { RecipeEffect.CreateGeologySurveyBlue_GeologySurveySerbule1, "CreateGeologySurveyBlue(GeologySurveySerbule1)" },
        { RecipeEffect.CreateGeologySurveyGreen_GeologySurveySerbule2, "CreateGeologySurveyGreen(GeologySurveySerbule2)" },
        { RecipeEffect.CreateGeologySurveyWhite_GeologySurveySerbule4, "CreateGeologySurveyWhite(GeologySurveySerbule4)" },
        { RecipeEffect.CreateGeologySurveyRedwall_GeologySurveySouthSerbule0, "CreateGeologySurveyRedwall(GeologySurveySouthSerbule0)" },
        { RecipeEffect.CreateGeologySurveyOrange_GeologySurveySouthSerbule3, "CreateGeologySurveyOrange(GeologySurveySouthSerbule3)" },
        { RecipeEffect.CreateGeologySurveyWhite_GeologySurveySouthSerbule4, "CreateGeologySurveyWhite(GeologySurveySouthSerbule4)" },
        { RecipeEffect.CreateGeologySurveyBlue_GeologySurveyEltibule1, "CreateGeologySurveyBlue(GeologySurveyEltibule1)" },
        { RecipeEffect.CreateGeologySurveyGreen_GeologySurveyEltibule2, "CreateGeologySurveyGreen(GeologySurveyEltibule2)" },
        { RecipeEffect.CreateGeologySurveyOrange_GeologySurveyEltibule3, "CreateGeologySurveyOrange(GeologySurveyEltibule3)" },
        { RecipeEffect.CreateGeologySurveyBlue_GeologySurveyKurMountains1, "CreateGeologySurveyBlue(GeologySurveyKurMountains1)" },
        { RecipeEffect.CreateGeologySurveyGreen_GeologySurveyKurMountains2, "CreateGeologySurveyGreen(GeologySurveyKurMountains2)" },
        { RecipeEffect.CreateGeologySurveyOrange_GeologySurveyKurMountains3, "CreateGeologySurveyOrange(GeologySurveyKurMountains3)" },
        { RecipeEffect.CreateGeologySurveyWhite_GeologySurveyKurMountains4, "CreateGeologySurveyWhite(GeologySurveyKurMountains4)" },
    };

    public static readonly Dictionary<RecipeItemKey, string> RecipeItemKeyTable = new Dictionary<RecipeItemKey, string>()
    {
        { RecipeItemKey.EquipmentSlot_MainHand, "EquipmentSlot:MainHand" },
        { RecipeItemKey.EquipmentSlot_OffHand, "EquipmentSlot:OffHand" },
        { RecipeItemKey.EquipmentSlot_Hands, "EquipmentSlot:Hands" },
        { RecipeItemKey.EquipmentSlot_Chest, "EquipmentSlot:Chest" },
        { RecipeItemKey.EquipmentSlot_Legs, "EquipmentSlot:Legs" },
        { RecipeItemKey.EquipmentSlot_Head, "EquipmentSlot:Head" },
        { RecipeItemKey.EquipmentSlot_Feet, "EquipmentSlot:Feet" },
        { RecipeItemKey.EquipmentSlot_Ring, "EquipmentSlot:Ring" },
        { RecipeItemKey.EquipmentSlot_Necklace, "EquipmentSlot:Necklace" },
        { RecipeItemKey.Rarity_Common, "Rarity:Common" },
        { RecipeItemKey.Rarity_Uncommon, "Rarity:Uncommon" },
        { RecipeItemKey.Rarity_Rare, "Rarity:Rare" },
        { RecipeItemKey.MinRarity_Exceptional, "MinRarity:Exceptional" },
        { RecipeItemKey.MinRarity_Uncommon, "MinRarity:Uncommon" },
        { RecipeItemKey.Rarity_Exceptional, "Rarity:Exceptional" },
        { RecipeItemKey.MinRarity_Epic, "MinRarity:Epic" },
        { RecipeItemKey.MinTSysPrereq_0, "MinTSysPrereq:0" },
        { RecipeItemKey.MaxTSysPrereq_30, "MaxTSysPrereq:30" },
        { RecipeItemKey.MinTSysPrereq_31, "MinTSysPrereq:31" },
        { RecipeItemKey.MaxTSysPrereq_60, "MaxTSysPrereq:60" },
        { RecipeItemKey.MinTSysPrereq_61, "MinTSysPrereq:61" },
        { RecipeItemKey.MaxTSysPrereq_90, "MaxTSysPrereq:90" },
        { RecipeItemKey.NotNoSendToSaddlebag, "!NoSendToSaddlebag" },
        { RecipeItemKey.MinRarity_Legendary, "MinRarity:Legendary" },
        { RecipeItemKey.Special_Mastercrafted, "Special:Mastercrafted" },
    };

    public static readonly Dictionary<XpTableEnum, string> XpTableEnumTable = new Dictionary<XpTableEnum, string>()
    {
        { XpTableEnum.CookingUnused, "Cooking-unused" },
    };

    public static readonly Dictionary<AbilitySelfParticle, string> AbilitySelfParticleTable = new Dictionary<AbilitySelfParticle, string>()
    {
        { AbilitySelfParticle.WolfSeeRed, "Wolf-SeeRed" },
    };

    public static Dictionary<Type, IDictionary> Tables { get; } = new Dictionary<Type, IDictionary>()
    {
        { typeof(AppearanceSkin), AppearanceSkinTable },
        { typeof(MapAreaName), MapAreaNameTable },
        { typeof(QuestObjectiveTarget), QuestObjectiveKillTargetTable },
        { typeof(RecipeAction), RecipeActionTable },
        { typeof(RecipeEffect), RecipeEffectTable },
        { typeof(RecipeItemKey), RecipeItemKeyTable },
        { typeof(AbilitySelfParticle), AbilitySelfParticleTable },
        { typeof(XpTableEnum), XpTableEnumTable },
    };
}
