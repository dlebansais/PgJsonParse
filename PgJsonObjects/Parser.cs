using PgJsonReader;
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
        bool LoadRaw(string FilePath, ICollection ObjectList, bool loadAsArray, bool loadAsObject, ParseErrorInfo ErrorInfo);
        void CreateIndex(string IndexFilePath, IDictionary<string, IGenericJsonObject> ObjectTable);
    }

    public class Parser<T, TI> : IParser
        where T : IJsonParsableObject, IJsonKey, IObjectContentGenerator, TI, new()
        //where TI: IJsonKey, IObjectContentGenerator
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
        public bool LoadRaw(string FilePath, ICollection GenericObjectList, bool loadAsArray, bool loadAsObject, ParseErrorInfo ErrorInfo)
        {
            ICollection<TI> ObjectList = GenericObjectList as ICollection<TI>;
            ObjectList.Clear();

            if (typeof(TI) == typeof(IPgAttribute))
            {
                IPgAttribute CockatriceDebuffCostDelta = new Attribute("COCKATRICEDEBUFF_COST_DELTA");
                ObjectList.Add((TI)CockatriceDebuffCostDelta);
                IPgAttribute LamiaDebuffCostDelta = new Attribute("LAMIADEBUFF_COST_DELTA");
                ObjectList.Add((TI)LamiaDebuffCostDelta);
            }

            bool Success = true;

            string Content = FileTools.LoadTextFile(FilePath, FileMode.Open);
            if (Content != null)
            {
                try
                {
                    using (IJsonReader reader = new JsonTextReader(Content))
                    {
                        IJsonValue RootValue = reader.Parse();

                        Dictionary<string, IJsonValue> Entries = new Dictionary<string, IJsonValue>();

                        if (RootValue is JsonObject RootObject)
                            Entries = RootObject.Entries;

                        else if (RootValue is JsonArray RootArray)
                        {
                            foreach (IJsonValue Item in RootArray)
                            {
                                string Id = null;

                                if (Item is JsonObject ItemObject)
                                {
                                    if (ItemObject.Entries.ContainsKey("Id"))
                                    {
                                        if (ItemObject.Entries["Id"] is JsonString IdString)
                                            Id = IdString.String;

                                        else if (ItemObject.Entries["Id"] is JsonInteger IdInteger)
                                            Id = IdInteger.Number.ToString();
                                    }
                                }

                                if (Id != null && !Entries.ContainsKey(Id))
                                    Entries.Add(Id, Item);
                            }
                        }

                        foreach (KeyValuePair<string, IJsonValue> EntryRaw in Entries)
                            if (EntryRaw.Key != null && EntryRaw.Key.Length > 0)
                            {
                                if ((loadAsArray || loadAsObject) && EntryRaw.Value is JsonArray ArrayValue)
                                {
                                    int Index = 0;
                                    foreach (object ObjectValue in ArrayValue)
                                        if (ObjectValue is JsonObject ObjectFields)
                                        {
                                            T NewSingleObject = new T();
                                            NewSingleObject.Init(EntryRaw.Key, Index++, ObjectFields, (loadAsArray || loadAsObject), ErrorInfo);

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
                                    NewObject.Init(EntryRaw.Key, 0, EntryRaw.Value, (loadAsArray || loadAsObject), ErrorInfo);

                                    ObjectList.Add(NewObject);
                                }
                            }


                        foreach (IGenericJsonObject Item in ObjectList)
                            Item.CheckUnparsedFields(ErrorInfo);
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
            }
            else
            {
                RecordTable = null;
                Success = false;
            }

            return Success;
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
