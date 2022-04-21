namespace PgObjects
{
    using System.Collections.Generic;

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
    }
}
