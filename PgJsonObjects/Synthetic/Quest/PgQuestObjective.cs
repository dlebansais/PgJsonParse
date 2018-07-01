using System.Collections.Generic;

namespace PgJsonObjects
{
    public abstract class PgQuestObjective<TPg> : GenericPgObject<TPg>
        where TPg : IDeserializablePgObject
    {
        public const int PropertiesOffset = 32;

        public PgQuestObjective(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public QuestObjectiveType Type { get { return (QuestObjectiveType)GetInt(0).Value; } }
        public override string Key { get { return GetString(4); } }
        public string Description { get { return GetString(8); } }
        public int Number { get { return RawNumber.HasValue ? RawNumber.Value : 0; } }
        public int? RawNumber { get { return GetInt(12); } }
        public int MinHour { get { return RawMinHour.HasValue ? RawMinHour.Value : 0; } }
        public int? RawMinHour { get { return GetInt(16); } }
        public int MaxHour { get { return RawMaxHour.HasValue ? RawMaxHour.Value : 0; } }
        public int? RawMaxHour { get { return GetInt(20); } }
        public bool MustCompleteEarlierObjectivesFirst { get { return RawMustCompleteEarlierObjectivesFirst.HasValue && RawMustCompleteEarlierObjectivesFirst.Value; } }
        public bool? RawMustCompleteEarlierObjectivesFirst { get { return GetBool(24, 0); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(28, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
    }
}
