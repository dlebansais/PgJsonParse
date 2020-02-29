namespace PgJsonReader
{
    public class JsonFloat : IJsonValue
    {
        public JsonFloat() { }
        public JsonFloat(float number) { Number = number; }

        public Json.Type Type { get { return Json.Type.Float; } }
        public float Number { get; }
    }
}
