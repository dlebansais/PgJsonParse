namespace PgJsonObjects
{
    public class PgXpTable
    {
        public string InternalName { get; set; }
        public PgXpTableLevelCollection XpAmountList { get; } = new PgXpTableLevelCollection();
        public XpTableCategory CategoryName { get; set; }
    }
}
