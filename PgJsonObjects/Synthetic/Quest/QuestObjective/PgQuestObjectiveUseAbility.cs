﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveUseAbility : PgQuestObjective<PgQuestObjectiveUseAbility>, IPgQuestObjectiveUseAbility
    {
        public PgQuestObjectiveUseAbility(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveUseAbility CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveUseAbility CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveUseAbility(data, ref offset);
        }

        public AbilityCollection AbilityTargetList { get { return GetObjectList(PropertiesOffset + 0, ref _AbilityTargetList, AbilityCollection.CreateItem, () => new AbilityCollection()); } } private AbilityCollection _AbilityTargetList;
        public AbilityKeyword AbilityTarget { get { return GetEnum<AbilityKeyword>(PropertiesOffset + 4); } }

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
            { "Target", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<AbilityKeyword>.ToString(AbilityTarget) } },
        }; } }
    }
}
