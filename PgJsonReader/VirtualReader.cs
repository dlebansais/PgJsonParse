namespace PgJsonReader
{
    using System;
    using System.IO;

    public class VirtualReader : IDisposable
    {
        #region Init
        public VirtualReader(string text)
            : this()
        {
            Content = text;
        }

        public VirtualReader(Stream stream)
            : this()
        {
            using StreamReader Reader = new StreamReader(stream);
            Content = Reader.ReadToEnd();
        }

        public VirtualReader(StringReader reader)
            : this()
        {
            Content = reader.ReadToEnd();
        }

        protected VirtualReader()
        {
            Content = "";
            Index = 0;
        }
        #endregion

        #region Client Interface
        public virtual int Peek()
        {
            if (Index < Content.Length)
                return Content[Index];
            else
                return -1;
        }

        public virtual int Read()
        {
            if (Index < Content.Length)
                return Content[Index++];
            else
                return -1;
        }

        private readonly string Content;
        private int Index;
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
        /// Finalizes an instance of the <see cref="VirtualReader"/> class.
        /// </summary>
        ~VirtualReader()
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
