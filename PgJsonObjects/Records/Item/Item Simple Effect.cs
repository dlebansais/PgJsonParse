using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemSimpleEffect : ItemEffect, IPgItemSimpleEffect
    {
        public ItemSimpleEffect(string Description)
        {
            this.Description = Description;
        }

        public string Description { get; private set; }

        public override string AsEffectString()
        {
            return Description;
        }

        public override bool Equals(object obj)
        {
            ItemSimpleEffect AsItemSimpleEffect;
            if ((AsItemSimpleEffect = obj as ItemSimpleEffect) != null)
                return AsItemSimpleEffect.Description == Description;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return Description.GetHashCode();
        }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddInt(0, data, ref offset, BaseOffset, 0);
            AddString(Description, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddStringList(new List<string>(), data, ref offset, BaseOffset, 8, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 12, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
