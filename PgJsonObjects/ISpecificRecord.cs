namespace PgJsonObjects
{
    interface ISpecificRecord
    {
        object ToSpecific(ParseErrorInfo errorInfo);
    }
}
