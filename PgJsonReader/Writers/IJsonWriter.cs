namespace PgJsonReader;

using System;
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
