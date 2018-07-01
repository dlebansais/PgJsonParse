using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgDirectedGoal : MainPgObject<PgDirectedGoal>, IPgDirectedGoal
    {
        public PgDirectedGoal(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 34;
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
        protected override List<string> FieldTableOrder { get { return GetStringList(28, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public bool IsCategoryGate { get { return RawIsCategoryGate.HasValue ? RawIsCategoryGate.Value : false; } }
        public bool? RawIsCategoryGate { get { return GetBool(32, 0); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Id", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawId } },
            { "Label", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Label } },
            { "Zone", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Zone  } },
            { "IsCategoryGate", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsCategoryGate } },
            { "LargeHint", new FieldParser() {
                Type = FieldType.String,
                GetString = () => LargeHint } },
            { "SmallHint", new FieldParser() {
                Type = FieldType.String,
                GetString = () => SmallHint } },
            { "CategoryGateId", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawCategoryGateId } },
        }; } }
    }
}
