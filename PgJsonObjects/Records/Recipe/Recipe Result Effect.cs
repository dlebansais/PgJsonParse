using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeResultEffect : SerializableJsonObject, IPgRecipeResultEffect
    {
        public RecipeEffect Effect { get; set; }
        public CraftedBoost Boost { get; set; }
        public int MinLevel { get { return RawMinLevel.HasValue ? RawMinLevel.Value : 0; } }
        public int? RawMinLevel { get; set; }
        public int MaxLevel { get { return RawMaxLevel.HasValue ? RawMaxLevel.Value : 0; } }
        public int? RawMaxLevel { get; set; }
        public DecomposeSkill Skill { get; set; }
        public Augment ExtractedAugment { get; set; }
        public float RepairMinEfficiency { get { return RawRepairMinEfficiency.HasValue ? RawRepairMinEfficiency.Value : 0; } }
        public float? RawRepairMinEfficiency { get; set; }
        public float RepairMaxEfficiency { get { return RawRepairMaxEfficiency.HasValue ? RawRepairMaxEfficiency.Value : 0; } }
        public float? RawRepairMaxEfficiency { get; set; }
        public FloatFormat RepairMinEfficiencyFormat { get; set; }
        public FloatFormat RepairMaxEfficiencyFormat { get; set; }
        public int RepairCooldown { get { return RawRepairCooldown.HasValue ? RawRepairCooldown.Value : 0; } }
        public int? RawRepairCooldown { get; set; }
        public int BoostLevel { get { return RawBoostLevel.HasValue ? RawBoostLevel.Value : 0; } }
        public int? RawBoostLevel { get; set; }
        public int AdditionalEnchantments { get { return RawAdditionalEnchantments.HasValue ? RawAdditionalEnchantments.Value : 0; } }
        public int? RawAdditionalEnchantments { get; set; }
        public Appearance BoostedAnimal { get; set; }
        public EnhancementEffect Enhancement { get; set; }
        public float AddedQuantity { get { return RawAddedQuantity.HasValue ? RawAddedQuantity.Value : 0; } }
        public float? RawAddedQuantity { get; set; }
        public int ConsumedEnhancementPoints { get { return RawConsumedEnhancementPoints.HasValue ? RawConsumedEnhancementPoints.Value : 0; } }
        public int? RawConsumedEnhancementPoints { get; set; }
        public ShamanicSlotPower SlotPower { get; set; }
        public MoonPhases MoonPhase { get; set; }
        public int SlotPowerLevel { get { return RawSlotPowerLevel.HasValue ? RawSlotPowerLevel.Value : 0; } }
        public int? RawSlotPowerLevel { get; set; }
        public int BrewPartCount { get { return RawBrewPartCount.HasValue ? RawBrewPartCount.Value : 0; } }
        public int? RawBrewPartCount { get; set; }
        public int BrewLevel { get { return RawBrewLevel.HasValue ? RawBrewLevel.Value : 0; } }
        public int? RawBrewLevel { get; set; }
        public List<RecipeItemKey> BrewPartList { get; set; } = new List<RecipeItemKey>();
        public List<RecipeResultKey> BrewResultList { get; set; } = new List<RecipeResultKey>();
        public int AdjustedReuseTime { get { return RawAdjustedReuseTime.HasValue ? RawAdjustedReuseTime.Value : 0; } }
        public int? RawAdjustedReuseTime { get; set; }
        public string RawItemName { get; set; }
        public IPgItem Item { get; set; }
        public bool IsCamouflaged { get { return RawIsCamouflaged.HasValue && RawIsCamouflaged.Value; } }
        public bool? RawIsCamouflaged { get; set; }
        public PowerWaxType PowerWaxType { get; set; }
        public RecipeItemKey RecipeItemKey { get; set; }

        public string CombinedEffect
        {
            get { return PgRecipeResultEffect.GetCombinedEffect(this); }
        }

        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddEnum(Effect, data, ref offset, BaseOffset, 0);
            AddEnum(Boost, data, ref offset, BaseOffset, 2);
            AddInt(RawMinLevel, data, ref offset, BaseOffset, 4);
            AddInt(RawMaxLevel, data, ref offset, BaseOffset, 8);
            AddEnum(Skill, data, ref offset, BaseOffset, 12);
            AddEnum(ExtractedAugment, data, ref offset, BaseOffset, 14);
            AddDouble(RawRepairMinEfficiency, data, ref offset, BaseOffset, 16);
            AddDouble(RawRepairMaxEfficiency, data, ref offset, BaseOffset, 20);
            AddEnum(RepairMinEfficiencyFormat, data, ref offset, BaseOffset, 24);
            AddEnum(RepairMaxEfficiencyFormat, data, ref offset, BaseOffset, 26);
            AddInt(RawRepairCooldown, data, ref offset, BaseOffset, 28);
            AddInt(RawBoostLevel, data, ref offset, BaseOffset, 32);
            AddInt(RawAdditionalEnchantments, data, ref offset, BaseOffset, 36);
            AddEnum(BoostedAnimal, data, ref offset, BaseOffset, 40);
            AddEnum(Enhancement, data, ref offset, BaseOffset, 42);
            AddDouble(RawAddedQuantity, data, ref offset, BaseOffset, 44);
            AddInt(RawConsumedEnhancementPoints, data, ref offset, BaseOffset, 48);
            AddEnum(SlotPower, data, ref offset, BaseOffset, 52);
            AddEnum(MoonPhase, data, ref offset, BaseOffset, 54);
            AddInt(RawSlotPowerLevel, data, ref offset, BaseOffset, 56);
            AddInt(RawBrewPartCount, data, ref offset, BaseOffset, 60);
            AddInt(RawBrewLevel, data, ref offset, BaseOffset, 64);
            AddEnumList(BrewPartList, data, ref offset, BaseOffset, 68, StoredEnumListTable);
            AddEnumList(BrewResultList, data, ref offset, BaseOffset, 72, StoredEnumListTable);
            AddInt(RawAdjustedReuseTime, data, ref offset, BaseOffset, 76);
            AddObject(Item as ISerializableJsonObject, data, ref offset, BaseOffset, 80, StoredObjectTable);
            AddBool(RawIsCamouflaged, data, ref offset, ref BitOffset, BaseOffset, 84, 0);
            CloseBool(ref offset, ref BitOffset);
            AddEnum(PowerWaxType, data, ref offset, BaseOffset, 86);
            AddEnum(RecipeItemKey, data, ref offset, BaseOffset, 88);

            FinishSerializing(data, ref offset, BaseOffset, 90, null, StoredObjectTable, null, StoredEnumListTable, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
    }
}
