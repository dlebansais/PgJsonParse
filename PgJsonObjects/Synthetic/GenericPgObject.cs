using System;
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
            CreatedObjectTable.Add(TableOffset, Item);

            return Item;
        }
    }

    public abstract class GenericPgObject<TPg> : GenericPgObject, IGenericPgObject, IDeserializablePgObject
        where TPg : IDeserializablePgObject
    {
        public GenericPgObject(byte[] data, int offset)
        {
            Data = data;
            Offset = offset;
        }

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
            if (((Data[Offset + valueOffset] >> valueBit) & 0x1) != 0)
                return ((Data[Offset + valueOffset] >> (valueBit + 1)) & 0x1) != 0;
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
            int Count = BitConverter.ToUInt16(Data, offsetString);
            int CharacterOffset = offsetString + 2;

            string Result = "";
            for (int i = 0; i < Count; i++)
            {
                char CharacterValue = BitConverter.ToChar(Data, CharacterOffset + i * 2);
                Result += CharacterValue;
            }

            return Result;
        }
    }
}
