namespace PgJsonObjects
{
    public abstract class MainJsonObject<T> : GenericJsonObject<T>, IMainJsonObject
        where T : class
    {
    }
}
