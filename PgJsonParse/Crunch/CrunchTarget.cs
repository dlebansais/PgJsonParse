using PgJsonObjects;
using System;
using System.Collections.Generic;

namespace PgJsonParse
{
    public class CrunchTarget
    {
        public static double EffectiveRatio = 1.0;
        public static double IneffectiveRatio = 1.0;
        public static double VeryEffectiveRatio = 1.0;
        public static double VeryIneffectiveRatio = 1.0;

        public CrunchTarget()
        {
            ArmorMitigation = 0.04;
            Evasion = 0;
        }

        public int MaxArmor { get; set; }
        public int MaxHealth { get; set; }
        public int MaxRage { get; set; }
        public List<DamageType> Effective { get; set; }
        public List<DamageType> Ineffective { get; set; }
        public List<DamageType> VeryEffective { get; set; }
        public List<DamageType> VeryIneffective { get; set; }
        public List<DamageType> Immune { get; set; }
        public double ArmorMitigation { get; set; }
        public double Evasion { get; set; }

        public double Armor { get; private set; }
        public double Health { get; private set; }
        public double Rage { get; private set; }
        public double RageThreshold { get; private set; }
        public bool IsVulnerable { get; private set; }

        public bool IsDead { get; private set; }
        public double TotalDamageReceived { get; private set; }

        public void StartFight()
        {
            Armor = MaxArmor;
            Health = MaxHealth;
            Rage = 0;
            RageThreshold = MaxRage;
            IsVulnerable = false;

            IsDead = false;
            TotalDamageReceived = 0;
        }

        public void HitWithAbility(Ability Ability, double AttackAccuracy, bool AoEOnly)
        {
            if (IsDead)
                return;

            double ActualEvasion = Evasion - Ability.PvE.Accuracy;
            if (AttackAccuracy < ActualEvasion)
                return;

            double Damage = 0;
            double HealthDamage = 0;
            double ArmorDamage = 0;

            if (!AoEOnly)
            {
                Damage += Ability.PvE.Damage;
                Damage += Ability.PvE.ExtraDamageIfTargetVulnerable;
                HealthDamage += Ability.PvE.HealthSpecificDamage;
                ArmorDamage += Ability.PvE.ArmorSpecificDamage;
            }
            Damage += Ability.PvE.AoE;

            double VulnerabilityModifier = GetVulnerabilityModifier(Ability);
            Damage *= VulnerabilityModifier;
            HealthDamage *= VulnerabilityModifier;
            ArmorDamage *= VulnerabilityModifier;

            DealDamage(Damage, HealthDamage, ArmorDamage);

            Rage += Ability.PvE.RageBoost;
            RageThreshold *= Ability.PvE.RageMultiplier;
        }

        public double GetVulnerabilityModifier(Ability Ability)
        {
            double VulnerabilityModifier;

            if (Effective != null && Effective.Contains(Ability.DamageType))
                VulnerabilityModifier = EffectiveRatio;
            else if (Ineffective != null && Ineffective.Contains(Ability.DamageType))
                VulnerabilityModifier = IneffectiveRatio;
            else if (VeryEffective != null && VeryEffective.Contains(Ability.DamageType))
                VulnerabilityModifier = VeryEffectiveRatio;
            else if (VeryIneffective != null && VeryIneffective.Contains(Ability.DamageType))
                VulnerabilityModifier = VeryIneffectiveRatio;
            else if (Immune != null && Immune.Contains(Ability.DamageType))
                VulnerabilityModifier = 0;
            else
                VulnerabilityModifier = 1.0;

            return VulnerabilityModifier;
        }

        public void DealDamage(double Damage, double HealthDamage, double ArmorDamage)
        {
            TotalDamageReceived += Damage;
            TotalDamageReceived += HealthDamage;

            double MitigatedDamage = Armor * ArmorMitigation;
            if (Damage > MitigatedDamage)
                Damage -= MitigatedDamage;
            else
                Damage = 0;

            HealthDamage += Damage / 2;
            ArmorDamage += Damage / 2;

            if (Health > HealthDamage)
                Health -= HealthDamage;
            else
            {
                Health = 0;
                IsDead = true;
            }

            if (Armor > ArmorDamage)
                Armor -= ArmorDamage;
            else
                Armor = 0;
        }

        public void TriggerVulnerability(double Chance, Random rng)
        {
            if (IsVulnerable)
            {
                //TODO : turn it off
            }
            else
            {
                if (rng.NextDouble() <= Chance)
                    IsVulnerable = true;
            }
        }
    }
}
