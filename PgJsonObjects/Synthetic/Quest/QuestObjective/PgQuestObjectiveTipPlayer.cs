using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveTipPlayer : PgQuestObjective<PgQuestObjectiveTipPlayer>, IPgQuestObjectiveTipPlayer
    {
        public PgQuestObjectiveTipPlayer(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveTipPlayer CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveTipPlayer CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveTipPlayer(data, ref offset);
        }

        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get { return GetInt(PropertiesOffset + 0); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Type", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<QuestObjectiveType>.ToString(Type, null, QuestObjectiveType.Internal_None) } },
            { "MustCompleteEarlierObjectivesFirst", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawMustCompleteEarlierObjectivesFirst } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Description } },
            { "Number", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumber } },
            { "MinAmount", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RawMinAmount.ToString() } },
        }; } }
    }
}
