namespace PgJsonObjects
{
    public interface IPgXpTable
    {
        string InternalName { get; }
        XpTableLevelCollection XpAmountList { get; }
        XpTableEnum EnumName { get; }
    }
}
