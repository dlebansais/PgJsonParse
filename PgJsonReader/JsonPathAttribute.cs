namespace PgJsonReader;

using System;

[AttributeUsage(validOn: AttributeTargets.Field)]
public class JsonPathAttribute : Attribute
{
    public JsonPathAttribute(string serializedName)
    {
        SerializedName = serializedName;
    }

    public string SerializedName { get; }
}
