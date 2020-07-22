namespace PgObjects
{
    using System.Collections.Generic;

    public static class EffectVerification
    {
        public static Dictionary<CombatKeyword, List<KeyValuePair<string, string>>> EffectVerificationTable = new Dictionary<CombatKeyword, List<KeyValuePair<string, string>>>()
        {
            { CombatKeyword.RestoreHealth, new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Restore", "Health"),
                    new KeyValuePair<string, string>("Restores", "Health"),
                    new KeyValuePair<string, string>("Heal Target", "Health"),
                    new KeyValuePair<string, string>("Restore", "Health (or Armor if Health is full) to nearby ally undead"),
                    new KeyValuePair<string, string>("Nearby Firewalls Heal", ""),
                    new KeyValuePair<string, string>("Restore", "Health to yourself"),
                    new KeyValuePair<string, string>("Restores", "Health to yourself"),
                    new KeyValuePair<string, string>("Restores", "Health after a 10-second delay"),
                    new KeyValuePair<string, string>("Restores", "Health to Target"),
                    new KeyValuePair<string, string>("Restore", "Health to you and nearby allies"),
                    new KeyValuePair<string, string>("Restores", "Health (or Armor if Health is full) to Pet"),
                    new KeyValuePair<string, string>("Existing Zombie is Healed", "Health"),
                    new KeyValuePair<string, string>("You heal", "per second when near your web trap"),
                    new KeyValuePair<string, string>("Restore", "Health to least-healthy ally"),
                    new KeyValuePair<string, string>("For 8 seconds, each time target attacks and damages you, heal", "Health"),
                    new KeyValuePair<string, string>("Heal", "Health every 4 seconds"),
                }
            },
            { CombatKeyword.RestorePower, new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Restores", "Power"),
                    new KeyValuePair<string, string>("Restore", "Power"),
                    new KeyValuePair<string, string>("You recover", "Power per second when near your web trap"),
                    new KeyValuePair<string, string>("Recover", "Power when melee attacks deal damage to you"),
                    new KeyValuePair<string, string>("Restores", "Power after a 12-second delay"),
                }
            },
            { CombatKeyword.RestoreArmor, new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Restores", "Armor"),
                    new KeyValuePair<string, string>("Restore", "Armor"),
                    new KeyValuePair<string, string>("Restores", "Armor after a 6-second delay"),
                    new KeyValuePair<string, string>("Restores", "Armor after a 10-second delay"),
                }
            },
            { CombatKeyword.RestoreHealthArmor, new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Restore", "Health (or Armor if Health is full) to nearby ally undead"),
                }
            },
            { CombatKeyword.RestoreHealthArmorPower, new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Restore", "Armor every 3 seconds"),
                    new KeyValuePair<string, string>("Restore", "Health every 4 seconds"),
                    new KeyValuePair<string, string>("Restore", "Power every 8 seconds"),
                }
            },
            { CombatKeyword.AddMaxHealth, new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Existing Zombie's Max Health", "for 60 seconds"),
                }
            },
            { CombatKeyword.DrainHealth, new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Reaps", "of the Health damage done (up to the max)"),
                }
            },
            { CombatKeyword.DrainArmor, new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Reaps", "of the Armor damage done (up to the max)"),
                }
            },
            { CombatKeyword.DrainHealthMax, new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Max Health Reaped", ""),
                }
            },
            { CombatKeyword.DrainArmorMax, new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Max Armor Reaped", ""),
                }
            },
            { CombatKeyword.DamageBoost, new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Deal", "Damage every 2 seconds"),
                    new KeyValuePair<string, string>("Existing Zombie's Direct Damage", "for 60 seconds"),
                    new KeyValuePair<string, string>("Existing Zombie's Direct Damage", "for 5 minutes"),
                    new KeyValuePair<string, string>("Existing Zombie's Damage Boosted", "for 60 seconds"),
                    new KeyValuePair<string, string>("Minion Damage", "for 10 seconds"),
                    new KeyValuePair<string, string>("Minion Damage Boost", "for 10 seconds"),
                    new KeyValuePair<string, string>("All your attacks deal", "damage for 10 seconds"),
                    new KeyValuePair<string, string>("For 8 seconds, each time target attacks and damages you, they suffer", "Trauma damage"),
                }
            },
            { CombatKeyword.DebuffMitigation, new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Target's mitigation reduced", "for 30 seconds"),
                }
            },
            { CombatKeyword.AddSprintSpeed, new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Sprint Speed", "(out of combat)"),
                }
            },
        };
    }
}
