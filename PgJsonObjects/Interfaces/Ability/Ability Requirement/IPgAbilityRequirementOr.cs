using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgAbilityRequirementOr
    {
        List<AbilityRequirement> OrList { get; }
        string ErrorMsg { get; }
    }
}
