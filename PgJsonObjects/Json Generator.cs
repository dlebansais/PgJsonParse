using Presentation;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class JsonGenerator : IDisposable
    {
        #region Constants
        public const char FieldSeparator = '§';
        public const char ObjectSeparator = '|';
        #endregion

        #region Init
        public JsonGenerator(bool useJavaFormat)
        {
            UseJavaFormat = useJavaFormat;

            NestingLevel = 0;
            LastReconstructedContentStack = new Stack<string>();
            ReconstructedContent = "";
            LastInsertedLineStack = new Stack<int>();
            InsertedLines = 0;

            if (UseJavaFormat)
            {
                NewLine = "\n";
                TabLine = "\t";
                SpaceLine = " ";
            }
            else
            {
                NewLine = "";
                TabLine = "";
                SpaceLine = "";
            }
        }
        #endregion

        #region Properties
        public bool UseJavaFormat { get; private set; }
        public string Content { get { return ReconstructedContent; } }
        #endregion

        #region Client Interface
        public void Begin()
        {
            ReconstructedContent = "{" + NewLine;
        }

        public void BeginAsArray()
        {
            ReconstructedContent = "[" + NewLine;
        }

        public void Next()
        {
            ReconstructedContent += "," + NewLine;
        }

        public void End()
        {
            ReconstructedContent += NewLine + "}";
        }

        public void EndAsArray()
        {
            ReconstructedContent += NewLine + "]";
        }

        public void OpenObject(string ObjectName)
        {
            if (InsertedLines > 0)
                Next();

            string Header = (ObjectName == null) ? ("{" + NewLine) : ("\"" + ObjectName + "\":" + SpaceLine + "{" + NewLine);

            ResetInsertedLines();
            ReconstructedContent += Indentation() + Header;
            NestingLevel++;
        }

        public void CloseObject()
        {
            NestingLevel--;

            string Footer = "}";

            if (InsertedLines > 0)
                ReconstructedContent += NewLine;

            ReconstructedContent += Indentation() + Footer;
            RestoreInsertedLines();
            InsertedLines++;
        }

        public void OpenArray(string ArrayName)
        {
            if (InsertedLines > 0)
                Next();

            ReconstructedContent += Indentation() + "\"" + ArrayName + "\":" + SpaceLine + "[" + NewLine;

            ResetInsertedLines();
            NestingLevel++;
        }

        public void OpenNestedArray()
        {
            if (InsertedLines > 0)
                Next();

            ReconstructedContent += Indentation() + "[" + NewLine;

            ResetInsertedLines();
            NestingLevel++;
        }

        public void CloseArray()
        {
            NestingLevel--;

            string Footer = "]";

            ReconstructedContent += NewLine;
            ReconstructedContent += Indentation() + Footer;
            RestoreInsertedLines();
            InsertedLines++;
        }

        public void AddEmptyArray(string Field)
        {
            if (InsertedLines > 0)
                Next();

            string StringLine = "\"" + Field + "\":" + SpaceLine + "[" + SpaceLine + "]";

            ReconstructedContent += Indentation() + StringLine;
            InsertedLines++;
        }

        public void AddNull(string Field)
        {
            if (InsertedLines > 0)
                Next();

            string StringLine = "\"" + Field + "\":" + SpaceLine + "null";

            ReconstructedContent += Indentation() + StringLine;
            InsertedLines++;
        }

        public void AddEnum<T>(string Field, T Value)
        {
            if ((int)(object)Value != 0)
                AddString(Field, StringToEnumConversion<T>.ToString(Value));
        }

        public void AddEnum<T>(string Field, T Value, Dictionary<T, string> StringMap)
        {
            if ((int)(object)Value != 0)
                AddString(Field, StringToEnumConversion<T>.ToString(Value, StringMap));
        }

        public void AddEnum<T>(string Field, T Value, Dictionary<T, string> StringMap, T DefaultValue)
        {
            if ((int)(object)Value != (int)(object)DefaultValue)
                AddString(Field, StringToEnumConversion<T>.ToString(Value, StringMap, DefaultValue));
        }

        public void AddEnum<T>(string Field, T Value, Dictionary<T, string> StringMap, T DefaultValue, T EmptyValue)
        {
            if ((int)(object)Value != (int)(object)DefaultValue)
                if ((int)(object)Value != (int)(object)EmptyValue)
                    AddString(Field, StringToEnumConversion<T>.ToString(Value, StringMap, DefaultValue, EmptyValue));
                else
                    AddString(Field, "");
        }

        public void AddString(string Field, string Value)
        {
            if (Value == null)
                return;

            if (InsertedLines > 0)
                Next();

            string StringLine;

            if (Value == GenericJsonObject.NullString)
                StringLine = "null";

            else
            {
                StringLine = "\"";

                foreach (char c in Value)
                {
                    switch (c)
                    {
                        case '\n':
                            StringLine += "\\n";
                            break;

                        case '"':
                            StringLine += "\\\"";
                            break;

                        default:
                            StringLine += c;
                            break;
                    }
                }

                StringLine += "\"";
            }

            string StringLineBegin = (Field == null) ? "" : ("\"" + Field + "\":" + SpaceLine);

            ReconstructedContent += Indentation() + StringLineBegin + StringLine;
            InsertedLines++;
        }

        public void AddInteger(string Field, int Value)
        {
            if (Value == 0)
                return;

            if (InsertedLines > 0)
                Next();

            string StringLine = "\"" + Field + "\":" + SpaceLine + Value;

            ReconstructedContent += Indentation() + StringLine;
            InsertedLines++;
        }

        public void AddInteger(string Field, int? Value)
        {
            if (Value == null)
                return;

            if (InsertedLines > 0)
                Next();

            string StringLine = "\"" + Field + "\":" + SpaceLine + Value.Value;

            ReconstructedContent += Indentation() + StringLine;
            InsertedLines++;
        }

        public void AddDouble(string Field, double Value)
        {
            if (Value == 0)
                return;

            if (InsertedLines > 0)
                Next();

            string StringLine = "\"" + Field + "\":" + SpaceLine + InvariantCulture.SingleToString((float)Value);

            ReconstructedContent += Indentation() + StringLine;
            InsertedLines++;
        }

        public void AddDouble(string Field, double? Value)
        {
            if (Value == null)
                return;

            if (InsertedLines > 0)
                Next();

            string StringLine = "\"" + Field + "\":" + SpaceLine + InvariantCulture.SingleToString((float)Value.Value);

            ReconstructedContent += Indentation() + StringLine;
            InsertedLines++;
        }

        public void AddBoolean(string Field, bool? Value)
        {
            if (Value == null)
                return;

            if (InsertedLines > 0)
                Next();

            string StringLine = "\"" + Field + "\":" + SpaceLine + (Value.Value ? "true" : "false");

            ReconstructedContent += Indentation() + StringLine;
            InsertedLines++;
        }

        public void AddStringList(string ArrayName, List<string> StringList)
        {
            AddStringList(ArrayName, StringList, false);
        }

        public void AddStringList(string ArrayName, List<string> StringList, bool IsListEmpty)
        {
            if (StringList.Count > 0)
            {
                OpenArray(ArrayName);

                foreach (string s in StringList)
                    AddString(null, s);

                CloseArray();
            }

            else if (IsListEmpty)
                AddEmptyArray(ArrayName);
        }

        public void AddIntegerList(string ArrayName, List<int> IntegerList, bool IsListEmpty)
        {
            if (IntegerList.Count > 0)
            {
                OpenArray(ArrayName);

                foreach (int n in IntegerList)
                {
                    if (InsertedLines > 0)
                        Next();

                    string StringLine = n.ToString();

                    ReconstructedContent += Indentation() + StringLine;
                    InsertedLines++;
                }

                CloseArray();
            }

            else if (IsListEmpty)
                AddEmptyArray(ArrayName);
        }
        #endregion

        #region Implementation
        private string Indentation()
        {
            string Result = "";
            for (int i = 0; i < NestingLevel + 1; i++)
                Result += TabLine;

            return Result;
        }

        private void ResetInsertedLines()
        {
            LastInsertedLineStack.Push(InsertedLines);
            LastReconstructedContentStack.Push(ReconstructedContent);

            InsertedLines = 0;
            ReconstructedContent = "";
        }

        private void RestoreInsertedLines()
        {
            InsertedLines = LastInsertedLineStack.Pop();

            string Current = ReconstructedContent;
            ReconstructedContent = LastReconstructedContentStack.Pop() + Current;
        }

        private readonly string NewLine;
        private readonly string TabLine;
        private readonly string SpaceLine;
        private int NestingLevel;
        private readonly Stack<string> LastReconstructedContentStack;
        private string ReconstructedContent;
        private readonly Stack<int> LastInsertedLineStack;
        private int InsertedLines;
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
        /// Finalizes an instance of the <see cref="JsonGenerator"/> class.
        /// </summary>
        ~JsonGenerator()
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
        }
        #endregion
    }
}
