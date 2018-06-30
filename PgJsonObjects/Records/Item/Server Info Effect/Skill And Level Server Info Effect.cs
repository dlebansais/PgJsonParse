using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SkillAndLevelServerInfoEffect : ServerInfoEffect, IPgSkillAndLevelServerInfoEffect
    {
        public SkillAndLevelServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, PowerSkill RawSkill, int SkillLevel)
            : base(ServerInfoEffect, RawLevel)
        {
            this.RawSkill = RawSkill;
            this.RawSkillLevel = SkillLevel;
        }

        private PowerSkill RawSkill;
        private bool IsSkillParsed;
        public IPgSkill Skill { get; private set; }
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get; private set; }

        public override string RawEffect
        {
            get
            {
                return base.RawEffect + "(" + StringToEnumConversion<PowerSkill>.ToString(RawSkill) + "," + SkillLevel.ToString() + ")";
            }
        }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (RawSkill != PowerSkill.Internal_None)
                    Result += TextMaps.PowerSkillTextMap[RawSkill];

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        public override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            Skill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawSkill, Skill, ref IsSkillParsed, ref IsConnected, LinkBack);

            return IsConnected;
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddInt((int?)Type, data, ref offset, BaseOffset, 0);
            AddObject(Skill as ISerializableJsonObject, data, ref offset, BaseOffset, 4, StoredObjectTable);
            AddInt(RawSkillLevel, data, ref offset, BaseOffset, 8);
            AddStringList(new List<string>(), data, ref offset, BaseOffset, 12, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 16, null, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
