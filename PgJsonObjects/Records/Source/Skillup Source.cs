using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SkillupSource : GenericSource<SkillupSource>, IPgSkillupSource
    {
        #region Init
        public SkillupSource(IPgSkill Skill)
        {
            this.Skill = Skill;
        }

        protected override int Type { get { return (int)SourceTypes.AutomaticFromSkill; } }
        #endregion

        #region Properties
        public IPgSkill Skill { get; private set; }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            SerializeJsonObjectInternalProlog(data, ref offset);
            int BaseOffset = offset;

            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddObject(Skill as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 4, null, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
