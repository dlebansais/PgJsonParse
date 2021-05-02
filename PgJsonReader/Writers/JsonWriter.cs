namespace PgJsonReader
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public interface IJsonWriter : IDisposable
    {
        void ObjectStart();
        void ObjectKey(string name);
        void ObjectEnd();
        void ArrayStart();
        void ArrayEnd();
        void Value(int value);
        void Value(string? value);
        void Value(float value);
        void Value(bool value);
        void Flush(Stream stream);
        void Flush(StringWriter writer);
    }

    public static class JsonWriter
    {
        public static void Object(this IJsonWriter writer, JsonObject obj)
        {
            writer.ObjectStart();

            foreach (KeyValuePair<string, IJsonValue?> Entry in obj.Entries)
            {
                writer.ObjectKey(Entry.Key);
                writer.Value(Entry.Value);
            }

            writer.ObjectEnd();
        }

        private static void Value(this IJsonWriter writer, IJsonValue? value)
        {
            if (value == null)
            {
                writer.Value(null);
            }
            else if (value.Type == Json.Type.Object)
            {
                writer.Object((JsonObject)value);
            }
            else if (value.Type == Json.Type.Array)
            {
                writer.ArrayStart();

                JsonValueCollection ArrayValue = (JsonValueCollection)value;
                foreach (IJsonValue ChildValue in ArrayValue)
                    writer.Value(ChildValue);

                writer.ArrayEnd();
            }
            else if (value.Type == Json.Type.String)
            {
                writer.Value(((JsonString)value).String);
            }
            else if (value.Type == Json.Type.Integer)
            {
                writer.Value(((JsonInteger)value).Number);
            }
            else if (value.Type == Json.Type.Float)
            {
                writer.Value(((JsonFloat)value).Number);
            }
            else if (value.Type == Json.Type.Boolean)
            {
                writer.Value(((JsonBool)value).Value);
            }
        }
    }
}
