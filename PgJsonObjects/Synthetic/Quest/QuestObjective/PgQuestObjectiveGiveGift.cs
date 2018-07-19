using Presentation;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveGiveGift : PgQuestObjective<PgQuestObjectiveGiveGift>, IPgQuestObjectiveGiveGift
    {
        public PgQuestObjectiveGiveGift(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveGiveGift CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveGiveGift CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveGiveGift(data, ref offset);
        }

        public float MinFavorReceived { get { return RawMinFavorReceived.HasValue ? RawMinFavorReceived.Value : 0; } }
        public float? RawMinFavorReceived { get { return (float)GetDouble(PropertiesOffset + 0); } }
        public float MaxFavorReceived { get { return RawMaxFavorReceived.HasValue ? RawMaxFavorReceived.Value : 0; } }
        public float? RawMaxFavorReceived { get { return (float)GetDouble(PropertiesOffset + 4); } }

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
            { "MinFavorReceived", new FieldParser() {
                Type = FieldType.String,
                GetString = () => FloatString(RawMinFavorReceived) } },
            { "MaxFavorReceived", new FieldParser() {
                Type = FieldType.String,
                GetString = () => FloatString(RawMaxFavorReceived) } },
        }; } }

        private string FloatString(float? value)
        {
            if (!value.HasValue)
                return null;

            string Result = InvariantCulture.SingleToString(value.Value, "F5");

            while (Result.Length > 1 && Result[Result.Length - 1] == '0')
                Result = Result.Substring(0, Result.Length - 1);

            return Result;
        }

        public override IPgQuestObjectiveRequirement QuestObjectiveRequirement { get { return null; } }
    }
}
