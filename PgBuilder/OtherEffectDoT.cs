﻿namespace PgBuilder
{
    using PgJsonObjects;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class OtherEffectDoT : OtherEffect
    {
        public static bool DebugTrace { get; } = false;

        public OtherEffectDoT(string abilityName, IPgDoT dot)
        {
            AbilityName = abilityName;
            DoT = dot;
            BaseValue = dot.DamagePerTick * dot.NumTicks;
            DeltaValue = 0;
            MultiplierValue = 1.0F;
            BoostMultiplierValue = 1.0F;

            if (DebugTrace)
            {
                int Result = CalculateFinalValue(DoT.NumTicks, DoT.DamagePerTick, DeltaValue, MultiplierValue, BoostMultiplierValue, HasAttributesThatMod);
                Debug.WriteLine($"{AbilityName}: base, total {Result}");
            }
        }

        public string AbilityName { get; }
        public IPgDoT DoT { get; }
        public int BaseValue { get; private set; }
        public int DisplayedValue
        {
            get
            {
                if (AbilityName == "Righteous Flame 8")
                {
                }

                if (AbilityName == "Fire Arrow 8")
                {
                }

                if (AbilityName == "Gut 8")
                {
                }

                int Result = CalculateFinalValue(DoT.NumTicks, DoT.DamagePerTick, DeltaValue, MultiplierValue, BoostMultiplierValue, HasAttributesThatMod);

                return Result;
            }
        }
        private float DeltaValue;
        private float MultiplierValue;
        private float BoostMultiplierValue;
        public bool RequireNoAggro { get { return DoT.SpecialRuleList.Contains(DoTSpecialRule.IfTargetNotLooking); } }
        public bool DelayedDoT { get { return DoT.SpecialRuleList.Contains(DoTSpecialRule.DelayedDamage); } }
        public DamageType DamageType { get { return DoT.DamageType; } }
        public bool HasAttributesThatMod { get { return ((IList<IPgAttribute>)DoT.AttributesThatModList).Count > 0; } }

        public override void Reset()
        {
            DeltaValue = 0;
            MultiplierValue = 1.0F;
            BoostMultiplierValue = 1.0F;

            if (DebugTrace)
            {
                int Result = CalculateFinalValue(DoT.NumTicks, DoT.DamagePerTick, DeltaValue, MultiplierValue, BoostMultiplierValue, HasAttributesThatMod);
                Debug.WriteLine($"{AbilityName}: reset, total {Result}");
            }
        }

        public void AddDelta(float value)
        {
            DeltaValue += value;

            if (DebugTrace)
            {
                int Result = CalculateFinalValue(DoT.NumTicks, DoT.DamagePerTick, DeltaValue, MultiplierValue, BoostMultiplierValue, HasAttributesThatMod);
                Debug.WriteLine($"{AbilityName}: add {(int)value}, total {Result}");
            }
        }

        public void AddMultiplier(float value)
        {
            MultiplierValue += value;

            if (DebugTrace)
            {
                int Result = CalculateFinalValue(DoT.NumTicks, DoT.DamagePerTick, DeltaValue, MultiplierValue, BoostMultiplierValue, HasAttributesThatMod);
                Debug.WriteLine($"{AbilityName}: multiply {(int)value * 100}, total {Result}");
            }
        }

        public void AddBoostMultiplier(float value)
        {
            BoostMultiplierValue += value;

            if (DebugTrace)
            {
                int Result = CalculateFinalValue(DoT.NumTicks, DoT.DamagePerTick, DeltaValue, MultiplierValue, BoostMultiplierValue, HasAttributesThatMod);
                Debug.WriteLine($"{AbilityName}: boost multiply {(int)value * 100}, total {Result}");
            }
        }

        public string Prefix
        {
            get
            {
                string Result;

                if (string.IsNullOrEmpty(DoT.RawPreface))
                {
                    if (RequireNoAggro)
                        Result = "If target's focus is not on you, deals";
                    else
                        Result = "Deals";
                }
                else
                    Result = DoT.RawPreface;

                return Result;
            }
        }

        //public bool IsDisplayable { get { return !DoT.SpecialRuleList.Contains(DoTSpecialRule.BuffActivated) || DoT.SpecialRuleList.Contains(DoTSpecialRule.IfTargetNotLooking); } }
        //public bool IsDisplayable { get { return !DoT.SpecialRuleList.Contains(DoTSpecialRule.BuffActivated) || BaseValue != 0; } }
        public bool IsDisplayable { get { return true; } }
        public override bool IsDisplayed { get { return IsDisplayable && (DeltaValue != 0 || BaseValue != 0); } }

        public string Damage { get { return DisplayedValue.ToString(); } }

        public string Suffix
        {
            get
            {
                string DamageTypeString = TextMaps.DamageTypeTextMap[DamageType];
                string ChangedValue = DoT.SpecialRuleList.Contains(DoTSpecialRule.ArmorDamage) ? "Armor" : "Health";
                int Duration = DoT.Duration;

                return $"{DamageTypeString} damage to {ChangedValue} over {Duration} seconds";
            }
        }

        public static int CalculateFinalValue(int numTicks, int baseDamagePerTick, float deltaDamage, float multiplierValue, float boostMultiplierValue, bool hasAttributesThatMod)
        {
            int Result;

            if (hasAttributesThatMod)
            {
                float TickBase = baseDamagePerTick;
                TickBase = (float)Math.Floor(TickBase * multiplierValue * boostMultiplierValue);

                int TotalRounded;

                if (deltaDamage != 0)
                {
                    float TickDelta = (float)Math.Floor(deltaDamage / numTicks);
                    TickDelta = TickDelta * multiplierValue * boostMultiplierValue;

                    TotalRounded = (int)Math.Floor(TickBase + TickDelta);
                }
                else
                    TotalRounded = (int)Math.Ceiling(TickBase);

                Result = TotalRounded * numTicks;
            }
            else
            {
                float TickBase = baseDamagePerTick;
                TickBase = (float)Math.Floor(TickBase * boostMultiplierValue);
                TickBase *= multiplierValue;

                int TotalRounded;

                if (deltaDamage != 0)
                {
                    float TickDelta = (float)Math.Floor(deltaDamage / numTicks);
                    TickDelta *= multiplierValue + boostMultiplierValue - 1.0F;

                    TotalRounded = (int)Math.Floor(TickBase + TickDelta);
                }
                else
                    TotalRounded = (int)Math.Ceiling(TickBase);

                Result = TotalRounded * numTicks;
            }

            return Result;
        }

        public override string ToString()
        {
            return AbilityName;
        }
    }
}
