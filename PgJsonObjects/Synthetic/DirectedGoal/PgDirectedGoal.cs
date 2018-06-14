namespace PgJsonObjects
{
    public class PgDirectedGoal : GenericPgObject, IPgDirectedGoal
    {
        public PgDirectedGoal(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get { return GetInt(0); } }
        public string Label { get { return GetString(4); } }
        public string Zone { get { return GetString(8); } }
        public string LargeHint { get { return GetString(12); } }
        public string SmallHint { get { return GetString(16); } }
        public int CategoryGateId { get { return RawCategoryGateId.HasValue ? RawCategoryGateId.Value : 0; } }
        public int? RawCategoryGateId { get { return GetInt(20); } }
        public bool IsCategoryGate { get { return RawIsCategoryGate.HasValue ? RawIsCategoryGate.Value : false; } }
        public bool? RawIsCategoryGate { get { return GetBool(24, 0); } }
    }
}
