namespace PgJsonObjects
{
    public interface IPgXpTable : IJsonKey, IObjectContentGenerator
    {
        string InternalName { get; }
        XpTableLevelCollection XpAmountList { get; }
        XpTableEnum EnumName { get; }
    }
}
