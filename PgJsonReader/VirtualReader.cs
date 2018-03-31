using System;
using System.IO;

namespace PgJsonReader
{
    public class VirtualReader : IDisposable
    {
        public VirtualReader(string text)
            : this()
        {
            Content = text;
        }

        public VirtualReader(Stream stream)
            : this()
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                Content = sr.ReadToEnd();
            }
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

        public virtual void Dispose()
        {
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        private string Content;
        private int Index;
    }
}
