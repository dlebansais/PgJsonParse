namespace PgJsonReader
{
    using System;
    using System.IO;

    public class JsonBinaryWriter : IJsonWriter
    {
        #region Init
        public JsonBinaryWriter(BinaryWriter writer)
        {
            Writer = writer;
        }

        public JsonBinaryWriter(Stream stream)
        {
            Writer = new BinaryWriter(stream);
        }
        #endregion

        #region Client Interface
        public void ObjectStart()
        {
            Writer.Write((byte)Json.Token.ObjectStart);
        }

        public void ObjectKey(string name)
        {
            Writer.Write((byte)Json.Token.ObjectKey);
            Writer.Write(StringHandler.WriteString(name));
        }

        public void ObjectEnd()
        {
            Writer.Write((byte)Json.Token.ObjectEnd);
        }

        public void ArrayStart()
        {
            Writer.Write((byte)Json.Token.ArrayStart);
        }

        public void ArrayEnd()
        {
            Writer.Write((byte)Json.Token.ArrayEnd);
        }

        public void Value(string? value)
        {
            if (value == null)
            {
                Writer.Write((byte)Json.Token.Null);
            }
            else
            {
                Writer.Write((byte)Json.Token.String);
                Writer.Write(StringHandler.WriteString(value));
            }
        }

        public void Value(int value)
        {
            Writer.Write((byte)Json.Token.Integer);
            Writer.Write(value);
        }

        public void Value(float value)
        {
            Writer.Write((byte)Json.Token.Float);
            Writer.Write(value);
        }

        public void Value(bool value)
        {
            Writer.Write((byte)Json.Token.Boolean);
            Writer.Write(value);
        }

        public void Flush(Stream stream)
        {
            Writer.Flush();
        }

        public void Flush(StringWriter writer)
        {
            writer.Flush();
        }
        #endregion

        #region Implementation
        private readonly IStringHandler StringHandler = new DefaultStringHandler();
        private readonly BinaryWriter Writer;
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
        /// Finalizes an instance of the <see cref="JsonBinaryWriter"/> class.
        /// </summary>
        ~JsonBinaryWriter()
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
            Writer.Dispose();
        }
        #endregion
    }
}
