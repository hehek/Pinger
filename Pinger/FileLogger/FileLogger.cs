using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Pinger.FileLogger
{
    public class FileLogger : ILogger
    {
        private static readonly object Lock = new object();
        private readonly FileLoggerConfiguration _config;
        public FileLogger(FileLoggerConfiguration config)
        {
            _config = config;
        }
        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.LogLevel; 
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (Lock)
                {
                    File.AppendAllText(_config.Path, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
}
