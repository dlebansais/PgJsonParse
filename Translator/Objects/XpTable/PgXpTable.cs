namespace PgObjects
{
    using System.Collections.Generic;

    public class PgXpTable
    {
        public string Key { get; set; } = string.Empty;
        public string InternalName { get; set; } = string.Empty;
        public List<int> XpAmountList { get; set; } = new List<int>();
    }
}
