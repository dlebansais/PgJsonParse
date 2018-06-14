using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public abstract class GenericPgObject
    {
        public const int NoValueInt = 0x6B6B6B6B;

        public GenericPgObject(byte[] data, int offset)
        {
            Data = data;
            Offset = offset;
        }

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
            return (T)(object)BitConverter.ToUInt16(Data, Offset + valueOffset);
        }

        protected int? GetInt(int valueOffset)
        {
            int Value = BitConverter.ToInt32(Data, Offset + valueOffset);

            if (Value == NoValueInt)
                return null;
            else
                return Value;
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
            int StoredOffset = Offset + BitConverter.ToInt32(Data, Offset + redirectionOffset);
            string Result = CreateString(StoredOffset);
            return Result;
        }

        protected T GetObject<T>(int redirectionOffset, ref T cachedValue)
        {
            if (cachedValue == null)
            {
                int StoredOffset = Offset + BitConverter.ToInt32(Data, Offset + redirectionOffset);
                cachedValue = CreateObject<T>(StoredOffset);
            }

            return cachedValue;
        }

        protected List<bool> GetBoolList(int redirectionOffset, ref List<bool> cachedList)
        {
            if (cachedList == null)
            {
                int LengthOffset = Offset + BitConverter.ToInt32(Data, Offset + redirectionOffset);
                int Count = BitConverter.ToUInt16(Data, LengthOffset);
                int ValueOffset = LengthOffset + 2;

                cachedList = new List<bool>();
                for (int i = 0; i < Count; i++)
                {
                    bool StoredValue = (BitConverter.ToUInt16(Data, i * 2) != 0);
                    cachedList.Add(StoredValue);
                }
            }

            return cachedList;
        }

        protected List<T> GetEnumList<T>(int redirectionOffset, ref List<T> cachedList)
        {
            if (cachedList == null)
            {
                int LengthOffset = Offset + BitConverter.ToInt32(Data, Offset + redirectionOffset);
                int Count = BitConverter.ToUInt16(Data, LengthOffset);
                int ValueOffset = LengthOffset + 2;

                cachedList = new List<T>();
                for (int i = 0; i < Count; i++)
                {
                    int StoredValue = BitConverter.ToUInt16(Data, i * 2);
                    T Value = (T)(object)StoredValue;
                    cachedList.Add(Value);
                }
            }

            return cachedList;
        }

        protected List<int> GetIntList(int redirectionOffset, ref List<int> cachedList)
        {
            if (cachedList == null)
            {
                int LengthOffset = Offset + BitConverter.ToInt32(Data, Offset + redirectionOffset);
                int Count = BitConverter.ToInt32(Data, LengthOffset);
                int ValueOffset = LengthOffset + 4;

                cachedList = new List<int>();
                for (int i = 0; i < Count; i++)
                {
                    int StoredValue = BitConverter.ToInt32(Data, i * 4);
                    cachedList.Add(StoredValue);
                }
            }

            return cachedList;
        }

        protected List<string> GetStringList(int redirectionOffset, ref List<string> cachedList)
        {
            if (cachedList == null)
            {
                int LengthOffset = Offset + BitConverter.ToInt32(Data, Offset + redirectionOffset);
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

        protected List<T> GetObjectList<T>(int redirectionOffset, ref List<T> cachedList)
        {
            if (cachedList == null)
            {
                int LengthOffset = Offset + BitConverter.ToInt32(Data, Offset + redirectionOffset);
                int Count = BitConverter.ToInt32(Data, LengthOffset);
                int ListOffset = LengthOffset + 4;

                cachedList = new List<T>();
                for (int i = 0; i < Count; i++)
                {
                    int StoredOffset = Offset + BitConverter.ToInt32(Data, ListOffset + i * 4);

                    T Object = CreateObject<T>(StoredOffset);
                    cachedList.Add(Object);
                }
            }

            return cachedList;
        }

        protected T CreateObject<T>(int offsetObject)
        {
            if (typeof(T) == typeof(PgAbility))
                return (T)(object)(new PgAbility(Data, offsetObject));
            else
                return default(T);
        }

        protected string CreateString(int offsetString)
        {
            int Count = BitConverter.ToUInt16(Data, offsetString);
            int CharacterOffset = offsetString + 2;

            string Result = "";
            for (int i = 0; i < Count; i++)
            {
                char CharacterValue = BitConverter.ToChar(Data, i * 2);
                Result += CharacterValue;
            }

            return Result;
        }
    }
}
