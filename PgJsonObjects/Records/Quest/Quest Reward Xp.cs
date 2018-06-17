using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRewardXp : SerializableJsonObject, IPgQuestRewardXp
    {
        public Skill Skill { get; set; }
        public int Xp { get { return RawXp.HasValue ? RawXp.Value : 0; } }
        public int? RawXp { get; set; }

        public PowerSkill RawSkill { get; set; }
        public bool IsSkillParsed { get; set; }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddObject(Skill, data, ref offset, BaseOffset, 0, StoredObjectTable);
            AddInt(RawXp, data, ref offset, BaseOffset, 4);

            FinishSerializing(data, ref offset, BaseOffset, 8, null, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
