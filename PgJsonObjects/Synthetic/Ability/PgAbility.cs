using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbility: IPgAbility
    {
        public PgAbility(byte[] data, int offset)
        {
            Data = data;
            Offset = offset;
        }

        public byte[] Data;
        public int Offset;

        public AbilityAnimation Animation { get { return GetEnum<AbilityAnimation>(0); } }
        public bool CanBeOnSidebar { get { return RawCanBeOnSidebar.HasValue && RawCanBeOnSidebar.Value; } }
        public bool? RawCanBeOnSidebar { get { return GetBool(2, 0); } }
        public bool CanSuppressMonsterShout { get { return RawCanSuppressMonsterShout.HasValue && RawCanSuppressMonsterShout.Value; } }
        public bool? RawCanSuppressMonsterShout { get { return GetBool(2, 2); } }
        public bool CanTargetUntargetableEnemies { get { return RawCanTargetUntargetableEnemies.HasValue && RawCanTargetUntargetableEnemies.Value; } }
        public bool? RawCanTargetUntargetableEnemies { get { return GetBool(2, 4); } }
        public bool DelayLoopIsAbortedIfAttacked { get { return RawDelayLoopIsAbortedIfAttacked.HasValue && RawDelayLoopIsAbortedIfAttacked.Value; } }
        public bool? RawDelayLoopIsAbortedIfAttacked { get { return GetBool(2, 6); } }
        public List<Deaths> CausesOfDeathList { get { return GetEnumList<Deaths>(4); } }
        public List<RecipeCost> CostList { get { return GetObjectList<RecipeCost>(8); } }
        public int CombatRefreshBaseAmount { get { return RawCombatRefreshBaseAmount.HasValue ? RawCombatRefreshBaseAmount.Value : 0; } }
        public int? RawCombatRefreshBaseAmount { get { return GetInt(12); } }
        public PowerSkill CompatibleSkill { get { return GetEnum<PowerSkill>(16); } }
        public DamageType DamageType { get { return GetEnum<DamageType>(18); } }
        public Item ConsumedItemLink { get { return GetObject<Item>(20); } }
        public double ConsumedItemChance { get { return RawConsumedItemChance.HasValue ? RawConsumedItemChance.Value : 0; } }
        public double? RawConsumedItemChance { get { return GetInt(24); } }
        public double ConsumedItemChanceToStickInCorpse { get { return RawConsumedItemChanceToStickInCorpse.HasValue ? RawConsumedItemChanceToStickInCorpse.Value : 0; } }
        public double? RawConsumedItemChanceToStickInCorpse { get { return GetDouble(28); } }
        public int ConsumedItemCount { get { return RawConsumedItemCount.HasValue ? RawConsumedItemCount.Value : 0; } }
        public int? RawConsumedItemCount { get { return GetInt(32); } }
        public string DelayLoopMessage { get { return GetString(36); } }
        public double DelayLoopTime { get { return RawDelayLoopTime.HasValue ? RawDelayLoopTime.Value : 0; } }
        public double? RawDelayLoopTime { get { return GetDouble(40); } }
        public string Description { get { return GetString(44); } }
        public AbilityIndicatingEnabled EffectKeywordsIndicatingEnabled { get { return GetEnum<AbilityIndicatingEnabled>(48); } }
        public AbilityPetType PetTypeTagReq { get { return GetEnum<AbilityPetType>(50); } }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get { return GetInt(52); } }
        public bool InternalAbility { get { return RawInternalAbility.HasValue && RawInternalAbility.Value; } }
        public bool? RawInternalAbility { get { return GetBool(56, 0); } }
        public bool IsHarmless { get { return RawIsHarmless.HasValue && RawIsHarmless.Value; } }
        public bool? RawIsHarmless { get { return GetBool(56, 2); } }
        public string InternalName { get { return GetString(60); } }
        public string ItemKeywordReqErrorMessage { get { return GetString(64); } }
        public List<AbilityItemKeyword> ItemKeywordReqList { get { return GetEnumList<AbilityItemKeyword>(68); } }
        public List<AbilityKeyword> KeywordList { get { return GetEnumList<AbilityKeyword>(72); } }
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get { return GetInt(76); } }
        public string Name { get { return GetString(80); } }
        public int PetTypeTagReqMax { get { return RawPetTypeTagReqMax.HasValue ? RawPetTypeTagReqMax.Value : 0; } }
        public int? RawPetTypeTagReqMax { get { return GetInt(84); } }
        public Ability Prerequisite { get { return GetObject<Ability>(88); } }
        public AbilityProjectile Projectile { get { return GetEnum<AbilityProjectile>(92); } }
        //public PowerSkill RawSkill { get { return GetEnum<PowerSkill>(94); } }
        public AbilityPvX PvE { get { return GetObject<AbilityPvX>(98); } }
        public AbilityPvX PvP { get { return GetObject<AbilityPvX>(102); } }
        public double ResetTime { get { return RawResetTime.HasValue ? RawResetTime.Value : 0; } }
        public double? RawResetTime { get { return GetDouble(104); } }
        public string SelfParticle { get { return GetString(108); } }
        public Ability SharesResetTimerWith { get { return GetObject<Ability>(112); } }
        public Skill Skill { get { return GetObject<Skill>(116); } }
        private bool IsSkillParsed;
        public List<AbilityRequirement> SpecialCasterRequirementList { get; } = new List<AbilityRequirement>();
        public string SpecialInfo { get; private set; }
        public int SpecialTargetingTypeReq { get { return RawSpecialTargetingTypeReq.HasValue ? RawSpecialTargetingTypeReq.Value : 0; } }
        private int? RawSpecialTargetingTypeReq;
        public AbilityTarget Target { get; private set; }
        public TargetEffectKeyword TargetEffectKeywordReq { get; private set; }
        public AbilityTargetParticle TargetParticle { get; private set; }
        public Ability UpgradeOf { get; private set; }
        public bool WorksInCombat { get { return RawWorksInCombat.HasValue && RawWorksInCombat.Value; } }
        public bool? RawWorksInCombat { get; private set; }
        public bool WorksUnderwater { get { return RawWorksUnderwater.HasValue && RawWorksUnderwater.Value; } }
        public bool? RawWorksUnderwater { get; private set; }
        public bool WorksWhileFalling { get { return RawWorksWhileFalling.HasValue && RawWorksWhileFalling.Value; } }
        public bool? RawWorksWhileFalling { get; private set; }
        public TooltipsExtraKeywords ExtraKeywordsForTooltips { get; private set; }
        public Ability AbilityGroup { get; private set; }
        public string RawSpecialCasterRequirementsErrorMessage { get; private set; }
        public bool IgnoreEffectErrors { get { return RawIgnoreEffectErrors.HasValue && RawIgnoreEffectErrors.Value; } }
        public bool? RawIgnoreEffectErrors { get; private set; }
        public Dictionary<string, Attribute> AttributesThatDeltaAmmoStickChanceTable { get; } = new Dictionary<string, Attribute>();
        public Dictionary<string, Attribute> AttributesThatDeltaDelayLoopTimeTable { get; } = new Dictionary<string, Attribute>();

        public static void Serialize(Ability source, out byte[] data, out int length)
        {
            int offset;

            offset = 0;
            SerializeInternal(source, null, ref offset);

            length = offset;
            data = new byte[length];

            offset = 0;
            SerializeInternal(source, data, ref offset);
        }

        protected static void SerializeInternal(Ability source, byte[] data, ref int offset)
        {
            AddUInt16((UInt16)source.Animation, data, ref offset);
        }

        protected static void AddUInt16(UInt16 value, byte[] data, ref int offset)
        {
            byte[] valueData = BitConverter.GetBytes(value);
            Array.Copy(valueData, 0, data, offset, valueData.Length);
            offset += valueData.Length;
        }

        protected T GetEnum<T>(int valueOffset)
        {
            return (T)(object)BitConverter.ToUInt16(Data, Offset + valueOffset);
        }

        protected bool? GetBool(int valueOffset, int valueBit)
        {
            if (((Data[Offset + valueOffset] >> valueBit) & 0x1) != 0)
                return ((Data[Offset + valueOffset] >> (valueBit + 1)) & 0x1) != 0;
            else
                return null;
        }

        protected List<T> GetEnumList<T>(int redirectionOffset)
        {
            int LengthOffset = Offset + BitConverter.ToInt32(Data, Offset + redirectionOffset);
            int Count = BitConverter.ToUInt16(Data, LengthOffset);
            int ValueOffset = LengthOffset + 2;

            List<T> Result = new List<T>(); 
            for (int i = 0; i < Count; i++)
            {
                int StoredValue = BitConverter.ToUInt16(Data, i * 2);
                T Value = (T)(object)StoredValue;
                Result.Add(Value);
            }

            return Result;
        }

        protected T GetObject<T>(int redirectionOffset)
        {
            int StoredOffset = Offset + BitConverter.ToInt32(Data, Offset + redirectionOffset);
            T Object = CreateObject<T>(StoredOffset);
            return Object;
        }

        protected List<T> GetObjectList<T>(int redirectionOffset)
        {
            int LengthOffset = Offset + BitConverter.ToInt32(Data, Offset + redirectionOffset);
            int Count = BitConverter.ToInt32(Data, LengthOffset);
            int ListOffset = LengthOffset + 4;

            List<T> Result = new List<T>();
            for (int i = 0; i < Count; i++)
            {
                int StoredOffset = Offset + BitConverter.ToInt32(Data, ListOffset + i * 4);

                T Object = CreateObject<T>(StoredOffset);
                Result.Add(Object);
            }

            return Result;
        }

        protected T CreateObject<T>(int offsetObject)
        {
            if (typeof(T) == typeof(PgAbility))
                return (T)(object)(new PgAbility(Data, offsetObject));
            else
                return default(T);
        }

        protected int? GetInt(int valueOffset)
        {
            int Value = BitConverter.ToInt32(Data, Offset + valueOffset);

            if (Value == 0x6B6B6B6B)
                return null;
            else
                return Value;
        }

        protected double? GetDouble(int valueOffset)
        {
            float Value = BitConverter.ToSingle(Data, Offset + valueOffset);

            if (float.IsNaN(Value))
                return null;
            else
                return Value;
        }

        protected string GetString(int redirectionOffset)
        {
            int StoredOffset = Offset + BitConverter.ToInt32(Data, Offset + redirectionOffset);
            string Result = CreateString(StoredOffset);
            return Result;
        }

        protected string CreateString(int offsetString)
        {
            int Count = BitConverter.ToUInt16(Data, offsetString);
            int CharacterOffset = offsetString + 2;

            string Result = "";
            for (int i = 0; i < Count; i++)
            {
                char CharacterValue = BitConverter.ToChar(Data, i * 2);
                Result += CharacterValue;
            }

            return Result;
        }
    }
}
