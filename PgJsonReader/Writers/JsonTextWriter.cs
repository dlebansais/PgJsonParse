namespace PgJsonReader
{
    using System;
    using System.IO;

    public class JsonTextWriter : IJsonWriter
    {
        #region Init
        public JsonTextWriter(bool format = true)
        {
            Writer = new VirtualWriter();
            Format = format;
        }
        #endregion

        #region Client Interface
        public void ObjectKey(string name)
        {
            Comma();
            Tabs();
            Writer.Write('"');
            Writer.Write(StringHandler.WriteString(name));
            Writer.Write('"');
            Writer.Write(':');
            Space();
            IsFirstItem = true;
            IsOpenKey = true;
        }

        public void ObjectStart()
        {
            Comma();
            if (!IsOpenKey)
                Tabs();
            Writer.Write('{');
            Newline();

            TabCount++;
            IsFirstItem = true;
            IsOpenKey = false;
        }

        public void ObjectEnd()
        {
            TabCount--;
            Newline();
            Tabs();
            Writer.Write('}');
            IsFirstItem = false;
        }

        public void ArrayStart()
        {
            Comma();
            if (!IsOpenKey)
                Tabs();
            Writer.Write('[');
            Newline();

            TabCount++;
            IsFirstItem = true;
            IsOpenKey = false;
        }

        public void ArrayEnd()
        {
            TabCount--;
            Newline();
            Tabs();
            Writer.Write(']');
            IsFirstItem = false;
        }

        public void Value(string? value)
        {
            Comma();
            if (!IsOpenKey)
                Tabs();
            if (value == null)
                Writer.Write("null");
            else
            {
                Writer.Write('"');
                Writer.Write(StringHandler.WriteString(value));
                Writer.Write('"');
            }
            IsFirstItem = false;
            IsOpenKey = false;
        }

        public void Value(int value)
        {
            Comma();
            if (!IsOpenKey)
                Tabs();
            Writer.Write(value);
            IsFirstItem = false;
            IsOpenKey = false;
        }

        public void Value(float value)
        {
            Comma();
            if (!IsOpenKey)
                Tabs();
            Writer.Write(value);
            IsFirstItem = false;
            IsOpenKey = false;
        }

        public void Value(bool value)
        {
            Comma();
            if (!IsOpenKey)
                Tabs();
            Writer.Write(value ? "true" : "false");
            IsFirstItem = false;
            IsOpenKey = false;
        }

        public void Flush(Stream stream)
        {
            Writer.Flush(stream);
        }

        public void Flush(StringWriter writer)
        {
            Writer.Flush(writer);
        }

        public override string ToString()
        {
            return Writer.ToString();
        }
        #endregion

        #region Implementation
        private void Tabs()
        {
            if (Format)
            {
                for (int i = 0; i < TabCount; i++)
                    Writer.Write('\t');
            }
        }

        private void Space()
        {
            if (Format)
                Writer.Write(' ');
        }

        private void Newline()
        {
            if (Format)
                Writer.WriteNewLine();
        }

        private void Comma()
        {
            if (!IsFirstItem)
            {
                Writer.Write(',');
                Newline();
            }
        }

        private readonly IStringHandler StringHandler = new DefaultStringHandler();
        private readonly VirtualWriter Writer;
        private readonly bool Format;
        private bool IsFirstItem = true;
        private bool IsOpenKey;
        private int TabCount;
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
        /// Finalizes an instance of the <see cref="JsonTextWriter"/> class.
        /// </summary>
        ~JsonTextWriter()
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
