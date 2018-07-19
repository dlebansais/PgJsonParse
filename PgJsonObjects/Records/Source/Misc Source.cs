namespace PgJsonObjects
{
    public class MiscSource : GenericSource<MiscSource>, IPgMiscSource
    {
        protected override int Type { get { return -1; } }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            SerializeJsonObjectInternalProlog(data, ref offset);

            FinishSerializing(data, ref offset, offset, 0, null, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
