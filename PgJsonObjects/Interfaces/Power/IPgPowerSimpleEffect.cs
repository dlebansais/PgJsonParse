using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgPowerSimpleEffect : IPgPowerEffect
    {
        string Description { get; }
        List<int> IconIdList { get; }
    }
}
