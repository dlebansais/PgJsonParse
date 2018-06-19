namespace PgJsonObjects
{
    public interface IDeserializablePgObject
    {
        IDeserializablePgObject Create(byte[] data, ref int offset);
    }
}
