using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRewardCurrency : SerializableJsonObject, IPgQuestRewardCurrency
    {
        public int Amount { get { return RawAmount.HasValue ? RawAmount.Value : 0; } }
        public int? RawAmount { get; set; }
        public Currency Currency { get; set; }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;

            AddInt(RawAmount, data, ref offset, BaseOffset, 0);
            AddEnum(Currency, data, ref offset, BaseOffset, 4);

            FinishSerializing(data, ref offset, BaseOffset, 6, null, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
