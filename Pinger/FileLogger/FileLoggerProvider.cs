using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Pinger.FileLogger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly FileLoggerConfiguration _config;

        internal readonly ConcurrentDictionary<string, Pinger.FileLogger.FileLogger> Loggers =
            new ConcurrentDictionary<string, Pinger.FileLogger.FileLogger>();

        public FileLoggerProvider(FileLoggerConfiguration config)
        {
            _config = config;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new Pinger.FileLogger.FileLogger(_config);
        }

        public void Dispose() => Loggers.Clear();
    }
}
