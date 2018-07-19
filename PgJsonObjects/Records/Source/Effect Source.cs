using System.Collections.Generic;

namespace PgJsonObjects
{
    public class EffectSource : GenericSource<EffectSource>, IPgEffectSource
    {
        #region Init
        public EffectSource(IPgEffect Effect)
        {
            this.Effect = Effect;
        }

        protected override int Type { get { return (int)SourceTypes.Effect; } }
        #endregion

        #region Properties
        public IPgEffect Effect { get; private set; }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            SerializeJsonObjectInternalProlog(data, ref offset);
            int BaseOffset = offset;

            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddObject(Effect as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 4, null, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
