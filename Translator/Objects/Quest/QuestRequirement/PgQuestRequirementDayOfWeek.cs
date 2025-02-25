using System.Collections.Generic;

namespace PgObjects
{
    public class PgQuestRequirementDayOfWeek : PgQuestRequirement
    {
        public List<DayOfWeek> DaysAllowedList { get; set; } = new List<DayOfWeek>();
    }
}
