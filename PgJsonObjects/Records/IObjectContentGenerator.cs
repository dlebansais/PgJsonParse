namespace PgJsonObjects
{
    public interface IObjectContentGenerator
    {
        void GenerateObjectContent(JsonGenerator Generator, bool openWithKey, bool openWithNullKey);
        void OpenGeneratorKey(JsonGenerator Generator, bool openWithKey, bool openWithNullKey);
        void ListAllObjectContent(JsonGenerator Generator);
        void ListObjectContent(JsonGenerator Generator, string ParserKey);
        void CloseGeneratorKey(JsonGenerator Generator, bool openWithKey, bool openWithNullKey);
    }
}
