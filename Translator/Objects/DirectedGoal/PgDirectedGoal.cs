namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgDirectedGoal
    {
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get; set; }
        public string Label { get; set; } = string.Empty;
        public string Zone { get; set; } = string.Empty;
        public string LargeHint { get; set; } = string.Empty;
        public string SmallHint { get; set; } = string.Empty;
        public PgDirectedGoal CategoryGate { get; set; }
        public List<Race> ForRaceList { get; } = new List<Race>();
    }
}
