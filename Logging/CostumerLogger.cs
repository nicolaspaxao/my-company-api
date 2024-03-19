
namespace CompanyAPI.Logging {
    public class CostumerLogger : ILogger {

        readonly string loggerName;
        readonly CostumerLoggerProviderConfiguration configuration;

        public CostumerLogger ( string loggerName , CostumerLoggerProviderConfiguration configuration ) {
            this.loggerName = loggerName;
            this.configuration = configuration;
        }

        public IDisposable? BeginScope<TState> ( TState state ) where TState : notnull {
            return null;
        }

        public bool IsEnabled ( LogLevel logLevel ) {
            return logLevel == configuration.logLevel;
        }

        public void Log<TState> ( LogLevel logLevel , EventId eventId , TState state , Exception? exception , Func<TState , Exception? , string> formatter ) {
            string message = $"{logLevel}: {eventId.Id} - {formatter(state, exception)}";
            WriteOnTextFile(message);
        }

        private void WriteOnTextFile(string message ) {
            string logPath = @"D:\Estudos\csharp-estudos\CompanyAPI\wwwroot\log\api_logs.txt";
            using (StreamWriter streamWriter = new StreamWriter( logPath, true )) {
                try {
                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                } catch (Exception) {
                    throw;
                }
            }
        }
    }
}
