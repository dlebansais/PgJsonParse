using PgJsonReader;

namespace PgJsonObjects
{
    public interface IJsonParsableObject
    {
        void Init(string key, int index, IJsonValue value, bool loadAsArray, ParseErrorInfo ErrorInfo);
        void CheckUnparsedFields(ParseErrorInfo errorInfo);
    }
}
