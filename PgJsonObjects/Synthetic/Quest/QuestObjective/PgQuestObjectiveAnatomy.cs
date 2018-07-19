using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveAnatomy : PgQuestObjective<PgQuestObjectiveAnatomy>, IPgQuestObjectiveAnatomy
    {
        public PgQuestObjectiveAnatomy(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveAnatomy CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveAnatomy CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveAnatomy(data, ref offset);
        }

        public IPgSkill Skill { get { return GetObject(PropertiesOffset + 0, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;

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
            { "AnatomyType", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Skill != null ? StringToEnumConversion<PowerSkill>.ToString(Skill.CombatSkill, null, PowerSkill.Internal_None).Substring(8) : null } },
        }; } }

        public override IList<IBackLinkable> GetLinkBack()
        {
            return new List<IBackLinkable>() { Skill };
        }

        public override IPgQuestObjectiveRequirement QuestObjectiveRequirement { get { return null; } }
    }
}
