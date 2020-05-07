namespace PgBuilder
{
    using PgJsonObjects;
    using System;

    public class OtherEffectDoT : OtherEffect
    {
        public OtherEffectDoT(IPgDoT dot)
        {
            DoT = dot;
            BaseValue = dot.DamagePerTick;
            ModifiedValue = 0;
        }

        public IPgDoT DoT { get; }
        public int BaseValue { get; private set; }
        public int DisplayedValue { get { return (int)Math.Round(BaseValue + ModifiedValue); } }
        private double ModifiedValue;

        public void AddValue(double value)
        {
            ModifiedValue += value;
        }

        public string Prefix
        {
            get
            {
                string Result;

                if (string.IsNullOrEmpty(DoT.RawPreface))
                {
                    if (DoT.SpecialRuleList.Contains( DoTSpecialRule.IfTargetNotLooking))
                        Result = "If target's focus is not on you, deals";
                    else
                        Result = "Deals";
                }
                else
                    Result = DoT.RawPreface;

                return Result;
            }
        }

        public override bool IsDisplayed { get { return (DisplayedValue != 0 || BaseValue != 0) && !DoT.SpecialRuleList.Contains(DoTSpecialRule.BuffActivated); } }

        public string Damage { get { return DisplayedValue.ToString(); } }

        public string Suffix
        {
            get
            {
                string DamageType = TextMaps.DamageTypeTextMap[DoT.DamageType];
                string ChangedValue = DoT.SpecialRuleList.Contains(DoTSpecialRule.ArmorDamage) ? "Armor" : "Health";
                int Duration = DoT.Duration;

                return $"{DamageType} damage to {ChangedValue} over {Duration} seconds";
            }
        }
    }
}
