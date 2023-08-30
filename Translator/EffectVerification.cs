namespace PgObjects
{
    using System.Collections.Generic;

    public static class EffectVerification
    {
        public static Dictionary<CombatKeyword, List<EffectVerificationEntry>> EffectVerificationTable { get; } = new Dictionary<CombatKeyword, List<EffectVerificationEntry>>()
        {
            {
                CombatKeyword.RestoreHealth, new List<EffectVerificationEntry>()
                {
                    new EffectVerificationEntry() { Prefix = "Restore", Suffix = "Health" },
                    new EffectVerificationEntry() { Prefix = "Restores", Suffix = "Health" },
                    new EffectVerificationEntry() { Prefix = "Heal Target", Suffix = "Health" },
                    new EffectVerificationEntry() { Prefix = "Restore", Suffix = "Health (or Armor if Health is full) to nearby ally undead" },
                    new EffectVerificationEntry() { Prefix = "Nearby Firewalls Heal", Suffix = string.Empty },
                    new EffectVerificationEntry() { Prefix = "Restore", Suffix = "Health to yourself" },
                    new EffectVerificationEntry() { Prefix = "Restores", Suffix = "Health to yourself" },
                    new EffectVerificationEntry() { Prefix = "Restores", Suffix = "Health/Armor after a 10-second delay" },
                    new EffectVerificationEntry() { Prefix = "Restores", Suffix = "Health to Target" },
                    new EffectVerificationEntry() { Prefix = "Restore", Suffix = "Health to you and nearby allies" },
                    new EffectVerificationEntry() { Prefix = "Restores", Suffix = "Health (or Armor if Health is full) to Pet" },
                    new EffectVerificationEntry() { Prefix = "Existing Zombie is Healed", Suffix = "Health" },
                    new EffectVerificationEntry() { Prefix = "Restores", Suffix = "Health/Sec when near your Web Trap", AllowRecurrence = true },
                    new EffectVerificationEntry() { Prefix = "Restore", Suffix = "Health to least-healthy ally" },
                    new EffectVerificationEntry() { Prefix = "For 8 seconds, each time target attacks and damages you, heal", Suffix = "Health" },
                    new EffectVerificationEntry() { Prefix = "Heal", Suffix = "Health every 4 seconds", AllowRecurrence = true },
                    new EffectVerificationEntry() { Prefix = "Restore", Suffix = "Health over 8 seconds", AllowDuration = true },
                    new EffectVerificationEntry() { Prefix = "Restores", Suffix = "Health 5 times over 15 secs", AllowDuration = true },
                    new EffectVerificationEntry() { Prefix = "You heal", Suffix = "per second when near your fire wall", AllowRecurrence = true },
                }
            },
            {
                CombatKeyword.RestorePower, new List<EffectVerificationEntry>()
                {
                    new EffectVerificationEntry() { Prefix = "Restores", Suffix = "Power" },
                    new EffectVerificationEntry() { Prefix = "Restore", Suffix = "Power" },
                    new EffectVerificationEntry() { Prefix = "Restores", Suffix = "Power Per Second when near your Web Trap", AllowRecurrence = true },
                    new EffectVerificationEntry() { Prefix = "Recover", Suffix = "Power when melee attacks deal damage to you" },
                    new EffectVerificationEntry() { Prefix = "Restores", Suffix = "Power after a 12-second delay" },
                }
            },
            {
                CombatKeyword.RestoreArmor, new List<EffectVerificationEntry>()
                {
                    new EffectVerificationEntry() { Prefix = "Restores", Suffix = "Armor" },
                    new EffectVerificationEntry() { Prefix = "Restore", Suffix = "Armor" },
                    new EffectVerificationEntry() { Prefix = "Restores", Suffix = "Armor after a 6-second delay" },
                    new EffectVerificationEntry() { Prefix = "Restores", Suffix = "Armor after a 10-second delay" },
                }
            },
            {
                CombatKeyword.RestoreHealthArmor, new List<EffectVerificationEntry>()
                {
                    new EffectVerificationEntry() { Prefix = "Restore", Suffix = "Health (or Armor if Health is full) to nearby ally undead" },
                }
            },
            {
                CombatKeyword.RestoreHealthArmorPower, new List<EffectVerificationEntry>()
                {
                    new EffectVerificationEntry() { Prefix = "Restore", Suffix = "Armor every 3 seconds", AllowRecurrence = true },
                    new EffectVerificationEntry() { Prefix = "Restore", Suffix = "Health every 4 seconds", AllowRecurrence = true },
                    new EffectVerificationEntry() { Prefix = "Restore", Suffix = "Power every 8 seconds", AllowRecurrence = true },
                }
            },
            {
                CombatKeyword.AddMaxHealth, new List<EffectVerificationEntry>()
                {
                    new EffectVerificationEntry() { Prefix = "Existing Zombie's Max Health", Suffix = "for 60 seconds", AllowDuration = true },
                }
            },
            {
                CombatKeyword.DrainHealth, new List<EffectVerificationEntry>()
                {
                    new EffectVerificationEntry() { Prefix = "Reaps", Suffix = "of the Health damage done (up to the max)" },
                }
            },
            {
                CombatKeyword.DrainArmor, new List<EffectVerificationEntry>()
                {
                    new EffectVerificationEntry() { Prefix = "Reaps", Suffix = "of the Armor damage done (up to the max)" },
                }
            },
            {
                CombatKeyword.DrainHealthMax, new List<EffectVerificationEntry>()
                {
                    new EffectVerificationEntry() { Prefix = "Max Health Reaped", Suffix = string.Empty },
                }
            },
            {
                CombatKeyword.DrainArmorMax, new List<EffectVerificationEntry>()
                {
                    new EffectVerificationEntry() { Prefix = "Max Armor Reaped", Suffix = string.Empty },
                }
            },
            {
                CombatKeyword.DamageBoost, new List<EffectVerificationEntry>()
                {
                    new EffectVerificationEntry() { Prefix = "Deal", Suffix = "Damage every 2 seconds" },
                    new EffectVerificationEntry() { Prefix = "Deal", Suffix = "Trauma Damage every 2 seconds" },
                    new EffectVerificationEntry() { Prefix = "Existing Zombie's Direct Damage", Suffix = "for 60 seconds", AllowDuration = true },
                    new EffectVerificationEntry() { Prefix = "Existing Zombie's Direct Damage", Suffix = "for 5 minutes", AllowDuration = true },
                    new EffectVerificationEntry() { Prefix = "Existing Zombie's Damage Boosted", Suffix = "for 60 seconds", AllowDuration = true },
                    new EffectVerificationEntry() { Prefix = "Minion Damage", Suffix = "for 10 seconds", AllowDuration = true },
                    new EffectVerificationEntry() { Prefix = "Minion Damage Boost", Suffix = "for 10 seconds", AllowDuration = true },
                    new EffectVerificationEntry() { Prefix = "All your attacks deal", Suffix = "damage for 10 seconds", AllowDuration = true },
                    new EffectVerificationEntry() { Prefix = "For 8 seconds, each time target attacks and damages you, they suffer", Suffix = "Trauma damage" },
                    new EffectVerificationEntry() { Prefix = "Future Infinite Legs attack damage +", Suffix = "for 10 seconds", AllowDuration = true },
                    new EffectVerificationEntry() { Prefix = "Future Infinite Legs attack damage +", Suffix = "for 12 seconds", AllowDuration = true },
                    new EffectVerificationEntry() { Prefix = "Future Infinite Legs attack damage +", Suffix = "for 15 seconds", AllowDuration = true },
                    new EffectVerificationEntry() { Prefix = "Future Infinite Legs attack damage +", Suffix = "for 18 seconds", AllowDuration = true },
                    new EffectVerificationEntry() { Prefix = "Undead targets suffer", Suffix = "indirect Nature damage" },
                    new EffectVerificationEntry() { Prefix = "Target suffers", Suffix = "Psychic damage after a 4-second delay" },
                    new EffectVerificationEntry() { Prefix = "Attack Damage Boost", Suffix = "for 10 seconds", AllowDuration = true },
                }
            },
            {
                CombatKeyword.DebuffMitigation, new List<EffectVerificationEntry>()
                {
                    new EffectVerificationEntry() { Prefix = "Target's mitigation reduced", Suffix = "for 30 seconds", AllowDuration = true },
                    new EffectVerificationEntry() { Prefix = "Target's Mitigation", Suffix = "for 30 seconds", AllowDuration = true },
                }
            },
            {
                CombatKeyword.AddSprintSpeed, new List<EffectVerificationEntry>()
                {
                    new EffectVerificationEntry() { Prefix = "Sprint Speed", Suffix = "(out of combat)" },
                }
            },
            {
                CombatKeyword.AddMitigation, new List<EffectVerificationEntry>()
                {
                    new EffectVerificationEntry() { Prefix = "Mitigates", Suffix = "Physical Damage" },
                    new EffectVerificationEntry() { Prefix = "Mitigates", Suffix = "Additional Physical Damage from Elites", TargetElite = true },
                }
            },
        };
    }
}
