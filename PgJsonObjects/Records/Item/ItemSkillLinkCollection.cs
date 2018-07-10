using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemSkillLinkCollection : List<IPgItemSkillLink>, ISerializableJsonObjectCollection
    {
        /*public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }*/

        public static PgItemSkillLink CreateItem(byte[] data, ref int offset)
        {
            return new PgItemSkillLink(data, ref offset);
        }
    }
}
