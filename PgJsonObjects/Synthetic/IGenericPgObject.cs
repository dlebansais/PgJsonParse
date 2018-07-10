namespace PgJsonObjects
{
    public interface IGenericPgObject
    {
        string Key { get; }
        void Init();
    }
}
