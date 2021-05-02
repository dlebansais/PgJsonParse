﻿namespace PgObjects
{
    using System.Collections.Generic;

    public class PgDirectedGoal : PgObject
    {
        public string Key { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Zone { get; set; } = string.Empty;
        public PgDirectedGoal CategoryGate { get; set; } = null!;
        public string LargeHint { get; set; } = string.Empty;
        public string SmallHint { get; set; } = string.Empty;
        public List<Race> ForRaceList { get; set; } = new List<Race>();

        public override int ObjectIconId { get { return DirectedGoalIconId; } }
        public override string ObjectName { get { return Label; } }
        public override string ToString() { return Label; }
    }
}
