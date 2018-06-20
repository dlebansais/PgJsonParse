using System;

namespace PgJsonReader
{

    public interface IJsonReader : IDisposable
    {
        Json.Token Read();
        Json.Token CurrentToken { get; }
        object CurrentValue { get; }
    }

    public static class JsonReader
    {
        public static IJsonValue Parse(this IJsonReader reader)
        {
            if (reader.Read() == Json.Token.EndOfFile)
                return new JsonObject();

            else if (reader.CurrentToken == Json.Token.ObjectKey)
            {
                JsonObject obj = new JsonObject();

                string key = (string)reader.CurrentValue;
                var value = reader.ParseValue();
                obj.Add(key, value);

                return obj;
            }

            else if (reader.CurrentToken == Json.Token.ArrayStart)
            {
                JsonArray array = new JsonArray();

                while (reader.CurrentToken != Json.Token.EndOfFile)
                {
                    var value = reader.ParseValue();
                    if (reader.CurrentToken == Json.Token.ArrayEnd)
                    {
                        if (array.Count > 0 || !(value is JsonArray))
                            break;
                        else
                        {
                            if (array.Count > 0 || !(value is JsonArray))
                                break;
                        }
                    }

                    array.Add(value);
                }

                return array;
            }

            else
                return reader.ParseObject();
        }

        private static JsonObject ParseObject(this IJsonReader reader)
        {
            var obj = new JsonObject();
            while (reader.Read() != Json.Token.EndOfFile)
            {
                if (reader.CurrentToken == Json.Token.ObjectStart)
                    continue;
                if (reader.CurrentToken == Json.Token.ObjectKey)
                {
                    string key = (string)reader.CurrentValue;
                    var value = reader.ParseValue();
                    obj.Add(key, value);
                }
                else if (reader.CurrentToken == Json.Token.ObjectEnd)
                    break;
            }
            return obj;
        }

        private static IJsonValue ParseValue(this IJsonReader reader)
        {
            reader.Read();

            if (reader.CurrentToken == Json.Token.String)
            {
                return new JsonString((string)reader.CurrentValue);
            }
            else if (reader.CurrentToken == Json.Token.Integer)
            {
                return new JsonInteger((int)reader.CurrentValue);
            }
            else if (reader.CurrentToken == Json.Token.Float)
            {
                return new JsonFloat((float)reader.CurrentValue);
            }
            else if (reader.CurrentToken == Json.Token.Boolean)
            {
                return new JsonBool((bool)reader.CurrentValue);
            }
            else if (reader.CurrentToken == Json.Token.ArrayStart)
            {
                var array = new JsonArray();
                while (reader.CurrentToken != Json.Token.EndOfFile)
                {
                    var value = reader.ParseValue();
                    if (reader.CurrentToken == Json.Token.ArrayEnd)
                    {
                        if (array.Count > 0 || !(value is JsonArray))
                            break;
                        else
                        {
                            if (array.Count > 0 || !(value is JsonArray))
                                break;
                        }
                    }

                    array.Add(value);
                }
                return array;
            }
            else if (reader.CurrentToken == Json.Token.ObjectStart)
            {
                return reader.ParseObject();
            }

            else if (reader.CurrentToken == Json.Token.Null)
            {
                return new JsonString(null);
            }

            return null;
        }

        public static T Deserialize<T>(this IJsonReader reader)
        {
            var instance = Activator.CreateInstance<T>();
            return instance;
        }
    }
}
