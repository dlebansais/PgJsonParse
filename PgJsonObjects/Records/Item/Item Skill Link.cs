using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemSkillLink : SerializableJsonObject, IPgItemSkillLink
    {
        public ItemSkillLink(string SkillName, int? SkillLevel)
        {
            this.SkillName = SkillName;
            RawSkillLevel = SkillLevel;
            Link = null;
            IsParsed = false;
        }

        public string SkillName { get; private set; }
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get; private set; }
        public IPgSkill Link { get; private set; }

        public bool HasLevel { get { return RawSkillLevel.HasValue && RawSkillLevel.Value > 0; } }
        public string ParsedLevel { get { return RawSkillLevel.HasValue && RawSkillLevel.Value > 0 ? RawSkillLevel.Value.ToString() : ""; } }
        public bool IsParsed { get; private set; }

        public void SetLink(IPgSkill Link)
        {
            this.Link = Link;
            IsParsed = true;
        }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddString(SkillName, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddInt(RawSkillLevel, data, ref offset, BaseOffset, 4);
            AddObject(Link as ISerializableJsonObject, data, ref offset, BaseOffset, 8, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 12, StoredStringtable, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
