namespace PgBuilder
{
    using PgJsonObjects;
    using System;
    using System.Diagnostics;

    public class OtherEffectDoT : OtherEffect
    {
        public OtherEffectDoT(string abilityName, IPgDoT dot)
        {
            AbilityName = abilityName;
            DoT = dot;
            BaseValue = dot.DamagePerTick * dot.NumTicks;
            DeltaValue = 0;
            MultiplierValue = 1.0;
            BoostMultiplierValue = 1.0;
            //Debug.WriteLine($"{AbilityName}: base {DisplayedValue}");
        }

        public string AbilityName { get; }
        public IPgDoT DoT { get; }
        public int BaseValue { get; private set; }
        public int DisplayedValue { get { return (int)Math.Round((((float)BaseValue + (float)DeltaValue) * (float)MultiplierValue) * (float)BoostMultiplierValue, MidpointRounding.AwayFromZero); } }
        private double DeltaValue;
        private double MultiplierValue;
        private double BoostMultiplierValue;
        public bool RequireNoAggro { get { return DoT.SpecialRuleList.Contains(DoTSpecialRule.IfTargetNotLooking); } }
        public DamageType DamageType { get { return DoT.DamageType; } }

        public void AddDelta(double value)
        {
            DeltaValue += value;
            //Debug.WriteLine($"{AbilityName}: add {(int)value}, total {DisplayedValue}");
        }

        public void AddMultiplier(double value)
        {
            MultiplierValue += value;
            //Debug.WriteLine($"{AbilityName}: multiply {(int)(value * 100)}, total {DisplayedValue}");
        }

        public void AddBoostMultiplier(double value)
        {
            BoostMultiplierValue += value;
            //Debug.WriteLine($"{AbilityName}: boost multiply {(int)(value * 100)}, total {DisplayedValue}");
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

        public bool IsDisplayable { get { return !DoT.SpecialRuleList.Contains(DoTSpecialRule.BuffActivated) || DoT.SpecialRuleList.Contains(DoTSpecialRule.IfTargetNotLooking); } }
        public override bool IsDisplayed { get { return IsDisplayable && (DisplayedValue != 0 || BaseValue != 0); } }

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
    }
}
