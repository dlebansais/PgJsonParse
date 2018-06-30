using System.Collections.Generic;

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

        public override string Key { get { return GetString(0); } }
        public AbilityCollection AbilityTargetList { get { return GetObjectList(4, ref _AbilityTargetList, AbilityCollection.CreateItem, () => new AbilityCollection()); } } private AbilityCollection _AbilityTargetList;
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public AbilityKeyword AbilityTarget { get { return GetEnum<AbilityKeyword>(12); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
