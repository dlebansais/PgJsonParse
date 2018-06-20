namespace PgJsonObjects
{
    public delegate T PgObjectCreator<T>(byte[] data, ref int offset);
    public delegate IMainPgObject PgObjectCreator(byte[] data, ref int offset);
}
