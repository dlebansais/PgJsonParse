namespace PgObjects
{
    public class PgXpTable
    {
        public string Key { get; set; } = string.Empty;
        public string InternalName { get; set; }
        public XpTableEnum AsEnum { get; set; }
        public PgXpTableLevelCollection XpAmountList { get; set; } = new PgXpTableLevelCollection();
    }
}
