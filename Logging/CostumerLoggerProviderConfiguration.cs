namespace CompanyAPI.Logging {
    public class CostumerLoggerProviderConfiguration {
        public LogLevel logLevel { get; set; } = LogLevel.Warning;

        public int eventId { get; set; } = 0;
    }
}
