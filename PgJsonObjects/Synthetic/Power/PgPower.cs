using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPower : GenericPgObject, IPgPower
    {
        public PgPower(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string Prefix { get { return GetString(0); } }
        public string Suffix { get { return GetString(4); } }
        public List<ItemSlot> SlotList { get { return GetEnumList(8, ref _SlotList); } } private List<ItemSlot> _SlotList;
        public Skill ConnectedSkill { get { return GetObject(12, ref _ConnectedSkill); } } private Skill _ConnectedSkill;
        public bool IsUnavailable { get { return RawIsUnavailable.HasValue && RawIsUnavailable.Value; } }
        public bool? RawIsUnavailable { get { return GetBool(16, 0); } }
    }
}
