namespace PgJsonObjects
{
    public abstract class MainPgObject : GenericPgObject, IMainPgObject
    {
        public MainPgObject(byte[] data, int offset)
            : base(data, offset)
        {
        }
    }
}
