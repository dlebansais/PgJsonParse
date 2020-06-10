namespace PgJsonObjects
{
    public class PgReport
    {
        public int ReportLevel { get { return RawReportLevel.HasValue ? RawReportLevel.Value : 0; } }
        public int? RawReportLevel { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
