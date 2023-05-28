namespace PgJsonReader;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

public class JsonTextReader : IJsonReader
{
    #region Init
    public JsonTextReader(Stream stream)
    {
        Reader = new VirtualReader(stream);
    }

    public JsonTextReader(StringReader stringReader)
    {
        Reader = new VirtualReader(stringReader);
    }

    public JsonTextReader(string text)
    {
        Reader = new VirtualReader(text);
    }
    #endregion

    #region Properties
    public Json.Token CurrentToken { get { return Token; } }
    public object? CurrentValue { get { return Value; } }
    #endregion

    #region Client Interface
    public Json.Token Read()
    {
        if (Token == Json.Token.EndOfFile)
            return Token;

        int NextToken;
        while ((NextToken = Reader.Read()) >= 0)
        {
            char CharacterRead = Convert.ToChar(NextToken);

            if (CharacterRead == '{')
            {
                LastIsKey = false;
                Stack.Push(Json.Token.ObjectStart);
                Value = null;
                return Token = Json.Token.ObjectStart;
            }
            else if (CharacterRead == '}')
            {
                Stack.Pop();
                Value = null;
                return Token = Json.Token.ObjectEnd;
            }
            else if (CharacterRead == '"')
            {
                bool IsKey = !LastIsKey && Stack.Count > 0 && Stack.Peek() == Json.Token.ObjectStart;
                LastIsKey = IsKey;

                Value = ReadString();
                return Token = IsKey ? Json.Token.ObjectKey : Json.Token.String;
            }
            else if (CharacterRead == '[')
            {
                LastIsKey = false;
                Stack.Push(Json.Token.ArrayStart);
                Value = null;
                return Token = Json.Token.ArrayStart;
            }
            else if (CharacterRead == ']')
            {
                Stack.Pop();
                Value = null;
                return Token = Json.Token.ArrayEnd;
            }
            else if (CharacterRead != ',' && CharacterRead != ':' && !char.IsWhiteSpace(CharacterRead))
            {
                LastIsKey = false;
                Builder.Clear();
                Builder.Append(CharacterRead);

                while ((NextToken = Reader.Peek()) >= 0)
                {
                    CharacterRead = Convert.ToChar(NextToken);
                    if (CharacterRead != ']' && CharacterRead != '}' && CharacterRead != ',')
                    {
                        Builder.Append(CharacterRead);
                        Reader.Read();
                    }
                    else
                        break;
                }

                var StringRead = Builder.ToString();

                if (int.TryParse(StringRead, out int AsInteger))
                {
                    Value = AsInteger;
                    return Token = Json.Token.Integer;
                }
#if CSHARP_XAML_FOR_HTML5
                else if (double.TryParse(StringRead, out double AsFloating))
#else
                else if (double.TryParse(StringRead, NumberStyles.Float, CultureInfo.InvariantCulture, out double AsFloating))
#endif
                {
                    Value = (float)AsFloating;
                    return Token = Json.Token.Float;
                }
                else if (bool.TryParse(StringRead, out bool AsBoolean))
                {
                    Value = AsBoolean;
                    return Token = Json.Token.Boolean;
                }
                else
                {
                    Value = null;
                    return Token = Json.Token.Null;
                }
            }
        }

        Value = null;
        return Token = Json.Token.EndOfFile;
    }
    #endregion

    #region Implementation
    private string ReadString()
    {
        Builder.Clear();

        int NextCharacter;
        char LastCharacter = char.MinValue;

        while ((NextCharacter = Reader.Read()) >= 0)
        {
            var CharacterRead = Convert.ToChar(NextCharacter);

            if (NextCharacter == '"' && LastCharacter != '\\')
                break;
            else if (LastCharacter == '\\')
                if (CharacterRead == 'n')
                    CharacterRead = '\n';

            LastCharacter = CharacterRead;
            if (CharacterRead != '\\')
                Builder.Append(CharacterRead);
        }

        return StringHandler.ReadString(Builder.ToString());
    }

    private readonly IStringHandler StringHandler = new DefaultStringHandler();
    private readonly VirtualReader Reader;
    private Json.Token Token;
    private object? Value;
    private readonly StringBuilder Builder = new StringBuilder();
    private readonly Stack<Json.Token> Stack = new Stack<Json.Token>();
    private bool LastIsKey = false;
    #endregion

    #region Implementation of IDisposable
    protected virtual void Dispose(bool isDisposing)
    {
        if (!IsDisposed)
        {
            IsDisposed = true;

            if (isDisposing)
                DisposeNow();
        }
    }

    /// <summary>
    /// Called when an object should release its resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="JsonTextReader"/> class.
    /// </summary>
    ~JsonTextReader()
    {
        Dispose(false);
    }

    /// <summary>
    /// True after <see cref="Dispose(bool)"/> has been invoked.
    /// </summary>
    private bool IsDisposed = false;

    /// <summary>
    /// Disposes of every reference that must be cleaned up.
    /// </summary>
    private void DisposeNow()
    {
        Reader.Dispose();
    }
    #endregion
}
