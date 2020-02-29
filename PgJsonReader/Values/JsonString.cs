namespace PgJsonReader
{
    public class JsonString : IJsonValue
    {
        public JsonString() { }
        public JsonString(string? text) { String = text; }

        public Json.Type Type { get { return Json.Type.String; } }
        public string? String { get; }
    }
}
