using System.Collections.Generic;
using System.Globalization;

namespace PgJsonObjects
{
    public class JsonGenerator
    {
        public static bool UseJavaFormat = true;
        public static char FieldSeparator = '§';
        public static char ObjectSeparator = '|';

        public JsonGenerator()
        {
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

        private string NewLine;
        private string TabLine;
        private string SpaceLine;

        public string Content { get { return ReconstructedContent; } }

        public void Begin()
        {
            ReconstructedContent = "{" + NewLine;
        }

        public void Next()
        {
            ReconstructedContent += "," + NewLine;
        }

        public void End()
        {
            ReconstructedContent += NewLine + "}";
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

        public void AddString(string Field, string Value)
        {
            if (Value == null)
                return;

            if (InsertedLines > 0)
                Next();

            string StringLine = (Field == null) ? "\"" : "\"" + Field + "\":" + SpaceLine + "\"";

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

            ReconstructedContent += Indentation() + StringLine;
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

        public void AddFloat(string Field, float Value)
        {
            if (Value == 0)
                return;

            if (InsertedLines > 0)
                Next();

            string StringLine = "\"" + Field + "\":" + SpaceLine + Value.ToString(CultureInfo.InvariantCulture.NumberFormat);

            ReconstructedContent += Indentation() + StringLine;
            InsertedLines++;
        }

        public void AddFloat(string Field, float? Value)
        {
            if (Value == null)
                return;

            if (InsertedLines > 0)
                Next();

            string StringLine = "\"" + Field + "\":" + SpaceLine + Value.Value.ToString(CultureInfo.InvariantCulture.NumberFormat);

            ReconstructedContent += Indentation() + StringLine;
            InsertedLines++;
        }

        public void AddDouble(string Field, double Value)
        {
            if (Value == 0)
                return;

            if (InsertedLines > 0)
                Next();

            string StringLine = "\"" + Field + "\":" + SpaceLine + Value.ToString(CultureInfo.InvariantCulture.NumberFormat);

            ReconstructedContent += Indentation() + StringLine;
            InsertedLines++;
        }

        public void AddDouble(string Field, double? Value)
        {
            if (Value == null)
                return;

            if (InsertedLines > 0)
                Next();

            string StringLine = "\"" + Field + "\":" + SpaceLine + Value.Value.ToString(CultureInfo.InvariantCulture.NumberFormat);

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

        public void AddList(string ArrayName, List<string> StringList)
        {
            AddList(ArrayName, StringList, false);
        }

        public void AddList(string ArrayName, List<string> StringList, bool IsListEmpty)
        {
            if (StringList.Count > 0 || IsListEmpty)
            {
                OpenArray(ArrayName);

                foreach (string s in StringList)
                    AddString(null, s);

                CloseArray();
            }

            else if (IsListEmpty)
                AddEmptyArray(ArrayName);
        }

        private int NestingLevel;
        private Stack<string> LastReconstructedContentStack;
        private string ReconstructedContent;
        private Stack<int> LastInsertedLineStack;
        private int InsertedLines;

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
    }
}
