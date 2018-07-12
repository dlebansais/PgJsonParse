using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PowerSimpleEffect : PowerEffect, IPgPowerSimpleEffect
    {
        public static readonly string IconIdPattern = "<icon=";

        public PowerSimpleEffect(string Description)
        {
            Description = Description.Trim();
            IconIdList = new List<int>();

            for(;;)
            {
                if (IconIdList.Count > 0 && Description.Contains(IconIdPattern))
                    Description = Description.Trim();

                if (Description.Length < IconIdPattern.Length)
                    break;

                if (!Description.StartsWith(IconIdPattern))
                    break;

                int EndIndex = Description.IndexOf('>');
                if (EndIndex < IconIdPattern.Length + 1)
                    break;

                string IdString = Description.Substring(IconIdPattern.Length, EndIndex - IconIdPattern.Length);

                int Id;
                if (!int.TryParse(IdString, out Id))
                    break;

                if (!IconIdList.Contains(Id))
                    IconIdList.Add(Id);

                Description = Description.Substring(EndIndex + 1);
            }

            this.Description = Description;
        }

        public override string Key { get { return null; } }

        public string Description { get; private set; }
        public List<int> IconIdList { get; private set; }

        public override string AsEffectString()
        {
            string Result = "";

            foreach (int Id in IconIdList)
                Result += IconIdPattern + Id.ToString() + ">";

            Result += Description;

            return Result;
        }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<int>> StoredIntListTable = new Dictionary<int, List<int>>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddInt(0, data, ref offset, BaseOffset, 0);
            AddString(Key, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddString(Description, data, ref offset, BaseOffset, 8, StoredStringtable);
            AddIntList(IconIdList, data, ref offset, BaseOffset, 12, StoredIntListTable);
            AddStringList(new List<string>(), data, ref offset, BaseOffset, 16, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 20, StoredStringtable, null, null, null, StoredIntListTable, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
