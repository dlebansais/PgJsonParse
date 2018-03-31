using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgJsonReader
{
    public class JsonTextWriter : IJsonWriter
    {

        public IStringHandler StringHandler = new DefaultStringHandler();

        private VirtualWriter writer;
        private bool format;
        private bool firstItem = true;
        private bool openKey;
        private int tabs;

        public JsonTextWriter(bool format = true)
        {
            writer = new VirtualWriter();
            this.format = format;
        }

        public override string ToString()
        {
            return writer.ToString();
        }

        private void Tabs()
        {
            if (format)
            {
                for (int i = 0; i < tabs; i++)
                    writer.Write('\t');
            }
        }

        private void Space()
        {
            if (format)
                writer.Write(' ');
        }

        private void Newline()
        {
            if (format)
                writer.WriteNewLine();
        }

        private void Comma()
        {
            if (!firstItem)
            {
                writer.Write(',');
                Newline();
            }
        }
        
        public void ObjectStart()
        {
            Comma();
            if (!openKey)
                Tabs();
            writer.Write('{');
            Newline();
            
            tabs++;
            firstItem = true;
            openKey = false;
        }

        public void ObjectKey(string name)
        {
            Comma();
            Tabs();
            writer.Write('"');
            writer.Write(StringHandler.WriteString(name));
            writer.Write('"');
            writer.Write(':');
            Space();
            firstItem = true;
            openKey = true;
        }

        public void ObjectEnd()
        {
            tabs--;
            Newline();
            Tabs();
            writer.Write('}');
            firstItem = false;
        }
        
        public void ArrayStart()
        {
            Comma();
            if (!openKey)
                Tabs();
            writer.Write('[');
            Newline();

            tabs++;
            firstItem = true;
            openKey = false;
        }

        public void ArrayEnd()
        {
            tabs--;
            Newline();
            Tabs();
            writer.Write(']');
            firstItem = false;
        }

        public void Value(string value)
        {
            Comma();
            if (!openKey)
                Tabs();
            if (value == null)
                writer.Write("null");
            else
            {
                writer.Write('"');
                writer.Write(StringHandler.WriteString(value));
                writer.Write('"');
            }
            firstItem = false;
            openKey = false;
        }

        public void Value(int value)
        {
            Comma();
            if (!openKey)
                Tabs();
            writer.Write(value);
            firstItem = false;
            openKey = false;
        }

        public void Value(float value)
        {
            Comma();
            if (!openKey)
                Tabs();
            writer.Write(value);
            firstItem = false;
            openKey = false;
        }

        public void Value(bool value)
        {
            Comma();
            if (!openKey)
                Tabs();
            writer.Write(value ? "true" : "false");
            firstItem = false;
            openKey = false;
        }

        public void Flush(Stream stream)
        {
            writer.Flush(stream);
        }

        public void Flush(StringWriter writer)
        {
            this.writer.Flush(writer);
        }

        public void Dispose()
        {
            writer.Dispose();
        }
    }
}
