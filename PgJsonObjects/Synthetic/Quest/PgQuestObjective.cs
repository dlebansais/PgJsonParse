using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public abstract class PgQuestObjective<TPg> : GenericPgObject<TPg>, IPgQuestObjective
        where TPg : IDeserializablePgObject
    {
        public const int PropertiesOffset = 24;

        public PgQuestObjective(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public QuestObjectiveType Type { get { return (QuestObjectiveType)GetInt(0).Value; } }
        public override string Key { get { return GetString(4); } }
        public string Description { get { return GetString(8); } }
        public int Number { get { return RawNumber.HasValue ? RawNumber.Value : 0; } }
        public int? RawNumber { get { return GetInt(12); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(16, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public bool MustCompleteEarlierObjectivesFirst { get { return RawMustCompleteEarlierObjectivesFirst.HasValue && RawMustCompleteEarlierObjectivesFirst.Value; } }
        public bool? RawMustCompleteEarlierObjectivesFirst { get { return GetBool(20, 0); } }

        public Quest ParentQuest { get; private set; }

        public void CopyFieldTableOrder(string key, List<string> fieldTableOrder)
        {
        }

        public bool Connect(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            return false;
        }
    }
}
