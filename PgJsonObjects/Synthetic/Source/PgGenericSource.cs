namespace PgJsonObjects
{
    public abstract class PgGenericSource<TPg> : GenericPgObject<TPg>, IPgGenericSource
        where TPg : IDeserializablePgObject
    {
        public const int PropertiesOffset = 4;

        public PgGenericSource(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public abstract void Init(IGenericPgObject Parent);
    }
}
