﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class GenericPgObject
    {
        public const int NoValueInt = 0x6B6B6B6B;
        public static Dictionary<int, object> CreatedObjectTable = new Dictionary<int, object>();

        public static IMainPgObject CreateMainObject(PgObjectCreator createNewObject, byte[] data, ref int offset)
        {
            int TableOffset = offset;
            IMainPgObject Item = createNewObject(data, ref offset);
            Item.Init();

            if (!CreatedObjectTable.ContainsKey(TableOffset))
                CreatedObjectTable.Add(TableOffset, Item);

            return Item;
        }
    }

    public abstract class GenericPgObject<TPg> : GenericPgObject, IGenericPgObject, IDeserializablePgObject, IObjectContentGenerator
        where TPg : IDeserializablePgObject
    {
        public GenericPgObject(byte[] data, int offset)
        {
            Data = data;
            Offset = offset;
        }

        public abstract string Key { get; }

        public IDeserializablePgObject Create(byte[] data, ref int offset)
        {
            return CreateItem(data, ref offset);
        }

        protected abstract TPg CreateItem(byte[] data, ref int offset);

        public virtual void Init()
        {
        }

        protected byte[] Data { get; private set; }
        protected int Offset { get; private set; }

        protected bool? GetBool(int valueOffset, int valueBit)
        {
            UInt16 Value = BitConverter.ToUInt16(Data, Offset + valueOffset);

            if (((Value >> valueBit) & 0x1) != 0)
                return ((Value >> (valueBit + 1)) & 0x1) != 0;
            else
                return null;
        }

        protected T GetEnum<T>(int valueOffset)
        {
            return (T)(object)(int)BitConverter.ToUInt16(Data, Offset + valueOffset);
        }

        protected int? GetInt(int valueOffset)
        {
            int Value = BitConverter.ToInt32(Data, Offset + valueOffset);

            if (Value == NoValueInt)
                return null;
            else
                return Value;
        }

        protected uint? GetUInt(int valueOffset)
        {
            uint Value = BitConverter.ToUInt32(Data, Offset + valueOffset);

            if (Value == NoValueInt)
                return null;
            else
                return Value;
        }

        protected TimeSpan? GetTimeSpan(int valueOffset)
        {
            int Value = BitConverter.ToInt32(Data, Offset + valueOffset);

            if (Value == NoValueInt)
                return null;
            else
                return TimeSpan.FromSeconds(Value);
        }

        protected double? GetDouble(int valueOffset)
        {
            float Value = BitConverter.ToSingle(Data, Offset + valueOffset);

            if (float.IsNaN(Value))
                return null;
            else
                return Value;
        }

        protected string GetString(int redirectionOffset)
        {
            int StoredOffset = /*Offset +*/ BitConverter.ToInt32(Data, Offset + redirectionOffset);
            if (StoredOffset != 0)
            {
                string Result = CreateString(StoredOffset);
                return Result;
            }
            else
                return null;
        }

        protected T GetObject<T>(int redirectionOffset, ref T cachedValue, PgObjectCreator<T> createNewObject)
        {
            if (cachedValue == null)
            {
                int StoredOffset = /*Offset +*/ BitConverter.ToInt32(Data, Offset + redirectionOffset);
                if (StoredOffset != 0)
                {
                    if (CreatedObjectTable.ContainsKey(StoredOffset))
                        cachedValue = (T)CreatedObjectTable[StoredOffset];
                    else
                    {
                        int TableOffset = StoredOffset;
                        cachedValue = createNewObject(Data, ref StoredOffset);
                        CreatedObjectTable.Add(TableOffset, cachedValue);
                        (cachedValue as IGenericPgObject).Init();
                    }
                }
                else
                    cachedValue = default(T);
            }

            return cachedValue;
        }

        protected List<bool> GetBoolList(int redirectionOffset, ref List<bool> cachedList)
        {
            if (cachedList == null)
            {
                int LengthOffset = /*Offset +*/ BitConverter.ToInt32(Data, Offset + redirectionOffset);
                int Count = BitConverter.ToUInt16(Data, LengthOffset);
                int ValueOffset = LengthOffset + 2;

                cachedList = new List<bool>();
                for (int i = 0; i < Count; i++)
                {
                    bool StoredValue = (BitConverter.ToUInt16(Data, ValueOffset + i * 2) != 0);
                    cachedList.Add(StoredValue);
                }
            }

            return cachedList;
        }

        protected List<T> GetEnumList<T>(int redirectionOffset, ref List<T> cachedList)
        {
            if (cachedList == null)
            {
                int LengthOffset = /*Offset +*/ BitConverter.ToInt32(Data, Offset + redirectionOffset);
                int Count = BitConverter.ToUInt16(Data, LengthOffset);
                int ValueOffset = LengthOffset + 2;

                cachedList = new List<T>();
                for (int i = 0; i < Count; i++)
                {
                    T StoredValue = (T)(object)(int)BitConverter.ToUInt16(Data, ValueOffset + i * 2);
                    cachedList.Add(StoredValue);
                }
            }

            return cachedList;
        }

        protected List<int> GetIntList(int redirectionOffset, ref List<int> cachedList)
        {
            if (cachedList == null)
            {
                int LengthOffset = /*Offset +*/ BitConverter.ToInt32(Data, Offset + redirectionOffset);
                int Count = BitConverter.ToInt32(Data, LengthOffset);
                int ValueOffset = LengthOffset + 4;

                if (Count > 200000)
                    ValueOffset = LengthOffset + 4;

                cachedList = new List<int>();
                for (int i = 0; i < Count; i++)
                {
                    int StoredValue = BitConverter.ToInt32(Data, ValueOffset + i * 4);
                    cachedList.Add(StoredValue);
                }
            }

            return cachedList;
        }

        protected List<uint> GetUIntList(int redirectionOffset, ref List<uint> cachedList)
        {
            if (cachedList == null)
            {
                int LengthOffset = /*Offset +*/ BitConverter.ToInt32(Data, Offset + redirectionOffset);
                int Count = BitConverter.ToInt32(Data, LengthOffset);
                int ValueOffset = LengthOffset + 4;

                cachedList = new List<uint>();
                for (int i = 0; i < Count; i++)
                {
                    uint StoredValue = BitConverter.ToUInt32(Data, ValueOffset + i * 4);
                    cachedList.Add(StoredValue);
                }
            }

            return cachedList;
        }

        protected List<string> GetStringList(int redirectionOffset, ref List<string> cachedList)
        {
            if (cachedList == null)
            {
                int LengthOffset = /*Offset +*/ BitConverter.ToInt32(Data, Offset + redirectionOffset);
                int Count = BitConverter.ToInt32(Data, LengthOffset);
                int ListOffset = LengthOffset + 4;

                cachedList = new List<string>();
                for (int i = 0; i < Count; i++)
                {
                    string StoredString = CreateString(ListOffset);
                    cachedList.Add(StoredString);

                    ListOffset += 2 + StoredString.Length * 2;
                }
            }

            return cachedList;
        }

        protected TList GetObjectList<T, TList>(int redirectionOffset, ref TList cachedList, PgObjectCreator<T> createNewObject, Func<TList> createNewList)
        {
            if (cachedList == null)
            {
                int LengthOffset = /*Offset +*/ BitConverter.ToInt32(Data, Offset + redirectionOffset);
                int Count = BitConverter.ToInt32(Data, LengthOffset);
                int ListOffset = LengthOffset + 4;

                cachedList = createNewList();
                System.Collections.IList asList = cachedList as System.Collections.IList;

                for (int i = 0; i < Count; i++)
                {
                    int StoredOffset = /*Offset +*/ BitConverter.ToInt32(Data, ListOffset + i * 4);

                    T Object;

                    if (CreatedObjectTable.ContainsKey(StoredOffset))
                        Object = (T)CreatedObjectTable[StoredOffset];
                    else
                    {
                        int TableOffset = StoredOffset;
                        Object = createNewObject(Data, ref StoredOffset);
                        CreatedObjectTable.Add(TableOffset, Object);
                    }

                    asList.Add(Object);
                }
            }

            return cachedList;
        }

        protected string CreateString(int offsetString)
        {
            if (offsetString < 0 || offsetString >= Data.Length)
                offsetString = 0;

            int Count = BitConverter.ToUInt16(Data, offsetString);
            int CharacterOffset = offsetString + 2;

            string Result = "";
            for (int i = 0; i < Count; i++)
            {
                if (CharacterOffset + i * 2 < 0 || CharacterOffset + i * 2 >= Data.Length)
                    offsetString = 0;

                char CharacterValue = BitConverter.ToChar(Data, CharacterOffset + i * 2);
                Result += CharacterValue;
            }

            return Result;
        }

        #region Implementation of IObjectContentGenerator
        public virtual void GenerateObjectContent(JsonGenerator Generator, bool openWithKey, bool openWithNullKey)
        {
            OpenGeneratorKey(Generator, openWithKey, openWithNullKey);
            ListAllObjectContent(Generator);
            CloseGeneratorKey(Generator, openWithKey, openWithNullKey);
        }

        public virtual void OpenGeneratorKey(JsonGenerator Generator, bool openWithKey, bool openWithNullKey)
        {
            if (Key != null && openWithKey)
                Generator.OpenObject(Key);
            else if (openWithNullKey)
                Generator.OpenObject(null);
        }

        public virtual void ListAllObjectContent(JsonGenerator Generator)
        {
            foreach (string ParserKey in GeneratorFieldTableOrder)
                ListObjectContent(Generator, ParserKey);
        }

        public virtual void ListObjectContent(JsonGenerator Generator, string ParserKey)
        {
            if (!GeneratorFieldTable.ContainsKey(ParserKey))
                ParserKey = null;
            if (ParserKey == null)
                ParserKey = null;

            FieldParser Parser = GeneratorFieldTable[ParserKey];

            IObjectContentGenerator Subitem;
            List<int> IntegerList;
            List<string> StringList;

            switch (Parser.Type)
            {
                default:
                    break;

                case FieldType.Unknown:
                    break;

                case FieldType.Integer:
                    Generator.AddInteger(ParserKey, Parser.GetInteger());
                    break;

                case FieldType.Bool:
                    Generator.AddBoolean(ParserKey, Parser.GetBool());
                    break;

                case FieldType.Float:
                    Generator.AddDouble(ParserKey, Parser.GetFloat());
                    break;

                case FieldType.String:
                    Generator.AddString(ParserKey, Parser.GetString());
                    break;

                case FieldType.Object:
                    Subitem = Parser.GetObject();
                    if (Subitem != null)
                        Subitem.GenerateObjectContent(Generator, true, false);
                    break;

                case FieldType.SimpleIntegerArray:
                case FieldType.IntegerArray:
                    IntegerList = Parser.GetIntegerArray();

                    if (Parser.SimplifyArray && IntegerList.Count == 1 && (Parser.GetArrayIsSimple == null || Parser.GetArrayIsSimple()))
                        Generator.AddInteger(ParserKey, IntegerList[0]);
                    else
                        Generator.AddIntegerList(ParserKey, IntegerList, Parser.GetArrayIsEmpty != null && Parser.GetArrayIsEmpty());
                    break;

                case FieldType.SimpleStringArray:
                case FieldType.StringArray:
                    if (Parser.GetStringArray == null)
                        StringList = null;
                    StringList = Parser.GetStringArray();
                    if (StringList == null)
                        StringList = null;

                    if (Parser.SimplifyArray && StringList.Count == 1 && (Parser.GetArrayIsSimple == null || Parser.GetArrayIsSimple()))
                        Generator.AddString(ParserKey, StringList[0]);
                    else
                        Generator.AddStringList(ParserKey, StringList, Parser.GetArrayIsEmpty != null && Parser.GetArrayIsEmpty());
                    break;

                case FieldType.ObjectArray:
                    IList ObjectArray = Parser.GetObjectArray() as IList;
                    bool IsListEmpty;
                    if (Parser.GetArrayIsEmpty != null)
                        IsListEmpty = Parser.GetArrayIsEmpty();
                    else
                        IsListEmpty = false;

                    if (ObjectArray == null)
                        ObjectArray = null;

                    if (ObjectArray.Count > 0 || IsListEmpty)
                    {
                        if (Parser.SimplifyArray && ObjectArray.Count == 1 && (Parser.GetArrayIsSimple == null || Parser.GetArrayIsSimple()) && ObjectArray[0] is IObjectContentGenerator FirstItem)
                        {
                            Generator.OpenObject(ParserKey);

                            FirstItem.GenerateObjectContent(Generator, false, false);

                            Generator.CloseObject();
                        }

                        else if (IsListEmpty)
                            Generator.AddEmptyArray(ParserKey);

                        else
                        {
                            Generator.OpenArray(ParserKey);
                            if (Parser.GetArrayIsNested != null && Parser.GetArrayIsNested())
                                Generator.OpenNestedArray();

                            foreach (IObjectContentGenerator Item in ObjectArray)
                                Item.GenerateObjectContent(Generator, false, true);

                            if (Parser.GetArrayIsNested != null && Parser.GetArrayIsNested())
                                Generator.CloseArray();
                            Generator.CloseArray();
                        }
                    }
                    break;
            }
        }

        public virtual void CloseGeneratorKey(JsonGenerator Generator, bool openWithKey, bool openWithNullKey)
        {
            if (Key != null && openWithKey)
                Generator.CloseObject();
            else if (openWithNullKey)
                Generator.CloseObject();
        }

        private Dictionary<string, FieldParser> GeneratorFieldTable { get { return FieldTable; } }
        private List<string> GeneratorFieldTableOrder { get { return FieldTableOrder; } }
        protected abstract Dictionary<string, FieldParser> FieldTable { get; }
        protected abstract List<string> FieldTableOrder { get; }
        #endregion
    }
}
