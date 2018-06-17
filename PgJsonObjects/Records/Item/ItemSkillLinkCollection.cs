﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemSkillLinkCollection : List<ItemSkillLink>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgItemSkillLink CreateItem(byte[] data, int offset)
        {
            return new PgItemSkillLink(data, offset);
        }
    }
}
