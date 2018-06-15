using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveUseAbility : GenericPgObject, IPgQuestObjectiveUseAbility
    {
        public PgQuestObjectiveUseAbility(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public List<Ability> AbilityTargetList { get { return GetObjectList(0, ref _AbilityTargetList); } } private List<Ability> _AbilityTargetList;
        public AbilityKeyword AbilityTarget { get { return GetEnum<AbilityKeyword>(4); } }
    }
}
