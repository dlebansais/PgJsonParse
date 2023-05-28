namespace PgJsonReader;

using System;

[AttributeUsage(validOn: AttributeTargets.Field)]
public class JsonIgnoreAttribute : Attribute { }
