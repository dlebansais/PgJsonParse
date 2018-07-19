using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemSource : GenericSource<ItemSource>, IPgItemSource
    {
        #region Init
        public ItemSource(IPgItem Item)
        {
            this.Item = Item;
        }

        protected override int Type { get { return (int)SourceTypes.Item; } }
        #endregion

        #region Properties
        public IPgItem Item { get; private set; }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            SerializeJsonObjectInternalProlog(data, ref offset);
            int BaseOffset = offset;

            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddObject(Item as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 4, null, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
