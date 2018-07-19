using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class LevelCapInteraction : GenericJsonObject<LevelCapInteraction>, IPgLevelCapInteraction
    {
        public LevelCapInteraction(PowerSkill OtherSkill, int OtherLevel, int Level)
        {
            this.OtherSkill = OtherSkill;
            RawOtherLevel = OtherLevel;
            RawLevel = Level;
            Link = null;
            IsParsed = false;
        }

        public int OtherLevel { get { return RawOtherLevel.Value; } }
        public int? RawOtherLevel { get; private set; }
        public int Level { get { return RawLevel.Value; } }
        public int? RawLevel { get; private set; }
        public IPgSkill Link { get; private set; }
        public bool IsParsed { get; private set; }
        public PowerSkill OtherSkill { get; private set; }

        public void SetLink(IPgSkill Link)
        {
            this.Link = Link;
            IsParsed = true;
        }


        public override string SortingName { get { return null; } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
        }; } }

        public override string TextContent { get { return ""; } }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            return false;
        }

        #region Debugging
        protected override string FieldTableName { get { return "LevelCapInteraction"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddInt(RawOtherLevel, data, ref offset, BaseOffset, 4);
            AddInt(RawLevel, data, ref offset, BaseOffset, 8);
            AddObject(Link as ISerializableJsonObject, data, ref offset, BaseOffset, 12, StoredObjectTable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 16, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 20, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
