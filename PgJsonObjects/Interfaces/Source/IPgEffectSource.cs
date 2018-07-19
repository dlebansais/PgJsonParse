namespace PgJsonObjects
{
    public interface IPgEffectSource : IPgGenericSource
    {
        IPgEffect Effect { get; }
    }
}
