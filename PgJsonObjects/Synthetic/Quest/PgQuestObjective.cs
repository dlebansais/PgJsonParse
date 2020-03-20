using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public abstract class PgQuestObjective<TPg> : GenericPgObject<TPg>, IPgQuestObjective
        where TPg : IDeserializablePgObject
    {
        public const int PropertiesOffset = 28;

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
        public int GroupId { get { return RawGroupId.HasValue ? RawGroupId.Value : 0; } }
        public int? RawGroupId { get { return GetInt(24); } }
        public abstract IPgQuestObjectiveRequirement QuestObjectiveRequirement { get; }

        public void CopyFieldTableOrder(string key, List<string> fieldTableOrder)
        {
        }

        public bool Connect(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            return false;
        }

        public override string SortingName { get { return null; } }

        public virtual IList<IBackLinkable> GetLinkBack()
        {
            return null;
        }

        public bool HasNumber { get { return RawNumber.HasValue && RawNumber.Value != 1; } }
        public bool HasMinAndMaxHours { get { return QuestObjectiveRequirement != null && QuestObjectiveRequirement.RawMinHour.HasValue && QuestObjectiveRequirement.RawMaxHour.HasValue; } }
        public string TimeCompletion
        {
            get
            {
                if (QuestObjectiveRequirement == null || !QuestObjectiveRequirement.RawMinHour.HasValue || !QuestObjectiveRequirement.RawMaxHour.HasValue)
                    return null;

                return "(this step must be completed between " + QuestObjectiveRequirement.RawMinHour.Value.ToString("D02") + ":00" + " and " + QuestObjectiveRequirement.RawMaxHour.Value.ToString("D02") + ":00" + ")";
            }
        }
    }
}
