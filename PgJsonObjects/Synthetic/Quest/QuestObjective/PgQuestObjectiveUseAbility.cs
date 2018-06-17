namespace PgJsonObjects
{
    public class PgQuestObjectiveUseAbility : GenericPgObject<PgQuestObjectiveUseAbility>, IPgQuestObjectiveUseAbility
    {
        public PgQuestObjectiveUseAbility(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveUseAbility CreateItem(byte[] data, int offset)
        {
            return new PgQuestObjectiveUseAbility(data, offset);
        }

        public AbilityCollection AbilityTargetList { get { return GetObjectList(0, ref _AbilityTargetList, AbilityCollection.CreateItem, () => new AbilityCollection()); } } private AbilityCollection _AbilityTargetList;
        public AbilityKeyword AbilityTarget { get { return GetEnum<AbilityKeyword>(4); } }
    }
}
