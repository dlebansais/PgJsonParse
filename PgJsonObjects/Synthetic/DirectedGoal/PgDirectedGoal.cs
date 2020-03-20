using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgDirectedGoal : MainPgObject<PgDirectedGoal>, IPgDirectedGoal
    {
        public PgDirectedGoal(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 36;
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

        public override void Init()
        {
            AddLinkBack(CategoryGate);
        }

        public override string Key { get { return GetString(0); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(4, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get { return GetInt(8); } }
        public string Label { get { return GetString(12); } }
        public string Zone { get { return GetString(16); } }
        public string LargeHint { get { return GetString(20); } }
        public string SmallHint { get { return GetString(24); } }
        public IPgDirectedGoal CategoryGate { get { return GetObject(28, ref _CategoryGate, PgDirectedGoal.CreateNew); } } private IPgDirectedGoal _CategoryGate;
        public List<Race> ForRaceList { get { return GetEnumList(32, ref _ForRaceList); } } private List<Race> _ForRaceList;

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
                GetBool = () => CategoryGate != null ? null : (bool?)true} },
            { "LargeHint", new FieldParser() {
                Type = FieldType.String,
                GetString = () => LargeHint } },
            { "SmallHint", new FieldParser() {
                Type = FieldType.String,
                GetString = () => SmallHint } },
            { "CategoryGateId", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => CategoryGate != null ? CategoryGate.RawId : null } },
            { "ForRaces", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<Race>.ToStringList(ForRaceList) } },
        }; } }

        public override string SortingName { get { return Label; } }
        public string SearchResultIconFileName { get { return "icon_" + DirectedGoal.SearchResultIconId; } }
    }
}
