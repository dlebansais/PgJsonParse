namespace PgJsonObjects
{
    public class PgQuestObjectiveUseAbility : GenericPgObject<PgQuestObjectiveUseAbility>, IPgQuestObjectiveUseAbility
    {
        public PgQuestObjectiveUseAbility(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveUseAbility CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveUseAbility CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveUseAbility(data, ref offset);
        }

        public AbilityCollection AbilityTargetList { get { return GetObjectList(0, ref _AbilityTargetList, AbilityCollection.CreateItem, () => new AbilityCollection()); } } private AbilityCollection _AbilityTargetList;
        public AbilityKeyword AbilityTarget { get { return GetEnum<AbilityKeyword>(4); } }
    }
}
