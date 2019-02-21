using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveUseAbilityOnTargets : PgQuestObjective<PgQuestObjectiveUseAbilityOnTargets>, IPgQuestObjectiveUseAbilityOnTargets
    {
        public PgQuestObjectiveUseAbilityOnTargets(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveUseAbilityOnTargets CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveUseAbilityOnTargets CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveUseAbilityOnTargets(data, ref offset);
        }

        public IPgAbilityCollection AbilityList { get { return GetObjectList(PropertiesOffset + 0, ref _AbilityList, PgAbilityCollection.CreateItem, () => new PgAbilityCollection()); } } private IPgAbilityCollection _AbilityList;
        public AbilityKeyword Keyword { get { return GetEnum<AbilityKeyword>(PropertiesOffset + 4); } }

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
            { "AbilityKeyword", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<AbilityKeyword>.ToString(Keyword) } },
        }; } }

        public override IList<IBackLinkable> GetLinkBack()
        {
            List<IBackLinkable> Result = new List<IBackLinkable>();

            if (AbilityList != null)
                Result.AddRange(AbilityList);

            return Result;
        }

        public override IPgQuestObjectiveRequirement QuestObjectiveRequirement { get { return null; } }
    }
}
