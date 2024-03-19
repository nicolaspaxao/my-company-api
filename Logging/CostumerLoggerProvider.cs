using System.Collections.Concurrent;

namespace CompanyAPI.Logging {
    public class CostumerLoggerProvider : ILoggerProvider {

        readonly CostumerLoggerProviderConfiguration configuration;
        readonly ConcurrentDictionary<string, CostumerLogger> loggers = new ConcurrentDictionary<string, CostumerLogger> ();

        public CostumerLoggerProvider ( CostumerLoggerProviderConfiguration configuration ) {
            this.configuration = configuration;
        }

        public ILogger CreateLogger ( string categoryName ) {
            return loggers.GetOrAdd ( categoryName,name => new CostumerLogger(name, configuration));
        }

        public void Dispose () {
            loggers.Clear();
        }
    }
}
