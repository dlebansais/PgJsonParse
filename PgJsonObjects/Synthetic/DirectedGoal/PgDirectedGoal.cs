﻿namespace PgJsonObjects
{
    public class PgDirectedGoal : MainPgObject<PgDirectedGoal>, IPgDirectedGoal
    {
        public PgDirectedGoal(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 26;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgDirectedGoal CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgDirectedGoal CreateNew(byte[] data, ref int offset)
        {
            return new PgDirectedGoal(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get { return GetInt(4); } }
        public string Label { get { return GetString(8); } }
        public string Zone { get { return GetString(12); } }
        public string LargeHint { get { return GetString(16); } }
        public string SmallHint { get { return GetString(20); } }
        public int CategoryGateId { get { return RawCategoryGateId.HasValue ? RawCategoryGateId.Value : 0; } }
        public int? RawCategoryGateId { get { return GetInt(24); } }
        public bool IsCategoryGate { get { return RawIsCategoryGate.HasValue ? RawIsCategoryGate.Value : false; } }
        public bool? RawIsCategoryGate { get { return GetBool(28, 0); } }
    }
}
