namespace PgJsonReader;

public class JsonBool : IJsonValue
{
    public JsonBool() { }
    public JsonBool(bool value) { Value = value; }

    public Json.Type Type { get { return Json.Type.Boolean; } }
    public bool Value { get; }
}
