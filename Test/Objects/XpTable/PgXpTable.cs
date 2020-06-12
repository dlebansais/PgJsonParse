namespace PgObjects
{
    using System.Collections.Generic;

    public class PgXpTable
    {
        public string InternalName { get; set; } = string.Empty;
        public List<int> XpAmountList { get; } = new List<int>();
    }
}
