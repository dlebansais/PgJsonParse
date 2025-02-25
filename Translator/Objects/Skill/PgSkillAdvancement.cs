namespace PgObjects
{
    using System.Collections.Generic;

    public abstract class PgSkillAdvancement
    {
        public bool IsActive { get; set; }
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; set; }
        public List<Race> RaceRestrictionList { get; set; } = new List<Race>();
    }
}
