namespace PgJsonObjects
{
    public abstract class MainPgObject<T> : GenericPgObject<T>, IMainPgObject
        where T : IDeserializablePgObject
    {
        public MainPgObject(byte[] data, int offset)
            : base(data, offset)
        {
        }
    }
}
