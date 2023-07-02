namespace PgObjects
{
    using System;

    public class PgQuestTime
    {
        public int Days { get { return RawDays.HasValue ? RawDays.Value : 0; } }
        public int? RawDays { get; set; }
        public int Hours { get { return RawHours.HasValue ? RawHours.Value : 0; } }
        public int? RawHours { get; set; }
        public int Minutes { get { return RawMinutes.HasValue ? RawMinutes.Value : 0; } }
        public int? RawMinutes { get; set; }

        public TimeSpan? ToTime()
        {
            if (Days == 0 && Hours == 0 && Minutes == 0)
                return null;
            else
                return TimeSpan.FromDays(Days) + TimeSpan.FromHours(Hours) + TimeSpan.FromMinutes(Minutes);
        }
    }
}
