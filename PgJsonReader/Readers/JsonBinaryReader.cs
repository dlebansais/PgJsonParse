namespace PgJsonReader;

using System;
using System.IO;

/// <summary>
/// A reader for Json data.
/// </summary>
public class JsonBinaryReader : IJsonReader
{
    #region Init
    public JsonBinaryReader(BinaryReader reader)
    {
        Reader = reader;
    }

    public JsonBinaryReader(Stream stream)
    {
        Reader = new BinaryReader(stream);
    }

    public JsonBinaryReader(byte[] buffer)
    {
        Reader = new BinaryReader(new MemoryStream(buffer));
    }
    #endregion

    #region Properties
    public BinaryReader Reader { get; }
    public Json.Token CurrentToken { get { return Token; } }
    public object? CurrentValue { get { return Value; } }
    #endregion

    #region Client Interface
    public Json.Token Read()
    {
        if (Reader.BaseStream.Position >= Reader.BaseStream.Length)
            return Token = Json.Token.EndOfFile;

        Token = (Json.Token)Reader.ReadByte();

        if (Token == Json.Token.ObjectKey || Token == Json.Token.String)
            Value = StringHandler.ReadString(Reader.ReadString());
        else if (Token == Json.Token.Float)
            Value = Reader.ReadSingle();
        else if (Token == Json.Token.Integer)
            Value = Reader.ReadInt32();
        else if (Token == Json.Token.Boolean)
            Value = Reader.ReadBoolean();
        else if (Token == Json.Token.Null)
            Value = null;

        return Token;
    }
    #endregion

    #region Implementation
    private readonly IStringHandler StringHandler = new DefaultStringHandler();
    private Json.Token Token;
    private object? Value;
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
    /// Finalizes an instance of the <see cref="JsonBinaryReader"/> class.
    /// </summary>
    ~JsonBinaryReader()
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
