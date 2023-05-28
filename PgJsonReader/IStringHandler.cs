namespace PgJsonReader;

using System;

public interface IStringHandler
{
    string ReadString(string input);
    string WriteString(string input);
}
