using System;

namespace PgJsonObjects
{
    public abstract class MainJsonObject<T> : GenericJsonObject<T>, IMainJsonObject
        where T : class
    {
        public virtual void SerializeJsonMainObject(byte[] data, ref int offset)
        {
            if (IsObjectSerialized(this))
            {
                int ObjectOffset = SerializedObjectTable[this];

                if (data != null)
                {
                    byte[] valueData = BitConverter.GetBytes(ObjectOffset);
                    Array.Copy(valueData, 0, data, offset, 4);
                }

                offset += 4;
            }
            else
            {
                SerializedObjectTable.Add(this, offset);

                if (data != null)
                {
                    byte[] valueData = new byte[4];
                    Array.Copy(valueData, 0, data, offset, 4);
                }

                offset += 4;

                SerializeJsonObjectInternal(data, ref offset);
            }
        }
    }
}
