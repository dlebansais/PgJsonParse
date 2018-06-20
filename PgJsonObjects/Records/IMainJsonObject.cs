namespace PgJsonObjects
{
    public interface IMainJsonObject
    {
        void SerializeJsonMainObject(byte[] data, ref int offset, int objectOffset);
    }
}
