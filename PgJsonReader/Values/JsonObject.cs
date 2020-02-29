namespace PgJsonReader
{
    using System.Collections;
    using System.Collections.Generic;

#pragma warning disable CA1710 // Identifiers should have correct suffix
    public class JsonObject : IJsonValue, IEnumerable<KeyValuePair<string, IJsonValue>>
#pragma warning restore CA1710 // Identifiers should have correct suffix
    {
        public Json.Type Type { get { return Json.Type.Object; } }
        public Dictionary<string, IJsonValue?> Entries { get; } = new Dictionary<string, IJsonValue?>();

        public int Count
        {
            get { return Entries.Count; }
        }

        public bool Has(string name)
        {
            return Entries.ContainsKey(name);
        }

        public IJsonValue? Get(string name)
        {
            if (Entries.TryGetValue(name, out IJsonValue? Value))
                return Value;

            return null;
        }

        public T Get<T>(string name) where T : IJsonValue
        {
#pragma warning disable CS8601 // Possible null reference assignment
            return (T)Get(name);
#pragma warning restore CS8601 // Possible null reference assignment
        }

        public IJsonValue? this[string name]
        {
            get { return Get(name); }
        }

        public bool Bool(string name, bool defaultValue = false)
        {
            if (Entries.TryGetValue(name, out IJsonValue? Value) && Value is JsonBool BooleanValue)
                return BooleanValue.Value;

            return defaultValue;
        }

        public string? String(string name, string? defaultValue = null)
        {
            if (Entries.TryGetValue(name, out IJsonValue? Value) && Value is JsonString StringValue)
                return StringValue.String;

            return defaultValue;
        }

        public float Number(string name, float defaultValue = 0f)
        {
            if (Entries.TryGetValue(name, out IJsonValue? Value) && Value is JsonFloat FloatValue)
                return FloatValue.Number;

            return defaultValue;
        }

        public JsonValueCollection? Array(string name)
        {
            if (Entries.TryGetValue(name, out IJsonValue? Value) && Value is JsonValueCollection ArrayValue)
                return ArrayValue;

            return null;
        }

        public JsonObject? Object(string name)
        {
            if (Entries.TryGetValue(name, out IJsonValue? Value) && Value is JsonObject ObjectValue)
                return ObjectValue;

            return null;
        }

        public IJsonValue? Add(string key, IJsonValue? value)
        {
            Entries.Add(key, value);
            return value;
        }

        public JsonString Add(string key, string value)
        {
            var str = new JsonString(value);
            Entries.Add(key, str);
            return str;
        }

        public JsonFloat Add(string key, float value)
        {
            var num = new JsonFloat(value);
            Entries.Add(key, num);
            return num;
        }

        public JsonObject AddObject(string key)
        {
            var obj = new JsonObject();
            Entries.Add(key, obj);
            return obj;
        }

        public JsonValueCollection AddArray(string key)
        {
            var arr = new JsonValueCollection();
            Entries.Add(key, arr);
            return arr;
        }

        public bool Remove(string key)
        {
            return Entries.Remove(key);
        }

        // enumerate
        public IEnumerator<KeyValuePair<string, IJsonValue>> GetEnumerator() { return Entries.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
    }
}
