namespace PgJsonReader
{
    using System.Collections.Generic;

    public class JsonValueCollection : List<IJsonValue>, IJsonValue
    {
        public Json.Type Type { get { return Json.Type.Array; } }

        public JsonString Add(string? value)
        {
            JsonString StringValue = new JsonString(value);
            Add(StringValue);

            return StringValue;
        }

        public JsonFloat Add(float value)
        {
            JsonFloat FloatValue = new JsonFloat(value);
            Add(FloatValue);

            return FloatValue;
        }

        public JsonObject AddObject()
        {
            JsonObject ObjectValue = new JsonObject();
            Add(ObjectValue);

            return ObjectValue;
        }

        public JsonValueCollection AddArray()
        {
            JsonValueCollection ArrayValue = new JsonValueCollection();
            Add(ArrayValue);

            return ArrayValue;
        }
    }
}
