namespace PgJsonReader
{
    using System;

    public interface IJsonReader : IDisposable
    {
        Json.Token Read();
        Json.Token CurrentToken { get; }
        object? CurrentValue { get; }
    }

    public static class JsonReader
    {
        public static IJsonValue Parse(this IJsonReader reader)
        {
            if (reader.Read() == Json.Token.EndOfFile)
                return new JsonObject();

            else if (reader.CurrentToken == Json.Token.ObjectKey)
            {
                JsonObject Object = new JsonObject();

                string? Key = (string?)reader.CurrentValue;
                if (Key != null)
                {
                    IJsonValue? Value = reader.ParseValue();
                    Object.Add(Key, Value);
                }

                return Object;
            }

            else if (reader.CurrentToken == Json.Token.ArrayStart)
            {
                JsonValueCollection Array = new JsonValueCollection();

                while (reader.CurrentToken != Json.Token.EndOfFile)
                {
                    IJsonValue? Value = reader.ParseValue();
                    if (reader.CurrentToken == Json.Token.ArrayEnd)
                    {
                        if (Array.Count > 0 || !(Value is JsonValueCollection))
                            break;
                        else
                        {
                            if (Array.Count > 0 || !(Value is JsonValueCollection))
                                break;
                        }
                    }

                    if (Value != null)
                        Array.Add(Value);
                }

                return Array;
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
                    string? Key = (string?)reader.CurrentValue;
                    if (Key != null)
                    {
                        IJsonValue? Value = reader.ParseValue();
                        obj.Add(Key, Value);
                    }
                }
                else if (reader.CurrentToken == Json.Token.ObjectEnd)
                    break;
            }
            return obj;
        }

        private static IJsonValue? ParseValue(this IJsonReader reader)
        {
            reader.Read();

            if (reader.CurrentToken == Json.Token.String)
            {
                return new JsonString((string?)reader.CurrentValue);
            }
            else if (reader.CurrentToken == Json.Token.Integer)
            {
                if (reader.CurrentValue != null)
                    return new JsonInteger((int)reader.CurrentValue);
            }
            else if (reader.CurrentToken == Json.Token.Float)
            {
                if (reader.CurrentValue != null)
                    return new JsonFloat((float)reader.CurrentValue);
            }
            else if (reader.CurrentToken == Json.Token.Boolean)
            {
                if (reader.CurrentValue != null)
                    return new JsonBool((bool)reader.CurrentValue);
            }
            else if (reader.CurrentToken == Json.Token.ArrayStart)
            {
                var ArrayValue = new JsonValueCollection();

                while (reader.CurrentToken != Json.Token.EndOfFile)
                {
                    IJsonValue? Value = reader.ParseValue();

                    if (reader.CurrentToken == Json.Token.ArrayEnd)
                    {
                        if (ArrayValue.Count > 0 || !(Value is JsonValueCollection))
                            break;
                        else
                        {
                            if (ArrayValue.Count > 0 || !(Value is JsonValueCollection))
                                break;
                        }
                    }

                    if (Value != null)
                        ArrayValue.Add(Value);
                }
                return ArrayValue;
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
    }
}
