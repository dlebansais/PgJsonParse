using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgPowerSimpleEffect
    {
        string Description { get; }
        List<int> IconIdList { get; }
    }
}
