namespace PgJsonObjects
{
    public delegate T PgObjectCreator<T>(byte[] data, ref int offset);
}
