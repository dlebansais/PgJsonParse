﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgJsonReader
{
    public class JsonTextReader : IJsonReader
    {

        public IStringHandler StringHandler = new DefaultStringHandler();

        private VirtualReader reader;
        private Json.Token token;
        private object value;

        private StringBuilder builder = new StringBuilder();
        private Stack<Json.Token> stack = new Stack<Json.Token>();
        private bool lastIsKey = false;

        public JsonTextReader(Stream stream)
        {
            reader = new VirtualReader(stream);
        }

        public JsonTextReader(StringReader reader)
        {
            this.reader = new VirtualReader(reader);
        }

        public JsonTextReader(string text)
        {
            reader = new VirtualReader(text);
        }

        public Json.Token CurrentToken { get { return token; } }

        public object CurrentValue { get { return value; } }

        public Json.Token Read()
        {
            if (token == Json.Token.EndOfFile)
                return token;

            int next;
            while ((next = reader.Read()) >= 0)
            {
                char ch = Convert.ToChar(next);
                if (ch == '{')
                {
                    lastIsKey = false;
                    stack.Push(Json.Token.ObjectStart);
                    value = null;
                    return token = Json.Token.ObjectStart;
                }
                else if (ch == '}')
                {
                    stack.Pop();
                    value = null;
                    return token = Json.Token.ObjectEnd;
                }
                else if (ch == '"')
                {
                    var isKey = !lastIsKey && stack.Count > 0 && stack.Peek() == Json.Token.ObjectStart;
                    lastIsKey = isKey;

                    value = ReadString();
                    return token = isKey ? Json.Token.ObjectKey : Json.Token.String;
                }
                else if (ch == '[')
                {
                    lastIsKey = false;
                    stack.Push(Json.Token.ArrayStart);
                    value = null;
                    return token = Json.Token.ArrayStart;
                }
                else if (ch == ']')
                {
                    stack.Pop();
                    value = null;
                    return token = Json.Token.ArrayEnd;
                }
                else if (ch != ',' && ch != ':' && !Char.IsWhiteSpace(ch))
                {
                    lastIsKey = false;
                    builder.Clear();
                    builder.Append(ch);

                    while ((next = reader.Peek()) >= 0)
                    {
                        ch = Convert.ToChar(next);
                        if (ch != ']' && ch != '}' && ch != ',')
                        {
                            builder.Append(ch);
                            reader.Read();
                        }
                        else
                            break;
                    }
                    
                    var str = builder.ToString();
                    if (int.TryParse(str, out int integer))
                    {
                        value = integer;
                        return token = Json.Token.Integer;
                    }
#if CSHARP_XAML_FOR_HTML5
                    else if (double.TryParse(str, out double single))
#else
                    else if (double.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out double single))
#endif
                    {
                        value = (float)single;
                        return token = Json.Token.Float;
                    }
                    else if (bool.TryParse(str, out bool truefalse))
                    {
                        value = truefalse;
                        return token = Json.Token.Boolean;
                    }
                    else
                    {
                        value = null;
                        return token = Json.Token.Null;
                    }
                }
            }

            value = null;
            return token = Json.Token.EndOfFile;
        }

        private string ReadString()
        {
            builder.Clear();

            int next;
            char last = char.MinValue;
            while ((next = reader.Read()) >= 0)
            {
                var ch = Convert.ToChar(next);

                if (next == '"' && last != '\\')
                    break;
                else if (last == '\\')
                    if (ch == 'n')
                        ch = '\n';

                last = ch;
                if (ch != '\\')
                    builder.Append(ch);
            }

            return StringHandler.ReadString(builder.ToString());
        }

        public void Dispose()
        {
            reader.Dispose();
        }
    }
}
