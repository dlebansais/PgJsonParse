using PgJsonObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows;

namespace PgJsonParse
{
    public interface IParser
    {
        void LoadRaw(string FilePath, ICollection ObjectList, ParseErrorInfo ErrorInfo);
        void CreateIndex(string IndexFilePath, IDictionary<string, IGenericJsonObject> ObjectTable);
    }

    public class Parser<T> : IParser
        where T : GenericJsonObject<T>, new()
    {
        #region Init
        public Parser()
        {
        }
        #endregion

        #region Properties
        public Dictionary<string, T> RecordTable { get; private set; }
        #endregion

        #region Client Interface
        public void LoadRaw(string FilePath, ICollection GenericObjectList, ParseErrorInfo ErrorInfo)
        {
            ICollection<T> ObjectList = GenericObjectList as ICollection<T>;
            ObjectList.Clear();

            string Content = LoadContent(FilePath);
            if (Content != null)
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();
                if (ser.MaxJsonLength < Content.Length * 2)
                    ser.MaxJsonLength = Content.Length * 2;

                try
                {
                    Dictionary<string, object> RecordTableRaw = ser.Deserialize<Dictionary<string, object>>(Content);

                    foreach (KeyValuePair<string, object> EntryRaw in RecordTableRaw)
                        if (EntryRaw.Key != null && EntryRaw.Key.Length > 0)
                        {
                            T NewObject = new T();
                            NewObject.Init(EntryRaw, ErrorInfo);

                            ObjectList.Add(NewObject);
                        }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Unable to parse " + Path.GetFileNameWithoutExtension(FilePath) + "\n\n" + e.Message, "Error", MessageBoxButton.OK);
                }

                /*

                string SortedContent = ser.Serialize(RecordTable);

                JsonGenerator Generator = new JsonGenerator();

                Generator.Begin();
                foreach (T Item in ObjectList)
                    Item.GenerateObjectContent(Generator);
                Generator.End();

                //int FirstDiff = CompareContent(SortedContent, Generator.Content);
                int FirstDiff = CompareContent(Content, Generator.Content);

                */
            }
            else
                RecordTable = null;
        }

        public static string GenerateObjectHeader(string ObjectName, int NestingLevel)
        {
            string Result = "";
            for (int i = 0; i < NestingLevel + 1; i++)
                Result += "\t";

            Result += "\"" + ObjectName + "\": {\n";

            return Result;
        }

        public static string GenerateObjectFooter(int NestingLevel)
        {
            string Result = "";
            for (int i = 0; i < NestingLevel + 1; i++)
                Result += "\t";

            Result += "},\n";

            return Result;
        }

        public static string GenerateArrayHeader(string ArrayName)
        {
            return "\t\t\"" + ArrayName + "\": [\n";
        }

        public static string GenerateArrayFooter()
        {
            return "\t\t],\n";
        }

        public static bool GenerateIntegerLine(string Field, int? Value, out string StringLine)
        {
            StringLine = null;

            if (Value == null)
                return false;

            StringLine = "\t\t\"" + Field + "\": " + Value.Value + ",\n";
            return true;
        }

        public static bool GenerateIntegerLine(string Field, int Value, out string StringLine)
        {
            StringLine = null;

            if (Value == 0)
                return false;

            StringLine = "\t\t\"" + Field + "\": " + Value + ",\n";
            return true;
        }

        public static bool GenerateFloatLine(string Field, float Value, bool EvenIfZero, out string StringLine)
        {
            StringLine = null;

            if (Value == 0 && !EvenIfZero)
                return false;

            if (Value == 0.05F)
                Value = 0.05F;

            StringLine = "\t\t\"" + Field + "\": " + Value.ToString(CultureInfo.InvariantCulture.NumberFormat) + ",\n";
            return true;
        }

        public static bool GenerateStringLine(string Field, string Value, out string StringLine)
        {
            StringLine = null;

            if (Value == null)
                return false;

            StringLine = "\t\t\"" + Field + "\": \"";

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

            StringLine += "\",\n";

            return true;
        }

        public static bool GenerateBooleanLine(string Field, bool? Value, out string StringLine)
        {
            StringLine = null;

            if (Value == null)
                return false;

            StringLine = "\t\t\"" + Field + "\": " + (Value.Value ? "true" : "false") + ",\n";
            return true;
        }
        #endregion

        #region Index
        public void CreateIndex(string IndexFilePath, IDictionary<string, IGenericJsonObject> ObjectTable)
        {
            try
            {
                if (File.Exists(IndexFilePath))
                    return;

                using (FileStream fs = new FileStream(IndexFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        foreach (KeyValuePair<string, IGenericJsonObject> Entry in ObjectTable)
                        {
                            try
                            {
                                string Content = Entry.Value.TextContent;
                                sw.WriteLine(Content + JsonGenerator.ObjectSeparator + Entry.Key);
                            }
                            catch
                            {
                                Debug.Print("Failed to write index for " + Entry.Value.ToString());
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }
        #endregion

        #region Implementation
        private string LoadContent(string FilePath)
        {
            string Result = null;

            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Result = LoadContent(fs);
            }

            return Result;
        }

        private string LoadContent(Stream SourceStream)
        {
            string Result = null;

            using (StreamReader Reader = new StreamReader(SourceStream))
            {
                Result = Reader.ReadToEnd();
            }

            return Result;
        }

        private int CompareContent(string OriginalContent, string ReconstructedContent)
        {
            int iOriginal = 0;
            int iReconstructed = 0;

            for (; iOriginal < OriginalContent.Length && iReconstructed < ReconstructedContent.Length; iOriginal++, iReconstructed++)
                if (char.ToLower(OriginalContent[iOriginal]) != char.ToLower(ReconstructedContent[iReconstructed]))
                {
                    int Offset = (iOriginal >= 10 ? iOriginal - 10 : 0);
                    string Diff1 = OriginalContent.Substring(iOriginal);
                    string Diff2 = ReconstructedContent.Substring(iReconstructed);
                    break;
                }

            return iOriginal;
        }
        #endregion
    }
}
