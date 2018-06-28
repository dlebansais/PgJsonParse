using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace PgJsonObjects
{
    public abstract class SerializableJsonObject : ISerializableJsonObject
    {
        protected static Dictionary<ISerializableJsonObject, int> SerializedObjectTable = new Dictionary<ISerializableJsonObject, int>();

        public static void ResetSerializedObjectTable()
        {
            SerializedObjectTable.Clear();
        }

        public static bool IsObjectSerialized(ISerializableJsonObject item)
        {
            return SerializedObjectTable.ContainsKey(item);
        }

        public virtual void SerializeJsonObject(byte[] data, ref int offset)
        {
            SerializedObjectTable.Add(this, offset);

            SerializeJsonObjectInternal(data, ref offset);
        }

        protected abstract void SerializeJsonObjectInternal(byte[] data, ref int offset);

        protected void AddBool(bool? value, byte[] data, ref int offset, ref int bitOffset, int baseOffset, int expectedOffset, int expectedBitOffset)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);
            Debug.Assert(bitOffset == expectedBitOffset);

            if (data != null)
            {
                int Mask = value.HasValue ? (0x01 | (value.Value ? 0x02 : 0)) : 0;
                Mask <<= bitOffset;

                data[offset] |= (byte)Mask;
            }

            bitOffset += 2;
            if (bitOffset == 16)
            {
                bitOffset = 0;
                offset += 2;
            }
        }

        protected void AddEnum<TObject>(TObject value, byte[] data, ref int offset, int baseOffset, int expectedOffset)
        {
            Debug.Assert(typeof(TObject).IsEnum);
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data != null)
            {
                byte[] valueData = BitConverter.GetBytes((UInt16)(int)(object)value);
                Array.Copy(valueData, 0, data, offset, 2);
            }

            offset += 2;
        }

        protected void AddInt(int? value, byte[] data, ref int offset, int baseOffset, int expectedOffset)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data != null)
            {
                int StoredValue = value.HasValue ? value.Value : GenericPgObject.NoValueInt;

                byte[] valueData = BitConverter.GetBytes(StoredValue);
                Array.Copy(valueData, 0, data, offset, 4);
            }

            offset += 4;
        }

        protected void AddUInt(uint? value, byte[] data, ref int offset, int baseOffset, int expectedOffset)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data != null)
            {
                uint StoredValue = value.HasValue ? value.Value : GenericPgObject.NoValueInt;

                byte[] valueData = BitConverter.GetBytes(StoredValue);
                Array.Copy(valueData, 0, data, offset, 4);
            }

            offset += 4;
        }

        protected void AddTimeSpan(TimeSpan? value, byte[] data, ref int offset, int baseOffset, int expectedOffset)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data != null)
            {
                int StoredValue = value.HasValue ? (int)value.Value.TotalSeconds : GenericPgObject.NoValueInt;

                byte[] valueData = BitConverter.GetBytes(StoredValue);
                Array.Copy(valueData, 0, data, offset, 4);
            }

            offset += 4;
        }

        protected void AddDouble(double? value, byte[] data, ref int offset, int baseOffset, int expectedOffset)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data != null)
            {
                float StoredValue = value.HasValue ? (float)value.Value : float.NaN;

                byte[] valueData = BitConverter.GetBytes(StoredValue);
                Array.Copy(valueData, 0, data, offset, 4);
            }

            offset += 4;
        }

        protected void AddString(string value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, string> StoredStringtable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            StoredStringtable.Add(offset, value);
            offset += 4;
        }

        protected void AddObject(ISerializableJsonObject value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, ISerializableJsonObject> StoredObjectTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            StoredObjectTable.Add(offset, value);
            offset += 4;
        }

        protected void AddBoolList(List<bool> value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, List<bool>> StoredBoolListTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            StoredBoolListTable.Add(offset, value);
            offset += 4;
        }

        protected void AddEnumList<TObject>(List<TObject> value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, IList> StoredEnumListTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            StoredEnumListTable.Add(offset, value);
            offset += 4;
        }

        protected void AddIntList(List<int> value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, List<int>> StoredIntListTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            StoredIntListTable.Add(offset, value);
            offset += 4;
        }

        protected void AddUIntList(List<uint> value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, List<uint>> StoredUIntListTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            StoredUIntListTable.Add(offset, value);
            offset += 4;
        }

        protected void AddStringList(List<string> value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, List<string>> StoredStringListTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            StoredStringListTable.Add(offset, value);
            offset += 4;
        }

        protected void AddObjectList(ISerializableJsonObjectCollection value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, ISerializableJsonObjectCollection> StoredObjectListTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            StoredObjectListTable.Add(offset, value);
            offset += 4;
        }

        protected void FinishSerializing(byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, string> StoredStringtable, Dictionary<int, ISerializableJsonObject> StoredObjectTable, Dictionary<int, List<bool>> StoredBoolListTable, Dictionary<int, IList> StoredEnumListTable, Dictionary<int, List<int>> StoredIntListTable, Dictionary<int, List<uint>> StoredUIntListTable, Dictionary<int, List<string>> StoredStringListTable, Dictionary<int, ISerializableJsonObjectCollection> StoredObjectListTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (StoredStringtable != null)
                foreach (KeyValuePair<int, string> Entry in StoredStringtable)
                    FinishSerializingString(data, ref offset, Entry.Key, Entry.Value);

            if (StoredObjectTable != null)
                foreach (KeyValuePair<int, ISerializableJsonObject> Entry in StoredObjectTable)
                    FinishSerializingObject(data, ref offset, Entry.Key, Entry.Value);

            if (StoredBoolListTable != null)
                foreach (KeyValuePair<int, List<bool>> Entry in StoredBoolListTable)
                    FinishSerializingBoolList(data, ref offset, Entry.Key, Entry.Value);

            if (StoredEnumListTable != null)
                foreach (KeyValuePair<int, IList> Entry in StoredEnumListTable)
                    FinishSerializingEnumList(data, ref offset, Entry.Key, Entry.Value);

            if (StoredIntListTable != null)
                foreach (KeyValuePair<int, List<int>> Entry in StoredIntListTable)
                    FinishSerializingIntList(data, ref offset, Entry.Key, Entry.Value);

            if (StoredUIntListTable != null)
                foreach (KeyValuePair<int, List<uint>> Entry in StoredUIntListTable)
                    FinishSerializingUIntList(data, ref offset, Entry.Key, Entry.Value);

            if (StoredStringListTable != null)
                foreach (KeyValuePair<int, List<string>> Entry in StoredStringListTable)
                    FinishSerializingStringList(data, ref offset, Entry.Key, Entry.Value);

            if (StoredObjectListTable != null)
                foreach (KeyValuePair<int, ISerializableJsonObjectCollection> Entry in StoredObjectListTable)
                    FinishSerializingObjectList(data, ref offset, Entry.Key, Entry.Value);
        }

        protected void FinishSerializingString(byte[] data, ref int offset, int redirectionOffset, string StringValue)
        {
            if (StringValue == null)
            {
                if (data != null)
                {
                    byte[] valueData = new byte[4];
                    Array.Copy(valueData, 0, data, redirectionOffset, 4);
                }
            }
            else
            {
                if (data != null)
                {
                    byte[] valueData = BitConverter.GetBytes(offset);
                    Array.Copy(valueData, 0, data, redirectionOffset, 4);
                }

                if (data != null)
                {
                    byte[] LengthData = BitConverter.GetBytes((UInt16)StringValue.Length);
                    Array.Copy(LengthData, 0, data, offset, 2);
                }

                offset += 2;

                for (int i = 0; i < StringValue.Length; i++)
                {
                    char CharacterValue = StringValue[i];

                    if (data != null)
                    {
                        byte[] CharacterData = BitConverter.GetBytes(CharacterValue);
                        Array.Copy(CharacterData, 0, data, offset, 2);
                    }

                    offset += 2;
                }
            }
        }

        protected void FinishSerializingObject(byte[] data, ref int offset, int redirectionOffset, ISerializableJsonObject ObjectValue)
        {
            if (ObjectValue == null)
            {
                if (data != null)
                {
                    byte[] valueData = new byte[4];
                    Array.Copy(valueData, 0, data, redirectionOffset, 4);
                }
            }
            else
            {
                if (IsObjectSerialized(ObjectValue))
                {
                    if (data != null)
                    {
                        int ObjectOffset = SerializedObjectTable[ObjectValue];

                        byte[] valueData = BitConverter.GetBytes(ObjectOffset);
                        Array.Copy(valueData, 0, data, redirectionOffset, 4);
                    }
                }
                else
                {
                    if (data != null)
                    {
                        byte[] valueData = BitConverter.GetBytes(offset);
                        Array.Copy(valueData, 0, data, redirectionOffset, 4);
                    }

                    ObjectValue.SerializeJsonObject(data, ref offset);
                }
            }
        }

        protected void FinishSerializingBoolList(byte[] data, ref int offset, int redirectionOffset, List<bool> BoolList)
        {
            if (data != null)
            {
                byte[] valueData = BitConverter.GetBytes(offset);
                Array.Copy(valueData, 0, data, redirectionOffset, valueData.Length);
            }

            if (data != null)
            {
                byte[] LengthData = BitConverter.GetBytes((UInt16)BoolList.Count);
                Array.Copy(LengthData, 0, data, offset, 2);
            }

            offset += 2;

            for (int i = 0; i < BoolList.Count; i++)
            {
                UInt16 BoolValue = (UInt16)(BoolList[i] ? 1 : 0);

                if (data != null)
                {
                    byte[] BoolData = BitConverter.GetBytes(BoolValue);
                    Array.Copy(BoolData, 0, data, offset, 2);
                }

                offset += 2;
            }
        }

        protected void FinishSerializingEnumList(byte[] data, ref int offset, int redirectionOffset, IList EnumList)
        {
            if (data != null)
            {
                byte[] valueData = BitConverter.GetBytes(offset);
                Array.Copy(valueData, 0, data, redirectionOffset, valueData.Length);
            }

            if (data != null)
            {
                byte[] LengthData = BitConverter.GetBytes((UInt16)EnumList.Count);
                Array.Copy(LengthData, 0, data, offset, 2);
            }

            offset += 2;

            for (int i = 0; i < EnumList.Count; i++)
            {
                UInt16 EnumValue = (UInt16)(int)EnumList[i];

                if (data != null)
                {
                    byte[] EnumData = BitConverter.GetBytes(EnumValue);
                    Array.Copy(EnumData, 0, data, offset, 2);
                }

                offset += 2;
            }
        }

        protected void FinishSerializingIntList(byte[] data, ref int offset, int redirectionOffset, List<int> IntList)
        {
            if (data != null)
            {
                byte[] valueData = BitConverter.GetBytes(offset);
                Array.Copy(valueData, 0, data, redirectionOffset, valueData.Length);
            }

            if (data != null)
            {
                byte[] LengthData = BitConverter.GetBytes(IntList.Count);
                Array.Copy(LengthData, 0, data, offset, 4);
            }

            offset += 4;

            for (int i = 0; i < IntList.Count; i++)
            {
                int IntValue = IntList[i];

                if (data != null)
                {
                    byte[] IntData = BitConverter.GetBytes(IntValue);
                    Array.Copy(IntData, 0, data, offset, 4);
                }

                offset += 4;
            }
        }

        protected void FinishSerializingUIntList(byte[] data, ref int offset, int redirectionOffset, List<uint> UIntList)
        {
            if (data != null)
            {
                byte[] valueData = BitConverter.GetBytes(offset);
                Array.Copy(valueData, 0, data, redirectionOffset, valueData.Length);
            }

            if (data != null)
            {
                byte[] LengthData = BitConverter.GetBytes(UIntList.Count);
                Array.Copy(LengthData, 0, data, offset, 4);
            }

            offset += 4;

            for (int i = 0; i < UIntList.Count; i++)
            {
                uint UIntValue = UIntList[i];

                if (data != null)
                {
                    byte[] IntData = BitConverter.GetBytes(UIntValue);
                    Array.Copy(IntData, 0, data, offset, 4);
                }

                offset += 4;
            }
        }

        protected void FinishSerializingStringList(byte[] data, ref int offset, int redirectionOffset, List<string> StringList)
        {
            if (data != null)
            {
                byte[] valueData = BitConverter.GetBytes(offset);
                Array.Copy(valueData, 0, data, redirectionOffset, valueData.Length);
            }

            if (data != null)
            {
                byte[] LengthData = BitConverter.GetBytes(StringList.Count);
                Array.Copy(LengthData, 0, data, offset, 4);
            }

            offset += 4;

            for (int i = 0; i < StringList.Count; i++)
            {
                string StringValue = StringList[i];

                if (data != null)
                {
                    byte[] LengthData = BitConverter.GetBytes((UInt16)StringValue.Length);
                    Array.Copy(LengthData, 0, data, offset, 2);
                }

                offset += 2;

                for (int j = 0; j < StringValue.Length; j++)
                {
                    char CharacterValue = StringValue[j];

                    if (data != null)
                    {
                        byte[] CharacterData = BitConverter.GetBytes(CharacterValue);
                        Array.Copy(CharacterData, 0, data, offset, 2);
                    }

                    offset += 2;
                }
            }
        }

        protected void FinishSerializingObjectList(byte[] data, ref int offset, int redirectionOffset, ISerializableJsonObjectCollection ObjectList)
        {
            if (data != null)
            {
                byte[] valueData = BitConverter.GetBytes(offset);
                Array.Copy(valueData, 0, data, redirectionOffset, valueData.Length);
            }

            if (data != null)
            {
                byte[] LengthData = BitConverter.GetBytes(ObjectList.Count);
                Array.Copy(LengthData, 0, data, offset, 4);
            }

            offset += 4;
            int ListOffset = offset;
            offset += ObjectList.Count * 4;

            for (int i = 0; i < ObjectList.Count; i++)
            {
                ISerializableJsonObject ObjectValue = ObjectList[i] as ISerializableJsonObject;
                int ObjectOffset;

                if (IsObjectSerialized(ObjectValue))
                    ObjectOffset = SerializedObjectTable[ObjectValue];
                else
                {
                    ObjectOffset = offset;
                    ObjectValue.SerializeJsonObject(data, ref offset);
                }

                if (data != null)
                {
                    byte[] valueData = BitConverter.GetBytes(ObjectOffset);
                    Array.Copy(valueData, 0, data, ListOffset + i * 4, 4);
                }
            }
        }

        protected void CloseBool(ref int offset, ref int bitOffset)
        {
            if (bitOffset > 0)
                offset += 2;

            bitOffset = 0;
        }

        public static void AlignSerializedLength(ref int offset)
        {
            offset = ((offset + 3) / 4) * 4;
        }
    }
}
