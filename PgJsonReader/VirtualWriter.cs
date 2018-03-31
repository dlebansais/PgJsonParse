using System;
using System.IO;

namespace PgJsonReader
{
    public class VirtualWriter : IDisposable
    {
        public VirtualWriter()
        {
            Content = "";
        }

        public virtual void Dispose()
        {
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public virtual void Write(char value)
        {
            Content += value.ToString();
        }

        public virtual void Write(string value)
        {
            Content += value.ToString();
        }

        public virtual void Write(float value)
        {
            Content += value.ToString();
        }

        public virtual void Write(int value)
        {
            Content += value.ToString();
        }

        public virtual void WriteNewLine()
        {
            Content += "\n";
        }

        public virtual void Flush(StringWriter writer)
        {
            writer.Write(Content);
            writer.Flush();
            Content = "";
        }

        public virtual void Flush(Stream stream)
        {
            using (StreamWriter temp = new StreamWriter(stream))
            {
                temp.Write(Content);
            }

            Content = "";
        }

        private string Content;
    }
}
