namespace PgJsonReader
{
    using System;
    using System.Globalization;
    using System.IO;

    public class VirtualWriter : IDisposable
    {
        #region Init
        public VirtualWriter()
        {
            Content = "";
        }
        #endregion

        #region Client Interface
        public virtual void Write(char value)
        {
            Content += value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual void Write(string value)
        {
            Content += value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual void Write(float value)
        {
            Content += value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual void Write(int value)
        {
            Content += value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual void WriteNewLine()
        {
            Content += "\n";
        }

        public virtual void Flush(StringWriter writer)
        {
            writer.Write(Content);
            writer.Flush();
            Content = string.Empty;
        }

        public virtual void Flush(Stream stream)
        {
            using (StreamWriter Writer = new StreamWriter(stream))
            {
                Writer.Write(Content);
            }

            Content = string.Empty;
        }

        private string Content;
        #endregion

        #region Implementation of IDisposable
        /// <summary>
        /// Called when an object should release its resources.
        /// </summary>
        /// <param name="isDisposing">Indicates if resources must be disposed now.</param>
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
        /// Finalizes an instance of the <see cref="VirtualWriter"/> class.
        /// </summary>
        ~VirtualWriter()
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
        protected virtual void DisposeNow()
        {
        }
        #endregion
    }
}
