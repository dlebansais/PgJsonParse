using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAIAbility : GenericPgObject<PgAIAbility>, IPgAIAbility
    {
        public PgAIAbility(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAIAbility CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAIAbility CreateNew(byte[] data, ref int offset)
        {
            return new PgAIAbility(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public int? RawMinLevel { get { return GetInt(4); } }
        public int? RawMaxLevel { get { return GetInt(8); } }
        public int? RawMinDistance { get { return GetInt(12); } }
        public int? RawMinRange { get { return GetInt(16); } }
        public int? RawMaxRange { get { return GetInt(20); } }
        public int? RawCueValue { get { return GetInt(24); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(28, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public AbilityCue Cue { get { return GetEnum<AbilityCue>(32); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "minLevel", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawMinLevel } },
            { "maxLevel", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawMaxLevel } },
            { "minDistance", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawMinDistance } },
            { "minRange", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawMinRange } },
            { "maxRange", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawMaxRange } },
            { "cue", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<AbilityCue>.ToString(Cue, null, AbilityCue.Internal_None) } },
            { "cueVal", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawCueValue } },
        }; } }

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion
    }
}
