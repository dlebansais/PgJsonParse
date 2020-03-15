﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRewardLevel : SerializableJsonObject, IPgQuestRewardLevel
    {
        public IPgSkill Skill { get; set; }
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; set; }

        public PowerSkill RawSkill { get; set; }
        public bool IsSkillParsed { get; set; }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddObject(Skill as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);
            AddInt(RawLevel, data, ref offset, BaseOffset, 4);
            AddStringList(new List<string>(), data, ref offset, BaseOffset, 8, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 12, null, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}