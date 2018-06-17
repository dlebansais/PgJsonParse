namespace PgJsonObjects
{
    public interface IGenericPgObject
    {
        IGenericPgObject CreateItem(byte[] data, int offset);
    }
}
