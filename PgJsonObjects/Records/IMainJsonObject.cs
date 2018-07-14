namespace PgJsonObjects
{
    public interface IMainJsonObject : IGenericJsonObject
    {
        void SerializeJsonMainObject(byte[] data, ref int offset, int objectOffset);
    }
}
