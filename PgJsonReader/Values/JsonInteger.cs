namespace PgJsonReader;

public class JsonInteger : IJsonValue
{
    public JsonInteger() { }
    public JsonInteger(int number) { Number = number; }

    public Json.Type Type { get { return Json.Type.Integer; } }
    public int Number { get; }
}
