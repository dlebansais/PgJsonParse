using System;

namespace PgJsonObjects
{
    public abstract class MainJsonObject<T> : GenericJsonObject<T>, IMainJsonObject
        where T : class
    {
        public virtual void SerializeJsonMainObject(byte[] data, ref int offset, int objectOffset)
        {
            if (IsObjectSerialized(this))
            {
                int AlreadySerializedObjectOffset = SerializedObjectTable[this];

                if (data != null)
                {
                    byte[] valueData = BitConverter.GetBytes(AlreadySerializedObjectOffset);
                    Array.Copy(valueData, 0, data, objectOffset, 4);
                }
            }
            else
            {
                SerializedObjectTable.Add(this, offset);

                if (offset == 0x9AF934)
                    offset = 0x9AF934;

                if (data != null)
                {
                    byte[] valueData = BitConverter.GetBytes(offset);
                    Array.Copy(valueData, 0, data, objectOffset, 4);
                }

                SerializeJsonObjectInternal(data, ref offset);
            }
        }
    }
}
