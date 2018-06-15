using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgQuestObjectiveUseAbility
    {
        List<Ability> AbilityTargetList { get; }
        AbilityKeyword AbilityTarget { get; }
    }
}
