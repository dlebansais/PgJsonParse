namespace PgJsonObjects
{
    public interface ISerializableJsonObject
    {
        void SerializeJsonObject(byte[] data, ref int offset);
    }
}
