using PgJsonReader;
using Presentation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows;

namespace PgJsonObjects
{
    public interface IParser
    {
        bool VerifyParse { get; set; }
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
        public bool VerifyParse { get; set; }
        #endregion

        #region Client Interface
        public void LoadRaw(string FilePath, ICollection GenericObjectList, ParseErrorInfo ErrorInfo)
        {
            ICollection<T> ObjectList = GenericObjectList as ICollection<T>;
            ObjectList.Clear();

            string Content = LoadContent(FilePath);
            if (Content != null)
            {
                try
                {
                    using (IJsonReader reader = new JsonTextReader(Content))
                    {
                        JsonObject RootObject = reader.Parse();
                        foreach (KeyValuePair<string, IJsonValue> EntryRaw in RootObject.Entries)
                            if (EntryRaw.Key != null && EntryRaw.Key.Length > 0)
                            {
                                T NewObject = new T();
                                NewObject.Init(EntryRaw, ErrorInfo);

                                ObjectList.Add(NewObject);
                            }
                    }
                }
                catch (Exception e)
                {
                    Confirmation.Show("Unable to parse " + Path.GetFileNameWithoutExtension(FilePath) + "\n\n" + e.Message, "Error", false, ConfirmationType.Error);
                }

                if (VerifyParse)
                {
                    try
                    {
                        using (JsonGenerator Generator = new JsonGenerator())
                        {
                            Generator.Begin();
                            foreach (T Item in ObjectList)
                                Item.GenerateObjectContent(Generator);
                            Generator.End();

                            //int FirstDiff = CompareContent(SortedContent, Generator.Content);
                            int FirstDiff = CompareContent(Content, Generator.Content);
                            if (FirstDiff >= 0 && FirstDiff < Content.Length)
                            {
                                if (FirstDiff > 150)
                                    FirstDiff -= 150;
                                else
                                    FirstDiff = 0;

                                int Length = 200;
                                if (Length > (Content.Length - FirstDiff))
                                    Length = (Content.Length - FirstDiff);
                                if (Length > (Generator.Content.Length - FirstDiff))
                                    Length = (Generator.Content.Length - FirstDiff);
                                if (Length > 0)
                                {
                                    Debug.WriteLine("** " + Path.GetFileName(FilePath));
                                    Debug.WriteLine(Content.Substring(FirstDiff, Length));
                                    Debug.WriteLine("**");
                                    Debug.WriteLine(Generator.Content.Substring(FirstDiff, Length));
                                    Debug.WriteLine("**");
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Confirmation.Show("Unable to verify " + Path.GetFileNameWithoutExtension(FilePath) + "\n\n" + e.Message, "Error", false, ConfirmationType.Error);
                    }
                }
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
                                if (Content.Length > 0)
                                {
                                    string Line = Content + JsonGenerator.ObjectSeparator + Entry.Key + Tools.NewLine;
                                    sw.Write(Line);
                                }
                            }
                            catch
                            {
                                Debug.WriteLine("Failed to write index for " + Entry.Value.ToString());
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
                if (OriginalContent[iOriginal].ToString().ToLower() != ReconstructedContent[iReconstructed].ToString().ToLower())
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
