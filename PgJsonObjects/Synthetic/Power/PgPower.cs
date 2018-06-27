﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPower : MainPgObject<PgPower>, IPgPower
    {
        public PgPower(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 20;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgPower CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgPower CreateNew(byte[] data, ref int offset)
        {
            return new PgPower(data, ref offset);
        }

        public string Prefix { get { return GetString(0); } }
        public string Suffix { get { return GetString(4); } }
        public List<ItemSlot> SlotList { get { return GetEnumList(8, ref _SlotList); } } private List<ItemSlot> _SlotList;
        public IPgSkill Skill { get { return GetObject(12, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;
        public bool IsUnavailable { get { return RawIsUnavailable.HasValue && RawIsUnavailable.Value; } }
        public bool? RawIsUnavailable { get { return GetBool(16, 0); } }
        public PowerSkill RawSkill { get { return GetEnum<PowerSkill>(18); } }
    }
}
