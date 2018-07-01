using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveRequirement : GenericPgObject<PgQuestObjectiveRequirement>, IPgQuestObjectiveRequirement
    {
        public PgQuestObjectiveRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveRequirement(data, ref offset);
        }

        public override string Key { get { return null; } }
        protected override List<string> FieldTableOrder { get { return new List<string>(); } }
        public string Type { get { return GetString(0); } }
        public int MinHour { get { return RawMinHour.HasValue ? RawMinHour.Value : 0; } }
        public int? RawMinHour { get { return GetInt(4); } }
        public int MaxHour { get { return RawMaxHour.HasValue ? RawMaxHour.Value : 0; } }
        public int? RawMaxHour { get { return GetInt(8); } }
        public EffectKeyword Keyword { get { return GetEnum<EffectKeyword>(12); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Type } },
            { "MinHour", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => MinHour } },
            { "MaxHour", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => MaxHour } },
            { "Keyword", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<EffectKeyword>.ToString(Keyword, null, EffectKeyword.Internal_None) } },
        }; } }
    }
}
