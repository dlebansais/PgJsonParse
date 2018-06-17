using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPower : MainPgObject<PgPower>, IPgPower
    {
        public PgPower(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgPower CreateItem(byte[] data, int offset)
        {
            return new PgPower(data, offset);
        }

        public string Prefix { get { return GetString(0); } }
        public string Suffix { get { return GetString(4); } }
        public List<ItemSlot> SlotList { get { return GetEnumList(8, ref _SlotList); } } private List<ItemSlot> _SlotList;
        public Skill Skill { get { return GetObject(12, ref _Skill); } } private Skill _Skill;
        public bool IsUnavailable { get { return RawIsUnavailable.HasValue && RawIsUnavailable.Value; } }
        public bool? RawIsUnavailable { get { return GetBool(16, 0); } }
        public PowerSkill RawSkill { get { return GetEnum<PowerSkill>(18); } }
    }
}
