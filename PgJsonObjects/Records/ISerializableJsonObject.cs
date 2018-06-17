namespace PgJsonObjects
{
    public interface ISerializableJsonObject
    {
        void SerializeJsonMainObject(byte[] data, ref int offset);
        void SerializeJsonObject(byte[] data, ref int offset);
    }
}
