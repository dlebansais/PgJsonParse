namespace PgObjects
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    public abstract class PgObject
    {
        public static PgNullObject Null { get; } = new PgNullObject();

        public static int AbilityIconId { get; } = 108;
        public static int AttributeIconId { get; } = 103;
        public static int DirectedGoalIconId { get; } = 2118;
        public static int EffectIconId { get; } = 108;
        public static int LoreBookIconId { get; } = 5792;
        public static int NpcIconId { get; } = 3592;
        public static int PlayerTitleIconId { get; } = 5851;
        public static int PowerIconId { get; } = 108;
        public static int StorageVaultIconId { get; } = 3585;
        public static int SkillIconId { get; } = 103;
        public static int KillIconId { get; } = 3402;

        public string Key { get; set; } = string.Empty;
        public List<string> LinkList { get; set; } = new List<string>();

        public abstract int ObjectIconId { get; }
        public abstract string ObjectName { get; }

        public static string GetItemKey(PgObject item)
        {
            string Prefix = null!;

            switch (item)
            {
                case PgAbility AsAbility:
                    Prefix = "A";
                    break;

                case PgAttribute AsAttribute:
                    Prefix = "T";
                    break;

                case PgDirectedGoal AsDirectedGoal:
                    Prefix = "D";
                    break;

                case PgEffect AsEffect:
                    Prefix = "E";
                    break;

                case PgItem AsItem:
                    Prefix = "I";
                    break;

                case PgLoreBook AsLoreBook:
                    Prefix = "L";
                    break;

                case PgNpc AsNpc:
                    Prefix = "N";
                    break;

                case PgPlayerTitle AsPlayerTitle:
                    Prefix = "P";
                    break;

                case PgPower AsPower:
                    Prefix = "W";
                    break;

                case PgQuest AsQuest:
                    Prefix = "Q";
                    break;

                case PgRecipe AsRecipe:
                    Prefix = "R";
                    break;

                case PgSkill AsSkill:
                    Prefix = "S";
                    break;

                case PgStorageVault AsStorageVault:
                    Prefix = "V";
                    break;
            }

            Debug.Assert(Prefix != null);

            PropertyInfo? KeyProperty = item.GetType()?.GetProperty("Key");
            string? Key = KeyProperty?.GetValue(item) as string;

            if (Key is not null && Key.Length > 0)
                return $"{Prefix}{Key}";
            else
                return string.Empty;
        }
    }
}
