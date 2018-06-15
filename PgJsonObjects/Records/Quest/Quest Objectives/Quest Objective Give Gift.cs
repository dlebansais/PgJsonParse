using Presentation;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveGiveGift : QuestObjective, IPgQuestObjectiveGiveGift
    {
        #region Init
        public QuestObjectiveGiveGift(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, float? RawMinFavorReceived, float? RawMaxFavorReceived)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.RawMinFavorReceived = RawMinFavorReceived;
            this.RawMaxFavorReceived = RawMaxFavorReceived;
        }

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
        #endregion

        #region Properties
        public float MinFavorReceived { get { return RawMinFavorReceived.HasValue ? RawMinFavorReceived.Value : 0; } }
        public float? RawMinFavorReceived { get; private set; }
        public float MaxFavorReceived { get { return RawMaxFavorReceived.HasValue ? RawMaxFavorReceived.Value : 0; } }
        public float? RawMaxFavorReceived { get; private set; }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;

            AddDouble(RawMinFavorReceived, data, ref offset, BaseOffset, 0);
            AddDouble(RawMaxFavorReceived, data, ref offset, BaseOffset, 4);

            FinishSerializing(data, ref offset, BaseOffset, 8, null, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
