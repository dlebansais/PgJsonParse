namespace PgJsonObjects
{
    public interface IDeserializablePgObject
    {
        IDeserializablePgObject Create(byte[] data, int offset);
    }
}
