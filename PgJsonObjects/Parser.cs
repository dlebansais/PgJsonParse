﻿using PgJsonReader;
using Presentation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace PgJsonObjects
{
    public interface IParser
    {
        bool VerifyParse { get; set; }
        bool LoadRaw(string FilePath, ICollection ObjectList, bool loadAsArray, bool useJavaFormat, ParseErrorInfo ErrorInfo);
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
        public bool LoadRaw(string FilePath, ICollection GenericObjectList, bool loadAsArray, bool useJavaFormat, ParseErrorInfo ErrorInfo)
        {
            ICollection<T> ObjectList = GenericObjectList as ICollection<T>;
            ObjectList.Clear();

            bool Success = true;

            string Content = FileTools.LoadTextFile(FilePath, FileMode.Open);
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
                                if (loadAsArray && EntryRaw.Value is JsonArray ArrayValue)
                                {
                                    int Index = 0;
                                    foreach (object ObjectValue in ArrayValue)
                                        if (ObjectValue is JsonObject ObjectFields)
                                        {
                                            T NewSingleObject = new T();
                                            NewSingleObject.Init(EntryRaw.Key, Index++, ObjectFields, loadAsArray, ErrorInfo);

                                            ObjectList.Add(NewSingleObject);
                                        }
                                        else
                                        {
                                            ErrorInfo.AddInvalidObjectFormat(EntryRaw.Key);
                                            break;
                                        }
                                }
                                else
                                {
                                    T NewObject = new T();
                                    NewObject.Init(EntryRaw.Key, 0, EntryRaw.Value, loadAsArray, ErrorInfo);

                                    ObjectList.Add(NewObject);
                                }
                            }
                    }
                }
                catch (Exception e)
                {
                    string Message = e.Message;
                    string StackTrace = e.StackTrace;
                    if (StackTrace.Length > 768)
                        StackTrace = StackTrace.Substring(StackTrace.Length - 768);

                    Debug.WriteLine(e.StackTrace);

                    Confirmation.Show("Unable to parse " + Path.GetFileNameWithoutExtension(FilePath) + "\n\n" + Message + "\n\n" + StackTrace, "Error", false, ConfirmationType.Error);

                    Success = false;
                }

                if (VerifyParse)
                {
                    try
                    {
                        using (JsonGenerator Generator = new JsonGenerator(useJavaFormat))
                        {
                            Generator.Begin();

                            if (loadAsArray)
                            {
                                string LastRootKey = null;

                                foreach (T Item in ObjectList)
                                {
                                    string Key = Item.Key;
                                    string[] Splitted = Key.Split('#');
                                    if (Splitted.Length == 2)
                                    {
                                        string RootKey = Splitted[0];

                                        if (RootKey != LastRootKey)
                                        {
                                            if (LastRootKey != null)
                                                Generator.CloseArray();

                                            Generator.OpenArray(RootKey);
                                            LastRootKey = RootKey;
                                        }

                                        Item.OpenGeneratorKey(Generator, false, true);
                                        Item.ListAllObjectContent(Generator);
                                        Item.CloseGeneratorKey(Generator, false, true);
                                    }
                                }

                                if (LastRootKey != null)
                                    Generator.CloseArray();
                            }
                            else
                            {
                                foreach (T Item in ObjectList)
                                    Item.GenerateObjectContent2(Generator, true, false);
                            }

                            Generator.End();

                            //int FirstDiff = CompareContent(SortedContent, Generator.Content);
                            int FirstDiff = CompareContent(Content, Generator.Content);
                            //int FirstDiff = -1;
                            if (FirstDiff >= 0 && FirstDiff < Content.Length)
                            {
                                if (FirstDiff > 150)
                                    FirstDiff -= 150;
                                else
                                    FirstDiff = 0;

                                int Length = 260;
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
                        Success = false;
                    }
                }
            }
            else
            {
                RecordTable = null;
                Success = false;
            }

            return Success;
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

            StringLine = "\t\t\"" + Field + "\": " + InvariantCulture.SingleToString(Value) + ",\n";
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
                if (FileTools.FileExists(IndexFilePath))
                    return;

                StringBuilder Builder = new StringBuilder();

                foreach (KeyValuePair<string, IGenericJsonObject> Entry in ObjectTable)
                {
                    try
                    {
                        string StringValue = Entry.Value.TextContent;
                        if (StringValue.Length > 0)
                        {
                            string Line = StringValue + JsonGenerator.ObjectSeparator + Entry.Key + InvariantCulture.NewLine;
                            Builder.Append(Line);
                        }
                    }
                    catch
                    {
                        Debug.WriteLine("Failed to write index for " + Entry.Value.ToString());
                    }
                }

                string Content = Builder.ToString();
                FileTools.CommitTextFile(IndexFilePath, Content);
            }
            catch
            {

            }
        }
        #endregion

        #region Implementation
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
