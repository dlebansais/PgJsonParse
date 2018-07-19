using System.Collections.Generic;

namespace PgJsonObjects
{
    public class GiftSource : GenericSource<GiftSource>, IPgGiftSource
    {
        #region Init
        public GiftSource(string NpcName, IPgGameNpc Npc)
        {
            this.NpcName = NpcName;
            this.Npc = Npc;
        }

        protected override int Type { get { return (int)SourceTypes.Gift; } }
        #endregion

        #region Properties
        public string NpcName { get; private set; }
        public IPgGameNpc Npc { get; private set; }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();

            SerializeJsonObjectInternalProlog(data, ref offset);
            int BaseOffset = offset;

            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddString(NpcName, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddObject(Npc as ISerializableJsonObject, data, ref offset, BaseOffset, 4, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 8, StoredStringtable, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
