namespace PgJsonReader;

using System;

public interface IJsonReader : IDisposable
{
    Json.Token Read();
    Json.Token CurrentToken { get; }
    object? CurrentValue { get; }
}
