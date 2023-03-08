using System.Collections.Generic;

namespace PgObjects
{
    public class PgAbilityRequirementEntityPhysicalState : PgAbilityRequirement
    {
        public List<AllowedState> AllowedStateList { get; set; } = new List<AllowedState>();
    }
}
