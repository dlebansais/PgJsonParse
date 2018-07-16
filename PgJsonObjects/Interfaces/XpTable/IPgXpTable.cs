namespace PgJsonObjects
{
    public interface IPgXpTable : IJsonKey, IObjectContentGenerator
    {
        string InternalName { get; }
        IPgXpTableLevelCollection XpAmountList { get; }
        XpTableEnum EnumName { get; }
    }
}
