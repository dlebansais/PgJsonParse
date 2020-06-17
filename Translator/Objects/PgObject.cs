namespace PgObjects
{
    public abstract class PgObject
    {
        public static int AbilityIconId { get; } = 108;
        public static int AttributeIconId { get; } = 103;
        public static int DirectedGoalIconId { get; } = 2118;
        public static int EffectIconId { get; } = 108;
        public static int LoreBookIconId { get; } = 5792;
        public static int NpcIconId { get; } = 3589;
        public static int PlayerTitleIconId { get; } = 5851;
        public static int PowerIconId { get; } = 108;
        public static int StorageVaultIconId { get; } = 5116;
        public static int KillIconId { get; } = 3402;

        public abstract int ObjectIconId { get; }
        public abstract string ObjectName { get; }
    }
}
