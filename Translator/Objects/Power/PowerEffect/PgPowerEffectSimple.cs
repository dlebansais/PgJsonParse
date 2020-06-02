namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgPowerEffectSimple : PgPowerEffect
    {
        public string Description { get; set; } = string.Empty;
        public List<int> IconIdList { get; set; }
    }
}
