namespace AniraSP.DAL.Domain.Sites {
    public class SiteSettings {
        public string Description { get; set; }
        public ReportSettings ReportSettings { get; set; }
        public ParsingSettings ParsingSettings { get; set; }
    }

    public class ReportSettings {
        public int StartReportHour { get; set; }
        public int StartReportMinute { get; set; }
    }

    public class ParsingSettings {
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int NumberOfTread { get; set; }
    }
}