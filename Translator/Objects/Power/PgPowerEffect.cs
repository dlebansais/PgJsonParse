namespace PgObjects
{
    using System.Collections.Generic;

    public class PgPowerEffect
    {
        public string Description { get; set; } = string.Empty;
        public List<int> IconIdList { get; set; } = new List<int>();
    }
}
